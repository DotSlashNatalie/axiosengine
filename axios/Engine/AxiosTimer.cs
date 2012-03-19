using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Axios.Engine.Interfaces;
using Microsoft.Xna.Framework;

namespace Axios.Engine
{
    /*
     * Modeled after Nicks' implemenentation
     * Source: http://www.gamedev.net/topic/473544-how-to-make-a-timer-using-xna/page__view__findpost__p__4107032
     * 
     */
    public class AxiosTimer : IAxiosGameObject
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

        public void Update(AxiosGameScreen gameScreen, GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {

            if (_enabled)
            {
                if (gameTime.TotalGameTime - lastTick >= interval)
                {
                    if (Tick != null)
                        Tick(this, null);

                    lastTick = gameTime.TotalGameTime;
                }
            }
            else
            {
                lastTick = gameTime.TotalGameTime;
            }
        }

        public virtual void LoadContent(AxiosGameScreen gameScreen)
        {
            
        }

        public void HandleInput(AxiosGameScreen gameScreen, FarseerPhysics.SamplesFramework.InputHelper input, Microsoft.Xna.Framework.GameTime gameTime)
        {
            
        }

        public void HandleCursor(AxiosGameScreen gameScreen, FarseerPhysics.SamplesFramework.InputHelper input)
        {
            
        }

        public void UnloadContent(AxiosGameScreen gameScreen)
        {
            
        }
    }
}
