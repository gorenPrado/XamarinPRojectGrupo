using MonkeyCache.FileStore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinCalendarProject.Base;
using XamarinCalendarProject.Models;
using XamarinCalendarProject.Repositorio;
using XamarinCalendarProject.Views;

namespace XamarinCalendarProject.ViewModels
{
    public class UsuarioViewModel : ViewModelBase
    {
        RepositorioCalendario repo;

        public UsuarioViewModel(RepositorioCalendario repo)
        {
            this.repo = repo;
            MessagingCenter.Subscribe<UsuarioViewModel>(this, "CARGAR", (sender) =>
            {
                Task.Run(async () =>
                {
                    await this.CargarPerfil();
                });
            });
            Task.Run(async () =>
            {
                await this.CargarPerfil();
            });
        }


        private async Task CargarPerfil()
        {
            String token = Barrel.Current.Get<String>("calendarioxamarin");
            Usuarios user = await this.repo.Perfil(token);
            this.Usuario = user;
        }

        private Usuarios _Usuario;
        public Usuarios Usuario
        {
            get { return this._Usuario; }
            set
            {
                this._Usuario = value;
                OnPropertyChanged("Usuario");
            }
        }
    }
}
