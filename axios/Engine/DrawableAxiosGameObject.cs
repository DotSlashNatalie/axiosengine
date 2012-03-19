using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.SamplesFramework;
using Axios.Engine.Interfaces;

namespace Axios.Engine
{
    public class DrawableAxiosGameObject : AxiosGameObject, IDrawableAxiosGameObject
    {
        protected int _draworder;
        protected Texture2D Texture;
        //protected float _scale = 1f;
        public Vector2 Position = new Vector2();
        public Vector2 Origin = new Vector2();

        protected Boolean _adjustunits = true;
        protected Boolean _relativetocamera = true;

        public override void LoadContent(AxiosGameScreen gameScreen)
        {
            base.LoadContent(gameScreen);

            
        }

        /*public float Scale
        {
            get { return _scale; }
            set { _scale = value; }
        }*/

        public virtual void Draw(AxiosGameScreen gameScreen, GameTime gameTime)
        {
/*#if DEBUG
            System.Diagnostics.Debugger.Break();
#endif*/
            if (_relativetocamera)
                gameScreen.ScreenManager.SpriteBatch.Begin(0, null, null, null, null, null, gameScreen.Camera.View);
            else
                gameScreen.ScreenManager.SpriteBatch.Begin();
            if (_adjustunits)
                gameScreen.ScreenManager.SpriteBatch.Draw(Texture, ConvertUnits.ToDisplayUnits(Position), null, Color.White, 0, Origin, _scale, SpriteEffects.None, 0);
            else
                gameScreen.ScreenManager.SpriteBatch.Draw(Texture, Position, null, Color.White, 0, Origin, _scale, SpriteEffects.None, 0);
            gameScreen.ScreenManager.SpriteBatch.End();
        }

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

        //Copied/adapted from http://create.msdn.com/en-US/education/catalog/tutorial/collision_2d_perpixel
        /// <summary>
        /// This method is a very simple collision detection based on textures.
        /// While Farseer (Box2D) is an excellent physics engine - it doesn't know, or care, about the textures.
        /// This method does a AABB test and if that is true - it tests the individual pixels in the textures.
        /// </summary>
        /// <param name="obj">Object to test against</param>
        /// <returns>true if the object is colliding, false if it isn't</returns>
        public bool CollidesWith(DrawableAxiosGameObject obj)
        {
            Rectangle thisobj = new Rectangle((int)this.Position.X, (int)this.Position.Y, this.Texture.Width, this.Texture.Height);
            Rectangle otherobj = new Rectangle((int)obj.Position.X, (int)obj.Position.Y, obj.Texture.Width, obj.Texture.Height);

            if (thisobj.Intersects(otherobj))
            {

                int top = Math.Max(thisobj.Top, otherobj.Top);
                int bottom = Math.Min(thisobj.Bottom, otherobj.Bottom);
                int left = Math.Max(thisobj.Left, otherobj.Left);
                int right = Math.Min(thisobj.Right, otherobj.Right);

                Color[] thisobjcolor = new Color[this.Texture.Width * this.Texture.Height];
                Color[] otherobjcolor = new Color[obj.Texture.Width * obj.Texture.Height];

                Texture.GetData(thisobjcolor);
                obj.Texture.GetData(otherobjcolor);

                // Check every point within the intersection bounds
                for (int y = top; y < bottom; y++)
                {
                    for (int x = left; x < right; x++)
                    {
                        // Get the color of both pixels at this point
                        Color colorA = thisobjcolor[(x - thisobj.Left) +
                                             (y - thisobj.Top) * thisobj.Width];
                        Color colorB = otherobjcolor[(x - otherobj.Left) +
                                             (y - otherobj.Top) * otherobj.Width];

                        // If both pixels are not completely transparent,
                        if (colorA.A != 0 && colorB.A != 0)
                        {
                            // then an intersection has been found
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        //Copied/adapted from http://create.msdn.com/en-US/education/catalog/tutorial/collision_2d_perpixel
        /// <summary>
        /// This method is a very simple collision detection based on textures.
        /// While Farseer (Box2D) is an excellent physics engine - it doesn't know, or care, about the textures.
        /// This method does a AABB test and if that is true - it tests the individual pixels in the textures.
        /// </summary>
        /// <param name="obj">Object to test against</param>
        /// <returns>true if the object is colliding, false if it isn't</returns>
        public bool CollidesWith(Vector2 pos, Rectangle rect)
        {
            Rectangle thisobj = new Rectangle((int)this.Position.X, (int)this.Position.Y, this.Texture.Width, this.Texture.Height);
            Rectangle otherobj = new Rectangle((int)pos.X, (int)pos.Y, rect.Width, rect.Height);

            Texture2D obj = new Texture2D(Texture.GraphicsDevice, rect.Width, rect.Height);
            Color[] arr = new Color[rect.Width * rect.Height];
            for (int i = 0; i < rect.Width * rect.Height; ++i)
                arr[i] = Color.Black;

            obj.SetData(arr);

            if (thisobj.Intersects(otherobj))
            {

                int top = Math.Max(thisobj.Top, otherobj.Top);
                int bottom = Math.Min(thisobj.Bottom, otherobj.Bottom);
                int left = Math.Max(thisobj.Left, otherobj.Left);
                int right = Math.Min(thisobj.Right, otherobj.Right);

                Color[] thisobjcolor = new Color[this.Texture.Width * this.Texture.Height];
                Color[] otherobjcolor = new Color[obj.Width * obj.Height];

                Texture.GetData(thisobjcolor);
                obj.GetData(otherobjcolor);

                // Check every point within the intersection bounds
                for (int y = top; y < bottom; y++)
                {
                    for (int x = left; x < right; x++)
                    {
                        // Get the color of both pixels at this point
                        Color colorA = thisobjcolor[(x - thisobj.Left) +
                                             (y - thisobj.Top) * thisobj.Width];
                        Color colorB = otherobjcolor[(x - otherobj.Left) +
                                             (y - otherobj.Top) * otherobj.Width];

                        // If both pixels are not completely transparent,
                        if (colorA.A != 0 && colorB.A != 0)
                        {
                            // then an intersection has been found
                            return true;
                        }
                    }
                }
            }

            return false;
        }
    }
}
