
using Microsoft.EntityFrameworkCore;

public class CategoryService : ICategoryInterface
{
    private MyDbContext context;

    private ServiceResponse<Category> serviceResponse = new ServiceResponse<Category>();

    public CategoryService(MyDbContext context)
    {
        this.context = context;
    }

    public async Task<ServiceResponse<Category>> Create(Category category)
    {
        try
        {
            var repeatedCategory = await context.Categories.AnyAsync(c => c.Name == category.Name);

            if(repeatedCategory)
            {
                serviceResponse.Message = "Categoria já existe";
                return serviceResponse;
            }

            context.Categories.Add(category);
            await context.SaveChangesAsync();

            serviceResponse.Data = category;
            serviceResponse.Message = "Categoria cadastrada";
        } catch (Exception e)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = e.Message;
        }

        return serviceResponse;
    }

    public async Task<ServiceResponse<List<Category>>> GetAll()
    {
        ServiceResponse<List<Category>> serviceResponse = new ServiceResponse<List<Category>>();

        try
        {
            serviceResponse.Data = await context.Categories.ToListAsync();

            if(serviceResponse.Data.Count == 0)
            {
                serviceResponse.Message = "Nenhuma categoria encontrada";
            }

        } catch (Exception e)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = e.Message;
        }

        return serviceResponse;

    }

    public async Task<ServiceResponse<Category>> GetById(Guid id)
    {
        try{
            serviceResponse.Data = await context.Categories.SingleOrDefaultAsync(c => c.Id == id);

            if(serviceResponse.Data == null)
            {
                serviceResponse.Message = "Nenhuma categoria encontrada";
            }
        } catch (Exception e)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = e.Message;
        }

        return serviceResponse;
    }

    public async Task<ServiceResponse<Category>> Update(Guid id, Category categoryUpdated)
    {
       try{
            var category = await context.Categories.SingleOrDefaultAsync(c => c.Id == id);

            if(category == null)
            {
                serviceResponse.Message = "Nenhuma categoria encontrada";
                return serviceResponse;
            }

            category.Name = categoryUpdated.Name;
            await context.SaveChangesAsync();

            serviceResponse.Data = category;

        } catch (Exception e)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = e.Message;
        }

        return serviceResponse;
    }

    public async Task<ServiceResponse<Category>> Delete(Guid id)
    {
        try{
            serviceResponse.Data = await context.Categories.SingleOrDefaultAsync(c => c.Id == id);

            if(serviceResponse.Data == null)
            {
                serviceResponse.Message = "Nenhuma categoria encontrada";
                return serviceResponse;
            }

            context.Categories.Remove(serviceResponse.Data);
            await context.SaveChangesAsync();
            
        } catch (Exception e)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = e.Message;
        }

        return serviceResponse;
    }

}