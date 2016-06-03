using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace AnalizadorSintactico
{
    public class methodParams
    {
        public string metodo;
        public List<ParameterBuilder> listParams;

        public methodParams(string meto)
        {
            metodo = meto;
            listParams = new List<ParameterBuilder>();

        }


    }
}
