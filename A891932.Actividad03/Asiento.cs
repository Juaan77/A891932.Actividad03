using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A891932.Actividad03
{
    class Asiento
    {
        public int Numero { get; set; }
        public DateTime Fecha { get; set; }

        public Dictionary<int, double> Debe = new Dictionary<int, double>();    // KEY: Nº de Cuenta || VALUE: Monto
        public Dictionary<int, double> Haber = new Dictionary<int, double>();   // KEY: Nº de Cuenta || VALUE: Monto

        // Constructor para crear Asientos manualmente dentro de la aplicación.
        // Se puede invocar desde el menu principal.
        public Asiento(int numero)
        {
            Numero = numero;
            Fecha = DateTime.Now;

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
                Console.WriteLine("Codigos de cuentas disponibles:\n");
                LibroDiario.ImprimirPlanDeCuentas();

                do
                {
                    debe = 0;          // Reset de variables para evitar acarreo de errores.
                    haber = 0;
                    codigo = Validadores.Codigo($"\nIngrese el codigo de cuenta del DEBE:");

                    if (!LibroDiario.PlanDeCuentas.ContainsKey(codigo))
                    {
                        Console.WriteLine($"El codigo '{codigo}' no está asociado a ninguna cuenta dentro del Plan de cuentas. Intente nuevamente...");
                        Console.ReadKey();
                    }
                    else
                    {
                        debe = Validadores.NumeroPositivo($"Ingrese el monto de '{LibroDiario.PlanDeCuentas[codigo].Nombre}':");
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
                    debe = 0;       // Reset de variables para evitar acarreo de errores.
                    haber = 0;
                    codigo = Validadores.Codigo($"\nIngrese el codigo de cuenta del HABER:");

                    if (!LibroDiario.PlanDeCuentas.ContainsKey(codigo))
                    {
                        Console.WriteLine($"El codigo '{codigo}' no está asociado a ninguna cuenta dentro del Plan de cuentas. Intente nuevamente...");
                        Console.ReadKey();
                    }
                    else
                    {
                        haber = Validadores.NumeroPositivo($"Ingrese el monto de '{LibroDiario.PlanDeCuentas[codigo].Nombre}':");
                        deseaContinuar = Validadores.SoN("Desea agregar mas cuentas dentro del HABER? (S)i o (N)o");

                        if (deseaContinuar == "N")
                        {
                            continuar = false;
                        }

                        HaberTemporal.Add(codigo, haber);
                        haberTotal += haber;                        
                    }
                } while (continuar == true);

                if(debeTotal != haberTotal)
                {
                    Console.WriteLine($"ERROR: El DEBE ({debeTotal}) no es IGUAL al HABER ({haberTotal}). Intente nuevamente...");
                    Console.ReadKey();
                    DebeTemporal.Clear();
                    HaberTemporal.Clear();
                    debeTotal = 0;              // Reset de variables para eliminar acarreo de errores.
                    haberTotal = 0;             // Reset de variables para eliminar acarreo de errores.
                }
                else
                {
                    salir = true;
                    Debe = DebeTemporal;
                    Haber = HaberTemporal;
                }

            } while (salir == false);

            Console.WriteLine($"Se ha creado el asiento Nº{Numero} con exito!");
            Console.ReadKey();
            Console.Clear();
        }

        // Constructor para importar asientos desde Diario.txt
        // Se ejecuta unicamente al inicio.
        public Asiento(string linea)
        {
            /*Dictionary<int, double> DebeTemporal = new Dictionary<int, double>();   // KEY: Nº de Cuenta || VALUE: Monto
            Dictionary<int, double> HaberTemporal = new Dictionary<int, double>();  // KEY: Nº de Cuenta || VALUE: Monto*/   
            var datos = linea.Split('|');
            datos[0].TrimEnd();
            datos[2].TrimEnd();
            datos[3].TrimStart();
            datos[4].TrimStart();

            // Lee la linea y se fija si hay numero de asiento (Si no lo hay es porque la linea es una continuacion de un asiento).
            if (datos[0] != "          ")    // Solucion guarra para que no crashee al leer texto vacio.
            {
                Numero = int.Parse(datos[0]);
                Fecha = DateTime.Parse(datos[1]);
                Debe.Add(int.Parse(datos[2]), double.Parse(datos[3]));
                //int codigoCuenta = int.Parse(datos[2]);
                //double montoDebe = double.Parse(datos[3]);
                //var montoHaber = double.Parse(datos[4]);
            }
            else if (datos[0] == "          ")    // Curso para continuacion de un asiento
            {
                if(datos[3] != "          ")      // Verifica si hay un monto en la columna DEBE
                {
                    Debe.Add(int.Parse(datos[2]), double.Parse(datos[3]));
                }
                else
                {
                    Haber.Add(int.Parse(datos[2]), double.Parse(datos[4]));
                }
            }
        }
        
        public string Serializar()
        {
            // return $"{}";
            string retorno = "";
            string padding = "";    // Solucion guarra para lograr el padding.
            int contador = 0;

            foreach(var item in Debe)
            {
                if(contador == 0)
                {
                    // Formato de salida:
                    //                  Numero                   |              Fecha            |          CodigoCuenta            |           Debe                    | Haber
                    retorno += $"{Numero.ToString().PadRight(10)}|{Fecha.ToString().PadRight(17)}|{item.Key.ToString().PadRight(12)}|{item.Value.ToString().PadLeft(10)}|";
                }
                else
                {
                    // Formato de salida:
                    //                  Numero           |              Fecha              |          CodigoCuenta            |           Debe                    | Haber
                    retorno += $"\n{padding.PadRight(10)}|{padding.ToString().PadRight(17)}|{item.Key.ToString().PadRight(12)}|{item.Value.ToString().PadLeft(10)}|";
                }

                contador++;
            }

            contador = 0;

            foreach(var item in Haber)
            {
                //                  Numero         |              Fecha              |          CodigoCuenta            |           Debe      |             Haber
                retorno += $"\n{padding.PadRight(10)}|{padding.ToString().PadRight(17)}|{item.Key.ToString().PadRight(12)}|{padding.PadLeft(10)}|{item.Value.ToString().PadLeft(10)}";
                contador++;

                // Inserta un newline al imprimir el ultimo haber para separarlo del proximo asiento.
                if(contador == Haber.Count())
                {
                    retorno += "\n";
                }
            }

            return retorno;
        }

    }
}
