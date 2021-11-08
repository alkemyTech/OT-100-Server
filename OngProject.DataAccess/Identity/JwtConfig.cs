namespace OngProject.DataAccess.Identity
{
    public class JwtConfig
    {
        public string Secret { get; init; }
        public int AccessTokenExpiration { get; init; }
    }
}