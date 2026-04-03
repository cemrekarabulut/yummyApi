using FluentValidation;
using YummyApi.Dtos.ContactDtos;
using YummyApi.Dtos.ContactDtos.FeatureDtos;
using YummyApi.Dtos.ContactDtos.MessageDto;
using YummyApi.Dtos.ContactDtos.ProductDtos;

namespace YummyApi.ValidationRules;

public sealed class CreateProductDtoValidator : AbstractValidator<CreateProductDto>
{
    public CreateProductDtoValidator()
    {
        RuleFor(x => x.ProductName).NotEmpty().MinimumLength(2).MaximumLength(100);
        RuleFor(x => x.ProductDescription).NotEmpty().MaximumLength(500);
        RuleFor(x => x.Price).GreaterThan(0).LessThan(5000);
        RuleFor(x => x.ImageUrl).MaximumLength(500);
        RuleFor(x => x.CategoryId).GreaterThan(0);
    }
}

public sealed class CreateFeatureDtoValidator : AbstractValidator<CreateFeatureDto>
{
    public CreateFeatureDtoValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(120);
        RuleFor(x => x.SubTitle).NotEmpty().MaximumLength(180);
        RuleFor(x => x.Description).NotEmpty().MaximumLength(1000);
        RuleFor(x => x.VideoUrl).MaximumLength(500);
        RuleFor(x => x.Image).MaximumLength(500);
    }
}

public sealed class UpdateFeatureDtoValidator : AbstractValidator<UpdateFeatureDto>
{
    public UpdateFeatureDtoValidator()
    {
        RuleFor(x => x.FeatureId).GreaterThan(0);
        RuleFor(x => x.Title).NotEmpty().MaximumLength(120);
        RuleFor(x => x.SubTitle).NotEmpty().MaximumLength(180);
        RuleFor(x => x.Description).NotEmpty().MaximumLength(1000);
        RuleFor(x => x.VideoUrl).MaximumLength(500);
        RuleFor(x => x.Image).MaximumLength(500);
    }
}

public sealed class UpdateMessageDtoValidator : AbstractValidator<UpdateMessageDto>
{
    public UpdateMessageDtoValidator()
    {
        RuleFor(x => x.MessageId).GreaterThan(0);
        RuleFor(x => x.NameSurname).NotEmpty().MaximumLength(120);
        RuleFor(x => x.Email).NotEmpty().EmailAddress().MaximumLength(200);
        RuleFor(x => x.Subject).NotEmpty().MaximumLength(200);
        RuleFor(x => x.MessageDetails).NotEmpty().MaximumLength(2000);
    }
}

public sealed class CreateContactDtoValidator : AbstractValidator<CreateContactDto>
{
    public CreateContactDtoValidator()
    {
        RuleFor(x => x.MapLocation).NotEmpty().MaximumLength(500);
        RuleFor(x => x.Adress).NotEmpty().MaximumLength(250);
        RuleFor(x => x.Phone).NotEmpty().MaximumLength(20);
        RuleFor(x => x.Email).NotEmpty().EmailAddress().MaximumLength(200);
        RuleFor(x => x.OpenHours).NotEmpty().MaximumLength(120);
    }
}

public sealed class UpdateContactDtoValidator : AbstractValidator<UpdateContactDto>
{
    public UpdateContactDtoValidator()
    {
        RuleFor(x => x.ContactId).GreaterThan(0);
        RuleFor(x => x.MapLocation).NotEmpty().MaximumLength(500);
        RuleFor(x => x.Adress).NotEmpty().MaximumLength(250);
        RuleFor(x => x.Phone).NotEmpty().MaximumLength(20);
        RuleFor(x => x.Email).NotEmpty().EmailAddress().MaximumLength(200);
        RuleFor(x => x.OpenHours).NotEmpty().MaximumLength(120);
    }
}
