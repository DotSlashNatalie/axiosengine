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

        public override void load(AxiosGameScreen gameScreen, ref Dictionary<string, Texture2D> cache, Layer layer)
        {
            base.load(gameScreen, ref cache, layer);
            if (gameScreen.LoadCircleItem(this))
            {
                _body = BodyFactory.CreateCircle(gameScreen.World, Radius, 1f);
                _body.Position = ConvertUnits.ToSimUnits(Position);
                _body.UserData = this;
            }
        }

    }
}
