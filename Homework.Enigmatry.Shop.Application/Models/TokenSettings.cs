namespace Homework.Enigmatry.Shop.Application.Models
{
    public class TokenSettings
    {
        public TimeSpan AccessTokenExpireTime { get; set; }
        public string JwtSecretKey { get; set; }
    }
}
