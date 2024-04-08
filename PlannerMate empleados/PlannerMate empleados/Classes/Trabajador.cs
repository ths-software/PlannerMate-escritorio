using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlannerMate_empleados.Classes
{
    public class Trabajador
    {
        [Key]
        public int IdTrabajador { get; set; }
        public string Nombre { get; set; }
        public string Contacto { get; set; }

        public Trabajador(int idTrabajador, string nombre, string contacto)
        {
            IdTrabajador = idTrabajador;
            Nombre = nombre;
            Contacto = contacto;
        }

        public Trabajador()
        {
        }

        public override bool Equals(object? obj)
        {
            return obj is Trabajador trabajador &&
                   IdTrabajador == trabajador.IdTrabajador &&
                   Nombre == trabajador.Nombre &&
                   Contacto == trabajador.Contacto;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(IdTrabajador, Nombre, Contacto);
        }

        //public override bool Equals(object obj)
        //{
        //    if (obj == null || GetType() != obj.GetType())
        //        return false;

        //    Trabajador otroTrabajador = (Trabajador)obj;
        //    return Nombre.Equals(otroTrabajador.Nombre);
        //}

        //public override int GetHashCode()
        //{
        //    return Nombre.GetHashCode();
        //}




    }
}
