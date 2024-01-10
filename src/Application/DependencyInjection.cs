using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using System.Reflection;
using FluentValidation;
namespace CleanArchitecture.Application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        // Working -> MediatR 12.0.1
        services.AddMediatR(cfg =>  cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()) );
        // Working -> MediatR 9.0.0, Removed Microsoft.DependencyInjectionFixed
        //services.AddMediatR();
        return services;
    }
}
