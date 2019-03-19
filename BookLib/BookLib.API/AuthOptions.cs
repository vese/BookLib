using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookLib.API
{
    public class AuthOptions
    {
        public const string ISSUER = "BookLib"; // издатель токена
        public const string AUDIENCE = "BookLibClient"; // потребитель токена
        const string KEY = "key123key321key456key654";   // ключ для шифрации
        public const int LIFETIME = 1; // время жизни токена - 1 минута
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
