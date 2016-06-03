using System;
using System.Collections.Generic;
using System.Linq;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using Antlr4.Runtime;
using AnalizadorSintactico;
using System.Collections;

using System.Threading;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;


class  GeneracionCodigo : Parser1BaseVisitor<Object>
{
            List<FieldBuilder> constGlobList;// Lista de constantes
            List<varGlobalMethod> varGlobList; // Lista de variables

            List<TypeBuilder> varClassList; // Lista de clases
            List<MethodBuilder> varMethList; //Lista de métodos
            
            List<CPVar> varClassList_var; // Lista de variables de clases 
            List<MVar> vaMethodList_var; // Lista de variables de metodos 
            List<methodParams> vaMethodList_params; // Lista de parametros de metodos 

           // List<Parametros> listTempParams; // Lista temporal de parametros  
            String[] listTempParams;

            public int nivelAct = 0;
            public string tipoAct = "";
            public bool banderaCambiarTipoGlob = true;
            public bool breakS = false;
            public int tipoDeVariableDesignator = 0; // 0 = variable ---- 1 = arreglo ---- 2 = clase




            AppDomain currentDom = Thread.GetDomain();

            StringBuilder asmFileNameBldr = new StringBuilder();
            string asmFileName = "GeneracionCod.exe";

            AssemblyName myAsmName = new AssemblyName();
            public AssemblyBuilder GenAsmBldr;
            public ModuleBuilder GenModuleBldr;

            public TypeBuilder ClassProgram; // Clase general   
            public TypeBuilder ClassAct; //Clase actual

            public MethodBuilder MethodMain; // Metodo principal
            public MethodBuilder MethodAct; //Metodo actual

            public ConstructorInfo objCtor;
            public ConstructorBuilder GenConstr; // Constructor

            public ILGenerator constGen; // Gen de constantes
            public ILGenerator mainGen; // Gen del metodo principal    
            public ILGenerator MethodActGen;  // Gen del metodo actual

 
  
    public GeneracionCodigo()
    {
        constGlobList = new List<FieldBuilder>();
        varGlobList = new List<varGlobalMethod>();

        varMethList = new List<MethodBuilder>();
        varClassList = new List<TypeBuilder>();

        vaMethodList_var = new List<MVar>();
        varClassList_var = new List<CPVar>();
        vaMethodList_params = new List<methodParams>(); 

        //listTempParams = new List<Parametros>();

        myAsmName.Name  = "MyDynamicAssembly";
        GenAsmBldr= AppDomain.CurrentDomain.DefineDynamicAssembly(myAsmName,AssemblyBuilderAccess.RunAndSave);
        GenModuleBldr = GenAsmBldr.DefineDynamicModule("GeneracionCodigo", "GeneracionCod.exe");
 
        
        
    }
    varGlobalMethod buscarvarGlobList (string nombre) {

        return (varGlobalMethod)varGlobList.Find(item => ((varGlobalMethod)item).var.Name.Equals(nombre));
        
    }

    FieldBuilder buscarconstGlobList(string nombre)
    {
        return (FieldBuilder)constGlobList.Find(item => ((FieldBuilder)item).Name.Equals(nombre));
    }


    varClass buscarvarClassList_var(string nombre, string nombreClase)
    {
        CPVar objClase = (CPVar)varClassList_var.Find(item => ((CPVar)item).clase.Equals(nombreClase));
        if (objClase != null)
        {
            varClass variable = (varClass)objClase.listVar.Find(item => ((varClass)item).var.Name.Equals(nombre));
            return variable;
        }
        else {
            return null;
        }

    }

    varLocalMethod buscarvaMethodList_var(string nombre)
    {
        MVar objMethod= (MVar)vaMethodList_var.Find(item => ((MVar)item).nom.Equals(MethodMain.Name));
        if (objMethod != null)
        {
            varLocalMethod variable = (varLocalMethod)objMethod.listVar.Find(item => ((varLocalMethod)item).nombreVar.Equals(nombre));
            if (variable == null) {
                return null;
            } //Var es el  LocalBuilder
            else
                return variable;
        }
        else {
            return null;
        }
    }

    ParameterBuilder buscarvaMethodList_params(string nombre)
    {
        methodParams objClase = (methodParams)vaMethodList_params.Find(item => ((methodParams)item).metodo.Equals(nombre));
        if (objClase != null)
        {
            ParameterBuilder variable = (ParameterBuilder)objClase.listParams.Find(item => ((ParameterBuilder)item).Name.Equals(nombre));
            return variable;
        }
        else {
            return null;
        }
    }


    MVar buscarvaMethodList_varMETHOD(string nombre)
    {
        MVar objMethod = (MVar)vaMethodList_var.Find(item => ((MVar)item).nom.Equals(MethodMain.Name));
        if (objMethod != null)
        {

            return objMethod;
        }
        else
        {
            return null;
        }
    }


    CPVar buscarvarClassList_varCLASS( string nombreClase)
    {
        CPVar objClase = (CPVar)varClassList_var.Find(item => ((CPVar)item).clase.Equals(nombreClase));
        if (objClase != null)
        {
     
            return objClase;
        }
        else
        {
            return null;
        }

    }





    public override object VisitProgramAST([NotNull] Parser1.ProgramASTContext context)
    {

        string idClase = context.ID().ToString();
        ClassProgram = GenModuleBldr.DefineType(idClase, TypeAttributes.Public | TypeAttributes.Class);
        MethodMain = ClassProgram.DefineMethod("main",MethodAttributes.Public | MethodAttributes.Static,typeof(void),null);
        Type objType = Type.GetType("System.Object");
        objCtor = objType.GetConstructor(new Type[0]);
        varMethList.Add(MethodMain);

        GenConstr = ClassProgram.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, null);
        constGen = GenConstr.GetILGenerator();
        mainGen = MethodMain.GetILGenerator();
      
        constGen.Emit(OpCodes.Ldarg_0);
        constGen.Emit(OpCodes.Call, objCtor);

        if (context.classDecl() != null)
        {
            for (int i = 0; i < context.classDecl().Count(); i++)
            {
                Visit(context.classDecl(i));
            }
        }
        if (context.constDecl() != null)
        {
            for (int i = 0; i < context.constDecl().Count(); i++)
            {
                Visit(context.constDecl(i));
            }
            
        }
        constGen.Emit(OpCodes.Ret);
        if (context.varDecl() != null)
        {
            for (int i = 0; i < context.varDecl().Count(); i++)
            {
                Visit(context.varDecl(i));
            }
        }

