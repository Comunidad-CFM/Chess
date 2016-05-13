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
        Evaluation evaluation;
        public int cantidadJugadas = 0;

        public Arbol(int[,] tablero) 
        {
            this.raiz = new Nodo(tablero, -100000);
            this.evaluation = new Evaluation();
        }

        /// <summary>
        /// Este metodo obtiene las piezas para un jugador en específico.
        /// </summary>
        /// <param name="tablero">Tablero de juego.</param>
        /// <param name="jugador">Jugador actual.</param>
        /// <returns>Lista de las piezas del jugador.</returns>
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

        /// <summary>
        /// Limpia una posición específica del tablero.
        /// </summary>
        /// <param name="tablero">Tablero de juego.</param>
        /// <param name="i">Fila a limpiar.</param>
        /// <param name="j">Columna a limpiar.</param>
        /// <returns>El tablero con dicha posición vacía.</returns>
        public int[,] limpiarCuadro(int[,] tablero, int i, int j)
        {
            int[,] copiaTablero = new int[8, 8];
            Array.Copy(tablero, copiaTablero, 64);

            copiaTablero[i, j] = 00;

            return copiaTablero;
        }

        /// <summary>
        /// Realiza una jugada en un tablero.
        /// </summary>
        /// <param name="tablero">Tablero de juego.</param>
        /// <param name="i">Fila de la posición vieja de la pieza.</param>
        /// <param name="j">Columna de la posición vieja de la pieza.</param>
        /// <param name="nuevoI">Fila de la nueva posición de la pieza.</param>
        /// <param name="nuevoJ">Columna de la nueva posición de la pieza.</param>
        /// <returns>El tablero con la jugada realizada.</returns>
        public int[,] obtenerTablero(int[,] tablero, int i, int j, int nuevoI, int nuevoJ)
        {
            int[,] copiaTablero = new int[8, 8];
            Array.Copy(tablero, copiaTablero, 64);

            copiaTablero[nuevoI, nuevoJ] = copiaTablero[i, j];
            copiaTablero[i, j] = 00;

            return copiaTablero;
        }

        /// <summary>
        /// Obtiene una lista de tableros las jugadas que puede realizar un peon específico a partir de un tablero.
        /// </summary>
        /// <param name="tablero">Tablero de juego.</param>
        /// <param name="jugador">Jugador actual.</param>
        /// <param name="i">Fila de la posición del peón.</param>
        /// <param name="j">Columna de la posición del peón.</param>
        /// <returns>Lista de tableros.</returns>
        public List<int[,]> peon(int[,] tablero, int jugador, int i, int j) 
        {
            List<int[,]> tableros = new List<int[,]>();

            switch (jugador)
            {
                // Peon blanco.
                case 1:
                    // Hacia la arriba-izquierda.
                    if (j - 1 >= 0 && i - 1 >= 0)
                    {
                        if (tablero[i - 1, j - 1] < 10 && tablero[i - 1, j - 1] != 0)
                            tableros.Add(obtenerTablero(limpiarCuadro(tablero, i - 1, j - 1), i, j, i - 1, j - 1));
                    }
                    // Hacia arriba 1.
                    if (i - 1 >= 0)
                    {
                        if (tablero[i - 1, j] == 0)
                        {
                            tableros.Add(obtenerTablero(tablero, i, j, i - 1, j));

                            // Hacia arriba 2.
                            if (i == 6)
                            {
                                if (i - 2 >= 0)
                                {
                                    if (tablero[i - 2, j] == 0)
                                        tableros.Add(obtenerTablero(tablero, i, j, i - 2, j));
                                }
                            }
                        }
                    }
                    // Hacia la derecha arriba.
                    if (j + 1 < 8 && i - 1 >= 0)
                    {
                        if (tablero[i - 1, j + 1] < 10 && tablero[i - 1, j + 1] != 0)
                            tableros.Add(obtenerTablero(limpiarCuadro(tablero, i - 1, j + 1), i, j, i - 1, j + 1));
                    }
                    break;
                // Peon negro.
                case 2:
                    // Hacia abajo-izquierda.
                    if (j - 1 >= 0 && i + 1 < 8)
                    {
                        if (tablero[i + 1, j - 1] > 10)
                            tableros.Add(obtenerTablero(limpiarCuadro(tablero, i + 1, j - 1), i, j, i + 1, j - 1));
                    }
                    // Hacia abajo 1
                    if (i + 1 < 8)
                    {
                        if (tablero[i + 1, j] == 0)
                        {
                            tableros.Add(obtenerTablero(tablero, i, j, i + 1, j));

                            // Hacia abajo 2
                            if (i == 1)
                            {
                                if (i + 2 < 8)
                                {
                                    if (tablero[i + 2, j] == 0)
                                        tableros.Add(obtenerTablero(tablero, i, j, i + 2, j));
                                }
                            }
                        }
                    }
                    // Hacia abajo-derecha
                    if (j + 1 < 8 && i + 1 < 8)
                    {
                        if (tablero[i + 1, j + 1] > 10)
                            tableros.Add(obtenerTablero(limpiarCuadro(tablero, i + 1, j + 1), i, j, i + 1, j + 1));
                    }
                    break;
            }

            return tableros;
        }

        /// <summary>
        /// Obtiene una lista de tableros las jugadas que puede realizar una torre específica a partir de un tablero.
        /// </summary>
        /// <param name="tablero">Tablero de juego.</param>
        /// <param name="jugador">Jugador actual.</param>
        /// <param name="i">Fila de la posición de la torre.</param>
        /// <param name="j">Columna de la posición de la torre.</param>
        /// <returns>Lista de tableros.</returns>
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
                                tableros.Add(obtenerTablero(limpiarCuadro(tablero, c, j), i, j, c, j));
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
                                tableros.Add(obtenerTablero(limpiarCuadro(tablero, c, j), i, j, c, j));
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
                                tableros.Add(obtenerTablero(limpiarCuadro(tablero, i, c), i, j, i, c));
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
                                tableros.Add(obtenerTablero(limpiarCuadro(tablero, i, c), i, j, i, c));
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
                                tableros.Add(obtenerTablero(limpiarCuadro(tablero, c, j), i, j, c, j));
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
                                tableros.Add(obtenerTablero(limpiarCuadro(tablero, c, j), i, j, c, j));
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
                                tableros.Add(obtenerTablero(limpiarCuadro(tablero, i, c), i, j, i, c));
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
                                tableros.Add(obtenerTablero(limpiarCuadro(tablero, i, c), i, j, i, c));
                                break;
                            }
                        }
                    }
                    break;
            }

            return tableros;
        }

        /// <summary>
        /// Obtiene una lista de tableros las jugadas que puede realizar un caballo específico a partir de un tablero.
        /// </summary>
        /// <param name="tablero">Tablero de juego.</param>
        /// <param name="jugador">Jugador actual.</param>
        /// <param name="i">Fila de la posición del caballo.</param>
        /// <param name="j">Columna de la posición del caballo.</param>
        /// <returns>Lista de tableros.</returns>
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
                                tableros.Add(obtenerTablero(limpiarCuadro(tablero, i - 2, j - 1), i, j, i - 2, j - 1));
                        }
                        // Derecha.
                        if (j + 1 < 8)
                        {
                            if (tablero[i - 2, j + 1] < 10)
                                tableros.Add(obtenerTablero(limpiarCuadro(tablero, i - 2, j + 1), i, j, i - 2, j + 1));
                        }
                    }
                    // Hacia abajo.
                    if (i + 2 < 8)
                    {
                        // Izquierda.
                        if (j - 1 >= 0)
                        {
                            if (tablero[i + 2, j - 1] < 10)
                                tableros.Add(obtenerTablero(limpiarCuadro(tablero, i + 2, j - 1), i, j, i + 2, j - 1));
                        }
                        // Derecha.
                        if (j + 1 < 8)
                        {
                            if (tablero[i + 2, j + 1] < 10)
                                tableros.Add(obtenerTablero(limpiarCuadro(tablero, i + 2, j + 1), i, j, i + 2, j + 1));
                        }
                    }
                    // Hacia la izquierda.
                    if (j - 2 >= 0)
                    {
                        // Arriba.
                        if (i - 1 >= 0)
                        {
                            if (tablero[i - 1, j - 2] < 10)
                                tableros.Add(obtenerTablero(limpiarCuadro(tablero, i - 1, j - 2), i, j, i - 1, j - 2));
                        }
                        // Derecha.
                        if (i + 1 < 8)
                        {
                            if (tablero[i + 1, j - 2] < 10)
                                tableros.Add(obtenerTablero(limpiarCuadro(tablero, i + 1, j - 2), i, j, i + 1, j - 2));
                        }
                    }
                    // Hacia la derecha.
                    if (j + 2 < 8)
                    {
                        // Arriba.
                        if (i - 1 >= 0)
                        {
                            if (tablero[i - 1, j + 2] < 10)
                                tableros.Add(obtenerTablero(limpiarCuadro(tablero, i - 1, j + 2), i, j, i - 1, j + 2));
                        }
                        // Abajo.
                        if (i + 1 < 8)
                        {
                            if (tablero[i + 1, j + 2] < 10)
                                tableros.Add(obtenerTablero(limpiarCuadro(tablero, i + 1, j + 2), i, j, i + 1, j + 2));
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
                                tableros.Add(obtenerTablero(limpiarCuadro(tablero, i - 2, j - 1), i, j, i - 2, j - 1));
                        }
                        // Hacia la derecha.
                        if (j + 1 < 8)
                        {
                            if (tablero[i - 2, j + 1] == 0 || tablero[i - 2, j + 1] > 10)
                                tableros.Add(obtenerTablero(limpiarCuadro(tablero, i - 2, j + 1), i, j, i - 2, j + 1));
                        }
                    }
                    // Hacia abajo.
                    if (i + 2 < 8)
                    {
                        // Hacia la izquierda.
                        if (j - 1 >= 0)
                        {
                            if (tablero[i + 2, j - 1] == 0 || tablero[i + 2, j - 1] > 10)
                                tableros.Add(obtenerTablero(limpiarCuadro(tablero, i + 2, j - 1), i, j, i + 2, j - 1));
                        }
                        // Hacia la derecha.
                        if (j + 1 < 8)
                        {
                            if (tablero[i + 2, j + 1] == 0 || tablero[i + 2, j + 1] > 10)
                                tableros.Add(obtenerTablero(limpiarCuadro(tablero, i + 2, j + 1), i, j, i + 2, j + 1));
                        }
                    }
                    // Hacia la izquierda.
                    if (j - 2 >= 0)
                    {
                        // Hacia arriba.
                        if (i - 1 >= 0)
                        {
                            if (tablero[i - 1, j - 2] == 0 || tablero[i - 1, j - 2] > 10)
                                tableros.Add(obtenerTablero(limpiarCuadro(tablero, i - 1, j - 2), i, j, i - 1, j - 2));
                        }
                        // Hacia abajo.
                        if (i + 1 < 8)
                        {
                            if (tablero[i + 1, j - 2] == 0 || tablero[i + 1, j - 2] > 10)
                                tableros.Add(obtenerTablero(limpiarCuadro(tablero, i + 1, j - 2), i, j, i + 1, j - 2));
                        }
                    }
                    // Hacia la derecha.
                    if (j + 2 < 8)
                    {
                        // Hacia arriba.
                        if (i - 1 >= 0)
                        {
                            if (tablero[i - 1, j + 2] == 0 || tablero[i - 1, j + 2] > 10)
                                tableros.Add(obtenerTablero(limpiarCuadro(tablero, i - 1, j + 2), i, j, i - 1, j + 2));
                        }
                        // Hacia abajo.
                        if (i + 1 < 8)
                        {
                            if (tablero[i + 1, j + 2] == 0 || tablero[i + 1, j + 2] > 10)
                                tableros.Add(obtenerTablero(limpiarCuadro(tablero, i + 1, j + 2), i, j, i + 1, j + 2));
                        }
                    }
                    break;
            }

            return tableros;
        }

        /// <summary>
        /// Obtiene una lista de tableros las jugadas que puede realizar un alfil específico a partir de un tablero.
        /// </summary>
        /// <param name="tablero">Tablero de juego.</param>
        /// <param name="jugador">Jugador actual.</param>
        /// <param name="i">Fila de la posición del alfil.</param>
        /// <param name="j">Columna de la posición del alfil.</param>
        /// <returns>Lista de tableros.</returns>
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
                                tableros.Add(obtenerTablero(limpiarCuadro(tablero, i - c, j - c), i, j, i - c, j - c));
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
                                tableros.Add(obtenerTablero(limpiarCuadro(tablero, i - c, j + c), i, j, i - c, j + c));
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
                                tableros.Add(obtenerTablero(limpiarCuadro(tablero, i + c, j + c), i, j, i + c, j + c));
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
                                tableros.Add(obtenerTablero(limpiarCuadro(tablero, i + c, j - c), i, j, i + c, j - c));
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
                                tableros.Add(obtenerTablero(limpiarCuadro(tablero, i - c, j - c), i, j, i - c, j - c));
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
                                tableros.Add(obtenerTablero(limpiarCuadro(tablero, i - c, j + c), i, j, i - c, j + c));
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
                                tableros.Add(obtenerTablero(limpiarCuadro(tablero, i + c, j + c), i, j, i + c, j + c));
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
                                tableros.Add(obtenerTablero(limpiarCuadro(tablero, i + c, j - c), i, j, i + c, j - c));
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

        /// <summary>
        /// Obtiene una lista de tableros las jugadas que puede realizar la reina a partir de un tablero.
        /// </summary>
        /// <param name="tablero">Tablero de juego.</param>
        /// <param name="jugador">Jugador actual.</param>
        /// <param name="i">Fila de la posición de la reina.</param>
        /// <param name="j">Columna de la posición de la reina.</param>
        /// <returns>Lista de tableros.</returns>
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
                                tableros.Add(obtenerTablero(limpiarCuadro(tablero, i - c, j - c), i, j, i - c, j - c));
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
                                tableros.Add(obtenerTablero(limpiarCuadro(tablero, i - c, j + c), i, j, i - c, j + c));
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
                                tableros.Add(obtenerTablero(limpiarCuadro(tablero, i + c, j + c), i, j, i + c, j + c));
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
                                tableros.Add(obtenerTablero(limpiarCuadro(tablero, i + c, j - c), i, j, i + c, j - c));
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
                                tableros.Add(obtenerTablero(limpiarCuadro(tablero, c, j), i, j, c, j));
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
                                tableros.Add(obtenerTablero(limpiarCuadro(tablero, c, j), i, j, c, j));
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
                                tableros.Add(obtenerTablero(limpiarCuadro(tablero, i, c), i, j, i, c));
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
                                tableros.Add(obtenerTablero(limpiarCuadro(tablero, i, c), i, j, i, c));
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
                                tableros.Add(obtenerTablero(limpiarCuadro(tablero, i, j - c), i, j, i, j - c));
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
                                tableros.Add(obtenerTablero(limpiarCuadro(tablero, i, j + c), i, j, i, j + c));
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
                                tableros.Add(obtenerTablero(limpiarCuadro(tablero, i - c, j), i, j, i - c, j));
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
                                tableros.Add(obtenerTablero(limpiarCuadro(tablero, i + c, j), i, j, i + c, j));
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
                                tableros.Add(obtenerTablero(limpiarCuadro(tablero, i - c, j - c), i, j, i - c, j - c));
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
                                tableros.Add(obtenerTablero(limpiarCuadro(tablero, i - c, j + c), i, j, i - c, j + c));
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
                                tableros.Add(obtenerTablero(limpiarCuadro(tablero, i + c, j + c), i, j, i + c, j + c));
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
                                tableros.Add(obtenerTablero(limpiarCuadro(tablero, i + c, j - c), i, j, i + c, j - c));
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

        /// <summary>
        /// Obtiene una lista de tableros las jugadas que puede realizar el rey a partir de un tablero.
        /// </summary>
        /// <param name="tablero">Tablero de juego.</param>
        /// <param name="jugador">Jugador actual.</param>
        /// <param name="i">Fila de la posición del rey.</param>
        /// <param name="j">Columna de la posición del rey.</param>
        /// <returns>Lista de tableros.</returns>
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
                            tableros.Add(obtenerTablero(limpiarCuadro(tablero, i - 1, j), i, j, i - 1, j));
                    }
                    // Hacia arriba derecha.
                    if (i - 1 >= 0 && j + 1 < 8)
                    {
                        if (tablero[i - 1, j + 1] == 0 || tablero[i - 1, j + 1] < 10)
                            tableros.Add(obtenerTablero(limpiarCuadro(tablero, i - 1, j + 1), i, j, i - 1, j + 1));
                    }
                    // Hacia la derecha.
                    if (j + 1 < 8)
                    {
                        if (tablero[i, j + 1] == 0 || tablero[i, j + 1] < 10)
                            tableros.Add(obtenerTablero(limpiarCuadro(tablero, i, j + 1), i, j, i, j + 1));
                    }
                    // Hacia la derecha abajo.
                    if (i + 1 < 8 && j + 1 < 8)
                    {
                        if (tablero[i + 1, j + 1] == 0 || tablero[i + 1, j + 1] < 10)
                            tableros.Add(obtenerTablero(limpiarCuadro(tablero, i + 1, j + 1), i, j, i + 1, j + 1));
                    }
                    // Hacia abajo.
                    if (i + 1 < 8)
                    {
                        if (tablero[i + 1, j] == 0 || tablero[i + 1, j] < 10)
                            tableros.Add(obtenerTablero(limpiarCuadro(tablero, i + 1, j), i, j, i + 1, j));
                    }
                    // Hacia abajo izquierda.
                    if (i + 1 < 8 && j - 1 >= 0)
                    {
                        if (tablero[i + 1, j - 1] == 0 || tablero[i + 1, j - 1] < 10)
                            tableros.Add(obtenerTablero(limpiarCuadro(tablero, i + 1, j - 1), i, j, i + 1, j - 1));
                    }
                    // Hacia la izquierda.
                    if (j - 1 >= 0)
                    {
                        if (tablero[i, j - 1] == 0 || tablero[i, j - 1] < 10)
                            tableros.Add(obtenerTablero(limpiarCuadro(tablero, i, j - 1), i, j, i, j - 1));
                    }
                    // Hacia la arriba izquierda.
                    if (i - 1 >= 0 && j - 1 >= 0)
                    {
                        if (tablero[i - 1, j - 1] == 0 || tablero[i - 1, j - 1] < 10)
                            tableros.Add(obtenerTablero(limpiarCuadro(tablero, i - 1, j - 1), i, j, i - 1, j - 1));
                    }
                    break;
                // Rey negro.
                case 2:
                    // Hacia arriba.
                    if (i - 1 >= 0)
                    {
                        if (tablero[i - 1, j] == 0 || tablero[i - 1, j] > 10)
                            tableros.Add(obtenerTablero(limpiarCuadro(tablero, i - 1, j), i, j, i - 1, j));
                    }
                    // Hacia arriba derecha
                    if (i - 1 >= 0 && j + 1 < 8)
                    {
                        if (tablero[i - 1, j + 1] == 0 || tablero[i - 1, j + 1] > 10)
                            tableros.Add(obtenerTablero(limpiarCuadro(tablero, i - 1, j + 1), i, j, i - 1, j + 1));
                    }
                    // Hacia la derecha.
                    if (j + 1 < 8)
                    {
                        if (tablero[i, j + 1] == 0 || tablero[i, j + 1] > 10)
                            tableros.Add(obtenerTablero(limpiarCuadro(tablero, i, j + 1), i, j, i, j + 1));
                    }
                    // Hacia abajo derecha.
                    if (i + 1 < 8 && j + 1 < 8)
                    {
                        if (tablero[i + 1, j + 1] == 0 || tablero[i + 1, j + 1] > 10)
                            tableros.Add(obtenerTablero(limpiarCuadro(tablero, i + 1, j + 1), i, j, i + 1, j + 1));
                    }
                    // Hacia abajo.
                    if (i + 1 < 8)
                    {
                        if (tablero[i + 1, j] == 0 || tablero[i + 1, j] > 10)
                            tableros.Add(obtenerTablero(limpiarCuadro(tablero, i + 1, j), i, j, i + 1, j));
                    }
                    // Hacia abajo izquierda.
                    if (i + 1 < 8 && j - 1 >= 0)
                    {
                        if (tablero[i + 1, j - 1] == 0 || tablero[i + 1, j - 1] > 10)
                            tableros.Add(obtenerTablero(limpiarCuadro(tablero, i + 1, j - 1), i, j, i + 1, j - 1));
                    }
                    // Hacia la izquierda.
                    if (j - 1 >= 0)
                    {
                        if (tablero[i, j - 1] == 0 || tablero[i, j - 1] > 10)
                            tableros.Add(obtenerTablero(limpiarCuadro(tablero, i, j - 1), i, j, i, j - 1));
                    }
                    // Hacia la izquierda arriba.
                    if (i - 1 >= 0 && j - 1 >= 0)
                    {
                        if (tablero[i - 1, j - 1] == 0 || tablero[i - 1, j - 1] > 10)
                            tableros.Add(obtenerTablero(limpiarCuadro(tablero, i - 1, j - 1), i, j, i - 1, j - 1));
                    }
                    break;
            }

            return tableros;
        }

        /// <summary>
        /// Agrega hijos a la lista de hijos de un nodo.
        /// </summary>
        /// <param name="hijos">Lista de hijos del nodo.</param>
        /// <param name="tableros">Lista de tableros a agregar a la lista de hijos.</param>
        /// <returns>La lista de hijos con los tableros agregados.</returns>
        public List<Nodo> agregarHijos(List<Nodo> hijos, List<int[,]> tableros) 
        {
            foreach (int[,] tablero in tableros)
            {
                hijos.Add(new Nodo(tablero, -1));
            }

            return hijos;
        }

        /// <summary>
        /// Metodo principal que recorre la lista de las piezas de un jugador y obtiene los tableros con las posibles jugadas.
        /// </summary>
        /// <param name="nodo">Nodo al que se van a buscar sus hijos.</param>
        /// <param name="jugador">Jugador actual.</param>
        /// <returns>Lista con los nodos hijos.</returns>
        public List<Nodo> obtenerNivel(Nodo nodo, int jugador)
        {
            List<Cuadro> cuadros = this.obtenerCuadros(nodo.tablero, jugador);

            foreach (Cuadro cuadro in cuadros)
            {
                switch (cuadro.codigo)
                {
                    // Peones.
                    case 1: case 11:
                        nodo.hijos = agregarHijos(nodo.hijos, peon(nodo.tablero, jugador, cuadro.i, cuadro.j));
                        break;
                    // Torres.
                    case 2: case 12:
                        nodo.hijos = agregarHijos(nodo.hijos, torre(nodo.tablero, jugador, cuadro.i, cuadro.j));
                        break;
                    // Caballos.
                    case 3: case 13:
                        nodo.hijos = agregarHijos(nodo.hijos, caballo(nodo.tablero, jugador, cuadro.i, cuadro.j));
                        break;
                    // Alfiles.
                    case 4: case 14:
                        nodo.hijos = agregarHijos(nodo.hijos, alfil(nodo.tablero, jugador, cuadro.i, cuadro.j));
                        break;
                    // Reina.
                    case 5: case 15:
                        nodo.hijos = agregarHijos(nodo.hijos, reina(nodo.tablero, jugador, cuadro.i, cuadro.j));
                        break;
                    // Rey.
                    case 6: case 16:
                        nodo.hijos = agregarHijos(nodo.hijos, rey(nodo.tablero, jugador, cuadro.i, cuadro.j));
                        break;
                }
            }

            return nodo.hijos;
        }

        /// <summary>
        /// Metodo recursivo que se encarga de generar el arbol de jugadas.
        /// </summary>
        /// <param name="nodo">Nodo al cual se le van a generar sus nodos hijos.</param>
        /// <param name="jugador">Jugador actual.</param>
        /// <param name="profundidad">Numero de profundidad.</param>
        public void arbolDeJugadas(Nodo nodo, int jugador, int profundidad) 
        {
            if (profundidad == 0)
                return;
            nodo.hijos = obtenerNivel(nodo, jugador);
            cantidadJugadas += nodo.hijos.Count;

            jugador = jugador % 2 + 1;
            Parallel.ForEach(nodo.hijos, hijo =>
            {
                hijo.utilidad = evaluation.utilidad(nodo.tablero, jugador % 2 + 1);
                arbolDeJugadas(hijo, jugador, profundidad - 1);
            });
        }
    }
}
