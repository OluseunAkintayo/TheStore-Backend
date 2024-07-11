using TheStore.Models.CategoryModel;

namespace TheStore.Services.CategoryService;

public class CategoryService {

  private readonly RepoService repo;
  public CategoryService(RepoService repoService){
    repo = repoService;
  }

  public CategoryResponse GetAllcategories() {
    try {
      var categories = repo.Categories.ToList();
      if(categories == null) {
        var error = new CategoryResponse() {
          Message = "Error retrieving product categories",
          Success = false
        };
        return error;
      }
      var response = new CategoryResponse() {
        Data = categories,
        Success = true,
        Message = "Product categories retrieved successfully!"
      };
      return response;
    } catch (Exception exception) {
      var error = new CategoryResponse(){
        Message = $"An error occurred: {exception.Message}",
        Success = false
      };
      return error;
    }
  }

  public CategoryResponse GetCategory(Guid id) {
    if(id == Guid.Empty) {
      var error = new CategoryResponse() {
        Success = false,
        Message = "Category ID cannot be empty"
      };
      return error;
    }

    var category = repo.Categories.Find(id);
    if(category == null) {
      var error = new CategoryResponse() {
        Success = false,
        Message = "Category not found"
      };
      return error;
    }
    
    var response = new CategoryResponse() {
      Data = new List<Category>(){ category },
      Success = true,
      Message = "Product category retrieved successfully"
    };
    return response;
  }
 
  public CategoryResponse CreateCategory(CategoryDto categoryDto) {
    var category = repo.Categories.FirstOrDefault(item => item.CategoryName == categoryDto.CategoryName);
    if (category != null) {
      var error = new CategoryResponse() {
        Success = false,
        Message = $"Duplicate Error: Category name {categoryDto.CategoryName} already exists. Please enter a unique category name",
      };
      return error;
    }

    Category newCategory = new() {
      CategoryName = categoryDto.CategoryName,
      Description = categoryDto.Description,
      IsActive = true,
      CreatedAt = DateTime.UtcNow
    };
    
    repo.Categories.Add(newCategory);
    repo.SaveChanges();

    var response = new CategoryResponse() {
      Success = true,
      Message = "Product category created successfully",
      Data = new List<Category>() { newCategory }
    };
    return response;
  }

  public CategoryResponse UpdateCategory(Guid id, CategoryDto category) {
    var item = repo.Categories.Find(id);
    if(item == null) {
      var error = new CategoryResponse() {
        Message = "Category not found",
        Success = false
      };
      return error;
    }

    item.CategoryName = category.CategoryName;
    item.Description = category.Description;
    item.ModifiedAt = DateTime.UtcNow;
    repo.SaveChanges();

    CategoryResponse response = new() {
      Success = true,
      Message = "Category updated successfully",
      Data = new List<Category>() { item }
    };
    return response;
  }

  public CategoryResponse DeactivateCategory(Guid id) {
    var item = repo.Categories.Find(id);
    if(item == null) {
      var error = new CategoryResponse() {
        Message = "Category not found",
        Success = false
      };
      return error;
    }
    item.IsActive = false;
    item.ModifiedAt = DateTime.UtcNow;

    repo.SaveChanges();
    var response = new CategoryResponse() {
      Message = "Category deactivated",
      Success = true,
      Data = new List<Category>() { item }
    };
    return response;
  }
}
