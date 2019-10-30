namespace EmpSubbieWebAPI.Models
{
    public class AccessTokenResponse
    {
        public string AccessToken { get; set; }
        public int ExpiresIn { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
    }
}