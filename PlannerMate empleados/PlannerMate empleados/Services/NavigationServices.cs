using PlannerMate_empleados.VMs;
using PlannerMate_empleados.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PlannerMate_empleados.Services
{
    static class NavigationServices
    {
        //public static UserControl LoadGestionDia() => new GestionDia();

        private static UserControl Trabajadores = null;
        private static UserControl Clientes = null;

        public static UserControl LoadCalendario() => new Calendario();

        public static UserControl LoadGestionDia() => new GestionDia();

        public static UserControl LoadAddCita() => new AddCitaWin();


        public static UserControl LoadTrabajadores() => Trabajadores == null ? new TrabajadoresWin() : Trabajadores;

        public static UserControl LoadClientes() => Clientes == null ? new ClientesWin() : Clientes;

        public static UserControl LoadEditarCita() => new EditarCitaWin();


        public static UserControl LoadAddCliente() => new AddClienteWin();
        public static UserControl LoadEditarCliente() => new EditarClienteWin();
    }
}
