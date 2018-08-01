using System;
using System.Xml.Linq;
using Fody;
using JetBrains.Annotations;

namespace InlineIL.Fody.Support
{
    internal class WeaverConfigOptions
    {
        public SequencePointsBehavior SequencePoints { get; private set; } = SequencePointsBehavior.Debug;

        public WeaverConfigOptions([CanBeNull] XElement element)
        {
            if (element != null)
                LoadFrom(element);
        }

        private void LoadFrom(XElement config)
        {
            foreach (var attribute in config.Attributes())
            {
                var attributeName = attribute.Name.LocalName;

                switch (attributeName)
                {
                    case nameof(SequencePoints):
                        SequencePoints = ParseEnum<SequencePointsBehavior>(attribute);
                        break;

                    default:
                        throw new WeavingException($"Unknown configuration attribute: {attributeName}");
                }
            }

            foreach (var element in config.Elements())
            {
                throw new WeavingException($"Unknown configuration element: {element.Name.LocalName}");
            }
        }

        private static T ParseEnum<T>(XAttribute attribute)
            where T : struct, Enum
        {
            if (Enum.TryParse<T>(attribute.Value, true, out var result))
                return result;

            throw new WeavingException($"Invalid value '{attribute.Value}' for configuration attribute {attribute.Name.LocalName}. Valid values are: {string.Join(", ", Enum.GetNames(typeof(T)))}");
        }

        public enum SequencePointsBehavior
        {
            False,
            True,
            Debug,
            Release
        }
    }
}
