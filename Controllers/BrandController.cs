using assignment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2;

[Route("api/[controller]")]
[ApiController]
public class BrandController : ControllerBase
{
    private readonly CarDBContext _dbContext;

    public BrandController(CarDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public IActionResult GetBrands()
    {
        var brands = _dbContext.Brands.Include(b => b.Cars)
            .Select(brand => new BrandResponse
            {
                Id = brand.Id,
                Name = brand.Name,
                Cars = brand.Cars.Select(c => new CarResponse { Model = c.Model, Year = c.Year }).ToList()
            }).ToList();
        return Ok(brands);
    }

    [HttpGet("{id}")]
    public IActionResult GetBrand(int id)
    {
        var brand = _dbContext.Brands.Include(b => b.Cars)
            .Select(Brand => new BrandResponse
            {
                Id = Brand.Id,
                Name = Brand.Name,
                Cars = Brand.Cars.Select(c => new CarResponse { Model = c.Model, Year = c.Year }).ToList()
            })
            .FirstOrDefault(m => m.Id == id);

        if (brand == null)
            return NotFound("Brand Not Found.");

        return Ok(brand);
    }

    [HttpPost]
    public IActionResult CreateBrand(BrandRequest brandRequest)
    {
        var brand = new Brand
        {
            Name = brandRequest.Name,
        };

        _dbContext.Brands.Add(brand);
        _dbContext.SaveChanges();

        var result = new BrandResponse
        {
            Id = brand.Id,
            Name = brand.Name
        };

        return Created(result.Id.ToString(), result);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateBrand(int id, BrandRequest request)
    {
        var brand = _dbContext.Brands.FirstOrDefault(m => m.Id == id);
        if (brand == null)
            return NotFound("Brand Not Found.");

        brand.Name = request.Name;

        _dbContext.Update(brand);
        _dbContext.SaveChanges();
        return Ok(true);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteBrand(int id)
    {
        var brand = _dbContext.Brands.FirstOrDefault(m => m.Id == id);
        if (brand == null)
            return NotFound("Brand Not Found.");

        _dbContext.Remove(brand);
        _dbContext.SaveChanges();
        return Ok(true);
    }

    [HttpPost("AddCarToBrand/{brandId}")]
    public IActionResult AddCarToBrand(int brandId, CarRequest request)
    {
        var brand = _dbContext.Brands.Include(b => b.Cars).FirstOrDefault(b => b.Id == brandId);

        if (brand == null)
        {
            return NotFound("Brand not found.");
        }

        brand.Cars.Add(new Car
        {
            BrandId = brandId,
            Model = request.Model,
            Year = request.Year
        });

        _dbContext.SaveChanges();

        var result = new CarResponse
        {
            Model = request.Model,
            Year = request.Year
        };

        return Created(result.Model, result);
    }
}
