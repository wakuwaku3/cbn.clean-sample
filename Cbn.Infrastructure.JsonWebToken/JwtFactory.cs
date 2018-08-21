using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Cbn.Infrastructure.Common.Foundation.Interfaces;
using Cbn.Infrastructure.JsonWebToken.Configuration.Interfaces;
using Cbn.Infrastructure.JsonWebToken.Entities;
using Cbn.Infrastructure.JsonWebToken.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace Cbn.Infrastructure.JsonWebToken
{
    public class JwtFactory : IJwtFactory
    {
        private ISystemClock systemClock;
        private IJwtConfig jwtConfig;

        public JwtFactory(
            ISystemClock systemClock,
            IJwtConfig jwtConfig)
        {
            this.systemClock = systemClock;
            this.jwtConfig = jwtConfig;
        }
        public string Create(JwtClaimInfo claimInfo)
        {
            var expires = this.systemClock.Now.AddDays(this.jwtConfig.JwtExpiresDate);

            var token = new JwtSecurityToken(
                issuer: this.jwtConfig.JwtIssuer,
                audience: this.jwtConfig.JwtAudience,
                claims: claimInfo.GetClaims(),
                expires: expires,
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.jwtConfig.JwtSecret)), SecurityAlgorithms.HmacSha256)
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}