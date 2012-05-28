#if WINDOWS
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XNACC.Console;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

/*
 * The empty AxiosCommandConsole is so that when you use the comamnd console
 * in your game you don't need #if WINDOWS/#endif precompiler - when you attempt
 * to use it on WP7/Xbox 360 it just won't do anything.
 * 
 * Perhaps one day we should develop a customized console that doesn't require keyboard input
 * to still allow debugging on WP7/Xbox 360
 *  -- Nathan Adams [adamsna@datanethost.net] - 5/26/2012
 */

namespace Axios.Engine
{
    public class AxiosCommandConsole : CommandConsoleBase
    {
        //private AxiosGameScreen _gameScreen;
        public AxiosCommandConsole(AxiosGameScreen gameScreen)
            : base(gameScreen.ScreenManager.Game)
        {
            //_gameScreen = gameScreen;
            Keyboard = gameScreen.ScreenManager.InputState;
        }

        public AxiosCommandConsole(AxiosGameScreen gameScreen, SpriteFont font)
            : base(gameScreen.ScreenManager.Game, font)
        {
            //_gameScreen = gameScreen;
            Keyboard = gameScreen.ScreenManager.InputState;
        }

        protected void LoadDefault()
        {
            FadeColor = Color.White * 0.5f;
            Texture2D tmp = new Texture2D(GraphicsDevice, 1, 1);
            tmp.SetData<Color>(new Color[] { Color.Black });
            FadeImage = tmp;
        }


        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);
        }
        protected override void LoadContent()
        {
            if (Font == null)
                Font = Game.Content.Load<SpriteFont>("Console");
            base.LoadContent();
        }
    }
}
#else

#endif