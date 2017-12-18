using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDeVendas.Aplicacao.Dto
{
    public class ClienteDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Cpf { get; set; }
        public string Telefone { get; set; }
        public string NumeroCartao { get; set; }
        public string CodigoSeguranca { get; set; }
        public string Senha { get; set; }
        public string ConfirmarSenha { get; set; }
    }
}
