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
        static MouseState _estadoMouseActual;//mantiene el estado del mouse durante el proceso (click y posicion)
        static MouseState _estadoMousePrevio;//mantiene el estado del mouse antes del porceso

        public EstadoMouse()
        { }

        public static MouseState GetState()
        {
            _estadoMousePrevio = _estadoMouseActual;
            _estadoMouseActual = Mouse.GetState();// captura el estado del mouse y este se va guardando en previo
            return _estadoMouseActual;

        }

        public static bool ClickMouse(bool left)
        {
            if (left) //Click derecho
            {//evalua si se ha dado clik derecho
                return _estadoMouseActual.LeftButton == ButtonState.Pressed;
            }
            else
            {// si no es izquierdo
                return _estadoMouseActual.RightButton == ButtonState.Pressed;
            }
        }

        public static bool NoClick(bool left)// si no hay click osea mientras no se preciona un click
        {
            if (left)
            {// evalua el estado previo del mouse y este se asigna que ya ha dado click
                return _estadoMouseActual.LeftButton == ButtonState.Pressed && !(_estadoMousePrevio.LeftButton == ButtonState.Pressed);
            }
            else
            {
                return _estadoMouseActual.RightButton == ButtonState.Pressed && !(_estadoMousePrevio.RightButton == ButtonState.Pressed);
            }
        }
    }
}
