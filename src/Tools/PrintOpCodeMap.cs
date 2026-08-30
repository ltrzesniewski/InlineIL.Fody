#!/usr/bin/env dotnet

#:package FodyHelpers@$(FodyVersion)

using System.Reflection;

var cecilCodes = typeof(Mono.Cecil.Cil.OpCodes)
                 .GetFields(BindingFlags.Public | BindingFlags.Static)
                 .Where(field => field.IsInitOnly && field.FieldType == typeof(Mono.Cecil.Cil.OpCode))
                 .Select(field => (Mono.Cecil.Cil.OpCode)field.GetValue(null)!)
                 .ToDictionary(i => i.Value);

var reflectionEmitCodes = typeof(System.Reflection.Emit.OpCodes)
                          .GetFields(BindingFlags.Public | BindingFlags.Static)
                          .Where(field => field.IsInitOnly && field.FieldType == typeof(System.Reflection.Emit.OpCode))
                          .Select(field => (System.Reflection.Emit.OpCode)field.GetValue(null)!)
                          .ToDictionary(i => i.Value);

var values = new HashSet<short>();
values.UnionWith(cecilCodes.Keys);
values.UnionWith(reflectionEmitCodes.Keys);

foreach (var value in values.OrderBy(i => unchecked((ushort)i)))
{
    var reflectionEmitCode = reflectionEmitCodes.TryGetValue(value, out var reflectionEmitOpCode) ? reflectionEmitOpCode.Name : "???";
    var cecilCode = cecilCodes.TryGetValue(value, out var cecilOpCode) ? cecilOpCode.Name : "???";

    Console.WriteLine($"{value:X4}: {reflectionEmitCode,-15} {cecilCode,-15}");
}
