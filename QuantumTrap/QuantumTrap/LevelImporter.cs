using QuantumTrap.GameLogic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace QuantumTrap
{
    public class LevelImporter
    {
        public Level Import(string levelName)
        {
            var filePath = BuildFileNameFromLevelName(levelName);
            var levelXml = LoadLevelXml(filePath);

            var level = BuildLevelFromXml(levelXml);

            return level;
        }

        private Level BuildLevelFromXml(XmlDocument levelXml)
        {
            var level = new Level();

            level.PlayerStart = GetObjectPosition(levelXml, "Player");
            level.ShadowStart = GetObjectPosition(levelXml, "Shadow");
            LoadTiles(level, levelXml, "RedTiles", TileType.Red);
            LoadTiles(level, levelXml, "BlueTiles", TileType.Blue);
            LoadTiles(level, levelXml, "GreenTiles", TileType.Green);
            LoadTiles(level, levelXml, "YellowTiles", TileType.Yellow);
            LoadTiles(level, levelXml, "BlackTiles", TileType.Black);

            return level;
        }

        private Position2 GetObjectPosition(XmlDocument levelXml, string objectNodeName)
        {
            var objectNode = levelXml.SelectSingleNode("//" + objectNodeName);
            var x = int.Parse(objectNode.Attributes["x"].Value) / 64;
            var y = int.Parse(objectNode.Attributes["y"].Value) / 64;

            return new Position2() { X = x, Y = y };
        }

        private void LoadTiles(Level level, XmlDocument levelXml, string tileNodeName, TileType tileType)
        {
            var tileNode = levelXml.SelectSingleNode("//" + tileNodeName);

            if (tileNode != null)
            {
                var bitString = tileNode.InnerText;

                for (int y = 0; y < Constants.LEVEL_HEIGHT; y++)
                {
                    for (int x = 0; x < Constants.LEVEL_WIDTH; x++)
                    {
                        var index = (y * Constants.LEVEL_WIDTH) + x + y; // the extra y being added is for newlines
                        var bitValue = bitString[index];

                        if (bitValue == '1')
                        {
                            level.TileMap[x][y].TileType = tileType;
                        }
                    }
                }
            }
        }

        private XmlDocument LoadLevelXml(string filePath)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filePath);

            return xmlDoc;
        }

        private string BuildFileNameFromLevelName(string levelName)
        {
            var currentDir = Directory.GetCurrentDirectory();
            var filename = levelName + ".oel";
            var filePath = Path.Combine(currentDir, "Content\\Levels", filename);

            return filePath;
        }


    }
}
