using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using ZeroElectric.Vinculum;

namespace roguegame
{
    internal class Map
    {
        public int mapWidth;
        public int[] mapTiles;
        public void Draw()
        {
           

            Console.ForegroundColor = ConsoleColor.Gray; // Change to map color
            int mapHeight = mapTiles.Length / mapWidth; // Calculate the height: the amount of rows
            for (int y = 0; y < mapHeight; y++) // for each row
            {
                for (int x = 0; x < mapWidth; x++) // for each column in the row
                {
                    int index = x + y * mapWidth; // Calculate index of tile at (x, y)
                    int tileId = mapTiles[index]; // Read the tile value at index

                    // Draw the tile graphics
                   
                   
                    

                    switch (tileId)
                    {
                        case 1:
                            Console.Write("."); // Floor
                            Raylib.DrawRectangle(x * Game.tilesize, y * Game.tilesize, Game.tilesize, Game.tilesize, Raylib.BLUE);
                            Raylib.DrawTexture(Game.FloorTexture, x * Game.tilesize, y * Game.tilesize, Raylib.WHITE);
                            break;
                        case 2:
                            Console.Write("#"); // Wall
                            Raylib.DrawRectangle(x * Game.tilesize, y * Game.tilesize, Game.tilesize, Game.tilesize, Raylib.BLUE);
                            Raylib.DrawTexture(Game.WallTexture, x * Game.tilesize, y   * Game.tilesize, Raylib.WHITE);
                            break;
                        case 3:
                            Console.Write("#"); // Wall
                            Raylib.DrawRectangle(x * Game.tilesize, y * Game.tilesize, Game.tilesize, Game.tilesize, Raylib.BLUE);
                            Raylib.DrawTexture(Game.PropTexture, x * Game.tilesize, y * Game.tilesize, Raylib.WHITE);
                            break;
                        case 4:
                            Console.Write("#"); // Wall
                            Raylib.DrawRectangle(x * Game.tilesize, y * Game.tilesize, Game.tilesize, Game.tilesize, Raylib.BLUE);
                            Raylib.DrawTexture(Game.EnemyTexture, x * Game.tilesize, y * Game.tilesize, Raylib.WHITE);
                            break;
                        default:
                            Console.Write(" ");
                            break;
                    }
                }
            }
        }
    }
}
