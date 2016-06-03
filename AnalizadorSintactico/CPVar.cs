using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace AnalizadorSintactico
{
    public class CPVar
    {
        public string clase;
        public List<varClass> listVar;

        public CPVar(string clas)
        {
            clase = clas;
            listVar = new List<varClass>();

        }


    }
}
