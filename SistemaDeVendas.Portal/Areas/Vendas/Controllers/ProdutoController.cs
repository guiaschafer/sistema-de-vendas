using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SistemaDeVendas.Aplicacao.Dto;

namespace SistemaDeVendas.Portal.Areas.Vendas.Controllers
{
    public class ProdutoController : Controller
    {
        // GET: Vendas/Produto
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
        public ActionResult Cadastrar(ProdutoDto model)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Atualizar(ProdutoDto model)
        {
            return View();
        }


    }
}