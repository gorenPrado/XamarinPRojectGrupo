using MonkeyCache.FileStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinCalendarProject.Code;

namespace XamarinCalendarProject.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainCalendarioView : MasterDetailPage
    {
        public MainCalendarioView()
        {
            InitializeComponent();
            this.CargarMaster();

            MessagingCenter.Subscribe<MainCalendarioView>(this, "Refresh", (sender) =>
            {
                this.CargarMaster();
            });
            MessagingCenter.Subscribe<MainCalendarioView>(this, "RefreshSoloMenu", (sender) =>
            {
                this.CargarMasterSoloMenu();
            });
        }
        private void CargarMaster()
        {
            List<MasterPageItem> menu = new List<MasterPageItem>();

            MasterPageItem item = new MasterPageItem();
            item.Imagen = "";
            item.Titulo = "Calendario App";
            item.Pagina = typeof(CalendarioView);
            menu.Add(item);

            if (Barrel.Current.IsExpired(key: "calendarioxamarin"))
            {
                item = new MasterPageItem();
                item.Imagen = "";
                item.Titulo = "Iniciar sesión";
                item.Pagina = typeof(LoginView);
                menu.Add(item);
            }
            else
            {
                item = new MasterPageItem();
                item.Imagen = "";
                item.Titulo = "Tu perfil";
                item.Pagina = typeof(PerfilView);
                menu.Add(item);

                item = new MasterPageItem();
                item.Imagen = "";
                item.Titulo = "Cerrar sesión";
                item.Pagina = typeof(LogoutView);
                menu.Add(item);
            }

            this.lsvmenu.ItemsSource = menu;
            Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(CalendarioView)));
        }

        private void CargarMasterSoloMenu()
        {
            List<MasterPageItem> menu = new List<MasterPageItem>();


            MasterPageItem item = new MasterPageItem();
            item.Imagen = "";
            item.Titulo = "Calendario App";
            item.Pagina = typeof(CalendarioView);
            menu.Add(item);

            if (Barrel.Current.IsExpired(key: "calendarioxamarin"))
            {
                item = new MasterPageItem();
                item.Imagen = "";
                item.Titulo = "Iniciar sesión";
                item.Pagina = typeof(LoginView);
                menu.Add(item);
            }
            else
            {
                item = new MasterPageItem();
                item.Imagen = "";
                item.Titulo = "Tu perfil";
                item.Pagina = typeof(PerfilView);
                menu.Add(item);

                item = new MasterPageItem();
                item.Imagen = "";
                item.Titulo = "Cerrar sesión";
                item.Pagina = typeof(LogoutView);
                menu.Add(item);
            }

            this.lsvmenu.ItemsSource = menu;

        }
        private void lsvmenu_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            MasterPageItem itemselected = e.SelectedItem as MasterPageItem;
            Type pagina = itemselected.Pagina;
            Detail = new NavigationPage((Page)Activator.CreateInstance(pagina));

            IsPresented = false;
        }
    }
}