using System.Net.Http;
using System.Threading.Tasks;
using Solid.Behaviours;

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