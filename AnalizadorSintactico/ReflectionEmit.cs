using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Reflection.Emit;

namespace AnalizadorSintactico
{
    public class ReflectionEmit
    {
        public Assembly CreateAssembly()
        {
            AssemblyName assemblyName = new AssemblyName();
            assemblyName.Name = "Math";

            AssemblyBuilder CreatedAssembly =
                 AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName,
                 AssemblyBuilderAccess.RunAndSave);

            ModuleBuilder AssemblyModule =
               CreatedAssembly.DefineDynamicModule("MathModule", "Math.dll");

            TypeBuilder MathType =
                   AssemblyModule.DefineType("DoMath", TypeAttributes.Public |
                   TypeAttributes.Class);



            //******************************************   Suma  ******************************************************

            System.Type[] ParamTypes = new Type[] { typeof(int), typeof(int), typeof(int) };

            MethodBuilder SumMethod = MathType.DefineMethod("Sum",
                   MethodAttributes.Public, typeof(int), ParamTypes);

            ParameterBuilder Param1 =
                   SumMethod.DefineParameter(1, ParameterAttributes.In, "num1");

            ParameterBuilder Param2 =
                   SumMethod.DefineParameter(2, ParameterAttributes.In, "num2");


            ILGenerator ilGenerator = SumMethod.GetILGenerator();
          

  

            //Operandos insertados en la pila en orden inverso
            ilGenerator.Emit(OpCodes.Ldarg, 2);
            ilGenerator.Emit(OpCodes.Ldarg, 1);

            ilGenerator.Emit(OpCodes.Add);
            ilGenerator.Emit(OpCodes.Ret);



            //******************************************   Resta  ******************************************************

            System.Type[] ParamTypesRes = new Type[] { typeof(int), typeof(int), typeof(int) };

            MethodBuilder ResMethod = MathType.DefineMethod("Res",
                   MethodAttributes.Public, typeof(int), ParamTypesRes);

            ParameterBuilder Param1Res =
                   ResMethod.DefineParameter(1, ParameterAttributes.In, "num1");

            ParameterBuilder Param2Res =
                   ResMethod.DefineParameter(2, ParameterAttributes.In, "num2");


            ILGenerator ilGeneratorRes = ResMethod.GetILGenerator();




            //Operandos insertados en la pila en orden inverso
            ilGeneratorRes.Emit(OpCodes.Ldarg, 1);
            ilGeneratorRes.Emit(OpCodes.Ldarg, 2);

            ilGeneratorRes.Emit(OpCodes.Sub);
            ilGeneratorRes.Emit(OpCodes.Ret);
        




            //******************************************   Multiplicación  ******************************************************

            System.Type[] ParamTypesMul = new Type[] { typeof(int), typeof(int), typeof(int) };

            MethodBuilder MulMethod = MathType.DefineMethod("Mul",
                   MethodAttributes.Public, typeof(int), ParamTypesMul);

            ParameterBuilder Param1Mul =
                   MulMethod.DefineParameter(1, ParameterAttributes.In, "num1");

            ParameterBuilder Param2Mul =
                   MulMethod.DefineParameter(2, ParameterAttributes.In, "num2");


            ILGenerator ilGeneratorMul = MulMethod.GetILGenerator();

      
            //Operandos insertados en la pila en orden inverso
            ilGeneratorMul.Emit(OpCodes.Ldarg, 2);
            ilGeneratorMul.Emit(OpCodes.Ldarg, 1);

         
            ilGeneratorMul.Emit(OpCodes.Mul);
            ilGeneratorMul.Emit(OpCodes.Ret);
         



            //******************************************   Division modular  ******************************************************

            System.Type[] ParamTypesDivMod = new Type[] { typeof(int), typeof(int), typeof(int) };

            MethodBuilder DivModMethod = MathType.DefineMethod("DivMod",
                   MethodAttributes.Public, typeof(int), ParamTypesDivMod);

            ParameterBuilder Param1DivMod =
                   DivModMethod.DefineParameter(1, ParameterAttributes.In, "num1");

            ParameterBuilder Param2DivMod =
                   DivModMethod.DefineParameter(2, ParameterAttributes.In, "num2");


            ILGenerator ilGeneratorDivMod = DivModMethod.GetILGenerator();


            //Operandos insertados en la pila en orden inverso
            ilGeneratorDivMod.Emit(OpCodes.Ldarg, 1);
            ilGeneratorDivMod.Emit(OpCodes.Ldarg, 2);


            ilGeneratorDivMod.Emit(OpCodes.Rem);
            ilGeneratorDivMod.Emit(OpCodes.Ret);


            //******************************************   Division normal  ******************************************************

            System.Type[] ParamTypesDiv = new Type[] { typeof(int), typeof(int), typeof(int) };

            MethodBuilder DivMethod = MathType.DefineMethod("Div",
                   MethodAttributes.Public, typeof(int), ParamTypesDiv);

            ParameterBuilder Param1Div =
                   DivMethod.DefineParameter(1, ParameterAttributes.In, "num1");

            ParameterBuilder Param2Div =
                   DivMethod.DefineParameter(2, ParameterAttributes.In, "num2");


