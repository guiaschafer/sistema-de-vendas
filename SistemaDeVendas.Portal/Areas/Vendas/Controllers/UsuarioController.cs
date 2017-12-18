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
    [Autorizar]
    public class UsuarioController : Controller
    {
        private readonly ServicoUsuario _servicoUsuario;
        private readonly ServicoPerfil _servicoPerfil;
        public UsuarioController(ServicoUsuario servicoUsuario, ServicoPerfil servicoPerfil)
        {
            _servicoUsuario = servicoUsuario;
            _servicoPerfil = servicoPerfil;
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
            
            ViewBag.SelectListPerfil = _servicoPerfil.ObterTodos();
            return View();
        }
        
        [HttpPost]
        public ActionResult Cadastrar(UsuarioDto model)
        {
            try
            {
                _servicoUsuario.Cadastrar(model);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                TempData["Erro"] = e.Message;
                return View(model);
            }
           
        }

    }
}