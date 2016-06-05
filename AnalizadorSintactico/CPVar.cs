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
        public ConstructorBuilder cons;
        public List<varClass> listVar;


        public CPVar(string clas,ConstructorBuilder c)
        {
            clase = clas;
            cons = c;
            listVar = new List<varClass>();

        }


    }
}
