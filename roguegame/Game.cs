using System.IO.MemoryMappedFiles;
using System.Numerics;
using ZeroElectric.Vinculum;

namespace roguegame
{
    internal class Game
    {
        public static readonly int tilesize = 16;

        // Pelaajan tekstuurit
        public static Texture PlayerTexture = Raylib.LoadTexture("tile_0002.png");
        public static Texture FloorTexture = Raylib.LoadTexture("tile_0049.png");
        public static Texture WallTexture = Raylib.LoadTexture("tile_0040.png");
        public static Texture PropTexture = Raylib.LoadTexture("tile_0082.png");
        public static Texture EnemyTexture = Raylib.LoadTexture("tile_0121.png");

        // Valikkotekstuurit
        public static Texture StartMenuTexture = Raylib.LoadTexture("aloitusvalikko.png");
        public static Texture PauseMenuTexture = Raylib.LoadTexture("taukovalikko.png");
        public static Texture SettingsMenuTexture = Raylib.LoadTexture("asetusvalikko.png");

        Map Level1;

        public void Run()
        {
            PlayerCharacter player = new PlayerCharacter();
            InitializeName(player);
            InitializeSpecies(player);
            InitializeRole(player);
            Raylib.InitWindow(600, 400, "roguegame");

            // Pelaajan alkuasema
            player.Position = new Vector2(1, 1);
            MapLoader loader = new MapLoader();
            Level1 = loader.LoadMapFile("mapfile.json");

            bool isPauseMenuOpen = false;
            bool isSettingsMenuOpen = false;

            // Aloitusvalikko: näytetään ensin
            bool inStartMenu = true;
            while (inStartMenu)
            {
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Raylib.BLACK);
                Raylib.DrawTexture(StartMenuTexture, 0, 0, Raylib.WHITE);
                Raylib.EndDrawing();

                // Odota Q-painallusta
                if (Raylib.IsKeyPressed(KeyboardKey.KEY_Q))
                {
                    inStartMenu = false;
                }
            }

            // Pelisilmukka
            while (!Raylib.WindowShouldClose())
            {
                // Tarkista taukovalikon tila
                if (Raylib.IsKeyPressed(KeyboardKey.KEY_T))
                {
                    isPauseMenuOpen = !isPauseMenuOpen;
                }

                // Tarkista asetusvalikon tila
                if (Raylib.IsKeyPressed(KeyboardKey.KEY_O))
                {
                    isSettingsMenuOpen = !isSettingsMenuOpen;
                }

                // Jos jokin valikoista on auki, piirretään vain se
                if (isPauseMenuOpen)
                {
                    DrawMenu(PauseMenuTexture);
                    continue;
                }
                else if (isSettingsMenuOpen)
                {
                    DrawMenu(SettingsMenuTexture);
                    continue;
                }

                // ------------Update:
                int moveX = 0;
                int moveY = 0;

                if (Raylib.IsKeyPressed(KeyboardKey.KEY_UP)) moveY = -1;
                else if (Raylib.IsKeyPressed(KeyboardKey.KEY_DOWN)) moveY = 1;
                else if (Raylib.IsKeyPressed(KeyboardKey.KEY_LEFT)) moveX = -1;
                else if (Raylib.IsKeyPressed(KeyboardKey.KEY_RIGHT)) moveX = 1;

                int x = (int)player.Position.X + moveX;
                int y = (int)player.Position.Y + moveY;
                int indeksi = x + y * Level1.mapWidth;
                int number = Level1.mapTiles[indeksi];
                if (number == 1)
                {
                    player.Position.X += moveX;
                    player.Position.Y += moveY;
                }

                // Prevent player from going outside screen
                if (player.Position.X < 0) player.Position.X = 0;
                else if (player.Position.X > Console.WindowWidth - 1) player.Position.X = Console.WindowWidth - 1;

                if (player.Position.Y < 0) player.Position.Y = 0;
                else if (player.Position.Y > Console.WindowHeight - 1) player.Position.Y = Console.WindowHeight - 1;

                // -----------Draw:
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Raylib.BLACK);
                Level1.Draw();

                int playerpixelx = (int)player.Position.X;
                int playerpixely = (int)player.Position.Y;

                Raylib.DrawRectangle(playerpixelx * Game.tilesize, playerpixely * Game.tilesize, Game.tilesize, Game.tilesize, Raylib.RED);
                Raylib.DrawTexture(PlayerTexture, playerpixelx * Game.tilesize, playerpixely * Game.tilesize, Raylib.WHITE);

                Raylib.EndDrawing();
            }
        }

        private void DrawMenu(Texture menuTexture)
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Raylib.BLACK);
            Raylib.DrawTexture(menuTexture, 0, 0, Raylib.WHITE);
            Raylib.EndDrawing();
        }

        private static void InitializeName(PlayerCharacter pelaaja)
        {
            while (true)
            {
                Console.Write("Anna hahmon nimi: ");
                string nameAnswer = Console.ReadLine();
                if (String.IsNullOrEmpty(nameAnswer))
                {
                    Console.WriteLine("Name cannot be empty!");
                    continue;
                }
                pelaaja.Name = nameAnswer;
                break;
            }

            Console.WriteLine(pelaaja.Name);
        }

        private static void InitializeRole(PlayerCharacter pelaaja)
        {
            Console.WriteLine("anna rooli");
            string roolivastaus = Console.ReadLine();
            if (roolivastaus == Role.Cook.ToString())
            {
                pelaaja.Role = Role.Cook;
            }
            else if (roolivastaus == Role.Smith.ToString())
            {
                pelaaja.Role = Role.Smith;
            }
            else if (roolivastaus == Role.Rogue.ToString())
            {
                pelaaja.Role = Role.Rogue;
            }
            Console.WriteLine(pelaaja.Role.ToString());
        }

        private static void InitializeSpecies(PlayerCharacter pelaaja)
        {
            Console.WriteLine("anna laji");
            string lajivastaus = Console.ReadLine();
            if (lajivastaus == Species.Duck.ToString())
            {
                pelaaja.Species = Species.Duck;
            }
            else if (lajivastaus == Species.Mongoose.ToString())
            {
                pelaaja.Species = Species.Mongoose;
            }
            else if (lajivastaus == Species.Elf.ToString())
            {
                pelaaja.Species = Species.Elf;
            }
            Console.WriteLine(pelaaja.Species.ToString());
        }
    }
}
