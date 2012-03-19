using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;

using Axios.Engine;
using Axios.Engine.Interfaces;
using FarseerPhysics.SamplesFramework;

namespace Axios.Engine
{

    public abstract class ComplexAxiosGameObject : AxiosGameObject
    {

        public List<SimpleAxiosGameObject> GameObjects;

        public ComplexAxiosGameObject()
        {

        }

        public override void LoadContent(AxiosGameScreen gameScreen)
        {
            base.LoadContent(gameScreen);

            CreateObjects(gameScreen);

            foreach (SimpleAxiosGameObject obj in GameObjects)
            {
                gameScreen.AddGameObject(obj);
            }

        }

        protected override void OnRemove(AxiosGameObject gameObject)
        {
            base.OnRemove(gameObject);

            foreach (SimpleAxiosGameObject g in GameObjects)
                g.Remove();
        }

        public abstract void CreateObjects(AxiosGameScreen gameScreen);
    }

}
