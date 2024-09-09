using System;

namespace TestAiMinimalAPI.Core.DTOs;

public class ProductDto
{
    public required string Name { get; set; }
    public decimal Price { get; set; }
}
