using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TareaEstructura_Grupo5
{
    /* Tarea estructura de datos: Árbol AVL - Grupo 5
     * Jennifer Noelia Portillo Argueta PA20037
     * David Josué Vásquez Pérez VP20002
     * Freddy Anastasio Villatoro Ramírez VR20003
     */
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        //lista de botones
        List<Button> listButtons = new List<Button>();

        // INDICADOR DEL BOTON A SELECCIONAR
        int _selectedButton = 0;
        //botones
        Button btn1;
        Button btn2;
        Button btn3;

        //
        int delay=0;
        //definimos el alto y el ancho de la ppantalla como constantes 
        const int Alto = 650; //  alto de la pantalla
        const int Ancho = 1309; // ancho de la panatalla 
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            //llamamos al alto y ancho de la pantalla 
            _graphics.PreferredBackBufferWidth = Ancho;
            _graphics.PreferredBackBufferHeight = Alto;
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // para el primer boton - isnertar

            btn1 = new Button(this);
            btn1.Texture =  Content.Load<Texture2D>("INSER");
            btn1.SourceRectangle =  new Rectangle (0,0,197, btn1.Texture.Height);
            btn1.Position = new Vector2 (462 - btn1.Texture.Width/4,20);

            this.Components.Add(btn1);

            // para el segundo boton - buscar

            btn2 = new Button(this);
            btn2.Texture = Content.Load<Texture2D>("BUSC");
            btn2.SourceRectangle = new Rectangle(0, 0, 197, btn2.Texture.Height);
            btn2.Position = new Vector2(693 - btn2.Texture.Width / 4, 20);

            this.Components.Add(btn2);

            // para el tercero boton - exportar

            btn3 = new Button(this);
            btn3.Texture = Content.Load<Texture2D>("EXPOR");
            btn3.SourceRectangle = new Rectangle(0, 0, 197, btn3.Texture.Height);
            btn3.Position = new Vector2(924 - btn3.Texture.Width / 4, 20);

            this.Components.Add(btn3);

            listButtons.Add(btn1);
            listButtons.Add(btn2);
            listButtons.Add(btn3);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            delay += gameTime.ElapsedGameTime.Milliseconds;
            KeyboardState kbs = Keyboard.GetState();
            if (kbs.IsKeyDown(Keys.Down) && delay >= 300)
            {
                _selectedButton = _selectedButton - 1 > 0 ? _selectedButton - 1 : listButtons.Count - 1;
                changeButton();
                delay = 0;
            }

            if (kbs.IsKeyDown(Keys.Up) && delay >= 300)
            {
                _selectedButton = _selectedButton + 1 < listButtons.Count ? _selectedButton + 1 : 0;
                changeButton();
                delay = 0;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.BlueViolet);

            // TODO: Add your drawing code hre

            base.Draw(gameTime);
        }

        private void changeButton() // para cambiar los btones
        {
            for (int i = 0; i < listButtons.Count; i++)
            {
                if (i == _selectedButton)
                    listButtons[i].IsSelected = true;
                else
                    listButtons[i].IsSelected = false;

                
            }
        }
    }
}