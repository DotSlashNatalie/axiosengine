using System;

using Axios.Engine.Interfaces;
using Microsoft.Xna.Framework;
using GameStateManagement;

namespace Axios.Engine
{
    /*
     * Modeled after Nicks' implemenentation
     * Source: http://www.gamedev.net/topic/473544-how-to-make-a-timer-using-xna/page__view__findpost__p__4107032
     * 
     */
    public class AxiosTimer : AxiosGameObject
    {
        TimeSpan interval = new TimeSpan(0, 0, 1);
        TimeSpan lastTick = new TimeSpan();
        private bool _enabled = false;
        public TimeSpan? offset = null;

        public event EventHandler Tick;

        public TimeSpan Interval
        {
            get { return interval; }
            set { interval = value; }
        }

        public Boolean Enabled
        {
            get { return _enabled; }
            set { _enabled = value; }
        }

        public AxiosTimer()
        {
            
        }

        public override void Update(AxiosGameScreen gameScreen, GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            // Issue here: if you add a timer later on and use the algorithm of
            // gameTime.TotalGameTime - lastTick >= interval
            // The timer will always run
            // What we should do is have an offset of the time it was added like this:
            // ((gameTime.TotalGameTime - offset) - lastTick)  >= interval
            if (gameScreen.ScreenManager.Game.IsActive) //only "tick" if the window has focus - otherwise the Timer will play catchup
            {
                if (offset == null)
                {
                    offset = gameTime.TotalGameTime;
                    return;
                }
                if (_enabled)
                {
                    if (((gameTime.TotalGameTime - offset) - lastTick) >= interval)
                    {
                        if (Tick != null)
                        {
                            //EventArgs e = new EventArgs();
                            //System.Diagnostics.Debugger.Break();
                            Tick(this, null);
                        }

                        lastTick = gameTime.TotalGameTime;
                    }
                }
                else
                {
                    lastTick = gameTime.TotalGameTime;
                }
            }
        }

        public override void LoadContent(AxiosGameScreen gameScreen)
        {
            
        }

        public override void HandleInput(AxiosGameScreen gameScreen, InputState input, Microsoft.Xna.Framework.GameTime gameTime)
        {
            
        }

        public override void HandleCursor(AxiosGameScreen gameScreen, InputState input)
        {
            
        }

        public override void UnloadContent(AxiosGameScreen gameScreen)
        {
            
        }
    }
}
