using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SistemaDeVendas.Aplicacao.Dto;
using SistemaDeVendas.Aplicacao.Entidades.Enum;
using SistemaDeVendas.Portal.Util;

namespace SistemaDeVendas.Portal.Areas.Vendas.Controllers
{
    [Autorizar(Perfis = PerfilUsuario.Cliente)]
    public class ClienteController : Controller
    {
        // GET: Vendas/Cliente
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Cadastrar()
        {
            return View();
        }

        public ActionResult Atualizar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Cadastrar(ClienteDto model)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Atualizar(ClienteDto model)
        {
            return View();
        }
    }
}