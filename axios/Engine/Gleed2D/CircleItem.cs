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
    public partial class CircleItem : Item
    {
        public float Radius;
        public Color FillColor;

        Body _body;

        public CircleItem()
        {
        }

        public override void load(ContentManager cm, World world, ref Dictionary<string, Texture2D> cache)
        {
            base.load(cm, world, ref cache);

            _body = BodyFactory.CreateCircle(world, Radius, 1f);
            _body.Position = Position;
            _body.UserData = this;
        }

    }
}
