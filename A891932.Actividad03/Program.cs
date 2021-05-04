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
            const string menuPrincipal = "I - Ingresar nuevo asiento\n" +
                "V - Ver asientos ingresados\n" +
                "P - Ver plan de cuentas\n" +
                "S - Guardar y salir";
            string opcionElegida = "";

            Console.WriteLine($"\tBienvenido {Environment.UserName} a su gestor de libro diario!\n");
            LibroDiario.Iniciar();

            do
            {
                Console.WriteLine(menuPrincipal);
                opcionElegida = Console.ReadLine().ToUpper();

                switch (opcionElegida)
                {
                    case "I":
                        LibroDiario.IngresarAsiento();
                        break;
                    case "V":
                        LibroDiario.VerAsientos();
                        break;
                    case "P":
                        LibroDiario.VerPlanDeCuentas();
                        break;
                    default:
                        Console.WriteLine($"'{opcionElegida}' no es una opción válida.\n");
                        break;
                }

            } while (opcionElegida != "S");

            LibroDiario.Guardar();

            Console.WriteLine("Adios!");
            Console.ReadKey();
        }
    }
}
