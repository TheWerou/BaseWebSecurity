using OBiBiapp.JWT.JWTModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OBiBiapp.JWT
{
    public interface IJWTHandler
    {
        public string SecretKey { get; set; } 

        public string SecurityAlgorithm { get; set; }

        public string GenerateTokenForAccountConform(string login, string email);

        public List<string> GetClaims(string token);

        public int ExpireMinutes { get; set; } 

        public string GenerateToken(string login);

        public bool IsTokenValid(string token);

    }
}
