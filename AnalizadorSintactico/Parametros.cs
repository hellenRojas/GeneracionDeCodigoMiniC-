using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalizadorSintactico
{
    class Parametros
    {

        public string idVar;
        public string tipo;

        public Parametros(string id,string tip)
        {
            idVar = id;
            tipo = tip;

        }
    }
}
