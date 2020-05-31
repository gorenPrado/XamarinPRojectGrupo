using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinCalendarProject.Models
{
    public class Usuarios
    {
        [JsonProperty("idUsuario")]
        public int IdUsuario { get; set; }
        [JsonProperty("nombre")]
        public String Nombre { get; set; }
        [JsonProperty("oficio")]
        public String Oficio { get; set; }
        [JsonProperty("nacionalidad")]
        public String Nacionalidad { get; set; }
        [JsonProperty("correo")]
        public String Correo { get; set; }
    }
}
