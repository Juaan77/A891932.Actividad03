using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A891932.Actividad03
{
    class Program
    {
        static void Main(string[] args)
        {
            const string menuPrincipal = "\t-MENU PRINCIPAL-\n\n" +
                "I - Ingresar nuevo asiento\n" +
                "V - Ver Libro Diario\n" +
                "P - Ver plan de cuentas\n" +
                "M - Modificar plan de cuentas\n" +
                "S - Guardar y salir\n";
            const string menuModificarPlan = "\t-MODIFICAR PLAN DE CUENTAS-\nA - Agregar nueva cuenta\n" +
                "E - Eliminar cuenta\n" +
                "V - Volver al menu principal\n";
            string opcionElegida;


            Console.WriteLine($"\tBienvenido {Environment.UserName} a su gestor de libro diario!\n");            
            LibroDiario.IniciarPlanDeCuentas();
            LibroDiario.IniciarDiario();
            Console.Clear();

            do
            {
                Console.WriteLine(menuPrincipal);
                opcionElegida = Console.ReadLine().ToUpper();

                switch (opcionElegida)
                {
                    case "I":                        
                        LibroDiario.AgregarAsiento();
                        break;
                    case "V":
                        Console.WriteLine("\tLibro Diario Actual:\n");
                        Console.WriteLine("NroAsiento|      Fecha      |CodigoCuenta|   Debe   |   Haber  ");
                        LibroDiario.ImprimirDiario();
                        Console.WriteLine("----Presione una tecla para continuar----\n");
                        Console.ReadKey();
                        break;
                    case "P":
                        Console.WriteLine("\tPlan de Cuentas Actual:\n");
                        LibroDiario.ImprimirPlanDeCuentas();
                        Console.WriteLine("----Presione una tecla para continuar----\n");
                        Console.ReadKey();
                        break;
                    case "M":
                        do
                        {
                            Console.WriteLine(menuModificarPlan);
                            opcionElegida = Console.ReadLine().ToUpper();

                            if (opcionElegida == "A")
                            {
                                LibroDiario.AgregarCuenta();
                            }
                            else if (opcionElegida == "E")
                            {
                                LibroDiario.QuitarCuenta();
                            }
                            else if (opcionElegida != "A" && opcionElegida != "E" && opcionElegida != "V")
                            {
                                Console.WriteLine($"'{opcionElegida}' no es una opcion valida\n");
                                Console.ReadKey();
                            }
                        } while (opcionElegida != "V");
                        break;                    
                }

                if (opcionElegida != "I" && opcionElegida != "V" && opcionElegida != "P" && opcionElegida != "M" && opcionElegida != "S")
                {
                    Console.WriteLine($"'{opcionElegida}' no es una opcion valida\n");
                    Console.ReadKey();
                }

            } while (opcionElegida != "S");

            Console.WriteLine("Saliendo al escritorio...");
            Console.ReadKey();
        }
    }
}
