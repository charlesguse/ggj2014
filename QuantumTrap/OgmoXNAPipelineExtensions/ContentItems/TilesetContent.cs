using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline;
using System.Xml;
using System.Globalization;

namespace OgmoXNAPipelineExtensions.ContentItems
{
    public class TilesetContent
    {
        public string Name;
        public string TextureFile;
        public ExternalReference<TextureContent> TextureReference;
        public int TileHeight;
        public int TileWidth;

        public TilesetContent()
        {
        }

        public TilesetContent(XmlNode node)
        {
            if (node.Attributes["Name"] != null)
                this.Name = node.Attributes["Name"].Value;
            if (node.Attributes["Image"] != null)
                this.TextureFile = node.Attributes["Image"].Value;
            if (node.Attributes["TileHeight"] != null)
                this.TileHeight = int.Parse(node.Attributes["TileHeight"].Value, CultureInfo.InvariantCulture);
            if (node.Attributes["TileWidth"] != null)
                this.TileWidth = int.Parse(node.Attributes["TileWidth"].Value, CultureInfo.InvariantCulture);
        }
    }
}
