using ABCSharedLibrary.Models.Requests.Token;

namespace App.Infrastructure.Models
{
    public class LoginRequest : TokenRequest
    {
        public string Tenant { get; set; }
    }
}
