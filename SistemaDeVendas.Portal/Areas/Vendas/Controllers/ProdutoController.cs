using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SistemaDeVendas.Aplicacao.Dto;
using SistemaDeVendas.Portal.Util;
using SistemaDeVendas.Aplicacao.Servicos;

namespace SistemaDeVendas.Portal.Areas.Vendas.Controllers
{
    [Autorizar(Perfis = Aplicacao.Entidades.Enum.PerfilUsuario.Gerente | Aplicacao.Entidades.Enum.PerfilUsuario.Vendedor)]
    public class ProdutoController : Controller
    {
        private readonly ServicoProduto _servicoProduto;

        public ProdutoController(ServicoProduto servicoProduto)
        {
            _servicoProduto = servicoProduto;
        }

        [Autorizar(Perfis = Aplicacao.Entidades.Enum.PerfilUsuario.Vendedor | Aplicacao.Entidades.Enum.PerfilUsuario.Gerente)]
        // GET: Vendas/Produto
        public ActionResult Index()
        {
            var produtos = _servicoProduto.Obtertodos();
            return View(produtos);
        }

        [Autorizar(Perfis = Aplicacao.Entidades.Enum.PerfilUsuario.Gerente)]

        public ActionResult Cadastrar()
        {
            return View();
        }

        [Autorizar(Perfis = Aplicacao.Entidades.Enum.PerfilUsuario.Vendedor | Aplicacao.Entidades.Enum.PerfilUsuario.Gerente)]
        public ActionResult Atualizar(int id)
        {
            var produto = _servicoProduto.Obter(id);
            return View(produto);
        }

        [Autorizar(Perfis = Aplicacao.Entidades.Enum.PerfilUsuario.Gerente)]
        [HttpPost]
        public ActionResult Cadastrar(ProdutoDto model)
        {
            _servicoProduto.Cadastrar(model);
            return RedirectToAction("Index");
        }

        [Autorizar(Perfis = Aplicacao.Entidades.Enum.PerfilUsuario.Vendedor | Aplicacao.Entidades.Enum.PerfilUsuario.Gerente)]
        [HttpPost]
        public ActionResult Atualizar(ProdutoDto model)
        {
            _servicoProduto.Atualizar(model);
            return RedirectToAction("Index");
        }


    }
}