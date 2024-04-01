using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlannerMate_empleados.Classes
{
    public class Cita
    {
        [Key]
        public int IdCita { get; set; }
        public string Nombre { get; set; }
        public string Contacto { get; set; }
        public DateTime Fecha { get; set; }       
        public string Descripcion { get; set; }

        [ForeignKey("Trabajador")]
        public int IdTrabajador { get; set; }
        public Trabajador Trabajador { get; set; }

        [ForeignKey("Cliente")]
        public int IdCliente { get; set; }
        public Cliente Cliente { get; set; }


        public override string ToString()
        {
            return $"{Descripcion}";
        }
    }
}
