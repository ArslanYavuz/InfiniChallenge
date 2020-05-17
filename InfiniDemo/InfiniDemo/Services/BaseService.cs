using InfiniDemo.Interfaces;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace InfiniDemo.Services
{
    public class BaseService : IBaseService
    {
        private string ApiUrl => "https://demo2.idesse.com/";
        private RestClient RestClient { get; set; }
        private RestRequest RestRequest { get; set; }
        private string MethodName { get; set; }
        private Type ResponseType { get; set; }
        private static IList<RestResponseCookie> LoginCookies { get; set; }


        public BaseService()
        {
            RestClient = new RestSharp.RestClient(ApiUrl);
        }

        public async Task<T> GetAsync<T>(params object[] parameters)
        {
            MethodName = parameters[0] as string;
            RestRequest = new RestRequest(MethodName, DataFormat.Json);

            if (parameters.Length > 1)
                foreach (var param in parameters)
                {
                    var paramTuple = param as Tuple<string, object>;
                    RestRequest.Parameters.Add(new Parameter(paramTuple.Item1, paramTuple.Item2, ParameterType.QueryString));
                }


            foreach (var cookie in LoginCookies)
                RestRequest.AddCookie(cookie.Name, cookie.Value);

            var getResponse = await RestClient.ExecuteAsync(RestRequest, Method.GET);


            var jsonResult = JsonConvert.DeserializeObject<T>(getResponse.Content);

            return jsonResult;
        }

        public async Task<T> PostAsync<T>(params object[] parameters)
        {

            MethodName = parameters[0] as string;
            RestRequest = new RestRequest(MethodName, DataFormat.Json);

            if (parameters.Length > 1)
                RestRequest.AddJsonBody(parameters[1]);

            var postResponse = await RestClient.ExecuteAsync<T>(RestRequest, Method.POST);

            if (MethodName.Contains("Login"))
                LoginCookies = postResponse.Cookies;

            var jsonResult = JsonConvert.DeserializeObject<T>(postResponse.Content);

            return jsonResult;


        }
    }
}
