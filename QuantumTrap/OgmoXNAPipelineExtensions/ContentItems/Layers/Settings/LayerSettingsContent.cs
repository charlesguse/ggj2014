using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Xml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OgmoXNAPipelineExtensions.ContentItems.Layers.Settings
{
    public abstract class LayerSettingsContent
    {
        public Color GridColor;
        public int GridDrawSize;
        public int GridSize;
        public string Name;

        public LayerSettingsContent()
        {
        }

        public LayerSettingsContent(XmlNode node)
        {
            if (node.Attributes["GridColor"] != null)
                this.GridColor = ColorHelper.FromHex(node.Attributes["GridColor"].Value);
            if (node.Attributes["DrawGridSize"] != null)
                this.GridDrawSize = int.Parse(node.Attributes["DrawGridSize"].Value, CultureInfo.InvariantCulture);
            if (node.Attributes["GridSize"] != null)
                this.GridSize = int.Parse(node.Attributes["GridSize"].Value, CultureInfo.InvariantCulture);
            if (node.Attributes["Name"] != null)
                this.Name = node.Attributes["Name"].Value;
        }
    }
}
