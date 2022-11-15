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
    //esta cllase nos permite unicamente dibujar el boton, aca no se ha agregado una textura
    public class Button : DrawableGameComponent
    {
        bool _isSelected = false; // variable booleana que detecta cuando se ha seleccionado un boton
        public event Action<Button> Click; // evento click de boton

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
            KeyboardState kbs = Keyboard.GetState(); // definimos el tecado

            if (kbs.IsKeyDown(Keys.Enter) && IsSelected) // si el boton seleccionado presionamos enter
                if (Click != null) Click(this);  // si el evento click el distinto a null, se activa el evento
        }

        #region Sprite Members
        // definimos set y gets de las clase - propiedades necesarias para poder dibujar los botones
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

        public bool IsSelected //PROPIEDAD BOOLEANA QUE NOS INDICA EL BOTON SELECCIONADO
        {
            get { return _isSelected; } // retorna lo seleccionado
            set { _isSelected = value; // evalia lo seleccionado
                SourceRectangle = _isSelected ? new Rectangle(197, 0, 197, Texture.Height) : new Rectangle(0,0,197,Texture.Height); //CUANDO SE SELECCIONA DEBE CAMBIAR LA IMAGEN Y VICEVERSA, EN LA CUAL CAMBIE LA PROPIEDAD BOOLEANA CAMBIA LA FUENTE DEL RECTANGULO DE LA TEXTURA
            }
        }
        #endregion

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin();
            SpriteBatch.Draw(Texture, Position, SourceRectangle, Color, Rotation, Origin, Scale, Effect, LayerDepth); // se dibuja el boton
            //SpriteBatch.Draw(TextureTextBox, Position, SourceRectangle, Color, Rotation, Origin, Scale, Effect, LayerDepth);
            SpriteBatch.End();
            base.Draw(gameTime);
        }
        
    }
}
