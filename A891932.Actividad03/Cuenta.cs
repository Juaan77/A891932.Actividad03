using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A891932.Actividad03
{
    public class Cuenta
    {
        public int Codigo { get; }
        public string Nombre { get; }
        public string Tipo { get; }


        public Cuenta(int codigo, string nombre, string tipo)
        {
            Codigo = codigo;
            Nombre = nombre;
            Tipo = tipo;
        }

        public Cuenta(string linea)
        {
            var datos = linea.Split('|');
            Codigo = int.Parse(datos[0]);
            Nombre = datos[1];
            Tipo = datos[2];
        }

        public string Serializar()
        {
            return $"{Codigo}|{Nombre}|{Tipo}";
        }
    }
}