        for (int i = 0; i < context.methodDecl().Count(); i++)
        {
            Visit(context.methodDecl(i));
        }
        /*
        MethodInfo writeMI = typeof(Console).GetMethod(
                                             "WriteLine",
                                             new Type[] { typeof(int) });


        //mainGen.Emit(OpCodes.Newobj, GenConstr);
        mainGen.Emit(OpCodes.Ldc_I4_8);
        mainGen.EmitCall(OpCodes.Call, writeMI, null);

        ¨*/


        
        MethodInfo readLineMI = typeof(Console).GetMethod(
                          "ReadLine",
                          new Type[0]);
        mainGen.EmitCall(OpCodes.Call, readLineMI, null);
        mainGen.Emit(OpCodes.Pop);
        mainGen.Emit(OpCodes.Ret);
        ClassProgram.CreateType();
        GenAsmBldr.SetEntryPoint(MethodMain);
        GenAsmBldr.Save(asmFileName);
        


        return null; 


    }

    public override object VisitConstDeclAST([NotNull] Parser1.ConstDeclASTContext context)
    {

        string tipoVar = (string)Visit(context.type());

        if (tipoVar == "int")
        {
                int val = int.Parse(context.NUMBER().ToString());
                FieldBuilder varGlob = ClassProgram.DefineField(context.ID().ToString(), typeof(int), FieldAttributes.Private);        
                constGen.Emit(OpCodes.Ldarg_0);
                constGen.Emit(OpCodes.Ldc_I4,val);
                constGen.Emit(OpCodes.Stfld, varGlob);
               // constGen.Emit(OpCodes.Ldfld, varGlob);
                constGlobList.Add(varGlob);
              
                /*
                MethodInfo writeMI = typeof(Console).GetMethod(
                                              "WriteLine",
                                              new Type[] { typeof(int)});


                mainGen.Emit(OpCodes.Newobj, GenConstr);
                mainGen.Emit(OpCodes.Ldfld, constGlobList[0]);
                mainGen.EmitCall(OpCodes.Call, writeMI, null);

        */

           
        }
        else if (tipoVar == "char")
        {

            string var  = context.CharConst().GetText();
            string resultado = var.Replace("'", "");
            int val = char.Parse(resultado);
            FieldBuilder varGlob = ClassProgram.DefineField(context.ID().ToString(), typeof(char), FieldAttributes.Private);
            constGen.Emit(OpCodes.Ldarg_0);
            constGen.Emit(OpCodes.Ldc_I4, val);
            constGen.Emit(OpCodes.Stfld, varGlob);
            // constGen.Emit(OpCodes.Ldfld, varGlob);
            constGlobList.Add(varGlob);

            /*
           MethodInfo writeMI = typeof(Console).GetMethod(
                                         "WriteLine",
                                         new Type[] { typeof(int) });

         
           mainGen.Emit(OpCodes.Newobj, GenConstr);
           mainGen.Emit(OpCodes.Ldfld, constGlobList[0]);
           mainGen.EmitCall(OpCodes.Call, writeMI, null);
          
            */
   }
       



   return null;
}


