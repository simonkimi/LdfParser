using System;
using System.Collections.Generic;

namespace LdfParser.Models;

[Serializable]
public class LdfSignal
{
    public required string Name { get; set; }

    public int SizeBit { get; set; }

    public int DefaultValue { get; set; }

    public required string Senders { get; set; }

    public required List<string> Receiver { get; set; }
}

[Serializable]
public class LdfSignalEncodingType
{
    public required string Name { get; set; }
    public List<LdfSignalEncodingValue> Values { get; set; } = [];
}

[Serializable]
public abstract class LdfSignalEncodingValue;

[Serializable]
public class LdfSignalEncodingLogicalValue : LdfSignalEncodingValue
{
    public int Value { get; set; }
    public required string Description { get; set; }
}

[Serializable]
public class LdfSignalEncodingPhysicalValue : LdfSignalEncodingValue
{
    public int MinValue { get; set; }
    public int MaxValue { get; set; }
    public double Factor { get; set; }
    public double Offset { get; set; }
    public required string Unit { get; set; }
}

[Serializable]
public class LdfSignalRepresentation
{
    public required string Name { get; set; }
    public required List<string> Signals { get; set; }
}