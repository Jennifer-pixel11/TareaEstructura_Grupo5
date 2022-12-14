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
using System.Diagnostics;
using System.Drawing.Imaging;
using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Fields;
using Document = Spire.Doc.Document;
using Section = Spire.Doc.Section;
using Paragraph = Spire.Doc.Documents.Paragraph;
using System.Threading;

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
        private bool opcion4 = false;



        private bool preOr = false;
        private bool InOr = false;
        private bool PosOr = false;
        string datoingresar = "";
        public string cadenaPreorden { get; set; }
        //lista de botones
        List<Button> listButtons = new List<Button>(); // lista para los botones de la clase buttons para tener cada uno de los botones creados


        int _selectedButton = 0;// INDICADOR DEL BOTON A SELECCIONAR
        //instancia de los botones
        Button btn1; // boton insertar
        Button btn2; //boton buscar
        Button btn3; //boton exportar
        Button btn4; //boton eLIMINAR

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
        int delay = 0;
        //definimos el alto y el ancho de la ppantalla como constantes 
        const int Alto = 650; //  alto de la pantalla
        const int Ancho = 1309; // ancho de la panatalla 

        // variables para los textbox
        //  private TextBox textoActual;
        private TextBox _TextBoxNumeros;//cuadro de textbox
        private Vector2 _PosicionTextBox; //contiene la posicion del txtbox
        private SpriteFont _fuente, fuentenodo;//fuente de texto
        private int _longitud; // longitud donde se almacena los numeros
        private MouseState _estadoMouse; //recibe el estado del mouse de la clase EstadoMouse
        private KeyboardState _estadoKeyboard;//recibe el estado del teclado de la clase EstadoKeyboard
        private int _tiempoAccion;//realiza la ejercucion de eliminarcion (tiempo de animacion para borrrar los numeros)


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
            btn1.Texture = Content.Load<Texture2D>("INSER"); //cargamos la imagen
            btn1.SourceRectangle = new Microsoft.Xna.Framework.Rectangle(0, 0, 197, btn1.Texture.Height); //definimos el rectangulo para el boton insertar
            btn1.Position = new Vector2(462 - btn1.Texture.Width / 4, 20); // definimos la posicion - se divide en 4 ya que son dos texturas en una
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

            btn4 = new Button(this);// inicializamos el boton exportar
            btn4.Texture = Content.Load<Texture2D>("ELIMI");//cargamos la imagen
            btn4.SourceRectangle = new Microsoft.Xna.Framework.Rectangle(0, 0, 172, btn4.Texture.Height);// definimos el rectangulo para el boton exportar
            btn4.Position = new Vector2(1150 - btn4.Texture.Width / 4, 20);// definimos la posicion-se divide en 4 la textura ya que son dos texturas en una
                                                                           // btn3.Click += btn => Exit();
                                                                           //agregamos los botones a la lista 
            listButtons.Add(btn1);
            listButtons.Add(btn2);
            listButtons.Add(btn3);
            listButtons.Add(btn4);


            this.Components.Add(btn1); //lista de componentes del juego y agregamos el boton insertar
            this.Components.Add(btn2); // lista de componenetes del juego y agregamos el boton buscar
            this.Components.Add(btn3); /// lista de componentes del jueg y agregamos el boton exportar
            this.Components.Add(btn4); /// lista de componentes del jueg y agregamos el boton eLIMINAR

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

            _estadoKeyboard = EstadoKeyboard.GetState(); // obtiene el estado del teclado
            _estadoMouse = EstadoMouse.GetState(); // obtiene el estado del mouse

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

            //mas eventos de clic en opcion3 =  exportar
            if (_estadoMouse.LeftButton == ButtonState.Pressed && (_estadoMouse.X >= 828 && _estadoMouse.X <= 1022) && (_estadoMouse.Y >= 25 && _estadoMouse.Y <= 60))// si con el mouse damos click en las dimensiones del boton exportar
            {

                opcion3 = true;// la opcion3 sera verdadera o se activara 


            }

            //botn eliminar
            if (_estadoMouse.LeftButton == ButtonState.Pressed && (_estadoMouse.X >= 1064 && _estadoMouse.X <= 1235) && (_estadoMouse.Y >= 25 && _estadoMouse.Y <= 60))// si con el mouse damos click en las dimensiones del boton exportar
            {

                opcion4 = true;// la opcion3 sera verdadera o se activara 


            }







            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Microsoft.Xna.Framework.Color.BlueViolet);
            //_spriteBatch.Begin(SpriteSortMode.FrontToBack);//esto hacia que se borraran los nodos
            _spriteBatch.Begin();
            _TextBoxNumeros.Render(_spriteBatch);





            //  _spriteBatch.DrawString(_fuente, _estadoMouse.X.ToString(), new Vector2(50, 50), Microsoft.Xna.Framework.Color.Black);


            if (opcion1)
            {

                arbolAVL.Insertar(Convert.ToInt32(datoingresar)); // se llama al metodo insertar
                                                                  //  arbolAVL.ImprimirPre(arbolAVL.Raiz);
                cont++; // aunmenta el contador

                //arbolAVL.ImprimirPre(arbolAVL.Raiz);

                opcion1 = false; // vuelve a false para que no ocurran errores
                                 //preOr = false;

            }


            //click con mouse en base a coordenadas de la textura

            if (opcion2)
            {
                arbolAVL.buscar(Convert.ToInt32(datoingresar));// se llama al metodo buscar

                cont++;// aunmenta el contador



                opcion2 = false;// vuelve a false para que no ocurran errores
            }

            if (opcion3)// PARA EXPORTAR
            {
                exportarWord(); //Se llama al metodo exportar

                cont++;//aumenta el contador

                opcion3 = false;//vuelve a false para que no ocurran errores
            }
            if (opcion4)
            {
                arbolAVL.Eliminar(Convert.ToInt32(datoingresar));

                cont++;// aunmenta el contador

                opcion4 = false;
            }


            arbolAVL.DibujarArbol(_graphics, _spriteBatch, 3, fuentenodo);// dibuja el arbol

            //



            // _spriteBatch.DrawString(_fuente, text: $"Recorrido preorde: " , position: new Vector2(655, 60), color: Microsoft.Xna.Framework.Color.White);




            //368 y 550 x en insertar
            //25-60 y en insertar

            //Buscar en x =  599,791 - y = 22,60
            //exportar en x = 828,1022 - y = 22,60
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        //Metodo para exportar la captura de pantalla a Word
        public void exportarWord()
        {
            //Aqui se realiza la captura de pantalla
            Bitmap bmCaptura = new Bitmap(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width, System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height);
            Graphics captura = Graphics.FromImage(bmCaptura);
            captura.CopyFromScreen(System.Windows.Forms.Screen.PrimaryScreen.Bounds.X, System.Windows.Forms.Screen.PrimaryScreen.Bounds.Y, 0, 0, System.Windows.Forms.Screen.PrimaryScreen.Bounds.Size);

            //Aqui se guarda la captura de pantalla y abre el explorador de archivos para guardar
            //Aqui en esta parte nos daba el siguiente error, System.Threading.ThreadStateException
            //Entonces por esa razon creamos donde se mostraba la excepcion un subproceso temporal y ejecutamos el codigo dentro
            Thread t = new Thread((ThreadStart)(() =>
            {
                //Segun la investigacion que hicimos acerca de la excepcion vimos que se activaba porque estabamos ejecutando 
                //el codigo dentro del estado predeterminado del apartamento de CefSharp
                //y nos aparecia exactamente en la funcion showDialog()
                System.Windows.Forms.SaveFileDialog guardar = new System.Windows.Forms.SaveFileDialog();
                if (guardar.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    Bitmap Tomaimagen = new Bitmap(bmCaptura);
                    Tomaimagen.Save(guardar.FileName, ImageFormat.Jpeg);

                }
            }));
            // Ejecuta el código desde un subproceso que se une al subproceso STA
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
            t.Join();

            //Para poder crear, insertar y abrir el documento de word usamos la biblioteca Spire.Doc desarrollada por E-iceblue
            //Tambien intentamos antes usar la biblioteca Interop que es desarrollada por Microsoft pero tuvimos problemas asi que
            //encontramos la biblioteca Spire y decidimos mejor usar esa
            //Crea el documento word con Spire.Doc
            Document document = new Document();
            Section s = document.AddSection();
            Paragraph p = s.AddParagraph();

            //Inserta la imagen y el tamaño  
            DocPicture Pic = p.AppendPicture(Image.FromFile(@"C:\Users\Noelia\Pictures\Saved Pictures\AVL.png"));
            Pic.Width = 480;
            Pic.Height = 360;

            //Guarda y lo muestra
            document.SaveToFile("Image.docx", FileFormat.Docx);
            System.Diagnostics.Process.Start(new ProcessStartInfo { FileName = @"Image.docx", UseShellExecute = true });
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
        {//matriz de teclas que es igual al estado de teclado
            Keys[] keys = _estadoKeyboard.GetPressedKeys();
            String value = String.Empty;



            //evalua el estado del mouse si se hace click izquierdo
            if (_estadoMouse.LeftButton == ButtonState.Pressed)
            {
                if (EstadoMouse.NoClick(true))//si no se hizo click izquierdo este llama al metodo ClickIzquierdo
                {
                    ClickIzquierdo();
                }
            }
            //a travez del tiempoAccion, este ejecuta borrar 
            // "haciendo la animacion de borrar" numero actualizando el txtbox
            if (_estadoKeyboard.IsKeyUp(Keys.Back) && _estadoKeyboard.IsKeyUp(Keys.Delete))
            {
                _tiempoAccion = 10;// tiempo de animacion para la ejecucion ya mencionados
            }
            if (keys.Count() > 0)
            {
                if (keys.Count() > 1)//si la matriz de teclas tiene mas de una tecla
                {
                    keys[0] = ExtraerNumero(keys);//extra un numero ingresado 
                }
                if (_estadoKeyboard.IsKeyDown(Keys.Back) || _estadoKeyboard.IsKeyDown(Keys.Delete))
                //cuando se preciona eliminar se ejecuta la "animacion" a travez de _tiempoAccion
                {
                    if (_tiempoAccion == 0) // cuando el tiempo esta en 0 vuelte a 10 milisegundos
                    {
                        _tiempoAccion = 10;
                    }
                    if (_tiempoAccion == 10)//cuando pasaron 10 milisegundos realiza el siguente proceso
                    {
                        if (_TextBoxNumeros.Seleccionado)// si _TextBoxNumeros esta seleccionado
                        {
                            _TextBoxNumeros.AgregarTexto('\b');//actualiza el metodo AgregarTexto
                        }
                        _tiempoAccion--;//elimina los numeros 1x1
                    }
                    else
                    {
                        _tiempoAccion--;//elimina los numeros 1x1
                    }
                    return;
                }
                if (_TextBoxNumeros.Seleccionado)
                {// valida los numeros del teclado (barra numerico) y tambien del teclado numerico (en caso de tener ese teclado)
                    if (((int)keys[0] >= 48 && (int)keys[0] <= 57) || ((int)keys[0] >= 96 && (int)keys[0] <= 105))
                    {
                        value = keys[0].ToString().Substring(keys[0].ToString().Length - 1);//captura el ultimo digito ingresado

                        if (EstadoKeyboard.TeclaNoPrecionada(keys[0]))//esta parte solo verifica que las otras teclas que no han sido precionadas, no aparezcan en el txtbox
                        {
                            _TextBoxNumeros.AgregarTexto(value.ToCharArray()[0]);//actualiza el txtBox
                        }
                    }
                }

                if (_TextBoxNumeros.Seleccionado)
                {
                    _TextBoxNumeros.AgregarTexto(dato);//actualiza el txtBox
                }
                datoingresar = value;

            }
        }

        private Texture2D CargarTextura(string textureName) // funcion para cargar la textura 
        {
            return Content.Load<Texture2D>(textureName);
        }

        private void ClickIzquierdo()//evalua si el click izquierdo fue dentro de la posicion del txtbox
        {
            if (_estadoMouse.X >= _TextBoxNumeros.Posicion.X && _estadoMouse.X <= _TextBoxNumeros.Posicion.X + _TextBoxNumeros.AnchoTextBox)//evalua la posicion del click dado en el eje x
            {
                {
                    if (_estadoMouse.Y >= _TextBoxNumeros.Posicion.Y && _estadoMouse.Y <= _TextBoxNumeros.Posicion.Y + _TextBoxNumeros.AltoTextBox)//evalua la posicion del click dado en el eje y
                    {
                        _TextBoxNumeros.Seleccionado = true;
                    }
                }
            }





        }

        //extrae el numero que fue ingresado
        private Keys ExtraerNumero(Keys[] keys)
        {
            foreach (Keys key in keys)
            {// como trabaja con ASCII, este toma un numero de 0 a 9 
                if ((int)key >= 48 && (int)key <= 105)
                {
                    return key;
                }
            }
            return Keys.None;
        }

    }
}