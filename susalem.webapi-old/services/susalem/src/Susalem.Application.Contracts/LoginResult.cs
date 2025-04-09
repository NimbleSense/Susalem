namespace Susalem;

public class LoginResult
{
    public string AccessToken { get; set; }

    public int ExpiresIn { get; set; }

    public string TokenType { get; set; }

    public string RefreshToken { get; set; }
}