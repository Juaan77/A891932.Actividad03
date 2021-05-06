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

    }
}
