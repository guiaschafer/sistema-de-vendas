using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDeVendas.Aplicacao.Entidades
{
    public class Produto
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public string UnidadeMedida { get; set; }
        public decimal ValorUnidade { get; set; }
    }
}
