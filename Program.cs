using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using TheStore.Services;
using TheStore.Services.BrandService;
using TheStore.Services.CategoryService;
using TheStore.Services.ProductService;
using TheStore.Services.StockService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<RepoService>(option => {
  option.UseNpgsql(builder.Configuration.GetConnectionString("PgDatabase"));
});

builder.Services.AddSwaggerGen(options => {
  options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme {
    In  = ParameterLocation.Header,
    Name = "Authorization",
    Type = SecuritySchemeType.ApiKey
  });
  options.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services.AddAuthentication(auth => {
  auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
  auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
  auth.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options => {
  options.TokenValidationParameters = new TokenValidationParameters {
    ValidateIssuer = true,
    ValidateAudience = true,
    ValidateLifetime = true,
    ValidateIssuerSigningKey = true,
    ValidIssuer = builder.Configuration["Jwt:Issuer"],
    ValidAudience = builder.Configuration["Jwt:Audience"],
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
  };
});
builder.Services.AddAuthorization();
builder.Services.AddMvc();
builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();

string[] allowedLocations = builder.Configuration.GetSection("AllowedCorsOrigins").Get<string[]>()!;
builder.Services.AddCors(options => {
  options.AddPolicy("AllowLocalOrigin", policy => {
    policy.WithOrigins(allowedLocations).AllowAnyHeader().AllowAnyMethod();
  });
});

builder.Services.AddTransient<BrandService>();
builder.Services.AddTransient<CategoryService>();
builder.Services.AddTransient<ProductCodeService>();
builder.Services.AddTransient<ProductService>();
builder.Services.AddTransient<StockService>();
builder.Services.AddTransient<AuthService>();
builder.Services.AddTransient<EmailService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseForwardedHeaders(new ForwardedHeadersOptions {
  ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseCors("AllowLocalOrigin");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
