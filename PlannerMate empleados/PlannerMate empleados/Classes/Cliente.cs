using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlannerMate_empleados.Classes
{
    public class Cliente
    {
        [Key]
        public int IdCliente { get; set; }
        public string Nombre { get; set; }
        public string Contacto { get; set; }
        public string Password { get; set; }

    }
}
