namespace JwtAuthLibrary.Service
{
    public interface IJwtTokenService
    {
        string GenerateToken(string username);
    }
}
