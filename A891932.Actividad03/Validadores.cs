using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A891932.Actividad03
{
    class Validadores
    {
        public static double NumeroPositivo(string textoAImprimir)
        {
            double numero;
            bool completo = false;

            do
            {
                Console.WriteLine(textoAImprimir);
                
                if (!double.TryParse(Console.ReadLine(), out numero))
                {
                    Console.WriteLine("Debe ingresar un número.\n");
                    Console.ReadKey();
                }
                else
                {
                    if(numero < 0)
                    {
                        Console.WriteLine("El número ingresado debe ser positivo.\n");
                        Console.ReadKey();
                        break;
                    }
                    else
                    {
                        completo = true;
                    }                    
                }

            } while (completo == false);

            return numero;
        }

        internal static string TipoCuenta(string textoAImprimir)
        {
            string ingreso;
            bool ok = false;

            do
            {
                Console.WriteLine(textoAImprimir);
                ingreso = Console.ReadLine().ToUpper();

                if(ingreso == "A")
                {
                    Console.WriteLine("ACTIVO");
                    ok = true;
                } else if(ingreso == "P")
                {
                    Console.WriteLine("PASIVO");
                    ok = true;
                } else
                {
                    Console.WriteLine($"La opcion '{ingreso}' no es valida\n");
                    Console.ReadKey();
                }
            } while (ok == false);

            return ingreso;
        }

        public static int Codigo(string textoAImprimir)
        {
            int numero;
            bool completo = false;

            do
            {
                Console.WriteLine(textoAImprimir);

                if (!int.TryParse(Console.ReadLine(), out numero))
                {
                    Console.WriteLine("Debe ingresar un número.\n");
                    Console.ReadKey();
                }
                else
                {
                    if (numero < 0)
                    {
                        Console.WriteLine("El número ingresado debe ser positivo.\n");
                        Console.ReadKey();                        
                    } else if(numero > 999)
                    {
                        Console.WriteLine("El número ingresado no debe tener más de 3 digitos\n");
                        Console.ReadKey();                        
                    }
                    else
                    {
                        completo = true;
                    }
                }

            } while (completo == false);

            return numero;
        }

        public static string Texto(string textoAImprimir)
        {
            string ingreso;
            bool ok = false;

            do
            {
                Console.WriteLine(textoAImprimir);
                ingreso = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(ingreso))
                {
                    Console.WriteLine("Este campo no puede estar vacio.\n");
                    Console.ReadKey();
                }
                else if (ingreso.Length > 41)
                {
                    Console.WriteLine("Este campo no puede tener una longitud mayor a 40 caracteres\n");
                    Console.ReadKey();
                }
                else
                {
                    ok = true;
                }

            } while (ok == false);

            return ingreso;
        }
    }
}
