using System.Collections.Generic;
using System.Linq;
using Fody;
using InlineIL.Fody.Extensions;
using InlineIL.Fody.Support;
using Mono.Cecil.Cil;

namespace InlineIL.Fody.Processing
{
    internal class LabelMapper
    {
        private readonly WeaverILProcessor _il;
        private readonly ILogger _log;
        private readonly Dictionary<string, LabelInfo> _labels = new Dictionary<string, LabelInfo>();

        public LabelMapper(WeaverILProcessor il, ILogger log)
        {
            _il = il;
            _log = log;
        }

        public Instruction MarkLabel(string labelName, SequencePoint? sequencePoint)
        {
            var labelInfo = _labels.GetOrAddNew(labelName);

            if (labelInfo.IsDefined)
                throw new WeavingException($"Label '{labelName}' is already defined");

            labelInfo.SequencePoint = sequencePoint;
            return labelInfo.PlaceholderTarget;
        }

        public Instruction CreateBranchInstruction(OpCode opCode, string labelName)
        {
            var labelInfo = _labels.GetOrAddNew(labelName);

            var resultInstruction = _il.Create(opCode, labelInfo.PlaceholderTarget);
            labelInfo.References.Add(resultInstruction);

            return resultInstruction;
        }

        public Instruction CreateSwitchInstruction(IEnumerable<string> labelNames)
        {
            var labelInfos = labelNames.Select(i => _labels.GetOrAddNew(i)).ToList();
            var resultInstruction = _il.Create(OpCodes.Switch, labelInfos.Select(i => i.PlaceholderTarget).ToArray());

            foreach (var info in labelInfos)
                info.References.Add(resultInstruction);

            return resultInstruction;
        }

        public void PostProcess()
        {
            foreach (var (name, info) in _labels)
            {
                if (!info.IsDefined)
                    throw new InstructionWeavingException(info.References.FirstOrDefault(), $"Undefined label: '{name}'");

                if (info.References.Count == 0)
                    _log.Warning($"Unused label: '{name}'", info.SequencePoint);

                _il.Remove(info.PlaceholderTarget);
            }
        }

        private class LabelInfo
        {
            public Instruction PlaceholderTarget { get; } = Instruction.Create(OpCodes.Nop);
            public ICollection<Instruction> References { get; } = new List<Instruction>();
            public SequencePoint? SequencePoint { get; set; }

            public bool IsDefined => PlaceholderTarget.Next != null;
        }
    }
}
