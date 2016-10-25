using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;



using Xamarin.Forms;


namespace agenda
{
    public class App : Application
    {
        public static AzureDataService AzureService;//Aqui no se por que no me reconoce la clase creada

        public App()
        {
            AzureService = new AzureDataService();
            MainPage = new NavigationPage(new PaginaListaContactos()); //Pagina donde se manda llamara la pgina en xaml

        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
