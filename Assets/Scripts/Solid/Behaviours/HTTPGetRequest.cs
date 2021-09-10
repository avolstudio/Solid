using System.Net.Http;
using System.Threading.Tasks;

namespace Solid.Behaviours
{
    public abstract class HTTPRequest: Awaitable<HttpResponseMessage>
    {
        public static HttpClient Client = new HttpClient();
        
        protected  string ServerAdress = "http://localhost:80";
        
        protected override void OnAwake(params object[] parameters)
        {
            if (parameters!= null || parameters.Length == 0)
            {
                return;
            }
            
            ServerAdress = (string) parameters[0];
        }
    }
    
    
    public class HTTPGetRequest: HTTPRequest
    {

        private async void Start()
        {
            var response = await SendMessage();

            Result = response;
            
            SetComplete(response.IsSuccessStatusCode);
        }

        private async Task<HttpResponseMessage> SendMessage()
        {
            var response = await Client.GetAsync(ServerAdress);
            
            return response;
        }
    }
}    

