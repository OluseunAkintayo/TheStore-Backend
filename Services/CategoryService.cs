using TheStore.Models.CategoryModel;

namespace TheStore.Services.CategoryService;

public class CategoryService {

  private readonly RepoService repo;
  public CategoryService(RepoService repoService){
    repo = repoService;
  }

  public CategoryResponse GetAllcategories() {
    try {
      var categories = repo.Categories.Where(item => !item.Deleted).OrderBy(item => item.CategoryName).ToList();
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

  public CategoryItemResponse GetCategory(Guid id) {
    if(id == Guid.Empty) {
      var error = new CategoryItemResponse() {
        Success = false,
        Message = "Category ID cannot be empty"
      };
      return error;
    }

    var category = repo.Categories.Find(id);
    if(category == null) {
      var error = new CategoryItemResponse() {
        Success = false,
        Message = "Category not found"
      };
      return error;
    }
    
    var response = new CategoryItemResponse() {
      Data = category,
      Success = true,
      Message = "Product category retrieved successfully"
    };
    return response;
  }
 
  public CategoryItemResponse CreateCategory(CategoryDto categoryDto) {
    var category = repo.Categories.FirstOrDefault(item => item.CategoryName == categoryDto.CategoryName);
    if (category != null) {
      var error = new CategoryItemResponse() {
        Success = false,
        Message = $"Duplicate Error: Category name {categoryDto.CategoryName} already exists. Please enter a unique category name",
      };
      return error;
    }

    Category newCategory = new() {
      CategoryName = categoryDto.CategoryName,
      Description = categoryDto.Description,
      IsActive = true,
      Deleted = false,
      CreatedAt = DateTime.UtcNow
    };
    
    repo.Categories.Add(newCategory);
    repo.SaveChanges();

    var response = new CategoryItemResponse() {
      Success = true,
      Message = "Product category created successfully",
      Data = newCategory
    };
    return response;
  }

  public CategoryItemResponse UpdateCategory(Guid id, EditCategoryDto category) {
    var item = repo.Categories.Find(id);
    if(item == null) {
      var error = new CategoryItemResponse() {
        Message = "Category not found",
        Success = false
      };
      return error;
    }

    item.CategoryName = category.CategoryName;
    item.Description = category.Description;
    item.IsActive = category.IsActive;
    item.ModifiedAt = DateTime.UtcNow;
    repo.SaveChanges();

    CategoryItemResponse response = new() {
      Success = true,
      Message = "Category updated successfully",
      Data = item
    };
    return response;
  }

  public CategoryItemResponse DeactivateCategory(Guid id) {
    var item = repo.Categories.Find(id);
    if(item == null) {
      var error = new CategoryItemResponse() {
        Message = "Category not found",
        Success = false
      };
      return error;
    }
    
    item.IsActive = false;
    item.Deleted = true;
    item.ModifiedAt = DateTime.UtcNow;

    repo.SaveChanges();

    var response = new CategoryItemResponse() {
      Message = "Category deactivated",
      Success = true
    };
    return response;
  }
}
