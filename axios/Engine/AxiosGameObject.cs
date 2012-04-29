using Axios.Engine.Interfaces;
using FarseerPhysics.Dynamics;
using FarseerPhysics.SamplesFramework;
using Microsoft.Xna.Framework;
using GameStateManagement;

namespace Axios.Engine
{
    public abstract class AxiosGameObject : AxiosEvents, IAxiosGameObject
    {
        protected float _scale = 1f;
        protected bool removing = false;

        public float Scale
        {
            get { return _scale; }
            set 
            {
                if (value != _scale)
                {
                    _scale = value;
                    OnScaleChange(this);
                }
            }
        }

        private string _name;

        public string Name
        {
            get
            {
                return this._name;
            }
            set
            {
                this._name = value;
            }
        }
        public virtual void Update(AxiosGameScreen gameScreen, Microsoft.Xna.Framework.GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            
        }

        public virtual void LoadContent(AxiosGameScreen gameScreen)
        {

        }

        public virtual void HandleInput(AxiosGameScreen gameScreen, InputState input, GameTime gameTime)
        {

        }

        public virtual void HandleCursor(AxiosGameScreen gameScreen, InputState input)
        {

        }

        public virtual void UnloadContent(AxiosGameScreen gameScreen)
        {
            RemoveEvents();
        }

        public void Remove()
        {
            this.OnRemove(this);
        }

        protected void SetCollideWithAll(Body b)
        {
            if (b != null)
            {
                b.CollidesWith = Category.All;
                b.CollisionCategories = Category.All;
            }
        }

        public override string ToString()
        {
            return this._name;
        }

    }
}
