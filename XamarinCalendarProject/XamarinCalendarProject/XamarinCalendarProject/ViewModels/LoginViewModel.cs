using MonkeyCache.FileStore;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using XamarinCalendarProject.Base;
using XamarinCalendarProject.Models;
using XamarinCalendarProject.Repositorio;
using XamarinCalendarProject.Views;

namespace XamarinCalendarProject.ViewModels
{
    public class LoginViewModel: ViewModelBase
    {
        RepositorioCalendario repo;

        public LoginViewModel(RepositorioCalendario repo)
        {
            this.repo = repo;
        }


        public Command Registro
        {
            get
            {
                return new Command(async () =>
                {
                    
                        Usuarios user = new Usuarios();
                        user.IdUsuario = 0;
                        user.Nombre = Nombre;
                        user.Oficio = Oficio;
                        user.Nacionalidad = Nacionalidad;
                        user.Correo = Correo;
                        
                        await this.repo.RegistrarUsuario(user);
                        await Application.Current.MainPage.DisplayAlert("Welcome!", "Gracias por registrarte", "Ok");
                        await Application.Current.MainPage.Navigation.PopModalAsync();
                    
                });
            }
        }
        public Command Registrarse
        {
            get
            {
                return new Command(async () =>
                {
                    LoginViewModel viewmodel = App.Locator.LoginViewModel;
                    RegistroView view = new RegistroView();
                    view.BindingContext = viewmodel;
                    await Application.Current.MainPage.Navigation.PushModalAsync(view);
                });
            }
        }

        public Command Login
        {
            get
            {
                return new Command(async () =>
                {
                    Usuarios user = await this.repo.Login(Correo, IdUsuario.ToString());
                    if (user != null)
                    {
                        MessagingCenter.Send(App.Locator.MainCalendarioView, "Refresh");
                        await Application.Current.MainPage.DisplayAlert("Iniciar sesión", "Bienvenido " + Correo, "Ok");
                        await App.Locator.LoginView.Navigation.PopModalAsync();
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Error", "Datos incorrectos", "Ok");
                    }
                });
            }
        }
        public Command Logout
        {
            get
            {
                return new Command(async () =>
                {
                    Barrel.Current.EmptyAll();
                    await Application.Current.MainPage.DisplayAlert("Cerrar sesión", "Se ha cerrado la sesión", "Ok");
                    MessagingCenter.Send(App.Locator.MainCalendarioView, "Refresh");
                });
            }
        }

        private int _IdUsuario;
        public int IdUsuario
        {
            get { return this._IdUsuario; }
            set
            {
                this._IdUsuario = value;
                OnPropertyChanged("IdUsuario");
            }
        }

        private String _Nombre;
        public String Nombre
        {
            get { return this._Nombre; }
            set
            {
                this._Nombre = value;
                OnPropertyChanged("Nombre");
            }
        }

        private String _Oficio;
        public String Oficio
        {
            get { return this._Oficio; }
            set
            {
                this._Oficio = value;
                OnPropertyChanged("Oficio");
            }
        }
        private String _Nacionalidad;
        public String Nacionalidad
        {
            get { return this._Nacionalidad; }
            set
            {
                this._Nacionalidad = value;
                OnPropertyChanged("Nacionalidad");
            }
        }
        private String _Correo;
        public String Correo
        {
            get { return this._Correo; }
            set
            {
                this._Correo = value;
                OnPropertyChanged("Correo");
            }
        }
    }
}
