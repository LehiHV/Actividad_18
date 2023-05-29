using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actividad_18.Shared.Models
{
    public class Edificios
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre es un campo Obligatorio")]
        public string? Nombre_Edificio { get; set; }
        [Required(ErrorMessage = "La Dirreccion es un campo Obligatorio")]
        public string? Dirreccion { get; set; }
        [Required(ErrorMessage = "El Tipo de Edificio es obligatorio")]
        public string? Tipo { get; set; }

        //
        [ForeignKey("Trabajadores")]
        public int? TrabajadoresId { get; set; }
        public virtual Trabajadores? Trabajador { get; set; }
    }
}
