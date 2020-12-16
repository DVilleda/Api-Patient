using Microsoft.IdentityModel.Tokens;
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
        private static string SecretKey = "XCAP05H6LoKvbRRa/QkqLNMI7cOHguaRyHzyg7n5qEkGjQmtBhz4SzYh4Fqwjyi3KJHlSXKPwVu2+bXr6CtpgQ==";

        public string GenererToken(int id, string typeUtilisateur)
        {
            byte[] key = Convert.FromBase64String(SecretKey);
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(key);
            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.NameIdentifier, id.ToString()),
                    new Claim(ClaimTypes.Actor, typeUtilisateur)
                }),
                SigningCredentials = new SigningCredentials(securityKey,
                  SecurityAlgorithms.HmacSha256Signature)
            };

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            JwtSecurityToken token = handler.CreateJwtSecurityToken(descriptor);
            return handler.WriteToken(token);
        }

        public static ClaimsPrincipal GetPrincipal(byte[] key, string token)
        {
            Debug.WriteLine("clé: " + token);
           
            try
            {
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                JwtSecurityToken jwtToken = (JwtSecurityToken)tokenHandler.ReadToken(token);
                if (jwtToken == null)
                {
                    return null;
                }
                
                Debug.WriteLine("passe ici");
                TokenValidationParameters parameters = new TokenValidationParameters()
                {
                    RequireExpirationTime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
                SecurityToken securityToken;
                ClaimsPrincipal principal = tokenHandler.ValidateToken(token,
                      parameters, out securityToken);
                return principal;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return null;
            }
        }

        public int DécoderTokenPourId(string token)
        {
            int id = -1;
            byte[] key = Convert.FromBase64String(SecretKey);
            ClaimsPrincipal principal = GetPrincipal(key, token);

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

            Claim idClaim = identity.FindFirst(ClaimTypes.NameIdentifier);
            id = Int32.Parse(idClaim.Value);
            return id;
        }

        public string DécoderTypeUtilisateur(string token)
        {
            string type = null;
            byte[] key = Convert.FromBase64String(SecretKey);
            ClaimsPrincipal principal = GetPrincipal(key, token);
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

            Claim idClaim = identity.FindFirst(ClaimTypes.Actor);
            type = idClaim.Value;
            return type;
        }
    }
}