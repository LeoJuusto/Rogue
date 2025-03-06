using ZeroElectric.Vinculum;

namespace roguegame
{
    internal class Map
    {
        public int mapWidth;
        public MapLayer[] layers;


        public Map()
        {
            mapWidth = 1;

            layers = new MapLayer[3];
            for (int i = 0; i < layers.Length; i++)
            {
                layers[i] = new MapLayer(mapWidth);
            }
            //joo
            //enemies = new List<Enemy>() { };
            //items = new List<Item>() { };
        }


        public MapLayer GetLayer(string layerName)
        {
            for (int i = 0; i < layers.Length; i++)
            {
                if (layers[i].name == layerName)
                {
                    return layers[i];
                }
            }
            Console.WriteLine($"Error: No layer with name: {layerName}");
    return null; // Wanted layer was not found!
        }
        public void Draw()
        {


            Console.ForegroundColor = ConsoleColor.Gray; // Change to map color
            int mapHeight = GetLayer("ground").mapTiles.Length / mapWidth;
            int[] juttu = GetLayer("ground").mapTiles;
            for (int y = 0; y < mapHeight; y++) // for each row
            {
                for (int x = 0; x < mapWidth; x++) // for each column in the row
                {
                    int index = x + y * mapWidth; // Calculate index of tile at (x, y)
                    int tileId = juttu[index]; // Read the tile value at index

                    // Draw the tile graphics
                    List<int> WallTileNumbers = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 12, 13, 14, 15, 16, 17, 18, 19, 20, 24, 25, 26, 27, 28, 29, 40, 57, 58, 59 };
                    if (WallTileNumbers.Contains(tileId))
                    {

                        Console.Write("#"); // Wall
                        Raylib.DrawRectangle(x * Game.tilesize, y * Game.tilesize, Game.tilesize, Game.tilesize, Raylib.BLUE);
                        Raylib.DrawTexture(Game.WallTexture, x * Game.tilesize, y * Game.tilesize, Raylib.WHITE);

                    }
                    else
                    {
                        Console.Write("."); // Floor
                        Raylib.DrawRectangle(x * Game.tilesize, y * Game.tilesize, Game.tilesize, Game.tilesize, Raylib.BLUE);
                        Raylib.DrawTexture(Game.FloorTexture, x * Game.tilesize, y * Game.tilesize, Raylib.WHITE);

                    }

                
                    
                }
            }
        }
    }
}
