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

        public override void load(AxiosGameScreen gameScreen, ref Dictionary<string, Texture2D> cache, Layer layer)
        {
            base.load(gameScreen, ref cache, layer);
            if (gameScreen.LoadRectangleItem(this))
            {
                _body = BodyFactory.CreateRectangle(gameScreen.World, ConvertUnits.ToSimUnits(Width), ConvertUnits.ToSimUnits(Height), 1f);
                _body.Position = ConvertUnits.ToSimUnits(Position) + new Vector2(ConvertUnits.ToSimUnits(Width) / 2, ConvertUnits.ToSimUnits(Height) / 2);
                _body.UserData = this;
            }
        }
    }
}
