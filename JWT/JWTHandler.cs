using Microsoft.IdentityModel.Tokens;
using OBiBiapp.JWT.JWTManager;
using OBiBiapp.JWT.JWTModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OBiBiapp.JWT
{
    public class JWTHandler : IJWTHandler
    {
        private IAuthService authService;

        public string SecretKey { get; set; } = "6575fae36288be6d1bad40b99808e37f";

        public string SecurityAlgorithm { get; set; } = SecurityAlgorithms.HmacSha256Signature;

        public int ExpireMinutes { get; set; } = 10080;

        public JWTHandler()
        {
            this.authService = new JWTService(SecretKey);
        }

        public string GenerateToken(string login)
        {     
            IAuthContainerModel model = this.GetJWTContainerModel(login);

            return authService.GenerateToken(model);

        }

        public bool IsTokenValid(string token)
        {
            return authService.IsTokenValid(token);
        }

        private JWTContainerModel GetJWTContainerModel(string name)
        {
            return new JWTContainerModel()
            {
                SecurityAlgorithm = this.SecurityAlgorithm,

                ExpireMinutes = this.ExpireMinutes,

                Claims = new Claim[]
                {
                    new Claim(ClaimTypes.Name, name),
                }
            };
        }
    }

}
