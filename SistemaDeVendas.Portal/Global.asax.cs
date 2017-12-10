using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper;
using SimpleInjector;
using SimpleInjector.Integration.Web.Mvc;
using SistemaDeVendas.Aplicacao.AutoMapper;
using SistemaDeVendas.Aplicacao.Servicos;

namespace SistemaDeVendas.Portal
{
    public class MvcApplication : System.Web.HttpApplication
    {
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
    }


}
