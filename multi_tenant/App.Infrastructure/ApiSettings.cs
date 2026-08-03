namespace App.Infrastructure
{
    public class ApiSettings
    {
        public string BaseApiUrl { get; set; }
        public TokenEndpoints TokenEndpoints { get; set; }
        public UserEndpoints UserEndpoints { get; set; }
        public TenantEndpoints TenantEndpoints { get; set; }
    }

    public class TokenEndpoints
    {
        public string Login { get; set; }
        public string Refreshtoken { get; set; }
    }
    public class UserEndpoints
    {
        public string Update { get; set; }
        public string ResetPassword { get; set; }
    }
    public class TenantEndpoints
    {
        public string Create { get; set; }
        public string Upgrade { get; set; }
        public string GetAll { get; set; }
        private string ById { get; set; }
        private string Activate { get; set; }
        private string Deactivate { get; set; }

        public string GetById(string tenatId)
        {
            return $"{ById}/{tenatId}";
        }
        public string FullActivate(string tenatId)
        {
            return $"{Activate}/{tenatId}/activate";
        }
        public string FullDeactivate(string tenatId)
        {
            return $"{Deactivate}/{tenatId}/deactivate";
        }

    }

}

