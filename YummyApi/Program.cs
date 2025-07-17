using YummyApi.Context;
using FluentValidation;
using FluentValidation.AspNetCore;
using System.Reflection;
using YummyApi.entities;
using YummyApi.ValidationRules;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddDbContext<ApiContext>();
builder.Services.AddScoped<IValidator<Product>, ProductValidator>();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());


builder.Services.AddControllers()
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<ProductValidator>());
 // FluentValidation desteği
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

// 🔧 Controller desteğini aktif et!


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Swagger UI
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

// 🔧 Controller route'larını aktif et!
app.MapControllers();

app.Run();
