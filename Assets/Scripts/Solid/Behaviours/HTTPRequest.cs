using System.Net.Http;
using System.Threading.Tasks;

namespace Solid.Behaviours
{
    public abstract class HTTPRequest: Awaitable<HttpResponseMessage>
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
    }
}    

