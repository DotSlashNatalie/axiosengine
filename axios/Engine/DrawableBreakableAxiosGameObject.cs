using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Axios.Engine.Interfaces;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using FarseerPhysics.Dynamics;
using FarseerPhysics.SamplesFramework;

namespace Axios.Engine
{
#if DFDSF
    class DrawableBreakableAxiosGameObject : AxiosBreakableGameObject, IDrawableAxiosGameObject
    {
        protected int _draworder;

        protected new List<SimpleDrawableAxiosGameObject> BodyParts = new List<SimpleDrawableAxiosGameObject>();
        protected new SimpleDrawableAxiosGameObject BodyPart = null;

        protected Boolean _adjustunits = true;
        protected Boolean _relativetocamera = true;

        public int DrawOrder
        {
            get
            {
                return this._draworder;
            }
            set
            {
                this._draworder = value;
            }
        }

        public override void LoadContent(AxiosGameScreen gameScreen)
        {
            base.LoadContent(gameScreen);

            
        }

        public virtual void Draw(AxiosGameScreen gameScreen, GameTime gameTime)
        {

            /*for(int i = 0; i < Body.Parts.Count; i++)
            {
                if (_relativetocamera)
                    gameScreen.ScreenManager.SpriteBatch.Begin(0, null, null, null, null, null, gameScreen.Camera.View);
                else
                    gameScreen.ScreenManager.SpriteBatch.Begin();
                if (_adjustunits)
                    DrawObject(gameScreen.ScreenManager.SpriteBatch, Textures[i], Body.Parts[i].Body, Origins[i], true);
                else
                    DrawObject(gameScreen.ScreenManager.SpriteBatch, Textures[i], Body.Parts[i].Body, Origins[i]);
                gameScreen.ScreenManager.SpriteBatch.End();
            }*/
            if (_isbroken)
                if (BodyParts.Count > 0)
                    foreach (SimpleDrawableAxiosGameObject obj in BodyParts)
                        obj.Draw(gameScreen, gameTime);
                else
                    if (BodyPart != null)
                        BodyPart.Draw(gameScreen, gameTime);
        }

    }
#endif
}
