using YummyApi.Dtos.ContactDtos;
using YummyApi.Dtos.ContactDtos.FeatureDtos;
using YummyApi.Dtos.ContactDtos.MessageDto;
using YummyApi.Dtos.ContactDtos.ProductDtos;
using YummyApi.entities;
using YummyApi;

namespace YummyApi.Services;

public interface IProductService
{
    Task<List<Product>> GetAllAsync(CancellationToken cancellationToken);
    Task<Product?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<List<ResultProductWithCategoryDto>> GetProductListWithCategoryAsync(CancellationToken cancellationToken);
    Task CreateAsync(Product product, CancellationToken cancellationToken);
    Task CreateWithCategoryAsync(CreateProductDto dto, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(Product product, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
}

public interface ICategoryService
{
    Task<List<Category>> GetAllAsync(CancellationToken cancellationToken);
    Task<Category?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task CreateAsync(Category category, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(Category category, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
}

public interface IFeatureService
{
    Task<List<ResultFeatureDto>> GetAllAsync(CancellationToken cancellationToken);
    Task<GetByIdFeatureDto?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task CreateAsync(CreateFeatureDto dto, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(UpdateFeatureDto dto, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
}

public interface IMessageService
{
    Task<List<ResultMessageDto>> GetAllAsync(CancellationToken cancellationToken);
    Task<GetByIdMessageDto?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(UpdateMessageDto dto, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
}

public interface IContactService
{
    Task<List<Contact>> GetAllAsync(CancellationToken cancellationToken);
    Task<Contact?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task CreateAsync(CreateContactDto dto, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(UpdateContactDto dto, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
}

public interface IChefService
{
    Task<List<Chef>> GetAllAsync(CancellationToken cancellationToken);
    Task<Chef?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task CreateAsync(Chef chef, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(Chef chef, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
}
