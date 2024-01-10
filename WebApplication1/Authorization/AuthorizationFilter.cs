using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApi.Controllers;

public class AuthorizationFilter : ActionFilterAttribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        bool HasAccountId = false;
        if (context.HttpContext.Request.QueryString.Value!=null)
        {
            string queryStringValue = context.HttpContext.Request.QueryString.Value;
            var parsedString = queryStringValue.Split("?");
            foreach(var query in parsedString)
            {
                var accountId = query.Split("=");
                if (!string.IsNullOrEmpty(accountId[0]) && !string.IsNullOrEmpty(accountId[1]))
                {
                    Console.WriteLine("&&&&&& GOOD " + accountId[0] + " " + accountId[1]);
                    HasAccountId = true;
                }
            }
        }
        if (!HasAccountId)
        {
            Console.WriteLine("&&&&&& BAD ");
            context.Result = new BadRequestResult();
        }
        
    }
}
