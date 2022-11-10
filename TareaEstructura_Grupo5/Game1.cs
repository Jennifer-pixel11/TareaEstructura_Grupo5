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
        DibujarArbolValanceado arbolAVL = new DibujarArbolValanceado(null);
        DibujarArbolValanceado arbolAVL_Letra = new DibujarArbolValanceado(null);
        //
        int delay=0;
        //definimos el alto y el ancho de la ppantalla como constantes 
        const int Alto = 650; //  alto de la pantalla
        const int Ancho = 1309; // ancho de la panatalla 

        // variables para los textbox
   
        private TextBox _TextBoxNumeros;
        private Vector2 _PosicionTextBox;
        private SpriteFont _fuente;
        private int _longitud;
        private MouseState _estadoMouse;
        private KeyboardState _estadoKeyboard;
        private int _tiempoAccion;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            //llamamos al alto y ancho de la pantalla 
            _graphics.PreferredBackBufferWidth = Ancho;
            _graphics.PreferredBackBufferHeight = Alto;
            //posicion donde aparecera el textbox
            _PosicionTextBox = new Vector2(100, 20);
            _longitud = 10;
            _tiempoAccion = 10;
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
            _fuente = Content.Load<SpriteFont>("texto");
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            //creando el textbox
            _TextBoxNumeros = new TextBox(CargarTextura("textbox"), new Point(122, 24), new Point(2, 12), _PosicionTextBox, _longitud, true, true, _fuente, String.Empty, 0.9f);

           
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _estadoKeyboard = EstadoKeyboard.GetState(); // 
            _estadoMouse = EstadoMouse.GetState(); //
            
            Entrada(gameTime); 
            _TextBoxNumeros.Update(); // traemos el update de la clase button en la cual esta la condicion del evento del boton
            

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
            _spriteBatch.Begin(SpriteSortMode.FrontToBack);
            _TextBoxNumeros.Render(_spriteBatch);
            _spriteBatch.End();

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

        protected void Entrada(GameTime gameTime)
        {
            Keys[] keys = _estadoKeyboard.GetPressedKeys();
            String value = String.Empty;

            if (_estadoMouse.LeftButton == ButtonState.Pressed)
            {
                if (EstadoMouse.NoClick(true))
                {
                    ClickIzquierdo();
                }
            }
            if (_estadoKeyboard.IsKeyUp(Keys.Back) && _estadoKeyboard.IsKeyUp(Keys.Delete))
            {
                _tiempoAccion = 10;
            }
            if (keys.Count() > 0)
            {
                if (keys.Count() > 1)
                {
                    keys[0] = ExtraerNumero(keys);
                }
                if (_estadoKeyboard.IsKeyDown(Keys.Back) || _estadoKeyboard.IsKeyDown(Keys.Delete))
                {
                    if (_tiempoAccion == 0)
                    {
                        _tiempoAccion = 10;
                    }
                    if (_tiempoAccion == 10)
                    {
                        if (_TextBoxNumeros.Seleccionado)
                        {
                            _TextBoxNumeros.AgregarTexto('\b');
                        }
                        _tiempoAccion--;
                    }
                    else
                    {
                        _tiempoAccion--;
                    }
                    return;
                }
                if (_TextBoxNumeros.Seleccionado)
                {
                    if (((int)keys[0] >= 48 && (int)keys[0] <= 57) || ((int)keys[0] >= 96 && (int)keys[0] <= 105))
                    {
                        value = keys[0].ToString().Substring(keys[0].ToString().Length - 1);

                        if (EstadoKeyboard.TeclaNoPrecionada(keys[0]))
                        {
                            _TextBoxNumeros.AgregarTexto(value.ToCharArray()[0]);
                        }
                    }
                }

            }
        }

        private Texture2D CargarTextura(string textureName) // funcion para cargar la textura 
        {
            return Content.Load<Texture2D>(textureName);
        }

        private void ClickIzquierdo()
        {
            if (_estadoMouse.X >= _TextBoxNumeros.Posicion.X && _estadoMouse.X <= _TextBoxNumeros.Posicion.X + _TextBoxNumeros.AnchoTextBox)
            {
                if (_estadoMouse.Y >= _TextBoxNumeros.Posicion.Y && _estadoMouse.Y <= _TextBoxNumeros.Posicion.Y + _TextBoxNumeros.AltoTextBox)
                {
                    _TextBoxNumeros.Seleccionado = true;
                }
            }
        }

        private Keys ExtraerNumero(Keys[] keys)
        {
            foreach (Keys key in keys)
            {
                if ((int)key >= 48 && (int)key <= 105)
                {
                    return key;
                }
            }
            return Keys.None;
        }
    }
}