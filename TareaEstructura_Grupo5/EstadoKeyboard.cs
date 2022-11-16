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
    public class EstadoKeyboard
    {
        static KeyboardState _estadoKeyboardActual;//mantiene el estado del teclado durante el proceso 
        static KeyboardState _estadoKeyboardPrevio;//mantiene el estado previo del teclado durante el proceso 


        public EstadoKeyboard()
        { }

        public static KeyboardState GetState()
        {
            _estadoKeyboardPrevio = _estadoKeyboardActual;
            _estadoKeyboardActual = Keyboard.GetState();// captura el estado del teclado y este se va guardando en previo
            return _estadoKeyboardActual;

        }

        public static bool TeclaPrecionada(Keys key)
        {
            // evalua si una tecla ha sido precionada y se almacena en  _estadoKeyboardActual
            return _estadoKeyboardActual.IsKeyDown(key);

        }

        public static bool TeclaNoPrecionada(Keys key)
        {//mientras no se preciona una tecla, este almacena el previo 
            return _estadoKeyboardActual.IsKeyDown(key) && !_estadoKeyboardPrevio.IsKeyDown(key);

        }
    }
}
