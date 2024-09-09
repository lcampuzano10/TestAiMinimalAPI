using System;
using Ardalis.Result;
using TestAiMinimalAPi.Core.Entities;
using TestAiMinimalAPI.Application.Interfaces;
using TestAiMinimalAPI.Core.DTOs;
using TestAiMinimalAPI.Core.Interfaces;

namespace TestAiMinimalAPI.Application.Services;

public class ProductService(IProductRepository repository) : IProductService
{
    public async Task<Result> AddProductAsync(ProductDto productDto)
    {
         var product = new Product { Name = productDto.Name, Price = productDto.Price};
         return await repository.AddAsync(product);
    }

    public async Task<Result<List<ProductDto>>> GetAllProductAsync()
    {
        var result = await repository.GetAllAsync();
        return result.IsSuccess ?
            Result<List<ProductDto>>.Success(result.Value.Select(p => 
            new ProductDto {Name = p.Name, Price = p.Price}).ToList()) :
            Result<List<ProductDto>>.Error(result.Errors as ErrorList);
    }

    public async Task<Result<ProductDto>> GetProductAsync(int id)
    {
        var result = await repository.GetByIdAsync(id);
        return result.IsSuccess ? 
            Result<ProductDto>.Success(
                new ProductDto { Name = result.Value.Name, Price = result.Value.Price}) :
            Result<ProductDto>.NotFound();
    }
}
