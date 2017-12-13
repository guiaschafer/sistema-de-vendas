using SistemaDeVendas.Aplicacao.Dto;
using SistemaDeVendas.Aplicacao.Entidades.Enum;
using SistemaDeVendas.Portal.Util;
using System.Web.Mvc;

namespace SistemaDeVendas.Portal.Controllers
{
    [Autorizar(Perfis = PerfilUsuario.Cliente)]
    public class HomeController : Controller
    {
        public ActionResult Index(UsuarioDto usuario)
        {
            return View(usuario);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}