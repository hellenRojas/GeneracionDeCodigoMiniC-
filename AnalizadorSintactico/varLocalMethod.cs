using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace AnalizadorSintactico
{
    //Clase q guarda las variables locales de los métodos(Individualmente)
    public class varLocalMethod
    {
       public string nombreVar;
       public LocalBuilder var;
       public string tipo;

       public varLocalMethod(string n,LocalBuilder v,string t)
        {
            nombreVar = n;
            var = v;
            tipo = t;

        }
    }
}
