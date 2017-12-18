using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SistemaDeVendas.Aplicacao.Dto;
using SistemaDeVendas.Aplicacao.Entidades.Enum;
using SistemaDeVendas.Aplicacao.Servicos;
using SistemaDeVendas.Portal.Util;

namespace SistemaDeVendas.Portal.Areas.Vendas.Controllers
{
    [Autorizar(Perfis = PerfilUsuario.Vendedor)]
    public class UsuarioController : Controller
    {
        private readonly ServicoUsuario _servicoUsuario;
        public UsuarioController(ServicoUsuario servicoUsuario)
        {
            _servicoUsuario = servicoUsuario;
        }

        // GET: Vendas/Usuario
        public ActionResult Index()
        {
            var lista = _servicoUsuario.Obtertodos();
            if (lista == null)
            {
                lista = new List<UsuarioDto>();
            }
            return View(lista);
        }

        public ActionResult Cadastrar()
        {
            return View();
        }

        public ActionResult Atualizar(int id)
        {
            var usuario = _servicoUsuario.Obter(id);
            return View(usuario);
        }

        [HttpPost]
        public ActionResult Cadastrar(UsuarioDto model)
        {
            _servicoUsuario.Cadastrar(model);
            return View("Index");
        }


        //[HttpPost]
        //public ActionResult Atualizar(UsuarioDto model)
        //{
        //    _servicoUsuario.Cadastrar(model);
        //    return View("Index");
        //}
    }
}