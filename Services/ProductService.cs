using Microsoft.EntityFrameworkCore;
using TheStore.Models;
using TheStore.Models.StockModel;
namespace TheStore.Services.ProductService;

public class ProductService {
  private readonly RepoService repo;
  private readonly ProductCodeService codeService;
  public ProductService(RepoService repoService, ProductCodeService _codeService){
    repo = repoService;
    codeService = _codeService;
  }

  
  public ProductResponse GetAdminProducts() {
    var products = repo.Products
      // .Include(item => item.Brand)
      // .Include(item => item.Category)
      .Include(item => item.Stock)
      .ToList();
    
    // var products = (
    //   from prods in repo.Products
    //   join stocks in repo.Stocks on prods.StockId equals stocks.ProductId
    //   select new Product {
    //     Id = prods.Id,
    //     ProductCode = prods.ProductCode,
    //     ProductName = prods.ProductName,
    //   }
    // ).ToList();
    
    if(products == null) {
      return new ProductResponse() {
        Success = false,
        Message = "Error fetching products",
      };
    }

    return new ProductResponse() {
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
      CreatedAt = now,
      IsActive = true,
      CreatedBy = userId,
      Deleted = false
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

  public ProductItemResponse UpdateProduct(Guid id, ProductDTO productDto) {
    Product? product = repo.Products.FirstOrDefault(item => item.Id == id);
    if (product == null) {
      return new ProductItemResponse {
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
    product.IsActive = productDto?.IsActive ?? product.IsActive;
    product.ProductDescription = productDto.Description;
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
