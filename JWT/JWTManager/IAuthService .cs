﻿using OBiBiapp.JWT.JWTModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OBiBiapp.JWT.JWTManager
{
    public interface IAuthService
    {
        string SecretKey { get; set; }

        bool IsTokenValid(string token);

        string GenerateToken(IAuthContainerModel model);

        IEnumerable<Claim> GetTokenClaims(string Token);
    }
}
