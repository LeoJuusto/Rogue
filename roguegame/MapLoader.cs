using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection.PortableExecutable;
using Newtonsoft.Json;

namespace roguegame
{
    internal class MapLoader
    {

        public Map LoadMapFile(string path)
        {
            bool exists = File.Exists(path);

            if (exists)
            {
                using StreamReader reader = new(path);
                string text = reader.ReadToEnd();
                Map deserializedProduct = JsonConvert.DeserializeObject<Map>(text);
                return deserializedProduct;
            }
            else
            {
                return null;
            }
        }

        public Map LoadTestMap()
        {
            Map Test = new Map();
            int mapWidth = 8;
            int[] mapTiles = new int[] {
    2, 2, 2, 2, 2, 2, 2, 2,
    2, 1, 1, 2, 1, 1, 1, 2,
    2, 1, 1, 2, 1, 1, 1, 2,
    2, 1, 1, 1, 1, 1, 3, 2,
    2, 2, 4, 4, 1, 1, 1, 2,
    2, 1, 1, 1, 1, 1, 1, 2,
    2, 2, 2, 2, 2, 2, 2, 2 };
            Test.mapWidth = mapWidth;
            Test.mapTiles = mapTiles;
            return Test;

        }
    }
}
