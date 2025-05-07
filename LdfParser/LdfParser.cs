using Antlr4.Runtime;
using LdfParser.Grammars;
using LdfParser.Models;

namespace LdfParser;

public class LdfFileParser(ICharStream stream)
{
    public LdfFile Parse()
    {
        var lexer = new LdfLexer(stream);
        var tokens = new CommonTokenStream(lexer);
        var parser = new LdfParser.Grammars.LdfParser(tokens);
        var result = parser.start();
        var visitor = new LdfGrammarVisitor();
        return visitor.Visit(result);
    }

    public static LdfFileParser FromFile(string filePath)
    {
        return new LdfFileParser(new AntlrFileStream(filePath));
    }

    public static LdfFileParser FromString(string input)
    {
        return new LdfFileParser(new AntlrInputStream(input));
    }
}