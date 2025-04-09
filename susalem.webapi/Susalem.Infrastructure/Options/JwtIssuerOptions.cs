using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Susalem.Infrastructure.Options;

public class JwtIssuerOptions
{
    public string Key { get; set; }

    /// <summary>
    /// "iss" (Issuer) Claim - The "iss" (issuer) claim identifies the principal that issued the JWT.
    /// </summary>
    public string Issuer { get; set; }

    /// <summary>
    /// "sub" (Subject) Claim - The "sub" (subject) claim identifies the principal that is the subject of the JWT.
    /// </summary>
    public string Subject { get; set; }

    /// <summary>
    /// "aud" (Audience) Claim - The "aud" (audience) claim identifies the recipients that the JWT is intended for.
    /// </summary>
    public string Audience { get; set; }

    /// <summary>
    /// "exp" (Expiration Time) Claim - The "exp" (expiration time) claim identifies the expiration time on or after which the JWT MUST NOT be accepted for processing.
    /// </summary>
    public DateTime Expiration => IssuedAt.Add(ValidFor);

    /// <summary>
    ///  "nbf" (Not Before) Claim - The "nbf" (not before) claim identifies the time before which the JWT MUST NOT be accepted for processing.
    /// </summary>
    public DateTime NotBefore => DateTime.UtcNow;

    /// <summary>
    /// "iat" (Issued At) Claim - The "iat" (issued at) claim identifies the time at which the JWT was issued.
    /// </summary>
    public DateTime IssuedAt => DateTime.UtcNow;

    /// <summary>
    /// Set the timespan the token will be valid for (default is 120min).
    /// </summary>
    public TimeSpan ValidFor { get; set; } = TimeSpan.FromDays(365);

    /// <summary>
    /// "jti" (JWT ID) Claim (default ID is a GUID)
    /// </summary>
    public Func<Task<string>> JtiGenerator =>
        () => Task.FromResult(Guid.NewGuid().ToString());

    /// <summary>
    /// The signing key to use when generating tokens.
    /// </summary>
    public SigningCredentials SigningCredentials
    {
        get
        {
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
            return new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
        }
    }
}
