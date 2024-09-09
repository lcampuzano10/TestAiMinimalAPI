using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TestAiMinimalAPI.Application.Interfaces;
using TestAiMinimalAPI.Application.Services;
using TestAiMinimalAPI.Core.DTOs;
using TestAiMinimalAPI.Core.Interfaces;
using TestAiMinimalAPI.Core.Validators;
using TestAiMinimalAPI.Infrastructure.Data;
using TestAiMinimalAPI.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("ProductDb"));
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddValidatorsFromAssemblyContaining<ProductDtoValidator>();

var app = builder.Build();

// Configure Endpoints
app.MapGet("/products/{id:int}", async (int id, IProductService productService) =>
{
    var result = await productService.GetProductAsync(id);
    return result.IsSuccess ? Results.Ok(result.Value) : Results.NotFound(result.Errors);
});

app.MapGet("/products", async (IProductService productService) =>
{
    var result = await productService.GetAllProductAsync();
    return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(result.Errors);
});

app.MapPost("/products", async (ProductDto productDto, IValidator<ProductDto> validator, IProductService productService) =>
{
    var validationResult = validator.Validate(productDto);
    if (!validationResult.IsValid)
        return Results.BadRequest(validationResult.Errors);

    var result = await productService.AddProductAsync(productDto);
    return result.IsSuccess ? Results.Ok() : Results.BadRequest(result.Errors);
});


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();