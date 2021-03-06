//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.6.4
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from ..\..\..\AtlusScriptLib\MessageScriptLanguage\Compiler\Parser\MessageScriptLexer.g4 by ANTLR 4.6.4

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

namespace AtlusScriptLibrary.FlowScriptLanguage.Compiler.Parser {
using Antlr4.Runtime;
using Antlr4.Runtime.Atn;
using Antlr4.Runtime.Misc;
using DFA = Antlr4.Runtime.Dfa.DFA;

[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.6.4")]
[System.CLSCompliant(false)]
public partial class MessageScriptLexer : Lexer {
	public const int
		OpenCode=1, CloseText=2, Text=3, MessageDialogTagId=4, SelectionDialogTagId=5, 
		CloseCode=6, OpenText=7, IntLiteral=8, Identifier=9, Whitespace=10;
	public const int MessageScriptCode = 1;
	public static string[] modeNames = {
		"DEFAULT_MODE", "MessageScriptCode"
	};

	public static readonly string[] ruleNames = {
		"OpenCode", "CloseText", "Text", "MessageDialogTagId", "SelectionDialogTagId", 
		"CloseCode", "OpenText", "IntLiteral", "Identifier", "DecIntLiteral", 
		"HexIntLiteral", "Letter", "Digit", "HexDigit", "HexLiteralPrefix", "Sign", 
		"Whitespace"
	};


	public MessageScriptLexer(ICharStream input)
		: base(input)
	{
		_interp = new LexerATNSimulator(this,_ATN);
	}

	private static readonly string[] _LiteralNames = {
		null, null, null, null, null, "'sel'"
	};
	private static readonly string[] _SymbolicNames = {
		null, "OpenCode", "CloseText", "Text", "MessageDialogTagId", "SelectionDialogTagId", 
		"CloseCode", "OpenText", "IntLiteral", "Identifier", "Whitespace"
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

	[System.Obsolete("Use IRecognizer.Vocabulary instead.")]
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

	public override string GrammarFileName { get { return "MessageScriptLexer.g4"; } }

	public override string[] RuleNames { get { return ruleNames; } }

	public override string[] ModeNames { get { return modeNames; } }

	public override string SerializedAtn { get { return _serializedATN; } }

	public static readonly string _serializedATN =
		"\x3\xAF6F\x8320\x479D\xB75C\x4880\x1605\x191C\xAB37\x2\ft\b\x1\b\x1\x4"+
		"\x2\t\x2\x4\x3\t\x3\x4\x4\t\x4\x4\x5\t\x5\x4\x6\t\x6\x4\a\t\a\x4\b\t\b"+
		"\x4\t\t\t\x4\n\t\n\x4\v\t\v\x4\f\t\f\x4\r\t\r\x4\xE\t\xE\x4\xF\t\xF\x4"+
		"\x10\t\x10\x4\x11\t\x11\x4\x12\t\x12\x3\x2\x3\x2\x3\x2\x3\x2\x3\x3\x3"+
		"\x3\x3\x3\x3\x3\x3\x4\x6\x4\x30\n\x4\r\x4\xE\x4\x31\x3\x5\x3\x5\x3\x5"+
		"\x3\x5\x3\x5\x3\x5\x5\x5:\n\x5\x3\x6\x3\x6\x3\x6\x3\x6\x3\a\x3\a\x3\a"+
		"\x3\a\x3\b\x3\b\x3\b\x3\b\x3\t\x3\t\x5\tJ\n\t\x3\n\x3\n\x3\n\x6\nO\n\n"+
		"\r\n\xE\nP\x3\v\x5\vT\n\v\x3\v\x6\vW\n\v\r\v\xE\vX\x3\f\x5\f\\\n\f\x3"+
		"\f\x3\f\x6\f`\n\f\r\f\xE\f\x61\x3\r\x3\r\x3\xE\x3\xE\x3\xF\x3\xF\x5\xF"+
		"j\n\xF\x3\x10\x3\x10\x3\x10\x3\x11\x3\x11\x3\x12\x3\x12\x3\x12\x3\x12"+
		"\x2\x2\x2\x13\x4\x2\x3\x6\x2\x4\b\x2\x5\n\x2\x6\f\x2\a\xE\x2\b\x10\x2"+
		"\t\x12\x2\n\x14\x2\v\x16\x2\x2\x18\x2\x2\x1A\x2\x2\x1C\x2\x2\x1E\x2\x2"+
		" \x2\x2\"\x2\x2$\x2\f\x4\x2\x3\t\x4\x2]]__\x4\x2\x43\\\x63|\x3\x2\x32"+
		";\x4\x2\x43H\x63h\x4\x2ZZzz\x4\x2--//\x5\x2\v\f\xF\xF\"\"v\x2\x4\x3\x2"+
		"\x2\x2\x2\x6\x3\x2\x2\x2\x2\b\x3\x2\x2\x2\x3\n\x3\x2\x2\x2\x3\f\x3\x2"+
		"\x2\x2\x3\xE\x3\x2\x2\x2\x3\x10\x3\x2\x2\x2\x3\x12\x3\x2\x2\x2\x3\x14"+
		"\x3\x2\x2\x2\x3$\x3\x2\x2\x2\x4&\x3\x2\x2\x2\x6*\x3\x2\x2\x2\b/\x3\x2"+
		"\x2\x2\n\x39\x3\x2\x2\x2\f;\x3\x2\x2\x2\xE?\x3\x2\x2\x2\x10\x43\x3\x2"+
		"\x2\x2\x12I\x3\x2\x2\x2\x14N\x3\x2\x2\x2\x16S\x3\x2\x2\x2\x18[\x3\x2\x2"+
		"\x2\x1A\x63\x3\x2\x2\x2\x1C\x65\x3\x2\x2\x2\x1Ei\x3\x2\x2\x2 k\x3\x2\x2"+
		"\x2\"n\x3\x2\x2\x2$p\x3\x2\x2\x2&\'\a]\x2\x2\'(\x3\x2\x2\x2()\b\x2\x2"+
		"\x2)\x5\x3\x2\x2\x2*+\a_\x2\x2+,\x3\x2\x2\x2,-\b\x3\x2\x2-\a\x3\x2\x2"+
		"\x2.\x30\n\x2\x2\x2/.\x3\x2\x2\x2\x30\x31\x3\x2\x2\x2\x31/\x3\x2\x2\x2"+
		"\x31\x32\x3\x2\x2\x2\x32\t\x3\x2\x2\x2\x33\x34\ao\x2\x2\x34\x35\au\x2"+
		"\x2\x35:\ai\x2\x2\x36\x37\a\x66\x2\x2\x37\x38\an\x2\x2\x38:\ai\x2\x2\x39"+
		"\x33\x3\x2\x2\x2\x39\x36\x3\x2\x2\x2:\v\x3\x2\x2\x2;<\au\x2\x2<=\ag\x2"+
		"\x2=>\an\x2\x2>\r\x3\x2\x2\x2?@\a_\x2\x2@\x41\x3\x2\x2\x2\x41\x42\b\a"+
		"\x3\x2\x42\xF\x3\x2\x2\x2\x43\x44\a]\x2\x2\x44\x45\x3\x2\x2\x2\x45\x46"+
		"\b\b\x3\x2\x46\x11\x3\x2\x2\x2GJ\x5\x16\v\x2HJ\x5\x18\f\x2IG\x3\x2\x2"+
		"\x2IH\x3\x2\x2\x2J\x13\x3\x2\x2\x2KO\x5\x1A\r\x2LO\x5\x1C\xE\x2MO\a\x61"+
		"\x2\x2NK\x3\x2\x2\x2NL\x3\x2\x2\x2NM\x3\x2\x2\x2OP\x3\x2\x2\x2PN\x3\x2"+
		"\x2\x2PQ\x3\x2\x2\x2Q\x15\x3\x2\x2\x2RT\x5\"\x11\x2SR\x3\x2\x2\x2ST\x3"+
		"\x2\x2\x2TV\x3\x2\x2\x2UW\x5\x1C\xE\x2VU\x3\x2\x2\x2WX\x3\x2\x2\x2XV\x3"+
		"\x2\x2\x2XY\x3\x2\x2\x2Y\x17\x3\x2\x2\x2Z\\\x5\"\x11\x2[Z\x3\x2\x2\x2"+
		"[\\\x3\x2\x2\x2\\]\x3\x2\x2\x2]_\x5 \x10\x2^`\x5\x1E\xF\x2_^\x3\x2\x2"+
		"\x2`\x61\x3\x2\x2\x2\x61_\x3\x2\x2\x2\x61\x62\x3\x2\x2\x2\x62\x19\x3\x2"+
		"\x2\x2\x63\x64\t\x3\x2\x2\x64\x1B\x3\x2\x2\x2\x65\x66\t\x4\x2\x2\x66\x1D"+
		"\x3\x2\x2\x2gj\x5\x1C\xE\x2hj\t\x5\x2\x2ig\x3\x2\x2\x2ih\x3\x2\x2\x2j"+
		"\x1F\x3\x2\x2\x2kl\a\x32\x2\x2lm\t\x6\x2\x2m!\x3\x2\x2\x2no\t\a\x2\x2"+
		"o#\x3\x2\x2\x2pq\t\b\x2\x2qr\x3\x2\x2\x2rs\b\x12\x4\x2s%\x3\x2\x2\x2\xE"+
		"\x2\x3\x31\x39INPSX[\x61i\x5\a\x3\x2\x6\x2\x2\b\x2\x2";
	public static readonly ATN _ATN =
		new ATNDeserializer().Deserialize(_serializedATN.ToCharArray());
}
} // namespace AtlusScriptLib.FlowScriptLanguage.Compiler.Parser
