using System.Linq;
using LdfParser.Models;
using LdfParser.Utils;

namespace LdfParser.Grammars;

public class LdfGrammarVisitor : LdfBaseVisitor<LdfFile>
{
    private readonly LdfFile _ldfFile = new();

    private LdfNodeAttribute? _currentAttribute;
    private LdfSignalEncodingType? _currentEncodingType;

    public override LdfFile VisitStart(LdfParser.StartContext context)
    {
        base.VisitStart(context);
        return _ldfFile;
    }

    public override LdfFile VisitHeader_protocol_version(LdfParser.Header_protocol_versionContext context)
    {
        _ldfFile.ProtocolVersion = context.ldf_version().GetText().Trim('"');
        return _ldfFile;
    }

    public override LdfFile VisitHeader_language_version(LdfParser.Header_language_versionContext context)
    {
        _ldfFile.LanguageVersion = context.ldf_version().GetText().Trim('"');
        return _ldfFile;
    }

    public override LdfFile VisitHeader_speed(LdfParser.Header_speedContext context)
    {
        _ldfFile.Speed = context.ldf_float().GetText();
        return _ldfFile;
    }

    public override LdfFile VisitNodes(LdfParser.NodesContext context)
    {
        {
            var master = context.node_master();
            var node = new LdfNode
            {
                Name = master.ldf_name().GetText()
            };
            _ldfFile.MasterNodes.Add(node);
        }
        var slaves = context.node_slaves();
        foreach (var ldfNameContext in slaves.ldf_name())
        {
            var node = new LdfNode
            {
                Name = ldfNameContext.GetText()
            };
            _ldfFile.SlaveNodes.Add(node);
        }

        return _ldfFile;
    }

    public override LdfFile VisitSignal_definition(LdfParser.Signal_definitionContext context)
    {
        var signal = new LdfSignal
        {
            Name = context.ldf_name(0).GetText(),
            SizeBit = int.Parse(context.ldf_int(0).GetText()),
            DefaultValue = int.Parse(context.ldf_int(1).GetText()),
            Senders = context.ldf_name(1).GetText(),
            Receiver = context.ldf_name().Skip(2).Select(x => x.GetText()).ToList()
        };
        _ldfFile.Signals.Add(signal);
        return _ldfFile;
    }

    public override LdfFile VisitFrame_definition(LdfParser.Frame_definitionContext context)
    {
        var frame = new LdfFrame
        {
            Name = context.ldf_name(0).GetText(),
            FrameId = (ushort)context.ldf_int(0).GetLdfNumber(),
            Sender = context.ldf_name(1).GetText(),
            ByteSize = (ushort)context.ldf_int(1).GetLdfNumber(),
            Signals = context.frame_signal().Select(signalContext => new LdfFrameSignal
            {
                Name = signalContext.ldf_name().GetText(),
                BitOffset = (int)signalContext.ldf_int().GetLdfNumber()
            }).OrderBy(x => x.BitOffset).ToList()
        };
        _ldfFile.Frames.Add(frame);
        return _ldfFile;
    }

    public override LdfFile VisitNode_attribute(LdfParser.Node_attributeContext context)
    {
        _currentAttribute = new LdfNodeAttribute
        {
            Name = context.ldf_name().GetText()
        };

        foreach (var nodeDefinitionContext in context.node_definition()) Visit(nodeDefinitionContext);

        _ldfFile.NodeAttributes.Add(_currentAttribute);
        _currentAttribute = null;
        return _ldfFile;
    }


    public override LdfFile VisitNode_definition_configured_nad(
        LdfParser.Node_definition_configured_nadContext context)
    {
        ThrowHelper.IfNull(_currentAttribute);
        _currentAttribute!.Nad = (byte)context.ldf_int().GetLdfNumber();
        return base.VisitNode_definition_configured_nad(context);
    }

    public override LdfFile VisitNode_definition_initial_nad(
        LdfParser.Node_definition_initial_nadContext context)
    {
        ThrowHelper.IfNull(_currentAttribute);
        _currentAttribute!.Nad = (byte)context.ldf_int().GetLdfNumber();
        return base.VisitNode_definition_initial_nad(context);
    }

    public override LdfFile VisitNode_definition_product_id(
        LdfParser.Node_definition_product_idContext context)
    {
        ThrowHelper.IfNull(_currentAttribute);
        _currentAttribute!.SupplierId = (ushort)context.ldf_int(0).GetLdfNumber();
        _currentAttribute.FunctionId = (ushort)context.ldf_int(1).GetLdfNumber();
        _currentAttribute.VariantId = (byte)context.ldf_int(2).GetLdfNumber();
        return base.VisitNode_definition_product_id(context);
    }


    public override LdfFile VisitNode_definition_configurable_frame(
        LdfParser.Node_definition_configurable_frameContext context)
    {
        ThrowHelper.IfNull(_currentAttribute);
        _currentAttribute!.ConfigurableFrame.Add(context.ldf_name().GetText());
        return base.VisitNode_definition_configurable_frame(context);
    }

    public override LdfFile VisitSignal_encoding_type(LdfParser.Signal_encoding_typeContext context)
    {
        _currentEncodingType = new LdfSignalEncodingType
        {
            Name = context.ldf_name().GetText()
        };
        foreach (var signalEncodingTypeValueContext in context.signal_encoding_type_value())
            Visit(signalEncodingTypeValueContext);

        _ldfFile.SignalEncodingTypes.Add(_currentEncodingType);
        _currentEncodingType = null;

        return _ldfFile;
    }

    public override LdfFile VisitSignal_encoding_logical_value(
        LdfParser.Signal_encoding_logical_valueContext context)
    {
        ThrowHelper.IfNull(_currentEncodingType);
        _currentEncodingType!.Values.Add(new LdfSignalEncodingLogicalValue
        {
            Value = (int)context.ldf_int().GetLdfNumber(),
            Description = context.ldf_str().GetText()
        });

        return _ldfFile;
    }

    public override LdfFile VisitSignal_encoding_physical_value(
        LdfParser.Signal_encoding_physical_valueContext context)
    {
        ThrowHelper.IfNull(_currentEncodingType);
        _currentEncodingType!.Values.Add(new LdfSignalEncodingPhysicalValue
        {
            MinValue = (int)context.ldf_int(0).GetLdfNumber(),
            MaxValue = (int)context.ldf_int(1).GetLdfNumber(),
            Factor = context.ldf_float(0).GetFloat(),
            Offset = context.ldf_float(1).GetFloat(),
            Unit = context.ldf_str().GetText().Trim('"')
        });
        return _ldfFile;
    }

    public override LdfFile VisitSignal_representation_definition(
        LdfParser.Signal_representation_definitionContext context)
    {
        var signalRepresentation = new LdfSignalRepresentation
        {
            Name = context.ldf_name(0).GetText(),
            Signals = [context.ldf_name(1).GetText(), ..context.ldf_name().Skip(2).Select(x => x.GetText())]
        };

        _ldfFile.SignalRepresentations.Add(signalRepresentation);

        return base.VisitSignal_representation_definition(context);
    }
}