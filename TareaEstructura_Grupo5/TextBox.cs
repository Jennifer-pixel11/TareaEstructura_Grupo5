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
    
    public  class TextBox 
    {
        
        public string TextoActual { get; set; }//es el texto que se muestra en el textbox
        public Vector2 PosiciónTextoActual { get; set; } //asigna la posicion donde se muestra el contenido
        public Vector2 PosicionCursor { get; set; }//"detecta la posicion" del cursor, si esta en el area asignado del txtbox este ejucuta las capruta de datos
        public int TiempoAnimacion { get; set; }//tiempo de animaion (en este caso seria por un cursor en el txtbox)
        public bool Visible { get; set; } //muestra el contenido si es true
        public float Capa { get; set; }//"capa donde se asigna el espacio para el txtbox
        public Vector2 Posicion { get; set; } //posicion de los elementos a utilizar
        public bool Seleccionado { get; set; } //indica si el area de txtbox esta seleccionado
        public int AnchoTextBox { get; set; }//ancho de area del txtbox
        public int AltoTextBox { get; set; }//alto de area del txtbox
        private int _anchoCursor;//ancho del area del cursor
        private int _alturaCursor;//alto del area del cursor
        private int _longitud;//contiene la cantidad maxima de caracteres
        private bool _numeros;//acepta solo numeros del teclado
        private Texture2D _textura;

        private Point _dimensionesCursor;//dimenciones del cursor
        private SpriteFont _fuente;//sprite del txt
        public int dato = 0;







        //Costructor TextBox
        public TextBox(Texture2D textura, Point dimencion, Point dimensionesCursor, Vector2 posicion, int longitud, bool numeros, bool visible, SpriteFont fuente, string texto, float capa)
        {
            _textura = textura;
            AnchoTextBox = dimencion.X;
            AltoTextBox = dimencion.Y;
            _anchoCursor = dimensionesCursor.X;
            _alturaCursor = dimensionesCursor.Y;
            _longitud = longitud;
            _numeros = numeros;
            TiempoAnimacion = 0;
            Visible = visible;
            Capa = capa;
            Posicion = posicion;
            PosicionCursor = new Vector2(posicion.X + 7, posicion.Y + 6);
            PosiciónTextoActual = new Vector2(posicion.X + 7, posicion.Y + 3);
            TextoActual = String.Empty;
            _dimensionesCursor = dimensionesCursor;
            Seleccionado = false;
            _fuente = fuente;
            TextoActual = texto;
        }

        public void Update()
        { }

        public void AgregarTexto(char texto)
        {
            Vector2 espacio = new Vector2();// realiza un seguimineto de espacio
            KeyboardState estadoTeclado = EstadoKeyboard.GetState();//obtiene el estado del teclado
            bool capturaNumero = true;//true si el estado de teclado son numeros 
            //valida el uso de las teclas de navegacion (izquierda y derecha )
            if (estadoTeclado.CapsLock || estadoTeclado.IsKeyDown(Keys.LeftShift) || estadoTeclado.IsKeyDown(Keys.RightShift))
            {
                capturaNumero = false;
            }

            //verifica que el valor de la longitud sea menor al maximo establecido que es 10
            if (_numeros && (int)Char.GetNumericValue(texto) < 0 || (int)Char.GetNumericValue(texto) > 9) //No permitir caracteres no numéricos si este cuadro de texto es solo numérico
            {
                if (texto != '\b')
                {
                    return;
                }
            }

            if (texto != '\b')
            {//verifica que el valor de la longitud sea menor al maximo establecido que es 10
                if (TextoActual.Length < _longitud)
                {
                    if (capturaNumero)
                    {
                        texto = Char.ToLower(texto);
                    }
                    TextoActual += texto;//agrega al txtbox de forma continua los numero ingresando
                    espacio = _fuente.MeasureString(texto.ToString());//agrega los numero ingresado en el espacio del txtbox
                    PosicionCursor = new Vector2(PosicionCursor.X + espacio.X, PosicionCursor.Y);//detecta la posicion del cursor
                }
            }
            else //Si es un carácter de retroceso o eliminación
            {
                if (TextoActual.Length > 0)
                {//la momento de eliminar o navegar no sobre pase la longitud de caracteres del espacio del textbox
                    espacio = _fuente.MeasureString(TextoActual.Substring(TextoActual.Length - 1));
                    TextoActual = TextoActual.Remove(TextoActual.Length - 1, 1); //Un retroceso elimina el último carácter de la cadena y mueve el cursor hacia atrás
                    PosicionCursor = new Vector2(PosicionCursor.X - espacio.X, PosicionCursor.Y);//detecta la posicion del cursor
                }
            }
        }

        //metodo render, muestra el contenido del TextoActual
        public void Render(SpriteBatch spriteBatch)
        {
            if (Visible)// si Visibles es true, nuestra el contenido al momento de dibujarlo, lo contrario es false, no se muestra nada
            {
                spriteBatch.Draw(_textura, Posicion, Color.White);//Dibujar la imagen de fondo
                spriteBatch.DrawString(_fuente, TextoActual, PosiciónTextoActual, Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, Capa);//Dibujar el texto actual
                
                //

            }
        }
    }
}
