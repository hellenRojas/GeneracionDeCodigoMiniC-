//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.5-SNAPSHOT
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from C:\Users\USUARIO1\Desktop\Respaldo progra arreglada\AnalizadorSintactico\Parser1.g4 by ANTLR 4.5-SNAPSHOT

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

namespace AnalizadorSintactico {

using System;

using Antlr4.Runtime.Misc;
using IParseTreeListener = Antlr4.Runtime.Tree.IParseTreeListener;
using IToken = Antlr4.Runtime.IToken;

/// <summary>
/// This interface defines a complete listener for a parse tree produced by
/// <see cref="Parser1"/>.
/// </summary>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.5-SNAPSHOT")]
[System.CLSCompliant(false)]
public interface IParser1Listener : IParseTreeListener {
	/// <summary>
	/// Enter a parse tree produced by the <c>classDeclAST</c>
	/// labeled alternative in <see cref="Parser1.classDecl"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterClassDeclAST([NotNull] Parser1.ClassDeclASTContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>classDeclAST</c>
	/// labeled alternative in <see cref="Parser1.classDecl"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitClassDeclAST([NotNull] Parser1.ClassDeclASTContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>sumaAddopAST</c>
	/// labeled alternative in <see cref="Parser1.addop"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterSumaAddopAST([NotNull] Parser1.SumaAddopASTContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>sumaAddopAST</c>
	/// labeled alternative in <see cref="Parser1.addop"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitSumaAddopAST([NotNull] Parser1.SumaAddopASTContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>restaAddopAST</c>
	/// labeled alternative in <see cref="Parser1.addop"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterRestaAddopAST([NotNull] Parser1.RestaAddopASTContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>restaAddopAST</c>
	/// labeled alternative in <see cref="Parser1.addop"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitRestaAddopAST([NotNull] Parser1.RestaAddopASTContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>condTermAST</c>
	/// labeled alternative in <see cref="Parser1.condTerm"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterCondTermAST([NotNull] Parser1.CondTermASTContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>condTermAST</c>
	/// labeled alternative in <see cref="Parser1.condTerm"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitCondTermAST([NotNull] Parser1.CondTermASTContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>constDeclAST</c>
	/// labeled alternative in <see cref="Parser1.constDecl"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterConstDeclAST([NotNull] Parser1.ConstDeclASTContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>constDeclAST</c>
	/// labeled alternative in <see cref="Parser1.constDecl"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitConstDeclAST([NotNull] Parser1.ConstDeclASTContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>programAST</c>
	/// labeled alternative in <see cref="Parser1.program"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterProgramAST([NotNull] Parser1.ProgramASTContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>programAST</c>
	/// labeled alternative in <see cref="Parser1.program"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitProgramAST([NotNull] Parser1.ProgramASTContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>methodDeclAST</c>
	/// labeled alternative in <see cref="Parser1.methodDecl"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterMethodDeclAST([NotNull] Parser1.MethodDeclASTContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>methodDeclAST</c>
	/// labeled alternative in <see cref="Parser1.methodDecl"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitMethodDeclAST([NotNull] Parser1.MethodDeclASTContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>typeAST</c>
	/// labeled alternative in <see cref="Parser1.type"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterTypeAST([NotNull] Parser1.TypeASTContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>typeAST</c>
	/// labeled alternative in <see cref="Parser1.type"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitTypeAST([NotNull] Parser1.TypeASTContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>mayorigualRelopAST</c>
	/// labeled alternative in <see cref="Parser1.relop"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterMayorigualRelopAST([NotNull] Parser1.MayorigualRelopASTContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>mayorigualRelopAST</c>
	/// labeled alternative in <see cref="Parser1.relop"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitMayorigualRelopAST([NotNull] Parser1.MayorigualRelopASTContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>mayorRelopAST</c>
	/// labeled alternative in <see cref="Parser1.relop"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterMayorRelopAST([NotNull] Parser1.MayorRelopASTContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>mayorRelopAST</c>
	/// labeled alternative in <see cref="Parser1.relop"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitMayorRelopAST([NotNull] Parser1.MayorRelopASTContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>menorRelopAST</c>
	/// labeled alternative in <see cref="Parser1.relop"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterMenorRelopAST([NotNull] Parser1.MenorRelopASTContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>menorRelopAST</c>
	/// labeled alternative in <see cref="Parser1.relop"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitMenorRelopAST([NotNull] Parser1.MenorRelopASTContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>diferenteRelopAST</c>
	/// labeled alternative in <see cref="Parser1.relop"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterDiferenteRelopAST([NotNull] Parser1.DiferenteRelopASTContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>diferenteRelopAST</c>
	/// labeled alternative in <see cref="Parser1.relop"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitDiferenteRelopAST([NotNull] Parser1.DiferenteRelopASTContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>menorigualRelopAST</c>
	/// labeled alternative in <see cref="Parser1.relop"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterMenorigualRelopAST([NotNull] Parser1.MenorigualRelopASTContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>menorigualRelopAST</c>
	/// labeled alternative in <see cref="Parser1.relop"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitMenorigualRelopAST([NotNull] Parser1.MenorigualRelopASTContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>comparacionRelopAST</c>
	/// labeled alternative in <see cref="Parser1.relop"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterComparacionRelopAST([NotNull] Parser1.ComparacionRelopASTContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>comparacionRelopAST</c>
	/// labeled alternative in <see cref="Parser1.relop"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitComparacionRelopAST([NotNull] Parser1.ComparacionRelopASTContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>formParsAST</c>
	/// labeled alternative in <see cref="Parser1.formPars"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterFormParsAST([NotNull] Parser1.FormParsASTContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>formParsAST</c>
	/// labeled alternative in <see cref="Parser1.formPars"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitFormParsAST([NotNull] Parser1.FormParsASTContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>actParsAST</c>
	/// labeled alternative in <see cref="Parser1.actPars"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterActParsAST([NotNull] Parser1.ActParsASTContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>actParsAST</c>
	/// labeled alternative in <see cref="Parser1.actPars"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitActParsAST([NotNull] Parser1.ActParsASTContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>designatorAST</c>
	/// labeled alternative in <see cref="Parser1.designator"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterDesignatorAST([NotNull] Parser1.DesignatorASTContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>designatorAST</c>
	/// labeled alternative in <see cref="Parser1.designator"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitDesignatorAST([NotNull] Parser1.DesignatorASTContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>condFactAST</c>
	/// labeled alternative in <see cref="Parser1.condFact"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterCondFactAST([NotNull] Parser1.CondFactASTContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>condFactAST</c>
	/// labeled alternative in <see cref="Parser1.condFact"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitCondFactAST([NotNull] Parser1.CondFactASTContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>conditionAST</c>
	/// labeled alternative in <see cref="Parser1.condition"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterConditionAST([NotNull] Parser1.ConditionASTContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>conditionAST</c>
	/// labeled alternative in <see cref="Parser1.condition"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitConditionAST([NotNull] Parser1.ConditionASTContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>divMulopAST</c>
	/// labeled alternative in <see cref="Parser1.mulop"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterDivMulopAST([NotNull] Parser1.DivMulopASTContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>divMulopAST</c>
	/// labeled alternative in <see cref="Parser1.mulop"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitDivMulopAST([NotNull] Parser1.DivMulopASTContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>divmodMulopAST</c>
	/// labeled alternative in <see cref="Parser1.mulop"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterDivmodMulopAST([NotNull] Parser1.DivmodMulopASTContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>divmodMulopAST</c>
	/// labeled alternative in <see cref="Parser1.mulop"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitDivmodMulopAST([NotNull] Parser1.DivmodMulopASTContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>mulMulopAST</c>
	/// labeled alternative in <see cref="Parser1.mulop"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterMulMulopAST([NotNull] Parser1.MulMulopASTContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>mulMulopAST</c>
	/// labeled alternative in <see cref="Parser1.mulop"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitMulMulopAST([NotNull] Parser1.MulMulopASTContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>readStatAST</c>
	/// labeled alternative in <see cref="Parser1.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterReadStatAST([NotNull] Parser1.ReadStatASTContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>readStatAST</c>
	/// labeled alternative in <see cref="Parser1.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitReadStatAST([NotNull] Parser1.ReadStatASTContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>returnStatAST</c>
	/// labeled alternative in <see cref="Parser1.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterReturnStatAST([NotNull] Parser1.ReturnStatASTContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>returnStatAST</c>
	/// labeled alternative in <see cref="Parser1.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitReturnStatAST([NotNull] Parser1.ReturnStatASTContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>pyStatAST</c>
	/// labeled alternative in <see cref="Parser1.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterPyStatAST([NotNull] Parser1.PyStatASTContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>pyStatAST</c>
	/// labeled alternative in <see cref="Parser1.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitPyStatAST([NotNull] Parser1.PyStatASTContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>whileStatAST</c>
	/// labeled alternative in <see cref="Parser1.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterWhileStatAST([NotNull] Parser1.WhileStatASTContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>whileStatAST</c>
	/// labeled alternative in <see cref="Parser1.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitWhileStatAST([NotNull] Parser1.WhileStatASTContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>writeStatAST</c>
	/// labeled alternative in <see cref="Parser1.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterWriteStatAST([NotNull] Parser1.WriteStatASTContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>writeStatAST</c>
	/// labeled alternative in <see cref="Parser1.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitWriteStatAST([NotNull] Parser1.WriteStatASTContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>foreachStatAST</c>
	/// labeled alternative in <see cref="Parser1.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterForeachStatAST([NotNull] Parser1.ForeachStatASTContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>foreachStatAST</c>
	/// labeled alternative in <see cref="Parser1.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitForeachStatAST([NotNull] Parser1.ForeachStatASTContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>designatorStatAST</c>
	/// labeled alternative in <see cref="Parser1.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterDesignatorStatAST([NotNull] Parser1.DesignatorStatASTContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>designatorStatAST</c>
	/// labeled alternative in <see cref="Parser1.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitDesignatorStatAST([NotNull] Parser1.DesignatorStatASTContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>ifStatAST</c>
	/// labeled alternative in <see cref="Parser1.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterIfStatAST([NotNull] Parser1.IfStatASTContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>ifStatAST</c>
	/// labeled alternative in <see cref="Parser1.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitIfStatAST([NotNull] Parser1.IfStatASTContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>forStatAST</c>
	/// labeled alternative in <see cref="Parser1.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterForStatAST([NotNull] Parser1.ForStatASTContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>forStatAST</c>
	/// labeled alternative in <see cref="Parser1.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitForStatAST([NotNull] Parser1.ForStatASTContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>blockStatAST</c>
	/// labeled alternative in <see cref="Parser1.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterBlockStatAST([NotNull] Parser1.BlockStatASTContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>blockStatAST</c>
	/// labeled alternative in <see cref="Parser1.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitBlockStatAST([NotNull] Parser1.BlockStatASTContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>breakStatAST</c>
	/// labeled alternative in <see cref="Parser1.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterBreakStatAST([NotNull] Parser1.BreakStatASTContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>breakStatAST</c>
	/// labeled alternative in <see cref="Parser1.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitBreakStatAST([NotNull] Parser1.BreakStatASTContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>blockAST</c>
	/// labeled alternative in <see cref="Parser1.block"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterBlockAST([NotNull] Parser1.BlockASTContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>blockAST</c>
	/// labeled alternative in <see cref="Parser1.block"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitBlockAST([NotNull] Parser1.BlockASTContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>exprAST</c>
	/// labeled alternative in <see cref="Parser1.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterExprAST([NotNull] Parser1.ExprASTContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>exprAST</c>
	/// labeled alternative in <see cref="Parser1.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitExprAST([NotNull] Parser1.ExprASTContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>termAST</c>
	/// labeled alternative in <see cref="Parser1.term"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterTermAST([NotNull] Parser1.TermASTContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>termAST</c>
	/// labeled alternative in <see cref="Parser1.term"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitTermAST([NotNull] Parser1.TermASTContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>exprFactorAST</c>
	/// labeled alternative in <see cref="Parser1.factor"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterExprFactorAST([NotNull] Parser1.ExprFactorASTContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>exprFactorAST</c>
	/// labeled alternative in <see cref="Parser1.factor"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitExprFactorAST([NotNull] Parser1.ExprFactorASTContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>truefalseFactorAST</c>
	/// labeled alternative in <see cref="Parser1.factor"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterTruefalseFactorAST([NotNull] Parser1.TruefalseFactorASTContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>truefalseFactorAST</c>
	/// labeled alternative in <see cref="Parser1.factor"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitTruefalseFactorAST([NotNull] Parser1.TruefalseFactorASTContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>newFactorAST</c>
	/// labeled alternative in <see cref="Parser1.factor"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterNewFactorAST([NotNull] Parser1.NewFactorASTContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>newFactorAST</c>
	/// labeled alternative in <see cref="Parser1.factor"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitNewFactorAST([NotNull] Parser1.NewFactorASTContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>designatorFactorAST</c>
	/// labeled alternative in <see cref="Parser1.factor"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterDesignatorFactorAST([NotNull] Parser1.DesignatorFactorASTContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>designatorFactorAST</c>
	/// labeled alternative in <see cref="Parser1.factor"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitDesignatorFactorAST([NotNull] Parser1.DesignatorFactorASTContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>floatFactorAST</c>
	/// labeled alternative in <see cref="Parser1.factor"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterFloatFactorAST([NotNull] Parser1.FloatFactorASTContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>floatFactorAST</c>
	/// labeled alternative in <see cref="Parser1.factor"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitFloatFactorAST([NotNull] Parser1.FloatFactorASTContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>numberFactorAST</c>
	/// labeled alternative in <see cref="Parser1.factor"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterNumberFactorAST([NotNull] Parser1.NumberFactorASTContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>numberFactorAST</c>
	/// labeled alternative in <see cref="Parser1.factor"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitNumberFactorAST([NotNull] Parser1.NumberFactorASTContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>charconstFactorAST</c>
	/// labeled alternative in <see cref="Parser1.factor"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterCharconstFactorAST([NotNull] Parser1.CharconstFactorASTContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>charconstFactorAST</c>
	/// labeled alternative in <see cref="Parser1.factor"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitCharconstFactorAST([NotNull] Parser1.CharconstFactorASTContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>varDeclAST</c>
	/// labeled alternative in <see cref="Parser1.varDecl"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterVarDeclAST([NotNull] Parser1.VarDeclASTContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>varDeclAST</c>
	/// labeled alternative in <see cref="Parser1.varDecl"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitVarDeclAST([NotNull] Parser1.VarDeclASTContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="Parser1.program"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterProgram([NotNull] Parser1.ProgramContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="Parser1.program"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitProgram([NotNull] Parser1.ProgramContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="Parser1.constDecl"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterConstDecl([NotNull] Parser1.ConstDeclContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="Parser1.constDecl"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitConstDecl([NotNull] Parser1.ConstDeclContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="Parser1.varDecl"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterVarDecl([NotNull] Parser1.VarDeclContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="Parser1.varDecl"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitVarDecl([NotNull] Parser1.VarDeclContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="Parser1.classDecl"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterClassDecl([NotNull] Parser1.ClassDeclContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="Parser1.classDecl"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitClassDecl([NotNull] Parser1.ClassDeclContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="Parser1.methodDecl"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterMethodDecl([NotNull] Parser1.MethodDeclContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="Parser1.methodDecl"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitMethodDecl([NotNull] Parser1.MethodDeclContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="Parser1.formPars"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterFormPars([NotNull] Parser1.FormParsContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="Parser1.formPars"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitFormPars([NotNull] Parser1.FormParsContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="Parser1.type"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterType([NotNull] Parser1.TypeContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="Parser1.type"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitType([NotNull] Parser1.TypeContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="Parser1.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterStatement([NotNull] Parser1.StatementContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="Parser1.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitStatement([NotNull] Parser1.StatementContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="Parser1.block"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterBlock([NotNull] Parser1.BlockContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="Parser1.block"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitBlock([NotNull] Parser1.BlockContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="Parser1.actPars"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterActPars([NotNull] Parser1.ActParsContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="Parser1.actPars"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitActPars([NotNull] Parser1.ActParsContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="Parser1.condition"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterCondition([NotNull] Parser1.ConditionContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="Parser1.condition"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitCondition([NotNull] Parser1.ConditionContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="Parser1.condTerm"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterCondTerm([NotNull] Parser1.CondTermContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="Parser1.condTerm"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitCondTerm([NotNull] Parser1.CondTermContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="Parser1.condFact"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterCondFact([NotNull] Parser1.CondFactContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="Parser1.condFact"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitCondFact([NotNull] Parser1.CondFactContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="Parser1.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterExpr([NotNull] Parser1.ExprContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="Parser1.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitExpr([NotNull] Parser1.ExprContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="Parser1.term"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterTerm([NotNull] Parser1.TermContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="Parser1.term"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitTerm([NotNull] Parser1.TermContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="Parser1.factor"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterFactor([NotNull] Parser1.FactorContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="Parser1.factor"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitFactor([NotNull] Parser1.FactorContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="Parser1.designator"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterDesignator([NotNull] Parser1.DesignatorContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="Parser1.designator"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitDesignator([NotNull] Parser1.DesignatorContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="Parser1.relop"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterRelop([NotNull] Parser1.RelopContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="Parser1.relop"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitRelop([NotNull] Parser1.RelopContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="Parser1.addop"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterAddop([NotNull] Parser1.AddopContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="Parser1.addop"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitAddop([NotNull] Parser1.AddopContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="Parser1.mulop"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterMulop([NotNull] Parser1.MulopContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="Parser1.mulop"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitMulop([NotNull] Parser1.MulopContext context);
}
} // namespace AnalizadorSintactico
