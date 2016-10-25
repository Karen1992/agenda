using System;


using Xamarin.Forms;

namespace agenda
{
    public partial class PaginaContacto : ContentPage
    {
        public string ID = "";

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            if(ID != "")//Evalua si es vacia o no
            {
                Contactos contacto = await App.AzureService.ObtenerContacto(ID);
                txtNombre.Text = contacto.Nombre;
                //pckSexo.SelectedIndex= contacto.Sexo;
                txtTelefono.Text = contacto.Telefono;

            }
        }
        public void btnGuardar_Click(object sender, EventArgs a)
        {
            String nombre = "txtNombre.Text";
            //int sexo = "pckSexo.SelectedIndex";
            String telefono = "txtTelefono.Text";

            if (ID == String.Empty)
            {
                App.AzureService.AgregarContacto(nombre,telefono);
            }
            else
            {
                App.AzureService.ActualizarContacto(ID, nombre,telefono);
                Navigation.PopAsync();//Regresa a la pagina anterior
            }
        }
            
            public void btnEliminar_Click(Object sender, EventArgs a)
            {
                if(ID !=  "")
                {
                    App.AzureService.EliminarContacto(ID);
                    Navigation.PopAsync(); 
                }
            }
       
        public PaginaContacto ()
        {
            InitializeComponent();
        }
    }

}
