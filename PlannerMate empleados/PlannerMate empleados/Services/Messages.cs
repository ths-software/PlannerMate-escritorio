using CommunityToolkit.Mvvm.Messaging.Messages;
using PlannerMate_empleados.Classes;
using System;
using System.Collections.Generic;
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

        //solicitud
        public class DiaSeleccionadoRequest : RequestMessage<Day> { }
    }
}
