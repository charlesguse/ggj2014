using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace OgmoXNAPipelineExtensions.ContentItems.Values
{
    static class ValueContentTemplateParser
    {
        internal static ValueTemplateContent Parse(XmlNode node)
        {
            ValueTemplateContent valueContent = null;
            switch (node.Name)
            {
                case "Boolean":
                    valueContent = new BooleanValueTemplateContent(node);
                    break;
                case "Integer":
                    valueContent = new IntegerValueTemplateContent(node);
                    break;
                case "Number":
                    valueContent = new NumberValueTemplateContent(node);
                    break;
                case "String":
                // Fallthrough.
                case "Text":
                    valueContent = new StringValueTemplateContent(node);
                    break;
            }
            return valueContent;
        }
    }
}
