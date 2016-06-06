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
            Parametros[] listTempParams;

            public int nivelAct = 0;
            public string tipoAct = "";
            public bool banderaCambiarTipoGlob = true;
            public bool breakS = false;
            public int tipoDeVariableDesignator = 0; // 0 = variable ---- 1 = arreglo ---- 2 = clase
            public bool esArray = false;
            public FieldBuilder elemenClassGlobal;
            public bool elemClassState = false;
            public LocalBuilder breaks; 



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
            //public ILGenerator MethodActGen; // Gen del metodo principal    
            public ILGenerator MethodActGen;  // Gen del metodo actual
            public ILGenerator classGen;  // Gen del metodo actual
    

 
  
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
        MVar objMethod= (MVar)vaMethodList_var.Find(item => ((MVar)item).nom.Equals(MethodAct.Name));
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

    varParams buscarvaMethodList_params(string nombre)
    {
        methodParams objClase = (methodParams)vaMethodList_params.Find(item => ((methodParams)item).metodo.Equals(MethodAct.Name));
        if (objClase != null)
        {
            varParams variable = (varParams)objClase.listParams.Find(item => ((varParams)item).var.Name.Equals(nombre));
            return variable;
        }
        else {
            return null;
        }
    }


    MVar buscarvaMethodList_varMETHOD(string nombre)
    {
        MVar objMethod = (MVar)vaMethodList_var.Find(item => ((MVar)item).nom.Equals(MethodAct.Name));
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
        ClassProgram = GenModuleBldr.DefineType(idClase, TypeAttributes.Public | TypeAttributes.Class | TypeAttributes.BeforeFieldInit | TypeAttributes.AutoClass | TypeAttributes.AnsiClass);

        MethodMain = ClassProgram.DefineMethod("main", MethodAttributes.Public | MethodAttributes.Static, typeof(void), null);
        varMethList.Add(MethodMain);
        Type objType = Type.GetType("System.Object");
        objCtor = objType.GetConstructor(new Type[0]);
        

        GenConstr = ClassProgram.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, null);
        constGen = GenConstr.GetILGenerator();
        MethodActGen = MethodMain.GetILGenerator();
        //MethodActGen = MethodMain.GetILGenerator();




        //CREAR LEN 
        System.Type[] ParamTypes = new Type[] { typeof(int[]) };
        MethodAct = ClassProgram.DefineMethod("len", MethodAttributes.Public | MethodAttributes.Static, typeof(int), ParamTypes);
        methodParams nuevaClaseParams = new methodParams("len", "int");
        ParameterBuilder Param = MethodAct.DefineParameter(0, ParameterAttributes.In, "arr");
        varParams varP = new varParams(Param, "int[]");
        nuevaClaseParams.listParams.Add(varP); 
        MethodActGen = MethodAct.GetILGenerator();
        MethodActGen.Emit(OpCodes.Ldarg,0);
        MethodActGen.Emit(OpCodes.Ldlen);
        MethodActGen.Emit(OpCodes.Ret);
        varMethList.Add(MethodAct);

        //CREAR CHR 
        System.Type[] ParamTypes2 = new Type[] { typeof(int) };
        MethodAct = ClassProgram.DefineMethod("chr", MethodAttributes.Public | MethodAttributes.Static, typeof(char), ParamTypes2);
        methodParams nuevaClaseParams2 = new methodParams("chr", "char");
        ParameterBuilder Param2 = MethodAct.DefineParameter(0, ParameterAttributes.In, "inte");
        varParams varP2 = new varParams(Param2, "int[]");
        nuevaClaseParams2.listParams.Add(varP2);
        MethodActGen = MethodAct.GetILGenerator();
        MethodActGen.Emit(OpCodes.Ldarg,'h');
        MethodActGen.Emit(OpCodes.Ret);
        varMethList.Add(MethodAct);

        //CREAR ORD 
        System.Type[] ParamTypes3 = new Type[] { typeof(char) };
        MethodAct = ClassProgram.DefineMethod("ord", MethodAttributes.Public | MethodAttributes.Static, typeof(int), ParamTypes2);
        methodParams nuevaClaseParams3 = new methodParams("ord", "int");
        ParameterBuilder Param3 = MethodAct.DefineParameter(0, ParameterAttributes.In, "cha");
        varParams varP3 = new varParams(Param3, "int[]");
        nuevaClaseParams3.listParams.Add(varP3);
        MethodActGen = MethodAct.GetILGenerator();
        MethodActGen.Emit(OpCodes.Ldarg, 0);
        MethodActGen.Emit(OpCodes.Ret);
        varMethList.Add(MethodAct);



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


        //MethodActGen.Emit(OpCodes.Newobj, GenConstr);
        MethodActGen.Emit(OpCodes.Ldc_I4_8);
        MethodActGen.EmitCall(OpCodes.Call, writeMI, null);

        ¨*/

        /*
        string idMethod = "sumar";
         System.Type[] ParamTypes = new Type[] { };
         MethodAct = ClassProgram.DefineMethod(idMethod, MethodAttributes.Public | MethodAttributes.Static, typeof(int), ParamTypes);



       MethodActGen = MethodAct.GetILGenerator();
       MethodActGen.Emit(OpCodes.Ldc_I4, 1);
       MethodActGen.Emit(OpCodes.Ldc_I4, 4);
       MethodActGen.Emit(OpCodes.Add);
       MethodActGen.Emit(OpCodes.Ret);


       
            object[] inputValsList = new object[] { };
       //object magicClassObject = GenConstr.Invoke(new object[] { });
       //Type tipo = Type.GetType(ClassProgram.Name);
         

        /*
      

       Metodo.Invoke(magicClassObject, new object[] { });
 
        */
        
        MethodInfo readLineMI = typeof(Console).GetMethod(
                          "ReadLine",
                          new Type[0]);
        MethodActGen.EmitCall(OpCodes.Call, readLineMI, null);
        MethodActGen.Emit(OpCodes.Pop);
        MethodActGen.Emit(OpCodes.Ret);
        ClassProgram.CreateType();

        //Type typeClass = ClassProgram.GetTypeInfo();
        /*
        //  MethodInfo Metodo = tipo.GetMethod("sumar");
        int c = (int)typeClass.InvokeMember("sumar",
              BindingFlags.InvokeMethod | BindingFlags.Public |
              BindingFlags.Static,
              null,
              null,
              inputValsList);
        Console.WriteLine(c);
        */
       
        //Console.WriteLine(c);
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


                MethodActGen.Emit(OpCodes.Newobj, GenConstr);
                MethodActGen.Emit(OpCodes.Ldfld, constGlobList[0]);
                MethodActGen.EmitCall(OpCodes.Call, writeMI, null);

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

         
           MethodActGen.Emit(OpCodes.Newobj, GenConstr);
           MethodActGen.Emit(OpCodes.Ldfld, constGlobList[0]);
           MethodActGen.EmitCall(OpCodes.Call, writeMI, null);
          
            */
   }
       



   return null;
}


