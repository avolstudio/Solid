using System;
using System.Net.Http;
using Solid.Core;

namespace Solid.Examples
{
    public abstract class HttpRequest: Awaitable<HttpResponseMessage>
    {
        public static HttpClient Client = new HttpClient();
        
        protected string ServerAdress;
        
        protected override void OnAwake(params object[] parameters)
        {
            if (parameters == null || parameters.Length == 0)
            {
                return;
            }
            
            ServerAdress = (string) parameters[0];
        }

        protected virtual void Update()
        {
            Client.Timeout = TimeSpan.FromSeconds(5);
        }
    }
}    

