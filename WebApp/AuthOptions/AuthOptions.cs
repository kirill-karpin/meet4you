using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace WebApp.AuthOptions;

public class AuthOptions
{
    public const string Issuer = "MyAuthServer";                                                            // издатель токена
    public const string Audience = "MyAuthClient";                                                          // потребитель токена
    private const string Key = "fb5ea4b45dccd28708013a34078e7de4317df5b9550cdba6a668005ca683a23a";          // ключ шифрования
    private const string RefreshKey = "150b30b352ede3bc5a422c45baa5e9586de62854f713d6532c699e9cdb451a1a";   // ключ шифрования
    public const int LifetimeAccesToken = 10;                                                               // время жизни токена - 10 минут
    public const int LifetimeRefreshToken = 30; 
    public static SymmetricSecurityKey GetSymmetricSecurityKey() => new (Encoding.ASCII.GetBytes(Key));
    public static SymmetricSecurityKey GetSymmetricSecurityRefreshKey() => new (Encoding.ASCII.GetBytes(RefreshKey));
    
}