//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Proyecto.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Vacaciones
    {
        public Nullable<System.DateTime> Año { get; set; }
        public string Comentarios { get; set; }
        public Nullable<int> Empleado { get; set; }
        public Nullable<System.DateTime> Fecha_Inicio { get; set; }
        public Nullable<System.DateTime> Fecha_Final { get; set; }
        public int id { get; set; }
    
        public virtual Empleados Empleados { get; set; }
    }
}
