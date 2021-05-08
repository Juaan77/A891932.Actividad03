using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A891932.Actividad03
{
    class Asiento
    {
        public int Numero { get; }
        public DateTime Fecha { get; }

        public Dictionary<int, double> Debe = new Dictionary<int, double>();  // KEY: Nº de Cuenta || VALUE: Monto
        public Dictionary<int, double> Haber = new Dictionary<int, double>();  // KEY: Nº de Cuenta || VALUE: Monto

        // Constructor para crear Asientos manualmente dentro de la aplicación.
        // Se puede invocar desde el menu principal.
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
            Dictionary<int, double> DebeTemporal = new Dictionary<int, double>();   // KEY: Nº de Cuenta || VALUE: Monto
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

        // Constructor para importar asientos desde Diario.txt
        // Se ejecuta unicamente al inicio.
        public Asiento(string linea)
        {
            var datos = linea.Split('|');            
            Numero = int.Parse(datos[0]);
            Fecha = DateTime.Parse(datos[1]);
            int codigoCuenta = int.Parse(datos[2]);
            var debe = double.Parse(datos[3]);
            var haber = double.Parse(datos[4]);

            bool finDeAsiento = false;

            do
            {

            }while(finDeAsiento = false);
        }
        
        public string Serializar()
        {
            // return $"{Numero}|{Fecha}|{CodigoCuenta}|{Debe}|{Haber}";
            string retorno = "";
            string padding = "";    // Solucion guarra para lograr el padding.

            foreach(var item in Debe)
            {
                if(Debe[0] == item.Value)
                {
                    retorno += $"{Numero.ToString().PadRight(5)} | {Fecha.ToString().PadRight(20)} | {item.Value.ToString().PadLeft(15)} |";
                }
                else
                {
                    retorno += $"{padding.PadRight(5)}|{padding.ToString().PadRight(20)}|{item.Value.ToString().PadLeft(15)}|";
                }
            }

            foreach(var item in Haber)
            {
                retorno += $"{padding.PadRight(5)}|{padding.ToString().PadRight(20)}|{padding.PadLeft(15)}|{item.Value.ToString().PadLeft(15)}";
            }

            return retorno;
        }

    }
}
