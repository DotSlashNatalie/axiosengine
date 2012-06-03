#if WINDOWS
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XNACC.Console;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Axios.Engine.Log;

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
        protected AxiosGameScreen GameScreen;
        protected List<string> RestrictedCommands = new List<string>();
        public AxiosCommandConsole(AxiosGameScreen gameScreen)
            : base(gameScreen.ScreenManager.Game)
        {
            GameScreen = gameScreen;
            Keyboard = gameScreen.ScreenManager.InputState;
        }

        public AxiosCommandConsole(AxiosGameScreen gameScreen, SpriteFont font)
            : base(gameScreen.ScreenManager.Game, font)
        {
            GameScreen = gameScreen;
            Keyboard = gameScreen.ScreenManager.InputState;
        }

        protected void LoadDefault()
        {
            FadeColor = Color.White * 0.5f;
            Texture2D tmp = new Texture2D(GraphicsDevice, 1, 1);
            tmp.SetData<Color>(new Color[] { Color.Black });
            FadeImage = tmp;
        }

        private void ShowAxiosLog()
        {
            AddOutputToLog("============");
            foreach (string l in AxiosLog.Instance.GetLogList())
                AddOutputToLog(l);
            AddOutputToLog("============");
        }

        public void ToggleCamera()
        {
            GameScreen.EnableCameraControl = !GameScreen.EnableCameraControl;
        }

        public override void InitializeCustomCommands()
        {
            AddCommand(new CmdObject("axioslog", "Displays the current Axios Log", input => { ShowAxiosLog(); }));
            AddCommand(new CmdObject("tcc", "Toggles user camera control", input => { ToggleCamera(); }));
            base.InitializeCustomCommands();

        }
        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);

            foreach (string cmd in RestrictedCommands)
            {
                if (ms_commands.Keys.Contains(cmd))
                    ms_commands.Remove(cmd);
            }
        }

        protected override void LoadContent()
        {

            if (Font == null)
                Font = Game.Content.Load<SpriteFont>("Console");
            base.LoadContent();
        }

        protected override void UnloadContent()
        {
            base.UnloadContent();
            //ms_commands.Remove("axioslog");
        }

    }
}
#else

#endif