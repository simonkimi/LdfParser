using System;
using LdfParser;

const string filePath =
    @"H:\Projects\2025_05_06_ldf_format\document\743fd8bf6537a85a97e5421ed22ec0c5.ldf";
var ldfParser = LdfFileParser.FromFile(filePath);


var ldfModel = ldfParser.Parse();

Console.WriteLine(ldfModel);