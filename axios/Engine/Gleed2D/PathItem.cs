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
using Gleed2D.InGame;

namespace Axios.Engine.Gleed2D
{
    public class PathItem : Item
    {
        private PathItemProperties _item;

        public PathItemProperties LayerItem
        {
            get { return _item; }
            set { }
        }

        Body _body;

        public PathItem(PathItemProperties i)
        {
            this._item = i;
        }

        public override void load(AxiosGameScreen gameScreen, ref Dictionary<string, Texture2D> cache)
        {
            base.load(gameScreen, ref cache);
            
            Vertices v = new Vertices(LayerItem.LocalPoints.Count);
            foreach (Vector2 vec in LayerItem.LocalPoints)
                v.Add(new Vector2(ConvertUnits.ToSimUnits(vec.X), ConvertUnits.ToSimUnits(vec.Y)));

            _body = BodyFactory.CreateLoopShape(gameScreen.World, v);
            _body.Position = ConvertUnits.ToSimUnits(this.LayerItem.Position);
            _body.UserData = this;
            
        }

    }
}
