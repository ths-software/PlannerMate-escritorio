using PlannerMate_empleados.Classes;
using PlannerMate_empleados.VMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PlannerMate_empleados.Windows
{
    public partial class GestionDia : UserControl
    {

        GestionDiaVM vm;
        public GestionDia()
        {
            vm = new GestionDiaVM();
            InitializeComponent();
            this.DataContext = vm;
        }


    }
}
