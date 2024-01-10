using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchitecture.Domain;

namespace CleanArchitecture.Application.Models;
public class SubscriptionDto
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string StartDate { get; set; } = string.Empty;
    public string EndDate { get; set; } = string.Empty;
    public string SKU { get; set; } = string.Empty;

    private class Mapping:Profile
    {
        public Mapping()
        {
            //Source -> Destination
            CreateMap<Subscription, SubscriptionDto>()
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.SBQQ__SubscriptionStartDate__c))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.SBQQ__SubscriptionEndDate__c))
                .ForMember(dest => dest.SKU, opt => opt.MapFrom(src => src.ProductCode));
        }
    }
}
