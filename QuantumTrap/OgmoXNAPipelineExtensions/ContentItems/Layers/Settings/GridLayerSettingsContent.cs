using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace OgmoXNAPipelineExtensions.ContentItems.Layers.Settings
{
    public class GridLayerSettingsContent : LayerSettingsContent
    {
        public bool ExportAsObjects;
        public string NewLine;

        public GridLayerSettingsContent()
        {
        }

        public GridLayerSettingsContent(XmlNode node)
            : base(node)
        {
            if (node.Attributes["ExportAsObjects"] != null)
                this.ExportAsObjects = bool.Parse(node.Attributes["ExportAsObjects"].Value);
            if (node.Attributes["NewLine"] != null)
                this.NewLine = node.Attributes["NewLine"].Value;
            this.NewLine = this.NewLine ?? "\n";                
        }
    }
}
