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
            const string menuPrincipal = "\t-MENU PRINCIPAL-\n" +
                "I - Ingresar nuevo asiento\n" +
                "V - Ver asientos ingresados\n" +
                "P - Ver plan de cuentas\n" +
                "M - Modificar plan de cuentas\n" +
                "S - Guardar y salir\n";
            const string menuModificarPlan = "\t-MODIFICAR PLAN DE CUENTAS-\nA - Agregar nueva cuenta\n" +
                "E - Eliminar cuenta\n" +
                "V - Volver al menu principal\n";
            string opcionElegida = "";

            Console.WriteLine($"\tBienvenido {Environment.UserName} a su gestor de libro diario!\n");

            do
            {
                Console.WriteLine(menuPrincipal);
                opcionElegida = Console.ReadLine().ToUpper();

                switch (opcionElegida)
                {
                    case "I":
                        
                        break;
                    case "V":
                        
                        break;
                    case "P":
                        LibroDiario.ImprimirPlanDeCuentas();
                        break;
                    case "M":
                        do
                        {
                            Console.WriteLine(menuModificarPlan);
                            opcionElegida = Console.ReadLine().ToUpper();

                            switch (opcionElegida)
                            {
                                case "A":
                                    LibroDiario.AgregarCuenta();
                                    break;
                                case "E":
                                    LibroDiario.QuitarCuenta();
                                    break;
                            }
                            
                            if (opcionElegida != "A" && opcionElegida != "E" && opcionElegida != "V")
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

            Console.WriteLine("Adios!");
            Console.ReadKey();
        }
    }
}