            ILGenerator ilGeneratorDiv = DivMethod.GetILGenerator();


            //Operandos insertados en la pila en orden inverso
            ilGeneratorDiv.Emit(OpCodes.Ldarg, 1);
            ilGeneratorDiv.Emit(OpCodes.Ldarg, 2);


            ilGeneratorDiv.Emit(OpCodes.Div);
            ilGeneratorDiv.Emit(OpCodes.Ret);


            /*
            //******************************************  Cond  ******************************************************

            System.Type[] ParamTypesCond = new Type[] { typeof(int), typeof(int), typeof(int) };

            MethodBuilder CondMethod = MathType.DefineMethod("Cond",
                   MethodAttributes.Public, typeof(int), ParamTypesCond);

            ParameterBuilder Param1Cond =
                   CondMethod.DefineParameter(1, ParameterAttributes.In, "num1");

            ParameterBuilder Param2Cond =
                   CondMethod.DefineParameter(2, ParameterAttributes.In, "num2");


            ILGenerator ilGeneratorCond = CondMethod.GetILGenerator();


            //Operandos insertados en la pila en orden inverso
            ilGeneratorCond.Emit(OpCodes.Ldarg, 1);
            ilGeneratorCond.Emit(OpCodes.Ldarg, 2);


            ilGeneratorCond.Emit(OpCodes.Div);
            ilGeneratorCond.Emit(OpCodes.Ret);


            */


            //******************************************  Mayor  ******************************************************
            
            System.Type[] ParamTypesMayor = new Type[] { typeof(int), typeof(int), typeof(int) };

            MethodBuilder MayorMethod = MathType.DefineMethod("Mayor",
                   MethodAttributes.Public, typeof(int), ParamTypesMayor);

            ParameterBuilder Param1Mayor =
                   MayorMethod.DefineParameter(1, ParameterAttributes.In, "num1");

            ParameterBuilder Param2Mayor =
                   MayorMethod.DefineParameter(2, ParameterAttributes.In, "num2");


            ILGenerator ilGeneratorMayor = MayorMethod.GetILGenerator();


            //Operandos insertados en la pila en orden inverso
            ilGeneratorMayor.Emit(OpCodes.Ldarg, 2);
            ilGeneratorMayor.Emit(OpCodes.Ldarg, 1);


            ilGeneratorMayor.Emit(OpCodes.Cgt);// 1 es true
            ilGeneratorMayor.Emit(OpCodes.Ret);

            //******************************************  Menor  ******************************************************

            System.Type[] ParamTypesMenor = new Type[] { typeof(int), typeof(int), typeof(int) };

            MethodBuilder MenorMethod = MathType.DefineMethod("Menor",
                   MethodAttributes.Public, typeof(int), ParamTypesMenor);

            ParameterBuilder Param1Menor =
                   MenorMethod.DefineParameter(1, ParameterAttributes.In, "num1");

            ParameterBuilder Param2Menor =
                   MenorMethod.DefineParameter(2, ParameterAttributes.In, "num2");


            ILGenerator ilGeneratorMenor = MenorMethod.GetILGenerator();


            //Operandos insertados en la pila en orden inverso
            ilGeneratorMenor.Emit(OpCodes.Ldarg, 1);
            ilGeneratorMenor.Emit(OpCodes.Ldarg, 2);


            ilGeneratorMenor.Emit(OpCodes.Clt);// 1 es true
            ilGeneratorMenor.Emit(OpCodes.Ret);


            //******************************************  Mayor o Igual  ******************************************************

            System.Type[] ParamTypesMayorIg = new Type[] { typeof(int), typeof(int), typeof(int) };

            MethodBuilder MayorIgMethod = MathType.DefineMethod("MayorIg",
                   MethodAttributes.Public, typeof(int), ParamTypesMayorIg);

            ParameterBuilder Param1MayorIg =
                   MayorIgMethod.DefineParameter(1, ParameterAttributes.In, "num1");

            ParameterBuilder Param2MayorIg =
                   MayorIgMethod.DefineParameter(2, ParameterAttributes.In, "num2");


            ILGenerator ilGeneratorMayorIg = MayorIgMethod.GetILGenerator();

            Label j = ilGeneratorMayorIg.DefineLabel();


            ilGeneratorMayorIg.Emit(OpCodes.Ldarg, 1);
            ilGeneratorMayorIg.Emit(OpCodes.Ldarg, 2);
            ilGeneratorMayorIg.Emit(OpCodes.Cgt);
            ilGeneratorMayorIg.Emit(OpCodes.Brtrue, j); 


            ilGeneratorMayorIg.Emit(OpCodes.Ldarg, 1);
            ilGeneratorMayorIg.Emit(OpCodes.Ldarg, 2);
            ilGeneratorMayorIg.Emit(OpCodes.Ceq);
            ilGeneratorMayorIg.Emit(OpCodes.Ret);

            ilGeneratorMayorIg.MarkLabel(j);
            ilGeneratorMayorIg.Emit(OpCodes.Ret);


           
            MathType.CreateType();

            return CreatedAssembly;

        }
    }
}
 