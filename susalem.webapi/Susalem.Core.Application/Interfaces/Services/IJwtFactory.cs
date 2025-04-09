using System.Security.Claims;

namespace Susalem.Core.Application.Interfaces.Services;

public interface IJwtFactory
{
    string GenerateJwtToken(ClaimsIdentity claimsIdentity);
}
