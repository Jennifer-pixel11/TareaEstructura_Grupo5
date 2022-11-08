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
        public string TextoActual { get; set; }
        public Vector2 PosiciónTextoActual { get; set; }
        public Vector2 PosicionCursor { get; set; }
        public int TiempoAnimacion { get; set; }
        public bool Visible { get; set; }
        public float Capa { get; set; }
        public Vector2 Posicion { get; set; }
        public bool Seleccionado { get; set; }
        public int AnchoTextBox { get; set; }
        public int AltoTextBox { get; set; }
        private int _anchoCursor;
        private int _alturaCursor;
        private int _longitud;
        private bool _numeros;
        private Texture2D _textura;

        private Point _dimensionesCursor;
        private SpriteFont _fuente;

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
            Vector2 espacio = new Vector2();
            KeyboardState estadoTeclado = EstadoKeyboard.GetState();
            bool capturaNumero = true;

            if (estadoTeclado.CapsLock || estadoTeclado.IsKeyDown(Keys.LeftShift) || estadoTeclado.IsKeyDown(Keys.RightShift))
            {
                capturaNumero = false;
            }
            if (_numeros && (int)Char.GetNumericValue(texto) < 0 || (int)Char.GetNumericValue(texto) > 9) //No permitir caracteres no numéricos si este cuadro de texto es solo numérico
            {
                if (texto != '\b')
                {
                    return;
                }
            }

            if (texto != '\b')
            {
                if (TextoActual.Length < _longitud)
                {
                    if (capturaNumero)
                    {
                        texto = Char.ToLower(texto);
                    }
                    TextoActual += texto;
                    espacio = _fuente.MeasureString(texto.ToString());
                    PosicionCursor = new Vector2(PosicionCursor.X + espacio.X, PosicionCursor.Y);
                }
            }
            else //Si es un carácter de retroceso o eliminación
            {
                if (TextoActual.Length > 0)
                {
                    espacio = _fuente.MeasureString(TextoActual.Substring(TextoActual.Length - 1));
                    TextoActual = TextoActual.Remove(TextoActual.Length - 1, 1); //Un retroceso elimina el último carácter de la cadena y mueve el cursor hacia atrás
                    PosicionCursor = new Vector2(PosicionCursor.X - espacio.X, PosicionCursor.Y);
                }
            }
        }

        public void Render(SpriteBatch spriteBatch)
        {
            if (Visible)
            {
                spriteBatch.Draw(_textura, Posicion, Color.White);//Dibujar la imagen de fondo
                spriteBatch.DrawString(_fuente, TextoActual, PosiciónTextoActual, Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, Capa);//Dibujar el texto actual

            }
        }
    }
}
