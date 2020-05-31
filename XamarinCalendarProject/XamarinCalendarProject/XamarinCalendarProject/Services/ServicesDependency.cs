using Autofac;
using System;
using System.Collections.Generic;
using System.Text;
using XamarinCalendarProject.Repositorio;
using XamarinCalendarProject.ViewModels;
using XamarinCalendarProject.Views;

namespace XamarinCalendarProject.Services
{
    public class ServiceDependency
    {

        private IContainer container;

        public ServiceDependency()
        {
            RegisterDependencies();
        }

        private void RegisterDependencies()
        {
            ContainerBuilder builder = new ContainerBuilder();
            
            builder.RegisterType<RepositorioCalendario>();
            builder.RegisterType<CalendarioViewModel>();

            builder.RegisterType<MainCalendarioView>();

            builder.RegisterType<LoginView>().SingleInstance();
            builder.RegisterType<LoginViewModel>();
            builder.RegisterType<UsuarioViewModel>();


            container = builder.Build();
        }
        public CalendarioViewModel CalendarioViewModel
        {
            get
            {
                return container.Resolve<CalendarioViewModel>();
            }
        }
        public MainCalendarioView MainCalendarioView
        {
            get
            {
                return container.Resolve<MainCalendarioView>();
            }
        }
        public LoginView LoginView
        {
            get
            {
                return this.container.Resolve<LoginView>();
            }
        }
        public LoginViewModel LoginViewModel
        {
            get
            {
                return container.Resolve<LoginViewModel>();
            }
        }
        public UsuarioViewModel UsuarioViewModel
        {
            get
            {
                return container.Resolve<UsuarioViewModel>();
            }
        }
    }
}
