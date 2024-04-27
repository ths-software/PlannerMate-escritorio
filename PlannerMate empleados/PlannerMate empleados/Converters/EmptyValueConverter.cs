using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PlannerMate_empleados.Converters
{
    class EmptyValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Verificar si el valor es nulo o una cadena vacía
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return "Ningún día seleccionado";
            }
            else
            {
                return $"Día seleccionado: {value}";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
