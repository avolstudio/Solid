using System.Net.Http;
using System.Threading.Tasks;

namespace Solid.Behaviours
{
    public class HTTPPostRequest:HTTPRequest
    {
        private string postData = "My first message+++";

        protected override void OnAwake(params object[] parameters)
        {
            base.OnAwake(parameters);
            
            postData = (string) parameters[1];
        }

        private async void Start()
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