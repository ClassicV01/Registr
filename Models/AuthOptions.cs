using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Registr.Models
{
    public static class AuthOptions
    {  
        public const string ISSUER = "MyAuthServer";
        public const string AUDIENCE = "MyAuthClient";
        const string KEY = "mysupersecretkeyIgore4ekSecuirity228";
        public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));

    }
}
