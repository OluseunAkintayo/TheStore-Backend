using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using BCrypt.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TheStore.Models;
using TheStore.Models.Users;
namespace TheStore.Services;

public class UploadService {
  private  readonly RepoService repo;
  public UploadService(RepoService _repo){
    repo = _repo;
  }
  public UploadResponse UploadFile(IFormFile file, Guid productId) {
    return null;
    // if(file == null || file.Length == 0) {
    //   UploadResponse error = new() {
    //     Success = false,
    //     Message = "File cannot be empty"
    //   };
    //   return error;
    // }
    // var memoryStream = new MemoryStream();
    // file.CopyTo(memoryStream);
    // Picture picture = new() {
    //   Filename = file.FileName,
    //   ContentType = file.ContentType,
    //   Data = memoryStream.ToArray(),
    //   ProductId = productId
    // };

    // repo.Pictures.Add(picture);
    // repo.SaveChanges();
    // UploadResponse response = new() {
    //   Success = true,
    //   Message = "File uploaded successfully",
    //   Data = picture
    // };
    // return response;
  }

  public UploadPictureResponse UploadPictures(List<IFormFile> files, Guid productId) {
    UploadPictureResponse response = new();
    if(files == null || files.Count == 0) {
      response.Success = false;
      response.Message = "File cannot be empty";
      return response;
    }

    foreach(IFormFile file in files) {
      try {
        string extension = "." + file.FileName.Split(".")[file.Name.Split(".").Length - 1];
        string[] fileNameArray = file.FileName.Split(" ");
        string fileName = string.Join("-", fileNameArray);
        var dirPath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/Uploads/Products/{productId}");
        if(!Path.Exists(dirPath)) {
          Directory.CreateDirectory(dirPath);
        }
        var absoluteFilePath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/Uploads/Products/{productId}", fileName);
        if(Path.Exists(absoluteFilePath)) {
          response.Success = false;
          response.Message = "File name must be unique";
          return response;
        }
        using var stream = new FileStream(absoluteFilePath, FileMode.Create);
        file.CopyTo(stream);
        response.PictureUri.Add(absoluteFilePath);
        return response;
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
