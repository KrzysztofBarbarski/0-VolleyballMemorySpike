using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using VolleyballMemorySpike.Client;
using VolleyballMemorySpike.Client.Helpers;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<AzureAdB2CAuthorizationMessageHandler>();

builder.Services.AddHttpClient(ApiClientHelper.Secured, client => client.BaseAddress = new Uri(ApiClientHelper.Url))
    .AddHttpMessageHandler<AzureAdB2CAuthorizationMessageHandler>();

builder.Services.AddHttpClient(ApiClientHelper.NotSecured, client => client.BaseAddress = new Uri(ApiClientHelper.Url));

builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient(ApiClientHelper.Secured));
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient(ApiClientHelper.NotSecured));

builder.Services.AddMsalAuthentication(options =>
{
    builder.Configuration.Bind("AzureAd", options.ProviderOptions.Authentication);
    options.ProviderOptions.DefaultAccessTokenScopes.Add(AzureAdB2CHelper.AzureB2CApiScope);
    options.ProviderOptions.LoginMode = "Redirect";

    options.ProviderOptions.Cache.CacheLocation = "localStorage";
});

await builder.Build().RunAsync();
