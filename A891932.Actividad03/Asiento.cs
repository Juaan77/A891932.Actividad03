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
        public int Numero { get; }
        public DateTime Fecha { get; }
        /*public int CodigoCuenta { get; }
        public double Debe { get; }
        public double Haber { get; }*/
        public Dictionary<int, double> Debe = new Dictionary<int, double>();  // KEY: Nº de Cuenta || VALUE: Monto
        public Dictionary<int, double> Haber = new Dictionary<int, double>();  // KEY: Nº de Cuenta || VALUE: Monto

        public Asiento(int numero,int codigoCuenta, double debe, double haber)
        {
            Numero = numero;
            Fecha = DateTime.Today;
            CodigoCuenta = codigoCuenta;
            Debe = debe;
            Haber = Haber;
        }

        // Constructor para crear Asientos manualmente dentro de la aplicación.
        public Asiento(int numero)
        {
            Numero = numero;
            Fecha = DateTime.Today;

            bool salir = false;
            int codigo;
            double debe = 0;            
            double haber = 0;
            double debeTotal = 0;
            double haberTotal = 0;
            Dictionary<int, double> DebeTemporal = new Dictionary<int, double>();  // KEY: Nº de Cuenta || VALUE: Monto
            Dictionary<int, double> HaberTemporal = new Dictionary<int, double>();  // KEY: Nº de Cuenta || VALUE: Monto

            do
            {
                bool continuar = true;
                string deseaContinuar;
                Console.WriteLine("Codigos Disponibles:\n");
                LibroDiario.ImprimirPlanDeCuentas();

                do
                {
                    codigo = Validadores.Codigo($"Ingrese el codigo de cuenta del DEBE:");

                    if (!LibroDiario.PlanDeCuentas.ContainsKey(codigo))
                    {
                        Console.WriteLine($"El codigo '{codigo}' no está asociado a ninguna cuenta dentro del Plan de cuentas");
                        Console.ReadKey();
                    }
                    else
                    {
                        debe = Validadores.NumeroPositivo("Ingrese el monto:");
                        deseaContinuar = Validadores.SoN("Desea agregar mas cuentas dentro del DEBE? (S)i o (N)o");

                        if(deseaContinuar == "N")
                        {
                            continuar = false;
                        }

                        DebeTemporal.Add(codigo, debe);
                        debeTotal += debe;
                    }
                } while (continuar == true);

                continuar = true;

                do
                {
                    codigo = Validadores.Codigo($"Ingrese el codigo de cuenta del HABER:");

                    if (!LibroDiario.PlanDeCuentas.ContainsKey(codigo))
                    {
                        Console.WriteLine($"El codigo '{codigo}' no está asociado a ninguna cuenta dentro del Plan de cuentas");
                        Console.ReadKey();
                    }
                    else
                    {
                        haber = Validadores.NumeroPositivo("Ingrese el monto:");
                        deseaContinuar = Validadores.SoN("Desea agregar mas cuentas dentro del HABER? (S)i o (N)o");

                        if (deseaContinuar == "N")
                        {
                            continuar = false;
                        }

                        HaberTemporal.Add(codigo, haber);
                        haberTotal += haber;
                    }
                } while (continuar == true);

                if(debeTotal != haber)
                {
                    Console.WriteLine($"ERROR: El DEBE ({debeTotal}) no es IGUAL al HABER ({haberTotal}). Intente nuevamente...");
                    Console.ReadKey();
                    DebeTemporal.Clear();
                    HaberTemporal.Clear();
                }
                else
                {
                    salir = true;
                    Debe = DebeTemporal;
                    Haber = HaberTemporal;
                }

            } while (salir == false);

            Console.Clear();
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
