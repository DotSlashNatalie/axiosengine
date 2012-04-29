using System;
using System.Collections.Generic;
using System.Linq;
using Axios.Engine.Interfaces;
using Axios.Engine.Log;
using Axios.Engine.Structures;
using Axios.Engine.UI;
using FarseerPhysics.Dynamics;
using FarseerPhysics.SamplesFramework;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using GameStateManagement;

namespace Axios.Engine
{
    
    public abstract class AxiosGameScreen : PhysicsGameScreen
    {
        private List<AxiosGameObject> _gameObjects;
        private List<AxiosGameObject> _objectstoremove = new List<AxiosGameObject>();
        private AxiosGameObject prevobj;
        private AxiosGameObject prevfocusobj;

        #region DebugTextVariables
#if DEBUG
        public SpriteFont DebugSpriteFont;
        public String DebugTextFont = "Fonts/helptext";
        public Color DebugTextColor = Color.Red;
#endif
        #endregion

        private List<AxiosTimer> _timers;
        private List<AxiosUIObject> _uiobjects;

        private AxiosUIObject prevuiobj;
        private AxiosUIObject prevuifocusobj;

        public AxiosGameScreen()
            : base()
        {
            this._gameObjects = new List<AxiosGameObject>();
            _timers = new List<AxiosTimer>();
            prevobj = null;
            prevfocusobj = null;
            this._uiobjects = new List<AxiosUIObject>();
            prevuiobj = null;
            prevuifocusobj = null;
        }

        /*public void AddGameObject<T>(T gameobject)
        {
            if (gameobject is AxiosGameObject || gameobject is AxiosUIObject)
                gameobject.LoadContent(this);

            if (gameobject is AxiosGameObject || gameobject is AxiosUIObject)
                gameobject.RemoveObject += new AxiosGameObject.RemoveAxiosGameObjectHandler(RemoveGameObject);
            
            if (gameobject is AxiosGameObject)
            {
                this._gameObjects.Add(gameobject);
            }
            else if (gameobject is AxiosUIObject)
            {
                this._uiobjects.Add(gameobject);
            }
        }*/

        /*public void AddGameObject(AxiosGameObject gameobject)
        {
            gameobject.LoadContent(this);
            gameobject.RemoveObject += new AxiosGameObject.AxiosGameObjectHandler(RemoveGameObject);
            this._gameObjects.Add(gameobject);
        }

        public void AddGameObject(AxiosTimer timer)
        {
            timer.LoadContent(this);
            _timers.Add(timer);
        }

        public void AddGameObject(AxiosUIObject uiobject)
        {
            uiobject.LoadContent(this);
            uiobject.RemoveObject += new AxiosEvents.AxiosGameObjectHandler(RemoveGameObject);
            _uiobjects.Add(uiobject);
        }*/

        public void AddGameObject(object obj)
        {

            if (obj is AxiosGameObject || obj is AxiosUIObject || obj is AxiosTimer)
            {
                AxiosGameObject tmp = obj as AxiosGameObject;
                if (obj is AxiosGameObject || obj is AxiosUIObject)
                    tmp.RemoveObject += new AxiosEvents.AxiosGameObjectHandler(RemoveGameObject);
                
                tmp.LoadContent(this);

                if (obj is AxiosGameObject && !(obj is AxiosUIObject))
                {
                    _gameObjects.Add(tmp);
                }
                else if (obj is AxiosUIObject)
                {
                    _uiobjects.Add(obj as AxiosUIObject);
                }
                else if (obj is AxiosTimer)
                {
                    _timers.Add(obj as AxiosTimer);
                }
            }

        }

        public void RemoveGameObject(AxiosTimer timer)
        {
            _timers.Remove(timer);
        }

        public void RemoveGameObject(AxiosUIObject uiobject)
        {
            uiobject.RemoveObject -= new AxiosGameObject.AxiosGameObjectHandler(RemoveGameObject);
            uiobject.UnloadContent(this);
            _uiobjects.Remove(uiobject);
        }

        public void RemoveGameObject(AxiosGameObject gameobject)
        {
            if (this._gameObjects.Contains(gameobject))
            {
                try
                {
                    gameobject.UnloadContent(this);
                    this._gameObjects.Remove(gameobject);
                }
                catch (Exception)
                {
                }
            }
            else
            {
                Singleton<AxiosLog>.Instance.AddLine("[Axios Engine] - Adding objects too fast...remove " + gameobject.Name + " later", LoggingFlag.DEBUG);
                this._objectstoremove.Add(gameobject);
            }

        }

