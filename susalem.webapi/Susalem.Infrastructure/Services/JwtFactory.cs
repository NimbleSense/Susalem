using Susalem.Core.Application.Interfaces.Services;
using Susalem.Infrastructure.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Susalem.Infrastructure.Services;

public class JwtFactory : IJwtFactory
{
    private readonly JwtIssuerOptions _jwtOptions;

    public JwtFactory(IConfiguration configuration)
    {
        _jwtOptions = configuration.GetRequiredSection("JWT").Get<JwtIssuerOptions>();
    }
                                          
    public string GenerateJwtToken(ClaimsIdentity claimsIdentity)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _jwtOptions.Issuer,
                Audience = _jwtOptions.Audience,
                Subject = claimsIdentity,
                NotBefore = _jwtOptions.NotBefore,
                Expires = _jwtOptions.Expiration,
                SigningCredentials = _jwtOptions.SigningCredentials
            });

            return tokenHandler.WriteToken(token);
        }
        catch(Exception ex)
        {
            return string.Empty;
        }
       
    }
}
