using RayGuiCreator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeroElectric.Vinculum;

namespace roguegame
{
    internal class SettingsMeny
    {
        public event EventHandler BacktoGameEvent;
        
        public void DrawMenu()
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Raylib.DARKPURPLE);


            int menuStartX = 10;
            int menuStartY = 0;
            int rowHeight = Raylib.GetScreenHeight() / 20;
            int menuWidth = Raylib.GetScreenWidth() / 4;

            // HUOM MenuCreator luodaan aina uudestaan ennen kuin valikko piirrettään.
            MenuCreator mainmenu = new MenuCreator(menuStartX, menuStartY, rowHeight, menuWidth);

            mainmenu.Label("Pause Menu");

            if (mainmenu.Button("Back to game"))
            {

                BacktoGameEvent.Invoke(this, EventArgs.Empty);

            }
            Raylib.EndDrawing();
        }
    }
}
