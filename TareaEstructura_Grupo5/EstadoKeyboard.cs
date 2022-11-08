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
        static KeyboardState _estadoKeyboardActual;
        static KeyboardState _estadoKeyboardPrevio;

        public EstadoKeyboard()
        { }

        public static KeyboardState GetState()
        {
            _estadoKeyboardPrevio = _estadoKeyboardActual;
            _estadoKeyboardActual = Keyboard.GetState();
            return _estadoKeyboardActual;

        }

        public static bool TeclaPrecionada(Keys key)
        {
            return _estadoKeyboardActual.IsKeyDown(key);

        }

        public static bool TeclaNoPrecionada(Keys key)
        {
            return _estadoKeyboardActual.IsKeyDown(key) && !_estadoKeyboardPrevio.IsKeyDown(key);

        }
    }
}
