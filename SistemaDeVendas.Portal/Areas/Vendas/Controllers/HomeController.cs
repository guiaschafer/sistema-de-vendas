using SistemaDeVendas.Portal.Util;
using System.Web.Mvc;

namespace SistemaDeVendas.Portal.Areas.Vendas.Controllers
{
    [Autorizar]
    public class HomeController : Controller
    {
        // GET: Vendas/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}