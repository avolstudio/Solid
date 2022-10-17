using System;
using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine;

namespace Solid.Examples
{
    public class HTTPGetRequest: HttpRequest
    {
        protected override async void OnStart()
        {
            var response = await Get();

            Result = response;
            
            SetComplete(response.IsSuccessStatusCode);
        }

        private async Task<HttpResponseMessage> Get()
        {
            HttpResponseMessage response = null;
            
            try
            {
                response = await Client.GetAsync(ServerAdress);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                
                SetComplete(false);
            }
            
            return response;
        }
    }
}