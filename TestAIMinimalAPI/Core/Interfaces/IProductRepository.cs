using System;
using Ardalis.Result;
using TestAiMinimalAPi.Core.Entities;

namespace TestAiMinimalAPI.Core.Interfaces;

public interface IProductRepository
{
    Task<Result<Product>> GetByIdAsync(int id);
    Task<Result<List<Product>>> GetAllAsync();
    Task<Result> AddAsync(Product product);
}
