using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Microsoft.Xna.Framework;
using System.Globalization;

namespace OgmoXNAPipelineExtensions.ContentItems
{
    public class NodeContent
    {
        public Vector2 Position;

        public NodeContent()
        {
        }

        public NodeContent(XmlNode node)
        {
            if (node.Attributes["X"] != null)
                this.Position.X = int.Parse(node.Attributes["X"].Value, CultureInfo.InvariantCulture);
            if (node.Attributes["Y"] != null)
                this.Position.Y = int.Parse(node.Attributes["Y"].Value, CultureInfo.InvariantCulture);
        }
    }
}
