using Microsoft.EntityFrameworkCore;
using TheStore.Models;
namespace TheStore.Services.BrandService;

public class BrandService {

  private readonly RepoService repo;
  public BrandService(RepoService repoService){
    repo = repoService;
  }

  public BrandResponse GetAllBrands() {
    var brands = repo.Brands.ToList();
    if(brands == null) {
      var error = new BrandResponse() {
        Message = "Error retrieving brands",
        Success = false
      };
      return error;
    }
    var response = new BrandResponse() {
      Data = brands,
      Success = true,
      Message = "Brands retrieved successfully!"
    };
    return response;
  }

  public BrandResponse GetBrand(Guid id) {
    if(id == Guid.Empty) {
      var error = new BrandResponse() {
        Success = false,
        Message = "Brand ID cannot be empty"
      };
      return error;
    }

    var brand = repo.Brands.Find(id);
    if(brand == null) {
      var error = new BrandResponse() {
        Success = false,
        Message = "Brand not found"
      };
      return error;
    }
    
    var response = new BrandResponse() {
      Data = new List<Brand>(){ brand },
      Success = true,
      Message = "Brand retrieved successfully"
    };
    return response;
  }
 
  public BrandResponse CreateBrand(BrandDto brandDto) {
    var product = repo.Brands.FirstOrDefault(item => item.BrandName == brandDto.BrandName);
    if (product != null) {
      var error = new BrandResponse() {
        Success = false,
        Message = $"Duplicate Error: Brand name {brandDto.BrandName} already exists. Please enter a unique brand name",
      };
      return error;
    }

    Brand newBrand = new() {
      BrandName = brandDto.BrandName,
      ManufacturerId = brandDto.ManufacturerId,
      CreatedAt = DateTime.UtcNow,
      IsActive = brandDto.IsActive
    };
    
    repo.Brands.Add(newBrand);
    repo.SaveChanges();
    var response = new BrandResponse() {
      Success = true,
      Message = "The brand was created successfully"
    };
    return response;
  }

  public BrandResponse UpdateBrand(Guid id, BrandDto brand) {
    var item = repo.Brands.Find(id);
    if(item == null) {
      var error = new BrandResponse() {
        Message = "Brand not found",
        Success = false
      };
      return error;
    }

    item.BrandName = brand.BrandName;
    item.ManufacturerId = brand.ManufacturerId;
    item.ModifiedAt = DateTime.UtcNow;
    item.IsActive = brand.IsActive;

    repo.SaveChanges();
    BrandResponse response = new() {
      Success = true,
      Message = "Brand updated successfully",
      Data = new List<Brand>() { item }
    };
    return response;
  }

  public BrandResponse DeactivateBrand(Guid id) {
    var item = repo.Brands.Find(id);
    if(item == null) {
      var error = new BrandResponse() {
        Message = "Brand not found",
        Success = false
      };
      return error;
    }
    item.IsActive = false;
    item.ModifiedAt = DateTime.UtcNow;

    repo.SaveChanges();
    var response = new BrandResponse() {
      Message = "Brand deactivated",
      Success = true,
      Data = new List<Brand>() { item }
    };
    return response;
  }
}