public override object VisitVarDeclAST([NotNull] Parser1.VarDeclASTContext context)
{
   string tipoVar =(string)Visit(context.type());
   TypeBuilder claseAct = varClassList.Last();
   if(tipoVar == "int"){
       if (nivelAct == 0)
       {
           for (int i = 0; i <= context.ID().Length - 1; i++)
           {
               FieldBuilder v = ClassProgram.DefineField(context.ID(i).ToString(), typeof(int), FieldAttributes.Public | FieldAttributes.Static);
               varGlobalMethod varGlob = new varGlobalMethod(v,"int");
               varGlobList.Add(varGlob);
           }
       }
       else if (nivelAct == 2)
       {
           for (int i = 0; i <= context.ID().Length - 1; i++)
           {
               CPVar nuevListVarClass;
               if (buscarvarClassList_varCLASS(varClassList.Last().Name) != null)
               {
                   nuevListVarClass = buscarvarClassList_varCLASS(varClassList.Last().Name);
               }
               else
               {
                   nuevListVarClass = new CPVar(claseAct.Name);
               }

               string name = context.ID(i).ToString();
               FieldBuilder v = claseAct.DefineField(name, typeof(int), FieldAttributes.Public);
               varClass varGlob = new varClass(v,"int");
               nuevListVarClass.listVar.Add(varGlob);
               varClassList_var.Add(nuevListVarClass);

           }
               
       }
       else {
           for (int i = 0; i <= context.ID().Length - 1; i++)
           {

               //CPVar nuevListVarClass = new CPVar(claseAct.Name);
               string name = context.ID(i).ToString();
               LocalBuilder varLoc = mainGen.DeclareLocal(typeof(int));//Declaro 2 variables locales 0

               
               MVar nuevListVarMethod;
                if(buscarvaMethodList_varMETHOD(MethodMain.Name)!= null){
                    nuevListVarMethod = buscarvaMethodList_varMETHOD(MethodMain.Name);
                }  
                else {
                    nuevListVarMethod= new MVar(MethodMain.Name);// Se crea un elemento con la lista de variables y el nombre del metodo
                }
                   
                 

               varLocalMethod varMethod = new varLocalMethod(name, varLoc,"int");//Se guarda en un objeto la variable local con su respectivo nombre
               nuevListVarMethod.listVar.Add(varMethod); // Se agrega la lista de variables de esa clase
               vaMethodList_var.Add(nuevListVarMethod); //Se agrega a la lista general de varaibles de metodos

           }

       }
            
   }
   else if (tipoVar == "char")
   {
       if (nivelAct == 0)
       {
           for (int i = 0; i <= context.ID().Length - 1; i++)
           {

               FieldBuilder v = ClassProgram.DefineField(context.ID(i).ToString(), typeof(char), FieldAttributes.Public | FieldAttributes.Static);
               varGlobalMethod varGlob = new varGlobalMethod(v, "char");
               varGlobList.Add(varGlob);
           }
       }
       else if (nivelAct == 2)
       {
           for (int i = 0; i <= context.ID().Length - 1; i++)
           {
               CPVar nuevListVarClass;
               if (buscarvarClassList_varCLASS(varClassList.Last().Name) != null)
               {
                   nuevListVarClass = buscarvarClassList_varCLASS(varClassList.Last().Name);
               }
               else
               {
                   nuevListVarClass = new CPVar(claseAct.Name);
               }

               string name = context.ID(i).ToString();
               FieldBuilder v = claseAct.DefineField(context.ID(i).GetText(), typeof(char), FieldAttributes.Public);
               varClass varGlob = new varClass(v, "char");
               nuevListVarClass.listVar.Add(varGlob);
               varClassList_var.Add(nuevListVarClass);
           }
       }
       else
       {
           for (int i = 0; i <= context.ID().Length - 1; i++)
           {

               //CPVar nuevListVarClass = new CPVar(claseAct.Name);
               string name = context.ID(i).ToString();
               LocalBuilder varLoc = mainGen.DeclareLocal(typeof(char));//Declaro 2 variables locales 0


               MVar nuevListVarMethod;
               if (buscarvaMethodList_varMETHOD(MethodMain.Name) != null)
               {
                   nuevListVarMethod = buscarvaMethodList_varMETHOD(MethodMain.Name);
               }
               else
               {
                   nuevListVarMethod = new MVar(MethodMain.Name);// Se crea un elemento con la lista de variables y el nombre del metodo
               }



               varLocalMethod varMethod = new varLocalMethod(name, varLoc,"char");//Se guarda en un objeto la variable local con su respectivo nombre
               nuevListVarMethod.listVar.Add(varMethod); // Se agrega la lista de variables de esa clase
               vaMethodList_var.Add(nuevListVarMethod); //Se agrega a la lista general de varaibles de metodos

           }

       }

   }
   else if (tipoVar == "float")
   {
       if (nivelAct == 0)
       {
           for (int i = 0; i <= context.ID().Length - 1; i++)
           {
               FieldBuilder v = ClassProgram.DefineField(context.ID(i).ToString(), typeof(float), FieldAttributes.Public | FieldAttributes.Static);
               varGlobalMethod varGlob = new varGlobalMethod(v, "float");
               varGlobList.Add(varGlob);
           }
       }
       else if (nivelAct == 2)
       {
           for (int i = 0; i <= context.ID().Length - 1; i++)
           {
               CPVar nuevListVarClass;
               if (buscarvarClassList_varCLASS(varClassList.Last().Name) != null)
               {
                   nuevListVarClass = buscarvarClassList_varCLASS(varClassList.Last().Name);
               }
               else
               {
                   nuevListVarClass = new CPVar(claseAct.Name);
               }

               string name = context.ID(i).ToString();
               FieldBuilder v = claseAct.DefineField(context.ID(i).GetText(), typeof(float), FieldAttributes.Public);
               varClass varGlob = new varClass(v, "float");
               nuevListVarClass.listVar.Add(varGlob);
               varClassList_var.Add(nuevListVarClass);

           }
       }
       else
       {
           for (int i = 0; i <= context.ID().Length - 1; i++)
           {
               //CPVar nuevListVarClass = new CPVar(claseAct.Name);
               string name = context.ID(i).ToString();
               LocalBuilder varLoc = mainGen.DeclareLocal(typeof(float));//Declaro 2 variables locales 0


               MVar nuevListVarMethod;
               if (buscarvaMethodList_varMETHOD(MethodMain.Name) != null)
               {
                   nuevListVarMethod = buscarvaMethodList_varMETHOD(MethodMain.Name);
               }
               else
               {
                   nuevListVarMethod = new MVar(MethodMain.Name);// Se crea un elemento con la lista de variables y el nombre del metodo
               }



               varLocalMethod varMethod = new varLocalMethod(name, varLoc,"float");//Se guarda en un objeto la variable local con su respectivo nombre
               nuevListVarMethod.listVar.Add(varMethod); // Se agrega la lista de variables de esa clase
               vaMethodList_var.Add(nuevListVarMethod); //Se agrega a la lista general de varaibles de metodos

           }
       }

   }
   else if (tipoVar == "boolean")
   {
       if (nivelAct == 0)
       {
           for (int i = 0; i <= context.ID().Length - 1; i++)
           {

               FieldBuilder v = ClassProgram.DefineField(context.ID(i).ToString(), typeof(bool), FieldAttributes.Public | FieldAttributes.Static);
               varGlobalMethod varGlob = new varGlobalMethod(v, "boolean");
               varGlobList.Add(varGlob);
           }
       }
       else if (nivelAct == 2)
       {
           for (int i = 0; i <= context.ID().Length - 1; i++)
           {
               CPVar nuevListVarClass;
               if (buscarvarClassList_varCLASS(varClassList.Last().Name) != null)
               {
                   nuevListVarClass = buscarvarClassList_varCLASS(varClassList.Last().Name);
               }
               else
               {
                   nuevListVarClass = new CPVar(claseAct.Name);
               }

               string name = context.ID(i).ToString();
               FieldBuilder v = claseAct.DefineField(context.ID(i).GetText(), typeof(bool), FieldAttributes.Public);
               varClass varGlob = new varClass(v, "boolean");
               nuevListVarClass.listVar.Add(varGlob);
               varClassList_var.Add(nuevListVarClass);
           }
       }
       else
       {
           for (int i = 0; i <= context.ID().Length - 1; i++)
           {

               //CPVar nuevListVarClass = new CPVar(claseAct.Name);
               string name = context.ID(i).ToString();
               LocalBuilder varLoc = mainGen.DeclareLocal(typeof(bool));//Declaro 2 variables locales 0


               MVar nuevListVarMethod;
               if (buscarvaMethodList_varMETHOD(MethodMain.Name) != null)
               {
                   nuevListVarMethod = buscarvaMethodList_varMETHOD(MethodMain.Name);
               }
               else
               {
                   nuevListVarMethod = new MVar(MethodMain.Name);// Se crea un elemento con la lista de variables y el nombre del metodo
               }



               varLocalMethod varMethod = new varLocalMethod(name, varLoc,"boolean");//Se guarda en un objeto la variable local con su respectivo nombre
               nuevListVarMethod.listVar.Add(varMethod); // Se agrega la lista de variables de esa clase
               vaMethodList_var.Add(nuevListVarMethod); //Se agrega a la lista general de varaibles de metodos

           }

       }

   }
   if (tipoVar == "int[]")
   {
       if (nivelAct == 0)
       {
           for (int i = 0; i <= context.ID().Length - 1; i++)
           {
               FieldBuilder v = ClassProgram.DefineField(context.ID(i).ToString(), typeof(int[]), FieldAttributes.Public | FieldAttributes.Static);
               varGlobalMethod varGlob = new varGlobalMethod(v, "int[]");
               varGlobList.Add(varGlob);
           }
       }
       else if (nivelAct == 2)
       {
           for (int i = 0; i <= context.ID().Length - 1; i++)
           {
               CPVar nuevListVarClass;
               if (buscarvarClassList_varCLASS(varClassList.Last().Name) != null)
               {
                   nuevListVarClass = buscarvarClassList_varCLASS(varClassList.Last().Name);
               }
               else
               {
                   nuevListVarClass = new CPVar(claseAct.Name);
               }

               string name = context.ID(i).ToString();
               FieldBuilder v = claseAct.DefineField(name, typeof(int[]), FieldAttributes.Private);
               varClass varGlob = new varClass(v, "int[]");
               nuevListVarClass.listVar.Add(varGlob);
               varClassList_var.Add(nuevListVarClass);

           }

       }
       else
       {
           for (int i = 0; i <= context.ID().Length - 1; i++)
           {

               //CPVar nuevListVarClass = new CPVar(claseAct.Name);
               string name = context.ID(i).ToString();
               LocalBuilder varLoc = mainGen.DeclareLocal(typeof(int[]));//Declaro 2 variables locales 0


               MVar nuevListVarMethod;
               if (buscarvaMethodList_varMETHOD(MethodMain.Name) != null)
               {
                   nuevListVarMethod = buscarvaMethodList_varMETHOD(MethodMain.Name);
               }
               else
               {
                   nuevListVarMethod = new MVar(MethodMain.Name);// Se crea un elemento con la lista de variables y el nombre del metodo
               }



               varLocalMethod varMethod = new varLocalMethod(name, varLoc,"int[]");//Se guarda en un objeto la variable local con su respectivo nombre
               nuevListVarMethod.listVar.Add(varMethod); // Se agrega la lista de variables de esa clase
               vaMethodList_var.Add(nuevListVarMethod); //Se agrega a la lista general de varaibles de metodos

           }

       }

   }

   else if (tipoVar == "char[]")
   {
       if (nivelAct == 0)
       {
           for (int i = 0; i <= context.ID().Length - 1; i++)
           {

               FieldBuilder v = ClassProgram.DefineField(context.ID(i).ToString(), typeof(char[]), FieldAttributes.Private | FieldAttributes.Static);
               varGlobalMethod varGlob = new varGlobalMethod(v, "char[]");
               varGlobList.Add(varGlob);
           }
       }
       else if (nivelAct == 2)
       {
           for (int i = 0; i <= context.ID().Length - 1; i++)
           {
               CPVar nuevListVarClass;
               if (buscarvarClassList_varCLASS(varClassList.Last().Name) != null)
               {
                   nuevListVarClass = buscarvarClassList_varCLASS(varClassList.Last().Name);
               }
               else
               {
                   nuevListVarClass = new CPVar(claseAct.Name);
               }

               string name = context.ID(i).ToString();
               FieldBuilder v = claseAct.DefineField(context.ID(i).GetText(), typeof(char[]), FieldAttributes.Public);
               varClass varGlob = new varClass(v, "char[]");
               nuevListVarClass.listVar.Add(varGlob);
               varClassList_var.Add(nuevListVarClass);
           }
       }
       else
       {
           for (int i = 0; i <= context.ID().Length - 1; i++)
           {

               //CPVar nuevListVarClass = new CPVar(claseAct.Name);
               string name = context.ID(i).ToString();
               LocalBuilder varLoc = mainGen.DeclareLocal(typeof(char[]));//Declaro 2 variables locales 0


               MVar nuevListVarMethod;
               if (buscarvaMethodList_varMETHOD(MethodMain.Name) != null)
               {
                   nuevListVarMethod = buscarvaMethodList_varMETHOD(MethodMain.Name);
               }
               else
               {
                   nuevListVarMethod = new MVar(MethodMain.Name);// Se crea un elemento con la lista de variables y el nombre del metodo
               }



               varLocalMethod varMethod = new varLocalMethod(name, varLoc,"char[]");//Se guarda en un objeto la variable local con su respectivo nombre
               nuevListVarMethod.listVar.Add(varMethod); // Se agrega la lista de variables de esa clase
               vaMethodList_var.Add(nuevListVarMethod); //Se agrega a la lista general de varaibles de metodos

           }

       }

   }
       //FALTAN ARREGLOS

       

   return null;

}

