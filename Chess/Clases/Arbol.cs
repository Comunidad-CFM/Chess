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

        public List<int[,]> torre(int[,] tablero, int jugador, int i, int j)
        { 
            List<int[,]> tableros = new List<int[,]>();
            int c;

            switch (jugador)
            { 
                // Torre blanca.
                case 1:
                    // Hacia arriba.
                    for (c = i - 1; c > -1; c--)
                    {
                        if (tablero[c, j] == 0)
                            tableros.Add(obtenerTablero(tablero, i, j, c, j));
                        else
                        {
                            if (tablero[c, j] > 10)
                                break;
                            else
                            {
                                tableros.Add(obtenerTablero(tablero, i, j, c, j));
                                break;
                            }
                        }
                    }
                    // Hacia abajo.
                    for (c = i + 1; c < 8; c++)
                    {
                        if (tablero[c, j] == 0)
                            tableros.Add(obtenerTablero(tablero, i, j, c, j));
                        else
                        {
                            if (tablero[c, j] > 10)
                                break;
                            else
                            {
                                tableros.Add(obtenerTablero(tablero, i, j, c, j));
                                break;
                            }
                        }
                    }
                    // Hacia la izquierda.
                    for (c = j - 1; c > -1; c--)
                    {
                        if (tablero[i, c] == 0)
                            tableros.Add(obtenerTablero(tablero, i, j, i, c));
                        else
                        {
                            if (tablero[i, c] > 10)
                                break;
                            else
                            {
                                tableros.Add(obtenerTablero(tablero, i, j, i, c));
                                break;
                            }
                        }
                    }
                    // Hacia la derecha.
                    for (c = j + 1; c < 8; c++)
                    {
                        if (tablero[i, c] == 0)
                            tableros.Add(obtenerTablero(tablero, i, j, i, c));
                        else
                        {
                            if (tablero[i, c] > 10)
                                break;
                            else
                            {
                                tableros.Add(obtenerTablero(tablero, i, j, i, c));
                                break;
                            }
                        }
                    }
                    break;
                // Torre negra.
                case 2:
                    // Hacia arriba.
                    for (c = i - 1; c > -1; c--)
                    {
                        if (tablero[c, j] == 0)
                            tableros.Add(obtenerTablero(tablero, i, j, c, j));
                        else
                        {
                            if (tablero[c, j] < 10)
                                break;
                            else
                            {
                                tableros.Add(obtenerTablero(tablero, i, j, c, j));
                                break;
                            }
                        }
                    }
                    // Hacia abajo.
                    for (c = i + 1; c < 8; c++)
                    {
                        if (tablero[c, j] == 0)
                            tableros.Add(obtenerTablero(tablero, i, j, c, j));
                        else
                        {
                            if (tablero[c, j] < 10)
                                break;
                            else
                            {
                                tableros.Add(obtenerTablero(tablero, i, j, c, j));
                                break;
                            }
                        }
                    }
                    // Hacia la izquierda.
                    for (c = j - 1; c > -1; c--)
                    {
                        if (tablero[i, c] == 0)
                            tableros.Add(obtenerTablero(tablero, i, j, i, c));
                        else
                        {
                            if (tablero[i, c] < 10)
                                break;
                            else
                            {
                                tableros.Add(obtenerTablero(tablero, i, j, i, c));
                                break;
                            }
                        }
                    }
                    // Hacia la derecha.
                    for (c = j + 1; c < 8; c++)
                    {
                        if (tablero[i, c] == 0)
                            tableros.Add(obtenerTablero(tablero, i, j, i, c));
                        else
                        {
                            if (tablero[i, c] < 10)
                                break;
                            else
                            {
                                tableros.Add(obtenerTablero(tablero, i, j, i, c));
                                break;
                            }
                        }
                    }
                    break;
            }

            return tableros;
        }

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

        public List<int[,]> alfil(int[,] tablero, int jugador, int i, int j)
        { 
            List<int[,]> tableros = new List<int[,]>();
            int c;

            switch (jugador)
            {
                // Alfil blanco.
                case 1:
                    // Hacia arriba izquierda.
                    for (c = 1; c < 8; c++)
                    {
                        if (i - c >= 0 && j - c >= 0)
                        {
                            if (tablero[i - c, j - c] == 0 || tablero[i - c, j - c] < 10)
                            {
                                tableros.Add(obtenerTablero(tablero, i, j, i - c, j - c));
                                if (tablero[i - c, j - c] < 10 && tablero[i - c, j - c] != 0)
                                    break;
                            }
                            else
                                break;
                        }
                        else
                            break;
                    }
                    // Hacia arriba derecha.
                    for (c = 1; c < 8; c++)
                    {
                        if (i - c >= 0 && j + c < 8)
                        {
                            if (tablero[i - c, j + c] == 0 || tablero[i - c, j + c] < 10)
                            {
                                tableros.Add(obtenerTablero(tablero, i, j, i - c, j + c));
                                if (tablero[i - c, j + c] < 10 && tablero[i - c, j + c] != 0)
                                    break;
                            }
                            else
                                break;
                        }
                        else
                            break;
                    }
                    // Hacia abajo derecha.
                    for (c = 1; c < 8; c++)
                    {
                        if (i + c < 8 && j + c < 8)
                        {
                            if (tablero[i + c, j + c] == 0 || tablero[i + c, j + c] < 10)
                            {
                                tableros.Add(obtenerTablero(tablero, i, j, i + c, j + c));
                                if (tablero[i + c, j + c] < 10 && tablero[i + c, j + c] != 0)
                                    break;
                            }
                            else
                                break;
                        }
                        else
                            break;
                    }
                    // Hacia abajo izquierda. 
                    for (c = 1; c < 8; c++)
                    {
                        if (i + c < 8 && j - c >= 0)
                        {
                            if (tablero[i + c, j - c] == 0 || tablero[i + c, j - c] < 10)
                            {
                                tableros.Add(obtenerTablero(tablero, i, j, i + c, j - c));
                                if (tablero[i + c, j - c] < 10 && tablero[i + c, j - c] != 0)
                                    break;
                            }
                            else
                                break;
                        }
                        else
                            break;
                    }
                    break;
                // Alfil negro.
                case 2:
                    // Arriba izquierda.
                    for (c = 1; c < 8; c++)
                    {
                        if (i - c >= 0 && j - c >= 0)
                        {
                            if (tablero[i - c, j - c] == 0 || tablero[i - c, j - c] > 10)
                            {
                                tableros.Add(obtenerTablero(tablero, i, j, i - c, j - c));
                                if (tablero[i - c, j - c] > 10)
                                    break;
                            }
                            else
                                break;
                        }
                        else
                            break;
                    }
                    // Arriba derecha.
                    for (c = 1; c < 8; c++)
                    {
                        if (i - c >= 0 && j + c < 8)
                        {
                            if (tablero[i - c, j + c] == 0 || tablero[i - c, j + c] > 10)
                            {
                                tableros.Add(obtenerTablero(tablero, i, j, i - c, j + c));
                                if (tablero[i - c, j + c] > 10)
                                    break;
                            }
                            else
                                break;
                        }
                        else
                            break;
                    }
                    // Abajo derecha.
                    for (c = 1; c < 8; c++)
                    {
                        if (i + c < 8 && j + c < 8)
                        {
                            if (tablero[i + c, j + c] == 0 || tablero[i + c, j + c] > 10)
                            {
                                tableros.Add(obtenerTablero(tablero, i, j, i + c, j + c));
                                if (tablero[i + c, j + c] > 10)
                                    break;
                            }
                            else
                                break;
                        }
                        else
                            break;
                    }
                    // Abajo izquierda.
                    for (c = 1; c < 8; c++)
                    {
                        if (i + c < 8 && j - c >= 0)
                        {
                            if (tablero[i + c, j - c] == 0 || tablero[i + c, j - c] > 10)
                            {
                                tableros.Add(obtenerTablero(tablero, i, j, i + c, j - c));
                                if (tablero[i + c, j - c] > 10)
                                    break;
                            }
                            else
                                break;
                        }
                        else
                            break;
                    }
                    break;
            }

            return tableros;
        }
        
        public List<int[,]> reina(int[,] tablero, int jugador, int i, int j)
        { 
            List<int[,]> tableros = new List<int[,]>();
            int c;

            switch (jugador)
            {
                // Reina blanca.
                case 1:
                    // Hacia arriba izquierda.
                    for (c = 1; c < 8; c++)
                    {
                        if (i - c >= 0 && j - c >= 0)
                        {
                            if (tablero[i - c, j - c] == 0 || tablero[i - c, j - c] < 10)
                            {
                                tableros.Add(obtenerTablero(tablero, i, j, i - c, j - c));
                                if (tablero[i - c, j - c] < 10 && tablero[i - c, j - c] != 0)
                                    break;
                            }
                            else
                                break;
                        }
                        else
                            break;
                    }
                    // Hacia arriba derecha.
                    for (c = 1; c < 8; c++)
                    {
                        if (i - c >= 0 && j + c < 8)
                        {
                            if (tablero[i - c, j + c] == 0 || tablero[i - c, j + c] < 10)
                            {
                                tableros.Add(obtenerTablero(tablero, i, j, i - c, j + c));
                                if (tablero[i - c, j + c] < 10 && tablero[i - c, j + c] != 0)
                                    break;
                            }
                            else
                                break;
                        }
                        else
                            break;
                    }
                    // Hacia abajo derecha.
                    for (c = 1; c < 8; c++)
                    {
                        if (i + c < 8 && j + c < 8)
                        {
                            if (tablero[i + c, j + c] == 0 || tablero[i + c, j + c] < 10)
                            {
                                tableros.Add(obtenerTablero(tablero, i, j, i + c, j + c));
                                if (tablero[i + c, j + c] < 10 && tablero[i + c, j + c] != 0)
                                    break;
                            }
                            else
                                break;
                        }
                        else
                            break;
                    }
                    // Hacia abajo izquierda.
                    for (c = 1; c < 8; c++)
                    {
                        if (i + c < 8 && j - c >= 0)
                        {
                            if (tablero[i + c, j - c] == 0 || tablero[i + c, j - c] < 10)
                            {
                                tableros.Add(obtenerTablero(tablero, i, j, i + c, j - c));
                                if (tablero[i + c, j - c] < 10 && tablero[i + c, j - c] != 0)
                                    break;
                            }
                            else
                                break;
                        }
                        else
                            break;
                    }
                    // Hacia arriba.
                    for (c = i - 1; c > -1; c--)
                    {
                        if (tablero[c, j] == 0)
                            tableros.Add(obtenerTablero(tablero, i, j, c, j));
                        else
                        {
                            if (tablero[c, j] > 10)
                                break;
                            else
                            {
                                tableros.Add(obtenerTablero(tablero, i, j, c, j));
                                break;
                            }
                        }
                    }
                    // Hacia abajo.
                    for (c = i + 1; c < 8; c++)
                    {
                        if (tablero[c, j] == 0)
                            tableros.Add(obtenerTablero(tablero, i, j, c, j));
                        else
                        {
                            if (tablero[c, j] > 10)
                                break;
                            else
                            {
                                tableros.Add(obtenerTablero(tablero, i, j, c, j));
                                break;
                            }
                        }
                    }
                    // Hacia la izquierda.
                    for (c = j - 1; c > -1; c--)
                    {
                        if (tablero[i, c] == 0)
                            tableros.Add(obtenerTablero(tablero, i, j, i, c));
                        else
                        {
                            if (tablero[i, c] > 10)
                                break;
                            else
                            {
                                tableros.Add(obtenerTablero(tablero, i, j, i, c));
                                break;
                            }
                        }
                    }
                    // Hacia la derecha.
                    for (c = j + 1; c < 8; c++)
                    {
                        if (tablero[i, c] == 0)
                            tableros.Add(obtenerTablero(tablero, i, j, i, c));
                        else
                        {
                            if (tablero[i, c] > 10)
                                break;
                            else
                            {
                                tableros.Add(obtenerTablero(tablero, i, j, i, c));
                                break;
                            }
                        }
                    }
                    break;
                // Reina negra.
                case 2:
                    // Hacia la izquierda.
                    for (c = 1; c < 8; c++)
                    {
                        if (j - c >= 0)
                        {
                            if (tablero[i, j - c] == 0 || tablero[i, j - c] > 10)
                            {
                                tableros.Add(obtenerTablero(tablero, i, j, i, j - c));
                                if (tablero[i, j - c] > 10)
                                    break;
                            }
                            else
                                break;
                        }
                        else
                            break;
                    }
                    // Hacia la derecha.
                    for (c = 1; c < 8; c++)
                    {
                        if (j + c < 8)
                        {
                            if (tablero[i, j + c] == 0 || tablero[i, j + c] > 10)
                            {
                                tableros.Add(obtenerTablero(tablero, i, j, i, j + c));
                                if (tablero[i, j + c] > 10)
                                    break;
                            }
                            else
                                break;
                        }
                        else
                            break;
                    }
                    // Hacia arriba.
                    for (c = 1; c < 8; c++)
                    {
                        if (i - c >= 0)
                        {
                            if (tablero[i - c, j] == 0 || tablero[i - c, j] > 10)
                            {
                                tableros.Add(obtenerTablero(tablero, i, j, i - c, j));
                                if (tablero[i - c, j] > 10)
                                    break;
                            }
                            else
                                break;
                        }
                        else
                            break;
                    }
                    // Hacia abajo.
                    for (c = 1; c < 8; c++)
                    {
                        if (i + c < 8)
                        {
                            if (tablero[i + c, j] == 0 || tablero[i + c, j] > 10)
                            {
                                tableros.Add(obtenerTablero(tablero, i, j, i + c, j));
                                if (tablero[i + c, j] > 10)
                                    break;
                            }
                            else
                                break;
                        }
                        else
                            break;
                    }
                    // Hacia arriba-izquierda
                    for (c = 1; c < 8; c++)
                    {
                        if (i - c >= 0 && j - c >= 0)
                        {
                            if (tablero[i - c, j - c] == 0 || tablero[i - c, j - c] > 10)
                            {
                                tableros.Add(obtenerTablero(tablero, i, j, i - c, j - c));
                                if (tablero[i - c, j - c] > 10)
                                    break;
                            }
                            else
                                break;
                        }
                        else
                            break;
                    }
                    // Hacia arriba-derecha.
                    for (c = 1; c < 8; c++)
                    {
                        if (i - c >= 0 && j + c < 8)
                        {
                            if (tablero[i - c, j + c] == 0 || tablero[i - c, j + c] > 10)
                            {
                                tableros.Add(obtenerTablero(tablero, i, j, i - c, j + c));
                                if (tablero[i - c, j + c] > 10)
                                    break;
                            }
                            else
                                break;
                        }
                        else
                            break;
                    }
                    // Hacia abajo-derecha.
                    for (c = 1; c < 8; c++)
                    {
                        if (i + c < 8 && j + c < 8)
                        {
                            if (tablero[i + c, j + c] == 0 || tablero[i + c, j + c] > 10)
                            {
                                tableros.Add(obtenerTablero(tablero, i, j, i + c, j + c));
                                if (tablero[i + c, j + c] > 10)
                                    break;
                            }
                            else
                                break;
                        }
                        else
                            break;
                    }
                    // Hacia abajo-izquierda.
                    for (c = 1; c < 8; c++)
                    {
                        if (i + c < 8 && j - c >= 0)
                        {
                            if (tablero[i + c, j - c] == 0 || tablero[i + c, j - c] > 10)
                            {
                                tableros.Add(obtenerTablero(tablero, i, j, i + c, j - c));
                                if (tablero[i + c, j - c] > 10)
                                    break;
                            }
                            else
                                break;
                        }
                        else
                            break;
                    }
                    break;
            }

            return tableros;
        }
        
        public List<int[,]> rey(int[,] tablero, int jugador, int i, int j)
        { 
            List<int[,]> tableros = new List<int[,]>();

            switch (jugador)
            {
                // Rey blanco.
                case 1:
                    // Hacia arriba.
                    if (i - 1 >= 0)
                    {
                        if (tablero[i - 1, j] == 0 || tablero[i - 1, j] < 10)
                            tableros.Add(obtenerTablero(tablero, i, j, i - 1, j));
                    }
                    // Hacia arriba derecha.
                    if (i - 1 >= 0 && j + 1 < 8)
                    {
                        if (tablero[i - 1, j + 1] == 0 || tablero[i - 1, j + 1] < 10)
                            tableros.Add(obtenerTablero(tablero, i, j, i - 1, j + 1));
                    }
                    // Hacia la derecha.
                    if (j + 1 < 8)
                    {
                        if (tablero[i, j + 1] == 0 || tablero[i, j + 1] < 10)
                            tableros.Add(obtenerTablero(tablero, i, j, i, j + 1));
                    }
                    // Hacia la derecha abajo.
                    if (i + 1 < 8 && j + 1 < 8)
                    {
                        if (tablero[i + 1, j + 1] == 0 || tablero[i + 1, j + 1] < 10)
                            tableros.Add(obtenerTablero(tablero, i, j, i + 1, j + 1));
                    }
                    // Hacia abajo.
                    if (i + 1 < 8)
                    {
                        if (tablero[i + 1, j] == 0 || tablero[i + 1, j] < 10)
                            tableros.Add(obtenerTablero(tablero, i, j, i + 1, j));
                    }
                    // Hacia abajo izquierda.
                    if (i + 1 < 8 && j - 1 >= 0)
                    {
                        if (tablero[i + 1, j - 1] == 0 || tablero[i + 1, j - 1] < 10)
                            tableros.Add(obtenerTablero(tablero, i, j, i + 1, j - 1));
                    }
                    // Hacia la izquierda.
                    if (j - 1 >= 0)
                    {
                        if (tablero[i, j - 1] == 0 || tablero[i, j - 1] < 10)
                            tableros.Add(obtenerTablero(tablero, i, j, i, j - 1));
                    }
                    // Hacia la arriba izquierda.
                    if (i - 1 >= 0 && j - 1 >= 0)
                    {
                        if (tablero[i - 1, j - 1] == 0 || tablero[i - 1, j - 1] < 10)
                            tableros.Add(obtenerTablero(tablero, i, j, i - 1, j - 1));
                    }
                    break;
                // Rey negro.
                case 2:
                    // Hacia arriba.
                    if (i - 1 >= 0)
                    {
                        if (tablero[i - 1, j] == 0 || tablero[i - 1, j] > 10)
                            tableros.Add(obtenerTablero(tablero, i, j, i - 1, j));
                    }
                    // Hacia arriba derecha
                    if (i - 1 >= 0 && j + 1 < 8)
                    {
                        if (tablero[i - 1, j + 1] == 0 || tablero[i - 1, j + 1] > 10)
                            tableros.Add(obtenerTablero(tablero, i, j, i - 1, j + 1));
                    }
                    // Hacia la derecha.
                    if (j + 1 < 8)
                    {
                        if (tablero[i, j + 1] == 0 || tablero[i, j + 1] > 10)
                            tableros.Add(obtenerTablero(tablero, i, j, i, j + 1));
                    }
                    // Hacia abajo derecha.
                    if (i + 1 < 8 && j + 1 < 8)
                    {
                        if (tablero[i + 1, j + 1] == 0 || tablero[i + 1, j + 1] > 10)
                            tableros.Add(obtenerTablero(tablero, i, j, i + 1, j + 1));
                    }
                    // Hacia abajo.
                    if (i + 1 < 8)
                    {
                        if (tablero[i + 1, j] == 0 || tablero[i + 1, j] > 10)
                            tableros.Add(obtenerTablero(tablero, i, j, i + 1, j));
                    }
                    // Hacia abajo izquierda.
                    if (i + 1 < 8 && j - 1 >= 0)
                    {
                        if (tablero[i + 1, j - 1] == 0 || tablero[i + 1, j - 1] > 10)
                            tableros.Add(obtenerTablero(tablero, i, j, i + 1, j - 1));
                    }
                    // Hacia la izquierda.
                    if (j - 1 >= 0)
                    {
                        if (tablero[i, j - 1] == 0 || tablero[i, j - 1] > 10)
                            tableros.Add(obtenerTablero(tablero, i, j, i, j - 1));
                    }
                    // Hacia la izquierda arriba.
                    if (i - 1 >= 0 && j - 1 >= 0)
                    {
                        if (tablero[i - 1, j - 1] == 0 || tablero[i - 1, j - 1] > 10)
                            tableros.Add(obtenerTablero(tablero, i, j, i - 1, j - 1));
                    }
                    break;
            }

            return tableros;
        }

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

        public List<Nodo> obtenerNivel(Nodo nodo, int jugador)
        {
            List<Cuadro> cuadros = obtenerCuadros(nodo.tablero, jugador);

            foreach (Cuadro cuadro in cuadros)
            {
                if (cuadro.codigo == 1 || cuadro.codigo == 11) // Peones
                {
                    nodo.hijos = agregarHijos(nodo.hijos, peon(nodo.tablero, jugador, cuadro.i, cuadro.j));
                }
                else if (cuadro.codigo == 2 || cuadro.codigo == 12) // Torres
                {
                    nodo.hijos = agregarHijos(nodo.hijos, torre(nodo.tablero, jugador, cuadro.i, cuadro.j));
                }
                else if (cuadro.codigo == 3 || cuadro.codigo == 13) // Caballos
                {
                    nodo.hijos = agregarHijos(nodo.hijos, caballo(nodo.tablero, jugador, cuadro.i, cuadro.j));
                }
                else if (cuadro.codigo == 4 || cuadro.codigo == 14) // Alfiles
                {
                    nodo.hijos = agregarHijos(nodo.hijos, alfil(nodo.tablero, jugador, cuadro.i, cuadro.j));
                }
                else if (cuadro.codigo == 5 || cuadro.codigo == 15) // Reina
                {
                    nodo.hijos = agregarHijos(nodo.hijos, reina(nodo.tablero, jugador, cuadro.i, cuadro.j));
                }
                else if (cuadro.codigo == 6 || cuadro.codigo == 16) // Rey
                {
                    nodo.hijos = agregarHijos(nodo.hijos, rey(nodo.tablero, jugador, cuadro.i, cuadro.j));
                }
            }

            return nodo.hijos;
        }

        public void arbolDeJugadas(Nodo nodo, int jugador, int profundidad) 
        {
            if (profundidad == 0)
                return;
            nodo.hijos = obtenerNivel(nodo, jugador);

            jugador = jugador % 2 + 1;
            foreach (Nodo hijo in nodo.hijos)
            {
                arbolDeJugadas(hijo, jugador, profundidad - 1);
            }            
        }
    }
}
