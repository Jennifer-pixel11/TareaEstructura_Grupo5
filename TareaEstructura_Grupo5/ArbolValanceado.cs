using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MessageBox = System.Windows.Forms.MessageBox;

namespace TareaEstructura_Grupo5
{
    class ArbolValanceado
    {
        public int valor;
        public ArbolValanceado NodoIzquierdo;
        public ArbolValanceado NodoDerecho;
        public ArbolValanceado NodoPadre;
        public int altura;
        public Rectangle prueba;
        private ArbolValanceado arbol;
        private const int Radio = 35;
        private const int DistanciaH = 140;
        private const int DistanciaV = 70;
        private int CoordenadaX;
        private int CoordenadaY;
        public ArbolValanceado()
        {

        }
        public ArbolValanceado Arbol

        {
            get { return arbol; }
            set { arbol = value; }
        }
        public ArbolValanceado(int valorNuevo, ArbolValanceado izquierdo, ArbolValanceado derecho, ArbolValanceado padre)
        {
            valor = valorNuevo;
            NodoIzquierdo = izquierdo;
            NodoDerecho = derecho;
            NodoPadre = padre;
            altura = 0;
        }
        //Funcion para insertar un nuevo valor en el arbol AVL
        public ArbolValanceado Insertar(int valorNuevo, ArbolValanceado Raiz)
        {
            if (Raiz == null) // si la raiz es igual a null o vacio
                Raiz = new ArbolValanceado(valorNuevo, null, null, null);//crea cada nodo
            else if (valorNuevo < Raiz.valor) // en caso de que el valor nuevo sea mayor al valor de la clave
            {
                Raiz.NodoIzquierdo = Insertar(valorNuevo, Raiz.NodoIzquierdo); // se insertara al lado izquierd
            }
            else if (valorNuevo > Raiz.valor) // en caso de que el valor nuevo sea menor al valor de la clave anterior 
            {
                Raiz.NodoDerecho = Insertar(valorNuevo, Raiz.NodoDerecho); // se insertara a la derecha
            }
            else
            {
                //MessageBox.Show("Valor Existente en el Arbol", "Error", MessageBoxButtons.OK);
            }

            


            //Realiza las rotaciones simples o dobles segun el caso
            if (Alturas(Raiz.NodoIzquierdo) - Alturas(Raiz.NodoDerecho) == 2) //llamamos el metodo de alturas y se le pasa el nodo padre el cual se resta con el nodo derecho el cual debe dar 2
            {
                //aca define que el lado izquierdo esta mas cargado del lado izquierdo - rotaciones de izquierda a derecha
                if (valorNuevo < Raiz.NodoIzquierdo.valor) // si valor nuevo es menor a valor del nodo izquierdo
                    Raiz = RotacionIzquierdaSimple(Raiz); // realiza una rotacion hacia la izquierda
                else
                    Raiz = RotacionIzquierdaDoble(Raiz);//realiza una rotacion doble a la izquierda
            } 
            if (Alturas(Raiz.NodoDerecho) - Alturas(Raiz.NodoIzquierdo) == 2) //llamamos ell metodo de alturas y le psamos el nodo derecho el cual se resta con el nodo izquierdo el cual debe ser igual a 2
            {
                //aca definimos cuando el lado derecho esta mas cargado - rotaciones de derecha a izquierda
                if (valorNuevo > Raiz.NodoDerecho.valor) // si el valor ingresado es mayor al valor del nodo derecho realiza una rotacion simple 
                    Raiz = RotacionDerechaSimple(Raiz);
                else
                    Raiz = RotacionDerechaDoble(Raiz); // realiza una rotacion doble a la derecha
            }
            Raiz.altura = max(Alturas(Raiz.NodoIzquierdo), Alturas(Raiz.NodoDerecho)) + 1;// se calcula la altura, se manda a cortar la altura de los nodos izq y dere y se toma el valor maximo de las ramas y saber su alltura  
            return Raiz;//retornamos la raiz
        }
        //FUNCION DE PRUEBA PARA REALIZAR LAS ROTACIONES
        //Función para obtener que rama es mayor
        private static int max(int lhs, int rhs)
        {
            return lhs > rhs ? lhs : rhs;
        }
        private static int Alturas(ArbolValanceado Raiz)
        {
            return Raiz == null ? -1 : Raiz.altura;
        }
        ArbolValanceado nodoE, nodoP;
        public ArbolValanceado Eliminar(int valorEliminar, ref ArbolValanceado Raiz)
        {
            if (Raiz != null)
            {
                if (valorEliminar < Raiz.valor)
                {
                    nodoE = Raiz;
                    Eliminar(valorEliminar, ref Raiz.NodoIzquierdo);
                }
                else
                {
                    if (valorEliminar > Raiz.valor)
                    {
                        nodoE = Raiz;
                        Eliminar(valorEliminar, ref Raiz.NodoDerecho);
                    }
                    else
                    {
                        //Posicionado sobre el elemento a eliminar
                        ArbolValanceado NodoEliminar = Raiz;




                        if (NodoEliminar.NodoDerecho == null)
                        {
                            Raiz = NodoEliminar.NodoIzquierdo;
                            if (Alturas(nodoE.NodoIzquierdo) - Alturas(nodoE.NodoDerecho) == 2)
                            {
                                //MessageBox.Show("nodoE" + nodoE.valor.ToString());
                                if (valorEliminar < nodoE.valor)
                                    nodoP = RotacionIzquierdaSimple(nodoE);
                                else
                                    nodoE = RotacionDerechaSimple(nodoE);
                            }
                            if (Alturas(nodoE.NodoDerecho) - Alturas(nodoE.NodoIzquierdo) == 2)
                            {
                                if (valorEliminar > nodoE.NodoDerecho.valor)
                                    nodoE = RotacionDerechaSimple(nodoE);
                                else
                                    nodoE = RotacionDerechaDoble(nodoE);
                                nodoP = RotacionDerechaSimple(nodoE);
                            }
                        }
                        else
                        {
                            if (NodoEliminar.NodoIzquierdo == null)
                            {
                                Raiz = NodoEliminar.NodoDerecho;
                            }
                            else
                            {
                                if (Alturas(Raiz.NodoIzquierdo) - Alturas(Raiz.NodoDerecho) > 0)
                                {
                                    ArbolValanceado AuxiliarNodo = null;
                                    ArbolValanceado Auxiliar = Raiz.NodoIzquierdo;
                                    bool Bandera = false;
                                    while (Auxiliar.NodoDerecho != null)
                                    {
                                        AuxiliarNodo = Auxiliar;
                                        Auxiliar = Auxiliar.NodoDerecho;
                                        Bandera = true;
                                    }
                                    Raiz.valor = Auxiliar.valor;
                                    NodoEliminar = Auxiliar;
                                    if (Bandera == true)
                                    {
                                        AuxiliarNodo.NodoDerecho = Auxiliar.NodoIzquierdo;
                                    }
                                    else
                                    {
                                        Raiz.NodoIzquierdo = Auxiliar.NodoIzquierdo;
                                    }
                                    //Realiza las rotaciones simples o dobles segun el caso
                                }
                                else
                                {
                                    if (Alturas(Raiz.NodoDerecho) - Alturas(Raiz.NodoIzquierdo) > 0)
                                    {
                                        ArbolValanceado AuxiliarNodo = null;
                                        ArbolValanceado Auxiliar = Raiz.NodoDerecho;
                                        bool Bandera = false;
                                        while (Auxiliar.NodoIzquierdo != null)
                                        {
                                            AuxiliarNodo = Auxiliar;
                                            Auxiliar = Auxiliar.NodoIzquierdo;
                                            Bandera = true;
                                        }
                                        Raiz.valor = Auxiliar.valor;
                                        NodoEliminar = Auxiliar;
                                        if (Bandera == true)
                                        {
                                            AuxiliarNodo.NodoIzquierdo = Auxiliar.NodoDerecho;
                                        }
                                        else
                                        {
                                            Raiz.NodoDerecho = Auxiliar.NodoDerecho;
                                        }
                                    }
                                    else
                                    {
                                        if (Alturas(Raiz.NodoDerecho) - Alturas(Raiz.NodoIzquierdo) == 0)
                                        {
                                            ArbolValanceado AuxiliarNodo = null;
                                            ArbolValanceado Auxiliar = Raiz.NodoIzquierdo;
                                            bool Bandera = false;
                                            while (Auxiliar.NodoDerecho != null)
                                            {
                                                AuxiliarNodo = Auxiliar;
                                                Auxiliar = Auxiliar.NodoDerecho;
                                                Bandera = true;
                                            }
                                            Raiz.valor = Auxiliar.valor;
                                            NodoEliminar = Auxiliar;
                                            if (Bandera == true)
                                            {
                                                AuxiliarNodo.NodoDerecho = Auxiliar.NodoIzquierdo;
                                            }
                                            else
                                            {
                                                Raiz.NodoIzquierdo = Auxiliar.NodoIzquierdo;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
               MessageBox.Show("Nodo inexistente en el arbol", "Error", MessageBoxButtons.OK);
            }
            return nodoP;
        }
        //Seccion de funciones de rotaciones
        //Rotacion Izquierda Simple
        private static ArbolValanceado RotacionIzquierdaSimple(ArbolValanceado k2)
        {
            //lo que se realiza en un rotacion simple 
            /*
             
            k2 = padre

            k1 =  hijo
             */
            ArbolValanceado k1 = k2.NodoIzquierdo; // k1 sera igual a k2 nodoizquierdo
            k2.NodoIzquierdo = k1.NodoDerecho; // k2 nodoizquierdo es igual k1 nododerecho
            k1.NodoDerecho = k2; //nododerecho = k2
            k2.altura = max(Alturas(k2.NodoIzquierdo), Alturas(k2.NodoDerecho)) + 1;//se calcula la altura de k2 y tener los valores almacenados en los objetos
            k1.altura = max(Alturas(k1.NodoIzquierdo), k2.altura) + 1;//se calcula la altura de k2 y tener los valores almacenados en los objetos

            return k1;//retorna k1 porque termina siendo el padre
        }
        //Rotacion Derecha Simple
        private static ArbolValanceado RotacionDerechaSimple(ArbolValanceado k1)
        {
            /*Rotacion simple a la derecha*/
            ArbolValanceado k2 = k1.NodoDerecho; // k2 = k1
            k1.NodoDerecho = k2.NodoIzquierdo; // k1 nodo dercho es iual a k2 nodo izquierdp
            k2.NodoIzquierdo = k1; // k2 nodo izquierdo es igual a k1
            k1.altura = max(Alturas(k1.NodoIzquierdo), Alturas(k1.NodoDerecho)) + 1;//se calcula la altura de k1 y tener los valores almacenados en los objetos
            k2.altura = max(Alturas(k2.NodoDerecho), k1.altura) + 1;//se calcula la altura de k1 y tener los valores almacenados en los objetos
            return k2; // retorna k2 porque termina siendo el padre
        }
        //Doble Rotacion Izquierda
        private static ArbolValanceado RotacionIzquierdaDoble(ArbolValanceado k3)
        {
            /*
              se realiza dos veces la rotacion
             */
            k3.NodoIzquierdo = RotacionDerechaSimple(k3.NodoIzquierdo);// llamamos a la rotacion derecha, el arbol crecio de derecha a izquierda y dejamos de pivote al ultimo elemento, a la derecha el mayor y al izquierda el menor
            return RotacionIzquierdaSimple(k3);//retornamos la rotacion simplle a la izquierda
        }
        //Doble Rotacion Derecha
        private static ArbolValanceado RotacionDerechaDoble(ArbolValanceado k1) 
        {
            /*
             ocurre lo mismo que el metodo anterior
             */
            k1.NodoDerecho = RotacionIzquierdaSimple(k1.NodoDerecho); // primero la rotacion izquierda simple 
            return RotacionDerechaSimple(k1);//
        }
        //Funcion para obtener la altura del arbol
        public int getAltura(ArbolValanceado nodoActual)
        {       /*
                 
                 */
            if (nodoActual == null)//
                return 0;//
            else
                return 1 + Math.Max(getAltura(nodoActual.NodoIzquierdo), getAltura(nodoActual.NodoDerecho));//
        }
        // *******************************************Buscar un valor en el arbol**********************************************
        public void buscar(int valorBuscar, ArbolValanceado Raiz) // metodo buscar 
        {

            if (Raiz != null) // si la raiz es diferente de null hara lo siguiente - verificara si ese dato existe
            {

                if (valorBuscar == Raiz.valor) // si la el valor a busacr es igual al nodo a busar
                {
                    MessageBox.Show("Nodo: " + Raiz.valor + " encontrado en la posición X: " + Raiz.CoordenadaX + ", Y: " + Raiz.CoordenadaY, "INFORMACIÓN NODOS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    encontrado(Raiz); // mostrara el dato encontrado
                }
                else // al contrario
                {
                    if (valorBuscar < Raiz.valor) // si el valor a buscar es menor que el valor de l clave
                    {
                        buscar(valorBuscar, Raiz.NodoIzquierdo); // 
                       // Raiz = Raiz.NodoIzquierdo;
                        // MessageBox.Show("Nodo encontrado en la posición X: " + valor.CoordenadaX + ", Y: " + valorBuscar.CoordenadaY, "Error", MessageBoxButtons.OK);
                        //    encontrado(p);
                    }
                    else // al contrario 
                    {
                        if (valorBuscar > Raiz.valor)
                        {
                            //Raiz = Raiz.NodoDerecho;
                            buscar(valorBuscar, Raiz.NodoDerecho);

                        }
                    }
                }

            }

            else // si el valor de la raiz es null 
                MessageBox.Show("Valor no encontrado", "Error", MessageBoxButtons.OK); // entonces mostrara un mensaje en el que indica que no fue encontrado




        }
        /*++++++++++++FUNCIONES PARA DIBUJAR EL ÁRBOL +++++++++++++*/

        //Encuentra la posición en donde debe crearse el nodo.
        public void PosicionNodo(ref int xmin, int ymin)
        {
            int aux1, aux2;
            CoordenadaY = (int)(ymin + Radio / 2);
            //obtiene la posición del Sub-Árbol izquierdo.
            if (NodoIzquierdo != null)
            {
                NodoIzquierdo.PosicionNodo(ref xmin, ymin + Radio + DistanciaV);
            }
            if ((NodoIzquierdo != null) && (NodoDerecho != null))
            {
                xmin += DistanciaH;
            }
            //Si existe el nodo derecho e izquierdo deja un espacio entre ellos.
            if (NodoDerecho != null)
            {
                NodoDerecho.PosicionNodo(ref xmin, ymin + Radio + DistanciaV);
            }
            // Posicion de nodos dercho e izquierdo.
            if (NodoIzquierdo != null)
            {
                if (NodoDerecho != null)
                {
                    //centro entre los nodos.
                    CoordenadaX = (int)((NodoIzquierdo.CoordenadaX + NodoDerecho.CoordenadaX) / 2);
                }
                else
                {
                    // no hay nodo derecho. centrar al nodo izquierdo.
                    aux1 = NodoIzquierdo.CoordenadaX;
                    NodoIzquierdo.CoordenadaX = CoordenadaX - 40;
                    CoordenadaX = aux1;
                }
            }
            else if (NodoDerecho != null)
            {
                aux2 = NodoDerecho.CoordenadaX;
                //no hay nodo izquierdo.centrar al nodo derecho.
                NodoDerecho.CoordenadaX = CoordenadaX + 40;
                CoordenadaX = aux2;
            }
            else
            {
                // Nodo hoja
                CoordenadaX = (int)(xmin + Radio / 2);
                xmin += Radio;
            }
        }
        // Dibuja las ramas de los nodos izquierdo y derecho
        public void encontrado(ArbolValanceado Raiz)
        {
            Rectangle rec = new Rectangle(Raiz.CoordenadaX, Raiz.CoordenadaY, 40, 40);
        }
        public void PosicionNodoreocrrido(ref int xmin, int ymin)
        {
            int aux1, aux2;
            CoordenadaY = (int)(ymin + Radio / 2);
            //obtiene la posición del Sub-Árbol izquierdo.
            if (NodoIzquierdo != null)
            {
                NodoIzquierdo.PosicionNodoreocrrido(ref xmin, ymin + Radio + DistanciaV);
            }
            if ((NodoIzquierdo != null) && (NodoDerecho != null))
            {
                xmin += DistanciaH;
            }
            //Si existe el nodo derecho e izquierdo deja un espacio entre ellos.
            if (NodoDerecho != null)
            {
                NodoDerecho.PosicionNodoreocrrido(ref xmin, ymin + Radio + DistanciaV);
            }
            // Posicion de nodos dercho e izquierdo.
            if (NodoIzquierdo != null)
            {
                if (NodoDerecho != null)
                {
                    //centro entre los nodos.
                    CoordenadaX = (int)((NodoIzquierdo.CoordenadaX + NodoDerecho.CoordenadaX) / 2);
                }
                else
                {
                    // no hay nodo derecho. centrar al nodo izquierdo.
                    aux1 = NodoIzquierdo.CoordenadaX;
                    NodoIzquierdo.CoordenadaX = CoordenadaX - 40;
                    CoordenadaX = aux1;
                }
            }
            else if (NodoDerecho != null)
            {
                aux2 = NodoDerecho.CoordenadaX;
                //no hay nodo izquierdo.centrar al nodo derecho.
                NodoDerecho.CoordenadaX = CoordenadaX + 40;
                CoordenadaX = aux2;
            }
            else
            {
                // Nodo hoja
                CoordenadaX = (int)(xmin + Radio / 2);
                xmin += Radio;
            }
        }
        public void DibujarRamas(GraphicsDeviceManager grafo, SpriteBatch sprite)
        {
            if (NodoIzquierdo != null) // si nodo izquierdo es distinto de null 
            {

                // dibuja arco
                MonoGame.Primitives2D.DrawLine(sprite, new Vector2(CoordenadaX, CoordenadaY), new Vector2(NodoIzquierdo.CoordenadaX, NodoIzquierdo.CoordenadaY), Color.Black);


                // grafo.DrawLine(Lapiz, CoordenadaX, CoordenadaY, NodoIzquierdo.CoordenadaX,
                // NodoIzquierdo.CoordenadaY);
                NodoIzquierdo.DibujarRamas(grafo, sprite); // se dibuja la rama para nodos izquierdps
            }
            if (NodoDerecho != null) // si nodo derecho es distinto a null 
            {   // dinujar arco
                MonoGame.Primitives2D.DrawLine(sprite, new Vector2(CoordenadaX, CoordenadaY), new Vector2(NodoDerecho.CoordenadaX, NodoDerecho.CoordenadaY), Color.Black);
                // grafo.DrawLine(Lapiz, CoordenadaX, CoordenadaY, NodoDerecho.CoordenadaX, NodoDerecho.CoordenadaY);
                NodoDerecho.DibujarRamas(grafo, sprite); // se dibuja la rama para los nodos derechso
            }
        }
        //Dibuja el nodo en la posición especificada.

        //Dibuja el nodo en la posición especificada.



        public void DibujarNodo(GraphicsDeviceManager grafo, SpriteBatch sprite, int dato, SpriteFont font)
        {
            //Dibuja el contorno del nodo.
            Rectangle rect = new Rectangle(
            (int)(CoordenadaX - Radio / 2),
            (int)(CoordenadaY - Radio / 2),
            Radio, Radio);
            if (valor == dato) // si el clave es igual a la variable dato
            {
                //dibuja el contorno del nodo
                MonoGame.Primitives2D.DrawCircle(sprite, new Vector2((int)(CoordenadaX - Radio / 2), (int)(CoordenadaY - Radio / 2)), Radio + 2, 52, Color.Red);

                for (int i = 0; i < 50; i++) // 
                {
                    // dibuja el nodo y agrega color
                    MonoGame.Primitives2D.DrawCircle(sprite, new Vector2((int)((CoordenadaX) - Radio / 2), (int)((CoordenadaY) - Radio / 2)), Radio - i, 52, Color.Gray);
                }

                //grafo.FillEllipse(encuentro, rect);

            }
            else
            {
                MonoGame.Primitives2D.DrawCircle(sprite, new Vector2((int)(CoordenadaX - Radio / 2), (int)(CoordenadaY - Radio / 2)), Radio + 2, 52, Color.Red);

                for (int i = 0; i < 50; i++)
                {
                    MonoGame.Primitives2D.DrawCircle(sprite, new Vector2((int)((CoordenadaX) - Radio / 2), (int)((CoordenadaY) - Radio / 2)), Radio - i, 52, Color.Gray);
                }

            }
            //letra
            sprite.DrawString(font, valor.ToString(), new Vector2((int)(CoordenadaX - Radio / 2), (int)(CoordenadaY - Radio / 2)), Color.White);
            //Dibuja los nodos hijos derecho e izquierdo.
            if (NodoIzquierdo != null) // si nodo izquierdo es distinto de null
            {
                NodoIzquierdo.DibujarNodo(grafo, sprite, dato, font); // dibuja el nodo izquierd

            }
            if (NodoDerecho != null) // si nodo derecho es distinto de null
            {
                NodoDerecho.DibujarNodo(grafo, sprite, dato, font); // dibuja el nodo derecho

            }



        }

        public void colorear(GraphicsDeviceManager grafo, SpriteBatch sprite, SpriteFont font)
        {
            Rectangle rect = new Rectangle((int)(CoordenadaX - Radio / 2), (int)(CoordenadaY - Radio / 2), Radio, Radio);
            Rectangle prueba = new Rectangle((int)(CoordenadaX - Radio / 2), (int)(CoordenadaY - Radio / 2), Radio, Radio);

            MonoGame.Primitives2D.DrawCircle(sprite, new Vector2((int)(CoordenadaX - Radio / 2), (int)(CoordenadaY - Radio / 2)), Radio + 2, 52, Color.White);

            for (int i = 0; i < 50; i++)
            {
                MonoGame.Primitives2D.DrawCircle(sprite, new Vector2((int)((CoordenadaX) - Radio / 2), (int)((CoordenadaY) - Radio / 2)), Radio - i, 52, Color.Green);
            }
            sprite.DrawString(font, valor.ToString(), new Vector2((int)(CoordenadaX - Radio / 2), (int)(CoordenadaY - Radio / 2)), Color.White);

            if (NodoIzquierdo != null)
            {
                NodoIzquierdo.colorear(grafo, sprite, font);
            }
            if (NodoDerecho != null)
            {
                NodoDerecho.colorear(grafo, sprite, font);
            }



        }
       
        /*public void ImprimirPre(GraphicsDeviceManager grafo, SpriteBatch sprite, ArbolValanceado Raiz, SpriteFont font)
        {
            if (Raiz != null)
            {
                //Console.Write(Raiz.info + " ");
                MonoGame.Primitives2D.DrawCircle(sprite, new Vector2((int)(CoordenadaX - Radio / 2), (int)(CoordenadaY - Radio / 2)), Radio + 2, 52, Color.White);

                for (int i = 0; i < 50; i++)
                {

                    MonoGame.Primitives2D.DrawCircle(sprite, new Vector2((int)((CoordenadaX) - Radio / 2), (int)((CoordenadaY) - Radio / 2)), Radio - i, 52, Color.Green);
                    ImprimirPre(grafo, sprite, Raiz.NodoIzquierdo, font);
                    ImprimirPre(grafo, sprite, Raiz.NodoDerecho, font);
                }
               
                //sprite.DrawString(font, Raiz.valor.ToString(), new Vector2((int)(CoordenadaX - 200), (int)(CoordenadaY - 500)), Color.Black);
                //DibujarArbol(ImprimirPre);
            }
        }*/



    }
}
