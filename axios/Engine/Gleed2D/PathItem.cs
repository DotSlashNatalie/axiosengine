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
    public partial class PathItem : Item
    {
        public Vector2[] LocalPoints;
        public Vector2[] WorldPoints;
        public bool IsPolygon;
        public int LineWidth;
        public Color LineColor;

        Body _body;

        public PathItem()
        {
        }

        public override void load(AxiosGameScreen gameScreen, ref Dictionary<string, Texture2D> cache)
        {
            base.load(gameScreen, ref cache);
            if (gameScreen.LoadPathItem(this))
            {
                Vertices v = new Vertices(LocalPoints.Length);
                foreach (Vector2 vec in LocalPoints)
                    v.Add(new Vector2(ConvertUnits.ToSimUnits(vec.X), ConvertUnits.ToSimUnits(vec.Y)));

                _body = BodyFactory.CreateLoopShape(gameScreen.World, v);
                _body.Position = ConvertUnits.ToSimUnits(this.Position);
                _body.UserData = this;
            }
        }

    }
}
