using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Common;
using FarseerPhysics.SamplesFramework;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Axios.Engine.Gleed2D
{
    public partial class RectangleItem : Item
    {
        public float Width;
        public float Height;
        public Color FillColor;

        Body _body;

        public RectangleItem()
        {
        }

        public override void load(ContentManager cm, World world, ref Dictionary<string, Texture2D> cache)
        {
            base.load(cm, world, ref cache);

            _body = BodyFactory.CreateRectangle(world, Width, Height, 1f);
            _body.Position = Position;
            _body.UserData = this;
        }
    }
}
