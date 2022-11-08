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
    public class EstadoMouse
    {
        static MouseState _estadoMouseActual;
        static MouseState _estadoMousePrevio;

        public EstadoMouse()
        { }

        public static MouseState GetState()
        {
            _estadoMousePrevio = _estadoMouseActual;
            _estadoMouseActual = Mouse.GetState();
            return _estadoMouseActual;

        }

        public static bool ClickMouse(bool left)
        {
            if (left) //Click derecho
            {
                return _estadoMouseActual.LeftButton == ButtonState.Pressed;
            }
            else
            {
                return _estadoMouseActual.RightButton == ButtonState.Pressed;
            }
        }

        public static bool NoClick(bool left)// si no hay click osea mientras no se preciona un click
        {
            if (left)
            {
                return _estadoMouseActual.LeftButton == ButtonState.Pressed && !(_estadoMousePrevio.LeftButton == ButtonState.Pressed);
            }
            else
            {
                return _estadoMouseActual.RightButton == ButtonState.Pressed && !(_estadoMousePrevio.RightButton == ButtonState.Pressed);
            }
        }
    }
}
