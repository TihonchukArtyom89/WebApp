using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class ContentController : ControllerBase
{
    private DataContext context;
    public ContentController(   DataContext dataContext)
    {
        context = dataContext;
    }
    [HttpGet("string")]
    public string GetString() => "This is string response.";
    [HttpGet("object")]
    [Produces("application/json")]
    public async Task<ProductBindingTarget> GetObject()
    {
        Product p= await context.Products.FirstAsync();
        return new ProductBindingTarget() { CategoryId=p.CategoryId,Name=p.Name,Price=p.Price,SupplierId=p.SupplierId};
    }
}