public override object VisitClassDeclAST([NotNull] Parser1.ClassDeclASTContext context)
{
       
   Type objType = Type.GetType("System.Object");
   string name = context.ID().ToString();
   TypeBuilder c = ClassProgram.DefineNestedType(context.ID().GetText(), TypeAttributes.AnsiClass
                                                            | TypeAttributes.AutoClass
                                                            | TypeAttributes.NestedPrivate
                                                            | TypeAttributes.BeforeFieldInit,
                                                            typeof(Object));


   c.CreateType();

   
   varClassList.Add(c);
   ClassAct = c;
   nivelAct = 2;
   for (int i = 0; i <= context.varDecl().Length - 1; i++)
   {
       //Visit(context.varDecl(i));
   }
        

      
   nivelAct = 0;
   return null;

}






public override object VisitMethodDeclAST([NotNull] Parser1.MethodDeclASTContext context)
{
   string idMethod = context.ID().GetText();
   string tipo = "";

   if (context.formPars() != null)
   {
      // System.Type[] ParamTypes = (System.Type[])Visit(context.formPars());
       if (context.VOID() != null)
       {
           tipo = "void";
           //MethodAct = ClassProgram.DefineMethod(idMethod, MethodAttributes.Public | MethodAttributes.Static, typeof(void), ParamTypes);
       }
       else
       {
           tipo = (string)Visit(context.type());
           if (tipo == "int")
           {
              // MethodAct = ClassProgram.DefineMethod(idMethod, MethodAttributes.Public | MethodAttributes.Static, typeof(int), ParamTypes);
           }
           if (tipo == "char")
           {
              // MethodAct = ClassProgram.DefineMethod(idMethod, MethodAttributes.Public | MethodAttributes.Static, typeof(char), ParamTypes);
           }
           if (tipo == "float")
           {
             //  MethodAct = ClassProgram.DefineMethod(idMethod, MethodAttributes.Public | MethodAttributes.Static, typeof(float), ParamTypes);
           }
           //MethodAct.InitLocals = true;

           //AGREGAR PARAMETROS 
           /*
           methodParams nuevaClaseParams = new methodParams(idMethod);
           for (int i = 0; i < listTempParams.Length; i++)
           {
               ParameterBuilder Param = MethodMain.DefineParameter(i, ParameterAttributes.In, listTempParams[i]);
               nuevaClaseParams.listParams.Add(Param); // Agrego los parametros a la lista de parametros de un elemento de la lista
           }
           vaMethodList_params.Add(nuevaClaseParams); // Agrego el objeto q contiene el nombre de la clase y los parametos a la lista global de parametros
           */
           //
       }
   }
   else
   {


       if (context.VOID() != null)
       {
           tipo = "void";
         //  MethodAct = ClassProgram.DefineMethod(idMethod, MethodAttributes.Public | MethodAttributes.Static, typeof(void), null);
       }
       else
       {
           tipo = (string)Visit(context.type());
           if (tipo == "int")
           {
         //      MethodAct = ClassProgram.DefineMethod(idMethod, MethodAttributes.Public | MethodAttributes.Static, typeof(int), null);
           }
           if (tipo == "char")
           {
          //     MethodAct = ClassProgram.DefineMethod(idMethod, MethodAttributes.Public | MethodAttributes.Static, typeof(char), null);
           }
           if (tipo == "float")
           {
          //     MethodAct = ClassProgram.DefineMethod(idMethod, MethodAttributes.Public | MethodAttributes.Static, typeof(float), null);
           }
         //  MethodAct.InitLocals = true;
       }
   }

    // Agrego el metodo a la lista global de metodos
  // MethodActGen = MethodAct.GetILGenerator(); // Hago el ILGenerator para hacer los emits
  // mainGen.Emit(OpCodes.Ldc_I4,2);

   // DECLARACIÓN DE VARIABLES 
    
   if (context.varDecl() != null) // si tiene declaración de variables se guardan en la tabla general
   {
       nivelAct = 1;
       for (int i = 0; i <= context.varDecl().Length - 1; i++)
       {
           Visit(context.varDecl(i));
       }

   }
       

   Visit(context.block());
  // MethodActGen.Emit(OpCodes.Ret);
  //  varMethList.Add(MethodAct);
   return null;
}

