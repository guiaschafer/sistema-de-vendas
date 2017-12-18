using System;
using SistemaDeVendas.Portal.Models;
using System.Web.Mvc;
using System.Web.Security;
using SistemaDeVendas.Aplicacao.Dto;
using SistemaDeVendas.Aplicacao.Servicos;
using SistemaDeVendas.Aplicacao.Seguranca;

namespace SistemaDeVendas.Portal.Controllers
{
    public class LoginController : Controller
    {
        public ServicoUsuario _servicoUsuario;
        public ServicoAutenticacao _servicoAutenticacao;
        public ServicoCliente _servicoCliente;

        public LoginController(ServicoUsuario servicoUsuario, ServicoAutenticacao servicoAutenticacao, ServicoCliente servicoCliente)
        {
            _servicoUsuario = servicoUsuario;
            _servicoAutenticacao = servicoAutenticacao;
            _servicoCliente = servicoCliente;
        }

        // GET: Login
        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
                return RedirectToAction("Index", "Home", new { Area = "Vendas" });
            return View();
        }

        public ActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Cadastrar(ClienteDto cliente)
        {
            try
            {
                if(cliente.Senha != cliente.ConfirmarSenha)
                {
                    TempData["Erro"] = "Senhas não conferem";
                    return View("Cadastrar", cliente);
                }
                _servicoCliente.Cadastrar(cliente);
            }
            catch (Exception e)
            {
                TempData["Erro"] = e.Message;
                return View(cliente);
            }

            return View("Index");
        }

        [HttpPost]
        public ActionResult Index(AutenticacaoDto model)
        {
            var retorno = _servicoAutenticacao.Autenticar(model);

            if (retorno != null)
            {
                var cookie = _servicoAutenticacao.GerarCookieComToken(retorno);
                Response.Cookies.Add(cookie);
                Session["Chave"] = ChaveAssimetrica.GetKeyPrivateFromContainer(model.Login);
                return RedirectToAction("Index", "Home", new { Area = "Vendas" });
            }

            return View();
        }

        [Authorize]
        public ActionResult Logoff(FormCollection f)
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }
    }
}