        public void RemoveAll()
        {
            AxiosLog.Instance.AddLine("Memory usage before cleanup: " + GC.GetTotalMemory(true).ToString(), LoggingFlag.DEBUG);
            foreach (AxiosGameObject g in _gameObjects)
                g.UnloadContent(this);
            foreach (AxiosUIObject ui in _uiobjects)
                ui.UnloadContent(this);
            this.World.Clear();
            this._gameObjects.Clear();
            _timers.Clear();
            _uiobjects.Clear();
            AxiosLog.Instance.AddLine("Memory usage after cleanup: ", LoggingFlag.DEBUG);
        }

        public override void Activate(bool instancePreserved)
        {
            base.Activate(instancePreserved);

#if DEBUG
            if (!Axios.Settings.ScreenSaver)
            {
                ContentManager man = new ContentManager(ScreenManager.Game.Services, "Content");
                this.DebugSpriteFont = man.Load<SpriteFont>(this.DebugTextFont);
            }
#endif
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            foreach (AxiosGameObject g in (from x in (from i in _gameObjects where i is IDrawableAxiosGameObject select (IDrawableAxiosGameObject)i) orderby x.DrawOrder select x))
                ((IDrawableAxiosGameObject)g).Draw(this, gameTime);

            foreach(AxiosUIObject g in (from x in _uiobjects orderby x.DrawOrder select x))
                ((IDrawableAxiosGameObject)g).Draw(this, gameTime);
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);

            if (this._objectstoremove.Count > 0)
            {
                List<AxiosGameObject> list = this._objectstoremove.ToList<AxiosGameObject>();
                foreach (AxiosGameObject obj in list)
                {
                    this.RemoveGameObject(obj);
                    this._objectstoremove.Remove(obj);
                }
            }


            foreach (AxiosGameObject g in _gameObjects.ToList())
                g.Update(this, gameTime, otherScreenHasFocus, coveredByOtherScreen);

            foreach (AxiosTimer t in _timers.ToList())
                t.Update(this, gameTime, otherScreenHasFocus, coveredByOtherScreen);

            foreach(AxiosUIObject g in _uiobjects.ToList())
                g.Update(this, gameTime, otherScreenHasFocus, coveredByOtherScreen);
            
        }

        public override void HandleCursor(InputState input)
        {
            base.HandleCursor(input);
            HandleMouseEvents(input);

            foreach (AxiosGameObject g in _gameObjects.ToList())
                g.HandleCursor(this, input);
        }

        private void HandleMouseEvents(InputState input)
        {
            Vector2 position = this.Camera.ConvertScreenToWorld(input.Cursor);
            Fixture fix = this.World.TestPoint(position);
            AxiosGameObject gobj;
            if (fix != null && fix.UserData != null && fix.UserData is AxiosGameObject)
            {
                gobj = (AxiosGameObject)fix.UserData;

                if (gobj != null && gobj != prevobj)
                {
                    gobj.OnMouseHover(this, input);


                    if (prevobj != gobj && prevobj != null)
                        prevobj.OnMouseLeave(this, input);
                }
                else if (gobj != null)
                {
                    if (input.IsNewMouseButtonRelease(MouseButtons.LeftButton))
                    {
                        if (prevobj != null)
                            prevobj.OnFocusLeave(this, input);
                        gobj.OnFocusEnter(this, input);
                        gobj.OnMouseUp(this, input);
                        prevfocusobj = gobj;
                        //prevobj = gobj;
                    }

                    if (input.IsNewMouseButtonPress(MouseButtons.LeftButton))
                        gobj.OnMouseDown(this, input);
                }

                if (gobj != null)
                    prevobj = gobj;
            }
            else
            {
                if (prevobj != null)
                    prevobj.OnMouseLeave(this, input);
                if (input.IsNewMouseButtonPress(MouseButtons.LeftButton) && prevfocusobj != null)
                {
                    prevfocusobj.OnFocusLeave(this, input);
                    prevfocusobj = null;
                }
                prevobj = null;
            }

            Vector2 uiobjpos;
            //Rectangle uirect;
            AxiosRectangle uirect;
            bool foundobject = false;
            Vector2 mousepos = this.Camera.ConvertScreenToWorld(input.Cursor);
            //Vector2 objpos;
            //System.Diagnostics.Debugger.Break();
            AxiosRectangle mousrect = new AxiosRectangle(mousepos.X, mousepos.Y, ConvertUnits.ToSimUnits(25), ConvertUnits.ToSimUnits(25));
            foreach(AxiosUIObject uiobject in _uiobjects)
            {
                uiobjpos = uiobject.Position;
                //objpos = this.Camera.ConvertScreenToWorld(uiobjpos);

                uirect = new AxiosRectangle(uiobjpos.X, uiobjpos.Y, ConvertUnits.ToSimUnits(uiobject.Width), ConvertUnits.ToSimUnits(uiobject.Height));
                
                if (uirect.Intersect(mousrect))
                {

                    if (input.IsNewMouseButtonPress(MouseButtons.LeftButton))
                    {
                        uiobject.OnMouseDown(this, input);
                    }

                    if (input.IsNewMouseButtonRelease(MouseButtons.LeftButton))
                    {
                        //System.Diagnostics.Debugger.Break();
                        if (prevuifocusobj != uiobject)
                        {
                            uiobject.OnFocusEnter(this, input);
                            if (prevuifocusobj != null)
                                prevuifocusobj.OnFocusLeave(this, input);
                            prevuifocusobj = uiobject;
                        }
                        uiobject.OnMouseUp(this, input);
                    }
                    
                    if (prevuiobj != uiobject)
                    {
                        //System.Diagnostics.Debugger.Break();
                        uiobject.OnMouseHover(this, input);
                        if (prevuiobj != null)
                            prevuiobj.OnMouseLeave(this, input);
                        prevuiobj = uiobject;
                    }
                    foundobject = true;
                    break;

                }
            }
            if (!foundobject && prevuiobj != null)
            {
                //mouse moved away from object
                prevuiobj.OnMouseLeave(this, input);
                prevuiobj = null;
            }

            if (input.IsNewMouseButtonRelease(MouseButtons.LeftButton))
            {
                if (!foundobject && prevuifocusobj != null)
                {

                    prevuifocusobj.OnFocusLeave(this, input);
                    prevuifocusobj = null;
                }
            }
        }

