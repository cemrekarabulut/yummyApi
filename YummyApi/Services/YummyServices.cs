using AutoMapper;
using Microsoft.EntityFrameworkCore;
using YummyApi.Context;
using YummyApi.Dtos.ContactDtos;
using YummyApi.Dtos.ContactDtos.FeatureDtos;
using YummyApi.Dtos.ContactDtos.MessageDto;
using YummyApi.Dtos.ContactDtos.ProductDtos;
using YummyApi.entities;
using YummyApi;

namespace YummyApi.Services;

public sealed class ProductService(ApiContext context, IMapper mapper) : IProductService
{
    public Task<List<Product>> GetAllAsync(CancellationToken cancellationToken) =>
        context.Products.AsNoTracking().ToListAsync(cancellationToken);

    public Task<Product?> GetByIdAsync(int id, CancellationToken cancellationToken) =>
        context.Products.AsNoTracking().FirstOrDefaultAsync(x => x.ProductId == id, cancellationToken);

    public async Task<List<ResultProductWithCategoryDto>> GetProductListWithCategoryAsync(CancellationToken cancellationToken)
    {
        var products = await context.Products
            .AsNoTracking()
            .Include(x => x.category)
            .ToListAsync(cancellationToken);

        return mapper.Map<List<ResultProductWithCategoryDto>>(products);
    }

