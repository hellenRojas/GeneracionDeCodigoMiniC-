//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.5-SNAPSHOT
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from c:\users\usuario1\documents\visual studio 2013\Projects\AnalizadorSintactico\AnalizadorSintactico\Lexer1.g4 by ANTLR 4.5-SNAPSHOT

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

namespace AnalizadorSintactico {
using Antlr4.Runtime;
using Antlr4.Runtime.Atn;
using Antlr4.Runtime.Misc;
using DFA = Antlr4.Runtime.Dfa.DFA;

[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.5-SNAPSHOT")]
[System.CLSCompliant(false)]
public partial class Lexer1 : Lexer {
	public const int
		WS=1, NEWLINE=2, COMMMETBLOCK=3, COMMMET=4, INT=5, STRING=6, FLOAT=7, 
		BOOLEAN=8, VOID=9, CONDICION_IF=10, CONDICION_ELSE_IF=11, CONDICION_ELSE=12, 
		CICLO_WHILE=13, CICLO_FOR=14, BREAK=15, RETURN=16, READ=17, WRITE=18, 
		CLASE=19, NEW=20, CONSTANTE=21, PyCOMA=22, COMA=23, ASIGN=24, PIZQ=25, 
		PDER=26, SUMA=27, MUL=28, DIV=29, DIVMOD=30, COMPARACION=31, DIFERENTE=32, 
		MENOR=33, MENORIGUAL=34, MAYOR=35, MAYORIGUAL=36, O=37, Y=38, INCRE=39, 
		DECRE=40, PUNTO=41, PCUADRADO_DER=42, PCUADRADO_IZQ=43, COR_DER=44, COR_IZQ=45, 
		NUM=46, ID=47, STR=48, LQUOTE=49;
	public const int STRI = 1;
	public static string[] modeNames = {
		"DEFAULT_MODE", "STRI"
	};

	public static readonly string[] ruleNames = {
		"WS", "NEWLINE", "COMMMETBLOCK", "COMMMET", "INT", "STRING", "FLOAT", 
		"BOOLEAN", "VOID", "CONDICION_IF", "CONDICION_ELSE_IF", "CONDICION_ELSE", 
		"CICLO_WHILE", "CICLO_FOR", "BREAK", "RETURN", "READ", "WRITE", "CLASE", 
		"NEW", "CONSTANTE", "PyCOMA", "COMA", "ASIGN", "PIZQ", "PDER", "SUMA", 
		"MUL", "DIV", "DIVMOD", "COMPARACION", "DIFERENTE", "MENOR", "MENORIGUAL", 
		"MAYOR", "MAYORIGUAL", "O", "Y", "INCRE", "DECRE", "PUNTO", "PCUADRADO_DER", 
		"PCUADRADO_IZQ", "COR_DER", "COR_IZQ", "NUM", "ID", "LQUOTE", "CharContenido", 
		"CharInicial", "STR", "TEXT"
	};


	public Lexer1(ICharStream input)
		: base(input)
	{
		_interp = new LexerATNSimulator(this,_ATN);
	}

	private static readonly string[] _LiteralNames = {
		null, "' '", null, null, null, "'int'", "'string'", "'float'", "'boolean'", 
		"'void'", "'if'", "'else if'", "'else'", "'while'", "'for'", "'break'", 
		"'return'", "'read'", "'write'", "'class'", "'new'", "'const'", "';'", 
		"','", "'='", "'('", "')'", "'+'", "'*'", "'/'", "'%'", "'=='", "'!='", 
		"'<'", "'<='", "'>'", "'>='", "'||'", "'&&'", "'++'", "'--'", "'.'", "'['", 
		"']'", "'{'", "'}'"
	};
	private static readonly string[] _SymbolicNames = {
		null, "WS", "NEWLINE", "COMMMETBLOCK", "COMMMET", "INT", "STRING", "FLOAT", 
		"BOOLEAN", "VOID", "CONDICION_IF", "CONDICION_ELSE_IF", "CONDICION_ELSE", 
		"CICLO_WHILE", "CICLO_FOR", "BREAK", "RETURN", "READ", "WRITE", "CLASE", 
		"NEW", "CONSTANTE", "PyCOMA", "COMA", "ASIGN", "PIZQ", "PDER", "SUMA", 
		"MUL", "DIV", "DIVMOD", "COMPARACION", "DIFERENTE", "MENOR", "MENORIGUAL", 
		"MAYOR", "MAYORIGUAL", "O", "Y", "INCRE", "DECRE", "PUNTO", "PCUADRADO_DER", 
		"PCUADRADO_IZQ", "COR_DER", "COR_IZQ", "NUM", "ID", "STR", "LQUOTE"
	};
	public static readonly IVocabulary DefaultVocabulary = new Vocabulary(_LiteralNames, _SymbolicNames);

	[System.Obsolete("Use Vocabulary instead.")]
	public static readonly string[] tokenNames = GenerateTokenNames(DefaultVocabulary, _SymbolicNames.Length);

	private static string[] GenerateTokenNames(IVocabulary vocabulary, int length) {
		string[] tokenNames = new string[length];
		for (int i = 0; i < tokenNames.Length; i++) {
			tokenNames[i] = vocabulary.GetLiteralName(i);
			if (tokenNames[i] == null) {
				tokenNames[i] = vocabulary.GetSymbolicName(i);
			}

			if (tokenNames[i] == null) {
				tokenNames[i] = "<INVALID>";
			}
		}

		return tokenNames;
	}

	[System.Obsolete]
	public override string[] TokenNames
	{
		get
		{
			return tokenNames;
		}
	}

	[NotNull]
	public override IVocabulary Vocabulary
	{
		get
		{
			return DefaultVocabulary;
		}
	}

	public override string GrammarFileName { get { return "Lexer1.g4"; } }

	public override string[] RuleNames { get { return ruleNames; } }

	public override string[] ModeNames { get { return modeNames; } }

	public override string SerializedAtn { get { return _serializedATN; } }

	public static readonly string _serializedATN =
		"\x3\xAF6F\x8320\x479D\xB75C\x4880\x1605\x191C\xAB37\x2\x33\x149\b\x1\b"+
		"\x1\x4\x2\t\x2\x4\x3\t\x3\x4\x4\t\x4\x4\x5\t\x5\x4\x6\t\x6\x4\a\t\a\x4"+
		"\b\t\b\x4\t\t\t\x4\n\t\n\x4\v\t\v\x4\f\t\f\x4\r\t\r\x4\xE\t\xE\x4\xF\t"+
		"\xF\x4\x10\t\x10\x4\x11\t\x11\x4\x12\t\x12\x4\x13\t\x13\x4\x14\t\x14\x4"+
		"\x15\t\x15\x4\x16\t\x16\x4\x17\t\x17\x4\x18\t\x18\x4\x19\t\x19\x4\x1A"+
		"\t\x1A\x4\x1B\t\x1B\x4\x1C\t\x1C\x4\x1D\t\x1D\x4\x1E\t\x1E\x4\x1F\t\x1F"+
		"\x4 \t \x4!\t!\x4\"\t\"\x4#\t#\x4$\t$\x4%\t%\x4&\t&\x4\'\t\'\x4(\t(\x4"+
		")\t)\x4*\t*\x4+\t+\x4,\t,\x4-\t-\x4.\t.\x4/\t/\x4\x30\t\x30\x4\x31\t\x31"+
		"\x4\x32\t\x32\x4\x33\t\x33\x4\x34\t\x34\x4\x35\t\x35\x3\x2\x3\x2\x3\x2"+
		"\x3\x2\x3\x3\x3\x3\x3\x3\x3\x3\x3\x4\x3\x4\x3\x4\x3\x4\a\x4y\n\x4\f\x4"+
		"\xE\x4|\v\x4\x3\x4\x3\x4\x3\x4\x3\x4\x3\x4\x3\x5\x3\x5\x3\x5\x3\x5\a\x5"+
		"\x87\n\x5\f\x5\xE\x5\x8A\v\x5\x3\x5\x3\x5\x3\x6\x3\x6\x3\x6\x3\x6\x3\a"+
		"\x3\a\x3\a\x3\a\x3\a\x3\a\x3\a\x3\b\x3\b\x3\b\x3\b\x3\b\x3\b\x3\t\x3\t"+
		"\x3\t\x3\t\x3\t\x3\t\x3\t\x3\t\x3\n\x3\n\x3\n\x3\n\x3\n\x3\v\x3\v\x3\v"+
		"\x3\f\x3\f\x3\f\x3\f\x3\f\x3\f\x3\f\x3\f\x3\r\x3\r\x3\r\x3\r\x3\r\x3\xE"+
		"\x3\xE\x3\xE\x3\xE\x3\xE\x3\xE\x3\xF\x3\xF\x3\xF\x3\xF\x3\x10\x3\x10\x3"+
		"\x10\x3\x10\x3\x10\x3\x10\x3\x11\x3\x11\x3\x11\x3\x11\x3\x11\x3\x11\x3"+
		"\x11\x3\x12\x3\x12\x3\x12\x3\x12\x3\x12\x3\x13\x3\x13\x3\x13\x3\x13\x3"+
		"\x13\x3\x13\x3\x14\x3\x14\x3\x14\x3\x14\x3\x14\x3\x14\x3\x15\x3\x15\x3"+
		"\x15\x3\x15\x3\x16\x3\x16\x3\x16\x3\x16\x3\x16\x3\x16\x3\x17\x3\x17\x3"+
		"\x18\x3\x18\x3\x19\x3\x19\x3\x1A\x3\x1A\x3\x1B\x3\x1B\x3\x1C\x3\x1C\x3"+
		"\x1D\x3\x1D\x3\x1E\x3\x1E\x3\x1F\x3\x1F\x3 \x3 \x3 \x3!\x3!\x3!\x3\"\x3"+
		"\"\x3#\x3#\x3#\x3$\x3$\x3%\x3%\x3%\x3&\x3&\x3&\x3\'\x3\'\x3\'\x3(\x3("+
		"\x3(\x3)\x3)\x3)\x3*\x3*\x3+\x3+\x3,\x3,\x3-\x3-\x3.\x3.\x3/\x3/\x3/\a"+
		"/\x129\n/\f/\xE/\x12C\v/\x5/\x12E\n/\x3\x30\x3\x30\a\x30\x132\n\x30\f"+
		"\x30\xE\x30\x135\v\x30\x3\x31\x3\x31\x3\x31\x3\x31\x3\x31\x3\x32\x3\x32"+
		"\x5\x32\x13E\n\x32\x3\x33\x3\x33\x3\x34\x3\x34\x3\x34\x3\x34\x3\x35\x3"+
		"\x35\x3\x35\x3\x35\x2\x2\x2\x36\x4\x2\x3\x6\x2\x4\b\x2\x5\n\x2\x6\f\x2"+
		"\a\xE\x2\b\x10\x2\t\x12\x2\n\x14\x2\v\x16\x2\f\x18\x2\r\x1A\x2\xE\x1C"+
		"\x2\xF\x1E\x2\x10 \x2\x11\"\x2\x12$\x2\x13&\x2\x14(\x2\x15*\x2\x16,\x2"+
		"\x17.\x2\x18\x30\x2\x19\x32\x2\x1A\x34\x2\x1B\x36\x2\x1C\x38\x2\x1D:\x2"+
		"\x1E<\x2\x1F>\x2 @\x2!\x42\x2\"\x44\x2#\x46\x2$H\x2%J\x2&L\x2\'N\x2(P"+
		"\x2)R\x2*T\x2+V\x2,X\x2-Z\x2.\\\x2/^\x2\x30`\x2\x31\x62\x2\x33\x64\x2"+
		"\x2\x66\x2\x2h\x2\x32j\x2\x2\x4\x2\x3\a\x4\x2\v\f\xF\xF\b\x2\f\f\xF\xF"+
		"\"\"\x32;\x43\\\x63|\x6\x2\"\"\x32;\x43\\\x63|\x4\x2\x32;\x61\x61\x4\x2"+
		"\x43\\\x63|\x14B\x2\x4\x3\x2\x2\x2\x2\x6\x3\x2\x2\x2\x2\b\x3\x2\x2\x2"+
		"\x2\n\x3\x2\x2\x2\x2\f\x3\x2\x2\x2\x2\xE\x3\x2\x2\x2\x2\x10\x3\x2\x2\x2"+
		"\x2\x12\x3\x2\x2\x2\x2\x14\x3\x2\x2\x2\x2\x16\x3\x2\x2\x2\x2\x18\x3\x2"+
		"\x2\x2\x2\x1A\x3\x2\x2\x2\x2\x1C\x3\x2\x2\x2\x2\x1E\x3\x2\x2\x2\x2 \x3"+
		"\x2\x2\x2\x2\"\x3\x2\x2\x2\x2$\x3\x2\x2\x2\x2&\x3\x2\x2\x2\x2(\x3\x2\x2"+
		"\x2\x2*\x3\x2\x2\x2\x2,\x3\x2\x2\x2\x2.\x3\x2\x2\x2\x2\x30\x3\x2\x2\x2"+
		"\x2\x32\x3\x2\x2\x2\x2\x34\x3\x2\x2\x2\x2\x36\x3\x2\x2\x2\x2\x38\x3\x2"+
		"\x2\x2\x2:\x3\x2\x2\x2\x2<\x3\x2\x2\x2\x2>\x3\x2\x2\x2\x2@\x3\x2\x2\x2"+
		"\x2\x42\x3\x2\x2\x2\x2\x44\x3\x2\x2\x2\x2\x46\x3\x2\x2\x2\x2H\x3\x2\x2"+
		"\x2\x2J\x3\x2\x2\x2\x2L\x3\x2\x2\x2\x2N\x3\x2\x2\x2\x2P\x3\x2\x2\x2\x2"+
		"R\x3\x2\x2\x2\x2T\x3\x2\x2\x2\x2V\x3\x2\x2\x2\x2X\x3\x2\x2\x2\x2Z\x3\x2"+
		"\x2\x2\x2\\\x3\x2\x2\x2\x2^\x3\x2\x2\x2\x2`\x3\x2\x2\x2\x2\x62\x3\x2\x2"+
		"\x2\x3h\x3\x2\x2\x2\x3j\x3\x2\x2\x2\x4l\x3\x2\x2\x2\x6p\x3\x2\x2\x2\b"+
		"t\x3\x2\x2\x2\n\x82\x3\x2\x2\x2\f\x8D\x3\x2\x2\x2\xE\x91\x3\x2\x2\x2\x10"+
		"\x98\x3\x2\x2\x2\x12\x9E\x3\x2\x2\x2\x14\xA6\x3\x2\x2\x2\x16\xAB\x3\x2"+
		"\x2\x2\x18\xAE\x3\x2\x2\x2\x1A\xB6\x3\x2\x2\x2\x1C\xBB\x3\x2\x2\x2\x1E"+
		"\xC1\x3\x2\x2\x2 \xC5\x3\x2\x2\x2\"\xCB\x3\x2\x2\x2$\xD2\x3\x2\x2\x2&"+
		"\xD7\x3\x2\x2\x2(\xDD\x3\x2\x2\x2*\xE3\x3\x2\x2\x2,\xE7\x3\x2\x2\x2.\xED"+
		"\x3\x2\x2\x2\x30\xEF\x3\x2\x2\x2\x32\xF1\x3\x2\x2\x2\x34\xF3\x3\x2\x2"+
		"\x2\x36\xF5\x3\x2\x2\x2\x38\xF7\x3\x2\x2\x2:\xF9\x3\x2\x2\x2<\xFB\x3\x2"+
		"\x2\x2>\xFD\x3\x2\x2\x2@\xFF\x3\x2\x2\x2\x42\x102\x3\x2\x2\x2\x44\x105"+
		"\x3\x2\x2\x2\x46\x107\x3\x2\x2\x2H\x10A\x3\x2\x2\x2J\x10C\x3\x2\x2\x2"+
		"L\x10F\x3\x2\x2\x2N\x112\x3\x2\x2\x2P\x115\x3\x2\x2\x2R\x118\x3\x2\x2"+
		"\x2T\x11B\x3\x2\x2\x2V\x11D\x3\x2\x2\x2X\x11F\x3\x2\x2\x2Z\x121\x3\x2"+
		"\x2\x2\\\x123\x3\x2\x2\x2^\x12D\x3\x2\x2\x2`\x12F\x3\x2\x2\x2\x62\x136"+
		"\x3\x2\x2\x2\x64\x13D\x3\x2\x2\x2\x66\x13F\x3\x2\x2\x2h\x141\x3\x2\x2"+
		"\x2j\x145\x3\x2\x2\x2lm\a\"\x2\x2mn\x3\x2\x2\x2no\b\x2\x2\x2o\x5\x3\x2"+
		"\x2\x2pq\t\x2\x2\x2qr\x3\x2\x2\x2rs\b\x3\x2\x2s\a\x3\x2\x2\x2tu\a\x31"+
		"\x2\x2uv\a,\x2\x2vz\x3\x2\x2\x2wy\t\x3\x2\x2xw\x3\x2\x2\x2y|\x3\x2\x2"+
		"\x2zx\x3\x2\x2\x2z{\x3\x2\x2\x2{}\x3\x2\x2\x2|z\x3\x2\x2\x2}~\a,\x2\x2"+
		"~\x7F\a\x31\x2\x2\x7F\x80\x3\x2\x2\x2\x80\x81\b\x4\x2\x2\x81\t\x3\x2\x2"+
		"\x2\x82\x83\a\x31\x2\x2\x83\x84\a\x31\x2\x2\x84\x88\x3\x2\x2\x2\x85\x87"+
		"\t\x4\x2\x2\x86\x85\x3\x2\x2\x2\x87\x8A\x3\x2\x2\x2\x88\x86\x3\x2\x2\x2"+
		"\x88\x89\x3\x2\x2\x2\x89\x8B\x3\x2\x2\x2\x8A\x88\x3\x2\x2\x2\x8B\x8C\b"+
		"\x5\x2\x2\x8C\v\x3\x2\x2\x2\x8D\x8E\ak\x2\x2\x8E\x8F\ap\x2\x2\x8F\x90"+
		"\av\x2\x2\x90\r\x3\x2\x2\x2\x91\x92\au\x2\x2\x92\x93\av\x2\x2\x93\x94"+
		"\at\x2\x2\x94\x95\ak\x2\x2\x95\x96\ap\x2\x2\x96\x97\ai\x2\x2\x97\xF\x3"+
		"\x2\x2\x2\x98\x99\ah\x2\x2\x99\x9A\an\x2\x2\x9A\x9B\aq\x2\x2\x9B\x9C\a"+
		"\x63\x2\x2\x9C\x9D\av\x2\x2\x9D\x11\x3\x2\x2\x2\x9E\x9F\a\x64\x2\x2\x9F"+
		"\xA0\aq\x2\x2\xA0\xA1\aq\x2\x2\xA1\xA2\an\x2\x2\xA2\xA3\ag\x2\x2\xA3\xA4"+
		"\a\x63\x2\x2\xA4\xA5\ap\x2\x2\xA5\x13\x3\x2\x2\x2\xA6\xA7\ax\x2\x2\xA7"+
		"\xA8\aq\x2\x2\xA8\xA9\ak\x2\x2\xA9\xAA\a\x66\x2\x2\xAA\x15\x3\x2\x2\x2"+
		"\xAB\xAC\ak\x2\x2\xAC\xAD\ah\x2\x2\xAD\x17\x3\x2\x2\x2\xAE\xAF\ag\x2\x2"+
		"\xAF\xB0\an\x2\x2\xB0\xB1\au\x2\x2\xB1\xB2\ag\x2\x2\xB2\xB3\a\"\x2\x2"+
		"\xB3\xB4\ak\x2\x2\xB4\xB5\ah\x2\x2\xB5\x19\x3\x2\x2\x2\xB6\xB7\ag\x2\x2"+
		"\xB7\xB8\an\x2\x2\xB8\xB9\au\x2\x2\xB9\xBA\ag\x2\x2\xBA\x1B\x3\x2\x2\x2"+
		"\xBB\xBC\ay\x2\x2\xBC\xBD\aj\x2\x2\xBD\xBE\ak\x2\x2\xBE\xBF\an\x2\x2\xBF"+
		"\xC0\ag\x2\x2\xC0\x1D\x3\x2\x2\x2\xC1\xC2\ah\x2\x2\xC2\xC3\aq\x2\x2\xC3"+
		"\xC4\at\x2\x2\xC4\x1F\x3\x2\x2\x2\xC5\xC6\a\x64\x2\x2\xC6\xC7\at\x2\x2"+
		"\xC7\xC8\ag\x2\x2\xC8\xC9\a\x63\x2\x2\xC9\xCA\am\x2\x2\xCA!\x3\x2\x2\x2"+
		"\xCB\xCC\at\x2\x2\xCC\xCD\ag\x2\x2\xCD\xCE\av\x2\x2\xCE\xCF\aw\x2\x2\xCF"+
		"\xD0\at\x2\x2\xD0\xD1\ap\x2\x2\xD1#\x3\x2\x2\x2\xD2\xD3\at\x2\x2\xD3\xD4"+
		"\ag\x2\x2\xD4\xD5\a\x63\x2\x2\xD5\xD6\a\x66\x2\x2\xD6%\x3\x2\x2\x2\xD7"+
		"\xD8\ay\x2\x2\xD8\xD9\at\x2\x2\xD9\xDA\ak\x2\x2\xDA\xDB\av\x2\x2\xDB\xDC"+
		"\ag\x2\x2\xDC\'\x3\x2\x2\x2\xDD\xDE\a\x65\x2\x2\xDE\xDF\an\x2\x2\xDF\xE0"+
		"\a\x63\x2\x2\xE0\xE1\au\x2\x2\xE1\xE2\au\x2\x2\xE2)\x3\x2\x2\x2\xE3\xE4"+
		"\ap\x2\x2\xE4\xE5\ag\x2\x2\xE5\xE6\ay\x2\x2\xE6+\x3\x2\x2\x2\xE7\xE8\a"+
		"\x65\x2\x2\xE8\xE9\aq\x2\x2\xE9\xEA\ap\x2\x2\xEA\xEB\au\x2\x2\xEB\xEC"+
		"\av\x2\x2\xEC-\x3\x2\x2\x2\xED\xEE\a=\x2\x2\xEE/\x3\x2\x2\x2\xEF\xF0\a"+
		".\x2\x2\xF0\x31\x3\x2\x2\x2\xF1\xF2\a?\x2\x2\xF2\x33\x3\x2\x2\x2\xF3\xF4"+
		"\a*\x2\x2\xF4\x35\x3\x2\x2\x2\xF5\xF6\a+\x2\x2\xF6\x37\x3\x2\x2\x2\xF7"+
		"\xF8\a-\x2\x2\xF8\x39\x3\x2\x2\x2\xF9\xFA\a,\x2\x2\xFA;\x3\x2\x2\x2\xFB"+
		"\xFC\a\x31\x2\x2\xFC=\x3\x2\x2\x2\xFD\xFE\a\'\x2\x2\xFE?\x3\x2\x2\x2\xFF"+
		"\x100\a?\x2\x2\x100\x101\a?\x2\x2\x101\x41\x3\x2\x2\x2\x102\x103\a#\x2"+
		"\x2\x103\x104\a?\x2\x2\x104\x43\x3\x2\x2\x2\x105\x106\a>\x2\x2\x106\x45"+
		"\x3\x2\x2\x2\x107\x108\a>\x2\x2\x108\x109\a?\x2\x2\x109G\x3\x2\x2\x2\x10A"+
		"\x10B\a@\x2\x2\x10BI\x3\x2\x2\x2\x10C\x10D\a@\x2\x2\x10D\x10E\a?\x2\x2"+
		"\x10EK\x3\x2\x2\x2\x10F\x110\a~\x2\x2\x110\x111\a~\x2\x2\x111M\x3\x2\x2"+
		"\x2\x112\x113\a(\x2\x2\x113\x114\a(\x2\x2\x114O\x3\x2\x2\x2\x115\x116"+
		"\a-\x2\x2\x116\x117\a-\x2\x2\x117Q\x3\x2\x2\x2\x118\x119\a/\x2\x2\x119"+
		"\x11A\a/\x2\x2\x11AS\x3\x2\x2\x2\x11B\x11C\a\x30\x2\x2\x11CU\x3\x2\x2"+
		"\x2\x11D\x11E\a]\x2\x2\x11EW\x3\x2\x2\x2\x11F\x120\a_\x2\x2\x120Y\x3\x2"+
		"\x2\x2\x121\x122\a}\x2\x2\x122[\x3\x2\x2\x2\x123\x124\a\x7F\x2\x2\x124"+
		"]\x3\x2\x2\x2\x125\x12E\a\x32\x2\x2\x126\x12A\x4\x33;\x2\x127\x129\x4"+
		"\x32;\x2\x128\x127\x3\x2\x2\x2\x129\x12C\x3\x2\x2\x2\x12A\x128\x3\x2\x2"+
		"\x2\x12A\x12B\x3\x2\x2\x2\x12B\x12E\x3\x2\x2\x2\x12C\x12A\x3\x2\x2\x2"+
		"\x12D\x125\x3\x2\x2\x2\x12D\x126\x3\x2\x2\x2\x12E_\x3\x2\x2\x2\x12F\x133"+
		"\x5\x66\x33\x2\x130\x132\x5\x64\x32\x2\x131\x130\x3\x2\x2\x2\x132\x135"+
		"\x3\x2\x2\x2\x133\x131\x3\x2\x2\x2\x133\x134\x3\x2\x2\x2\x134\x61\x3\x2"+
		"\x2\x2\x135\x133\x3\x2\x2\x2\x136\x137\a$\x2\x2\x137\x138\x3\x2\x2\x2"+
		"\x138\x139\b\x31\x3\x2\x139\x13A\b\x31\x4\x2\x13A\x63\x3\x2\x2\x2\x13B"+
		"\x13E\x5\x66\x33\x2\x13C\x13E\t\x5\x2\x2\x13D\x13B\x3\x2\x2\x2\x13D\x13C"+
		"\x3\x2\x2\x2\x13E\x65\x3\x2\x2\x2\x13F\x140\t\x6\x2\x2\x140g\x3\x2\x2"+
		"\x2\x141\x142\a$\x2\x2\x142\x143\x3\x2\x2\x2\x143\x144\b\x34\x5\x2\x144"+
		"i\x3\x2\x2\x2\x145\x146\v\x2\x2\x2\x146\x147\x3\x2\x2\x2\x147\x148\b\x35"+
		"\x3\x2\x148k\x3\x2\x2\x2\n\x2\x3z\x88\x12A\x12D\x133\x13D\x6\x2\x3\x2"+
		"\x5\x2\x2\x4\x3\x2\x4\x2\x2";
	public static readonly ATN _ATN =
		new ATNDeserializer().Deserialize(_serializedATN.ToCharArray());
}
} // namespace AnalizadorSintactico