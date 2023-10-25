using Duende.IdentityServer.Models;
namespace IdentityServer;
public static class Config
{
    //https://localhost:5002/.well-known/openid-configuration
    public static IEnumerable<IdentityResource> IdentityResources => new IdentityResource[]
    {
            new IdentityResources.OpenId()
    };

    public static IEnumerable<ApiScope> ApiScopes => new ApiScope[]
    {
            new ApiScope(name: "api1", displayName: "MinhaAPI")
    };

    public static IEnumerable<Client> Clients =>   new List<Client>
    {
        new Client
        {
            ClientId = "client",

            // usuário não iterativo, usa o clientid/secret para autenticacao
            AllowedGrantTypes = GrantTypes.ClientCredentials,

            // segredo para autenticacao
            ClientSecrets =
            {
                new Secret("secret".Sha256())
            },

            // escopos que o cliente pode acessar
            AllowedScopes = { "api1" }
        }
    };
}