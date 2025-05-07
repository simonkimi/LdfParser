using System;
using System.Collections.Generic;

namespace LdfParser.Models;

[Serializable]
public class LdfFile
{
    public string ProtocolVersion { get; set; }
    public string LanguageVersion { get; set; }
    public string Speed { get; set; }
    public List<LdfNode> MasterNodes { get; set; } = [];
    public List<LdfNode> SlaveNodes { get; set; } = [];
    public List<LdfNodeAttribute> NodeAttributes { get; set; } = [];
    public List<LdfSignal> Signals { get; set; } = [];
    public List<LdfFrame> Frames { get; set; } = [];
    public List<LdfSignalEncodingType> SignalEncodingTypes { get; set; } = [];

    public List<LdfSignalRepresentation> SignalRepresentations { get; set; } = [];
}