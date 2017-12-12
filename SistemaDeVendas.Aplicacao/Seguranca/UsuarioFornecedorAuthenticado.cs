using System.Linq;
using AMcom.Persistencia.EntityFramework;
using Jequiti.Aplicacao.Servicos;
using Jequiti.Dominio.Entidades;
using Jequiti.Infra.EntityFramework;

namespace Jequiti.Aplicacao.Infra.Seguranca
{
    public static class UsuarioFornecedorAutenticado
    {
        public static string ObterCnpj()
        {
            using (var escopo = new JequitiContexto())
            {
                using (var repositorioEscopo = new EntityFrameworkRepositorioEscopo(escopo))
                {
                    var repositorioFornecedor = repositorioEscopo.ObterRepositorio<Fornecedor>();

                    var idUsuario = ServicoAutenticacao.ObterIdUsuarioToken();

                    return repositorioFornecedor.Filtrar(f => f.Usuario.Id == idUsuario)
                        .FirstOrDefault().Cnpj;
                }
            }
        }
    }
}