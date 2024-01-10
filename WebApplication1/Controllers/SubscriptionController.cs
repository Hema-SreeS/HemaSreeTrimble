using AutoMapper;
using CleanArchitecture.Application;
using CleanArchitecture.Application.Models;
using CleanArchitecture.Application.Query;
using CleanArchitecture.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[ApiController]
[Route("api/[controller]")]

public class SubscriptionController : ControllerBase
{
    //Approach 1 -> Using Service
    //private readonly IProductService _service;

    //public ProductController(IProductService service)
    //{
    //    _service = service;
    //}
    //[HttpGet]
    //public ActionResult<List<Product>> GetAllProducts()
    //{
    //    Console.WriteLine("** 1 ProductCOntroller");
    //    return Ok(_service.GetAllProducts());
    //}

    //Approach 2 -> using Mediator
    private readonly IMediator mediator;
    private readonly IMapper mapper;
    public SubscriptionController(IMediator mediator, IMapper mapper)
    {
        this.mediator = mediator;
        this.mapper= mapper;

    }

    [HttpGet]
    [AuthorizationFilter]
    public async Task<List<SubscriptionDto>> Get(string accountId)
    {
        Console.WriteLine("** 1 ProductController MediatR");
        var result = await mediator.Send(new GetSubcsriptionForAccountQuery(accountId));
        var subscriptionDto = mapper.Map<List<SubscriptionDto>>(result);
        foreach (var subscription in subscriptionDto) 
        {
            Console.WriteLine("** 2 " + subscription +" "+ subscription.Id +" " +subscription.Name+" "+subscription.StartDate+" "+subscription.EndDate+" "+subscription.SKU);
        }
        return subscriptionDto;
    }
}
