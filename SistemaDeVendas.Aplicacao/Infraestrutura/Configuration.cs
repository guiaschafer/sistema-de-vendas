using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDeVendas.Aplicacao.Infraestrutura
{
    public class Configuration:DbMigrationsConfiguration<Contexto>
    {
        public Configuration()
        {
            CommandTimeout = int.MaxValue;
            AutomaticMigrationsEnabled = false;
            ContextKey = typeof(Contexto).FullName;
            MigrationsDirectory = "Infraestrutura\\Migrations";

        }
    }
}
