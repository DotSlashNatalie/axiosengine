using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using FarseerPhysics.Dynamics;
using Gleed2D.InGame;

namespace Axios.Engine.Gleed2D
{
    public class Item
    {


        /// <summary>
        /// Called by Level.FromFile(filename) on each Item after the deserialization process.
        /// Should be overriden and can be used to load anything needed by the Item (e.g. a texture).
        /// </summary>
        public virtual void load(AxiosGameScreen gameScreen, ref Dictionary<string, Texture2D> cache)
        {

        }

        public virtual void draw(SpriteBatch sb)
        {
        }
    }
}
