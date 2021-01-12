using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;

namespace ServicePatient.Models.DAOS
{
    public class JWTAuthentication
    {
        private static string SecretKey = "ERMN05OPLoDvbTTa/QkqLNMI7cPLguaRyHzyg7n5qNBVjQmtBhz4SzYh4NBVCXi3KJHlSXKP+oi2+bXr6CUYTR==";
        public string GenererToken(int id, string typeUtilisateur)
        {
            byte[] key = Convert.FromBase64String(SecretKey);
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(key);
            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, id.ToString()),
                    new Claim(ClaimTypes.Role, typeUtilisateur)
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
            };
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            JwtSecurityToken token = handler.CreateJwtSecurityToken(descriptor);
            return handler.WriteToken(token);
        }

        public static ClaimsPrincipal GetPrincipal(string token)
        {
           
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;
                if (jwtToken == null)
                    return null;
                var symmetricKey = Convert.FromBase64String(SecretKey);
                var validationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime =false,
                    IssuerSigningKey = new SymmetricSecurityKey(symmetricKey)
                };

                SecurityToken securityToken;
                var principal = tokenHandler.ValidateToken(token.ToString(), validationParameters, out securityToken);

                return principal;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                Debug.WriteLine("FAIL PRINCIPAL");
                return null;
            }
        }

        public int DécoderTokenPourId(string token)
        {
            int id = -1;
            byte[] key = Convert.FromBase64String(SecretKey);
            ClaimsPrincipal principal = GetPrincipal(token);

            if (principal == null)
            {
                return -1;
            }
            ClaimsIdentity identity = null;
            try
            {
                identity = (ClaimsIdentity)principal.Identity;
            }
            catch (NullReferenceException)
            {
                return -1;
            }

            Claim idClaim = identity.FindFirst(ClaimTypes.Name);
            id = Int32.Parse(idClaim.Value);
            return id;
        }

        public string DécoderTypeUtilisateur(string token)
        {
            string type = null;
            byte[] key = Convert.FromBase64String(SecretKey);
            ClaimsPrincipal principal = GetPrincipal(token);
            if (principal == null)
                return null;
            ClaimsIdentity identity = null;
            try
            {
                identity = (ClaimsIdentity)principal.Identity;
            }
            catch (NullReferenceException)
            {
                return null;
            }

            Claim idClaim = identity.FindFirst(ClaimTypes.Role);
            type = idClaim.Value;
            return type;
        }
    }
}