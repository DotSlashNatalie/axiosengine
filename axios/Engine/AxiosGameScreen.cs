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
using Axios.Engine.Gleed2D;

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

        protected Level Level;

        private Camera camera;
#if WINDOWS
        AxiosCommandConsole _console = null;
#endif
        protected bool AllowKeyboardWhileConsoleIsActive = false;

#if WINDOWS
        public AxiosCommandConsole Console
        {
            get { return _console; }
            private set { _console = value; }
        }
#endif

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

#if WINDOWS
            if (obj is AxiosCommandConsole)
            {
                if (_console != null)
                {
                    //remove the current one first

                    ScreenManager.Game.Components.Remove(_console);
                    _console.Dispose();
                    _console = null;
                }
                _console = (AxiosCommandConsole)obj;
                ScreenManager.Game.Components.Add(_console);
                _console.LoadContent(ScreenManager.Game.Content);
            }
#endif
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
                AxiosLog.Instance.AddLine("[Axios Engine] - Adding objects too fast...remove " + gameobject.Name + " later", LoggingFlag.DEBUG);
                this._objectstoremove.Add(gameobject);
            }

        }

        public void RemoveAll()
        {
            AxiosLog.Instance.AddLine("[Axios Engine] - Memory usage before cleanup: " + GC.GetTotalMemory(true).ToString(), LoggingFlag.DEBUG);
            foreach (AxiosGameObject g in _gameObjects)
                g.UnloadContent(this);
            foreach (AxiosUIObject ui in _uiobjects)
                ui.UnloadContent(this);
            this.World.Clear();
            this._gameObjects.Clear();
            _timers.Clear();
            _uiobjects.Clear();
            AxiosLog.Instance.AddLine("[Axios Engine] - Memory usage after cleanup: ", LoggingFlag.DEBUG);
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
            camera = new Camera(ScreenManager.GraphicsDevice.Viewport.Width, ScreenManager.GraphicsDevice.Viewport.Height);

#if DEBUG
            EnableCameraControl = true;
#else
            EnableCameraControl = false;
#endif
        }

        public override void Draw(GameTime gameTime)
        {

            //System.Diagnostics.Debugger.Break();
            if (Level != null)
            {
                foreach (Layer layer in Level.Layers)
                {
                    Vector2 oldcameraposition = camera.Position;
                    camera.Position *= layer.ScrollSpeed;
                    
                    ScreenManager.SpriteBatch.Begin(0, null, null, null, null, null, Camera.View);
                    layer.draw(ScreenManager.SpriteBatch);
                    ScreenManager.SpriteBatch.End();

                    camera.Position = oldcameraposition;
                }
            }

            foreach (AxiosGameObject g in (from x in (from i in _gameObjects where i is IDrawableAxiosGameObject select (IDrawableAxiosGameObject)i) orderby x.DrawOrder select x))
                ((IDrawableAxiosGameObject)g).Draw(this, gameTime);

            foreach(AxiosUIObject g in (from x in _uiobjects orderby x.DrawOrder select x))
                ((IDrawableAxiosGameObject)g).Draw(this, gameTime);

            base.Draw(gameTime); //This is placed at the end so that Farseer debug information is visible
            
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

                uirect = new AxiosRectangle(ConvertUnits.ToSimUnits(uiobjpos.X), ConvertUnits.ToSimUnits(uiobjpos.Y), ConvertUnits.ToSimUnits(uiobject.Width), ConvertUnits.ToSimUnits(uiobject.Height));
                
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
#if WINDOWS
            if (_console == null || !AxiosCommandConsole.Active || (AllowKeyboardWhileConsoleIsActive && AxiosCommandConsole.Active))
#endif
            {
                base.HandleInput(gameTime, input);

                foreach (AxiosGameObject g in _gameObjects.ToList())
                    g.HandleInput(this, input, gameTime);

                foreach (AxiosUIObject g in _uiobjects.ToList())
                    g.HandleInput(this, input, gameTime);
            }
        }

        public override void Unload()
        {
            //System.Diagnostics.Debugger.Break();
            base.Deactivate();
            AxiosLog.Instance.AddLine("Memory usage before cleanup: " + GC.GetTotalMemory(true).ToString(), LoggingFlag.DEBUG);
            foreach (AxiosGameObject g in _gameObjects)
                g.UnloadContent(this);

            foreach (AxiosUIObject g in _uiobjects)
                g.UnloadContent(this);

            this._gameObjects.Clear();
            this._uiobjects.Clear();
            this.World.Clear();
            _timers.Clear();
            _uiobjects.Clear();
            AxiosLog.Instance.AddLine("Memory usage after cleanup: " + GC.GetTotalMemory(true).ToString(), LoggingFlag.DEBUG);
            //AxiosRegularFile f = new AxiosRegularFile("log.log");
            //f.WriteData(AxiosLog.Instance.GetLog(), FileMode.Append);
            //AxiosIsolatedFile f = new AxiosIsolatedFile("log.log");
            //f.WriteData(AxiosLog.Instance.GetLog(), FileMode.Append);
            //CleanUp();
#if WINDOWS
            if (_console != null)
            {
                //System.Diagnostics.Debugger.Break();
                ScreenManager.Game.Components.Remove(_console);
                _console.Dispose();
                _console = null;
            }
#endif
        }


        /// <summary>
        /// This allows you to customize functionality for loading a circle item.
        /// </summary>
        /// <param name="circleitem"></param>
        /// <returns></returns>
        public virtual bool LoadCircleItem(CircleItem circleitem)
        {
            return true;
        }

        public virtual bool LoadPathItem(PathItem pathitem)
        {
            return true;
        }

        public virtual bool LoadRectangleItem(RectangleItem rectangleitem)
        {
            return true;
        }

        public virtual bool LoadTextureItem(TextureItem textureitem)
        {
            return true;
        }

    }
}
