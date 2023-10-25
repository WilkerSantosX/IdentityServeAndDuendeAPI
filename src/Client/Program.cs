using IdentityModel.Client;
using System.Text.Json;

// descobre os endpoints a partir do metadata
var client = new HttpClient();

var disco = await client.GetDiscoveryDocumentAsync("https://localhost:5002");

if (disco.IsError)
{
    Console.WriteLine(disco.Error);
    return;
}
// request token
var tokenResponse = await client.RequestClientCredentialsTokenAsync(new
 ClientCredentialsTokenRequest
{
    Address = disco.TokenEndpoint,

    ClientId = "client",
    ClientSecret = "secret",
    Scope = "api1"
});

if (tokenResponse.IsError)
{
    Console.WriteLine(tokenResponse.Error);
    return;
}

Console.WriteLine(tokenResponse.AccessToken);

Console.WriteLine("\n\n");

// call api
var apiClient = new HttpClient();
apiClient.SetBearerToken(tokenResponse.AccessToken);

var response = await apiClient.GetAsync("https://localhost:6001/identity");
if (!response.IsSuccessStatusCode)
{
    Console.WriteLine(response.StatusCode);
}
else
{
    var doc = JsonDocument.Parse(await response.Content.ReadAsStringAsync()).RootElement;
    Console.WriteLine(JsonSerializer.Serialize(doc, new JsonSerializerOptions { WriteIndented = true }));
}

Console.WriteLine("\n\n ########### Processamento Concluído   ##############\n");
Console.ReadKey();