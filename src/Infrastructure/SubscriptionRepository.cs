using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Application;
using CleanArchitecture.Application.Handler;
using CleanArchitecture.Application.Models;
using CleanArchitecture.Domain;
using Newtonsoft.Json;

namespace CleanArchitecture.Infrastructure
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        public async Task<List<Subscription>> GetSubscriptionForAccount(string accountId)
        {
            var handler = new HttpClientHandler();
            handler.UseCookies = false;
            string accessToken = "";
            List<Subscription> subscriptions = new List<Subscription>();

            using (var httpClient = new HttpClient(handler))
            {
                using (var getSalesforceToken = new HttpRequestMessage(new HttpMethod("POST"), "https://trimbledx--r4dev.sandbox.my.salesforce.com/services/oauth2/token"))
                {
                    getSalesforceToken.Headers.TryAddWithoutValidation("Cookie", "BrowserId=AYDcqAsPEeysrEfld3bj0g; CookieConsentPolicy=0:0; LSKey-c$CookieConsentPolicy=0:0; BrowserId=qCv8rmebEe2919FH1fsGOA; CookieConsentPolicy=0:1; LSKey-c$CookieConsentPolicy=0:1; BrowserId=bIIR-HycEe6HtS0lTIzarw; CookieConsentPolicy=0:1; LSKey-c$CookieConsentPolicy=0:1");

                    var contentList = new List<string>();
                    contentList.Add($"grant_type={Uri.EscapeDataString("password")}");
                    contentList.Add($"client_id={Uri.EscapeDataString("3MVG9JEx.BE6yifMM.5EK8msTUFz4kbb26frVYH_sCN6aQP.9poonB3ztl8urWqTMXpMD7dgWxKSUKVs8lo.r")}");
                    contentList.Add($"client_secret={Uri.EscapeDataString("3B4EF22A9079938562C9A851FAF77ECD653654AD2927518D2415C9EB7E48CAAB")}");
                    contentList.Add($"username={Uri.EscapeDataString("mulesoft_integration@trimble.com.dx.r4dev")}");
                    contentList.Add($"password={Uri.EscapeDataString("Trimbl3@2023R4DevIeQxNfQJWf5q3IHNLmV4QTVjs")}");
                    getSalesforceToken.Content = new StringContent(string.Join("&", contentList));
                    getSalesforceToken.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/x-www-form-urlencoded");

                    var getSubscription = await httpClient.SendAsync(getSalesforceToken);
                    Console.WriteLine("** response " + getSubscription + " Responses " + getSubscription.Content.ReadAsStringAsync().Result);
                    var getSubscriptionResponse = JsonConvert.DeserializeObject<SalesforceResponse>(getSubscription.Content.ReadAsStringAsync().Result);

                    if (getSubscriptionResponse?.access_token != null)
                    {
                        accessToken = getSubscriptionResponse.access_token;
                        Console.WriteLine("** tokenType" + accessToken);
                    }
                    using (var request2 = new HttpRequestMessage(new HttpMethod("POST"), "https://trimbledx--r4dev.sandbox.my.salesforce.com/services/data/v54.0/composite"))
                    {
                        //request2.Headers.TryAddWithoutValidation("Authorization", "Bearer 00D8G000000Hnm4!ARQAQBLINCPxt9LdtXaZjPo9B9TTG9816IQWsqKUg_9bDC59OcrHi3AhH4UlZTBnfEEtSPej6O8VTy79cukDnvqp0tF8MY_5");
                        if (accessToken != null)
                        {
                            request2.Headers.TryAddWithoutValidation("Authorization", "Bearer " + accessToken);
                            request2.Headers.TryAddWithoutValidation("Cookie", "BrowserId=bIIR-HycEe6HtS0lTIzarw; CookieConsentPolicy=0:1; LSKey-c$CookieConsentPolicy=0:1");
                            //request2.Content = new StringContent("{\n  \"compositeRequest\": [{\n    \"method\": \"GET\",\n    \"url\": \"/services/data/v54.0/query?q=SELECT Id, Name, Account.TNV_Enterprise_Customer_Id__c, SBQQ__RequiredByAsset__c, SBQQ__RequiredById__c,SBQQ__SubscriptionStartDate__c,SBQQ__SubscriptionEndDate__c,Product2.ProductCode,Product2.Name, Product2.TNV_Provisioning_Method__c,Product2.MYT_Product_Launch_Url__c,Quantity,Product2.TNV_Do_Not_Auto_Renew__c,SBQQ__OrderProduct__r.MYT_EMS_Entitlement_Id__c from Asset where Account.TNV_Enterprise_Customer_Id__c='1698658143722544' and SBQQ__RequiredById__c=null\",\n    \"referenceId\": \"query1\"\n  }]\n}");
                            request2.Content = new StringContent("{\n  \"compositeRequest\": [{\n    \"method\": \"GET\",\n    \"url\": \"/services/data/v54.0/query?q=SELECT Id, Name, Account.TNV_Enterprise_Customer_Id__c, SBQQ__RequiredByAsset__c, SBQQ__RequiredById__c,SBQQ__SubscriptionStartDate__c,SBQQ__SubscriptionEndDate__c,Product2.ProductCode,Product2.Name, Product2.TNV_Provisioning_Method__c,Product2.MYT_Product_Launch_Url__c,Quantity,Product2.TNV_Do_Not_Auto_Renew__c,SBQQ__OrderProduct__r.MYT_EMS_Entitlement_Id__c from Asset where Account.TNV_Enterprise_Customer_Id__c='"+accountId+"' and SBQQ__RequiredById__c=null\",\n    \"referenceId\": \"query1\"\n  }]\n}");
                           
                            request2.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

                            var response2 = await httpClient.SendAsync(request2);
                            Console.WriteLine("** response " + response2 + " Responses " + response2.Content.ReadAsStringAsync().Result);

                            var accountSubscriptionResponse = JsonConvert.DeserializeObject<AccountSubscriptionResponse>(response2.Content.ReadAsStringAsync().Result);

                            if (accountSubscriptionResponse != null && accountSubscriptionResponse?.compositeResponse != null && accountSubscriptionResponse?.compositeResponse.Length > 0)
                            {
                                foreach (var response in accountSubscriptionResponse.compositeResponse)
                                {
                                    if (response.body != null && response.body.records != null)
                                    {
                                        foreach (var record in response.body.records)
                                        {
                                            if (record != null)
                                            {
                                                Subscription subscription = new Subscription();
                                                if (record.Id != null)
                                                {
                                                    subscription.Id = record.Id;
                                                }
                                                if (record.Name != null)
                                                {
                                                    subscription.Name = record.Name;
                                                }
                                                if (record.SBQQ__SubscriptionStartDate__c != null)
                                                {
                                                    subscription.SBQQ__SubscriptionStartDate__c = record.SBQQ__SubscriptionStartDate__c;
                                                }
                                                if (record.SBQQ__SubscriptionEndDate__c != null)
                                                {
                                                    subscription.SBQQ__SubscriptionEndDate__c = record.SBQQ__SubscriptionEndDate__c;
                                                }
                                                if (record.Product2 != null && record.Product2.ProductCode != null)
                                                {
                                                    subscription.ProductCode = record.Product2.ProductCode;
                                                }
                                                subscriptions.Add(subscription);
                                                Console.WriteLine($"Id: {record.Id}");
                                                Console.WriteLine($"Name: {record.Name}");
                                                Console.WriteLine($"Subscription Start Date: {record.SBQQ__SubscriptionStartDate__c}");
                                                Console.WriteLine($"Subscription End Date: {record.SBQQ__SubscriptionEndDate__c}");
                                                Console.WriteLine($"Product Code: {record.Product2?.ProductCode}");

                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                }
            }
            Console.WriteLine("** Subscription Count " + subscriptions.Count);
            return subscriptions;
        }

    }
}
