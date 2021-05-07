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
        static readonly Dictionary<int, Asiento> Diario = new Dictionary<int, Asiento>();
        static string nombreDiario = "Diario.txt";
        static string ubicacionDiario;                                                                  // En caso de que la ubicacion predeterminada no se utilice, la nueva se almacenara en esta variable.

        static readonly Dictionary<int, Cuenta> PlanDeCuentas = new Dictionary<int, Cuenta>();
        static string nombrePlanDeCuentas = "Plan de cuentas.txt";
        static string ubicacionPlanDeCuentas;                                                           // En caso de que la ubicacion predeterminada no se utilice, la nueva se almacenara en esta variable.

        
        // -----------------------------------METODOS-----------------------------------  //


        // Agrega una cuenta a Plan verificando que el codigo para identificarla este disponible.
        public static void AgregarCuenta()
        {
            int codigo = Validadores.Codigo("Ingrese el codigo de la cuenta nueva");
            string nombre = Validadores.Texto("Ingrese el nombre de la cuenta nueva");
            string tipo = Validadores.TipoCuenta("Es (A)ctivo o (P)asivo?");
            Cuenta nueva = new Cuenta(codigo, nombre, tipo);

            if(PlanDeCuentas.ContainsKey(codigo))
            {
                Console.WriteLine($"Error: El codigo '{codigo}' ya esta siendo utilizado por otra cuenta\n");
                Console.ReadKey();
            }
            else
            {
                PlanDeCuentas.Add(nueva.Codigo, nueva);
                Console.WriteLine($"Se ha agregado la cuenta '{nueva.Nombre}' con el codigo {nueva.Codigo}\n");
                Console.ReadKey();
                GrabarPlan();
            }

        }

        // Verifica que la cuenta exista en Plan y la elimina.
        public static void QuitarCuenta()
        {
            int codigo = Validadores.Codigo("Ingrese el codigo de la cuenta a eliminar");    
            
            if (!PlanDeCuentas.TryGetValue(codigo, out var cuentaEliminada))
            {
                Console.WriteLine($"No existe una cuenta con el codigo '{codigo}'");
                Console.ReadKey();
            }
            else
            {
                PlanDeCuentas.Remove(codigo);
                Console.WriteLine($"Se ha eliminado la cuenta '{cuentaEliminada.Nombre}' con el codigo {cuentaEliminada.Codigo}");
                Console.ReadKey();
                GrabarPlan();
            }
        }

        // Print de consola del diccionario Plan.
        public static void ImprimirPlanDeCuentas()
        {
            Console.WriteLine("\tPlan de Cuentas Actual:\n");

            if (PlanDeCuentas.Count == 0)
            {
                Console.WriteLine("No se han ingresado cuentas...\n");
            }
            else
            {
                foreach (var cuenta in PlanDeCuentas)
                {
                    Console.WriteLine($"{cuenta.Key} | {cuenta.Value.Nombre} | {cuenta.Value.Tipo} ");
                }
            }

            Console.WriteLine("----Presione una tecla para continuar----\n");
            Console.ReadKey();
        }

        // Busca 'Plan de cuentas.txt' en la ubicación predeterminada.
        // Si no existe, pregunta si se desea importar uno existente o crear uno nuevo.
        // Solo se utiliza una vez al iniciar el programa.
        public static void IniciarPlanDeCuentas()
        {
            Console.WriteLine("Buscando 'Plan de cuentas.txt'...");
            if (File.Exists(nombrePlanDeCuentas))
            {
                ubicacionPlanDeCuentas = nombrePlanDeCuentas;

                using (var reader = new StreamReader(nombrePlanDeCuentas))     // Importa el txt (si este existe en la ubicacion por defecto)
                {
                    reader.ReadLine();                                            // Solución muy sucia para ignorar la primera linea del txt.

                    while (!reader.EndOfStream)
                    {
                        var linea = reader.ReadLine();
                        var cuenta = new Cuenta(linea);
                        PlanDeCuentas.Add(cuenta.Codigo, cuenta);
                    }
                }

                Console.WriteLine("Plan de cuentas encontrado!");
            }
            else
            {
                bool ok = false;
                string opcion;

                do
                {
                    Console.WriteLine("No se ha encontrado un Plan de Cuentas. Desea (I)mportar uno existente o (C)rear uno nuevo?");                    
                    opcion = Console.ReadLine().ToUpper();

                    // Verifica que 'Plan de cuentas.txt' exista y lo importa.
                    if (opcion == "I")
                    {
                        Console.WriteLine($"Ingrese la ubicación del archivo 'Plan de cuentas.txt' (Ej: 'C:/Plan de cuentas.txt' o 'C:/Users/{Environment.UserName}/documents/Plan de cuentas.txt'"); 
                        ubicacionPlanDeCuentas = Console.ReadLine();

                        if (!File.Exists(ubicacionPlanDeCuentas))
                        {
                            Console.WriteLine($"No se ha encontrado el archivo 'Plan de cuentas.txt' en {ubicacionPlanDeCuentas}");
                            Console.ReadKey();
                        }
                        else
                        {
                            // Importa el txt si lo encuentra
                            using (var reader = new StreamReader(ubicacionPlanDeCuentas))
                            {
                                reader.ReadLine(); // Solución muy sucia y rapida para ignorar la primera linea del txt (Codigo|Nombre|Tipo)

                                while (!reader.EndOfStream)
                                {
                                    var linea = reader.ReadLine();
                                    var cuenta = new Cuenta(linea);
                                    PlanDeCuentas.Add(cuenta.Codigo, cuenta);
                                }
                            }

                            
                            ok = true;
                        }
                    }else if(opcion == "C")
                    {
                        // Crea 'Plan de cuentas.txt' en la ubicacion 'C:\'
                        // Agrega una linea de referencia.
                        Console.WriteLine($"Se ha creado el archivo 'Plan de cuentas' en la ubicación '.../bin/Debug' de este proyecto\n");

                        using (StreamWriter writer = File.CreateText(nombrePlanDeCuentas))
                        {
                            writer.Write("Codigo|Nombre|Tipo");
                        }

                        ok = true;
                    }
                    else
                    {
                        Console.WriteLine($"'{opcion}' no es una opción válida.");
                        Console.ReadKey();
                    }
                } while (ok == false);
            }
        }

        // Guarda los cambios realizados al plan de cuentas en el archivo Plan de cuentas.txt
        private static void GrabarPlan()
        {
            using (var writer = new StreamWriter(ubicacionPlanDeCuentas, append: false))
            {
                writer.WriteLine("Codigo|Nombre|Tipo");

                foreach (var cuentas in PlanDeCuentas.Values)
                {
                    var linea = cuentas.Serializar();
                    writer.WriteLine(linea);
                }
            }
        }
       
        public static void IniciarDiario()
        {
            Console.WriteLine($"Buscando 'Diario.txt'...");
            if (File.Exists(nombreDiario))
            {
                ubicacionDiario = nombreDiario;

                using (var reader = new StreamReader(nombreDiario))     // Importa el txt (si este existe en la ubicacion por defecto)
                {
                    reader.ReadLine();                                            // Solución muy sucia para ignorar la primera linea del txt.

                    while (!reader.EndOfStream)
                    {
                        var linea = reader.ReadLine();
                        var asiento = new Asiento(linea);
                        Diario.Add(asiento.Numero, asiento);
                    }
                }

                Console.WriteLine("Libro Diario encontrado!");
            }
            else
            {
                bool ok = false;
                string opcion;

                do
                {
                    Console.WriteLine("No se ha encontrado un Libro Diario. Desea (I)mportar uno existente o (C)rear uno nuevo?");
                    opcion = Console.ReadLine().ToUpper();

                    // Verifica que 'Diario.txt' exista y lo importa.
                    if (opcion == "I")
                    {
                        Console.WriteLine($"Ingrese la ubicación del archivo 'Diario.txt' (Ej: 'C:/Users/{Environment.UserName}/documents/Diario.txt'");
                        ubicacionDiario = Console.ReadLine();

                        if (!File.Exists(ubicacionDiario))
                        {
                            Console.WriteLine($"No se ha encontrado el archivo 'Diario.txt' en {ubicacionDiario}");
                            Console.ReadKey();
                        }
                        else
                        {
                            // Importa el txt si lo encuentra
                            using (var reader = new StreamReader(ubicacionDiario))
                            {
                                reader.ReadLine(); // Solución muy sucia y rapida para ignorar la primera linea del txt (NroAsiento|Fecha|CodigoCuenta|Debe|Haber)

                                while (!reader.EndOfStream)
                                {
                                    var linea = reader.ReadLine();
                                    var asiento = new Asiento(linea);
                                    Diario.Add(asiento.Numero, asiento);
                                }
                            }


                            ok = true;
                        }
                    }
                    else if (opcion == "C")
                    {
                        // Crea 'Diario.txt' dentro del proyecto.
                        // Agrega una linea de referencia.
                        Console.WriteLine($"Se ha creado el archivo 'Diario.txt' en la ubicación '.../bin/Debug' de este proyecto\n");

                        using (StreamWriter writer = File.CreateText(nombreDiario))
                        {
                            writer.Write("NroAsiento|Fecha|CodigoCuenta|Debe|Haber");
                        }

                        ok = true;
                    }
                    else
                    {
                        Console.WriteLine($"'{opcion}' no es una opción válida.");
                        Console.ReadKey();
                    }
                } while (ok == false);
            }
        }

        public static void ImprimirDiario()
        {
            Console.WriteLine("\tLibro Diario Actual:\n");

            if (Diario.Count == 0)
            {
                Console.WriteLine("No se han ingresado asientos...\n");
            }
            else
            {
                foreach (var asiento in Diario)
                {
                    Console.WriteLine($"{asiento.Key} | {asiento.Value.Fecha} | {asiento.Value.CodigoCuenta} | {asiento.Value.Debe} | {asiento.Value.Haber} ");
                }
            }

            Console.WriteLine("----Presione una tecla para continuar----\n");
            Console.ReadKey();
        }

        private static void GrabarDiario()
        {
            using (var writer = new StreamWriter(ubicacionDiario, append: false))
            {
                writer.WriteLine("NroAsiento | Fecha | CodigoCuenta | Debe | Haber");

                foreach (var cuentas in Diario.Values)
                {
                    var linea = cuentas.Serializar();
                    writer.WriteLine(linea);
                }
            }
        }       
    }
}
