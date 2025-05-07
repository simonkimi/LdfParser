using System.Collections.Generic;

namespace LdfParser.Models;

public class LdfFrame
{
    /// <summary>
    /// 帧名称
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// 帧ID
    /// </summary>
    public ushort FrameId { get; set; }

    /// <summary>
    /// 发送节点
    /// </summary>
    public required string Sender { get; set; }

    /// <summary>
    /// 数据段字节
    /// </summary>
    public int ByteSize { get; set; }

    public required List<LdfFrameSignal> Signals { get; set; }
}

public class LdfFrameSignal
{
    /// <summary>
    /// 信号名称
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// 偏移量
    /// </summary>
    public int BitOffset { get; set; }
}