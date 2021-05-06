using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A891932.Actividad03
{
    class LibroDiario
    {
        static readonly Dictionary<int, Cuenta> Diario = new Dictionary<int, Cuenta>();
        static string nombreDiario = "Diario.txt";

        static readonly Dictionary<int, Cuenta> Plan = new Dictionary<int, Cuenta>();
        static string nombrePlan = "Plan de Cuentas.txt";

        public static void AgregarCuenta()
        {
            int codigo = Validadores.Codigo("Ingrese el codigo de la cuenta nueva");
            string nombre = Validadores.Texto("Ingrese el nombre de la cuenta nueva");
            string tipo = Validadores.TipoCuenta("Es (A)ctivo o (P)asivo?");
            Cuenta nueva = new Cuenta(codigo, nombre, tipo);
            if(Plan.ContainsKey(codigo))
            {
                Console.WriteLine($"Error: El codigo '{codigo}' ya esta siendo utilizado por otra cuenta\n");
                Console.ReadKey();
            }
            else
            {
                Plan.Add(nueva.Codigo, nueva);
                Console.WriteLine($"Se ha agregado la cuenta '{nueva.Nombre}' con el codigo {nueva.Codigo}\n");
                Console.ReadKey();
                GrabarPlan();
            }

        }

        public static void QuitarCuenta()
        {
            int codigo = Validadores.Codigo("Ingrese el codigo de la cuenta a eliminar");            
            if (!Plan.TryGetValue(codigo, out var cuentaEliminada))
            {
                Console.WriteLine($"No existe una cuenta con el codigo '{codigo}'");
                Console.ReadKey();
            }
            else
            {
                Plan.Remove(codigo);
                Console.WriteLine($"Se ha eliminado la cuenta '{cuentaEliminada.Nombre}' con el codigo {cuentaEliminada.Codigo}");
                Console.ReadKey();
                GrabarPlan();
            }
        }

        public static void ImprimirPlanDeCuentas()
        {
            Console.WriteLine("\tPlan de Cuentas Actual:\n");

            if (Plan.Count == 0)
            {
                Console.WriteLine("No se han ingresado cuentas...\n");
            }
            else
            {
                foreach (var cuenta in Plan)
                {
                    Console.WriteLine($"{cuenta.Key} | {cuenta.Value.Nombre} | {cuenta.Value.Tipo} ");
                }
            }

            Console.WriteLine("----Presione una tecla para continuar----");
            Console.ReadKey();
        }

        public static void ImportarPlanDeCuentas()
        {
            if (File.Exists(nombrePlan))
            {
                using (var reader = new StreamReader(nombrePlan))     // Importa el txt (si este existe en la ubicacion por defecto)
                {
                    while (!reader.EndOfStream)
                    {
                        var linea = reader.ReadLine();
                        var cuenta = new Cuenta(linea);
                        Plan.Add(cuenta.Codigo, cuenta);
                    }
                }
            }
        }

        private static void GrabarPlan()
        {
            using (var writer = new StreamWriter(nombrePlan, append: false))
            {
                foreach (var cuentas in Plan.Values)
                {
                    var linea = cuentas.Serializar();
                    writer.WriteLine(linea);
                }
            }
        }
       
        /* private static void GrabarDiario()
        {
            using (var writer = new StreamWriter(nombreDiario, append: false))
            {
                foreach (var cuentas in Diario.Values)
                {
                    var linea = cuentas.Serializar();
                    writer.WriteLine(linea);
                }
            }
        }
       */
    }
}
