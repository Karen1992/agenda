using System;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.Sync;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using agenda;

namespace agenda
{
    class AzureDataService
        //Clase donde se hace la conexion de Azure
    {
        public MobileServiceClient MobileService { get; set; } //Conexion al BackEnd de Azure
        IMobileServiceSyncTable<Contactos> tablaContactos;
     
        bool isInitialized;
        public async Task Initialized()
        {
            if (isInitialized)
                return;

            MobileService = new MobileServiceClient("@http://teshuix.azurewebsites.net");//URL Azure

            const string path = "syncstore-contactos.db"; //Datos locales

            var store = new MobileServiceSQLiteStore(path);
            store.DefineTable<Contactos>();

            await MobileService.SyncContext.InitializeAsync(store, new MobileServiceSyncHandler()); //Inicializacion asincrona
            tablaContactos = MobileService.GetSyncTable<Contactos>();
            isInitialized = true; //Inicializacion de la BD
             
        }

            public async Task<IEnumerable<Contactos>> ObtenerContactos()//Metodo consultar contactos
        {
            await Initialized();
            await SyncContactos();
            return await tablaContactos.OrderBy(a => a.Nombre).ToEnumerableAsync(); //Select y coleccion enumerable

        }

        public async Task<Contactos> ObtenerContacto(String id)//Metodo para consultar un solo contacto
        {
            await Initialized();
            await SyncContactos();
            return (await tablaContactos.Where(a => a.Id == id).Take(1).ToEnumerableAsync()).FirstOrDefault();//Devolver solo el primero
        }

        public async Task<Contactos> AgregarContacto(String nombre, String telefono)//Metodo agregar contacto
        {
            await Initialized();
            var item = new Contactos
        {
            Nombre= nombre,
            //Sexo= sexo,
            Telefono= telefono

        };

        await tablaContactos.InsertAsync(item);
        await SyncContactos();
        return item;
        }

       public async Task<Contactos> ActualizarContacto(String id, String nombre, string telefono)//Metodo de actualizar contacto
    {
            await Initialized();
            var item = await ObtenerContacto(id);
        {
                item.Nombre = nombre;
               // item.Sexo = sexo;
                item.Telefono = telefono;
        };

        await tablaContactos.UpdateAsync(item);
        await SyncContactos();
        return item;
    }

    public async Task EliminarContacto(string id)//Metodo de eliminar contacto
    {
        await Initialized();
        var item = await ObtenerContacto(id);
        await tablaContactos.DeleteAsync(item);
        await SyncContactos();//Manda al almacenamiento remoto

    }

    public async  Task SyncContactos()  //Obtiene los datos locales para actualizar la BD de Azure
    {
        await tablaContactos.PullAsync("Contactos", tablaContactos.CreateQuery());
        await MobileService.SyncContext.PushAsync();

    }

}
}
