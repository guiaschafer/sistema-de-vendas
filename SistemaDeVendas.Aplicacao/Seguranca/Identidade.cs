
using System;
using System.Linq;
using System.Security.Principal;

namespace SistemaDeVendas.Aplicacao.Seguranca
{
    public class Identidade : IIdentity
    {
        public string AuthenticationType { get; private set; }

        public bool IsAuthenticated { get; private set; }

        public string IdUsuario { get; private set; }

        public string Name { get; private set; }

        public Token Token { get; private set; }

        public Identidade()
        {
            AuthenticationType = "Token";
        }

        public Identidade(Token token)
            : this()
        {
            if (token == null)
                throw new ArgumentNullException(nameof(token));

            Token = token;
            IsAuthenticated = token.Valido;
            
            IdUsuario = token.Claims
                .FirstOrDefault(c => c.Type.Equals(Claims.Id, StringComparison.OrdinalIgnoreCase))?.Value;
            Name = token.Claims
                .FirstOrDefault(c => c.Type.Equals(Claims.Nome, StringComparison.OrdinalIgnoreCase))?.Value;
       
        }
    }
}
