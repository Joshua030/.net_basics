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
        public string All { get; set; }
        public string ById { get; set; }
        public string RolesByID { get; set; }
        public string Register { get; set; }

        public string UpdateRoles { get; set; }
        public string UpdateStatus { get; set; }

        public string GetById(string userId) => $"{ById}/{userId}";
        public string GetRolesById(string userId) => $"{RolesByID}/{userId}";
        public string UpdateRolesById(string userId) => $"{UpdateRoles}/{userId}";
    }
    public class TenantEndpoints
    {
        public string Create { get; set; }
        public string Upgrade { get; set; }
        public string GetAll { get; set; }
        // Must be public: configuration binding only sets public properties.
        public string ById { get; set; }
        public string Activate { get; set; }
        public string Deactivate { get; set; }

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

