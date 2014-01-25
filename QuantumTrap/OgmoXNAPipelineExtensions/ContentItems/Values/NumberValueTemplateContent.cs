using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Xml;

namespace OgmoXNAPipelineExtensions.ContentItems.Values
{
    public class NumberValueTemplateContent : ValueTemplateContent<float>
    {
        public float Max;
        public float Min;

        public NumberValueTemplateContent()
        {
        }

        public NumberValueTemplateContent(XmlNode node)
            : base(node)
        {
            if (node.Attributes["Default"] != null)
                this.Default = float.Parse(node.Attributes["Default"].Value, CultureInfo.InvariantCulture);
            if (node.Attributes["Max"] != null)
                this.Max = float.Parse(node.Attributes["Max"].Value, CultureInfo.InvariantCulture);
            if (node.Attributes["Min"] != null)
                this.Min = float.Parse(node.Attributes["Min"].Value, CultureInfo.InvariantCulture);
        }
    }
}
