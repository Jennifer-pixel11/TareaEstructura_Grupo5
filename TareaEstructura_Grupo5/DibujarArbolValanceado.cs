﻿using System;
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
           
          //  Raiz.ImprimirPre(gra, sprite, Raiz, font);
           // Raiz.ImprimirPre(sprite, Raiz, font);



        }
       




        /*public void ImprimirPre(ArbolValanceado Raiz)
        {
            if (Raiz != null)
            {
                //Console.Write(Raiz.info + " ");
                ImprimirPre(Raiz.NodoIzquierdo);
                ImprimirPre(Raiz.NodoDerecho);
                //DibujarArbol(ImprimirPre);
            }
        }*/



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
            {
                MessageBox.Show("Arbol AVL Vacío", "Error", MessageBoxButtons.OK);
            }
                //nada = 3;
               
            else
            {
                Raiz.buscar(x, Raiz);
            }
               

        }

        public void colorear(GraphicsDeviceManager gra, SpriteBatch sprite,  SpriteFont font, ArbolValanceado Raiz, bool post, bool inor, bool preor)
        {
            //Brush entorno = Brushes.Red;
            if (inor == true)
            {
                if (Raiz != null)
                {
                    colorear(gra, sprite,  font, Raiz.NodoIzquierdo, post, inor, preor);
                    Raiz.colorear(gra,sprite, font);
                    Thread.Sleep(10);
                    // pausar la ejecución 1000 milisegundos
                    Raiz.colorear(gra, sprite, font);
                    colorear(gra, sprite, font, Raiz.NodoDerecho, post, inor, preor);
                }
            }
            else
            if (preor == true)
            {
                if (Raiz != null)
                {
                    Raiz.colorear(gra, sprite, font);
                    Thread.Sleep(1000);
                    // pausar la ejecución 1000 milisegundos
                    Raiz.colorear(gra, sprite, font);
                    colorear(gra, sprite, font, Raiz.NodoIzquierdo, post, inor, preor);
                    colorear(gra, sprite,  font, Raiz.NodoDerecho, post, inor, preor);
                }
            }
            else if (post == true)
            {
                if (Raiz != null)
                {
                    colorear(gra, sprite,  font, Raiz.NodoIzquierdo, post, inor, preor);
                    colorear(gra, sprite,  font, Raiz.NodoDerecho, post, inor, preor);
                    Raiz.colorear(gra, sprite, font);
                    Thread.Sleep(1000); // pausar la ejecución 1000 milisegundos
                    Raiz.colorear(gra, sprite, font);
                }
            }
        }

    }
}


