using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A891932.Actividad03
{
    class Asiento
    {
        // NroAsiento|Fecha|CodigoCuenta|Debe|Haber
        int Numero { get; }
        DateTime Fecha { get; }
        int CodigoCuenta { get; }
        double Debe { get; }
        double Haber { get; }

        public Asiento(int numero, int codigoCuenta, double debe, double haber)
        {
            Numero = numero;
            Fecha = DateTime.Today;
            CodigoCuenta = codigoCuenta;
            Debe = debe;
            Haber = Haber;
        }

        public Asiento(string linea)
        {
            var datos = linea.Split('|');
            Numero = int.Parse(datos[0]);
            Fecha = DateTime.Parse(datos[1]);
            CodigoCuenta = int.Parse(datos[2]);
            Debe = double.Parse(datos[3]);
            Haber = double.Parse(datos[4]);
        }

        public string Serializar()
        {
            return $"{Numero}|{Fecha}|{CodigoCuenta}|{Debe}|{Haber}";
        }

    }
}
