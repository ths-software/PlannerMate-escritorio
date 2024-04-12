using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlannerMate_empleados.Classes
{
    public class Trabajador
    {
        [Key]
        public int idTrabajador { get; set; }
        public string nombre { get; set; }
        public string contacto { get; set; }

        public ObservableCollection<Cita> Citas { get; set; }


        public Trabajador(int idTrabajador, string nombre, string contacto)
        {
            Citas = new ObservableCollection<Cita>();
            this.idTrabajador = idTrabajador;
            this.nombre = nombre;
            this.contacto = contacto;
        }

        public Trabajador()
        {
            nombre = "vacio";
            Citas = new ObservableCollection<Cita>();
        }

        public override bool Equals(object? obj)
        {
            return obj is Trabajador trabajador &&
                   idTrabajador == trabajador.idTrabajador &&
                   nombre == trabajador.nombre &&
                   contacto == trabajador.contacto;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(idTrabajador, nombre, contacto);
        }

        public override string ToString()
        {
            return nombre;
        }
    }
}