public override object VisitVarDeclAST([NotNull] Parser1.VarDeclASTContext context)
{
   string tipoVar =(string)Visit(context.type());
   TypeBuilder claseAct;
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
            claseAct = varClassList.Last();
           for (int i = 0; i <= context.ID().Length - 1; i++)
           {
               CPVar nuevListVarClass;
             
                nuevListVarClass = buscarvarClassList_varCLASS(varClassList.Last().Name);
            
             

               string name = context.ID(i).ToString();
               FieldBuilder v = claseAct.DefineField(name, typeof(int), FieldAttributes.Public);
               varClass varGlob = new varClass(v,"int");
               nuevListVarClass.listVar.Add(varGlob);
             

           }
               
       }
       else {
           for (int i = 0; i <= context.ID().Length - 1; i++)
           {

               //CPVar nuevListVarClass = new CPVar(claseAct.Name);
               string name = context.ID(i).ToString();
               LocalBuilder varLoc = MethodActGen.DeclareLocal(typeof(int));//Declaro 2 variables locales 0

               
               MVar nuevListVarMethod;
                if(buscarvaMethodList_varMETHOD(MethodAct.Name)!= null){
                    nuevListVarMethod = buscarvaMethodList_varMETHOD(MethodAct.Name);
                }  
                else {
                    nuevListVarMethod= new MVar(MethodAct.Name);// Se crea un elemento con la lista de variables y el nombre del metodo
                    vaMethodList_var.Add(nuevListVarMethod); //Se agrega a la lista general de varaibles de metodos
                }
                   
                 

               varLocalMethod varMethod = new varLocalMethod(name, varLoc,"int");//Se guarda en un objeto la variable local con su respectivo nombre
               nuevListVarMethod.listVar.Add(varMethod); // Se agrega la lista de variables de esa clase
       

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
               claseAct = varClassList.Last();
               CPVar nuevListVarClass;
            
                   nuevListVarClass = buscarvarClassList_varCLASS(varClassList.Last().Name);
               
              

               string name = context.ID(i).ToString();
               FieldBuilder v = claseAct.DefineField(context.ID(i).GetText(), typeof(char), FieldAttributes.Public);
               varClass varGlob = new varClass(v, "char");
               nuevListVarClass.listVar.Add(varGlob);
              
           }
       }
       else
       {
           for (int i = 0; i <= context.ID().Length - 1; i++)
           {

               //CPVar nuevListVarClass = new CPVar(claseAct.Name);
               string name = context.ID(i).ToString();
               LocalBuilder varLoc = MethodActGen.DeclareLocal(typeof(char));//Declaro 2 variables locales 0


               MVar nuevListVarMethod;
               if (buscarvaMethodList_varMETHOD(MethodAct.Name) != null)
               {
                   nuevListVarMethod = buscarvaMethodList_varMETHOD(MethodAct.Name);
               }
               else
               {
                   nuevListVarMethod = new MVar(MethodAct.Name);// Se crea un elemento con la lista de variables y el nombre del metodo
                   vaMethodList_var.Add(nuevListVarMethod); //Se agrega a la lista general de varaibles de metodos
               }



               varLocalMethod varMethod = new varLocalMethod(name, varLoc,"char");//Se guarda en un objeto la variable local con su respectivo nombre
               nuevListVarMethod.listVar.Add(varMethod); // Se agrega la lista de variables de esa clase
         

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
               claseAct = varClassList.Last();
               CPVar nuevListVarClass;
              
                   nuevListVarClass = buscarvarClassList_varCLASS(varClassList.Last().Name);
               

               string name = context.ID(i).ToString();
               FieldBuilder v = claseAct.DefineField(context.ID(i).GetText(), typeof(float), FieldAttributes.Public);
               varClass varGlob = new varClass(v, "float");
               nuevListVarClass.listVar.Add(varGlob);
    

           }
       }
       else
       {
           for (int i = 0; i <= context.ID().Length - 1; i++)
           {
               //CPVar nuevListVarClass = new CPVar(claseAct.Name);
               string name = context.ID(i).ToString();
               LocalBuilder varLoc = MethodActGen.DeclareLocal(typeof(float));//Declaro 2 variables locales 0


               MVar nuevListVarMethod;
               if (buscarvaMethodList_varMETHOD(MethodAct.Name) != null)
               {
                   nuevListVarMethod = buscarvaMethodList_varMETHOD(MethodAct.Name);
               }
               else
               {
                   nuevListVarMethod = new MVar(MethodAct.Name);// Se crea un elemento con la lista de variables y el nombre del metodo
                   vaMethodList_var.Add(nuevListVarMethod); //Se agrega a la lista general de varaibles de metodos
               }



               varLocalMethod varMethod = new varLocalMethod(name, varLoc,"float");//Se guarda en un objeto la variable local con su respectivo nombre
               nuevListVarMethod.listVar.Add(varMethod); // Se agrega la lista de variables de esa clase
    

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
               claseAct = varClassList.Last();
               CPVar nuevListVarClass;
              
                   nuevListVarClass = buscarvarClassList_varCLASS(varClassList.Last().Name);

              

               string name = context.ID(i).ToString();
               FieldBuilder v = claseAct.DefineField(context.ID(i).GetText(), typeof(bool), FieldAttributes.Public);
               varClass varGlob = new varClass(v, "boolean");
               nuevListVarClass.listVar.Add(varGlob);
            
           }
       }
       else
       {
           for (int i = 0; i <= context.ID().Length - 1; i++)
           {

               //CPVar nuevListVarClass = new CPVar(claseAct.Name);
               string name = context.ID(i).ToString();
               LocalBuilder varLoc = MethodActGen.DeclareLocal(typeof(bool));//Declaro 2 variables locales 0


               MVar nuevListVarMethod;
               if (buscarvaMethodList_varMETHOD(MethodAct.Name) != null)
               {
                   nuevListVarMethod = buscarvaMethodList_varMETHOD(MethodAct.Name);
               }
               else
               {
                   nuevListVarMethod = new MVar(MethodAct.Name);// Se crea un elemento con la lista de variables y el nombre del metodo
                   vaMethodList_var.Add(nuevListVarMethod); //Se agrega a la lista general de varaibles de metodos
               }



               varLocalMethod varMethod = new varLocalMethod(name, varLoc,"boolean");//Se guarda en un objeto la variable local con su respectivo nombre
               nuevListVarMethod.listVar.Add(varMethod); // Se agrega la lista de variables de esa clase
        

           }

       }

   }
   else if (tipoVar == "int[]")
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
               claseAct = varClassList.Last();
               CPVar nuevListVarClass;
              
                   nuevListVarClass = buscarvarClassList_varCLASS(varClassList.Last().Name);
              
               string name = context.ID(i).ToString();
               FieldBuilder v = claseAct.DefineField(name, typeof(int[]), FieldAttributes.Public);
               varClass varGlob = new varClass(v, "int[]");
               nuevListVarClass.listVar.Add(varGlob);
   

           }

       }
       else
       {
           for (int i = 0; i <= context.ID().Length - 1; i++)
           {

               //CPVar nuevListVarClass = new CPVar(claseAct.Name);
               string name = context.ID(i).ToString();
               LocalBuilder varLoc = MethodActGen.DeclareLocal(typeof(int[]));//Declaro 2 variables locales 0


               MVar nuevListVarMethod;
               if (buscarvaMethodList_varMETHOD(MethodAct.Name) != null)
               {
                   nuevListVarMethod = buscarvaMethodList_varMETHOD(MethodAct.Name);
               }
               else
               {
                   nuevListVarMethod = new MVar(MethodAct.Name);// Se crea un elemento con la lista de variables y el nombre del metodo
                   vaMethodList_var.Add(nuevListVarMethod); //Se agrega a la lista general de varaibles de metodos
               }



               varLocalMethod varMethod = new varLocalMethod(name, varLoc,"int[]");//Se guarda en un objeto la variable local con su respectivo nombre
               nuevListVarMethod.listVar.Add(varMethod); // Se agrega la lista de variables de esa clase
   

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
               claseAct = varClassList.Last();
               CPVar nuevListVarClass;
             
                   nuevListVarClass = buscarvarClassList_varCLASS(varClassList.Last().Name);
              

               string name = context.ID(i).ToString();
               FieldBuilder v = claseAct.DefineField(context.ID(i).GetText(), typeof(char[]), FieldAttributes.Public);
               varClass varGlob = new varClass(v, "char[]");
               nuevListVarClass.listVar.Add(varGlob);

           }
       }
       else
       {
           for (int i = 0; i <= context.ID().Length - 1; i++)
           {

               //CPVar nuevListVarClass = new CPVar(claseAct.Name);
               string name = context.ID(i).ToString();
               LocalBuilder varLoc = MethodActGen.DeclareLocal(typeof(char[]));//Declaro 2 variables locales 0


               MVar nuevListVarMethod;
               if (buscarvaMethodList_varMETHOD(MethodAct.Name) != null)
               {
                   nuevListVarMethod = buscarvaMethodList_varMETHOD(MethodAct.Name);
               }
               else
               {
                   nuevListVarMethod = new MVar(MethodAct.Name);// Se crea un elemento con la lista de variables y el nombre del metodo
                   vaMethodList_var.Add(nuevListVarMethod); //Se agrega a la lista general de varaibles de metodos
               }



               varLocalMethod varMethod = new varLocalMethod(name, varLoc, "char[]");//Se guarda en un objeto la variable local con su respectivo nombre
               nuevListVarMethod.listVar.Add(varMethod); // Se agrega la lista de variables de esa clase


           }

       }

   }
   else {
       //"#$%&/()=?=)(/&%$#$%&/()/&%$#"#$%&/()=(/&%$#"$%&/()=(/&%$#$%&/()=)(/&%$#"$%=?=)(/&%$#(==)(/&%$#"#$%&/()=?=)(/&%$#$%&/()=?=)(/&%$#$%&/()=?=)(/&%$#"#$%&/()=)(/&%$#"$%&/()=(&$#"
       if (nivelAct == 0)
       {
           for (int i = 0; i <= context.ID().Length - 1; i++)
           {
              
                //TypeBuilder clase = (TypeBuilder)varClassList.Find(item => ((TypeBuilder)item).Name.Equals( context.ID(i).ToString()));
                FieldBuilder v = ClassProgram.DefineField(context.ID(i).ToString(), typeof(object), FieldAttributes.Private | FieldAttributes.Static);
               varGlobalMethod varGlob = new varGlobalMethod(v, tipoVar);
               varGlobList.Add(varGlob);
           }
       }
       else if (nivelAct == 2)
       {
           for (int i = 0; i <= context.ID().Length - 1; i++)
           {
               claseAct = varClassList.Last();
               CPVar nuevListVarClass;
             
                   nuevListVarClass = buscarvarClassList_varCLASS(varClassList.Last().Name);
               
               string name = context.ID(i).ToString();
               FieldBuilder v = claseAct.DefineField(context.ID(i).GetText(), typeof(object), FieldAttributes.Public);
               varClass varGlob = new varClass(v, tipoVar);
               nuevListVarClass.listVar.Add(varGlob);

           }
       }
       else
       {
           for (int i = 0; i <= context.ID().Length - 1; i++)
           {

               //CPVar nuevListVarClass = new CPVar(claseAct.Name);
               string name = context.ID(i).ToString();



               TypeBuilder clase = (TypeBuilder)varClassList.Find(item => ((TypeBuilder)item).Name.Equals(tipoVar));
                Type typeClass = clase.GetTypeInfo();

               LocalBuilder varLoc = MethodActGen.DeclareLocal(typeClass);//Declaro 2 variables locales 0


               MVar nuevListVarMethod;
               if (buscarvaMethodList_varMETHOD(MethodAct.Name) != null)
               {
                   nuevListVarMethod = buscarvaMethodList_varMETHOD(MethodAct.Name);
               }
               else
               {
                   nuevListVarMethod = new MVar(MethodAct.Name);// Se crea un elemento con la lista de variables y el nombre del metodo
                   vaMethodList_var.Add(nuevListVarMethod); //Se agrega a la lista general de varaibles de metodos
               }



               varLocalMethod varMethod = new varLocalMethod(name, varLoc, tipoVar);//Se guarda en un objeto la variable local con su respectivo nombre
               nuevListVarMethod.listVar.Add(varMethod); // Se agrega la lista de variables de esa clase


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
                                                            | TypeAttributes.NestedPublic
                                                            | TypeAttributes.BeforeFieldInit,
                                                            typeof(Object));

  
    CPVar nuevListVarClass;
    nuevListVarClass = new CPVar(c.Name, null);
    varClassList_var.Add(nuevListVarClass);

   
   varClassList.Add(c);
   ClassAct = c;
   nivelAct = 2;
   for (int i = 0; i <= context.varDecl().Length - 1; i++)
   {
       Visit(context.varDecl(i));
   }

   Type type = Type.GetType("System.Object");
   ConstructorInfo objtor = type.GetConstructor(new Type[0]);
   ConstructorBuilder contC = c.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, null);
   classGen = contC.GetILGenerator();
   classGen.Emit(OpCodes.Ldarg_0);
   classGen.Emit(OpCodes.Call, objtor);
   classGen.Emit(OpCodes.Ret);
   nuevListVarClass.cons = contC;
   

   c.CreateType();
   nivelAct = 0;
   return null;

}




