using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using FarseerPhysics.Dynamics;

namespace Axios.Engine
{
    class BreakableAxiosGameObject : AxiosGameObject
    {

        public BreakableBody Body;

        public delegate void BodyBroken(BreakableAxiosGameObject body);

        public event BodyBroken OnBodyBreak;

        protected bool _calledBodyBroken = false;

        public override void LoadContent(AxiosGameScreen gameScreen)
        {
            base.LoadContent(gameScreen);

            Body = new BreakableBody();
        }

        protected virtual void LoadSimpleBreakableBody()
        {
            
        }

        public override void Update(AxiosGameScreen gameScreen, Microsoft.Xna.Framework.GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameScreen, gameTime, otherScreenHasFocus, coveredByOtherScreen);

            if (!_calledBodyBroken)
            {
                if (Body.Broken == true)
                    OnBodyBreak(this);
            }
        }
    }
}
