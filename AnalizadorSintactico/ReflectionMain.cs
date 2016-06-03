using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Reflection;
using System.Reflection.Emit;

namespace AnalizadorSintactico
{


    class ReflectionMain
    {

        public static Type BuildDynAssembly()
        {

            Type pointType = null;

            AppDomain currentDom = Thread.GetDomain();

            Console.Write("Please enter a name for your new assembly: ");
            StringBuilder asmFileNameBldr = new StringBuilder();
            string asmFileName = "prueba.exe";

            AssemblyName myAsmName = new AssemblyName();
            myAsmName.Name = "MyDynamicAssembly";

            AssemblyBuilder myAsmBldr = currentDom.DefineDynamicAssembly(
                               myAsmName,
                               AssemblyBuilderAccess.RunAndSave);

            // We've created a dynamic assembly space - now, we need to create a module
            // within it to reflect the type Point into.

            ModuleBuilder myModuleBldr = myAsmBldr.DefineDynamicModule(asmFileName,
                                               asmFileName);

            TypeBuilder act = temp(myModuleBldr);


            TypeBuilder tb = act.DefineNestedType("Anidada", TypeAttributes.Abstract
                                                            | TypeAttributes.AnsiClass
                                                            | TypeAttributes.AutoClass
                                                            | TypeAttributes.Sealed
                                                            | TypeAttributes.NestedPrivate
                                                            | TypeAttributes.BeforeFieldInit, typeof(System.Object));
            FieldBuilder varClaseAnidada = tb.DefineField("x", typeof(int), FieldAttributes.Static | FieldAttributes.Public);
            act.DefineField("varAnidada", tb, FieldAttributes.Public);

            tb.CreateType();


            FieldBuilder xField = act.DefineField("x", typeof(int),
                                                         FieldAttributes.Private | FieldAttributes.Static);
            FieldBuilder yField = act.DefineField("y", typeof(char),
                                                         FieldAttributes.Private | FieldAttributes.Static);

            // Build the constructor.

            Type objType = Type.GetType("System.Object");
            ConstructorInfo objCtor = objType.GetConstructor(new Type[0]);

            ConstructorBuilder pointCtor = act.DefineConstructor(
                                       MethodAttributes.Public,
                                      CallingConventions.Standard,
                                      null);
            ILGenerator ctorIL = pointCtor.GetILGenerator();
            ctorIL.Emit(OpCodes.Ldarg_0);
            ctorIL.Emit(OpCodes.Call, objCtor);
            ctorIL.Emit(OpCodes.Ldarg_0);
            ctorIL.Emit(OpCodes.Ldc_I4_0);
            ctorIL.Emit(OpCodes.Stfld, xField);
            ctorIL.Emit(OpCodes.Ldarg_0);
            ctorIL.Emit(OpCodes.Ldc_I4_S, 97);
            ctorIL.Emit(OpCodes.Stfld, yField);
            ctorIL.Emit(OpCodes.Ret);


            // Build the DotProduct method.

            Console.WriteLine("Constructor built.");


            // Build the PointMain method.

            Console.WriteLine("DotProduct built.");

            MethodBuilder pointMainBldr = act.DefineMethod("Main",
                                        MethodAttributes.Public |
                                        MethodAttributes.Static,
                                        typeof(void),
                                        null);
            pointMainBldr.InitLocals = true;


            ILGenerator pmIL = pointMainBldr.GetILGenerator();

            MethodInfo writeMI1 = typeof(Console).GetMethod(
                         "WriteLine",
                         new Type[] { typeof(int) });

            MethodInfo writeMI2 = typeof(Console).GetMethod(
                        "WriteLine",
                        new Type[] { typeof(char) });


            /* MethodInfo convertInt32MI = typeof(Convert).GetMethod(
                                     "ToInt32",
                                         new Type[] { typeof(string) });

             Type[] wlParams = new Type[] { typeof(string), typeof(object[]) };
             MethodInfo writeLineMI = typeof(Console).GetMethod(
                                  "WriteLine",
                                  wlParams);*/


            //pmIL.Emit(OpCodes.Ldstr,"Hola Mundo!!!");
            // Call the overload of Console.WriteLine that prints a string.
            //pmIL.EmitCall(OpCodes.Call, writeMI, null);
            // The Hello method returns the value of the second argument;
            // to do this, load the onto the stack and return.
            //pmIL.Emit(OpCodes.Ldarg_1);
            pmIL.Emit(OpCodes.Ldsfld, xField);
            pmIL.Emit(OpCodes.Ldc_I4_3); // pmIL.Emit(OpCodes.Ldc_I4, 3);
            pmIL.Emit(OpCodes.Add);
            pmIL.Emit(OpCodes.Stsfld, xField);
            pmIL.Emit(OpCodes.Ldsfld, xField);
            pmIL.EmitCall(OpCodes.Call, writeMI1, null);
            pmIL.Emit(OpCodes.Ldsfld, yField);
            pmIL.EmitCall(OpCodes.Call, writeMI2, null);

            // prueba

            pmIL.Emit(OpCodes.Ldc_I4_S, 10);
            pmIL.Emit(OpCodes.Stsfld, varClaseAnidada);
            pmIL.Emit(OpCodes.Ldsfld, varClaseAnidada);
            pmIL.EmitCall(OpCodes.Call, writeMI1, null);

            //fin prueba


            MethodInfo readLineMI = typeof(Console).GetMethod(
                                    "ReadKey",
                                    new Type[0]);
            pmIL.EmitCall(OpCodes.Call, readLineMI, null);


            pmIL.Emit(OpCodes.Ret);

            Console.WriteLine("Main (entry point) built.");

            pointType = act.CreateType();

            Console.WriteLine("Type completed.");

            myAsmBldr.SetEntryPoint(pointMainBldr);

            myAsmBldr.Save(asmFileName);

            Console.WriteLine("Assembly saved as '{0}'.", asmFileName);
            Console.WriteLine("Type '{0}' at the prompt to run your new " +
                      "dynamically generated dot product calculator.",
                   asmFileName);

            // After execution, this program will have generated and written to disk,
            // in the directory you executed it from, a program named 
            // <name_you_entered_here>.exe. You can run it by typing
            // the name you gave it during execution, in the same directory where
            // you executed this program.

            return pointType;
        }

        public static TypeBuilder temp(object act)
        {
            return ((ModuleBuilder)act).DefineType("ReflectionMain");
        }

        public static void f()
        {

            Type myType = BuildDynAssembly();
            Console.WriteLine("---");

            // Let's invoke the type 'Point' created in our dynamic assembly. 

            object ptInstance = Activator.CreateInstance(myType, new object[] { });

            myType.InvokeMember("Main",
                     BindingFlags.InvokeMethod,
                     null,
                     ptInstance,
                     new object[0]);



        }
      
    }

}
