using TheStore.Models;

namespace TheStore.Services;

public class ManufacturerService {
  private readonly RepoService repo;
  public ManufacturerService(RepoService repoService){
    repo = repoService;
  }

  public ManufacturerResponse GetManufacturers() {
    try {
      var manufacturers = repo.Manufacturers.Where(item => !item.Deleted).OrderBy(item => item.ManufacturerName).ToList();
      if(manufacturers == null) {
        var error = new ManufacturerResponse() {
          Message = "Error retrieving manufacturers",
          Success = false
        };
        return error;
      }
      var response = new ManufacturerResponse() {
        Data = manufacturers,
        Success = true,
        Message = "Manufacturers retrieved successfully!"
      };
      return response;
    } catch (Exception exception) {
      var error = new ManufacturerResponse(){
        Message = $"An error occurred: {exception.Message}",
        Success = false
      };
      return error;
    }
  }

  public ManufacturerItemResponse GetManufacturer(Guid id) {
    if(id == Guid.Empty) {
      var error = new ManufacturerItemResponse() {
        Success = false,
        Message = "Manufacturer ID cannot be empty"
      };
      return error;
    }

    var manufacturer = repo.Manufacturers.Find(id);
    if(manufacturer == null) {
      var error = new ManufacturerItemResponse() {
        Success = false,
        Message = "Manufacturer not found"
      };
      return error;
    }
    
    var response = new ManufacturerItemResponse() {
      Data = manufacturer,
      Success = true,
      Message = "Manufacturer retrieved successfully"
    };
    return response;
  }
 
  public ManufacturerItemResponse CreateManufacturer(ManufacturerDto manufacturerDto, Guid userId) {
    var ManufacturerItem = repo.Manufacturers.FirstOrDefault(item => item.ManufacturerName == manufacturerDto.ManufacturerName);
    if (ManufacturerItem != null) {
      var error = new ManufacturerItemResponse() {
        Success = false,
        Message = $"Duplicate Error: Manufacturer name {manufacturerDto.ManufacturerName} already exists. Please enter a unique manufacturer name",
      };
      return error;
    }

    Manufacturer manufacturer = new() {
      ManufacturerName = manufacturerDto.ManufacturerName,
      IsActive = true,
      CreatedAt = DateTime.UtcNow,
      Deleted = false,
      CreatedBy = userId
    };
    
    repo.Manufacturers.Add(manufacturer);
    repo.SaveChanges();

    var response = new ManufacturerItemResponse() {
      Success = true,
      Message = "Manufacturer created successfully",
      Data = manufacturer
    };
    return response;
  }

  public ManufacturerItemResponse UpdateManufacturer(Guid id, EditManufacturerDto manufacturerDto, Guid userId) {
    var manufacturer = repo.Manufacturers.Find(id);
    if(manufacturer == null) {
      var error = new ManufacturerItemResponse() {
        Message = "Manufacturer not found",
        Success = false
      };
      return error;
    }

    manufacturer.ManufacturerName = manufacturerDto.ManufacturerName;
    manufacturer.IsActive = manufacturerDto.IsActive;
    manufacturer.ModifiedAt = DateTime.UtcNow;
    manufacturer.ModifiedBy = userId;
    repo.SaveChanges();

    ManufacturerItemResponse response = new() {
      Success = true,
      Message = "Manufacturer updated successfully",
      Data = manufacturer
    };
    return response;
  }

  public ManufacturerItemResponse DeactivateManufacturer(Guid id, Guid userId) {
    var manufacturer = repo.Manufacturers.Find(id);
    if(manufacturer == null) {
      var error = new ManufacturerItemResponse() {
        Message = "Manufacturer not found",
        Success = false
      };
      return error;
    }

    manufacturer.IsActive = false;
    manufacturer.Deleted = true;
    manufacturer.ModifiedAt = DateTime.UtcNow;
    manufacturer.ModifiedBy = userId;

    repo.SaveChanges();
    
    var response = new ManufacturerItemResponse() {
      Message = "Manufacturer deactivated",
      Success = true
    };
    return response;
  }
}
