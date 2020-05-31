using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinCalendarProject.Models
{
    public class Eventos
    {
        [JsonProperty("idEvento")]
        public int IdEvento { get; set; }
        [JsonProperty("fechaInc")]
        public DateTime FechaInc { get; set; }
        [JsonProperty("fechaFnl")]
        public DateTime FechaFnl { get; set; }
        [JsonProperty("descripcion")]
        public String Descripcion { get; set; }
        [JsonProperty("idUsuario")]
        public int IdUsuario { get; set; }
    }
}
