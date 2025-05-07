using System.Collections.Generic;

namespace LdfParser.Models;

public class LdfSignal
{
    public string Name { get; set; }

    public int StartBit { get; set; }

    public int DefaultValue { get; set; }

    public string Senders { get; set; }

    public List<string> Receiver { get; set; }
}

public class LdfSignalEncodingType
{
    public string Name { get; set; }
    public List<LdfSignalEncodingValue> Values { get; set; } = [];
}

public abstract class LdfSignalEncodingValue;

public class LdfSignalEncodingLogicalValue : LdfSignalEncodingValue
{
    public int Value { get; set; }
    public string Description { get; set; }
}

public class LdfSignalEncodingPhysicalValue : LdfSignalEncodingValue
{
    public int MinValue { get; set; }
    public int MaxValue { get; set; }
    public double Factor { get; set; }
    public double Offset { get; set; }
    public string Unit { get; set; }
}

public class LdfSignalRepresentation
{
    public string Name { get; set; }
    public List<string> Signals { get; set; }
}