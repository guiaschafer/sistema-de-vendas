using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDeVendas.Aplicacao.Entidades
{
    public class Pedido
    {
        public int Id { get; set; }
        public virtual Cliente Cliente { get; set; }
        public virtual Produto Item { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorTotal { get; set; }
    }
}
