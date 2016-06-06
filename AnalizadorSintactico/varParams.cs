using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace AnalizadorSintactico
{
    //Clase q guarda las variables locales de los métodos(Individualmente)
    public class varParams
    {
        public ParameterBuilder var;
        public string tipo;

        public varParams(ParameterBuilder v, string t)
        {
            var = v;
            tipo = t;

        }
    }
}
