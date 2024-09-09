using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TestAiMinimalAPi.Core.Entities;

namespace TestAiMinimalAPI.Infrastructure.Data;
public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }
}
