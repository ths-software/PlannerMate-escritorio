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
        public int idCita { get; set; }
        public string nombre { get; set; }
        public string contacto { get; set; }
        public DateTime fecha { get; set; }
        public string descripcion { get; set; }
        public Trabajador trabajador { get; set; }
        public Cliente Cliente { get; set; }

        public Cita(int idCita, string nombre, string contacto, DateTime fecha, string descripcion, Trabajador trabajador, Cliente cliente)
        {
            this.idCita = idCita;
            this.nombre = nombre;
            this.contacto = contacto;
            this.fecha = fecha;
            this.descripcion = descripcion;
            this.trabajador = trabajador;
            this.Cliente = cliente;
        }

        public Cita()
        {
        }

        public override string ToString()
        {
            return $"{descripcion}";
        }
    }
}
