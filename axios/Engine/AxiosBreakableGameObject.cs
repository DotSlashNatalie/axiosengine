using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using FarseerPhysics.Dynamics;

using Axios.Engine.Interfaces;

namespace Axios.Engine
{
    //I think using a template here would be good
    //It would solve the problem of having to repeat methods in DrawableBreakableAxiosGameObject
    abstract class AxiosBreakableGameObject : AxiosGameObject
     
    {

        /// <summary>
        /// BodyParts is what the body will break into
        /// Body is what will be used to show the object as a whole
        /// </summary>
        protected List<SimpleAxiosGameObject> BodyParts = new List<SimpleAxiosGameObject>();
        protected SimpleAxiosGameObject BodyPart = null;


        public delegate void BodyBroken(AxiosBreakableGameObject body);

        public event BodyBroken OnBodyBreak;

        protected bool _calledBodyBroken = false;

        protected bool _isbroken = false;

        private int _draworder;

        public bool Broken
        {
            get { return _isbroken; }
            set { _isbroken = true; Break(); }
        }

        public override void LoadContent(AxiosGameScreen gameScreen)
        {
            base.LoadContent(gameScreen);

            BodyParts = new List<SimpleAxiosGameObject>();


            CreateBodyPart(gameScreen);
            CreateBodyParts(gameScreen);

            gameScreen.AddGameObject(BodyPart);
            BodyPart.BodyPart.Enabled = true;
            foreach (SimpleAxiosGameObject obj in BodyParts)
            {
                gameScreen.AddGameObject(obj);
                obj.BodyPart.Enabled = false;
            }

        }

        //The developer will have to define the BodyPart creation in an overriden method
        public abstract void CreateBodyPart(AxiosGameScreen gameScreen);

        //The developer will have to define the BodyParts creation in an overriden method
        public abstract void CreateBodyParts(AxiosGameScreen gameScreen);

        public void Break()
        {
            OnBodyBreak(this);
            _isbroken = true;

            BodyPart.BodyPart.Enabled = false;
            foreach (SimpleAxiosGameObject s in BodyParts)
                s.BodyPart.Enabled = true;
        }

        public override void Update(AxiosGameScreen gameScreen, Microsoft.Xna.Framework.GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameScreen, gameTime, otherScreenHasFocus, coveredByOtherScreen);

            
        }

        protected override void OnRemove(AxiosGameObject gameObject)
        {
            base.OnRemove(gameObject);

            if (BodyPart != null)
                BodyPart.Remove();
        }

        

        public int DrawOrder
        {
            get
            {
                return _draworder;
            }
            set
            {
                _draworder = value;
            }
        }


        public void Draw(AxiosGameScreen gameScreen, Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (_isbroken)
            {
                if (BodyParts.Count > 0 && BodyParts[0] is IDrawableAxiosGameObject)
                {
                    foreach (SimpleAxiosGameObject b in BodyParts)
                        ((IDrawableAxiosGameObject)b).Draw(gameScreen, gameTime);
                }
            }
            else
            {
                if (BodyPart != null && BodyPart is IDrawableAxiosGameObject)
                    ((IDrawableAxiosGameObject)BodyPart).Draw(gameScreen, gameTime);
            }

        }
    }
}
