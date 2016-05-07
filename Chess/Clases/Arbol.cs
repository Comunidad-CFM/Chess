using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Clases
{
    class Arbol
    {
        public Nodo raiz;
        public Arbol(int[,] tablero) 
        {
            this.raiz = new Nodo(tablero);
        }

        public int[,] obtenerTablero(int[,] tablero, int i, int j, int nuevoI, int nuevoJ)
        {
            int[,] copiaTablero = new int[8, 8];
            Array.Copy(tablero, copiaTablero, 64);

            copiaTablero[nuevoI, nuevoJ] = copiaTablero[i, j];
            copiaTablero[i, j] = 00;

            return copiaTablero;
        }

        public List<int[,]> peon(int[,] tablero, int jugador, int i, int j) 
        {
            List<int[,]> tableros = new List<int[,]>();

            switch (jugador)
            {
                // Peon blanco.
                case 1:
                    // Hacia la izquierda.
                    if (j - 1 >= 0)
                    {
                        if (tablero[i - 1, j - 1] < 10 && tablero[i - 1, j - 1] != 0)
                            tableros.Add(obtenerTablero(tablero, i, j, i - 1, j - 1));
                    }
                    // Hacia arriba 1.
                    if (tablero[i - 1, j] == 0)
                    {
                        tableros.Add(obtenerTablero(tablero, i, j, i - 1, j));

                        // Hacia arriba 2.
                        if (i == 6)
                        {
                            if (tablero[i - 2, j] == 0)
                                tableros.Add(obtenerTablero(tablero, i, j, i - 2, j));
                        }
                    }
                    // Hacia la derecha.
                    if (j + 1 < 8)
                    {
                        if (tablero[i - 1, j + 1] < 10 && tablero[i - 1, j + 1] != 0)
                            tableros.Add(obtenerTablero(tablero, i, j, i - 1, j + 1));
                    }
                    break;
                // Peon negro.
                case 2:
                    // Hacia abajo-izquierda.
                    if (j - 1 >= 0)
                    {
                        if (tablero[i + 1, j - 1] > 10)
                            tableros.Add(obtenerTablero(tablero, i, j, i + 1, j - 1));
                    }
                    // Hacia abajo 1
                    if (tablero[i + 1, j] == 0)
                    {
                        tableros.Add(obtenerTablero(tablero, i, j, i + 1, j));

                        // Hacia abajo 2
                        if (i == 1)
                        {
                            if (tablero[i + 2, j] == 0)
                                tableros.Add(obtenerTablero(tablero, i, j, i + 2, j));
                        }
                    }
                    // Hacia abajo-derecha
                    if (j + 1 < 8)
                    {
                        if (tablero[i + 1, j + 1] > 10)
                            tableros.Add(obtenerTablero(tablero, i, j, i + 1, j + 1));
                    }
                    break;
            }

            return tableros;
        }
    
        public void torre()
        { }

        public List<int[,]> caballo(int[,] tablero, int jugador, int i, int j)
        {
            List<int[,]> tableros = new List<int[,]>();

            switch (jugador)
            {
                // Caballo blanco.
                case 1:
                    // Hacia arriba.
                    if (i - 2 >= 0)
                    {
                        // Izquierda.
                        if (j - 1 >= 0)
                        {
                            if (tablero[i - 2, j - 1] < 10)
                                tableros.Add(obtenerTablero(tablero, i, j, i - 2, j - 1));
                        }
                        // Derecha.
                        if (j + 1 < 8)
                        {
                            if (tablero[i - 2, j + 1] < 10)
                                tableros.Add(obtenerTablero(tablero, i, j, i - 2, j + 1));
                        }
                    }
                    // Hacia abajo.
                    if (i + 2 < 8)
                    {
                        // Izquierda.
                        if (j - 1 >= 0)
                        {
                            if (tablero[i + 2, j - 1] < 10)
                                tableros.Add(obtenerTablero(tablero, i, j, i + 2, j - 1));
                        }
                        // Derecha.
                        if (j + 1 < 8)
                        {
                            if (tablero[i + 2, j + 1] < 10)
                                tableros.Add(obtenerTablero(tablero, i, j, i + 2, j + 1));
                        }
                    }
                    // Hacia la izquierda.
                    if (j - 2 >= 0)
                    {
                        // Arriba.
                        if (i - 1 >= 0)
                        {
                            if (tablero[i - 1, j - 2] < 10)
                                tableros.Add(obtenerTablero(tablero, i, j, i - 1, j - 2));
                        }
                        // Derecha.
                        if (i + 1 < 8)
                        {
                            if (tablero[i + 1, j - 2] < 10)
                                tableros.Add(obtenerTablero(tablero, i, j, i + 1, j - 2));
                        }
                    }
                    // Hacia la derecha.
                    if (j + 2 < 8)
                    {
                        // Arriba.
                        if (i - 1 >= 0)
                        {
                            if (tablero[i - 1, j + 2] < 10)
                                tableros.Add(obtenerTablero(tablero, i, j, i - 1, j + 2));
                        }
                        // Abajo.
                        if (i + 1 < 8)
                        {
                            if (tablero[i + 1, j + 2] < 10)
                                tableros.Add(obtenerTablero(tablero, i, j, i + 1, j + 2));
                        }
                    }
                    break;
                // Caballo negro.
                case 2:
                    // Hacia arriba.
                    if (i - 2 >= 0)
                    {
                        // Hacia la izquierda.
                        if (j - 1 >= 0)
                        {
                            if (tablero[i - 2, j - 1] == 0 || tablero[i - 2, j - 1] > 10)
                                tableros.Add(obtenerTablero(tablero, i, j, i - 2, j - 1));
                        }
                        // Hacia la derecha.
                        if (j + 1 < 8)
                        {
                            if (tablero[i - 2, j + 1] == 0 || tablero[i - 2, j + 1] > 10)
                                tableros.Add(obtenerTablero(tablero, i, j, i - 2, j + 1));
                        }
                    }
                    // Hacia abajo.
                    if (i + 2 < 8)
                    {
                        // Hacia la izquierda.
                        if (j - 1 >= 0)
                        {
                            if (tablero[i + 2, j - 1] == 0 || tablero[i + 2, j - 1] > 10)
                                tableros.Add(obtenerTablero(tablero, i, j, i + 2, j - 1));
                        }
                        // Hacia la derecha.
                        if (j + 1 < 8)
                        {
                            if (tablero[i + 2, j + 1] == 0 || tablero[i + 2, j + 1] > 10)
                                tableros.Add(obtenerTablero(tablero, i, j, i + 2, j + 1));
                        }
                    }
                    // Hacia la izquierda.
                    if (j - 2 >= 0)
                    {
                        // Hacia arriba.
                        if (i - 1 >= 0)
                        {
                            if (tablero[i - 1, j - 2] == 0 || tablero[i - 1, j - 2] > 10)
                                tableros.Add(obtenerTablero(tablero, i, j, i - 1, j - 2));
                        }
                        // Hacia abajo.
                        if (i + 1 < 8)
                        {
                            if (tablero[i + 1, j - 2] == 0 || tablero[i + 1, j - 2] > 10)
                                tableros.Add(obtenerTablero(tablero, i, j, i + 1, j - 2));
                        }
                    }
                    // Hacia la derecha.
                    if (j + 2 < 8)
                    {
                        // Hacia arriba.
                        if (i - 1 >= 0)
                        {
                            if (tablero[i - 1, j + 2] == 0 || tablero[i - 1, j + 2] > 10)
                                tableros.Add(obtenerTablero(tablero, i, j, i - 1, j + 2));
                        }
                        // Hacia abajo.
                        if (i + 1 < 8)
                        {
                            if (tablero[i + 1, j + 2] == 0 || tablero[i + 1, j + 2] > 10)
                                tableros.Add(obtenerTablero(tablero, i, j, i + 1, j + 2));
                        }
                    }
                    break;
            }

            return tableros;
        }

        public void alfil()
        { }
        public void reina()
        { }
        public void rey()
        { }

        public List<Nodo> agregarHijos(List<Nodo> hijos, List<int[,]> tableros) 
        {
            foreach (int[,] tablero in tableros)
            {
                hijos.Add(new Nodo(tablero));
            }

            return hijos;
        }

        public List<Cuadro> obtenerCuadros(int[,] tablero, int jugador)
        {
            List<Cuadro> cuadros = new List<Cuadro>();
            int i, j, length = 8;

            for (i = 0; i < length; i++)
            {
                for (j = 0; j < length; j++)
                {
                    if (tablero[i, j] > 0 && tablero[i, j] < 10 && jugador == 2) // Negras
                    {
                        cuadros.Add(new Cuadro(i, j, tablero[i, j]));
                    }
                    else if (tablero[i, j] > 10 && jugador == 1) // Blancas
                    {
                        cuadros.Add(new Cuadro(i, j, tablero[i, j]));
                    }
                }
            }

            return cuadros;
        }

        public void arbolDeJugadas(int jugador) 
        { 
            List<Cuadro> cuadros = obtenerCuadros(this.raiz.tablero, jugador); 
            
            foreach (Cuadro cuadro in cuadros)
            {
                if (cuadro.codigo == 1 || cuadro.codigo == 11) // Peones
                {
                    this.raiz.hijos = agregarHijos(this.raiz.hijos, peon(this.raiz.tablero, jugador, cuadro.i, cuadro.j));
                }
                else if(cuadro.codigo == 2 || cuadro.codigo == 12) // Torres
                {
                
                }
                else if(cuadro.codigo == 3 || cuadro.codigo == 13) // Caballos
                {
                    this.raiz.hijos = agregarHijos(this.raiz.hijos, caballo(this.raiz.tablero, jugador, cuadro.i, cuadro.j));
                }
                else if(cuadro.codigo == 4 || cuadro.codigo == 14) // Alfiles
                {
                
                }
                else if(cuadro.codigo == 5 || cuadro.codigo == 15) // Reina
                {
                
                }
                else if (cuadro.codigo == 6 || cuadro.codigo == 16) // Rey
                {
                
                }
            }
        }
    }
}
