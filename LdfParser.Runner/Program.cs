using System;
using LdfParser;

const string filePath =
    @"G:\Projects\2025_05_06_ldf_format\document\LotusSdbVDE_L5U2_V01_3_Lotus_T132A_High_CEM_CEM_LIN6_250227.ldf";
var ldfParser = LdfFileParser.FromFile(filePath);


var ldfModel = ldfParser.Parse();

Console.WriteLine(ldfModel);