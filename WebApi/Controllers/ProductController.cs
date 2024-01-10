using CleanArchitecture.Application;
using CleanArchitecture.Domain;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _service; 
        
    public ProductController(IProductService service)
    {
        _service = service;
    }
    [HttpGet]
    public ActionResult<List<Product>> GetAllProducts()
    {
        Console.WriteLine("** 1 ProductCOntroller");
        return Ok(_service.GetAllProducts());
    } 
}
