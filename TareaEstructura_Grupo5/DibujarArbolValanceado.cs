using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MessageBox = System.Windows.Forms.MessageBox;

namespace TareaEstructura_Grupo5
{

    internal class DibujarArbolValanceado
    {
        public ArbolValanceado Raiz;
        public ArbolValanceado aux;
        // Constructor.
        public DibujarArbolValanceado()
        {
            aux = new ArbolValanceado();
        }
        public DibujarArbolValanceado(ArbolValanceado RaizNueva)
        {
            Raiz = RaizNueva;
        }
        // Agrega un nuevo valor al arbol.
        public void Insertar(int dato)
        {
            if (Raiz == null)
                Raiz = new ArbolValanceado(dato, null, null, null);
            else
                Raiz = Raiz.Insertar(dato, Raiz);
        }
        //Eliminar un valor del arbol
        public void Eliminar(int dato)
        {
            if (Raiz == null)
                Raiz = new ArbolValanceado(dato, null, null, null);
            else
                Raiz.Eliminar(dato, ref Raiz);
        }
        private const int Radio = 35;
        private const int DistanciaH = 140;
        private const int DistanciaV = 70;
        private int CoordenadaX;
        private int CoordenadaY;
        public void PosicionNodoreocrrido(ref int xmin, ref int ymin)
        {
            CoordenadaY = (int)(ymin + Radio / 2);
            CoordenadaX = (int)(xmin + Radio / 2);
            xmin += Radio;
        }

        //Dibuja el árbol
        public void DibujarArbol(GraphicsDeviceManager gra, SpriteBatch sprite, int dato, SpriteFont font)
        {
            int x = 300;
            int y = 100;
            if (Raiz == null) return;
            //Posicion de todos los Nodos.
            Raiz.PosicionNodo(ref x, y);
            //Dibuja los Enlaces entre nodos.
            Raiz.DibujarRamas(gra, sprite);
            //Dibuja todos los Nodos.
            Raiz.DibujarNodo(gra, sprite, dato, font);
            //Raiz.colorearRecorrido(gra, sprite, dato, font, true, true, true);

        }

        
           
        





        public int x1 = 250;
        public int y2 = 75;
        public void restablecer_valores()
        {
            x1 = 100;
            y2 = 75;
        }
        public void buscar(int x)
        {
            //int nada = 0;
            if (Raiz == null)
                //nada = 3;
                MessageBox.Show("Arbol AVL Vacío", "Error", MessageBoxButtons.OK);
            else
                Raiz.buscar(x, Raiz);

        }

        

    }
}