        public override void HandleInput(GameTime gameTime, InputState input)
        {
            base.HandleInput(gameTime, input);

            foreach (AxiosGameObject g in _gameObjects.ToList())
                g.HandleInput(this, input, gameTime);

            foreach (AxiosUIObject g in _uiobjects.ToList())
                g.HandleInput(this, input, gameTime);
        }

        public override void Deactivate()
        {
            base.Deactivate();
            //AxiosLog.Instance.AddLine("Memory usage before cleanup: " + GC.GetTotalMemory(true).ToString(), LoggingFlag.DEBUG);
            foreach (AxiosGameObject g in _gameObjects)
                g.UnloadContent(this);

            foreach (AxiosUIObject g in _uiobjects)
                g.UnloadContent(this);

            this._gameObjects.Clear();
            this._uiobjects.Clear();
            this.World.Clear();
            _timers.Clear();
            _uiobjects.Clear();
            //AxiosLog.Instance.AddLine("Memory usage after cleanup: " + GC.GetTotalMemory(true).ToString(), LoggingFlag.DEBUG);
            //AxiosRegularFile f = new AxiosRegularFile("log.log");
            //f.WriteData(AxiosLog.Instance.GetLog(), FileMode.Append);
            //AxiosIsolatedFile f = new AxiosIsolatedFile("log.log");
            //f.WriteData(AxiosLog.Instance.GetLog(), FileMode.Append);
            //CleanUp();
        }

#if WINDOWS
// System.Drawing is NOT avaiable on WP7 or Xbox
        /*
         * http://stackoverflow.com/a/7394185/195722
         * 
         * 
         * 
         */
        public Texture2D GetTexture(System.Drawing.Bitmap bitmap)
        {
            BlendState oldstate = ScreenManager.GraphicsDevice.BlendState;
            ScreenManager.GraphicsDevice.BlendState = BlendState.AlphaBlend;
            Texture2D tex = new Texture2D(this.ScreenManager.GraphicsDevice, bitmap.Width, bitmap.Height, true, SurfaceFormat.Color);

            System.Drawing.Imaging.BitmapData data = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height), System.Drawing.Imaging.ImageLockMode.ReadOnly, bitmap.PixelFormat);

            int bufferSize = data.Height * data.Stride;

            //create data buffer 
            byte[] bytes = new byte[bufferSize];

            // copy bitmap data into buffer
            System.Runtime.InteropServices.Marshal.Copy(data.Scan0, bytes, 0, bytes.Length);

            // copy our buffer to the texture
            tex.SetData(bytes);

            // unlock the bitmap data
            bitmap.UnlockBits(data);

            this.ScreenManager.GraphicsDevice.BlendState = oldstate;
            return tex;
        }
#endif
    }
}
