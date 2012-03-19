using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;

using Axios.Engine.Log;

namespace Axios.Engine
{
    public abstract class SimpleAxiosGameObject : AxiosGameObject
    {
        public Body BodyPart;
        public Vector2 Position;
        public Vector2 Origin;

        public bool ApplyConstantVelocity = false;
        public Vector2 ConstantVelocity;

        public SimpleAxiosGameObject()
        {
            AxiosLog.Instance.AddLine("[Axios Engine] - Creating SimpleAxiosGameObject " + Name, LoggingFlag.DEBUG);
            Position = new Vector2();
        }

        public override void UnloadContent(AxiosGameScreen gameScreen)
        {
            AxiosLog.Instance.AddLine("[Axios Engine] - Unloading SimpleAxiosGameObject " + Name, LoggingFlag.DEBUG);
            base.UnloadContent(gameScreen);
            gameScreen.World.RemoveBody(BodyPart);  
        }

        public override void Update(AxiosGameScreen gameScreen, GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameScreen, gameTime, otherScreenHasFocus, coveredByOtherScreen);

            if (ApplyConstantVelocity)
            {
                if (Math.Abs(BodyPart.LinearVelocity.X) > ConstantVelocity.X || Math.Abs(BodyPart.LinearVelocity.X) < ConstantVelocity.X)
                {
                    //Figure which direction it's going and adjust

                    if (Math.Abs(BodyPart.LinearVelocity.X) > BodyPart.LinearVelocity.X) //negative
                        BodyPart.LinearVelocity = new Vector2(-ConstantVelocity.X, BodyPart.LinearVelocity.Y);
                    else
                        BodyPart.LinearVelocity = new Vector2(ConstantVelocity.X, BodyPart.LinearVelocity.Y);

                }

                if (Math.Abs(BodyPart.LinearVelocity.Y) > ConstantVelocity.Y || Math.Abs(BodyPart.LinearVelocity.Y) < ConstantVelocity.Y)
                {
                    //Figure which direction it's going and adjust

                    if (Math.Abs(BodyPart.LinearVelocity.Y) > BodyPart.LinearVelocity.Y) //negative
                        BodyPart.LinearVelocity = new Vector2(BodyPart.LinearVelocity.X, -ConstantVelocity.Y);
                    else
                        BodyPart.LinearVelocity = new Vector2(BodyPart.LinearVelocity.X, ConstantVelocity.Y);
                }
            }
        }

        
        
    }
}
