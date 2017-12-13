using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using AutoMapper;
using SimpleInjector;
using SimpleInjector.Integration.Web.Mvc;
using SistemaDeVendas.Aplicacao.AutoMapper;
using SistemaDeVendas.Aplicacao.Seguranca;
using SistemaDeVendas.Aplicacao.Servicos;

namespace SistemaDeVendas.Portal
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public override void Init()
        {
            this.AuthenticateRequest += TratarRequisicaoAutenticacao;
            base.Init();
        }

        protected void Application_Start()
        {
            AutoMapperConfig.RegisterMappings();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Mapper.Initialize(configuration =>
            {
                var map = AutoMapperConfig.GetProfiles()
                    .Union(AutoMapperConfig.GetProfiles());

                foreach (Profile profile in map)
                {
                    configuration.AddProfile(profile);
                }
            });
            ConfigurarDependencias();
        }

        private void ConfigurarDependencias()
        {
            var container = new Container();

            container.Register<ServicoAutenticacao>();
            //container.Register<ServiceDisciplina>();
            //container.Register<ServiceLaboratorio>();
            container.Register<ServicoUsuario>();
            //container.Register<ServiceAgendamento>();


            container.Verify();

            DependencyResolver.SetResolver(
                new SimpleInjectorDependencyResolver(container));
        }

        private void TratarRequisicaoAutenticacao(object sender, EventArgs e)
        {
            HttpCookie authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];

            if (authCookie != null)
            {
                string encTicket = authCookie.Value;
                if (!String.IsNullOrWhiteSpace(encTicket))
                {
                    FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(encTicket);
                    if (!ticket.Expired)
                    {
                        var servicoAutenticacao = DependencyResolver.Current.GetService<ServicoAutenticacao>();
                        var token = new Token(ticket.UserData);

                        var principal = (servicoAutenticacao.Autenticar(token) != null) ?
                            servicoAutenticacao.CriarIdentidadePrincipal(token) :
                            servicoAutenticacao.CriarIdentidadePrincipalVazia();

                        HttpContext.Current.User = principal;
                        Thread.CurrentPrincipal = principal;
                    }
                }
            }
        }

    }


}