//************************************************  PARAMETROS DE UNA FUNCION *************************************************************

public override object VisitFormParsAST([NotNull] Parser1.FormParsASTContext context)
{
   System.Type[] ParamTypes = new Type[context.type().Length] ;

   string tipopAct = "";
   string idAct = "";
   listTempParams = new String[context.type().Length];

   if (context.type(0) != null)
   {
       for (int i = 0; i <= context.type().Length - 1; i++)
       {
           tipopAct = (string)Visit(context.type(i));
           idAct = context.ID(i).GetText();

           if(tipopAct == "int"){
               ParamTypes[i] = typeof(int);
           }

           else if (tipopAct == "float")
           {
               ParamTypes[i] = typeof(int);
           }
           else if (tipopAct == "char")
           {
               ParamTypes[i] = typeof(int);
           }
           listTempParams[i] = idAct;
                
       }

   }

   return ParamTypes;
        
  
}

//************************************************  TIPOS *************************************************************

public override object VisitTypeAST([NotNull] Parser1.TypeASTContext context)
{
   string var = null;
   if (context.ID().GetText() == "int")
   {
       if (context.PCUADRADO_IZQ() != null)
       {
           var = "int[]";
       }
       else
       {
           var = "int";
       }
   }
   else if (context.ID().GetText() == "char")
   {
       if (context.PCUADRADO_IZQ() != null)
       {
           var = "char[]";
       }
       else
       {
           var = "char";
       }
   }
   else if (context.ID().GetText() == "float")
   {
          
           var = "float";
            
   }
   else if (context.ID().GetText() == "boolean")
   {
           
           var = "boolean";
           
   }
 //FALTA LO DE LAS CLASES
   return var;
}

//************************************************  STATEMENT *************************************************************

