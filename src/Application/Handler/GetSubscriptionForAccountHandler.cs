using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Application.Models;
using CleanArchitecture.Application.Query;
using CleanArchitecture.Domain;
using MediatR;
using Newtonsoft.Json;

namespace CleanArchitecture.Application.Handler;
public class GetSubscriptionForAccountHandler : IRequestHandler<GetSubcsriptionForAccountQuery, List<Subscription>>
{
    readonly ISubscriptionRepository subscriptionRepository;
    public GetSubscriptionForAccountHandler(ISubscriptionRepository subscriptionRepository)
    {
        this.subscriptionRepository = subscriptionRepository;
    }
    public async Task<List<Subscription>> Handle(GetSubcsriptionForAccountQuery query, CancellationToken cancellationToken)
    {
        return await subscriptionRepository.GetSubscriptionForAccount(query.accountId);
    }
}
