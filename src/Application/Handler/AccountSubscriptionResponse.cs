using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Handler;
public class AccountSubscriptionResponse
{
    public BodyResponse[]? compositeResponse { get; set; }
}

public class BodyResponse
{
    public RecordResponse? body { get; set; }
}

public class RecordResponse
{
    public Record[]? records { get; set; }
}

public class Record
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? SBQQ__SubscriptionStartDate__c { get; set; }
    public string? SBQQ__SubscriptionEndDate__c { get; set; }
    public ProductCodeResponse? Product2 { get; set; }
}

public class ProductCodeResponse
{
    public string? ProductCode { get; set; }
}
