using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
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
        //opciones para las iteraciones de mouse y botones de menu
        private bool opcion1 = false;
        private bool opcion2 = false;
        private bool opcion3 = false;
        string datoingresar = "";
        public string cadenaPreorden { get; set; }
        //lista de botones
        List<Button> listButtons = new List<Button>(); // lista para los botones de la clase buttons para tener cada uno de los botones creados

        
        int _selectedButton = 0;// INDICADOR DEL BOTON A SELECCIONAR
        //instancia de los botones
        Button btn1; // boton insertar
        Button btn2; //boton buscar
        Button btn3; //boton exportar
        DibujarArbolValanceado arbolAVL = new DibujarArbolValanceado(null); //creacion del objeto arbolAVL

       // DibujarArbolValanceado arbolAVL_Letra = new DibujarArbolValanceado(null);
        //Graphics g;
        int pintaR = 0;

        int cont = 0;
        char dato;
        int datb = 0;
        int cont2 = 0;
        Graphics g;
        //int pintaR = 0;
        Microsoft.Xna.Framework.Color _color = Microsoft.Xna.Framework.Color.White;
        
        //
        int delay=0;
        //definimos el alto y el ancho de la ppantalla como constantes 
        const int Alto = 650; //  alto de la pantalla
        const int Ancho = 1309; // ancho de la panatalla 

        // variables para los textbox
      //  private TextBox textoActual;
        private TextBox _TextBoxNumeros;
        private Vector2 _PosicionTextBox;
        private SpriteFont _fuente,fuentenodo;
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
            //_TextBoxNumeros.TextoActual(dato);
        


        }

        protected override void Initialize()
        {

            /********************************************INICIALIZAMOS BOTONES*********************************************************/
            // para el primer boton - isnertar
           
            btn1 = new Button(this); // inicializamos el boton insertar
            //setteamos las propiedades del boton
            btn1.Texture =  Content.Load<Texture2D>("INSER"); //cargamos la imagen
            btn1.SourceRectangle =  new Microsoft.Xna.Framework.Rectangle(0,0,197, btn1.Texture.Height); //definimos el rectangulo para el boton insertar
            btn1.Position = new Vector2 (462 - btn1.Texture.Width/4,20); // definimos la posicion - se divide en 4 ya que son dos texturas en una
            //USAMOS EXPRESION LANDA
          //  btn1.Click += btn => _color = Microsoft.Xna.Framework.Color.Orange;

            // para el segundo boton - buscar

            btn2 = new Button(this);//inicializamos el boton buscar 
            btn2.Texture = Content.Load<Texture2D>("BUSC");//cargamos la imagen
            btn2.SourceRectangle = new Microsoft.Xna.Framework.Rectangle(0, 0, 197, btn2.Texture.Height);//definimos el rectangulo para el boton buscar
            btn2.Position = new Vector2(693 - btn2.Texture.Width / 4, 20);// definimos la posicion
           // btn2.Click += btn => _color = Microsoft.Xna.Framework.Color.Blue;


            // para el tercero boton - exportar

            btn3 = new Button(this);// inicializamos el boton exportar
            btn3.Texture = Content.Load<Texture2D>("EXPOR");//cargamos la imagen
            btn3.SourceRectangle = new Microsoft.Xna.Framework.Rectangle(0, 0, 197, btn3.Texture.Height);// definimos el rectangulo para el boton exportar
            btn3.Position = new Vector2(924 - btn3.Texture.Width / 4, 20);// definimos la posicion-se divide en 4 la textura ya que son dos texturas en una
           // btn3.Click += btn => Exit();


            //agregamos los botones a la lista 
            listButtons.Add(btn1);
            listButtons.Add(btn2);
            listButtons.Add(btn3);

            this.Components.Add(btn1); //lista de componentes del juego y agregamos el boton insertar
            this.Components.Add(btn2); // lista de componenetes del juego y agregamos el boton buscar
            this.Components.Add(btn3); /// lista de componentes del jueg y agregamos el boton exportar
            base.Initialize();
        }

        protected override void LoadContent()
        {
            fuentenodo = Content.Load<SpriteFont>("texto");
            _fuente = Content.Load<SpriteFont>("texto");
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            //creando el textbox
            _TextBoxNumeros = new TextBox(CargarTextura("textbox"), new Microsoft.Xna.Framework.Point(122, 24), new Microsoft.Xna.Framework.Point(2, 12), _PosicionTextBox, _longitud, true, true, _fuente, String.Empty, 0.9f);

           
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _estadoKeyboard = EstadoKeyboard.GetState(); // 
            _estadoMouse = EstadoMouse.GetState(); //
            
            Entrada(gameTime); 
            _TextBoxNumeros.Update(); // traemos el update de la clase button en la cual esta la condicion del evento del boton
            

            delay += gameTime.ElapsedGameTime.Milliseconds; // definimos un delay para cuando se vea el efecto de seleccion de boton, por repeticion ira sumando los milisegundos
            KeyboardState kbs = Keyboard.GetState(); // INSTANCIA DEL KEYBOARD

            if (kbs.IsKeyDown(Keys.Down) && delay >= 300) // si se presiona la tecla hacia abajo y tendra un delay mayor o igual a 300 permitira realizar el cambio de boton 
            {
                //si llega a 0 que vuelva al del principio
                _selectedButton = _selectedButton - 1 > 0 ? _selectedButton - 1 : listButtons.Count - 1; // tiene que cambiar el indice seleccionado
                changeButton();// cambia de boton o de la imagen ya que crea un efecto de seleccion
                delay = 0; // delay vuelve a 0
            }

            if (kbs.IsKeyDown(Keys.Up) && delay >= 300) // si se presiona la tecla arriba y tendra un delay mayor o igual a 300
            {
                _selectedButton = _selectedButton + 1 < listButtons.Count ? _selectedButton + 1 : 0; // 
                changeButton();// cambia de boton o de la imagen ya que crea un efecto de seleccion
                delay = 0;//delay vuelve a 0
            }

            //mas eventos de clic en opcion1 = insertar
            if (_estadoMouse.LeftButton == ButtonState.Pressed && (_estadoMouse.X >= 368 && _estadoMouse.X <= 550) && (_estadoMouse.Y >= 25 && _estadoMouse.Y <= 60)) // si con el mouse damos click en las dimensiones del boton insertar 
            {

                opcion1 = true; // la opcion1 sera verdadera o se activara 
               

            }

            //mas eventos de clic en opcion2 =  buscar
            if (_estadoMouse.LeftButton == ButtonState.Pressed && (_estadoMouse.X >= 599 && _estadoMouse.X <= 791) && (_estadoMouse.Y >= 25 && _estadoMouse.Y <= 60))// si con el mouse damos click en las dimensiones del boton buscar
            {
                
                opcion2 = true;// la opcion2 sera verdadera o se activara 


            }

            //mas eventos de clic en opcion1 = insertar
            if (_estadoMouse.LeftButton == ButtonState.Pressed && (_estadoMouse.X >= 828 && _estadoMouse.X <= 1022) && (_estadoMouse.Y >= 25 && _estadoMouse.Y <= 60))// si con el mouse damos click en las dimensiones del boton exportar
            {
                
                opcion3 = true;// la opcion3 sera verdadera o se activara 


            }
           
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Microsoft.Xna.Framework.Color.BlueViolet);
            //_spriteBatch.Begin(SpriteSortMode.FrontToBack);//esto hacia que se borraran los nodos
            _spriteBatch.Begin();
            _TextBoxNumeros.Render(_spriteBatch);



           

           // _spriteBatch.DrawString(_fuente, _estadoMouse.Y.ToString(), new Vector2(50, 50), Microsoft.Xna.Framework.Color.Black);
         

             //click con mouse en base a coordenadas de la textura
            if(opcion1)
            {

                arbolAVL.Insertar(Convert.ToInt32(datoingresar)); // se llama al metodo insertar
               
                cont++; // aunmenta el contador

                //arbolAVL.ImprimirPre(arbolAVL.Raiz);
                
                opcion1 = false; // vuelve a false para que no ocurran errores
                
            }
            if (opcion2)
            {
                arbolAVL.buscar(Convert.ToInt32(datoingresar));// se llama al metodo buscar

                cont++;// aunmenta el contador



                opcion2 = false;// vuelve a false para que no ocurran errores
            }

            if (opcion3)
            {

                opcion3 = false;
            }
           
            arbolAVL.DibujarArbol(_graphics, _spriteBatch, 3, fuentenodo); // dibuja el arbol

            //
            //arbolAVL.ImprimirPre(_fuente);


          //  _spriteBatch.DrawString(_fuente, text: $"Recorrido preorde: {ImprimirPrem()}", position: new Vector2(655, 60), color: Microsoft.Xna.Framework.Color.White) ;
            
            // _spriteBatch.DrawString(_fuente, , new Vector2(50, 50), Microsoft.Xna.Framework.Color.Black);

            //  arbolAVL.colorear(_graphics, _spriteBatch,  fuentenodo, arbolAVL.Raiz, true, false, false);

            //368 y 550 x en insertar
            //25-60 y en insertar

            //Buscar en x =  599,791 - y = 22,60
            //exportar en x = 828,1022 - y = 22,60
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        

        private void changeButton() // para cambiar los btones
        {
            for (int i = 0; i < listButtons.Count; i++) //  cantidad de botonnes que contiene la lista
            {
                if (i == _selectedButton) // si el indice seleccioado es igual a selected button
                    listButtons[i].IsSelected = true; // el cambio se realizara en base al indice de la lista de botones
                else //  sino
                    listButtons[i].IsSelected = false; // no se realiza cambio

                
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

                if (_TextBoxNumeros.Seleccionado)
                {
                    _TextBoxNumeros.AgregarTexto(dato);
                }
                datoingresar = value;  

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

        private string ImprimirPre(ArbolValanceado Raiz)
        {
            if (Raiz != null)
            {
                cadenaPreorden+=Raiz.valor + " ";
                ImprimirPre(Raiz.NodoIzquierdo);
                ImprimirPre(Raiz.NodoDerecho);
                //DibujarArbol(ImprimirPre);
            }
            return cadenaPreorden;
        }
        public string ImprimirPrem()
        {

            
            ImprimirPre(arbolAVL.Raiz);
            return cadenaPreorden;
            //Console.WriteLine();

        }

    }
}