namespace Cbn.Infrastructure.JsonWebToken.Configuration.Interfaces
{
    public interface IJwtConfig
    {
        string JwtSecret { get; }
        int JwtExpiresDate { get; }
    }
}