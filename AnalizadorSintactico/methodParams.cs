﻿using System;
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
        public string tipo;
        public List<varParams> listParams;


        public methodParams(string meto,string t)
        {
            metodo = meto;
            tipo = t;
            listParams = new List<varParams>();

        }


    }
}
