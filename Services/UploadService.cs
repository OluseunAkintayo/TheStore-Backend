using TheStore.Models;
namespace TheStore.Services;

public class UploadService {
  private  readonly RepoService repo;
  public UploadService(RepoService _repo){
    repo = _repo;
  }

  public UploadPictureResponse UploadPictures(List<IFormFile> files, string productCode) {
    UploadPictureResponse response = new();
    if(files == null || files.Count == 0) {
      response.Success = false;
      response.Message = "File cannot be empty";
      return response;
    }
    response.PictureUri = new List<string>();
    foreach(IFormFile file in files) {
      try {
        string extension = "." + file.FileName.Split(".")[file.Name.Split(".").Length - 1];
        string[] fileNameArray = file.FileName.Split(" ");
        string fileName = string.Join("-", fileNameArray);
        var dirPath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/Uploads/Products/{productCode}");
        if(!Path.Exists(dirPath)) {
          Directory.CreateDirectory(dirPath);
        }
        var completeFilePath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/Uploads/Products/{productCode}", fileName);
        if(Path.Exists(completeFilePath)) {
          response.Success = false;
          response.Message = "File name must be unique";
        }
        using var stream = new FileStream(completeFilePath, FileMode.Create);
        file.CopyTo(stream);
        response.PictureUri.Add(completeFilePath);
      } catch (Exception e) {
        response.Success = false;
        response.Message = e.Message;
      }
    }

    response.Success = true;
    response.Message = "Files uploaded successfully";
    return response;
  }
}
