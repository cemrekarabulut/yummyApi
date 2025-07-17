using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using YummyApi.entities;

namespace YummyApi.ValidationRules
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(x => x.ProductName).NotEmpty().WithMessage("Ürün adı boş bırakılamaz.");
            RuleFor(x => x.ProductName).MinimumLength(2).WithMessage("En az 2 karakter veri girişi yapın.");
            RuleFor(x => x.ProductName).MaximumLength(50).WithMessage("En fazla 50 karakterveri girişi yapın.");

            RuleFor(x => x.Price).NotEmpty().WithMessage("Ürün fiyatı boş geçilemez.").GreaterThan(0)
            .WithMessage("Ürün fiyatı en az 0 olabilir.").LessThan(5000).WithMessage("Ürün fiyatı en fazla 5000 olabilir.");

            RuleFor(x => x.ProductDescription).NotEmpty().WithMessage("Ürün açıklaması boş geçilemez.");

      }  
    }
}