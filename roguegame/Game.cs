using System.Numerics;

namespace roguegame
{
    internal class Game
    {

        public void Run()
        {
            PlayerCharacter player = new PlayerCharacter();
            InitializeName(player);
            InitializeSpecies(player);
            InitializeRole(player);

            player.Position = new Vector2(1, 1);

            Console.Clear();
            // Draw the player
            Console.SetCursorPosition((int)player.Position.X, (int)player.Position.Y);
            Console.Write("@");

            // Start the game loop:
            while (true)
            {
                // ------------Update:
                // Prepare to read movement input
                int moveX = 0;
                int moveY = 0;
                // Wait for keypress and compare value to ConsoleKey enum
                ConsoleKeyInfo key = Console.ReadKey();
                if (key.Key == ConsoleKey.UpArrow)
                {
                    moveY = -1;
                }
                else if (key.Key == ConsoleKey.DownArrow)
                {
                    moveY = 1;
                }
                else if (key.Key == ConsoleKey.LeftArrow)
                {
                    moveX = -1;
                }
                else if (key.Key == ConsoleKey.RightArrow)
                {
                    moveX = 1;
                }

                //
                // TODO: CHECK COLLISION WITH WALLS
                //

                // Move the player
                player.Position.X += moveX;
                player.Position.Y += moveY;
                // Prevent player from going outside screen
                if (player.Position.X < 0)
                {
                    player.Position.X = 0;
                }
                else if (player.Position.X > Console.WindowWidth - 1)
                {
                    player.Position.X = Console.WindowWidth - 1;
                }
                if (player.Position.Y < 0)
                {
                    player.Position.Y = 0;
                }
                else if (player.Position.Y > Console.WindowHeight - 1)
                {
                    player.Position.Y = Console.WindowHeight - 1;
                }

                // -----------Draw:
                // Clear the screen so that player appears only in one place
                Console.Clear();
                // Draw the player
                Console.SetCursorPosition((int)player.Position.X, (int)player.Position.Y);
                Console.Write("@");
            } // game loop ends












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
