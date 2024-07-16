using TheStore.Models;
using TheStore.Models.StockModel;
namespace TheStore.Services.StockService;

public class StockService {
  private readonly RepoService repo;
  public StockService(RepoService _repo) {
    repo = _repo;
  }

  public ProductStockResponse ListStock() {
    // var stocks = repo.Stocks.ToList();
    var stocks = (
      from s in repo.Stocks
      join p in repo.Products on s.ProductId equals p.Id
      select new StockItems() {
        StockId = s.StockId,
        ProductId = s.ProductId,
        ProductName = p.ProductName,
        Quantity = s.Quantity,
        Cost = p.Cost,
        Price = p.Price,
        CreatedAt = p.CreatedAt
      }
    ).ToList();
    if(stocks == null) {
      var error = new ProductStockResponse() {
        Message = "Error retrieving items",
        Success = false
      };
    }
    var response = new ProductStockResponse() {
      Message = "Items retrieved successfully",
      Success = true,
      Data = stocks
    };
    return response;
  }

  public StockResponse UpdateStock(Guid Id, UpdateStockDto stockDto, Guid userId) {
    var stock = repo.Stocks.Find(Id);
    if(stock == null) {
      StockResponse error = new() {
        Message = "Error retrieving stock",
        Success = false
      };
      return error;
    }

    stock.CostPrice = stockDto.CostPrice;
    stock.Quantity = stockDto.Quantity;
    stock.ReorderLevel = stockDto.ReorderLevel;
    stock.ModifiedAt = DateTime.UtcNow;
    stock.ModifiedBy = userId;

    Product product = repo.Products.Find(stock.ProductId)!;
    product.Cost = stock.CostPrice;

    repo.SaveChanges();
    
    StockResponse response = new() {
      Message = "Stock updated successfully",
      Success = true
    };
    return response;
  }
}
