using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
namespace TareaEstructura_Grupo5
{
    public class Button : DrawableGameComponent
    {
        int cont = 0;
        int dato = 0;
        int datb = 0;
        int cont2 = 0;
        bool _isSelected = false;
        public event Action<Button> Click;

        #region Constructor
        public Button (Game game) :  base (game)
        {
            
            SpriteBatch = new SpriteBatch(game.GraphicsDevice);

            Position = Vector2.Zero;
            SourceRectangle = null;
            Color = Color.White;
            Scale = Vector2.One;

        }
        #endregion

        public override void Update(GameTime gameTime)
        {
            KeyboardState kbs = Keyboard.GetState();

            if (kbs.IsKeyDown(Keys.Enter) && IsSelected)
                if (Click != null) Click(this); 
        }

        #region Sprite Members

        public SpriteBatch SpriteBatch { get; set; }
        public Texture2D Texture { get; set; }
        public Texture2D TextureTextBox { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Origin { get; set; }
        public Vector2 Scale { get; set; }

        public SpriteEffects Effect { get; set; }
        public Rectangle? SourceRectangle { get; set; }
        public Color Color { get; set; }

        public float Rotation { get; set; }
        public float LayerDepth { get; set; }

        public bool IsSelected
        {
            get { return _isSelected; }
            set { _isSelected = value;
                SourceRectangle = _isSelected ? new Rectangle(197, 0, 197, Texture.Height) : new Rectangle(0,0,197,Texture.Height);
            }
        }
        #endregion

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin();
            SpriteBatch.Draw(Texture, Position, SourceRectangle, Color, Rotation, Origin, Scale, Effect, LayerDepth);
            //SpriteBatch.Draw(TextureTextBox, Position, SourceRectangle, Color, Rotation, Origin, Scale, Effect, LayerDepth);
            SpriteBatch.End();
            base.Draw(gameTime);
        }
        
    }
}
