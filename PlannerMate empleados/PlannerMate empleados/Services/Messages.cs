using CommunityToolkit.Mvvm.Messaging.Messages;
using PlannerMate_empleados.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlannerMate_empleados.Services
{
    public class DiaSeleccionadoMessage : ValueChangedMessage<Day>
    {
        //difusión
        public DiaSeleccionadoMessage(Day dia) : base(dia)
        {

        }

        //solicitudes
        public class DiaSeleccionadoRequest : RequestMessage<Day> { }
        public class TrabajadoresRequest : RequestMessage<ObservableCollection<Trabajador>> { }

        public class CitaSeleccionadaRequest : RequestMessage<Cita> { }
        public class CitasRequest : RequestMessage<ObservableCollection<Cita>> { }
        public class ClienteSeleccionadoRequest : RequestMessage<Cliente> { }
    }
}
