using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/categories")]
public class CategoryController
{   
    ICategoryInterface categoryService;

    public CategoryController(ICategoryInterface categoryService)
    {
        this.categoryService = categoryService;
    }

    [HttpPost]
    [Authorize(Roles = "ADMIN")]
    public async Task<ServiceResponse<Category>> Create(Category category)
    {
        return await categoryService.Create(category);
    }

    [HttpGet]
    public async Task<ServiceResponse<List<Category>>> GetAll()
    {
        return await categoryService.GetAll();
    }

    [HttpGet("{id}")]
    public async Task<ServiceResponse<Category>> GetById(Guid id)
    {
        return await categoryService.GetById(id);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "ADMIN")]
    public async Task<ServiceResponse<Category>> Update(Guid id, Category categoryUpdated)
    {
        return await categoryService.Update(id, categoryUpdated);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "ADMIN")]
    public async Task<ServiceResponse<Category>> Delete(Guid id)
    {
        return await categoryService.Delete(id);
    }

}