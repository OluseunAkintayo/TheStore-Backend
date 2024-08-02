using TheStore.Models;
namespace TheStore.Services.BrandService;

public class BrandService {

  private readonly RepoService repo;
  public BrandService(RepoService repoService){
    repo = repoService;
  }

  public AllBrandsResponse GetAllBrands() {
    var brands = (
      from brand in repo.Brands
      join manufacturer in repo.Manufacturers on brand.ManufacturerId equals manufacturer.Id
      select new AllBrands() {
        BrandId = brand.BrandId,
        BrandName = brand.BrandName,
        ManufacturerId = brand.ManufacturerId,
        ManufacturerName = manufacturer.ManufacturerName,
        CreatedAt = brand.CreatedAt
      }
    ).ToList();

    if(brands == null) {
      var error = new AllBrandsResponse() {
        Message = "Error retrieving brands",
        Success = false
      };
      return error;
    }
    var response = new AllBrandsResponse() {
      Data = brands,
      Success = true,
      Message = "Brands retrieved successfully!"
    };
    return response;
  }

  public BrandItemResponse GetBrand(Guid id) {
    if(id == Guid.Empty) {
      var error = new BrandItemResponse() {
        Success = false,
        Message = "Brand ID cannot be empty"
      };
      return error;
    }

    var brand = repo.Brands.Find(id);
    if(brand == null) {
      var error = new BrandItemResponse() {
        Success = false,
        Message = "Brand not found"
      };
      return error;
    }
    
    var response = new BrandItemResponse() {
      Data = brand ,
      Success = true,
      Message = "Brand retrieved successfully"
    };
    return response;
  }
 
  public BrandItemResponse CreateBrand(BrandDto brandDto) {
    var product = repo.Brands.FirstOrDefault(item => item.BrandName == brandDto.BrandName);
    if (product != null) {
      var error = new BrandItemResponse() {
        Success = false,
        Message = $"Duplicate Error: Brand name {brandDto.BrandName} already exists. Please enter a unique brand name",
      };
      return error;
    }

    Brand newBrand = new() {
      BrandName = brandDto.BrandName,
      ManufacturerId = brandDto.ManufacturerId,
      CreatedAt = DateTime.UtcNow,
      IsActive = brandDto.IsActive,
      Deleted = false
    };
    
    repo.Brands.Add(newBrand);
    repo.SaveChanges();
    var response = new BrandItemResponse() {
      Success = true,
      Message = "The brand was created successfully"
    };
    return response;
  }

  public BrandItemResponse UpdateBrand(Guid id, BrandDto brand) {
    var item = repo.Brands.Find(id);
    if(item == null) {
      var error = new BrandItemResponse() {
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
    BrandItemResponse response = new() {
      Success = true,
      Message = "Brand updated successfully",
      Data = item
    };
    return response;
  }

  public BrandItemResponse DeactivateBrand(Guid id) {
    var item = repo.Brands.Find(id);
    if(item == null) {
      var error = new BrandItemResponse() {
        Message = "Brand not found",
        Success = false
      };
      return error;
    }
    item.IsActive = false;
    item.Deleted = true;
    item.ModifiedAt = DateTime.UtcNow;

    repo.SaveChanges();
    var response = new BrandItemResponse() {
      Message = "Brand deactivated",
      Success = true
    };
    return response;
  }
}
