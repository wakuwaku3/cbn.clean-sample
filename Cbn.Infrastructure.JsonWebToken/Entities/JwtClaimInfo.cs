using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Cbn.Infrastructure.JsonWebToken.Entities
{
    public class JwtClaimInfo
    {
        public JwtClaimInfo()
        {

        }
        public JwtClaimInfo(ClaimsPrincipal claimsPrincipal)
        {
            foreach (var claim in claimsPrincipal.Claims)
            {
                switch (claim.Type)
                {
                    case ClaimTypes.Sid:
                        this.UserId = claim.Value;
                        break;
                    case ClaimTypes.Name:
                        this.Name = claim.Value;
                        break;
                    case ClaimTypes.Email:
                        this.Email = claim.Value;
                        break;
                    case ClaimTypes.Locality:
                        this.CultureInfo = CultureInfo.GetCultureInfo(claim.Value);
                        break;
                }
            }
        }

        public string UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public CultureInfo CultureInfo { get; set; }

        public IEnumerable<Claim> GetClaims()
        {
            // JwtBearerAuthentication 用
            yield return new Claim(JwtRegisteredClaimNames.Jti, this.UserId);
            yield return new Claim(JwtRegisteredClaimNames.Sub, this.Name);
            yield return new Claim(JwtRegisteredClaimNames.Email, this.Email);
            // User.Identity プロパティ用
            yield return new Claim(ClaimTypes.Sid, this.UserId);
            yield return new Claim(ClaimTypes.Name, this.Name);
            yield return new Claim(ClaimTypes.Locality, this.CultureInfo.ToString());
            yield return new Claim(ClaimTypes.Email, this.Email);
        }
    }
}