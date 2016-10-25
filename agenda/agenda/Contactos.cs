using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace agenda
{
    class Contactos
    {
        [Newtonsoft.Json.JsonProperty("Id")]
        public string Id { get; set; }
        //Campos de Azure que ya existen
        [Microsoft.WindowsAzure.MobileServices.Version]
        public string AzureVersion { get; set; }

        public string Nombre { get; set; }
        //public int Sexo { get; set;}
        //public string paterno { get; set; }
        //public string materno { get; set; }
        public string Telefono { get; set; }
    }
}
