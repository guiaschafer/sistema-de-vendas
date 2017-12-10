using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SistemaDeVendas.Aplicacao.Dto;

namespace SistemaDeVendas.Portal.Controllers
{
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