public override object VisitMethodDeclAST([NotNull] Parser1.MethodDeclASTContext context)
{
   string idMethod = context.ID().GetText();
   string tipo = "";

   if (idMethod == "main")
   {
       MethodAct = MethodMain;
       MethodActGen = MethodMain.GetILGenerator(); // Hago el ILGenerator para hacer los emits
    }
   else{
       System.Type[] ParamTypes;
       if (context.formPars() != null)
       {
          ParamTypes = (System.Type[])Visit(context.formPars());
       
       }
       else
       {

           ParamTypes = new Type[] { };
      
       }

       if (context.VOID() != null)
       {
           tipo = "void";
           MethodAct = ClassProgram.DefineMethod(idMethod, MethodAttributes.Public | MethodAttributes.Static, typeof(void), ParamTypes);

           if (listTempParams!= null)
           {
               methodParams nuevaClaseParams = new methodParams(idMethod, "void");
               for (int i = 0; i < listTempParams.Length; i++)
               {
                   ParameterBuilder Param = MethodAct.DefineParameter(i, ParameterAttributes.In, listTempParams[i].idVar);
                   varParams varP = new varParams(Param, listTempParams[i].tipo);
                   nuevaClaseParams.listParams.Add(varP); // Agrego los parametros a la lista de parametros de un elemento de la lista
               }
               vaMethodList_params.Add(nuevaClaseParams);
           }
       }
       else
       {
           tipo = (string)Visit(context.type());
           if (tipo == "int")
           {
               MethodAct = ClassProgram.DefineMethod(idMethod, MethodAttributes.Public | MethodAttributes.Static, typeof(int), ParamTypes);
           }
           else if (tipo == "char")
           {
               MethodAct = ClassProgram.DefineMethod(idMethod, MethodAttributes.Public | MethodAttributes.Static, typeof(char), ParamTypes);
           }

           else if (tipo == "int[]")
           {
               MethodAct = ClassProgram.DefineMethod(idMethod, MethodAttributes.Public | MethodAttributes.Static, typeof(int[]), ParamTypes);
           }
           else if (tipo == "char[]")
           {
               MethodAct = ClassProgram.DefineMethod(idMethod, MethodAttributes.Public | MethodAttributes.Static, typeof(char[]), ParamTypes);
           }
           else if (tipo == "float")
           {
               MethodAct = ClassProgram.DefineMethod(idMethod, MethodAttributes.Public | MethodAttributes.Static, typeof(float), ParamTypes);
           }
           else
           {
               TypeBuilder clase = (TypeBuilder)varClassList.Find(item => ((TypeBuilder)item).Name.Equals(tipo));
               Type typeClass = clase.GetTypeInfo();
               MethodAct = ClassProgram.DefineMethod(idMethod, MethodAttributes.Public | MethodAttributes.Static, typeClass, ParamTypes);

           }
           MethodAct.InitLocals = true;


           if (listTempParams != null)
           {
               methodParams nuevaClaseParams = new methodParams(idMethod, tipo);
               for (int i = 0; i < listTempParams.Length; i++)
               {
                   ParameterBuilder Param = MethodAct.DefineParameter(i, ParameterAttributes.In, listTempParams[i].idVar);
                   varParams varP = new varParams(Param, listTempParams[i].tipo);
                   nuevaClaseParams.listParams.Add(varP); // Agrego los parametros a la lista de parametros de un elemento de la lista
               }
               vaMethodList_params.Add(nuevaClaseParams);
           }

       }
          MethodActGen = MethodAct.GetILGenerator(); // Hago el ILGenerator para hacer los emits
   }

   
    
   if (context.varDecl() != null) // si tiene declaración de variables se guardan en la tabla general
   {
       nivelAct = 1;
       for (int i = 0; i <= context.varDecl().Length - 1; i++)
       {
           Visit(context.varDecl(i));
       }

   }
       

   Visit(context.block());
   if (idMethod != "main")
   {
   // MethodActGen.Emit(OpCodes.Ret);
    varMethList.Add(MethodAct);
   }
   if (tipo == "void")
   {
        MethodActGen.Emit(OpCodes.Ret);
     
   }
   
   return null;
}

