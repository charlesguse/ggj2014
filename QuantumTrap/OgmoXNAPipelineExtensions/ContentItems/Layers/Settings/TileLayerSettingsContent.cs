using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace OgmoXNAPipelineExtensions.ContentItems.Layers.Settings
{
    public class TileLayerSettingsContent : LayerSettingsContent
    {
        public bool ExportTileIDs;
        public bool ExportTileSize;
        public bool MultipleTilesets;

        public TileLayerSettingsContent()
        {
        }

        public TileLayerSettingsContent(XmlNode node)
            : base(node)
        {
            if (node.Attributes["ExportTileIDs"] != null)
                this.ExportTileIDs = bool.Parse(node.Attributes["ExportTileIDs"].Value);
            if (node.Attributes["ExportTileSize"] != null)
                this.ExportTileSize = bool.Parse(node.Attributes["ExportTileSize"].Value);
            if (node.Attributes["MultipleTilesets"] != null)
                this.MultipleTilesets = bool.Parse(node.Attributes["MultipleTilesets"].Value);
        }
    }
}
