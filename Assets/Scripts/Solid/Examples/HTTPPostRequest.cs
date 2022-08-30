using System.Net.Http;
using System.Threading.Tasks;

namespace Solid.Examples
{
    public class HttpPostRequest:HttpRequest
    {
        protected string postData = "My first message+++";

        protected override void OnAwake(params object[] parameters)
        {
            base.OnAwake(parameters);
            
            postData = (string) parameters[1];
        }

        protected override async void OnStart()
        {
            var response = await SendMessage();

            Result = response;
            
            SetComplete(response.IsSuccessStatusCode);
        }

        private async Task<HttpResponseMessage> SendMessage()
        {
            var response = await Client.PostAsync(ServerAdress,new StringContent(postData));
            
            return response;
        }
    }
}