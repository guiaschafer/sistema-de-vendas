using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDeVendas.Aplicacao.Entidades
{
    public class ItemPedido
    {
        public int Id { get; set; }
        public int Quantidade { get; set; }
        public virtual Produto Item {get;set;}
        public virtual Pedido Pedido { get; set; }
    }
}