    public async Task CreateAsync(Product product, CancellationToken cancellationToken)
    {
        await context.Products.AddAsync(product, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task CreateWithCategoryAsync(CreateProductDto dto, CancellationToken cancellationToken)
    {
        var entity = mapper.Map<Product>(dto);
        await context.Products.AddAsync(entity, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> UpdateAsync(Product product, CancellationToken cancellationToken)
    {
        var exists = await context.Products.AnyAsync(x => x.ProductId == product.ProductId, cancellationToken);
        if (!exists)
        {
            return false;
        }

        context.Products.Update(product);
        await context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var entity = await context.Products.FindAsync([id], cancellationToken);
        if (entity is null)
        {
            return false;
        }

        context.Products.Remove(entity);
        await context.SaveChangesAsync(cancellationToken);
        return true;
    }
}

public sealed class CategoryService(ApiContext context) : ICategoryService
{
    public Task<List<Category>> GetAllAsync(CancellationToken cancellationToken) =>
        context.Categories.AsNoTracking().ToListAsync(cancellationToken);

    public Task<Category?> GetByIdAsync(int id, CancellationToken cancellationToken) =>
        context.Categories.AsNoTracking().FirstOrDefaultAsync(x => x.CategoryId == id, cancellationToken);

    public async Task CreateAsync(Category category, CancellationToken cancellationToken)
    {
        await context.Categories.AddAsync(category, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> UpdateAsync(Category category, CancellationToken cancellationToken)
    {
        var exists = await context.Categories.AnyAsync(x => x.CategoryId == category.CategoryId, cancellationToken);
        if (!exists)
        {
            return false;
        }

        context.Categories.Update(category);
        await context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var entity = await context.Categories.FindAsync([id], cancellationToken);
        if (entity is null)
        {
            return false;
        }

        context.Categories.Remove(entity);
        await context.SaveChangesAsync(cancellationToken);
        return true;
    }
}

public sealed class FeatureService(ApiContext context, IMapper mapper) : IFeatureService
{
    public async Task<List<ResultFeatureDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        var entities = await context.Features.AsNoTracking().ToListAsync(cancellationToken);
        return mapper.Map<List<ResultFeatureDto>>(entities);
    }

    public async Task<GetByIdFeatureDto?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var entity = await context.Features.AsNoTracking().FirstOrDefaultAsync(x => x.FeatureId == id, cancellationToken);
        return entity is null ? null : mapper.Map<GetByIdFeatureDto>(entity);
    }

    public async Task CreateAsync(CreateFeatureDto dto, CancellationToken cancellationToken)
    {
        var entity = mapper.Map<Feature>(dto);
        await context.Features.AddAsync(entity, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> UpdateAsync(UpdateFeatureDto dto, CancellationToken cancellationToken)
    {
        var exists = await context.Features.AnyAsync(x => x.FeatureId == dto.FeatureId, cancellationToken);
        if (!exists)
        {
            return false;
        }

        var entity = mapper.Map<Feature>(dto);
        context.Features.Update(entity);
        await context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var entity = await context.Features.FindAsync([id], cancellationToken);
        if (entity is null)
        {
            return false;
        }

        context.Features.Remove(entity);
        await context.SaveChangesAsync(cancellationToken);
        return true;
    }
}

public sealed class MessageService(ApiContext context, IMapper mapper) : IMessageService
{
    public async Task<List<ResultMessageDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        var entities = await context.Messages.AsNoTracking().ToListAsync(cancellationToken);
        return mapper.Map<List<ResultMessageDto>>(entities);
    }

    public async Task<GetByIdMessageDto?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var entity = await context.Messages.AsNoTracking().FirstOrDefaultAsync(x => x.MessageId == id, cancellationToken);
        return entity is null ? null : mapper.Map<GetByIdMessageDto>(entity);
    }

    public async Task<bool> UpdateAsync(UpdateMessageDto dto, CancellationToken cancellationToken)
    {
        var exists = await context.Messages.AnyAsync(x => x.MessageId == dto.MessageId, cancellationToken);
        if (!exists)
        {
            return false;
        }

        var entity = mapper.Map<Message>(dto);
        context.Messages.Update(entity);
        await context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var entity = await context.Messages.FindAsync([id], cancellationToken);
        if (entity is null)
        {
            return false;
        }

        context.Messages.Remove(entity);
        await context.SaveChangesAsync(cancellationToken);
        return true;
    }
}

public sealed class ContactService(ApiContext context) : IContactService
{
    public Task<List<Contact>> GetAllAsync(CancellationToken cancellationToken) =>
        context.Contacts.AsNoTracking().ToListAsync(cancellationToken);

    public Task<Contact?> GetByIdAsync(int id, CancellationToken cancellationToken) =>
        context.Contacts.AsNoTracking().FirstOrDefaultAsync(x => x.ContactId == id, cancellationToken);

    public async Task CreateAsync(CreateContactDto dto, CancellationToken cancellationToken)
    {
        var entity = new Contact
        {
            Email = dto.Email,
            Adress = dto.Adress,
            Phone = dto.Phone,
            MapLocation = dto.MapLocation,
            OpenHours = dto.OpenHours
        };

        await context.Contacts.AddAsync(entity, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> UpdateAsync(UpdateContactDto dto, CancellationToken cancellationToken)
    {
        var exists = await context.Contacts.AnyAsync(x => x.ContactId == dto.ContactId, cancellationToken);
        if (!exists)
        {
            return false;
        }

        var entity = new Contact
        {
            ContactId = dto.ContactId,
            Email = dto.Email,
            Adress = dto.Adress,
            Phone = dto.Phone,
            MapLocation = dto.MapLocation,
            OpenHours = dto.OpenHours
        };

        context.Contacts.Update(entity);
        await context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var entity = await context.Contacts.FindAsync([id], cancellationToken);
        if (entity is null)
        {
            return false;
        }

        context.Contacts.Remove(entity);
        await context.SaveChangesAsync(cancellationToken);
        return true;
    }
}

public sealed class ChefService(ApiContext context) : IChefService
{
    public Task<List<Chef>> GetAllAsync(CancellationToken cancellationToken) =>
        context.Chefs.AsNoTracking().ToListAsync(cancellationToken);

    public Task<Chef?> GetByIdAsync(int id, CancellationToken cancellationToken) =>
        context.Chefs.AsNoTracking().FirstOrDefaultAsync(x => x.ChefId == id, cancellationToken);

    public async Task CreateAsync(Chef chef, CancellationToken cancellationToken)
    {
        await context.Chefs.AddAsync(chef, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> UpdateAsync(Chef chef, CancellationToken cancellationToken)
    {
        var exists = await context.Chefs.AnyAsync(x => x.ChefId == chef.ChefId, cancellationToken);
        if (!exists)
        {
            return false;
        }

        context.Chefs.Update(chef);
        await context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var entity = await context.Chefs.FindAsync([id], cancellationToken);
        if (entity is null)
        {
            return false;
        }

        context.Chefs.Remove(entity);
        await context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
