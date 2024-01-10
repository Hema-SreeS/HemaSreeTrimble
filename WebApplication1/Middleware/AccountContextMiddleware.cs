
using Azure;
using System.Text;
using System.Net.Http.Headers;
using Azure.Core;
using MediatR;
namespace WebApplication1.Middleware;
public class AccountContextMiddleware : IMiddleware
{
    private readonly AccountContext accountContext;
    public AccountContextMiddleware(AccountContext accountContext)
    {
        this.accountContext = accountContext;
    }
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        if (context.Request.QueryString.Value != null)
        {
            Console.WriteLine("MIDDLEWARE Account ID -> " + context.Request.QueryString.Value);
            if (context.Request.QueryString.Value != null)
            {
                string queryStringValue = context.Request.QueryString.Value;
                var parsedString = queryStringValue.Split("?");
                foreach (var query in parsedString)
                {
                    var accountId = query.Split("=");
                    if (!string.IsNullOrEmpty(accountId[0]) && !string.IsNullOrEmpty(accountId[1]))
                    {
                        Console.WriteLine("MIDDLEWARE ACCOUNTID -> " + accountId[0] + " " + accountId[1]);

                        accountContext.AccountId = accountId[1];
                    }
                }
            }
        }
        await next(context);
    }
}
