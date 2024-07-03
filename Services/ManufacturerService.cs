using TheStore.Models;

namespace TheStore.Services;

public class ManufacturerService {

  private readonly RepoService repo;
  public ManufacturerService(RepoService repoService){
    repo = repoService;
  }

  public ManufacturerResponse GetManufacturers() {
    try {
      var manufacturers = repo.Manufacturers.ToList();
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

  public ManufacturerResponse GetManufacturer(Guid id) {
    if(id == Guid.Empty) {
      var error = new ManufacturerResponse() {
        Success = false,
        Message = "Manufacturer ID cannot be empty"
      };
      return error;
    }

    var manufacturer = repo.Manufacturers.Find(id);
    if(manufacturer == null) {
      var error = new ManufacturerResponse() {
        Success = false,
        Message = "Manufacturer not found"
      };
      return error;
    }
    
    var response = new ManufacturerResponse() {
      Data = new List<Manufacturer>(){ manufacturer },
      Success = true,
      Message = "Manufacturer retrieved successfully"
    };
    return response;
  }
 
  public ManufacturerResponse CreateManufacturer(ManufacturerDto manufacturerDto) {
    var ManufacturerItem = repo.Manufacturers.FirstOrDefault(item => item.ManufacturerName == manufacturerDto.ManufacturerName);
    if (ManufacturerItem != null) {
      var error = new ManufacturerResponse() {
        Success = false,
        Message = $"Duplicate Error: Manufacturer name {manufacturerDto.ManufacturerName} already exists. Please enter a unique manufacturer name",
      };
      return error;
    }

    Manufacturer manufacturer = new() {
      ManufacturerName = manufacturerDto.ManufacturerName,
      CreatedAt = DateTime.UtcNow,
      IsActive = true,
      ModifiedAt = null
    };
    
    repo.Manufacturers.Add(manufacturer);
    repo.SaveChanges();

    var response = new ManufacturerResponse() {
      Success = true,
      Message = "Manufacturer created successfully",
      Data = new List<Manufacturer>() { manufacturer }
    };
    return response;
  }

  public ManufacturerResponse UpdateManufacturer(Guid id, ManufacturerDto manufacturerDto) {
    var manufacturer = repo.Manufacturers.Find(id);
    if(manufacturer == null) {
      var error = new ManufacturerResponse() {
        Message = "Manufacturer not found",
        Success = false
      };
      return error;
    }

    manufacturer.ManufacturerName = manufacturerDto.ManufacturerName;
    manufacturer.ModifiedAt = DateTime.UtcNow;
    repo.SaveChanges();

    ManufacturerResponse response = new() {
      Success = true,
      Message = "Manufacturer updated successfully",
      Data = new List<Manufacturer>() { manufacturer }
    };
    return response;
  }

  public ManufacturerResponse DeactivateManufacturer(Guid id) {
    var manufacturer = repo.Manufacturers.Find(id);
    if(manufacturer == null) {
      var error = new ManufacturerResponse() {
        Message = "Manufacturer not found",
        Success = false
      };
      return error;
    }

    manufacturer.IsActive = false;
    manufacturer.ModifiedAt = DateTime.UtcNow;

    repo.SaveChanges();
    var response = new ManufacturerResponse() {
      Message = "Manufacturer deactivated",
      Success = true,
      Data = new List<Manufacturer>() { manufacturer }
    };
    return response;
  }
}
