using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actividad_18.Shared.Models
{
    public class Comandas
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public int Horas { get; set; }
        public DateTime FechaI { get; set; }
        public DateTime FechaF { get; set; }
        public int Id_Edificios {get; set; }
        public string? Nombre_Edificio {get ; set; }
        [ForeignKey("Trabajadores")]
        public int? TrabajadorId { get; set; }
        public virtual Trabajadores? Trabajador { get; set; }
    }
}
