using System;
using FarseerPhysics.SamplesFramework;
using GameStateManagement;

namespace Axios.Engine
{
    public abstract class AxiosEvents
    {
        protected Boolean _hasFocus;

        public bool HasFocus
        {
            get
            {
                return this._hasFocus;
            }
            set
            {
                this._hasFocus = value;
            }
        }

        public delegate void AxiosHandler(object sender, AxiosGameScreen gameScreen, InputState input);
        
        public delegate void AxiosGameObjectHandler(AxiosGameObject sender);

        #region GameObjectEventMethods

        public virtual void OnFocusEnter(AxiosGameScreen gameScreen, InputState input)
        {
            this.HasFocus = true;
            this.OnEvent(FocusEnter, gameScreen, input);
        }

        public virtual void OnFocusLeave(AxiosGameScreen gameScreen, InputState input)
        {
            this.HasFocus = false;
            this.OnEvent(FocusLeave, gameScreen, input);
        }

        public virtual void OnMouseHover(AxiosGameScreen gameScreen, InputState input)
        {
            this.OnEvent(MouseHover, gameScreen, input);
        }

        public virtual void OnMouseLeave(AxiosGameScreen gameScreen, InputState input)
        {
            this.OnEvent(MouseLeave, gameScreen, input);
        }

        public virtual void OnValueChange(AxiosGameScreen gameScreen, InputState input)
        {
            this.OnEvent(ValueChange, gameScreen, input);
        }

        public virtual void OnMouseDown(AxiosGameScreen gameScreen, InputState input)
        {
            this.OnEvent(MouseDown, gameScreen, input);
        }

        public virtual void OnMouseUp(AxiosGameScreen gameScreen, InputState input)
        {
            this.OnEvent(MouseUp, gameScreen, input);
        }

        public virtual void OnScaleChange(AxiosGameObject gameObject)
        {
            if (this.ScaleChanged != null)
                this.ScaleChanged(gameObject);
        }

        private void OnEvent(AxiosHandler e, AxiosGameScreen gameScreen, InputHelper input)
        {
            AxiosHandler handle = e;
            if (handle != null)
                handle(this, gameScreen, input);
        }

        #endregion

        #region GameObjectEvents

        /// <summary>
        /// This event is fired when the the object looses focus
        /// </summary>
        /// <param name="sender">The object sending the event</param>
        /// <param name="gameScreen">The gamescreen that this happened on</param>
        /// <param name="kworld">The current version of the kosmos world</param>
        public event AxiosHandler FocusLeave;

        public event AxiosHandler MouseHover;

        public event AxiosHandler MouseLeave;

        public event AxiosHandler MouseDown;

        public event AxiosHandler MouseUp;

        /// <summary>
        /// This event is fired when the the object gains focus
        /// </summary>
        /// <param name="sender">The object sending the event</param>
        /// <param name="gameScreen">The gamescreen that this happened on</param>
        /// <param name="kworld">The current version of the kosmos world</param>
        public event AxiosHandler FocusEnter;

        /// <summary>
        /// This event is fired when the object's value changes
        /// </summary>
        /// <param name="sender">The object sending the event</param>
        /// <param name="gameScreen">The gamescreen that this happened on</param>
        /// <param name="kworld">The current version of the kosmos world</param>
        public event AxiosHandler ValueChange;

        public event AxiosGameObjectHandler RemoveObject;

        public event AxiosGameObjectHandler ScaleChanged;

        #endregion

        protected virtual void OnRemove(AxiosGameObject gameObject)
        {
            RemoveObject(gameObject);
        }

        protected void RemoveEvents()
        {
            this.MouseDown = null;
            this.MouseHover = null;
            this.MouseLeave = null;
            this.MouseUp = null;
            this.FocusEnter = null;
            this.FocusLeave = null;
        }
    }
}
