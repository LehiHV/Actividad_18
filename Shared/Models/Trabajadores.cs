using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actividad_18.Shared.Models
{
    public class Trabajadores
    {
        public int Id { get; set; }
        [Required (ErrorMessage ="El nombre es un campo Obligatorio")]
        public string? Nombre { get; set; }
        [Required(ErrorMessage = "El Puesto es un campo Obligatorio")]
        public string? Puesto { get; set; }
        [Required(ErrorMessage = "El Salario es obligatorio")]
        public int Salario { get; set; }
        [Required(ErrorMessage = "El Id del Supervisor es obligatorio, los Supervisores tienen su mismo Id")]
        public string? Id_Sup { get; set; }
        public virtual ICollection<Edificios>? Edificio { get; set; }
        public virtual ICollection<Comandas>? Comanda { get; set; }

    }
}
