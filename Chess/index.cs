﻿using Chess.Clases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chess
{
    public partial class index : Form
    {
        int[,] table,
               bits,
               jugadaAnterior = new int[8, 8];

        Pieza[,] tablero;
        Arbol arbol;

        Nodo nodo;
        MiniMax miniMax;
        AlphaBeta alphaBeta;
        AlphaBetaSort alphaBetaSort;
        Stopwatch timer;
        int I, 
            J,
            turnoActual = 1;
        bool move_QB = true,
             move_RB = true,
             move_QW = true,
             move_RW = true,
             devuelto = false;

        public index()
        {
            InitializeComponent();
            prepararTableros();


            // Para las blancas
            // 11: Peon
            // 12: Torre
            // 13: Caballo
            // 14: Alfil
            // 15: Reina:
            // 16: Rey

            // Para las negras
            // 01: Peon
            // 02: Torre
            // 03: Caballo
            // 04: Alfil
            // 05: Reina:
            // 06: Rey

            // Para las vacías
            // 00
        }

        private void index_Load(object sender, EventArgs e)
        { }

        /// <summary>
        /// Cambia el turno de los jugadores.
        /// </summary>
        public void cambiarTurno() 
        { 
            turnoActual = turnoActual % 2 + 1;
        }

        /// <summary>
        /// Pinta en la UI el jugador actual.
        /// </summary>
        public void setJugadorActual() {
            if (turnoActual == 1)
            {
                jugadorActual.ImageLocation = @"..\..\Imagenes\knight_white.png";
            }
            else 
            {
                jugadorActual.ImageLocation = @"..\..\Imagenes\knight_black.png";
            }
        }

        /// <summary>
        /// Inicializa los tableros utilizados para el juego.
        /// </summary>
        public void inicializarTableros() 
        {
            this.tablero = new Pieza[8, 8];
            this.bits = new int[8, 8];
            this.table = new int[8, 8]
            {
                {02, 03, 04, 05, 06, 04, 03, 02},
                {01, 01, 01, 01, 01, 01, 01, 01},
                {00, 00, 00, 00, 00, 00, 00, 00},
                {00, 00, 00, 00, 00, 00, 00, 00},
                {00, 00, 00, 00, 00, 00, 00, 00},
                {00, 00, 00, 00, 00, 00, 00, 00},
                {11, 11, 11, 11, 11, 11, 11, 11},
                {12, 13, 14, 15, 16, 14, 13, 12},
            };
        }

        /// <summary>
        /// Resetea uno de los tableros utilizados.
        /// </summary>
        public void resetearBits()
        {
            int i, j, length = 8;

            for (i = 0; i < length; i++)
            {
                for (j = 0; j < length; j++)
                {
                    this.bits[i, j] = 0;
                }
            }

            I = 0;
            J = 0;
        }

        /// <summary>
        /// Valida si es fin de juego.
        /// </summary>
        public void finDeJuego() 
        {
            bool reyNegro = buscarPieza(this.table, 6);
            bool reyBlanco = buscarPieza(this.table, 16);

            if (!reyNegro) // Ganan las blancas.
            {
                ganador.ImageLocation = @"..\..\Imagenes\knight_white.png";
                grupoGanador.Visible = true;
            }
            else if (!reyBlanco) // Ganan las negras.
            {
                ganador.ImageLocation = @"..\..\Imagenes\knight_black.png";
                grupoGanador.Visible = true;
            }
        }

        /// <summary>
        /// Actualiza los tableros de juego.
        /// </summary>
        public void validar()
        {
            int i, j, length = 8;

            for (i = 0; i < length; i++)
                for (j = 0; j < length; j++)
                {
                    if (this.table[i, j] != 0)
                        this.bits[i, j] = 1;
                    else
                        this.bits[i, j] = 0;
                }
            for (i = 0; i < length; i++)
            {
                for (j = 0; j < length; j++)
                {
                    if (i % 2 == 0)
                        if (j % 2 == 1)
                            this.tablero[i, j].BackColor = Color.SandyBrown;
                        else
                            this.tablero[i, j].BackColor = Color.PeachPuff;
                    else
                        if (j % 2 == 1)
                            this.tablero[i, j].BackColor = Color.PeachPuff;
                        else
                            this.tablero[i, j].BackColor = Color.SandyBrown;
                }
            }
        }

        /// <summary>
        /// Cambia piezas en el tablero de juego.
        /// </summary>
        /// <param name="i">Fila.</param>
        /// <param name="j">Columna.</param>
        public void cambiarPiezas(int i, int j)
        {
            int codigo = this.table[i, j];

            if (this.table[I, J] == 02)
                move_RB = false;
            if (this.table[I, J] == 12)
                move_RW = false;
            if (this.table[I, J] == 06)
                move_QB = false;
            if (this.table[I, J] == 16)
                move_QW = false;

            this.table[i, j] = this.table[I, J];

            if (this.table[I, J] == 06)
            {
                if (i == 0 && j == 2)
                { this.table[0, 3] = 02; this.table[0, 0] = 0; }
                if (i == 0 && j == 6)
                { this.table[0, 5] = 02; this.table[0, 7] = 0; }
            }
            if (this.table[I, J] == 16)
            {
                if (i == 7 && j == 2)
                { this.table[7, 3] = 02; this.table[7, 0] = 0; }
                if (i == 7 && j == 6)
                { this.table[7, 5] = 02; this.table[7, 7] = 0; }
            }
            this.table[I, J] = 0;
            validar();
            dibujar();
            marcarJugadas();
            finDeJuego();
        }

        /// <summary>
        /// Dibuja el tablero de juego en la UI.
        /// </summary>
        public void dibujar() 
        {
            int i, j, length = 8;

            for (i = 0; i < length; i++)
            {
                for (j = 0; j < length; j++)
                {
                    switch (this.table[i, j])
                    {
                        case 00: this.tablero[i, j].BackgroundImage = null; break;
                        case 01: this.tablero[i, j].BackgroundImage = System.Drawing.Image.FromFile(@"..\..\Imagenes\knight_black.png"); break;
                        case 02: this.tablero[i, j].BackgroundImage = System.Drawing.Image.FromFile(@"..\..\Imagenes\tower_black.png"); break;
                        case 03: this.tablero[i, j].BackgroundImage = System.Drawing.Image.FromFile(@"..\..\Imagenes\horse_black.png"); break;
                        case 04: this.tablero[i, j].BackgroundImage = System.Drawing.Image.FromFile(@"..\..\Imagenes\bishop-black.png"); break;
                        case 05: this.tablero[i, j].BackgroundImage = System.Drawing.Image.FromFile(@"..\..\Imagenes\queen_black.png"); break;
                        case 06: this.tablero[i, j].BackgroundImage = System.Drawing.Image.FromFile(@"..\..\Imagenes\king_black.png"); break;
                        case 11: this.tablero[i, j].BackgroundImage = System.Drawing.Image.FromFile(@"..\..\Imagenes\knight_white.png"); break;
                        case 12: this.tablero[i, j].BackgroundImage = System.Drawing.Image.FromFile(@"..\..\Imagenes\tower_white.png"); break;
                        case 13: this.tablero[i, j].BackgroundImage = System.Drawing.Image.FromFile(@"..\..\Imagenes\horse_white.png"); break;
                        case 14: this.tablero[i, j].BackgroundImage = System.Drawing.Image.FromFile(@"..\..\Imagenes\bishop-white.png"); break;
                        case 15: this.tablero[i, j].BackgroundImage = System.Drawing.Image.FromFile(@"..\..\Imagenes\queen_white.png"); break;
                        case 16: this.tablero[i, j].BackgroundImage = System.Drawing.Image.FromFile(@"..\..\Imagenes\king_white.png"); break;
                    }
                }
            }
        }

        /// <summary>
        /// Prepara los tableros para el inicio del juego.
        /// </summary>
        public void prepararTableros()
        {
            inicializarTableros();
            setJugadorActual();

            int i, j, length = 8;

            for (i = 0; i < length; i++)
            {
                for (j = 0; j < length; j++)
                {
                    if (this.table[i, j] != 0)
                    {
                        this.bits[i, j] = 1;
                    }
                    else {
                        this.bits[i, j] = 1;
                    }

                    this.tablero[i, j] = new Pieza();
                    this.tablero[i, j].Parent = this;
                    this.tablero[i, j].i = i;
                    this.tablero[i, j].j = j;
                    this.tablero[i, j].Location = new Point(j * 50 + 50, i * 50 + 50);
                    this.tablero[i, j].Size = new Size(50, 50);
                    this.tablero[i, j].Click += new EventHandler(click);

                    if (i % 2 == 0)
                    {
                        if (j % 2 == 1)
                        {
                            this.tablero[i, j].BackColor = Color.SandyBrown;
                        }
                        else
                        {
                            this.tablero[i, j].BackColor = Color.PeachPuff;
                        }
                    }
                    else
                    {
                        if (j % 2 == 1)
                        {
                            this.tablero[i, j].BackColor = Color.PeachPuff;
                        }
                        else
                        {
                            this.tablero[i, j].BackColor = Color.SandyBrown;
                        }
                    }

                    this.tablero[i, j].BackgroundImageLayout = ImageLayout.Center;
                }
            }

            dibujar();
        }

        /// <summary>
        /// Da sugerencias de las jugadas que puede realizar una pieza seleccionada.
        /// </summary>
        public void marcarJugadas()
        {
            int i, j, length = 8;

            for (i = 0; i < length; i++)
            {
                for (j = 0; j < length; j++)
                {
                    if (this.bits[i, j] == 2)
                    {
                        this.tablero[i, j].BackColor = System.Drawing.ColorTranslator.FromHtml("#3A5022");
                    }
                    else
                        if (i % 2 == 0)
                        {
                            if (j % 2 == 1)
                            {
                                this.tablero[i, j].BackColor = Color.SandyBrown;
                            }
                            else
                            {
                                this.tablero[i, j].BackColor = Color.PeachPuff;
                            }
                        }
                        else
                        {
                            if (j % 2 == 1)
                            {
                                this.tablero[i, j].BackColor = Color.PeachPuff;
                            }
                            else
                            {
                                this.tablero[i, j].BackColor = Color.SandyBrown;
                            }
                        }
                    if (this.bits[i, j] == 3)
                    {
                        this.tablero[i, j].BackColor = System.Drawing.ColorTranslator.FromHtml("#E74C3C");
                    }
                }
            }
        }

        /// <summary>
        /// Valida si una jugada es valida.
        /// </summary>
        /// <param name="codigo">Codigo de la pieza.</param>
        /// <param name="i">Fila de la posición de la pieza.</param>
        /// <param name="j">Columna de la posición de la pieza.</param>
        public void validarJugada(int codigo, int i, int j)
        {
            resetearBits();
            int c;

            switch (codigo)
            {
                // Peon negro.
                case 1:
                    // Hacia abajo-izquierda.
                    if (j - 1 >= 0)
                    {
                        if (this.table[i + 1, j - 1] > 10)
                            this.bits[i + 1, j - 1] = 2;
                    }
                    // Hacia abajo 1
                    if (this.table[i + 1, j] == 0)
                    {
                        this.bits[i + 1, j] = 2;

                        // Hacia abajo 2
                        if (i == 1)
                        {
                            if (this.table[i + 2, j] == 0)
                                this.bits[i + 2, j] = 2;
                        }
                    }
                    // Hacia abajo-derecha
                    if (j + 1 < 8)
                    {
                        if (this.table[i + 1, j + 1] > 10)
                            this.bits[i + 1, j + 1] = 2;
                    }
                    break;
                // Torre negra.
                case 2:
                    // Hacia arriba.
                    for (c = i - 1; c > -1; c--)
                    {
                        if (this.table[c, j] == 0)
                            this.bits[c, j] = 2;
                        else
                        {
                            if (this.table[c, j] < 10)
                                break;
                            else
                            {
                                this.bits[c, j] = 2;
                                break;
                            }
                        }
                    }
                    // Hacia abajo.
                    for (c = i + 1; c < 8; c++)
                    {
                        if (this.table[c, j] == 0)
                            this.bits[c, j] = 2;
                        else
                        {
                            if (this.table[c, j] < 10)
                                break;
                            else
                            {
                                this.bits[c, j] = 2;
                                break;
                            }
                        }
                    }
                    // Hacia la izquierda.
                    for (c = j - 1; c > -1; c--)
                    {
                        if (this.table[i, c] == 0)
                            this.bits[i, c] = 2;
                        else
                        {
                            if (this.table[i, c] < 10)
                                break;
                            else
                            {
                                this.bits[i, c] = 2; 
                                break;
                            }
                        }
                    }
                    // Hacia la derecha.
                    for (c = j + 1; c < 8; c++)
                    {
                        if (this.table[i, c] == 0)
                            this.bits[i, c] = 2;
                        else
                        {
                            if (this.table[i, c] < 10)
                                break;
                            else
                            {
                                this.bits[i, c] = 2;
                                break;
                            }
                        }
                    }
                    break;
                // Caballo negro.
                case 3:
                    // Hacia arriba.
                    if (i - 2 >= 0)
                    {
                        // Hacia la izquierda.
                        if (j - 1 >= 0)
                        {
                            if (this.table[i - 2, j - 1] == 0 || this.table[i - 2, j - 1] > 10)
                                this.bits[i - 2, j - 1] = 2;
                        }
                        // Hacia la derecha.
                        if (j + 1 < 8)
                        {
                            if (this.table[i - 2, j + 1] == 0 || this.table[i - 2, j + 1] > 10)
                                this.bits[i - 2, j + 1] = 2;
                        }
                    }
                    // Hacia abajo.
                    if (i + 2 < 8)
                    {
                        // Hacia la izquierda.
                        if (j - 1 >= 0)
                        {
                            if (this.table[i + 2, j - 1] == 0 || this.table[i + 2, j - 1] > 10)
                                this.bits[i + 2, j - 1] = 2;
                        }
                        // Hacia la derecha.
                        if (j + 1 < 8)
                        {
                            if (this.table[i + 2, j + 1] == 0 || this.table[i + 2, j + 1] > 10)
                                this.bits[i + 2, j + 1] = 2;
                        }
                    }
                    // Hacia la izquierda.
                    if (j - 2 >= 0)
                    {
                        // Hacia arriba.
                        if (i - 1 >= 0)
                        {
                            if (this.table[i - 1, j - 2] == 0 || this.table[i - 1, j - 2] > 10)
                                this.bits[i - 1, j - 2] = 2;
                        }
                        // Hacia abajo.
                        if (i + 1 < 8)
                        {
                            if (this.table[i + 1, j - 2] == 0 || this.table[i + 1, j - 2] > 10)
                                this.bits[i + 1, j - 2] = 2;
                        }
                    }
                    // Hacia la derecha.
                    if (j + 2 < 8)
                    {
                        // Hacia arriba.
                        if (i - 1 >= 0)
                        {
                            if (this.table[i - 1, j + 2] == 0 || this.table[i - 1, j + 2] > 10)
                                this.bits[i - 1, j + 2] = 2;
                        }
                        // Hacia abajo.
                        if (i + 1 < 8)
                        {
                            if (this.table[i + 1, j + 2] == 0 || this.table[i + 1, j + 2] > 10)
                                this.bits[i + 1, j + 2] = 2;
                        }
                    } 
                    break;
                // Alfil negro.
                case 4:
                    // Arriba izquierda.
                    for (c = 1; c < 8; c++)
                    {
                        if (i - c >= 0 && j - c >= 0)
                        {
                            if (this.table[i - c, j - c] == 0 || this.table[i - c, j - c] > 10)
                            {
                                this.bits[i - c, j - c] = 2;
                                if (this.table[i - c, j - c] > 10)
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
                            if (this.table[i - c, j + c] == 0 || this.table[i - c, j + c] > 10)
                            { 
                                this.bits[i - c, j + c] = 2; 
                                if (this.table[i - c, j + c] > 10) 
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
                            if (this.table[i + c, j + c] == 0 || this.table[i + c, j + c] > 10)
                            { 
                                this.bits[i + c, j + c] = 2; 
                                if (this.table[i + c, j + c] > 10) 
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
                            if (this.table[i + c, j - c] == 0 || this.table[i + c, j - c] > 10)
                            { 
                                this.bits[i + c, j - c] = 2;
                                if (this.table[i + c, j - c] > 10)
                                    break; 
                            }
                            else
                                break;
                        }
                        else
                            break;
                    }
                    break;
                // Reina negra.
                case 5:
                    // Hacia la izquierda.
                    for (c = 1; c < 8; c++)
                    {
                        if (j - c >= 0)
                        {
                            if (this.table[i, j - c] == 0 || this.table[i, j - c] > 10)
                            {
                                this.bits[i, j - c] = 2;
                                if (this.table[i, j - c] > 10)
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
                            if (this.table[i, j + c] == 0 || this.table[i, j + c] > 10)
                            {
                                this.bits[i, j + c] = 2;
                                if (this.table[i, j + c] > 10)
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
                            if (this.table[i - c, j] == 0 || this.table[i - c, j] > 10)
                            {
                                this.bits[i - c, j] = 2;
                                if (this.table[i - c, j] > 10)
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
                            if (this.table[i + c, j] == 0 || this.table[i + c, j] > 10)
                            {
                                this.bits[i + c, j] = 2;
                                if (this.table[i + c, j] > 10)
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
                            if (this.table[i - c, j - c] == 0 || this.table[i - c, j - c] > 10)
                            {
                                this.bits[i - c, j - c] = 2;
                                if (this.table[i - c, j - c] > 10)
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
                            if (this.table[i - c, j + c] == 0 || this.table[i - c, j + c] > 10)
                            {
                                this.bits[i - c, j + c] = 2;
                                if (this.table[i - c, j + c] > 10)
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
                            if (this.table[i + c, j + c] == 0 || this.table[i + c, j + c] > 10)
                            {
                                this.bits[i + c, j + c] = 2;
                                if (this.table[i + c, j + c] > 10)
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
                            if (this.table[i + c, j - c] == 0 || this.table[i + c, j - c] > 10)
                            { 
                                this.bits[i + c, j - c] = 2;
                                if (this.table[i + c, j - c] > 10)
                                    break;
                            }
                            else
                                break;
                        }
                        else
                            break;
                    }
                    break;
                // Rey negro.
                case 6:
                    // Hacia arriba.
                    if (i - 1 >= 0)
                    {
                        if (this.table[i - 1, j] == 0 || this.table[i - 1, j] > 10)
                            this.bits[i - 1, j] = 2;
                    }
                    // Hacia arriba derecha
                    if (i - 1 >= 0 && j + 1 < 8)
                    {
                        if (this.table[i - 1, j + 1] == 0 || this.table[i - 1, j + 1] > 10)
                            this.bits[i - 1, j + 1] = 2;
                    }
                    // Hacia la derecha.
                    if (j + 1 < 8)
                    {
                        if (this.table[i, j + 1] == 0 || this.table[i, j + 1] > 10)
                            this.bits[i, j + 1] = 2;
                    }
                    // Hacia abajo derecha.
                    if (i + 1 < 8 && j + 1 < 8)
                    {
                        if (this.table[i + 1, j + 1] == 0 || this.table[i + 1, j + 1] > 10)
                            this.bits[i + 1, j + 1] = 2;
                    }
                    // Hacia abajo.
                    if (i + 1 < 8)
                    {
                        if (this.table[i + 1, j] == 0 || this.table[i + 1, j] > 10)
                            this.bits[i + 1, j] = 2;
                    }
                    // Hacia abajo izquierda.
                    if (i + 1 < 8 && j - 1 >= 0)
                    {
                        if (this.table[i + 1, j - 1] == 0 || this.table[i + 1, j - 1] > 10)
                            this.bits[i + 1, j - 1] = 2;
                    }
                    // Hacia la izquierda.
                    if (j - 1 >= 0)
                    {
                        if (this.table[i, j - 1] == 0 || this.table[i, j - 1] > 10)
                            this.bits[i, j - 1] = 2;
                    }
                    // Hacia la izquierda arriba.
                    if (i - 1 >= 0 && j - 1 >= 0)
                    {
                        if (this.table[i - 1, j - 1] == 0 || this.table[i - 1, j - 1] > 10)
                            this.bits[i - 1, j - 1] = 2;
                    }
                    if (move_QB && move_RB)
                    {
                        if (this.table[0, 1] == 0 && this.table[0, 2] == 0 && this.table[0, 3] == 0)
                            this.bits[0, 2] = 2;
                        if (this.table[0, 5] == 0 && this.table[0, 6] == 0)
                            this.bits[0, 6] = 2;
                    }
                    break;
                // Peon blanco.
                case 11:
                    // Hacia la izquierda.
                    if (j - 1 >= 0)
                    {
                        if (this.table[i - 1, j - 1] < 10 && this.table[i - 1, j - 1] != 0)
                            this.bits[i - 1, j - 1] = 2;
                    }
                    // Hacia arriba 1.
                    if (this.table[i - 1, j] == 0)
                    {
                        this.bits[i - 1, j] = 2;

                        // Hacia arriba 2.
                        if (i == 6)
                        {
                            if (this.table[i - 2, j] == 0)
                                this.bits[i - 2, j] = 2;
                        }
                    }
                    // Hacia la derecha.
                    if (j + 1 < 8)
                    {
                        if (this.table[i - 1, j + 1] < 10 && this.table[i - 1, j + 1] != 0)
                            this.bits[i - 1, j + 1] = 2;
                    }
                    break;
                // Torre blanca.
                case 12:
                    // Hacia arriba.
                    for (c = i - 1; c > -1; c--)
                    {
                        if (this.table[c, j] == 0)
                            this.bits[c, j] = 2;
                        else
                        {
                            if (this.table[c, j] > 10)
                                break;
                            else
                            {
                                this.bits[c, j] = 2;
                                break;
                            }
                        }
                    }
                    // Hacia abajo.
                    for (c = i + 1; c < 8; c++)
                    {
                        if (this.table[c, j] == 0)
                            this.bits[c, j] = 2;
                        else
                        {
                            if (this.table[c, j] > 10)
                                break;
                            else
                            {
                                this.bits[c, j] = 2;
                                break;
                            }
                        }
                    }
                    // Hacia la izquierda.
                    for (c = j - 1; c > -1; c--)
                    {
                        if (this.table[i, c] == 0)
                            this.bits[i, c] = 2;
                        else
                        {
                            if (this.table[i, c] > 10)
                                break;
                            else
                            {
                                this.bits[i, c] = 2;
                                break;
                            }
                        }
                    }
                    // Hacia la derecha.
                    for (c = j + 1; c < 8; c++)
                    {
                        if (this.table[i, c] == 0)
                            this.bits[i, c] = 2;
                        else
                        {
                            if (this.table[i, c] > 10)
                                break;
                            else
                            {
                                this.bits[i, c] = 2;
                                break;
                            }
                        }
                    }
                    break;
                // Caballo blanco.
                case 13:
                    // Hacia la izquierda.
                    if (i - 2 >= 0)
                    {
                        // Arriba.
                        if (j - 1 >= 0)
                        {
                            if (this.table[i - 2, j - 1] < 10)
                                this.bits[i - 2, j - 1] = 2;
                        }
                        // Abajo.
                        if (j + 1 < 8)
                        {
                            if (this.table[i - 2, j + 1] < 10)
                                this.bits[i - 2, j + 1] = 2;
                        }
                    }
                    // Hacia la derecha.
                    if (i + 2 < 8)
                    {
                        // Arriba.
                        if (j - 1 >= 0)
                        {
                            if (this.table[i + 2, j - 1] < 10)
                                this.bits[i + 2, j - 1] = 2;
                        }
                        // Abajo.
                        if (j + 1 < 8)
                        {
                            if (this.table[i + 2, j + 1] < 10)
                                this.bits[i + 2, j + 1] = 2;
                        }
                    }
                    // Hacia arriba.
                    if (j - 2 >= 0)
                    {
                        // Izquierda.
                        if (i - 1 >= 0)
                        {
                            if (this.table[i - 1, j - 2] < 10)
                                this.bits[i - 1, j - 2] = 2;
                        }
                        // Derecha.
                        if (i + 1 < 8)
                        {
                            if (this.table[i + 1, j - 2] < 10)
                                this.bits[i + 1, j - 2] = 2;
                        }
                    }
                    // Hacia arriba.
                    if (j + 2 < 8)
                    {
                        // ariba.
                        if (i - 1 >= 0)
                        {
                            if (this.table[i - 1, j + 2] < 10)
                                this.bits[i - 1, j + 2] = 2;
                        }
                        // Abajo.
                        if (i + 1 < 8)
                        {
                            if (this.table[i + 1, j + 2] < 10)
                                this.bits[i + 1, j + 2] = 2;
                        }
                    } 
                    break;
                // Alfil blanco.
                case 14:
                    // Hacia arriba izquierda.
                    for (c = 1; c < 8; c++)
                    {
                        if (i - c >= 0 && j - c >= 0)
                        {
                            if (this.table[i - c, j - c] == 0 || this.table[i - c, j - c] < 10)
                            { 
                                this.bits[i - c, j - c] = 2; 
                                if (this.table[i - c, j - c] < 10 && this.table[i - c, j - c] != 0) 
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
                            if (this.table[i - c, j + c] == 0 || this.table[i - c, j + c] < 10)
                            { 
                                this.bits[i - c, j + c] = 2; 
                                if (this.table[i - c, j + c] < 10 && this.table[i - c, j + c] != 0) 
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
                            if (this.table[i + c, j + c] == 0 || this.table[i + c, j + c] < 10)
                            { 
                                this.bits[i + c, j + c] = 2;
                                if (this.table[i + c, j + c] < 10 && this.table[i + c, j + c] != 0) 
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
                            if (this.table[i + c, j - c] == 0 || this.table[i + c, j - c] < 10)
                            { 
                                this.bits[i + c, j - c] = 2;
                                if (this.table[i + c, j - c] < 10 && this.table[i + c, j - c] != 0)
                                    break; 
                            }
                            else
                                break;
                        }
                        else
                            break;
                    }
                    break;
                // Reina blanca.
                case 15:
                    // Hacia arriba izquierda.
                    for (c = 1; c < 8; c++)
                    {
                        if (i - c >= 0 && j - c >= 0)
                        {
                            if (this.table[i - c, j - c] == 0 || this.table[i - c, j - c] < 10)
                            {
                                this.bits[i - c, j - c] = 2;
                                if (this.table[i - c, j - c] < 10 && this.table[i - c, j - c] != 0)
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
                            if (this.table[i - c, j + c] == 0 || this.table[i - c, j + c] < 10)
                            { 
                                this.bits[i - c, j + c] = 2; 
                                if (this.table[i - c, j + c] < 10 && this.table[i - c, j + c] != 0) 
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
                            if (this.table[i + c, j + c] == 0 || this.table[i + c, j + c] < 10)
                            { 
                                this.bits[i + c, j + c] = 2; 
                                if (this.table[i + c, j + c] < 10 && this.table[i + c, j + c] != 0) 
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
                            if (this.table[i + c, j - c] == 0 || this.table[i + c, j - c] < 10)
                            { 
                                this.bits[i + c, j - c] = 2; 
                                if (this.table[i + c, j - c] < 10 && this.table[i + c, j - c] != 0)
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
                        if (this.table[c, j] == 0)
                            this.bits[c, j] = 2;
                        else
                        {
                            if (this.table[c, j] > 10)
                                break;
                            else
                            {
                                this.bits[c, j] = 2; 
                                break; 
                            }
                        }
                    }
                    // Hacia abajo.
                    for (c = i + 1; c < 8; c++)
                    {
                        if (this.table[c, j] == 0)
                            this.bits[c, j] = 2;
                        else
                        {
                            if (this.table[c, j] > 10)
                                break;
                            else
                            { 
                                this.bits[c, j] = 2; 
                                break; 
                            }
                        }
                    }
                    // Hacia la izquierda.
                    for (c = j - 1; c > -1; c--)
                    {
                        if (this.table[i, c] == 0)
                            this.bits[i, c] = 2;
                        else
                        {
                            if (this.table[i, c] > 10)
                                break;
                            else
                            { 
                                this.bits[i, c] = 2;
                                break; 
                            }
                        }
                    }
                    // Hacia la derecha.
                    for (c = j + 1; c < 8; c++)
                    {
                        if (this.table[i, c] == 0)
                            this.bits[i, c] = 2;
                        else
                        {
                            if (this.table[i, c] > 10)
                                break;
                            else
                            { 
                                this.bits[i, c] = 2; 
                                break; 
                            }
                        }
                    }
                    break;
                // Rey blanco.
                case 16:
                    // Hacia arriba.
                    if (i - 1 >= 0)
                    {
                        if (this.table[i - 1, j] == 0 || this.table[i - 1, j] < 10)
                            this.bits[i - 1, j] = 2;
                    }
                    // Hacia arriba derecha.
                    if (i - 1 >= 0 && j + 1 < 8)
                    {
                        if (this.table[i - 1, j + 1] == 0 || this.table[i - 1, j + 1] < 10)
                            this.bits[i - 1, j + 1] = 2;
                    }
                    // Hacia la derecha.
                    if (j + 1 < 8)
                    {
                        if (this.table[i, j + 1] == 0 || this.table[i, j + 1] < 10)
                            this.bits[i, j + 1] = 2;
                    }
                    // Hacia la derecha abajo.
                    if (i + 1 < 8 && j + 1 < 8)
                    {
                        if (this.table[i + 1, j + 1] == 0 || this.table[i + 1, j + 1] < 10)
                            this.bits[i + 1, j + 1] = 2;
                    }
                    // Hacia abajo.
                    if (i + 1 < 8)
                    {
                        if (this.table[i + 1, j] == 0 || this.table[i + 1, j] < 10)
                            this.bits[i + 1, j] = 2;
                    }
                    // Hacia abajo izquierda.
                    if (i + 1 < 8 && j - 1 >= 0)
                    {
                        if (this.table[i + 1, j - 1] == 0 || this.table[i + 1, j - 1] < 10)
                            this.bits[i + 1, j - 1] = 2;
                    }
                    // Hacia la izquierda.
                    if (j - 1 >= 0)
                    {
                        if (this.table[i, j - 1] == 0 || this.table[i, j - 1] < 10)
                            this.bits[i, j - 1] = 2;
                    }
                    // Hacia la arriba izquierda.
                    if (i - 1 >= 0 && j - 1 >= 0)
                    {
                        if (this.table[i - 1, j - 1] == 0 || this.table[i - 1, j - 1] < 10)
                            this.bits[i - 1, j - 1] = 2;
                    }
                    if (move_QW && move_RW)
                    {
                        if (this.table[7, 1] == 0 && this.table[7, 2] == 0 && this.table[7, 3] == 0)
                            this.bits[7, 2] = 2;
                        if (this.table[7, 5] == 0 && this.table[7, 6] == 0)
                            this.bits[7, 6] = 2;
                    }
                    break;
            }

            this.bits[i, j] = 3;
            dibujar();
            marcarJugadas();
        }

        /// <summary>
        /// Accion de cada pieza.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void click(object sender, EventArgs e)
        {
            int i = (sender as Pieza).i;
            int j = (sender as Pieza).j;

            switch (this.bits[i, j])
            {
                case 1:
                    validarJugada(this.table[i, j], i, j);
                    I = i;
                    J = j;
                    break;

                case 3:
                    validar();
                    break;

                case 2:                    
                    Array.Copy(this.table, this.jugadaAnterior, 64);
                    devuelto = false;
                    cambiarPiezas(i, j);
                    cambiarTurno();
                    setJugadorActual();
                    break;
            }
        }

        /// <summary>
        /// Busca una pieza por codigo.
        /// </summary>
        /// <param name="tablero">Tablero de juego.</param>
        /// <param name="codigo">Codigo de la pieza a buscar.</param>
        /// <returns>True si la encuentra, false sino la encuentra.</returns>
        private bool buscarPieza(int[,] tablero, int codigo)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (tablero[i, j] == codigo)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Se encarga de obtener la mejor jugada y de obtener la duración del proceso.
        /// </summary>
        public void buscarMejorJugada()
        {
            this.arbol = new Arbol(table);
            this.timer = Stopwatch.StartNew();
            this.arbol.arbolDeJugadas(arbol.raiz, turnoActual, 4);

            if (radioAlphaBeta.Checked)
            {
                this.alphaBeta = new AlphaBeta();
                this.nodo = this.alphaBeta.alphaBeta(arbol.raiz);
            }
            else if (radioMiniMax.Checked)
            {
                this.miniMax = new MiniMax();
                this.nodo = this.miniMax.miniMax(arbol.raiz);
            }
            else if (radioAlphaBetaSort.Checked)
            {
                this.alphaBetaSort = new AlphaBetaSort();
                this.nodo = this.alphaBetaSort.alphaBetaSort(arbol.raiz);
            }

            this.timer.Stop();

            labelDuracion.Text = timer.ElapsedMilliseconds.ToString();
            labelJugadas.Text = arbol.cantidadJugadas.ToString();
            labelPuntuacion.Text = arbol.raiz.utilidad.ToString();
        }

        /// <summary>
        /// Se encarga de realizar la mejor jugada y de pintarla en la UI.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mejorJugada(object sender, EventArgs e)
        {
            this.jugadaAnterior = this.table;
            devuelto = false;
            buscarMejorJugada();

            this.table = this.nodo.tablero;
            dibujar();
            // Validar si falta algún rey.
            cambiarTurno();
            setJugadorActual();

            finDeJuego();            
        }

        /// <summary>
        /// Devolver la ultima jugada hecha
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void devolverJugada(object sender, EventArgs e)
        {
            if (!devuelto) { 
                Array.Copy(this.jugadaAnterior, this.table, 64);                              
                cambiarTurno();
                setJugadorActual();
                dibujar();
                devuelto = true;
            }
        }

        /// <summary>
        /// Sugiere la mejor jugada al jugador.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ayuda(object sender, EventArgs e)
        {
            buscarMejorJugada();

            int i, j, length = 8;

            for (i = 0; i < length; i++) 
            {
                for (j = 0; j < length; j++)
                {
                    if (this.table[i, j] != this.nodo.tablero[i, j])
                    {
                        this.tablero[i, j].BackColor = System.Drawing.ColorTranslator.FromHtml("#729BB2");
                    }
                }
            }
        }
    }
}
