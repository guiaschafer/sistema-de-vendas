using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SistemaDeVendas.Aplicacao.Dto;
using SistemaDeVendas.Aplicacao.Entidades.Enum;
using SistemaDeVendas.Aplicacao.Servicos;
using SistemaDeVendas.Portal.Util;
using SistemaDeVendas.Aplicacao.Seguranca;

namespace SistemaDeVendas.Portal.Areas.Vendas.Controllers
{
    [Autorizar(Perfis = PerfilUsuario.Cliente)]
    public class ClienteController : Controller
    {
        private readonly ServicoCliente _servicoCliente;

        public ClienteController(ServicoCliente servicoCliente)
        {
            _servicoCliente = servicoCliente;
        }
        // GET: Vendas/Cliente

        [Autorizar(Perfis = PerfilUsuario.Vendedor | PerfilUsuario.Gerente)]

        public ActionResult Index()
        {
            var clientes = _servicoCliente.ObterTodos();
            return View(clientes);
        }

        [Autorizar]
        public ActionResult Cadastrar()
        {
            return View();
        }

        [Autorizar(Perfis = PerfilUsuario.Cliente)]

        public ActionResult Detalhes()
        {
            var cliente = _servicoCliente.ObterClientePorUsuario(Int32.Parse((User.Identity as Identidade).IdUsuario), Session["Chave"].ToString());

            return View(cliente);
        }
        [Autorizar]

        [HttpPost]
        public ActionResult Cadastrar(ClienteDto model)
        {

            _servicoCliente.Cadastrar(model);
            return View();
        }

        [Autorizar]
        public ActionResult Atualizar(int id)
        {
            var cliente = _servicoCliente.ObterCliente(id);

            if (cliente == null)
            {
                return View("Index");
            }

            return View(cliente);
        }
    }
}