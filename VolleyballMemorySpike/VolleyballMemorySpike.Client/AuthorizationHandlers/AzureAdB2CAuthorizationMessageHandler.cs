using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using VolleyballMemorySpike.Client.Helpers;

public class AzureAdB2CAuthorizationMessageHandler : AuthorizationMessageHandler
{
    public AzureAdB2CAuthorizationMessageHandler(IAccessTokenProvider provider, 
        NavigationManager navigationManager)
        : base(provider, navigationManager)
    {
        ConfigureHandler(
            authorizedUrls: new[] { ApiClientHelper.Url },
            scopes: new[] { AzureAdB2CHelper.AzureB2CApiScope });
    }
}
