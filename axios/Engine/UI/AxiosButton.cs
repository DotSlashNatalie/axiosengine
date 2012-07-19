using FarseerPhysics.SamplesFramework;
using Microsoft.Xna.Framework.Graphics;
using GameStateManagement;

namespace Axios.Engine.UI
{
    public class AxiosButton : AxiosUIObject
    {
        protected Texture2D _hovertexture;
        protected Texture2D _clicktexture;
        protected Texture2D _normaltexture;

        /// <summary>
        /// HoverTexture is the texture that will be set when the mouse hovers over the button
        /// </summary>
        public Texture2D HoverTexture
        {
            get { return this._hovertexture; }
            set { this._hovertexture = value; }
        }

        /// <summary>
        /// The ClickTexture is the texture that will be set when the user clicks on the button
        /// </summary>
        public Texture2D ClickTexture
        {
            get { return this._clicktexture; }
            set { this._clicktexture = value; }
        }

        /// <summary>
        /// The normal texture is the texture when the button is not active
        /// </summary>
        public Texture2D NormalTexture
        {
            get { return this._normaltexture; }
            set { this._normaltexture = value; this.Texture = value; }
        }

        public AxiosButton()
        {
            
        }

        public override void LoadContent(AxiosGameScreen gameScreen)
        {
            base.LoadContent(gameScreen);

        }

        public override void OnMouseHover(AxiosGameScreen gameScreen, InputState input)
        {
            base.OnMouseHover(gameScreen, input);
            
            this.Texture = _hovertexture;
            
        }

        public override void OnMouseLeave(AxiosGameScreen gameScreen, InputState input)
        {
            base.OnMouseLeave(gameScreen, input);

            this.Texture = _normaltexture;
        }

        public override void OnMouseDown(AxiosGameScreen gameScreen, InputState input)
        {
            base.OnMouseDown(gameScreen, input);

            this.Texture = _clicktexture;
        }

        public override void OnMouseUp(AxiosGameScreen gameScreen, InputState input)
        {
            base.OnMouseUp(gameScreen, input);

            this.Texture = _hovertexture;

            
        }



        public override void HandleCursor(AxiosGameScreen gameScreen, InputState input)
        {
            base.HandleCursor(gameScreen, input);


        }
    }
}
