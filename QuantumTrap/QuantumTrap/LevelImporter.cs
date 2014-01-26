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

            return new Level();
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
