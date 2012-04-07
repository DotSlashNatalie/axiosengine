using System;

using Axios.Engine.Interfaces;
using Microsoft.Xna.Framework;

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

            if (_enabled)
            {
                if (gameTime.TotalGameTime - lastTick >= interval)
                {
                    if (Tick != null)
                    {
                        //EventArgs e = new EventArgs();
                        
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

        public override void LoadContent(AxiosGameScreen gameScreen)
        {
            
        }

        public override void HandleInput(AxiosGameScreen gameScreen, FarseerPhysics.SamplesFramework.InputHelper input, Microsoft.Xna.Framework.GameTime gameTime)
        {
            
        }

        public override void HandleCursor(AxiosGameScreen gameScreen, FarseerPhysics.SamplesFramework.InputHelper input)
        {
            
        }

        public override void UnloadContent(AxiosGameScreen gameScreen)
        {
            
        }
    }
}
