using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Application.Models;
using CleanArchitecture.Domain;

namespace CleanArchitecture.Application
{
    public interface ISubscriptionRepository
    {
        public Task<List<Subscription>> GetSubscriptionForAccount(string accountId);
    }
}
