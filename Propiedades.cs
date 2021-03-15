using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pago_de_Propiedades
{
    class Propiedades
    {
        string numero_casa;
        string dpi;
        double mantenimiento;

        public string Numero_casa { get => numero_casa; set => numero_casa = value; }
        public string Dpi { get => dpi; set => dpi = value; }
        public double Mantenimiento { get => mantenimiento; set => mantenimiento = value; }
        
    }
}
