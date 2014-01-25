using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Xml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using OgmoXNAPipelineExtensions.ContentItems.Values;

namespace OgmoXNAPipelineExtensions.ContentItems
{
    public class ObjectTemplateContent
    {
        public int Height;
        public bool IsResizableX;
        public bool IsResizableY;
        public bool IsTiled;
        public string Name;
        public Vector2 Origin;
        public Rectangle Source;
        public string TextureFile;
        public ExternalReference<TextureContent> TextureReference;
        public int Width;
        public List<ValueTemplateContent> Values = new List<ValueTemplateContent>();

        public ObjectTemplateContent()
        {
        }

        public ObjectTemplateContent(XmlNode node)
        {
            if (node.Attributes["Height"] != null)
                this.Height = int.Parse(node.Attributes["Height"].Value, CultureInfo.InvariantCulture);
            if (node.Attributes["Width"] != null)
                this.Width = int.Parse(node.Attributes["Width"].Value, CultureInfo.InvariantCulture);
            if (node.Attributes["ResizableX"] != null)
                this.IsResizableX = bool.Parse(node.Attributes["ResizableX"].Value);
            if (node.Attributes["ResizableY"] != null)
                this.IsResizableY = bool.Parse(node.Attributes["ResizableY"].Value);
            if (node.Attributes["Tile"] != null)
                this.IsTiled = bool.Parse(node.Attributes["Tile"].Value);
            if (node.Attributes["Name"] != null)
                this.Name = node.Attributes["Name"].Value;
            if (node.Attributes["OriginX"] != null)
                this.Origin.X = int.Parse(node.Attributes["OriginX"].Value, CultureInfo.InvariantCulture);
            if (node.Attributes["OriginY"] != null)
                this.Origin.Y = int.Parse(node.Attributes["OriginY"].Value, CultureInfo.InvariantCulture);
            if (node.Attributes["ImageOffsetX"] != null)
                this.Source.X = int.Parse(node.Attributes["ImageOffsetX"].Value, CultureInfo.InvariantCulture);
            else
                this.Source.X = 0;
            if (node.Attributes["ImageOffsetY"] != null)
                this.Source.Y = int.Parse(node.Attributes["ImageOffsetY"].Value, CultureInfo.InvariantCulture);
            else
                this.Source.Y = 0;
            if (node.Attributes["ImageWidth"] != null)
                this.Source.Width = int.Parse(node.Attributes["ImageWidth"].Value, CultureInfo.InvariantCulture);
            else
                this.Source.Width = this.Width;
            if (node.Attributes["ImageHeight"] != null)
                this.Source.Height = int.Parse(node.Attributes["ImageHeight"].Value, CultureInfo.InvariantCulture);
            else
                this.Source.Height = this.Height;
            if (node.Attributes["Image"] != null)
                this.TextureFile = node.Attributes["Image"].Value;
            // Values
            XmlNode valuesNode = node.SelectSingleNode("Values");
            if (valuesNode != null)
            {
                foreach (XmlNode valueNode in valuesNode.SelectNodes("Boolean|Integer|Number|String|Text"))
                {
                    ValueTemplateContent valueContent = ValueContentTemplateParser.Parse(valueNode);
                    if (valueContent != null)
                        this.Values.Add(valueContent);
                }
            }
        }
    }
}
