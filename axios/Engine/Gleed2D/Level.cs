﻿using System;
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

namespace Axios.Engine.Gleed2D
{
    public partial class Level
    {
        private World _world;

        /// <summary>
        /// The name of the level.
        /// </summary>
        [XmlAttribute()]
        public String Name;

        [XmlAttribute()]
        public bool Visible;

        /// <summary>
        /// A Level contains several Layers. Each Layer contains several Items.
        /// </summary>
        public List<Layer> Layers;

        /// <summary>
        /// A Dictionary containing any user-defined Properties.
        /// </summary>
        public SerializableDictionary CustomProperties;

        private Dictionary<string, Texture2D> _texturecache;


        public Level()
        {
            Visible = true;
            Layers = new List<Layer>();
            CustomProperties = new SerializableDictionary();
            _texturecache = new Dictionary<string, Texture2D>();
        }
        
        public Level(World world)
        {
            Visible = true;
            Layers = new List<Layer>();
            CustomProperties = new SerializableDictionary();
            _world = world;
            _texturecache = new Dictionary<string, Texture2D>();
        }

        public static Level FromFile(string filename, ContentManager cm, World world, ref Dictionary<string, Texture2D> cache)
        {
            FileStream stream = System.IO.File.Open(filename, FileMode.Open);
            XmlSerializer serializer = new XmlSerializer(typeof(Level));
            Level level = (Level)serializer.Deserialize(stream);
            stream.Close();

            foreach (Layer layer in level.Layers)
            {
                foreach (Item item in layer.Items)
                {
                    item.CustomProperties.RestoreItemAssociations(level);
                    item.load(cm, world, ref cache);
                }
            }

            return level;
        }

        public Item getItemByName(string name)
        {
            foreach (Layer layer in Layers)
            {
                foreach (Item item in layer.Items)
                {
                    if (item.Name == name) return item;
                }
            }
            return null;
        }

        public Layer getLayerByName(string name)
        {
            foreach (Layer layer in Layers)
            {
                if (layer.Name == name) return layer;
            }
            return null;
        }

        public void draw(SpriteBatch sb)
        {
            foreach (Layer layer in Layers) layer.draw(sb);
        }


    }

    


    
}
