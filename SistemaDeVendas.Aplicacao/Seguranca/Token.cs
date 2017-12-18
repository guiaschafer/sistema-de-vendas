using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Security.Claims;
using System.ServiceModel.Security.Tokens;
using System.Text;
using SistemaDeVendas.Aplicacao.Properties;

namespace SistemaDeVendas.Aplicacao.Seguranca
{
    public class Token
    {
        private IList<Claim> claims;

        public IEnumerable<Claim> Claims { get { return claims; } }

        public DateTime ExpiraEm { get; set; }

        public DateTime? EmitidoEm { get; set; }

        public string Id
        {
            get
            {
                return claims
                    .FirstOrDefault(c => c.Type.Equals(Seguranca.Claims.Id, StringComparison.OrdinalIgnoreCase))
                    ?.Value;
            }
            set
            {
                var idClaim = claims
                    .FirstOrDefault(c => c.Type.Equals(Seguranca.Claims.Id, StringComparison.OrdinalIgnoreCase));

                if (idClaim != null)
                {
                    claims.Remove(idClaim);
                }

                claims.Add(new Claim(Seguranca.Claims.Id, value));
            }
        }

        public string Login
        {
            get
            {
                return claims
                    .FirstOrDefault(c => c.Type.Equals(Seguranca.Claims.Login, StringComparison.OrdinalIgnoreCase))
                    ?.Value;
            }
            set
            {
                var idClaim = claims
                    .FirstOrDefault(c => c.Type.Equals(Seguranca.Claims.Login, StringComparison.OrdinalIgnoreCase));

                if (idClaim != null)
                {
                    claims.Remove(idClaim);
                }

                claims.Add(new Claim(Seguranca.Claims.Login, value));
            }
        }

        public bool Expirado
        {
            get
            {
                return (ExpiraEm > DateTime.UtcNow);
            }
        }

        public bool Valido { get; private set; }

        public Token()
        {
            claims = new List<Claim>();
            EmitidoEm = DateTime.UtcNow;
        }

        public Token(string tokenSerializado)
            : this()
        {
            if (string.IsNullOrWhiteSpace(tokenSerializado))
            {
                throw new ArgumentNullException(nameof(tokenSerializado));
            }

            Deserializar(tokenSerializado);
        }

        public Token(IEnumerable<Claim> claims)
            : this()
        {
            if (claims == null)
                throw new ArgumentNullException(nameof(claims));

            this.claims = claims.ToList();
        }

        private void Deserializar(string tokenSerializado)
        {
            var parametros = new TokenValidationParameters()
            {
                IssuerSigningToken = new BinarySecretSecurityToken(GerarChaveAssinatura()),
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateActor = false,
            };

            try
            {
                SecurityToken outToken;

                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenValidado = tokenHandler.ValidateToken(tokenSerializado, parametros, out outToken);

                claims = tokenValidado.Claims.ToList();
                EmitidoEm = outToken.ValidFrom;
                ExpiraEm = outToken.ValidTo;

                Valido = true;
            }
            catch
            {
                Valido = false;
            }
        }

        public Token AdicionarClaim(Claim claim)
        {
            if (claim != null)
            {
                claims.Add(claim);
            }

            return this;
        }

        public string GerarJwt()
        {
            var jwtToken = new JwtSecurityToken("vendas",
                claims: Claims,
                notBefore: EmitidoEm,
                expires: ExpiraEm,
                signingCredentials: GerarAssinatura());

            var handler = new JwtSecurityTokenHandler();

            return handler.WriteToken(jwtToken);
        }

        private byte[] GerarChaveAssinatura()
        {
            return Encoding.Unicode.GetBytes(Resources.ChaveAssinaturaToken);
        }

        private SigningCredentials GerarAssinatura()
        {
            var signingKey = new SigningCredentials(
                new InMemorySymmetricSecurityKey(GerarChaveAssinatura()),
                "http://www.w3.org/2001/04/xmldsig-more#hmac-sha256",
                "http://www.w3.org/2001/04/xmlenc#sha256"
            );

            return signingKey;
        }
    }
}
