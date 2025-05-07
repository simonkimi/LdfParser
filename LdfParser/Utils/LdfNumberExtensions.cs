using System;
using Antlr4.Runtime;

namespace LdfParser.Utils;

public static class LdfNumberExtensions
{
    public static long GetLdfNumber(this Grammars.LdfParser.Ldf_intContext input)
    {
        var inputText = input.GetText();
        return inputText.StartsWith("0x", StringComparison.OrdinalIgnoreCase)
            ? Convert.ToInt64(inputText, 16)
            : Convert.ToInt64(inputText);
    }

    public static double GetFloat(this Grammars.LdfParser.Ldf_floatContext input)
    {
        return Convert.ToDouble(input.GetText());
    }
}