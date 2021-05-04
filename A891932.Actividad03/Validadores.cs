using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A891932.Actividad03
{
    class Validadores
    {
        private static float Numeros(string textoAImprimir)
        {
            float numero;
            bool completo = false;

            do
            {
                Console.WriteLine(textoAImprimir);
                
                if (!float.TryParse(Console.ReadLine(), out numero))
                {
                    Console.WriteLine("Debe ingresar un número.");
                    Console.ReadKey();
                }
                else
                {
                    if(numero < 0)
                    {
                        Console.WriteLine("El número ingresado debe ser positivo.");
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
    }
}
