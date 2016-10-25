using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using agenda;

using Xamarin.Forms;

namespace agenda
{
    public partial class PaginaListaContactos : ContentPage
    {
        public PaginaListaContactos()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()//Cuando se este cargando esta pagina
        {
            base.OnAppearing();
            lsvContactos.ItemsSource = await App.AzureService.ObtenerContactos();
                         //origen de datos
        }


        private void lsvContactos_Selected(object sender, SelectedItemChangedEventArgs e)//Manejador de evento
        {
            if (e.SelectedItem  != null)
            {
                Contactos contacto = e.SelectedItem as Contactos;
                PaginaContacto pagina = new PaginaContacto();
                pagina.ID = contacto.Id;
                Navigation.PushAsync(pagina);

            }
        }

        void  btnNuevo_Click(Object sender, EventArgs a)
        {
            Navigation.PushAsync(new PaginaContacto());
        }
    }
}
