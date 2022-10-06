using Microsoft.Reporting.WinForms;
using System;

namespace MAMACONEJA
{
    public class DatosEtiquetas
    {        
        internal static ReportDataSource datasource;        
        public int Articulo_id { get; set; }
        public string Clave_Articulo { get; set; }
        public string Producto { get; set; }
        public string Linea { get; set; }
        public double Precio { get; set; }
        public double UnidadLista { get; set; }
        public double PrecioMayoreo { get; set; }
        public double UnidadMayoreo { get; set; }
        public double PrecioMedMayoreo { get; set; }
        public double UnidadMedMayoreo { get; set; }
        public string Uv { get; set; }
        public DateTime FechaMod { get; set; }
    }
}
