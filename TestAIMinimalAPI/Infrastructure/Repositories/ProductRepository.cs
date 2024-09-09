using System;
using Ardalis.Result;
using Microsoft.EntityFrameworkCore;
using TestAiMinimalAPi.Core.Entities;
using TestAiMinimalAPI.Core.Interfaces;
using TestAiMinimalAPI.Infrastructure.Data;

namespace TestAiMinimalAPI.Infrastructure.Repositories;

public class ProductRepository(AppDbContext context) : IProductRepository
{
    public async Task<Result> AddAsync(Product product)
    {
        context.Products.Add(product);
        await context.SaveChangesAsync();
        return Result.Success();
    }

    public async Task<Result<List<Product>>> GetAllAsync()
    {
        var products = await context.Products.ToListAsync();
        return Result<List<Product>>.Success(products);
    }

    public async Task<Result<Product>> GetByIdAsync(int id)
    {
        var product = await context.Products.FindAsync(id);

        return product != null ? Result<Product>.Success(product) : Result<Product>.NotFound();
    }
}
