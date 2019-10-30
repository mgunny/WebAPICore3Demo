
namespace EmpSubbieWebAPI.Models
{
    /// <summary>
    /// App Settings Class
    /// </summary>
    public class AppSettings
    {            
        public JWTConfiguration JWTConfiguration { get; set; }
    }


    public class JWTConfiguration
    {
        public string SecretKey { get; set; }
        public string ValidIssuer { get; set; }
        public string ValidAudience { get; set; }
        public int Expiration { get; set; }
        public string Path { get; set; }

        public string ServerTokenEmail { get; set; }
        public string ServerTokenPassword { get; set; }

    }
}
