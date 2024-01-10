
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using System.Reflection;
using CleanArchitecture.Application;
using CleanArchitecture.Infrastructure;
using WebApplication1.Middleware;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Approach 1 -> Using Service -> Break Controller -> MediatR-> Service ->Infrastructure
//builder.Services.AddScoped<IProductService, ProductService>();

//builder.Services.AddScoped<IProductRepository, ProductRepository>();

//Tryouts For MediatR
//builder.Services.AddMediatR(typeof(ProductRepository).Assembly);

//Try - Handler was nnot found for request error
//var assembly = Assembly.GetExecutingAssembly();
//builder.Services.AddMediatR(assembly);

//Try - Build Error
//var assembly = AppDomain.CurrentDomain.Load("Data");
//builder.Services.AddMediatR(assembly);

//Worked 
//builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());

//Working
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddScoped<AccountContextMiddleware>();
builder.Services.AddScoped<AccountContext>();
var app = builder.Build();
app.UseMiddleware<AccountContextMiddleware>();

//Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