public override object VisitDesignatorStatAST([NotNull] Parser1.DesignatorStatASTContext context)
{
   //: designator (ASIGN expr | PIZQ (actPars)? PDER | INCRE | DECRE) PyCOMA							#designatorStatAST
   //Visit(context.expr());
    string idDesig = (string)Visit(context.designator());
   ParameterBuilder param = buscarvaMethodList_params(idDesig);
   varLocalMethod localVar = buscarvaMethodList_var(idDesig);
   varGlobalMethod globalVar = buscarvarGlobList(idDesig);
   if (context.ASIGN() != null) {
       /*
       if (param != null)
       {
           if (tipoAct == "int")
           {
               int Expr = (int)Visit(context.expr());
               MethodActGen.Emit(OpCodes.Ldarg, Expr);

           }
           else if (tipoAct == "char")
           {

           }
           else if (tipoAct == "flaot")
           {

           }
       }
        */
           if(tipoDeVariableDesignator == 0){
                Visit(context.expr());
                 if (localVar != null)
                {

                    mainGen.Emit(OpCodes.Stloc, localVar.var);//localVar.var // var es el localStora

                  
                }
                else if (globalVar != null) {
                
                        mainGen.Emit(OpCodes.Stsfld, globalVar.var);

            
                }
            }
           if (tipoDeVariableDesignator == 1) //ARREGLO
           {
              LocalBuilder tam = mainGen.DeclareLocal(typeof(int));
              mainGen.Emit(OpCodes.Stloc, tam);
               if (localVar != null)
               {
                   mainGen.Emit(OpCodes.Ldloc, localVar.var);
                   mainGen.Emit(OpCodes.Ldloc, tam);
                   Visit(context.expr());
                   mainGen.Emit(OpCodes.Stelem_I4);
                  
               

               }
               else if (globalVar != null)
               {
                    mainGen.Emit(OpCodes.Ldfld, globalVar.var);
                    mainGen.Emit(OpCodes.Ldloc, tam);
                    Visit(context.expr());
                    mainGen.Emit(OpCodes.Stelem_I4);

               }
           }
           if (tipoDeVariableDesignator == 2) //CLASE
           {
               Visit(context.expr());
               if (localVar != null)
               {

                   mainGen.Emit(OpCodes.Stloc, localVar.var);//localVar.var // var es el localStora


               }
               else if (globalVar != null)
               {

                   mainGen.Emit(OpCodes.Stsfld, globalVar.var);


               }
           }
        
        }
   else if (context.INCRE() != null)
   {
        /*
       if (param != null)
       {
           if (tipoAct == "int")
           {
               int Expr = (int)Visit(context.expr());
               MethodActGen.Emit(OpCodes.Ldarg, Expr);

           }
           else if (tipoAct == "char")
           {

           }
           else if (tipoAct == "flaot")
           {

           }
       }
        */
             if (localVar != null)
            {
                 mainGen.Emit(OpCodes.Ldc_I4_1);
                 mainGen.Emit(OpCodes.Ldloc, localVar.var);
                 mainGen.Emit(OpCodes.Add);
                 mainGen.Emit(OpCodes.Stloc, localVar.var);//localVar.var // var es el localStora

                  
            }
            else if (globalVar != null) {

                mainGen.Emit(OpCodes.Ldc_I4_1);
                mainGen.Emit(OpCodes.Ldloc, globalVar.var);
                mainGen.Emit(OpCodes.Add);
                mainGen.Emit(OpCodes.Stsfld, globalVar.var);

            
            }
        }

   else if (context.DECRE() != null)
   {
       /*
     if (param != null)
     {
         if (tipoAct == "int")
         {
             int Expr = (int)Visit(context.expr());
             MethodActGen.Emit(OpCodes.Ldarg, Expr);

         }
         else if (tipoAct == "char")
         {

         }
         else if (tipoAct == "flaot")
         {

         }
     }
      */
       if (localVar != null)
       {
           mainGen.Emit(OpCodes.Ldc_I4_1);
           mainGen.Emit(OpCodes.Ldloc, localVar.var);
           mainGen.Emit(OpCodes.Sub);
           mainGen.Emit(OpCodes.Stloc, localVar.var);//localVar.var // var es el localStora


       }
       else if (globalVar != null)
       {

           mainGen.Emit(OpCodes.Ldc_I4_1);
           mainGen.Emit(OpCodes.Ldloc, globalVar.var);
           mainGen.Emit(OpCodes.Sub);
           mainGen.Emit(OpCodes.Stsfld, globalVar.var);


       }
   }

        return null;
    }

    public override object VisitIfStatAST([NotNull] Parser1.IfStatASTContext context)
    {
        if (context.statement(1) != null)
        {
            Label gblelse = mainGen.DefineLabel();
            Label gblif = mainGen.DefineLabel();
            Label salga = mainGen.DefineLabel();

            Visit(context.condition());
            mainGen.Emit(OpCodes.Brfalse, gblelse);
            mainGen.Emit(OpCodes.Br, gblif);


            mainGen.MarkLabel(gblif);
            Visit(context.statement(0));
            mainGen.Emit(OpCodes.Br, salga);

            mainGen.MarkLabel(gblelse);
            Visit(context.statement(1));


            mainGen.MarkLabel(salga);
        }
        else {
            Label gblelse = mainGen.DefineLabel();
            Label gblif = mainGen.DefineLabel();
            Label salga = mainGen.DefineLabel();

            Visit(context.condition());


            mainGen.Emit(OpCodes.Brfalse, salga);
            mainGen.Emit(OpCodes.Br, gblif);


            mainGen.MarkLabel(gblif);
            Visit(context.statement(0));
            mainGen.Emit(OpCodes.Br, salga);
            mainGen.MarkLabel(salga);
        }

        return null;
    
    }

    public override object VisitForStatAST([NotNull] Parser1.ForStatASTContext context)
    {
        if (context.condition() != null && context.statement() != null)
        {
            Label k = mainGen.DefineLabel();
            Label p = mainGen.DefineLabel();
            Label j = mainGen.DefineLabel();

            mainGen.MarkLabel(p);
            Visit(context.condition());
            mainGen.Emit(OpCodes.Brtrue, k);
            mainGen.Emit(OpCodes.Br, j);


            mainGen.MarkLabel(k);
            Visit(context.statement(0));
            Visit(context.statement(1));

            mainGen.Emit(OpCodes.Br, p);

            mainGen.MarkLabel(j);
        }
        else {
            Label k = mainGen.DefineLabel();
            Label p = mainGen.DefineLabel();
            Label j = mainGen.DefineLabel();

            mainGen.MarkLabel(p);
            Visit(context.statement(1));

            mainGen.Emit(OpCodes.Br, p);

        }

        return null;
    }
    public override object VisitWhileStatAST([NotNull] Parser1.WhileStatASTContext context)
    {
 
        Label k = mainGen.DefineLabel();
        Label p = mainGen.DefineLabel();
        Label j = mainGen.DefineLabel();

        mainGen.MarkLabel(p);
        Visit(context.condition());
        mainGen.Emit(OpCodes.Brtrue,k);
        mainGen.Emit(OpCodes.Br,j);

        mainGen.MarkLabel(k);
        Visit(context.statement());

        if (breakS == true) {
            mainGen.Emit(OpCodes.Br, j);
        }


        mainGen.Emit(OpCodes.Br,p);

        mainGen.MarkLabel(j);
        breakS = false;

        return null;
    }

    public override object VisitBreakStatAST([NotNull] Parser1.BreakStatASTContext context)
    {
        breakS = true;
        return null;
    }

    //FALTA
    public override object VisitForeachStatAST([NotNull] Parser1.ForeachStatASTContext context)
    {
        
        return null;
    }

    public override object VisitReturnStatAST([NotNull] Parser1.ReturnStatASTContext context)
    {
       
        return null;
    }
    public override object VisitReadStatAST([NotNull] Parser1.ReadStatASTContext context)
    {
        
        return null;
    }

    public override object VisitWriteStatAST([NotNull] Parser1.WriteStatASTContext context)
    {
        
        Visit(context.expr());
        MethodInfo writeMI;
        if (context.NUMBER() != null)
        {
            
            if (tipoAct.Equals("int"))
            {
                
                    writeMI = typeof(Console).GetMethod(
                                            "WriteLine",
                                            new Type[] { typeof(int), typeof(int) });
                
            }
            else if (tipoAct.Equals("string"))
            {
                writeMI = typeof(Console).GetMethod(
                                         "WriteLine",
                                         new Type[] { typeof(string), typeof(int) });
            }
            else if (tipoAct.Equals("float"))
            {
                writeMI = typeof(Console).GetMethod(
                                         "WriteLine",
                                         new Type[] { typeof(float), typeof(int) });
            }
            else if (tipoAct.Equals("char"))
            {
                
                    writeMI = typeof(Console).GetMethod(
                                             "WriteLine",
                                             new Type[] { typeof(char), typeof(int) });
                
            }
            else
            {
                writeMI = typeof(Console).GetMethod(
                                         "WriteLine",
                                         new Type[] { typeof(bool), typeof(int) });
            }
        }
        else
        {
            if (tipoAct.Equals("int"))
            {
               
                    writeMI = typeof(Console).GetMethod(
                                            "WriteLine",
                                            new Type[] { typeof(int)});
                
            }
            else if (tipoAct.Equals("string"))
            {
                writeMI = typeof(Console).GetMethod(
                                         "WriteLine",
                                         new Type[] { typeof(string)});
            }
            else if (tipoAct.Equals("float"))
            {
                writeMI = typeof(Console).GetMethod(
                                         "WriteLine",
                                         new Type[] { typeof(float)});
            }
            else if (tipoAct.Equals("char"))
            {
               
                    writeMI = typeof(Console).GetMethod(
                                             "WriteLine",
                                             new Type[] { typeof(char) });
                
            }
            else
            {
                writeMI = typeof(Console).GetMethod(
                                         "WriteLine",
                                         new Type[] { typeof(bool)});
            }
        }
        
        mainGen.EmitCall(OpCodes.Call, writeMI, null);
        /*
        for (int i = 0; i < variablesGlobales.Count; i++)
        {
            if (variablesGlobales[i].Name.Equals("nPoints"))
            {
                ILGlobal.Emit(OpCodes.Newobj, pointCtor);
                ILGlobal.Emit(OpCodes.Ldfld, variablesGlobales[i]);
                break;
            }
        }
        MethodInfo writeP = typeof(Console).GetMethod("WriteLine", new Type[] { typeof(int) });
        ILGlobal.EmitCall(OpCodes.Call, writeP, null);
   */

            return null;
  
    }

    //************************************************  BLOCK *************************************************************

    public override object VisitBlockStatAST([NotNull] Parser1.BlockStatASTContext context)
    {
        Visit(context.block());

        return null;
    }


    public override object VisitBlockAST([NotNull] Parser1.BlockASTContext context)
    {
     
        for (int i = 0; i <= context.statement().Length - 1; i++)
        {
            Visit(context.statement(i));
        }
        return null;

    }
    //*****************************************  PARAMETOS(LLAMADA A FUNCION) **********************************************************

    public override object VisitActParsAST([NotNull] Parser1.ActParsASTContext context)
    {
        return null;
    }

    //xq boolean
    //************************************************  CONDITION *************************************************************

    public override object VisitConditionAST([NotNull] Parser1.ConditionASTContext context)
    {
        banderaCambiarTipoGlob = false;
        LocalBuilder varLoc = mainGen.DeclareLocal(typeof(bool));//Declaro 2 variables locales 0

        Label k = mainGen.DefineLabel();
        Label p = mainGen.DefineLabel();

        for (int i = 0; i <= context.condTerm().Length - 1; i++)
        {
            Visit(context.condTerm(i));
            
            mainGen.Emit(OpCodes.Brtrue, k);


        }
        mainGen.Emit(OpCodes.Ldc_I4_0);
        mainGen.Emit(OpCodes.Br, p);

        mainGen.MarkLabel(k);
        mainGen.Emit(OpCodes.Ldc_I4_1);
        mainGen.Emit(OpCodes.Br, p);

        mainGen.MarkLabel(p);
        banderaCambiarTipoGlob = true;
        return null;
    }


    public override object VisitCondTermAST([NotNull] Parser1.CondTermASTContext context)
    {
        LocalBuilder varLoc = mainGen.DeclareLocal(typeof(bool));//Declaro 2 variables locales 0

        Label k = mainGen.DefineLabel();
        Label p = mainGen.DefineLabel();

        for (int i = 0; i <= context.condFact().Length - 1; i++)
        {
            Visit(context.condFact(i));

            mainGen.Emit(OpCodes.Brfalse, k);


        }
        mainGen.Emit(OpCodes.Ldc_I4_1);
        mainGen.Emit(OpCodes.Br, p);

        mainGen.MarkLabel(k);
        mainGen.Emit(OpCodes.Ldc_I4_0);
        mainGen.Emit(OpCodes.Br, p);

        mainGen.MarkLabel(p);

        return null;
    }



    public override object VisitCondFactAST([NotNull] Parser1.CondFactASTContext context)
    {
            Visit(context.expr(0));
            Visit(context.expr(1));

        if (context.relop().GetText() == "==") 
        {
            mainGen.Emit(OpCodes.Ceq);
        }
        else if (context.relop().GetText() == "!=")
        {
            Label j = mainGen.DefineLabel();
            Label k = mainGen.DefineLabel();
            Label salir = mainGen.DefineLabel();
            mainGen.Emit(OpCodes.Ceq);
            mainGen.Emit(OpCodes.Brtrue, j);
            mainGen.Emit(OpCodes.Br,k);


            mainGen.MarkLabel(j);
            mainGen.Emit(OpCodes.Ldc_I4_0);
            mainGen.Emit(OpCodes.Br, salir);

            mainGen.MarkLabel(k);
            mainGen.Emit(OpCodes.Ldc_I4_1);
            mainGen.Emit(OpCodes.Br, salir);

            mainGen.MarkLabel(salir);

        }
        else if (context.relop().GetText() == ">=")
        {

            Label j = mainGen.DefineLabel();
            Label k = mainGen.DefineLabel();
            Label salir = mainGen.DefineLabel();

            mainGen.Emit(OpCodes.Cgt);
            mainGen.Emit(OpCodes.Brtrue, j);


            Visit(context.expr(0));
            Visit(context.expr(1));

            mainGen.Emit(OpCodes.Ceq);
            mainGen.Emit(OpCodes.Brfalse, k);
            mainGen.Emit(OpCodes.Br,j);


            mainGen.MarkLabel(j);
            mainGen.Emit(OpCodes.Ldc_I4_1);
            mainGen.Emit(OpCodes.Br,salir);

            mainGen.MarkLabel(k);
            mainGen.Emit(OpCodes.Ldc_I4_0);
            mainGen.Emit(OpCodes.Br,salir);

            mainGen.MarkLabel(salir);

        }
        else if (context.relop().GetText() == "<=")
        {
            Label j = mainGen.DefineLabel();
            Label k = mainGen.DefineLabel();
            Label salir = mainGen.DefineLabel();

            mainGen.Emit(OpCodes.Clt);
            mainGen.Emit(OpCodes.Brtrue, j);


            Visit(context.expr(0));
            Visit(context.expr(1));

            mainGen.Emit(OpCodes.Ceq);
            mainGen.Emit(OpCodes.Brfalse, k);
            mainGen.Emit(OpCodes.Br, j);


            mainGen.MarkLabel(j);
            mainGen.Emit(OpCodes.Ldc_I4_1);
            mainGen.Emit(OpCodes.Br, salir);

            mainGen.MarkLabel(k);
            mainGen.Emit(OpCodes.Ldc_I4_0);
            mainGen.Emit(OpCodes.Br, salir);

            mainGen.MarkLabel(salir);


        }
        else if (context.relop().GetText() == ">")
        {
            mainGen.Emit(OpCodes.Cgt);

        }
        else if (context.relop().GetText() == "<")
        {
            mainGen.Emit(OpCodes.Clt);

        }

        return null;


    }



    //************************************************  EXPRESIONES *************************************************************

    public  override object VisitExprAST([NotNull] Parser1.ExprASTContext context)
    {
     
        Visit(context.term(0));

    
            if(context.RESTA()!= null){
                mainGen.Emit(OpCodes.Ldc_I4, -1);
                mainGen.Emit(OpCodes.Mul);
            }

            for (int i = 1; i <= context.term().Length - 1; i++)
            {
                char op = Convert.ToChar(Visit(context.addop(i - 1)));
               Visit(context.term(i));

                if (op == '+')
                {
                    mainGen.Emit(OpCodes.Add);

                }
                else if (op == '-')
                {
                    mainGen.Emit(OpCodes.Sub);
                }

            
        }



       


        return null;

//        return (char)Visit(context.term(0));

    }
        //************************************************  TERMINALES *************************************************************

    public override Object VisitTermAST([NotNull] Parser1.TermASTContext context){

        Visit(context.factor(0));
  
            for (int i = 1; i <= context.factor().Length - 1; i++)
            {
                char op = Convert.ToChar(Visit(context.mulop(i - 1)));


                Visit(context.factor(i)); // Mete en pila
                 
                if (op == '*')
                {

                    mainGen.Emit(OpCodes.Mul);

                }
                else if (op == '/')
                {

                    mainGen.Emit(OpCodes.Div);
                }

                else if (op == '%')
                {

                    mainGen.Emit(OpCodes.Rem);
                }



                }
          
          
       
         
        return null;
    }

    //************************************************  FACTOR *************************************************************

    public override object VisitDesignatorFactorAST([NotNull] Parser1.DesignatorFactorASTContext context)
    {
        
        string idDesig = (string)Visit(context.designator());
        ParameterBuilder param = buscarvaMethodList_params(idDesig);
        varLocalMethod localVar = buscarvaMethodList_var(idDesig);
        varGlobalMethod globalVar = buscarvarGlobList(idDesig);
        FieldBuilder ConstlVar = buscarconstGlobList(idDesig);
       // if (tipoDeVariableDesignator == 0)
       // {
            
            if (localVar != null)
            {
                if (banderaCambiarTipoGlob == true)
                {
                    tipoAct = localVar.tipo;
                }

                mainGen.Emit(OpCodes.Ldloc, localVar.var);

            }
            else if (globalVar != null)
            {
                if (banderaCambiarTipoGlob == true)
                {
                    tipoAct = globalVar.tipo;
                }

                mainGen.Emit(OpCodes.Ldsfld, globalVar.var);
          
            }
            else if (ConstlVar != null)
            {
                mainGen.Emit(OpCodes.Newobj, GenConstr);
                mainGen.Emit(OpCodes.Ldfld, ConstlVar);

            }
       // }
        
        if (tipoDeVariableDesignator == 1)
        {
              LocalBuilder tam = mainGen.DeclareLocal(typeof(int));
              mainGen.Emit(OpCodes.Stloc, tam);
           if (localVar != null)
               {
                   if (banderaCambiarTipoGlob == true)
                   {
                       tipoAct = localVar.tipo;
                   }

                   mainGen.Emit(OpCodes.Ldloc, localVar.var);
                   mainGen.Emit(OpCodes.Ldloc, tam);
                   mainGen.Emit(OpCodes.Ldelem_I4);



            }
            else if (globalVar != null)
            {
                if (banderaCambiarTipoGlob == true)
                {
                    tipoAct = globalVar.tipo;
                }

                mainGen.Emit(OpCodes.Ldloc, globalVar.var);
                mainGen.Emit(OpCodes.Ldloc, tam);
                mainGen.Emit(OpCodes.Ldelem_I4);

            }
          
        }
        
          return tipoAct;
    }

    //HECHOS
    public override object VisitNumberFactorAST([NotNull] Parser1.NumberFactorASTContext context)
    {
        if (banderaCambiarTipoGlob == true)
        {
            tipoAct = "int";
        }
        int res = int.Parse(context.NUMBER().GetText());
        mainGen.Emit(OpCodes.Ldc_I4, res);
        return tipoAct;
    }
    public override object VisitCharconstFactorAST([NotNull] Parser1.CharconstFactorASTContext context)
    {
        if (banderaCambiarTipoGlob == true)
        {
            tipoAct = "char";
        }
        string var = context.CharConst().GetText();
        string resultado = var.Replace("'", "");
        mainGen.Emit(OpCodes.Ldc_I4, char.Parse(resultado));
        return tipoAct;
    }

    public override object VisitFloatFactorAST([NotNull] Parser1.FloatFactorASTContext context)
    {
        if (banderaCambiarTipoGlob == true)
        {
            tipoAct = "float";
        }
        float res = float.Parse(context.FLOAT().GetText());
        mainGen.Emit(OpCodes.Ldc_R4, res);
        return tipoAct;
    }



    public override object VisitTruefalseFactorAST([NotNull] Parser1.TruefalseFactorASTContext context)
    {
        if (banderaCambiarTipoGlob == true)
        {
            tipoAct = "boolean";
        }
        if(context.TRUE() != null){
             mainGen.Emit(OpCodes.Ldc_I4_1);
            return tipoAct;
        }

        else{
            mainGen.Emit(OpCodes.Ldc_I4_0);
            return tipoAct;
        }
      
    }
    public override object VisitNewFactorAST([NotNull] Parser1.NewFactorASTContext context)
    {
        //| NEW ID (PCUADRADO_IZQ expr PCUADRADO_DER)?			
        string nombre = context.ID().GetText();
        if(nombre == "int"){

            if (banderaCambiarTipoGlob == true)
            {
                tipoAct = "int[]";
            }
            Visit(context.expr());
            mainGen.Emit(OpCodes.Newarr,typeof(int));


        }
        if (nombre == "char")
        {
            if (banderaCambiarTipoGlob == true)
            {
                tipoAct = "char[]";
            }
            Visit(context.expr());
            mainGen.Emit(OpCodes.Newarr, typeof(char));
        }

		
        return null;


    }
    public override object VisitExprFactorAST([NotNull] Parser1.ExprFactorASTContext context)
    {

        return null;
    }

    //********************************************  DESIGNATOR(VAR,CLASES,METODOS) *****************************************************

    public override object VisitDesignatorAST([NotNull] Parser1.DesignatorASTContext context)
    {
        if (context.expr(0)!= null) {
            tipoDeVariableDesignator = 1;
         
                Visit(context.expr(0));
           
        }
        else if (context.ID(1) != null)
        {
            tipoDeVariableDesignator = 2;
        }
        else {
            tipoDeVariableDesignator = 0; 
        }
       return context.ID(0).GetText();

     
    }



    //************************************************  COMPARACIONES *************************************************************

    //#comparacionRelopAST
    public override object VisitComparacionRelopAST([NotNull] Parser1.ComparacionRelopASTContext context)
    {

        return "==";
    }
    //	#diferenteRelopAST
    public override object VisitDiferenteRelopAST([NotNull] Parser1.DiferenteRelopASTContext context)
    {
        return "!=";
    }
    //	#mayorRelopAST
    public override object VisitMayorRelopAST([NotNull] Parser1.MayorRelopASTContext context)
    {
        return ">";
    }
    //	#mayorigualRelopAST
    public override object VisitMayorigualRelopAST([NotNull] Parser1.MayorigualRelopASTContext context)
    {
        return ">=";
    }
    //	#menorRelopAST

    public override object VisitMenorRelopAST([NotNull] Parser1.MenorRelopASTContext context)
    {
        return "<";
    }
    //	#menorigualRelopAST

    public override object VisitMenorigualRelopAST([NotNull] Parser1.MenorigualRelopASTContext context)
    {
        return "<=";
    }

    public override object VisitMulMulopAST([NotNull] Parser1.MulMulopASTContext context)
    {
        return "*";
    }
    public override object VisitDivMulopAST([NotNull] Parser1.DivMulopASTContext context)
    {
        return "/";
    }
    public override object VisitDivmodMulopAST([NotNull] Parser1.DivmodMulopASTContext context)
    {
        return "%";
    }
    public override object VisitSumaAddopAST([NotNull] Parser1.SumaAddopASTContext context)
    {

        return "+";
    }

    public override object VisitRestaAddopAST([NotNull] Parser1.RestaAddopASTContext context)
    {
        return "-";
    }
}