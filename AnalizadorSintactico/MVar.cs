using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace AnalizadorSintactico
{
    public class MVar
    {
        public string nom;
        public List<varLocalMethod> listVar;

        public MVar(string n)
        {
            nom = n;
            listVar = new List<varLocalMethod>();

        }


    }
}
