using Microsoft.IdentityModel.Tokens;
using OBiBiapp.JWT.JWTManager;
using OBiBiapp.JWT.JWTModel;
using OBiBiapp.Model;
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

        public List<ConformReqDto> ListOfConformToken;

        public string SecretKey { get; set; } = "6575fae36288be6d1bad40b99808e37f";

        public string SecurityAlgorithm { get; set; } = SecurityAlgorithms.HmacSha256Signature;

        public int ExpireMinutes { get; set; } = 10080;

        public JWTHandler()
        {
            this.authService = new JWTService(SecretKey);
            this.ListOfConformToken = new List<ConformReqDto>();
        }

        public string GenerateToken(string login)
        {     
            IAuthContainerModel model = this.GetJWTContainerModel(login);

            return authService.GenerateToken(model);
        }

        public void ConformLogin(string login, string email)
        {
            var requestConfirm = this.ListOfConformToken.Find(c => c.Login == login && c.Login == email);
            this.ListOfConformToken.Remove(requestConfirm);
        }

        public List<string> GetClaims(string token)
        {
            var clamims = this.authService.GetTokenClaims(token);
            var ListOfString = new List<string>();
            foreach (var item in clamims)
            {
                ListOfString.Add(item.Value);
            }
            return ListOfString;
        }

        public string GenerateTokenForAccountConform(string login, string email)
        {
            this.ExpireMinutes = 1000;
            IAuthContainerModel model = this.GetJWTContainerModelForMailConform(login, email);
            this.ExpireMinutes = 10080;
            var tokenGen = authService.GenerateToken(model);
            var conformRequest = new ConformReqDto()
            {
                Login = login,
                Email = email,
                Token = tokenGen,
            };
            this.ListOfConformToken.Add(conformRequest);
            return tokenGen;
        }

        public bool IsTokenValid(string token)
        {
            return authService.IsTokenValid(token);
        }

        private JWTContainerModel GetJWTContainerModel(string name )
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

        private JWTContainerModel GetJWTContainerModelForMailConform(string name, string email)
        {
            return new JWTContainerModel()
            {
                SecurityAlgorithm = this.SecurityAlgorithm,

                ExpireMinutes = this.ExpireMinutes,

                Claims = new Claim[]
                {
                    new Claim(ClaimTypes.Name, name),
                    new Claim(ClaimTypes.Email, email),
                }
            };
        }
    }

}
