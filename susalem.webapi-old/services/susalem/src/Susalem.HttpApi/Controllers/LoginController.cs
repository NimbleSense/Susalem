using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Volo.Abp;

namespace Susalem.Controllers;

[Controller]
[ControllerName("Login")]
public class LoginController : SusalemController
{
    private readonly IConfiguration _configuration;
    private readonly IHttpClientFactory _httpClientFactory;

    public LoginController(IConfiguration configuration, IHttpClientFactory httpClientFactory)
    {
        _configuration = configuration;
        _httpClientFactory = httpClientFactory;
    }

    [HttpPost]
    [Route("api/login")]
    public async Task<LoginResult> LoginAsync([FromBody]LoginDto input)
    {
        var client = _httpClientFactory.CreateClient("Identity");

        var disco = await client.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
        {
            Address = _configuration["AuthServer:Authority"],
            Policy = { RequireHttps = Convert.ToBoolean(_configuration["AuthServer:RequireHttpsMetadata"]) }
        });

        if (disco.IsError) throw new UserFriendlyException(disco.Error);

        var passwordTokenRequest = new PasswordTokenRequest
        {
            Address = disco.TokenEndpoint,
            ClientId = _configuration["AuthServer:ClientId"],
            ClientSecret = _configuration["AuthServer:ClientSecret"],
            UserName = input.Username,
            Password = input.Password,
            Scope = "SusalemApi offline_access"
        };

        var authenticateResponse = await client.RequestPasswordTokenAsync(passwordTokenRequest);

        if (authenticateResponse.IsError)
            throw new UserFriendlyException(authenticateResponse.ErrorDescription,
                authenticateResponse.ErrorType.ToString(),
                authenticateResponse.Error);

        return new()
        {
            AccessToken = authenticateResponse.AccessToken,
            ExpiresIn = authenticateResponse.ExpiresIn,
            RefreshToken = authenticateResponse.RefreshToken,
            TokenType = authenticateResponse.TokenType
        };
    }
}