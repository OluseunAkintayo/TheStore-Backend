using Microsoft.EntityFrameworkCore;
using TheStore.Models;
namespace TheStore.Services.ProductService;

public class ProductService {
  private readonly RepoService repo;
  private readonly ProductCodeService codeService;
  public ProductService(RepoService repoService, ProductCodeService _codeService){
    repo = repoService;
    codeService = _codeService;
  }

  public ProductResponse GetProducts() {
    List<Product> Products = repo.Products
      // .Include(item => item.Brand)
      // .Include(item => item.Category)
      .Include(item => item.StockLevel)
      .ToList();
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

  public ProductResponse NewProduct(ProductDTO productDto, Guid userId) {
    Product? product = repo.Products.FirstOrDefault(item => item.ProductName == productDto.ProductName);
    if (product != null) {
      var error = new ProductResponse() {
        Success = false,
        Message = $"Duplicate Error: {productDto.ProductName} already exists. Please enter a unique product name",
      };
      return error;
    }

    if (productDto.Cost > productDto.Price) {
      var error = new ProductResponse() {
        Success = false,
        Message = $"Pricing Error: Product cost cannot be greater than the price.",
      };
      return error;
    }

    Product newProduct = new() {
      ProductCode = (productDto.ProductCode != null && productDto.ProductCode.Length > 0) ? productDto.ProductCode : codeService.GetProductCode(),
      ProductName = productDto.ProductName,
      BrandId = productDto.BrandId,
      CategoryId = productDto.CategoryId,
      Cost = productDto.Cost,
      Price = productDto.Price,
      CreatedAt = DateTime.UtcNow,
      CreatedBy = userId,
      IsActive = true,
      ProductDescription = productDto.ProductName
    };

    repo.Products.Add(newProduct);
    repo.SaveChanges();

    ProductResponse response = new() {
      Data = new List<Product>(){ newProduct },
      Success = true,
      Message = "The product was created successfully",
    };
    return response;
  }


  public ProductResponse GetProduct(Guid id) {
    Product? product = repo.Products.Find(id);
    if (product == null) {
      return new ProductResponse {
        Success = false,
        Message = $"Product not found",
      };
    }

    return new ProductResponse() {
      Data = new List<Product> { product },
      Success = true,
      Message = "The product was retrieved successfully",
    };
  }

  public ProductResponse UpdateProduct(Guid id, ProductDTO productDto, Guid userId) {
    Product? product = repo.Products.FirstOrDefault(item => item.Id == id);
    if (product == null) {
      return new ProductResponse {
        Success = false,
        Message = "Product not found",
      };
    }

    product.ProductCode = (productDto.ProductCode != null && productDto.ProductCode.Length > 0) ? productDto.ProductCode : product.ProductCode;
    product.ProductName = productDto.ProductName;
    product.BrandId = productDto.BrandId;
    product.CategoryId = productDto.CategoryId;
    product.Cost = productDto.Cost;
    product.Price = productDto.Price;
    product.IsActive = true;
    product.ProductDescription = productDto.ProductName;
    product.ModifiedAt = DateTime.UtcNow;
    product.ModifiedBy = userId;

    repo.SaveChanges();
    
    return new ProductResponse {
      Success = true,
      Message = "The product has been updated",
      Data = new List<Product> { product }
    };
  }
}
