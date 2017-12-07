//using System;
//using System.IO;
//using RestSharp;
//using RestSharp.Authenticators;
//using Microsoft.AspNet.Identity;
//using System.Threading.Tasks;

//internal class OnlineStoreEmailService
    
    //{

    //public static RestResponse SendSimpleMessage(string contactEmail, string TrackingNumber)
    //    {
    //    RestClient client = new RestClient
    //    {
    //        BaseUrl = new Uri("https://api.mailgun.net/v3"),
    //        Authenticator =
    //        new HttpBasicAuthenticator("api", "key-087e68efbc2ee9791de4888f0250d320")
    //    };
    //    RestRequest request = new RestRequest();
    //        request.AddParameter("domain", "sandbox27edaaaa60a74e9fbaa0f622da4f0d1d.mailgun.org", ParameterType.UrlSegment);
    //        request.Resource = "{domain}/messages";
    //        request.AddParameter("from", "Excited User <excited@mailgun@sandbox27edaaaa60a74e9fbaa0f622da4f0d1d.mailgun.org>");
    //        request.AddParameter("to", contactEmail);
    //        request.AddParameter("to", "excited@sandbox27edaaaa60a74e9fbaa0f622da4f0d1d.mailgun.org");
    //        request.AddParameter("subject", "Hello");
    //        request.AddParameter("text", "Thank you," + contactEmail + "Your business is appreciated. Your tracking number is: " + TrackingNumber);
    //        request.Method = Method.POST;
            
    //        return (RestResponse)client.Execute(request);
    //    }
    ////static void Main(string[] args)
    //{
    //    InvalidProgramException.SendSimpleMessage();
    //    Console.WriteLine("All is done | Check your Inbox");
    //    Console.ReadKey();
    //}

//}