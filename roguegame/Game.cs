using RayGuiCreator;
using System.Numerics;
using ZeroElectric.Vinculum;
using TurboMapReader;

namespace roguegame
{
    internal class Game
    {
        
        enum GameStatus
        {
            CharacterCreation, MainMenu, GameLoop, PauseMenu, SettingsMenu
        }
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
        Stack<GameStatus>currentStatusStack=new Stack<GameStatus>();
        TextBoxEntry playerNameEntry = new TextBoxEntry(15);
        PlayerCharacter player = new PlayerCharacter();
        PauseMenu pauseMenu= new PauseMenu();
        SettingsMeny settingsMenu = new SettingsMeny();
        private void PauseMenuBackPressed(object sender, EventArgs arguments)
        {
            currentStatusStack.Pop();
            
        }
        private void PauseMenuSettingsPressed(object sender, EventArgs arguments)
        {
            currentStatusStack.Push(GameStatus.SettingsMenu);

        }
        private void SettingsMenuBackPressed(object sender, EventArgs arguments)
        {
            currentStatusStack.Pop();

        }
        private static Map ConvertFromTiledMap(TiledMap loadedMap)
        {
            Map omaMap = new Map();
            omaMap.mapWidth = loadedMap.width;
            for(int i = 0; i <  loadedMap.layers.Count; i++)
            {
                MapLayer omaLayer = new MapLayer(loadedMap.layers[i].data.Length);
                omaLayer.name = loadedMap.layers[i].name;
                omaLayer.mapTiles = loadedMap.layers[i].data;
                omaMap.layers[i] = omaLayer;
            }
            return omaMap;
        }
        public void Run()
        {


            
            Raylib.InitWindow(600, 400, "roguegame");

            pauseMenu.BacktoGameEvent += PauseMenuBackPressed;
            pauseMenu.goToSettingsEvent += PauseMenuSettingsPressed;

            settingsMenu.BacktoGameEvent += SettingsMenuBackPressed;

            currentStatusStack.Push(GameStatus.MainMenu);

            // Pelaajan alkuasema
            player.Position = new Vector2(2, 2);
            MapLoader loader = new MapLoader();

            TurboMapReader.TiledMap mapMadeInTiled = TurboMapReader.MapReader.LoadMapFromFile("tiledmap.tmj");
            Level1 = ConvertFromTiledMap(mapMadeInTiled);


            // Aloitusvalikko: näytetään ensin

            bool inStartMenu = true;
            

            // Pelisilmukka
            while (!Raylib.WindowShouldClose())
            {
                if (currentStatusStack.Peek() == GameStatus.MainMenu)
                {
                    Raylib.BeginDrawing();
                    Raylib.ClearBackground(Raylib.BLACK);


                    int menuStartX = 10;
                    int menuStartY = 0;
                    int rowHeight = Raylib.GetScreenHeight() / 20;
                    int menuWidth = Raylib.GetScreenWidth() / 4;

                    // HUOM MenuCreator luodaan aina uudestaan ennen kuin valikko piirrettään.
                    MenuCreator charmenu = new MenuCreator(menuStartX, menuStartY, rowHeight, menuWidth);

                    charmenu.Label("Main Menu");
                    if (charmenu.Button("Settings"))
                    {
                        currentStatusStack.Push(GameStatus.SettingsMenu);
                    }

                    if (charmenu.Button("Start game"))
                    {
                        currentStatusStack.Push(GameStatus.CharacterCreation);
                    }
                    Raylib.EndDrawing();
                }
                // Tarkista taukovalikon tila
                if (currentStatusStack.Peek() == GameStatus.PauseMenu)
                {
                    pauseMenu.DrawMenu();
                    continue;
                }
       

                // Jos jokin valikoista on auki, piirretään vain se
                else if (currentStatusStack.Peek() == GameStatus.SettingsMenu)
                {
                    settingsMenu.DrawMenu();
                    continue;
                }
                else if (currentStatusStack.Peek() == GameStatus.CharacterCreation)
                {
                    DrawCharacterMenu();
                    continue;
                }
                else if (currentStatusStack.Peek() == GameStatus.GameLoop)
                {
                    if (Raylib.IsKeyPressed(KeyboardKey.KEY_P))
                    {
                        currentStatusStack.Push(GameStatus.PauseMenu);
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
                    int number = Level1.GetLayer("ground").mapTiles[indeksi];
                    List<int> WallTileNumbers = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 12, 13, 14, 15, 16, 17, 18, 19, 20, 24, 25, 26, 27, 28, 29, 40, 57, 58, 59 };
                    if (WallTileNumbers.Contains(number))
                    {
         
                    }
                    else
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
        }
        private void DrawCharacterMenu()
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Raylib.BLACK);


            int menuStartX = 10;
            int menuStartY = 0;
            int rowHeight = Raylib.GetScreenHeight() / 20;
            int menuWidth = Raylib.GetScreenWidth() / 4;

            // HUOM MenuCreator luodaan aina uudestaan ennen kuin valikko piirrettään.
            MenuCreator charmenu = new MenuCreator(menuStartX, menuStartY, rowHeight, menuWidth);

            charmenu.Label("Character Menu");

            charmenu.TextBox(playerNameEntry);
            charmenu.Label("Choose class");
            if (charmenu.Button("Cook"))
            {
                player.Role = Role.Cook;
            }
            if (charmenu.Button("Smith"))
            {
                player.Role = Role.Smith;
            }
            if (charmenu.Button("Ready!"))
            {
                string nameAnswer = playerNameEntry.ToString();
                if (String.IsNullOrEmpty(nameAnswer))
                {
                    Console.WriteLine("Name cannot be empty!");

                }
                else
                {
                    currentStatusStack.Push(GameStatus.GameLoop);
                }

            }
            Raylib.EndDrawing();
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
