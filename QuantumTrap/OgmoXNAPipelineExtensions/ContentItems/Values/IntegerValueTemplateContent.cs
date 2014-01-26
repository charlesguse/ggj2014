using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Xml;

namespace OgmoXNAPipelineExtensions.ContentItems.Values
{
    public class IntegerValueTemplateContent : ValueTemplateContent<int>
    {
        public int Max;
        public int Min;

        public IntegerValueTemplateContent()
        {
        }

        public IntegerValueTemplateContent(XmlNode node)
            : base(node)
        {
            if (node.Attributes["Default"] != null)
                this.Default = int.Parse(node.Attributes["Default"].Value, CultureInfo.InvariantCulture);
            if (node.Attributes["Max"] != null)
                this.Max = int.Parse(node.Attributes["Max"].Value, CultureInfo.InvariantCulture);
            if (node.Attributes["Min"] != null)
                this.Min = int.Parse(node.Attributes["Min"].Value, CultureInfo.InvariantCulture);
        }
    }
}
