using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Application.Models;
using CleanArchitecture.Domain;
using MediatR;

namespace CleanArchitecture.Application.Query;
public record GetSubcsriptionForAccountQuery(string accountId) : IRequest<List<Subscription>>;
