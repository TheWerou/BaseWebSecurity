using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OBiBiapp.JWT.JWTModel
{
    public class JWTContainerModel : IAuthContainerModel
    {
        public int ExpireMinutes { get; set; }

        public string SecurityAlgorithm { get; set; } 

        public Claim[] Claims { get; set; }
       
    }
}
