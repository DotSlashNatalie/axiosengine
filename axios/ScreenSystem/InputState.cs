#region File Description
//-----------------------------------------------------------------------------
// InputState.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using FarseerPhysics.SamplesFramework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace GameStateManagement
{

    /// <summary>
    ///   an enum of all available mouse buttons.
    /// </summary>
    public enum MouseButtons
    {
        LeftButton,
        MiddleButton,
        RightButton,
        ExtraButton1,
        ExtraButton2
    }

    /// <summary>
    /// Helper for reading input from keyboard, gamepad, and touch input. This class 
    /// tracks both the current and previous state of the input devices, and implements 
    /// query methods for high level input actions such as "move up through the menu"
    /// or "pause the game".
    /// </summary>
    public class InputState
    {
        public const int MaxInputs = 4;

        public readonly KeyboardState[] CurrentKeyboardStates;
        public readonly GamePadState[] CurrentGamePadStates;

        public readonly KeyboardState[] LastKeyboardStates;
        public readonly GamePadState[] LastGamePadStates;

        public readonly bool[] GamePadWasConnected;

        /*
         * Needed for virtual stick on WP7
         *  -- Nathan Adams [adamsna@datanethost.net] - 4/12/2012
         */
        private GamePadState _currentVirtualState;
        private GamePadState _lastVirtualState;
        private bool _handleVirtualStick;
        /*
         * I didn't create an array for the virtual stick because there will only be one
         *  -- Nathan Adams [adamsna@datanethost.net] - 4/12/2012
         */


        /*
         * Adding variables for the cursor 
         *  -- Nathan Adams [adamsna@datanethost.net] - 4/15/2012
         * 
         */
        private MouseState _currentMouseState;
        private MouseState _lastMouseState;
        
        private Vector2 _cursor;
        private bool _cursorIsValid;
        private bool _cursorIsVisible;
        private bool _cursorMoved;
        private Sprite _cursorSprite;

#if WINDOWS_PHONE
        private VirtualStick _phoneStick;
        private VirtualButton _phoneA;
        private VirtualButton _phoneB;
#endif

        public TouchCollection TouchState;

        public readonly List<GestureSample> Gestures = new List<GestureSample>();

        private ScreenManager _manager;
        private Viewport _viewport;
        

        /// <summary>
        /// Constructs a new input state.
        /// </summary>
        public InputState(ScreenManager manager)
        {
            _manager = manager;
            CurrentKeyboardStates = new KeyboardState[MaxInputs];
            CurrentGamePadStates = new GamePadState[MaxInputs];

            LastKeyboardStates = new KeyboardState[MaxInputs];
            LastGamePadStates = new GamePadState[MaxInputs];

            GamePadWasConnected = new bool[MaxInputs];
            _currentVirtualState = new GamePadState();
            _lastVirtualState = new GamePadState();

            _cursorIsVisible = false;
            _cursorMoved = false;
#if WINDOWS_PHONE
            _cursorIsValid = false;
#else
            _cursorIsValid = true;
#endif
            _cursor = Vector2.Zero;

            _handleVirtualStick = false;
        }

        public MouseState MouseState
        {
            get { return _currentMouseState; }
        }

        public GamePadState VirtualState
        {
            get { return _currentVirtualState; }
        }

        public MouseState PreviousMouseState
        {
            get { return _lastMouseState; }
        }

        public GamePadState PreviousVirtualState
        {
            get { return _lastVirtualState; }
        }

        public bool ShowCursor
        {
            get { return _cursorIsVisible && _cursorIsValid; }
            set { _cursorIsVisible = value; }
        }

        public bool EnableVirtualStick
        {
            get { return _handleVirtualStick; }
            set { _handleVirtualStick = value; }
        }

        public Vector2 Cursor
        {
            get { return _cursor; }
        }

        public bool IsCursorMoved
        {
            get { return _cursorMoved; }
        }

        public bool IsCursorValid
        {
            get { return _cursorIsValid; }
        }

        public void LoadContent()
        {
            ContentManager man = new ContentManager(_manager.Game.Services, "Content");
            _cursorSprite = new Sprite(man.Load<Texture2D>("Common/cursor"));
#if WINDOWS_PHONE
            // virtual stick content
            _phoneStick = new VirtualStick(man.Load<Texture2D>("Common/socket"),
                                           man.Load<Texture2D>("Common/stick"), new Vector2(80f, 400f));

            Texture2D temp = man.Load<Texture2D>("Common/buttons");
            _phoneA = new VirtualButton(temp, new Vector2(695f, 380f), new Rectangle(0, 0, 40, 40), new Rectangle(0, 40, 40, 40));
            _phoneB = new VirtualButton(temp, new Vector2(745f, 360f), new Rectangle(40, 0, 40, 40), new Rectangle(40, 40, 40, 40));
#endif
            _viewport = _manager.GraphicsDevice.Viewport;
        }

        private GamePadState HandleVirtualStickWin()
        {
            Vector2 _leftStick = Vector2.Zero;
            List<Buttons> _buttons = new List<Buttons>();
            PlayerIndex pout;
            if (IsNewKeyPress(Keys.A, PlayerIndex.One, out pout))
            {
                _leftStick.X -= 1f;
            }
            if (IsNewKeyPress(Keys.S, PlayerIndex.One, out pout))
            {
                _leftStick.Y -= 1f;
            }
            if (IsNewKeyPress(Keys.D, PlayerIndex.One, out pout))
            {
                _leftStick.X += 1f;
            }
            if (IsNewKeyPress(Keys.W, PlayerIndex.One, out pout))
            {
                _leftStick.Y += 1f;
            }
            if (IsNewKeyPress(Keys.Space, PlayerIndex.One, out pout))
            {
                _buttons.Add(Buttons.A);
            }
            if (IsNewKeyPress(Keys.LeftControl, PlayerIndex.One, out pout))
            {
                _buttons.Add(Buttons.B);
            }
            if (_leftStick != Vector2.Zero)
            {
                _leftStick.Normalize();
            }

            return new GamePadState(_leftStick, Vector2.Zero, 0f, 0f, _buttons.ToArray());
        }

        private GamePadState HandleVirtualStickWP7()
        {
            List<Buttons> _buttons = new List<Buttons>();
            Vector2 _stick = Vector2.Zero;
#if WINDOWS_PHONE
            _phoneA.Pressed = false;
            _phoneB.Pressed = false;
            TouchCollection touchLocations = TouchPanel.GetState();
            foreach (TouchLocation touchLocation in touchLocations)
            {
                _phoneA.Update(touchLocation);
                _phoneB.Update(touchLocation);
                _phoneStick.Update(touchLocation);
            }
            if (_phoneA.Pressed)
            {
                _buttons.Add(Buttons.A);
            }
            if (_phoneB.Pressed)
            {
                _buttons.Add(Buttons.B);
            }
            _stick = _phoneStick.StickPosition;
#endif
            return new GamePadState(_stick, Vector2.Zero, 0f, 0f, _buttons.ToArray());
        }

        public void Draw()
        {
            if (_cursorIsVisible && _cursorIsValid)
            {
                _manager.SpriteBatch.Begin();
                _manager.SpriteBatch.Draw(_cursorSprite.Texture, _cursor, null, Color.White, 0f, _cursorSprite.Origin, 1f, SpriteEffects.None, 0f);
                _manager.SpriteBatch.End();
            }
#if WINDOWS_PHONE
            if (_handleVirtualStick)
            {
                _manager.SpriteBatch.Begin();
                _phoneA.Draw(_manager.SpriteBatch);
                _phoneB.Draw(_manager.SpriteBatch);
                _phoneStick.Draw(_manager.SpriteBatch);
                _manager.SpriteBatch.End();
            }
#endif
        }

        /// <summary>
        /// Reads the latest state user input.
        /// </summary>
        public void Update(GameTime gameTime)
        {
            PlayerIndex p;
            _lastMouseState = _currentMouseState;
            if (_handleVirtualStick)
            {
                _lastVirtualState = _currentVirtualState;
            }

            _currentMouseState = Mouse.GetState();

            if (_handleVirtualStick)
            {
#if XBOX
            _currentVirtualState= GamePad.GetState(PlayerIndex.One);
#elif WINDOWS
                if (GamePad.GetState(PlayerIndex.One).IsConnected)
                {
                    _currentVirtualState = GamePad.GetState(PlayerIndex.One);
                }
                else
                {
                    _currentVirtualState = HandleVirtualStickWin();
                }
#elif WINDOWS_PHONE
                _currentVirtualState = HandleVirtualStickWP7();
#endif
            }
            for (int i = 0; i < MaxInputs; i++)
            {
                LastKeyboardStates[i] = CurrentKeyboardStates[i];
                LastGamePadStates[i] = CurrentGamePadStates[i];

                CurrentKeyboardStates[i] = Keyboard.GetState((PlayerIndex)i);
                CurrentGamePadStates[i] = GamePad.GetState((PlayerIndex)i);

                // Keep track of whether a gamepad has ever been
                // connected, so we can detect if it is unplugged.
                if (CurrentGamePadStates[i].IsConnected)
                {
                    GamePadWasConnected[i] = true;
                }
            }

            // Get the raw touch state from the TouchPanel
            TouchState = TouchPanel.GetState();

            // Read in any detected gestures into our list for the screens to later process
            Gestures.Clear();
            while (TouchPanel.IsGestureAvailable)
            {
                //System.Diagnostics.Debugger.Break();
                Gestures.Add(TouchPanel.ReadGesture());
            }
            //System.Diagnostics.Debugger.Break();

            // Update cursor
            Vector2 oldCursor = _cursor;

            if (CurrentGamePadStates[0].IsConnected && CurrentGamePadStates[0].ThumbSticks.Left != Vector2.Zero)
            {
                Vector2 temp = CurrentGamePadStates[0].ThumbSticks.Left;
                _cursor += temp * new Vector2(300f, -300f) * (float)gameTime.ElapsedGameTime.TotalSeconds;
                Mouse.SetPosition((int)_cursor.X, (int)_cursor.Y);
            }
            else
            {
                _cursor.X = _currentMouseState.X;
                _cursor.Y = _currentMouseState.Y;
            }

            if (this.IsNewKeyPress(Keys.P, PlayerIndex.One, out p))
                Console.WriteLine(_cursor.ToString());

            _cursor.X = MathHelper.Clamp(_cursor.X, 0f, _viewport.Width);
            _cursor.Y = MathHelper.Clamp(_cursor.Y, 0f, _viewport.Height);

            if (this.IsNewKeyPress(Keys.P, PlayerIndex.One, out p))
                Console.WriteLine(_cursor.ToString());

            if (_cursorIsValid && oldCursor != _cursor)
            {
                _cursorMoved = true;
            }
            else
            {
                _cursorMoved = false;
            }

#if WINDOWS
            if (_viewport.Bounds.Contains(_currentMouseState.X, _currentMouseState.Y))
            {
                _cursorIsValid = true;
            }
            else
            {
                _cursorIsValid = false;
            }
#elif WINDOWS_PHONE
            if (_currentMouseState.LeftButton == ButtonState.Pressed)
            {
                _cursorIsValid = true;
            }
            else
            {
                _cursorIsValid = false;
            }
#endif
            
            if (this.IsNewKeyPress(Keys.P, PlayerIndex.One, out p))
                Console.WriteLine(_viewport.ToString());
        }


        /// <summary>
        /// Helper for checking if a key was pressed during this update. The
        /// controllingPlayer parameter specifies which player to read input for.
        /// If this is null, it will accept input from any player. When a keypress
        /// is detected, the output playerIndex reports which player pressed it.
        /// </summary>
        public bool IsKeyPressed(Keys key, PlayerIndex? controllingPlayer, out PlayerIndex playerIndex)
        {
            if (controllingPlayer.HasValue)
            {
                // Read input from the specified player.
                playerIndex = controllingPlayer.Value;

                int i = (int)playerIndex;

                return CurrentKeyboardStates[i].IsKeyDown(key);
            }
            else
            {
                // Accept input from any player.
                return (IsKeyPressed(key, PlayerIndex.One, out playerIndex) ||
                        IsKeyPressed(key, PlayerIndex.Two, out playerIndex) ||
                        IsKeyPressed(key, PlayerIndex.Three, out playerIndex) ||
                        IsKeyPressed(key, PlayerIndex.Four, out playerIndex));
            }
        }


        /// <summary>
        /// Helper for checking if a button was pressed during this update.
        /// The controllingPlayer parameter specifies which player to read input for.
        /// If this is null, it will accept input from any player. When a button press
        /// is detected, the output playerIndex reports which player pressed it.
        /// </summary>
        public bool IsButtonPressed(Buttons button, PlayerIndex? controllingPlayer, out PlayerIndex playerIndex)
        {
            if (controllingPlayer.HasValue)
            {
                // Read input from the specified player.
                playerIndex = controllingPlayer.Value;

                int i = (int)playerIndex;

                return CurrentGamePadStates[i].IsButtonDown(button);
            }
            else
            {
                // Accept input from any player.
                return (IsButtonPressed(button, PlayerIndex.One, out playerIndex) ||
                        IsButtonPressed(button, PlayerIndex.Two, out playerIndex) ||
                        IsButtonPressed(button, PlayerIndex.Three, out playerIndex) ||
                        IsButtonPressed(button, PlayerIndex.Four, out playerIndex));
            }
        }


        /// <summary>
        /// Helper for checking if a key was newly pressed during this update. The
        /// controllingPlayer parameter specifies which player to read input for.
        /// If this is null, it will accept input from any player. When a keypress
        /// is detected, the output playerIndex reports which player pressed it.
        /// </summary>
        public bool IsNewKeyPress(Keys key, PlayerIndex? controllingPlayer, out PlayerIndex playerIndex)
        {
            if (controllingPlayer.HasValue)
            {
                // Read input from the specified player.
                playerIndex = controllingPlayer.Value;

                int i = (int)playerIndex;

                return (CurrentKeyboardStates[i].IsKeyDown(key) &&
                        LastKeyboardStates[i].IsKeyUp(key));
            }
            else
            {
                // Accept input from any player.
                return (IsNewKeyPress(key, PlayerIndex.One, out playerIndex) ||
                        IsNewKeyPress(key, PlayerIndex.Two, out playerIndex) ||
                        IsNewKeyPress(key, PlayerIndex.Three, out playerIndex) ||
                        IsNewKeyPress(key, PlayerIndex.Four, out playerIndex));
            }
        }

        public bool IsNewKeyRelease(Keys key, PlayerIndex? controllingPlayer, out PlayerIndex playerIndex)
        {
            if (controllingPlayer.HasValue)
            {
                // Read input from the specified player.
                playerIndex = controllingPlayer.Value;

                int i = (int)playerIndex;

                return (CurrentKeyboardStates[i].IsKeyUp(key) &&
                        LastKeyboardStates[i].IsKeyDown(key));
            }
            else
            {
                // Accept input from any player.
                return (IsNewKeyRelease(key, PlayerIndex.One, out playerIndex) ||
                        IsNewKeyRelease(key, PlayerIndex.Two, out playerIndex) ||
                        IsNewKeyRelease(key, PlayerIndex.Three, out playerIndex) ||
                        IsNewKeyRelease(key, PlayerIndex.Four, out playerIndex));
            }
        }


        /// <summary>
        /// Helper for checking if a button was newly pressed during this update.
        /// The controllingPlayer parameter specifies which player to read input for.
        /// If this is null, it will accept input from any player. When a button press
        /// is detected, the output playerIndex reports which player pressed it.
        /// </summary>
        public bool IsNewButtonPress(Buttons button, PlayerIndex? controllingPlayer, out PlayerIndex playerIndex)
        {
            if (controllingPlayer.HasValue)
            {
                // Read input from the specified player.
                playerIndex = controllingPlayer.Value;

                int i = (int)playerIndex;

                return (CurrentGamePadStates[i].IsButtonDown(button) &&
                        LastGamePadStates[i].IsButtonUp(button));
            }
            else
            {
                // Accept input from any player.
                return (IsNewButtonPress(button, PlayerIndex.One, out playerIndex) ||
                        IsNewButtonPress(button, PlayerIndex.Two, out playerIndex) ||
                        IsNewButtonPress(button, PlayerIndex.Three, out playerIndex) ||
                        IsNewButtonPress(button, PlayerIndex.Four, out playerIndex));
            }
        }

        public bool IsNewButtonRelease(Buttons button, PlayerIndex? controllingPlayer, out PlayerIndex playerIndex)
        {
            if (controllingPlayer.HasValue)
            {
                // Read input from the specified player.
                playerIndex = controllingPlayer.Value;

                int i = (int)playerIndex;

                return (CurrentGamePadStates[i].IsButtonUp(button) &&
                        LastGamePadStates[i].IsButtonDown(button));
            }
            else
            {
                // Accept input from any player.
                return (IsNewButtonRelease(button, PlayerIndex.One, out playerIndex) ||
                        IsNewButtonRelease(button, PlayerIndex.Two, out playerIndex) ||
                        IsNewButtonRelease(button, PlayerIndex.Three, out playerIndex) ||
                        IsNewButtonRelease(button, PlayerIndex.Four, out playerIndex));
            }
        }

        public bool IsNewVirtualButtonPress(Buttons button)
        {
            return (_lastVirtualState.IsButtonUp(button) &&
                    _currentVirtualState.IsButtonDown(button));
        }

        public bool IsNewVirtualButtonRelease(Buttons button)
        {
            return (_lastVirtualState.IsButtonDown(button) &&
                    _currentVirtualState.IsButtonUp(button));
        }

        /// <summary>
        ///   Helper for checking if a mouse button was newly pressed during this update.
        /// </summary>
        public bool IsNewMouseButtonPress(MouseButtons button)
        {
            switch (button)
            {
                case MouseButtons.LeftButton:
                    return (_currentMouseState.LeftButton == ButtonState.Pressed &&
                            _lastMouseState.LeftButton == ButtonState.Released);
                case MouseButtons.RightButton:
                    return (_currentMouseState.RightButton == ButtonState.Pressed &&
                            _lastMouseState.RightButton == ButtonState.Released);
                case MouseButtons.MiddleButton:
                    return (_currentMouseState.MiddleButton == ButtonState.Pressed &&
                            _lastMouseState.MiddleButton == ButtonState.Released);
                case MouseButtons.ExtraButton1:
                    return (_currentMouseState.XButton1 == ButtonState.Pressed &&
                            _lastMouseState.XButton1 == ButtonState.Released);
                case MouseButtons.ExtraButton2:
                    return (_currentMouseState.XButton2 == ButtonState.Pressed &&
                            _lastMouseState.XButton2 == ButtonState.Released);
                default:
                    return false;
            }
        }

        /// <summary>
        /// Checks if the requested mouse button is released.
        /// </summary>
        /// <param name="button">The button.</param>
        public bool IsNewMouseButtonRelease(MouseButtons button)
        {
            switch (button)
            {
                case MouseButtons.LeftButton:
                    return (_lastMouseState.LeftButton == ButtonState.Pressed &&
                            _currentMouseState.LeftButton == ButtonState.Released);
                case MouseButtons.RightButton:
                    return (_lastMouseState.RightButton == ButtonState.Pressed &&
                            _currentMouseState.RightButton == ButtonState.Released);
                case MouseButtons.MiddleButton:
                    return (_lastMouseState.MiddleButton == ButtonState.Pressed &&
                            _currentMouseState.MiddleButton == ButtonState.Released);
                case MouseButtons.ExtraButton1:
                    return (_lastMouseState.XButton1 == ButtonState.Pressed &&
                            _currentMouseState.XButton1 == ButtonState.Released);
                case MouseButtons.ExtraButton2:
                    return (_lastMouseState.XButton2 == ButtonState.Pressed &&
                            _currentMouseState.XButton2 == ButtonState.Released);
                default:
                    return false;
            }
        }
    }
}
