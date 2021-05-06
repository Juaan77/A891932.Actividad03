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
        static string nombreDiario = "C:\\Diario.txt";

        static readonly Dictionary<int, Cuenta> Plan = new Dictionary<int, Cuenta>();
        static string nombrePlan = $"C:/Users/{Environment.UserName}/documents/Plan de cuentas.txt";

        static string ubicacionPlanDeCuentas;

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

            Console.WriteLine("----Presione una tecla para continuar----\n");
            Console.ReadKey();
        }

        public static void PlanDeCuentas()
        {
            if (File.Exists(nombrePlan))
            {
                ubicacionPlanDeCuentas = nombrePlan;

                using (var reader = new StreamReader(ubicacionPlanDeCuentas))     // Importa el txt (si este existe en la ubicacion por defecto)
                {
                    reader.ReadLine(); // Solución muy sucia para ignorar la primera linea del txt.

                    while (!reader.EndOfStream)
                    {
                        var linea = reader.ReadLine();
                        var cuenta = new Cuenta(linea);
                        Plan.Add(cuenta.Codigo, cuenta);
                    }
                }
            }
            else
            {
                bool ok = false;
                string opcion;

                do
                {
                    Console.WriteLine("No se ha encontrado un Plan de Cuentas existente. Desea (I)mportar uno existente o (C)rear uno nuevo?\n");                    
                    opcion = Console.ReadLine().ToUpper();

                    // Verifica que 'Plan de cuentas.txt' exista y lo importa.
                    if (opcion == "I")
                    {
                        Console.WriteLine($"Ingrese la ubicación del archivo 'Plan de cuentas.txt' (Ej: 'C:/Plan de cuentas.txt' o 'C:/Users/{Environment.UserName}/documents/Plan de cuentas.txt'"); 
                        ubicacionPlanDeCuentas = Console.ReadLine();

                        if (!File.Exists(ubicacionPlanDeCuentas))
                        {
                            Console.WriteLine($"No se ha encontrado el archivo 'Plan de cuentas.txt' en {ubicacionPlanDeCuentas}\n");
                            Console.ReadKey();
                        }
                        else
                        {
                            using (var reader = new StreamReader(ubicacionPlanDeCuentas))
                            {
                                reader.ReadLine(); // Solución muy sucia para ignorar la primera linea del txt.

                                while (!reader.EndOfStream)
                                {
                                    var linea = reader.ReadLine();
                                    var cuenta = new Cuenta(linea);
                                    Plan.Add(cuenta.Codigo, cuenta);
                                }
                            }

                            
                            ok = true;
                        }
                    }else if(opcion == "C")     // Crea 'Plan de cuentas.txt' en la ubicacion 'C:\'
                    {
                        Console.WriteLine($"Se ha creado el archivo 'Plan de cuentas' en la ubicacion '{nombrePlan}'\n");
                        using (StreamWriter writer = File.CreateText(nombrePlan))
                        {
                            writer.Write("Codigo|Nombre|Tipo");
                        }

                        ok = true;
                    }
                    else
                    {
                        Console.WriteLine($"'{opcion} no es una opción válida.'\n");
                        Console.ReadKey();
                    }
                } while (ok == false);
            }
        }

        private static void GrabarPlan()
        {
            using (var writer = new StreamWriter(ubicacionPlanDeCuentas))
            {
                writer.WriteLine("Codigo|Nombre|Tipo");
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
