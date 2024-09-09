using System;
using Ardalis.Result;
using TestAiMinimalAPI.Core.DTOs;

namespace TestAiMinimalAPI.Application.Interfaces;

public interface IProductService
{
    Task<Result<ProductDto>> GetProductAsync(int id);
    Task<Result<List<ProductDto>>> GetAllProductAsync();
    Task<Result> AddProductAsync(ProductDto productDto);
}
