using Antlr4.Runtime;
using LdfParser.Grammars;

var stream =
    new AntlrFileStream(
        @"G:\Projects\2025_05_06_ldf_format\document\LotusSdbVDE_L5U2_V01_3_Lotus_T132A_High_CEM_CEM_LIN6_250227.ldf");
var lexer = new LdfLexer(stream);
var tokens = new CommonTokenStream(lexer);
var parser = new LdfParser.Grammars.LdfParser(tokens);

var result = parser.start();