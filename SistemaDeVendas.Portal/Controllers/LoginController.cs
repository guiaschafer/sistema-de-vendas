using SistemaDeVendas.Portal.Models;
using System.Web.Mvc;
using System.Web.Security;
using SistemaDeVendas.Aplicacao.Dto;
using SistemaDeVendas.Aplicacao.Servicos;

namespace SistemaDeVendas.Portal.Controllers
{
    public class LoginController : Controller
    {
        public ServicoUsuario _servicoUsuario;
        public ServicoAutenticacao _servicoAutenticacao;

        public LoginController(ServicoUsuario servicoUsuario, ServicoAutenticacao servicoAutenticacao)
        {
            _servicoUsuario = servicoUsuario;
            _servicoAutenticacao = servicoAutenticacao;
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
        public ActionResult Cadastrar(UsuarioDto usuario)
        {
            _servicoUsuario.Cadastrar(usuario);

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