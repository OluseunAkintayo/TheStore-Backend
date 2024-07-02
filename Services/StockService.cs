using Microsoft.EntityFrameworkCore;
using TheStore.Models.StockModel;
namespace TheStore.Services.StockService;

public class StockService {
  private readonly RepoService repo;
  public StockService(RepoService _repo) {
    repo = _repo;
  }

  public StockResponse GetStock() {
    try {
      List<Stock> stocks = repo.Stocks.ToList();
      if(stocks != null) {
        var productStock = repo.Products.Include(item => item.Brand).Include(item => item.Category).Include(item => item.StockLevel).ToList();
        return new StockResponse {
          ProductStock = productStock,
          Success = true,
          Message = "Stocks retrieved"
        };
      }
      return new StockResponse {
        Message = $"Error retrieving stocks",
        Success = false
      };
    } catch(Exception exception) {
      return new StockResponse {
        Message = $"Error retrieving stocks: {exception.Message}",
        Success = false
      };
    }
  }
}
