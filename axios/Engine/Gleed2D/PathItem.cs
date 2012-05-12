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

        public override void load(ContentManager cm, World world, ref Dictionary<string, Texture2D> cache)
        {
            base.load(cm, world, ref cache);

            Vertices v = new Vertices(WorldPoints.Length);
            foreach (Vector2 vec in WorldPoints)
                v.Add(new Vector2(ConvertUnits.ToSimUnits(vec.X), ConvertUnits.ToSimUnits(vec.Y)));

            _body = BodyFactory.CreateLoopShape(world, v);
            _body.Position = this.Position;
            _body.UserData = this;
        }

    }
}
