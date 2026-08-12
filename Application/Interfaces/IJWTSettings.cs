namespace Application.Interfaces
{
    public interface IJWTSettings
    {
        string SecretKey { get; set; }
        string Issuer { get; set; }
        string Audience { get; set; }
        int ExpirationMinutes { get; set; }
    }
}
