using System;
using System.Collections.Generic;

namespace LdfParser.Models;

[Serializable]
public class LdfNode
{
    /// <summary>
    ///     主机节点
    /// </summary>
    public required string Name { get; set; }
}

[Serializable]
public class LdfNodeAttribute
{
    /// <summary>
    ///     节点名称
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    ///     Nad值
    /// </summary>
    public byte Nad { get; set; }

    /// <summary>
    ///     供应商ID
    /// </summary>
    public ushort SupplierId { get; set; }

    /// <summary>
    ///     功能ID
    /// </summary>
    public ushort FunctionId { get; set; }

    /// <summary>
    ///     可变Id
    /// </summary>
    public byte VariantId { get; set; }

    /// <summary>
    ///     节点名称
    /// </summary>
    public List<string> ConfigurableFrame { get; set; } = [];
}