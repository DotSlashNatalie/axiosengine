#if WINDOWS
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XNACC.Console;
using Microsoft.Xna.Framework.Graphics;

namespace Axios.Engine
{
    class AxiosCommandConsole : CommandConsoleBase
    {
        public AxiosCommandConsole(AxiosGameScreen gameScreen)
            : base(gameScreen.ScreenManager.Game)
        {
            Keyboard = gameScreen.ScreenManager.InputState;
        }

        public AxiosCommandConsole(AxiosGameScreen gameScreen, SpriteFont font)
            : base(gameScreen.ScreenManager.Game, font)
        {
            Keyboard = gameScreen.ScreenManager.InputState;
        }
    }
}
#endif