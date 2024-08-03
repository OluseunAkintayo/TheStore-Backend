using TheStore.Models;
using TheStore.Models.StockModel;
namespace TheStore.Services.ProductService;

public class ProductService {
  private readonly RepoService repo;
  private readonly ProductCodeService codeService;
  private readonly UploadService uploadService;
  public ProductService(RepoService repoService, ProductCodeService _codeService, UploadService _uploadService){
    repo = repoService;
    codeService = _codeService;
    uploadService = _uploadService;
  }

  public AdminProductResponse GetAdminProducts() {
    var products = (
      from product in repo.Products
      join brand in repo.Brands on product.BrandId equals brand.BrandId
      join category in repo.Categories on product.CategoryId equals category.CategoryId
      join stock in repo.Stocks on product.Id equals stock.ProductId
      select new AdminProduct() {
        Id = product.Id,
        ProductCode = product.ProductCode,
        ProductName = product.ProductName,
        ProductDescription = product.ProductDescription,
        Cost = product.Cost,
        Price = product.Price,
        StockId = stock.StockId,
        Quantity = stock.Quantity,
        ReorderLevel = stock.ReorderLevel,
        BrandId = brand.BrandId,
        BrandName = brand.BrandName,
        CategoryId = category.CategoryId,
        CategoryName = category.CategoryName,
        Pictures = product.Pictures,
        IsActive = product.IsActive,
        Deleted = product.Deleted,
        CreatedBy = product.CreatedBy,
        ModifiedAt = product.ModifiedAt
      }
    ).Where(item => !item.Deleted).ToList();

    if(products == null) {
      return new AdminProductResponse() {
        Success = false,
        Message = "Error fetching products",
      };
    }

    return new AdminProductResponse() {
      Success = true,
      Message = "Fetched products successfully",
      Data = products
    };
  }
  
  public ProductResponse GetProducts() {
    List<Product> Products = repo.Products.Where(item => !item.Deleted).ToList();
    if(Products == null) {
      return new ProductResponse() {
        Success = false,
        Message = "Error fetching products",
      };
    }

    return new ProductResponse() {
      Success = true,
      Message = "Fetched products successfully",
      Data = Products
    };
  }

  public ProductItemResponse NewProduct(ProductDTO productDto, Guid userId) {
    if (productDto.Cost > productDto.Price) {
      var error = new ProductItemResponse() {
        Success = false,
        Message = $"Pricing Error: Product cost cannot be greater than the price.",
      };
      return error;
    }

    DateTime now = DateTime.UtcNow;
    Product newProduct = new() {
      ProductCode = productDto.ProductCode,
      ProductName = productDto.ProductName,
      ProductDescription = productDto.Description,
      BrandId = productDto.BrandId,
      CategoryId = productDto.CategoryId,
      Cost = (decimal)productDto.Cost,
      Price = (decimal)productDto.Price,
      Pictures = productDto.Pictures,
      CreatedAt = now,
      IsActive = true,
      CreatedBy = userId,
      Deleted = false,
    };

    repo.Products.Add(newProduct);
    
    Stock stock = new() {
      ProductId = newProduct.Id,
      CostPrice = newProduct.Cost,
      CreatedBy = userId,
      CreatedAt = now,
      Quantity = 0,
      ReorderLevel = 0
    };

    repo.Stocks.Add(stock);
    newProduct.StockId = stock.StockId;
    repo.SaveChanges();

    ProductItemResponse response = new() {
      Data = newProduct,
      Success = true,
      Message = "The product was created successfully",
    };
    return response;
  }

  public ProductItemResponse GetProduct(Guid id) {
    Product? product = repo.Products.Find(id);
    if (product == null) {
      return new ProductItemResponse {
        Success = false,
        Message = $"Product not found",
      };
    }

    return new ProductItemResponse() {
      Data = product,
      Success = true,
      Message = "The product was retrieved successfully",
    };
  }

  public ProductItemResponse UpdateProduct(Guid id, EditProductDTO editProductDto) {
    Product? product = repo.Products.FirstOrDefault(item => item.Id == id);
    if (product == null) {
      return new ProductItemResponse {
        Success = false,
        Message = "Product not found",
      };
    }

    product.ProductName = editProductDto.ProductName;
    product.BrandId = editProductDto.BrandId;
    product.CategoryId = editProductDto.CategoryId;
    product.Cost = editProductDto.Cost;
    product.Price = editProductDto.Price;
    product.IsActive = editProductDto.IsActive;
    product.ProductDescription = editProductDto.Description;
    product.Pictures = editProductDto.Pictures;
    product.ModifiedAt = DateTime.UtcNow;

    repo.SaveChanges();
    
    return new ProductItemResponse {
      Success = true,
      Message = "The product has been updated",
      Data = product
    };
  }

  public ProductItemResponse DeleteProduct(Guid Id, Guid userId) {
    var product = repo.Products.Find(Id);
    if(product == null) {
      ProductItemResponse error = new() {
        Message = "Product not found",
        Success = false
      };
      return error;
    }
    product.IsActive = false;
    product.Deleted = true;
    product.ModifiedAt = DateTime.UtcNow;
    product.ModifiedBy = userId;
    
    // repo.Products.Remove(product);
    repo.SaveChanges();
    ProductItemResponse response = new() {
      Success = true,
      Message = "Product deleted successfully",
    };
    return response;
  }
}
