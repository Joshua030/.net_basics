namespace App.Infrastructure
{
    public class ApiSettings
    {
        public string BaseApiUrl { get; set; }
        public TokenEndpoints TokenEndpoints { get; set; }
        public UserEndpoints UserEndpoints { get; set; }
    }

    public class TokenEndpoints
    {
        public string Login { get; set; }
        public string Refreshtoken { get; set; }
    }
    public class UserEndpoints
    {
        public string Update { get; set; }
    }


}

