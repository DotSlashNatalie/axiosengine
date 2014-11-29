using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.IO;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Common;
using FarseerPhysics.SamplesFramework;
using FarseerPhysics.Factories;
using Gleed2D.InGame;

namespace Axios.Engine.Gleed2D
{
    public partial class TextureItem : Item
    {
        public Texture2D texture;

        private TextureItemProperties _item;

        public TextureItemProperties LayerItem
        {
            get { return _item; }
            set { }
        }

        public TextureItem(TextureItemProperties i)
        {
            this._item = i;
        }

        /// <summary>
        /// Called by Level.FromFile(filename) on each Item after the deserialization process.
        /// Loads all assets needed by the TextureItem, especially the Texture2D.
        /// You must provide your own implementation. However, you can rely on all public fields being
        /// filled by the level deserialization process.
        /// </summary>
        public override void load(AxiosGameScreen gameScreen, ref Dictionary<string, Texture2D> cache)
        {
            base.load(gameScreen, ref cache);
            //throw new NotImplementedException();

            //TODO: provide your own implementation of how a TextureItem loads its assets
            //for example:
            //this.texture = Texture2D.FromFile(<GraphicsDevice>, texture_filename);
            //or by using the Content Pipeline:
            if (!cache.ContainsKey(LayerItem.AssetName))
            {
                cache[LayerItem.AssetName] = gameScreen.ScreenManager.Game.Content.Load<Texture2D>(LayerItem.AssetName);   
            }
            this.texture = cache[LayerItem.AssetName];
            //Visible = gameScreen.LoadTextureItem(this);
            
            //this.texture = cm.Load<Texture2D>(asset_name);
            
        }

        public override void draw(SpriteBatch sb)
        {
            if (!LayerItem.Visible) return;
            SpriteEffects effects = SpriteEffects.None;
            if (LayerItem.FlipHorizontally) effects |= SpriteEffects.FlipHorizontally;
            if (LayerItem.FlipVertically) effects |= SpriteEffects.FlipVertically;
            sb.Draw(texture, LayerItem.Position, null, LayerItem.TintColor, LayerItem.Rotation, LayerItem.Origin, LayerItem.Scale, effects, 0);
        }
    }
}
