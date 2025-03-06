using RayGuiCreator;
using ZeroElectric.Vinculum;

namespace roguegame
{
    internal class PauseMenu
    {
        public event EventHandler BacktoGameEvent;
        public event EventHandler goToSettingsEvent;
        public void DrawMenu()
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Raylib.BLUE);


            int menuStartX = 10;
            int menuStartY = 0;
            int rowHeight = Raylib.GetScreenHeight() / 20;
            int menuWidth = Raylib.GetScreenWidth() / 4;

            // HUOM MenuCreator luodaan aina uudestaan ennen kuin valikko piirrettään.
            MenuCreator mainmenu = new MenuCreator(menuStartX, menuStartY, rowHeight, menuWidth);

            mainmenu.Label("Pause Menu");
            if (mainmenu.Button("Options"))
            {

                goToSettingsEvent.Invoke(this, EventArgs.Empty);

            }

            if (mainmenu.Button("Back to game"))
            {

                BacktoGameEvent.Invoke(this, EventArgs.Empty);

            }
            Raylib.EndDrawing();
        }
    }
}