//************************************************  PARAMETROS DE UNA FUNCION *************************************************************

public override object VisitFormParsAST([NotNull] Parser1.FormParsASTContext context)
{
   System.Type[] ParamTypes = new Type[context.type().Length] ;

   string tipopAct = "";
   string idAct = "";
   listTempParams = new Parametros[context.type().Length];

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
           else if (tipopAct == "int[]")
           {
               ParamTypes[i] = typeof(int[]);
           }
           else if (tipopAct == "char[]")
           {
               ParamTypes[i] = typeof(char[]);
           }
           else
           {

               TypeBuilder clase = (TypeBuilder)varClassList.Find(item => ((TypeBuilder)item).Name.Equals(tipopAct));
               Type typeClass = clase.GetTypeInfo();
               ParamTypes[i] = typeClass;
           }
           Parametros x = new Parametros(idAct, tipopAct);
           listTempParams[i] = x;
                
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
   else {
       var = context.ID().GetText();
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
   varParams param = buscarvaMethodList_params(idDesig);
   varLocalMethod localVar = buscarvaMethodList_var(idDesig);
   varGlobalMethod globalVar = buscarvarGlobList(idDesig);
    
   if (context.ASIGN() != null) {
      
           if(tipoDeVariableDesignator == 0){
                Visit(context.expr());
                if (param != null)
                {

                    MethodActGen.Emit(OpCodes.Starg, param.var.Position);//localVar.var // var es el localStora


                }
                else if (localVar != null)
                {

                    MethodActGen.Emit(OpCodes.Stloc, localVar.var);//localVar.var // var es el localStora

                  
                }
                else if (globalVar != null) {
                
                        MethodActGen.Emit(OpCodes.Stsfld, globalVar.var);

            
                }
            }
           if (tipoDeVariableDesignator == 1) //ARREGLO
           {
              LocalBuilder tam = MethodActGen.DeclareLocal(typeof(int));
              MethodActGen.Emit(OpCodes.Stloc, tam);
              if (param != null)
              {

                  MethodActGen.Emit(OpCodes.Starg, param.var.Position);//localVar.var // var es el localStora
                  MethodActGen.Emit(OpCodes.Ldloc, tam);
                  Visit(context.expr());
                  MethodActGen.Emit(OpCodes.Stelem_I4);

              }

              else if (localVar != null)
               {
                   MethodActGen.Emit(OpCodes.Ldloc, localVar.var);
                   MethodActGen.Emit(OpCodes.Ldloc, tam);
                   Visit(context.expr());
                   MethodActGen.Emit(OpCodes.Stelem_I4);
                  
               

               }
               else if (globalVar != null)
               {
                    MethodActGen.Emit(OpCodes.Ldfld, globalVar.var);
                    MethodActGen.Emit(OpCodes.Ldloc, tam);
                    Visit(context.expr());
                    MethodActGen.Emit(OpCodes.Stelem_I4);

               }
           }
           else if (tipoDeVariableDesignator == 2) //CLASE
           {
               MethodActGen.Emit(OpCodes.Pop);
              Visit(context.expr());

               if (elemClassState == true)
               {
                   MethodActGen.Emit(OpCodes.Stfld, elemenClassGlobal);
                   elemClassState = false;
               }
               else {
                   if (param != null)
                   {

                       MethodActGen.Emit(OpCodes.Starg, param.var.Position);//localVar.var // var es el localStora


                   }
                   else if (localVar != null)
                   {

                       MethodActGen.Emit(OpCodes.Stloc, localVar.var);//localVar.var // var es el localStora


                   }
                   else if (globalVar != null)
                   {

                       MethodActGen.Emit(OpCodes.Stsfld, globalVar.var);

                   }
                  
               }
                  

              
           }
        
        }
   else if (context.INCRE() != null)
   {
        
       if (param != null)
       {
          

               MethodActGen.Emit(OpCodes.Ldc_I4_1);
               MethodActGen.Emit(OpCodes.Ldarg,param.var.Position);
               MethodActGen.Emit(OpCodes.Add);
               MethodActGen.Emit(OpCodes.Starg, param.var.Position);

           
       }
        
       else  if (localVar != null)
            {
                 MethodActGen.Emit(OpCodes.Ldc_I4_1);
                 MethodActGen.Emit(OpCodes.Ldloc, localVar.var);
                 MethodActGen.Emit(OpCodes.Add);
                 MethodActGen.Emit(OpCodes.Stloc, localVar.var);

                  
            }
            else if (globalVar != null) {

                MethodActGen.Emit(OpCodes.Ldc_I4_1);
                MethodActGen.Emit(OpCodes.Ldloc, globalVar.var);
                MethodActGen.Emit(OpCodes.Add);
                MethodActGen.Emit(OpCodes.Stsfld, globalVar.var);

            
            }
        }

   else if (context.DECRE() != null)
   {
       
     if (param != null)
     {
         MethodActGen.Emit(OpCodes.Ldc_I4_1);
         MethodActGen.Emit(OpCodes.Starg, param.var.Position);
         MethodActGen.Emit(OpCodes.Sub);
         MethodActGen.Emit(OpCodes.Stloc, param.var.Position);
           

         
     }
      
       if (localVar != null)
       {
           MethodActGen.Emit(OpCodes.Ldc_I4_1);
           MethodActGen.Emit(OpCodes.Ldloc, localVar.var);
           MethodActGen.Emit(OpCodes.Sub);
           MethodActGen.Emit(OpCodes.Stloc, localVar.var);//localVar.var // var es el localStora


       }
       else if (globalVar != null)
       {

           MethodActGen.Emit(OpCodes.Ldc_I4_1);
           MethodActGen.Emit(OpCodes.Ldloc, globalVar.var);
           MethodActGen.Emit(OpCodes.Sub);
           MethodActGen.Emit(OpCodes.Stsfld, globalVar.var);


       }
   }
   else if (context.PIZQ() != null) {


       if (context.actPars() != null)
       {
           Visit(context.actPars());
           MethodBuilder meto = (MethodBuilder)varMethList.Find(item => ((MethodBuilder)item).Name.Equals(idDesig));
           methodParams b = (methodParams)vaMethodList_params.Find(item => ((methodParams)item).metodo.Equals(idDesig));
           MethodActGen.EmitCall(OpCodes.Call, meto, null);
           if(b.tipo != "void"){
                MethodActGen.Emit(OpCodes.Pop);
             }
       }
       else
       {


           MethodBuilder meto = (MethodBuilder)varMethList.Find(item => ((MethodBuilder)item).Name.Equals(idDesig));

           methodParams b = (methodParams)vaMethodList_params.Find(item => ((methodParams)item).metodo.Equals(idDesig));
           MethodActGen.EmitCall(OpCodes.Call, meto, null);
           if (b.tipo != "void")
           {
               MethodActGen.Emit(OpCodes.Pop);
           }
           //tipoAct = "int";
       }
         
   
   }

        return null;
    }

    public override object VisitIfStatAST([NotNull] Parser1.IfStatASTContext context)
    {
        if (context.statement(1) != null)
        {
            Label gblelse = MethodActGen.DefineLabel();
            Label gblif = MethodActGen.DefineLabel();
            Label salga = MethodActGen.DefineLabel();

            Visit(context.condition());
            MethodActGen.Emit(OpCodes.Brfalse, gblelse);
            MethodActGen.Emit(OpCodes.Br, gblif);


            MethodActGen.MarkLabel(gblif);
            Visit(context.statement(0));
            MethodActGen.Emit(OpCodes.Br, salga);

            MethodActGen.MarkLabel(gblelse);
            Visit(context.statement(1));


            MethodActGen.MarkLabel(salga);
        }
        else {
            Label gblelse = MethodActGen.DefineLabel();
            Label gblif = MethodActGen.DefineLabel();
            Label salga = MethodActGen.DefineLabel();

            Visit(context.condition());


            MethodActGen.Emit(OpCodes.Brfalse, salga);
            MethodActGen.Emit(OpCodes.Br, gblif);


            MethodActGen.MarkLabel(gblif);
            Visit(context.statement(0));
            MethodActGen.Emit(OpCodes.Br, salga);
            MethodActGen.MarkLabel(salga);
        }

        return null;
    
    }

    public override object VisitForStatAST([NotNull] Parser1.ForStatASTContext context)
    {//VERIFICAR BREAK
        breaks = MethodActGen.DeclareLocal(typeof(int));
        MethodActGen.Emit(OpCodes.Ldc_I4_0);
        MethodActGen.Emit(OpCodes.Stloc, breaks);

        if (context.condition() != null && context.statement() != null)
        {
            MethodActGen.Emit(OpCodes.Ldc_I4_0);
            MethodActGen.Emit(OpCodes.Stloc, breaks);

            Label k = MethodActGen.DefineLabel();
            Label p = MethodActGen.DefineLabel();
            Label j = MethodActGen.DefineLabel();

            MethodActGen.MarkLabel(p);
            Visit(context.condition());
            MethodActGen.Emit(OpCodes.Brtrue, k);
            MethodActGen.Emit(OpCodes.Br, j);


            MethodActGen.MarkLabel(k);
            Visit(context.statement(1));
            Visit(context.statement(0));

            MethodActGen.Emit(OpCodes.Ldloc, breaks);
            MethodActGen.Emit(OpCodes.Brtrue, j);
            MethodActGen.Emit(OpCodes.Br, p);

            MethodActGen.MarkLabel(j);
        }
        else {
            Label k = MethodActGen.DefineLabel();
            Label p = MethodActGen.DefineLabel();
            Label j = MethodActGen.DefineLabel();

            MethodActGen.MarkLabel(p);
            Visit(context.statement(1));
            MethodActGen.Emit(OpCodes.Ldloc,breaks);
            MethodActGen.Emit(OpCodes.Brtrue, j);
            MethodActGen.Emit(OpCodes.Br, p);
            
            MethodActGen.MarkLabel(j);
        }
        MethodActGen.Emit(OpCodes.Ldc_I4_0);
        MethodActGen.Emit(OpCodes.Stloc, breaks);
        return null;
    }
    public override object VisitWhileStatAST([NotNull] Parser1.WhileStatASTContext context)
    {
        breaks = MethodActGen.DeclareLocal(typeof(int));
        MethodActGen.Emit(OpCodes.Ldc_I4_0);
        MethodActGen.Emit(OpCodes.Stloc, breaks);
 
        Label k = MethodActGen.DefineLabel();
        Label p = MethodActGen.DefineLabel();
        Label j = MethodActGen.DefineLabel();

        MethodActGen.MarkLabel(p);
        Visit(context.condition());
        MethodActGen.Emit(OpCodes.Brtrue,k);
        MethodActGen.Emit(OpCodes.Br,j);

        MethodActGen.MarkLabel(k);
        Visit(context.statement());


        MethodActGen.Emit(OpCodes.Ldloc,breaks);
        MethodActGen.Emit(OpCodes.Brtrue, j);

        MethodActGen.Emit(OpCodes.Br,p);

        MethodActGen.MarkLabel(j);
        MethodActGen.Emit(OpCodes.Ldc_I4_0);
        MethodActGen.Emit(OpCodes.Stloc, breaks);

        return null;
    }

    public override object VisitBreakStatAST([NotNull] Parser1.BreakStatASTContext context)
    {
       
        MethodActGen.Emit(OpCodes.Ldc_I4_1);
        MethodActGen.Emit(OpCodes.Stloc, breaks);

        return null;
    }

    //FALTA
    public override object VisitForeachStatAST([NotNull] Parser1.ForeachStatASTContext context)
    {
        breaks = MethodActGen.DeclareLocal(typeof(int));
        MethodActGen.Emit(OpCodes.Ldc_I4_0);
        MethodActGen.Emit(OpCodes.Stloc, breaks);
        // CICLO_FOREACH PIZQ type ID IN expr PDER statement												#foreachStatAST
        string tipo = Visit(context.type()).ToString();
        string nombVar = context.ID().ToString();
        if(tipo == "int"){
            LocalBuilder varLoc = MethodActGen.DeclareLocal(typeof(int));//Declaro 2 variables locales 0
          

            MVar nuevListVarMethod;
            if (buscarvaMethodList_varMETHOD(MethodAct.Name) != null)
            {
                nuevListVarMethod = buscarvaMethodList_varMETHOD(MethodAct.Name);
            }
            else
            {
                nuevListVarMethod = new MVar(MethodAct.Name);// Se crea un elemento con la lista de variables y el nombre del metodo
                vaMethodList_var.Add(nuevListVarMethod); //Se agrega a la lista general de varaibles de metodos

            }

            varLocalMethod v = new varLocalMethod(nombVar, varLoc, tipo);
            nuevListVarMethod.listVar.Add(v); // Se agrega la lista de variables de esa clase
           
            LocalBuilder len = MethodActGen.DeclareLocal(typeof(int));//Declaro 2 variables locales 0
            LocalBuilder arr = MethodActGen.DeclareLocal(typeof(int[]));//Declaro 2 variables locales 0
            LocalBuilder i = MethodActGen.DeclareLocal(typeof(int));//Declaro 2 variables locales 0
            Label k = MethodActGen.DefineLabel();
            Label p = MethodActGen.DefineLabel();
            Label j = MethodActGen.DefineLabel();

            Visit(context.expr());

            MethodActGen.Emit(OpCodes.Stloc, arr);
            MethodActGen.Emit(OpCodes.Ldloc, arr);
            MethodActGen.Emit(OpCodes.Ldlen);
            MethodActGen.Emit(OpCodes.Stloc, len);
            MethodActGen.Emit(OpCodes.Ldc_I4_0);
            MethodActGen.Emit(OpCodes.Stloc,i);

            //Condicion
            MethodActGen.MarkLabel(p);
            MethodActGen.Emit(OpCodes.Ldloc, i);
            MethodActGen.Emit(OpCodes.Ldloc, len);
            MethodActGen.Emit(OpCodes.Clt);

            MethodActGen.Emit(OpCodes.Brtrue, k);
            MethodActGen.Emit(OpCodes.Br, j);
        
            MethodActGen.MarkLabel(k);
            // se reemplaza el var de x
            MethodActGen.Emit(OpCodes.Ldloc, arr);
            MethodActGen.Emit(OpCodes.Ldloc, i);
            MethodActGen.Emit(OpCodes.Ldelem_I4);
            LocalBuilder t = buscarvaMethodList_var(nombVar).var;


            MethodActGen.Emit(OpCodes.Stloc,t);

            Visit(context.statement());
            MethodActGen.Emit(OpCodes.Ldloc, breaks);
            MethodActGen.Emit(OpCodes.Brtrue, j);

            //DECREMENTAR
            MethodActGen.Emit(OpCodes.Ldloc, i);
            MethodActGen.Emit(OpCodes.Ldc_I4_1);
            MethodActGen.Emit(OpCodes.Add);
            MethodActGen.Emit(OpCodes.Stloc,i);

            MethodActGen.Emit(OpCodes.Br, p);

            MethodActGen.MarkLabel(j);
            MethodActGen.Emit(OpCodes.Ldc_I4_0);
            MethodActGen.Emit(OpCodes.Stloc, breaks);
            //ELIMINA LA VARIABLE 
            //nuevListVarMethod.listVar.Find(item => ((varLocalMethod)item).nombreVar.Equals(nombVar));
            nuevListVarMethod.listVar.Remove(v);
           
        }
          if(tipo == "char"){
              LocalBuilder varLoc = MethodActGen.DeclareLocal(typeof(char));//Declaro 2 variables locales 0


              MVar nuevListVarMethod;
              if (buscarvaMethodList_varMETHOD(MethodAct.Name) != null)
              {
                  nuevListVarMethod = buscarvaMethodList_varMETHOD(MethodAct.Name);
              }
              else
              {
                  nuevListVarMethod = new MVar(MethodAct.Name);// Se crea un elemento con la lista de variables y el nombre del metodo
                  vaMethodList_var.Add(nuevListVarMethod); //Se agrega a la lista general de varaibles de metodos

              }

              varLocalMethod v = new varLocalMethod(nombVar, varLoc, tipo);
              nuevListVarMethod.listVar.Add(v); // Se agrega la lista de variables de esa clase

              LocalBuilder len = MethodActGen.DeclareLocal(typeof(char));//Declaro 2 variables locales 0
              LocalBuilder arr = MethodActGen.DeclareLocal(typeof(char[]));//Declaro 2 variables locales 0
              LocalBuilder i = MethodActGen.DeclareLocal(typeof(char));//Declaro 2 variables locales 0
              Label k = MethodActGen.DefineLabel();
              Label p = MethodActGen.DefineLabel();
              Label j = MethodActGen.DefineLabel();

              Visit(context.expr());

              MethodActGen.Emit(OpCodes.Stloc, arr);
              MethodActGen.Emit(OpCodes.Ldloc, arr);
              MethodActGen.Emit(OpCodes.Ldlen);
              MethodActGen.Emit(OpCodes.Stloc, len);
              MethodActGen.Emit(OpCodes.Ldc_I4_0);
              MethodActGen.Emit(OpCodes.Stloc, i);

              //Condicion
              MethodActGen.MarkLabel(p);
              MethodActGen.Emit(OpCodes.Ldloc, i);
              MethodActGen.Emit(OpCodes.Ldloc, len);
              MethodActGen.Emit(OpCodes.Clt);

              MethodActGen.Emit(OpCodes.Brtrue, k);
              MethodActGen.Emit(OpCodes.Br, j);

              MethodActGen.MarkLabel(k);
              // se reemplaza el var de x
              MethodActGen.Emit(OpCodes.Ldloc, arr);
              MethodActGen.Emit(OpCodes.Ldloc, i);
              MethodActGen.Emit(OpCodes.Ldelem_I4);
              LocalBuilder t = buscarvaMethodList_var(nombVar).var;


              MethodActGen.Emit(OpCodes.Stloc, t);
              Visit(context.statement());

              //DECREMENTAR
              MethodActGen.Emit(OpCodes.Ldloc, i);
              MethodActGen.Emit(OpCodes.Ldc_I4_1);
              MethodActGen.Emit(OpCodes.Add);
              MethodActGen.Emit(OpCodes.Stloc, i);

              MethodActGen.Emit(OpCodes.Ldloc, breaks);
              MethodActGen.Emit(OpCodes.Brtrue, j);

              MethodActGen.Emit(OpCodes.Br, p);

              MethodActGen.MarkLabel(j);
              MethodActGen.Emit(OpCodes.Ldc_I4_0);
              MethodActGen.Emit(OpCodes.Stloc, breaks);
              //ELIMINA LA VARIABLE 
              //nuevListVarMethod.listVar.Find(item => ((varLocalMethod)item).nombreVar.Equals(nombVar));
              nuevListVarMethod.listVar.Remove(v);
           
        }

      

        /*
             MethodActGen.Emit(OpCodes.Stloc, tam);
            MethodActGen.Emit(OpCodes.Ldloc, localVar.var);
            MethodActGen.Emit(OpCodes.Ldloc, tam);
            Visit(context.expr());
            MethodActGen.Emit(OpCodes.Stelem_I4);

       */
        

        /*
        char[] df = new char[3];
        df[1] = 'd';
        df[1] = 'd';
        df[1] = 'e';
        foreach(char t in df){
         }
           */




        return null;
    }

    public override object VisitReturnStatAST([NotNull] Parser1.ReturnStatASTContext context)
    {
        if (context.expr() != null) {



            Visit(context.expr());
        }

        MethodActGen.Emit(OpCodes.Ret);
        return null;
    }
    public override object VisitReadStatAST([NotNull] Parser1.ReadStatASTContext context)
    {
        string idDesig = (string)Visit(context.designator());
        MethodInfo readLineMI;
        varParams param = buscarvaMethodList_params(idDesig);
        varLocalMethod localVar = buscarvaMethodList_var(idDesig);
        varGlobalMethod globalVar = buscarvarGlobList(idDesig);
        FieldBuilder ConstlVar = buscarconstGlobList(idDesig);
        if (tipoDeVariableDesignator == 0)
        {

            if (localVar != null)
            {
                if (banderaCambiarTipoGlob == true)
                {
                    tipoAct = localVar.tipo;
                }

                if (tipoAct.Equals("int"))
                {

                    readLineMI = typeof(Console).GetMethod(
                            "ReadLine",
                            new Type[0]);

                    MethodActGen.EmitCall(OpCodes.Call, readLineMI, null);


                }

                else if (tipoAct.Equals("float"))
                {
                    readLineMI = typeof(Console).GetMethod(
                                             "Readline",
                                             new Type[] { typeof(float) });
                }
                else if (tipoAct.Equals("char"))
                {

                    readLineMI = typeof(Console).GetMethod(
                                             "Readline",
                                             new Type[] { typeof(char) });

                }
                else
                {
                    readLineMI = typeof(Console).GetMethod(
                                             "Readline",
                                             new Type[] { typeof(bool) });
                }

                
                MethodActGen.EmitCall(OpCodes.Call, readLineMI, null);
                MethodActGen.Emit(OpCodes.Stloc);

            }
            else if (param != null)
            {
                if (banderaCambiarTipoGlob == true)
                {
                    tipoAct = param.tipo;
                }

                MethodActGen.Emit(OpCodes.Ldarg, param.var.Position);

            }
            else if (globalVar != null)
            {
                if (banderaCambiarTipoGlob == true)
                {
                    tipoAct = globalVar.tipo;
                }

                MethodActGen.Emit(OpCodes.Ldsfld, globalVar.var);

            }

            else if (ConstlVar != null)
            {
                MethodActGen.Emit(OpCodes.Newobj, GenConstr);
                MethodActGen.Emit(OpCodes.Ldfld, ConstlVar);

            }

        }



      

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
        
        MethodActGen.EmitCall(OpCodes.Call, writeMI, null);
    
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
        for (int i = 0; i <= context.expr().Length - 1; i++)
        {
            Visit(context.expr(i));
        }
        return null;
    }

    //xq boolean
    //************************************************  CONDITION *************************************************************

    public override object VisitConditionAST([NotNull] Parser1.ConditionASTContext context)
    {
        banderaCambiarTipoGlob = false;
        LocalBuilder varLoc = MethodActGen.DeclareLocal(typeof(bool));//Declaro 2 variables locales 0

        Label k = MethodActGen.DefineLabel();
        Label p = MethodActGen.DefineLabel();

        for (int i = 0; i <= context.condTerm().Length - 1; i++)
        {
            Visit(context.condTerm(i));
            
            MethodActGen.Emit(OpCodes.Brtrue, k);


        }
        MethodActGen.Emit(OpCodes.Ldc_I4_0);
        MethodActGen.Emit(OpCodes.Br, p);

        MethodActGen.MarkLabel(k);
        MethodActGen.Emit(OpCodes.Ldc_I4_1);
        MethodActGen.Emit(OpCodes.Br, p);

        MethodActGen.MarkLabel(p);
        banderaCambiarTipoGlob = true;
        return null;
    }


    public override object VisitCondTermAST([NotNull] Parser1.CondTermASTContext context)
    {
        LocalBuilder varLoc = MethodActGen.DeclareLocal(typeof(bool));//Declaro 2 variables locales 0

        Label k = MethodActGen.DefineLabel();
        Label p = MethodActGen.DefineLabel();

        for (int i = 0; i <= context.condFact().Length - 1; i++)
        {
            Visit(context.condFact(i));

            MethodActGen.Emit(OpCodes.Brfalse, k);


        }
        MethodActGen.Emit(OpCodes.Ldc_I4_1);
        MethodActGen.Emit(OpCodes.Br, p);

        MethodActGen.MarkLabel(k);
        MethodActGen.Emit(OpCodes.Ldc_I4_0);
        MethodActGen.Emit(OpCodes.Br, p);

        MethodActGen.MarkLabel(p);

        return null;
    }



    public override object VisitCondFactAST([NotNull] Parser1.CondFactASTContext context)
    {
            Visit(context.expr(0));
            Visit(context.expr(1));

        if (context.relop().GetText() == "==") 
        {
            MethodActGen.Emit(OpCodes.Ceq);
        }
        else if (context.relop().GetText() == "!=")
        {
            Label j = MethodActGen.DefineLabel();
            Label k = MethodActGen.DefineLabel();
            Label salir = MethodActGen.DefineLabel();
            MethodActGen.Emit(OpCodes.Ceq);
            MethodActGen.Emit(OpCodes.Brtrue, j);
            MethodActGen.Emit(OpCodes.Br,k);


            MethodActGen.MarkLabel(j);
            MethodActGen.Emit(OpCodes.Ldc_I4_0);
            MethodActGen.Emit(OpCodes.Br, salir);

            MethodActGen.MarkLabel(k);
            MethodActGen.Emit(OpCodes.Ldc_I4_1);
            MethodActGen.Emit(OpCodes.Br, salir);

            MethodActGen.MarkLabel(salir);

        }
        else if (context.relop().GetText() == ">=")
        {

            Label j = MethodActGen.DefineLabel();
            Label k = MethodActGen.DefineLabel();
            Label salir = MethodActGen.DefineLabel();

            MethodActGen.Emit(OpCodes.Cgt);
            MethodActGen.Emit(OpCodes.Brtrue, j);


            Visit(context.expr(0));
            Visit(context.expr(1));

            MethodActGen.Emit(OpCodes.Ceq);
            MethodActGen.Emit(OpCodes.Brfalse, k);
            MethodActGen.Emit(OpCodes.Br,j);


            MethodActGen.MarkLabel(j);
            MethodActGen.Emit(OpCodes.Ldc_I4_1);
            MethodActGen.Emit(OpCodes.Br,salir);

            MethodActGen.MarkLabel(k);
            MethodActGen.Emit(OpCodes.Ldc_I4_0);
            MethodActGen.Emit(OpCodes.Br,salir);

            MethodActGen.MarkLabel(salir);

        }
        else if (context.relop().GetText() == "<=")
        {
            Label j = MethodActGen.DefineLabel();
            Label k = MethodActGen.DefineLabel();
            Label salir = MethodActGen.DefineLabel();

            MethodActGen.Emit(OpCodes.Clt);
            MethodActGen.Emit(OpCodes.Brtrue, j);


            Visit(context.expr(0));
            Visit(context.expr(1));

            MethodActGen.Emit(OpCodes.Ceq);
            MethodActGen.Emit(OpCodes.Brfalse, k);
            MethodActGen.Emit(OpCodes.Br, j);


            MethodActGen.MarkLabel(j);
            MethodActGen.Emit(OpCodes.Ldc_I4_1);
            MethodActGen.Emit(OpCodes.Br, salir);

            MethodActGen.MarkLabel(k);
            MethodActGen.Emit(OpCodes.Ldc_I4_0);
            MethodActGen.Emit(OpCodes.Br, salir);

            MethodActGen.MarkLabel(salir);


        }
        else if (context.relop().GetText() == ">")
        {
            MethodActGen.Emit(OpCodes.Cgt);

        }
        else if (context.relop().GetText() == "<")
        {
            MethodActGen.Emit(OpCodes.Clt);

        }

        return null;


    }



    //************************************************  EXPRESIONES *************************************************************

    public  override object VisitExprAST([NotNull] Parser1.ExprASTContext context)
    {
     
        Visit(context.term(0));

    
            if(context.RESTA()!= null){
                MethodActGen.Emit(OpCodes.Ldc_I4, -1);
                MethodActGen.Emit(OpCodes.Mul);
            }

            for (int i = 1; i <= context.term().Length - 1; i++)
            {
                char op = Convert.ToChar(Visit(context.addop(i - 1)));
               Visit(context.term(i));

                if (op == '+')
                {
                    MethodActGen.Emit(OpCodes.Add);

                }
                else if (op == '-')
                {
                    MethodActGen.Emit(OpCodes.Sub);
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

                    MethodActGen.Emit(OpCodes.Mul);

                }
                else if (op == '/')
                {

                    MethodActGen.Emit(OpCodes.Div);
                }

                else if (op == '%')
                {

                    MethodActGen.Emit(OpCodes.Rem);
                }



                }
          
          
       
         
        return null;
    }

    //************************************************  FACTOR *************************************************************

    public override object VisitDesignatorFactorAST([NotNull] Parser1.DesignatorFactorASTContext context)
    {
        if (context.PIZQ() == null)
        {
            string idDesig = (string)Visit(context.designator());
            varParams param = buscarvaMethodList_params(idDesig);
            varLocalMethod localVar = buscarvaMethodList_var(idDesig);
            varGlobalMethod globalVar = buscarvarGlobList(idDesig);
            FieldBuilder ConstlVar = buscarconstGlobList(idDesig);
            if (tipoDeVariableDesignator == 0)
            {

                if (localVar != null)
                {
                    if (banderaCambiarTipoGlob == true)
                    {
                        tipoAct = localVar.tipo;
                    }

                    MethodActGen.Emit(OpCodes.Ldloc, localVar.var);

                }
                else if (param != null)
                {
                    if (banderaCambiarTipoGlob == true)
                    {
                        tipoAct = param.tipo;
                    }

                    MethodActGen.Emit(OpCodes.Ldarg, param.var.Position);

                }
                else if (globalVar != null)
                {
                    if (banderaCambiarTipoGlob == true)
                    {
                        tipoAct = globalVar.tipo;
                    }

                    MethodActGen.Emit(OpCodes.Ldsfld, globalVar.var);

                }
     
                else if (ConstlVar != null)
                {
                    MethodActGen.Emit(OpCodes.Newobj, GenConstr);
                    MethodActGen.Emit(OpCodes.Ldfld, ConstlVar);

                }

            }

            if (tipoDeVariableDesignator == 1)
            {
                LocalBuilder tam = MethodActGen.DeclareLocal(typeof(int));
                MethodActGen.Emit(OpCodes.Stloc, tam);
                if (localVar != null)
                {
                    if (banderaCambiarTipoGlob == true)
                    {
                        tipoAct = localVar.tipo;
                    }

                    if (tipoAct == "int[]")
                    {
                        tipoAct = "int";
                    }
                    else
                    {
                        tipoAct = "char";
                    }

                    MethodActGen.Emit(OpCodes.Ldloc, localVar.var);
                    MethodActGen.Emit(OpCodes.Ldloc, tam);
                    MethodActGen.Emit(OpCodes.Ldelem_I4);



                }

                else if (param != null)
                {
                    if (banderaCambiarTipoGlob == true)
                    {
                        tipoAct = param.tipo;
                    }
                    if (tipoAct == "int[]")
                    {
                        tipoAct = "int";
                    }
                    else
                    {
                        tipoAct = "char";
                    }

                    MethodActGen.Emit(OpCodes.Ldarg, param.var.Position);
                    MethodActGen.Emit(OpCodes.Ldloc, tam);
                    MethodActGen.Emit(OpCodes.Ldelem_I4);

                }

                else if (globalVar != null)
                {
                    if (banderaCambiarTipoGlob == true)
                    {
                        tipoAct = globalVar.tipo;
                    }
                    if (tipoAct == "int[]")
                    {
                        tipoAct = "int";
                    }
                    else
                    {
                        tipoAct = "char";
                    }

                    MethodActGen.Emit(OpCodes.Ldloc, globalVar.var);
                    MethodActGen.Emit(OpCodes.Ldloc, tam);
                    MethodActGen.Emit(OpCodes.Ldelem_I4);

                }


            }

            else if (tipoDeVariableDesignator == 2)
            {

            }

            return tipoAct;
        }
        else {
            string idDesig = (string)Visit(context.designator());
            if (context.actPars() != null)
            {
                Visit(context.actPars());
                MethodBuilder meto = (MethodBuilder)varMethList.Find(item => ((MethodBuilder)item).Name.Equals(idDesig));
                MethodActGen.EmitCall(OpCodes.Call, meto, null);  

            }
            else {


                MethodBuilder meto = (MethodBuilder)varMethList.Find(item => ((MethodBuilder)item).Name.Equals(idDesig));
                MethodActGen.EmitCall(OpCodes.Call, meto, null);  
                //tipoAct = "int";
                }
         

            return null;
        }
    }

    //HECHOS
    public override object VisitNumberFactorAST([NotNull] Parser1.NumberFactorASTContext context)
    {
        if (banderaCambiarTipoGlob == true)
        {
            tipoAct = "int";
        }
        int res = int.Parse(context.NUMBER().GetText());
        MethodActGen.Emit(OpCodes.Ldc_I4, res);
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
        MethodActGen.Emit(OpCodes.Ldc_I4, char.Parse(resultado));
        return tipoAct;
    }

    public override object VisitFloatFactorAST([NotNull] Parser1.FloatFactorASTContext context)
    {
        if (banderaCambiarTipoGlob == true)
        {
            tipoAct = "float";
        }
        float res = float.Parse(context.FLOAT().GetText());
        MethodActGen.Emit(OpCodes.Ldc_R4, res);
        return tipoAct;
    }



    public override object VisitTruefalseFactorAST([NotNull] Parser1.TruefalseFactorASTContext context)
    {
        if (banderaCambiarTipoGlob == true)
        {
            tipoAct = "boolean";
        }
        if(context.TRUE() != null){
             MethodActGen.Emit(OpCodes.Ldc_I4_1);
            return tipoAct;
        }

        else{
            MethodActGen.Emit(OpCodes.Ldc_I4_0);
            return tipoAct;
        }
      
    }
    public override object VisitNewFactorAST([NotNull] Parser1.NewFactorASTContext context)
    {
        string nombre = context.ID().GetText();
       
        //| NEW ID (PCUADRADO_IZQ expr PCUADRADO_DER)?			
 
        if(nombre == "int"){

            if (banderaCambiarTipoGlob == true)
            {
                tipoAct = "int[]";
            }
            Visit(context.expr());
            MethodActGen.Emit(OpCodes.Newarr,typeof(int));


        }
        else if (nombre == "char")
        {
            if (banderaCambiarTipoGlob == true)
            {
                tipoAct = "char[]";
            }
            Visit(context.expr());
            MethodActGen.Emit(OpCodes.Newarr, typeof(char));
        }
        else {
            CPVar clase = (CPVar)varClassList_var.Find(item => ((CPVar)item).clase.Equals(nombre));
            if (banderaCambiarTipoGlob == true)
            {
                tipoAct = nombre;
            }
            MethodActGen.Emit(OpCodes.Newobj,clase.cons);
            
        }

		
        return null;


    }
    public override object VisitExprFactorAST([NotNull] Parser1.ExprFactorASTContext context)
    {
        Visit(context.expr());
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
            //BUSCAR LA VARIABLE TIPO CLASE
            string idDesig = context.ID(0).GetText();
            varParams param = buscarvaMethodList_params(idDesig);
            varLocalMethod localVar = buscarvaMethodList_var(idDesig);
            varGlobalMethod globalVar = buscarvarGlobList(idDesig);
            FieldBuilder ConstlVar = buscarconstGlobList(idDesig);

            if (localVar != null)
            {
                if (banderaCambiarTipoGlob == true)
                {
                    tipoAct = localVar.tipo;
                }

                MethodActGen.Emit(OpCodes.Ldloc, localVar.var);

            }
            else if (globalVar != null)
            {
                if (banderaCambiarTipoGlob == true)
                {
                    tipoAct = globalVar.tipo;
                }

                MethodActGen.Emit(OpCodes.Ldsfld, globalVar.var);

            }
            //******************************************************
            string tipoTemp = "";
            for (int i = 1; i < context.ID().Length; i++)
            {
                //BUSCAR LA VARIABLE TIPO CLASE
                if (context.ID(i + 1) != null)
                {

                    tipoDeVariableDesignator = 2;
                }
                else {
                    tipoDeVariableDesignator = 2;
                    varClass clasVar = buscarvarClassList_var(context.ID(i).GetText(), tipoAct);
                    MethodActGen.Emit(OpCodes.Ldfld,clasVar.var);

                    if (banderaCambiarTipoGlob == true)
                    {
                        tipoAct = clasVar.tipo;
                    }
                    elemClassState = true;
                    elemenClassGlobal =  clasVar.var;
                }

            }
      
        }

        else
        {
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