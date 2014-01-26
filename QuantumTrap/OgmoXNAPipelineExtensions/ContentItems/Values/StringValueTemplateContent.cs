using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Xml;

namespace OgmoXNAPipelineExtensions.ContentItems.Values
{
    public class StringValueTemplateContent : ValueTemplateContent<string>
    {
        public int MaxChars;

        public StringValueTemplateContent()
        {
        }

        public StringValueTemplateContent(XmlNode node)
            : base(node)
        {
            if (node.Attributes["Default"] != null)
                this.Default = node.Attributes["Default"].Value;
            if (node.Attributes["MaxChars"] != null)
                this.MaxChars = int.Parse(node.Attributes["MaxChars"].Value, CultureInfo.InvariantCulture);
        }
    }
}
