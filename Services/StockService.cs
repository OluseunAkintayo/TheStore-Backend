using TheStore.Models;
using TheStore.Models.StockModel;
namespace TheStore.Services.StockService;

public class StockService {
  private readonly RepoService repo;
  public StockService(RepoService _repo) {
    repo = _repo;
  }

  // public StockResponse GetStock() {
  //   return null;
  // }

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
