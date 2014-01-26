using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Xml;
using Microsoft.Xna.Framework;
using OgmoXNAPipelineExtensions.ContentItems.Layers.Settings;

namespace OgmoXNAPipelineExtensions.ContentItems.Layers
{
    public class TileContent
    {
        public int Height;
        public Vector2 Position = Vector2.Zero;
        public Vector2 TextureOffset;
        public int SourceIndex;
        public string TilesetName;
        public int Width;

        public TileContent()
        {
        }

        public TileContent(XmlNode node, TileLayerContent layer, TileLayerSettingsContent settings)
        {
            if (!settings.MultipleTilesets && !settings.ExportTileSize)
            {
                this.Height = layer.TileHeight;
                this.Width = layer.TileWidth;
            }
            else if(settings.MultipleTilesets || settings.ExportTileSize) 
            {
                if (node.Attributes["Th"] != null)
                    this.Height = int.Parse(node.Attributes["Th"].Value, CultureInfo.InvariantCulture);
                if (node.Attributes["Tw"] != null)
                    this.Width = int.Parse(node.Attributes["Tw"].Value, CultureInfo.InvariantCulture);
            }
            if (node.Attributes["X"] != null)
                this.Position.X = int.Parse(node.Attributes["X"].Value, CultureInfo.InvariantCulture);
            if (node.Attributes["Y"] != null)
                this.Position.Y = int.Parse(node.Attributes["Y"].Value, CultureInfo.InvariantCulture);
            if (node.Attributes["Tx"] != null)
                this.TextureOffset.X = int.Parse(node.Attributes["Tx"].Value, CultureInfo.InvariantCulture);
            if (node.Attributes["Ty"] != null)
                this.TextureOffset.Y = int.Parse(node.Attributes["Ty"].Value, CultureInfo.InvariantCulture);
            if (node.Attributes["Id"] != null)
                this.SourceIndex = int.Parse(node.Attributes["Id"].Value, CultureInfo.InvariantCulture);
            if (settings.MultipleTilesets)
            {
                if (node.Attributes["Set"] != null)
                    this.TilesetName = node.Attributes["Set"].Value;
            }
            else
                this.TilesetName = layer.Tilesets[0];
        }
    }
}
