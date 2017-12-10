using SistemaDeVendas.Portal.Models;
using System.Web.Mvc;
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



            return RedirectToAction("Index","Home",retorno);
        }
    }
}