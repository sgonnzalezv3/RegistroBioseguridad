using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RegistroBioseguridad.Models.ViewModels
{
    public class ConsultaViewModel
    {
        public string Identificacion { get; set; }

        public int CodigoIngreso { get; set; }

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public string Organizacion { get; set; }

        public string FechaIngreso { get; set; }
        public string Icono { get; set; }
    }
}