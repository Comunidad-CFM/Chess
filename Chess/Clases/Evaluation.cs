using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Clases
{
    class Evaluation
    {
        //public int peon = 100,
        //           caballo = 320,
        //           alfil = 325,
        //           torre = 500,
        //           reina = 975,
        //           rey = 32767;
        public double peon = 0.1,
                      caballo = 0.2,
                      alfil = 0.3,
                      torre = 0.4,
                      reina = 0.5,
                      rey = 0.6;
        public int peonValor = 1,
                   alfilValor = 3,
                   caballoValor = 3,
                   torreValor = 5,
                   reinaValor = 9;

        private readonly short[] PawnTable = new short[]
        {
            0,  0,  0,  0,  0,  0,  0,  0,
            50, 50, 50, 50, 50, 50, 50, 50,
            10, 10, 20, 30, 30, 20, 10, 10,
            5,  5, 10, 27, 27, 10,  5,  5,
            0,  0,  0, 25, 25,  0,  0,  0,
            5, -5,-10,  0,  0,-10, -5,  5,
            5, 10, 10,-25,-25, 10, 10,  5,
            0,  0,  0,  0,  0,  0,  0,  0
        };
        private readonly short[] KnightTable = new short[]
        {
            -50,-40,-30,-30,-30,-30,-40,-50,
            -40,-20,  0,  0,  0,  0,-20,-40,
            -30,  0, 10, 15, 15, 10,  0,-30,
            -30,  5, 15, 20, 20, 15,  5,-30,
            -30,  0, 15, 20, 20, 15,  0,-30,
            -30,  5, 10, 15, 15, 10,  5,-30,
            -40,-20,  0,  5,  5,  0,-20,-40,
            -50,-40,-20,-30,-30,-20,-40,-50
        };
        private readonly short[] BishopTable = new short[]
        {
            -20,-10,-10,-10,-10,-10,-10,-20,
            -10,  0,  0,  0,  0,  0,  0,-10,
            -10,  0,  5, 10, 10,  5,  0,-10,
            -10,  5,  5, 10, 10,  5,  5,-10,
            -10,  0, 10, 10, 10, 10,  0,-10,
            -10, 10, 10, 10, 10, 10, 10,-10,
            -10,  5,  0,  0,  0,  0,  5,-10,
            -20,-10,-40,-10,-10,-40,-10,-20
        };
        private readonly short[] KingTable = new short[]
        {
            -30, -40, -40, -50, -50, -40, -40, -30,
            -30, -40, -40, -50, -50, -40, -40, -30,
            -30, -40, -40, -50, -50, -40, -40, -30,
            -30, -40, -40, -50, -50, -40, -40, -30,
            -20, -30, -30, -40, -40, -30, -30, -20,
            -10, -20, -20, -20, -20, -20, -20, -10, 
            20,  20,   0,   0,   0,   0,  20,  20,
            20,  30,  10,   0,   0,  10,  30,  20
        };
        private readonly short[] KingTableEndGame = new short[]
        {
            -50,-40,-30,-20,-20,-30,-40,-50,
            -30,-20,-10,  0,  0,-10,-20,-30,
            -30,-10, 20, 30, 30, 20,-10,-30,
            -30,-10, 30, 40, 40, 30,-10,-30,
            -30,-10, 30, 40, 40, 30,-10,-30,
            -30,-10, 20, 30, 30, 20,-10,-30,
            -30,-30,  0,  0,  0,  0,-30,-30,
            -50,-30,-30,-30,-30,-30,-30,-50
        };

        public Evaluation()
        { }

        #region Heuristica por valor de la pieza.
        public double valorPosicion(int[,] tablero) 
        {
            int i,
                j,
                length = 8,
                codigo,
                peonesBlancos = 0,
                peonesNegros = 0,
                torresBlancas = 0,
                torresNegras = 0,
                caballosBlancos = 0,
                caballosNegros = 0,
                alfilesBlancos = 0,
                alfilesNegros = 0,
                reinaBlanca = 0,
                reinaNegra = 0;

            for (i = 0; i < length; i++)
            {
                for (j = 0; j < length; j++)
                {
                    codigo = tablero[i,j]; 
                    switch (codigo)
                    {
                        // Peones.
                        case 1:
                            peonesNegros++;
                            break;
                        case 11:
                            peonesBlancos++;
                            break;
                        // Torres.
                        case 2:
                            torresNegras++;
                            break;
                        case 12:
                            torresBlancas++;
                            break;
                        // Caballos.
                        case 3: 
                            caballosNegros++;
                            break;
                        case 13:
                            caballosBlancos++;
                            break;
                        // Alfiles.
                        case 4: 
                            alfilesNegros++;
                            break;
                        case 14:
                            alfilesBlancos++;
                            break;
                        // Reina.
                        case 5: 
                            reinaNegra++;
                            break;
                        case 15:
                            reinaBlanca++;
                            break;
                    }
                }
            }

            return (peonesBlancos - peonesNegros) + (3 * (caballosBlancos - caballosNegros)) + (((alfilesBlancos == 2 ? 3.25 : 3) * alfilesBlancos) - ((alfilesNegros == 2 ? 3.25 : 3) * alfilesNegros)) + (5 * (torresBlancas - torresNegras)) + (9 * (reinaBlanca - reinaNegra));
        }
        #endregion

        #region Heuristica por valor de la posicion.
        public double valorPieza(int codigo) 
        {
            double valor = 0;
            switch (codigo) 
            {
                // Peones.
                case 1: case 11:
                    valor = peon;
                    break;
                // Torres.
                case 2: case 12:
                    valor = torre;
                    break;
                // Caballos.
                case 3: case 13:
                    valor = caballo;
                    break;
                // Alfiles.
                case 4: case 14:
                    valor = alfil;
                    break;
                // Reina.
                case 5: case 15:
                    valor = reina;
                    break;
                // Rey.
                case 6: case 16:
                    valor = rey;
                    break;
            }

            return valor;
        }

        public int isEndGamePhase(int[,] tablero)
        {
            int i,
                j,
                length = 8,
                count = 0;

            for (i = 0; i < length; i++)
            {
                for (j = 0; j < length; j++)
                {
                    if (tablero[i, j] != 0)
                        count++;
                }
            }

            return count;
        }

        public double EvaluatePieceScore(Cuadro cuadro, bool endGamePhase, ref byte bishopCount, ref bool insufficientMaterial)
        {
            double score = 0;
            byte index = (byte)(cuadro.i * 8 + cuadro.j);

            // If whites.
            if (cuadro.codigo > 10)
            {
                index = (byte)(63 - (cuadro.i * 8 + cuadro.j));

            }
            score += valorPieza(cuadro.codigo);

            switch (cuadro.codigo)
            {
                // Peones.
                case 1: case 11:
                    if (index % 8 == 0 || index % 8 == 7)
                    {
                        //Rook Pawns are worth 15% less because they can only attack one way
                        score -= 15;
                    }

                    score += PawnTable[index];
                    break;
                // Torres.
                case 2: case 12:
                    
                    break;
                // Caballos.
                case 3: case 13:
                    score += KnightTable[index];
                    //In the end game remove a few points for Knights since they are worth less
                    if (endGamePhase)
                    {
                        score -= 10;
                    }
                    break;
                // Alfiles.
                case 4: case 14:
                    bishopCount++;
                    if (bishopCount >= 2)
                    {
                        //2 Bishops receive a bonus
                        score += 10;
                    }

                    //In the end game Bishops are worth more
                    if (endGamePhase)
                    {
                        score += 10;
                    }

                    score += BishopTable[index];
                    break;
                // Reina.
                case 5: case 15:
                    if (!endGamePhase)
                    {
                        score -= 10;
                    }
                    break;
                // Rey.
                case 6: case 16:
                    if (endGamePhase)
                    {
                        score += KingTableEndGame[index];
                    }
                    else
                    {
                        score += KingTable[index];
                    }
                    break;
            }

            return score;
        }

        public double EvaluateBoardScore(int[,] tablero) 
        {
            double utilidad = 0;
            bool insufficientMaterial = true,
                endGamephase = (isEndGamePhase(tablero) <= 10 ? true : false);

            byte blackBishopCount = 0;
            byte whiteBishopCount = 0;
            byte knightCount = 0;

            int remainingPieces = 0,
                i,
                j,
                length = 8;

            for (i = 0; i < length; i++)
            {
                for (j = 0; j < length; j++)
                {
                    Cuadro cuadro = new Cuadro(i, j, tablero[i, j]);
                    if (cuadro.codigo == 0)
                        continue;

                    //Calculate Remaining Material for end game determination
                    remainingPieces++;

                    if (cuadro.codigo > 10) // Blancas.
                    {
                        utilidad += EvaluatePieceScore(cuadro, endGamephase, ref whiteBishopCount, ref insufficientMaterial);
                    }
                    else if (cuadro.codigo < 10 && cuadro.codigo != 0) // Negras
                    {
                        utilidad -= EvaluatePieceScore(cuadro, endGamephase, ref whiteBishopCount, ref insufficientMaterial);
                    }

                    if (cuadro.codigo == 6 || cuadro.codigo == 16)
                    {
                        knightCount++;

                        if (knightCount > 1)
                        {
                            insufficientMaterial = false;
                        }
                    }

                    if ((blackBishopCount + whiteBishopCount) > 1)
                    {
                        insufficientMaterial = false;
                    }
                }    
            }

            return utilidad;
        }
        #endregion

        #region Heuristica por cantidad de casillas movibles.
        public int jugadasBlancas(int[,] tablero, int i, int j)
        {
            int jugadas = 0, c;
            switch (tablero[i, j])
            {
                // Torre.
                case 12:
                    // Hacia arriba.
                    for (c = i - 1; c > -1; c--)
                    {
                        if (tablero[c, j] == 0)
                            jugadas++;
                        else
                        {
                            if (tablero[c, j] > 10)
                                break;
                            else
                            {
                                jugadas++;
                                break;
                            }
                        }
                    }
                    // Hacia abajo.
                    for (c = i + 1; c < 8; c++)
                    {
                        if (tablero[c, j] == 0)
                            jugadas++;
                        else
                        {
                            if (tablero[c, j] > 10)
                                break;
                            else
                            {
                                jugadas++;
                                break;
                            }
                        }
                    }
                    // Hacia la izquierda.
                    for (c = j - 1; c > -1; c--)
                    {
                        if (tablero[i, c] == 0)
                            jugadas++;
                        else
                        {
                            if (tablero[i, c] > 10)
                                break;
                            else
                            {
                                jugadas++;
                                break;
                            }
                        }
                    }
                    // Hacia la derecha.
                    for (c = j + 1; c < 8; c++)
                    {
                        if (tablero[i, c] == 0)
                            jugadas++;
                        else
                        {
                            if (tablero[i, c] > 10)
                                break;
                            else
                            {
                                jugadas++;
                                break;
                            }
                        }
                    }
                    break;
                // Caballo.
                case 13:
                    // Hacia arriba.
                    if (i - 2 >= 0)
                    {
                        // Izquierda.
                        if (j - 1 >= 0)
                        {
                            if (tablero[i - 2, j - 1] < 10)
                                jugadas++;
                        }
                        // Derecha.
                        if (j + 1 < 8)
                        {
                            if (tablero[i - 2, j + 1] < 10)
                                jugadas++;
                        }
                    }
                    // Hacia abajo.
                    if (i + 2 < 8)
                    {
                        // Izquierda.
                        if (j - 1 >= 0)
                        {
                            if (tablero[i + 2, j - 1] < 10)
                                jugadas++;
                        }
                        // Derecha.
                        if (j + 1 < 8)
                        {
                            if (tablero[i + 2, j + 1] < 10)
                                jugadas++;
                        }
                    }
                    // Hacia la izquierda.
                    if (j - 2 >= 0)
                    {
                        // Arriba.
                        if (i - 1 >= 0)
                        {
                            if (tablero[i - 1, j - 2] < 10)
                                jugadas++;
                        }
                        // Derecha.
                        if (i + 1 < 8)
                        {
                            if (tablero[i + 1, j - 2] < 10)
                                jugadas++;
                        }
                    }
                    // Hacia la derecha.
                    if (j + 2 < 8)
                    {
                        // Arriba.
                        if (i - 1 >= 0)
                        {
                            if (tablero[i - 1, j + 2] < 10)
                                jugadas++;
                        }
                        // Abajo.
                        if (i + 1 < 8)
                        {
                            if (tablero[i + 1, j + 2] < 10)
                                jugadas++;
                        }
                    }
                    break;
                // Alfil.
                case 14:
                    // Hacia arriba izquierda.
                    for (c = 1; c < 8; c++)
                    {
                        if (i - c >= 0 && j - c >= 0)
                        {
                            if (tablero[i - c, j - c] == 0 || tablero[i - c, j - c] < 10)
                            {
                                jugadas++;
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
                                jugadas++;
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
                                jugadas++;
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
                                jugadas++;
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
                // Reina.
                case 15:
                    // Hacia arriba izquierda.
                    for (c = 1; c < 8; c++)
                    {
                        if (i - c >= 0 && j - c >= 0)
                        {
                            if (tablero[i - c, j - c] == 0 || tablero[i - c, j - c] < 10)
                            {
                                jugadas++;
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
                                jugadas++;
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
                                jugadas++;
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
                                jugadas++;
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
                            jugadas++;
                        else
                        {
                            if (tablero[c, j] > 10)
                                break;
                            else
                            {
                                jugadas++;
                                break;
                            }
                        }
                    }
                    // Hacia abajo.
                    for (c = i + 1; c < 8; c++)
                    {
                        if (tablero[c, j] == 0)
                            jugadas++;
                        else
                        {
                            if (tablero[c, j] > 10)
                                break;
                            else
                            {
                                jugadas++;
                                break;
                            }
                        }
                    }
                    // Hacia la izquierda.
                    for (c = j - 1; c > -1; c--)
                    {
                        if (tablero[i, c] == 0)
                            jugadas++;
                        else
                        {
                            if (tablero[i, c] > 10)
                                break;
                            else
                            {
                                jugadas++;
                                break;
                            }
                        }
                    }
                    // Hacia la derecha.
                    for (c = j + 1; c < 8; c++)
                    {
                        if (tablero[i, c] == 0)
                            jugadas++;
                        else
                        {
                            if (tablero[i, c] > 10)
                                break;
                            else
                            {
                                jugadas++;
                                break;
                            }
                        }
                    }
                    break;
                // Rey.
                case 16:
                    // Hacia arriba.
                    if (i - 1 >= 0)
                    {
                        if (tablero[i - 1, j] == 0 || tablero[i - 1, j] < 10)
                            jugadas++;
                    }
                    // Hacia arriba derecha.
                    if (i - 1 >= 0 && j + 1 < 8)
                    {
                        if (tablero[i - 1, j + 1] == 0 || tablero[i - 1, j + 1] < 10)
                            jugadas++;
                    }
                    // Hacia la derecha.
                    if (j + 1 < 8)
                    {
                        if (tablero[i, j + 1] == 0 || tablero[i, j + 1] < 10)
                            jugadas++;
                    }
                    // Hacia la derecha abajo.
                    if (i + 1 < 8 && j + 1 < 8)
                    {
                        if (tablero[i + 1, j + 1] == 0 || tablero[i + 1, j + 1] < 10)
                            jugadas++;
                    }
                    // Hacia abajo.
                    if (i + 1 < 8)
                    {
                        if (tablero[i + 1, j] == 0 || tablero[i + 1, j] < 10)
                            jugadas++;
                    }
                    // Hacia abajo izquierda.
                    if (i + 1 < 8 && j - 1 >= 0)
                    {
                        if (tablero[i + 1, j - 1] == 0 || tablero[i + 1, j - 1] < 10)
                            jugadas++;
                    }
                    // Hacia la izquierda.
                    if (j - 1 >= 0)
                    {
                        if (tablero[i, j - 1] == 0 || tablero[i, j - 1] < 10)
                            jugadas++;
                    }
                    // Hacia la arriba izquierda.
                    if (i - 1 >= 0 && j - 1 >= 0)
                    {
                        if (tablero[i - 1, j - 1] == 0 || tablero[i - 1, j - 1] < 10)
                            jugadas++;
                    }
                    break;
            }

            return jugadas;
        }

        public int jugadasNegras(int[,] tablero, int i, int j)
        {
            int jugadas = 0, c;
            switch (tablero[i, j])
            {
                // Torre.
                case 2:
                    // Hacia arriba.
                    for (c = i - 1; c > -1; c--)
                    {
                        if (tablero[c, j] == 0)
                            jugadas++;
                        else
                        {
                            if (tablero[c, j] < 10)
                                break;
                            else
                            {
                                jugadas++;
                                break;
                            }
                        }
                    }
                    // Hacia abajo.
                    for (c = i + 1; c < 8; c++)
                    {
                        if (tablero[c, j] == 0)
                            jugadas++;
                        else
                        {
                            if (tablero[c, j] < 10)
                                break;
                            else
                            {
                                jugadas++;
                                break;
                            }
                        }
                    }
                    // Hacia la izquierda.
                    for (c = j - 1; c > -1; c--)
                    {
                        if (tablero[i, c] == 0)
                            jugadas++;
                        else
                        {
                            if (tablero[i, c] < 10)
                                break;
                            else
                            {
                                jugadas++;
                                break;
                            }
                        }
                    }
                    // Hacia la derecha.
                    for (c = j + 1; c < 8; c++)
                    {
                        if (tablero[i, c] == 0)
                            jugadas++;
                        else
                        {
                            if (tablero[i, c] < 10)
                                break;
                            else
                            {
                                jugadas++;
                                break;
                            }
                        }
                    }
                    break;
                // Caballo.
                case 3:
                    // Hacia arriba.
                    if (i - 2 >= 0)
                    {
                        // Hacia la izquierda.
                        if (j - 1 >= 0)
                        {
                            if (tablero[i - 2, j - 1] == 0 || tablero[i - 2, j - 1] > 10)
                                jugadas++;
                        }
                        // Hacia la derecha.
                        if (j + 1 < 8)
                        {
                            if (tablero[i - 2, j + 1] == 0 || tablero[i - 2, j + 1] > 10)
                                jugadas++;
                        }
                    }
                    // Hacia abajo.
                    if (i + 2 < 8)
                    {
                        // Hacia la izquierda.
                        if (j - 1 >= 0)
                        {
                            if (tablero[i + 2, j - 1] == 0 || tablero[i + 2, j - 1] > 10)
                                jugadas++;
                        }
                        // Hacia la derecha.
                        if (j + 1 < 8)
                        {
                            if (tablero[i + 2, j + 1] == 0 || tablero[i + 2, j + 1] > 10)
                                jugadas++;
                        }
                    }
                    // Hacia la izquierda.
                    if (j - 2 >= 0)
                    {
                        // Hacia arriba.
                        if (i - 1 >= 0)
                        {
                            if (tablero[i - 1, j - 2] == 0 || tablero[i - 1, j - 2] > 10)
                                jugadas++;
                        }
                        // Hacia abajo.
                        if (i + 1 < 8)
                        {
                            if (tablero[i + 1, j - 2] == 0 || tablero[i + 1, j - 2] > 10)
                                jugadas++;
                        }
                    }
                    // Hacia la derecha.
                    if (j + 2 < 8)
                    {
                        // Hacia arriba.
                        if (i - 1 >= 0)
                        {
                            if (tablero[i - 1, j + 2] == 0 || tablero[i - 1, j + 2] > 10)
                                jugadas++;
                        }
                        // Hacia abajo.
                        if (i + 1 < 8)
                        {
                            if (tablero[i + 1, j + 2] == 0 || tablero[i + 1, j + 2] > 10)
                                jugadas++;
                        }
                    }
                    break;
                // Alfil.
                case 4:
                    // Arriba izquierda.
                    for (c = 1; c < 8; c++)
                    {
                        if (i - c >= 0 && j - c >= 0)
                        {
                            if (tablero[i - c, j - c] == 0 || tablero[i - c, j - c] > 10)
                            {
                                jugadas++;
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
                                jugadas++;
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
                                jugadas++;
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
                                jugadas++;
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
                // Reina.
                case 5:
                    // Hacia la izquierda.
                    for (c = 1; c < 8; c++)
                    {
                        if (j - c >= 0)
                        {
                            if (tablero[i, j - c] == 0 || tablero[i, j - c] > 10)
                            {
                                jugadas++;
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
                                jugadas++;
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
                                jugadas++;
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
                                jugadas++;
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
                                jugadas++;
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
                                jugadas++;
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
                                jugadas++;
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
                                jugadas++;
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
                // Rey.
                case 6:
                    // Hacia arriba.
                    if (i - 1 >= 0)
                    {
                        if (tablero[i - 1, j] == 0 || tablero[i - 1, j] > 10)
                            jugadas++;
                    }
                    // Hacia arriba derecha
                    if (i - 1 >= 0 && j + 1 < 8)
                    {
                        if (tablero[i - 1, j + 1] == 0 || tablero[i - 1, j + 1] > 10)
                            jugadas++;
                    }
                    // Hacia la derecha.
                    if (j + 1 < 8)
                    {
                        if (tablero[i, j + 1] == 0 || tablero[i, j + 1] > 10)
                            jugadas++;
                    }
                    // Hacia abajo derecha.
                    if (i + 1 < 8 && j + 1 < 8)
                    {
                        if (tablero[i + 1, j + 1] == 0 || tablero[i + 1, j + 1] > 10)
                            jugadas++;
                    }
                    // Hacia abajo.
                    if (i + 1 < 8)
                    {
                        if (tablero[i + 1, j] == 0 || tablero[i + 1, j] > 10)
                            jugadas++;
                    }
                    // Hacia abajo izquierda.
                    if (i + 1 < 8 && j - 1 >= 0)
                    {
                        if (tablero[i + 1, j - 1] == 0 || tablero[i + 1, j - 1] > 10)
                            jugadas++;
                    }
                    // Hacia la izquierda.
                    if (j - 1 >= 0)
                    {
                        if (tablero[i, j - 1] == 0 || tablero[i, j - 1] > 10)
                            jugadas++;
                    }
                    // Hacia la izquierda arriba.
                    if (i - 1 >= 0 && j - 1 >= 0)
                    {
                        if (tablero[i - 1, j - 1] == 0 || tablero[i - 1, j - 1] > 10)
                            jugadas++;
                    }
                    break;
            }

            return jugadas;
        }

        public double movimientos(int[,] tablero)
        {
            int i, j, length = 8, cantJugadasBlancas = 0, cantJugadasNegras = 0;

            for (i = 0; i < length; i++)
            {
                for (j = 0; j < length; j++)
                {
                    if (tablero[i, j] < 10 && tablero[i, j] != 0) // Negras.
                    {
                        cantJugadasNegras += jugadasNegras(tablero, i, j);
                    }
                    else if (tablero[i, j] > 10) // Blancas.
                    {
                        cantJugadasBlancas += jugadasBlancas(tablero, i, j);
                    }
                }
            }

            return (cantJugadasBlancas * 0.1) - (cantJugadasNegras * 0.1);
        }
#endregion

        #region Heuristica de defensa del rey.
        public int[] buscarPieza(int[,] tablero, int codigo)
        {
            int[] posicion = new int[2];
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (tablero[i, j] == codigo)
                    {
                        posicion[0] = i;
                        posicion[1] = j;
                        return posicion;
                    }
                }
            }
            return posicion;
        }
        public double defensa(int[,] tablero, int i, int j)
        {
            double puntos = 0;
            switch (tablero[i, j])
            {
                //negro
                case 6:
                    //izquierdo
                    if (j - 1 >= 0)
                    {
                        if ((tablero[i, j - 1] < 10 || tablero[i, j - 1] != 0) || tablero[i, j - 1] == 11)
                        {
                            puntos += 0.2;
                        }
                        else
                        {
                            puntos -= 0.1;
                        }
                    }
                    //abajo izquierda
                    if (i + 1 < 8 && j - 1 >= 0)
                    {
                        if (tablero[i + 1, j - 1] < 10 || tablero[i + 1, j - 1] != 0)
                        {
                            puntos += 0.1;
                        }
                        else
                        {
                            puntos -= 0.2;
                        }
                    }
                    //arriba
                    if (i + 1 < 8)
                    {
                        if ((tablero[i + 1, j] < 10 && tablero[i + 1, j] != 0) || tablero[i + 1, j] == 11)
                        {
                            puntos += 0.1;
                        }
                        else
                        {
                            //arriba 2
                            if (i + 2 < 8)
                            {
                                if ((tablero[i + 2, j] < 10 && tablero[i + 2, j] != 0) || tablero[i + 2, j] == 11)
                                {
                                    puntos += 0.1;
                                }
                                else
                                {
                                    puntos -= 0.2;
                                }
                            }
                        }
                    }
                    //arriba derecha
                    if (i + 1 < 8 && j + 1 < 8)
                    {
                        if (tablero[i + 1, j + 1] < 10 || tablero[i + 1, j + 1] != 0)
                        {
                            puntos += 0.1;
                        }
                        else
                        {
                            puntos -= 0.2;
                        }
                    }
                    //derecha
                    if (j + 1 < 8)
                    {
                        if (tablero[i, j + 1] < 10 || tablero[i, j + 1] != 0)
                        {
                            puntos += 0.2;
                        }
                        else
                        {
                            puntos -= 0.1;
                        }
                    }
                    break;
                //blanco
                case 16:
                    //izquierdo
                    if (j - 1 >= 0)
                    {
                        if (tablero[i, j - 1] > 10 || tablero[i, j - 1] == 01)
                        {
                            puntos += 0.2;
                        }
                        else
                        {
                            puntos -= 0.1;
                        }
                    }
                    //arriba izquierda
                    if (i - 1 >= 0 && j - 1 >= 0)
                    {
                        if (tablero[i - 1, j - 1] > 10)
                        {
                            puntos += 0.1;
                        }
                        else
                        {
                            puntos -= 0.2;
                        }
                    }
                    //arriba
                    if (i - 1 >= 0)
                    {
                        if (tablero[i - 1, j] > 10 || tablero[i - 1, j] == 01)
                        {
                            puntos += 0.1;
                        }
                        else
                        {
                            //arriba 2
                            if (i - 2 >= 0)
                            {
                                if (tablero[i - 2, j] > 10 || tablero[i - 2, j] == 01)
                                {
                                    puntos += 0.1;
                                }
                                else
                                {
                                    puntos -= 0.2;
                                }
                            }
                        }
                    }
                    //arriba derecha
                    if (i - 1 >= 0 && j + 1 < 8)
                    {
                        if (tablero[i - 1, j + 1] > 10)
                        {
                            puntos += 0.1;
                        }
                        else
                        {
                            puntos -= 0.2;
                        }
                    }
                    //derecha
                    if (j + 1 < 8)
                    {
                        if (tablero[i, j + 1] > 10 || tablero[i, j + 1] == 01)
                        {
                            puntos += 0.2;
                        }
                        else
                        {
                            puntos -= 0.1;
                        }
                    }
                    break;
            }

            return puntos;
        }
        #endregion

        public double utilidad(int[,] tablero, int jugador) 
        {
            int[] posicion = buscarPieza(tablero, (jugador == 1 ? 16 : 6));
            return valorPosicion(tablero) + EvaluateBoardScore(tablero) + movimientos(tablero) + defensa(tablero, posicion[0], posicion[1]);
        }
    }
}
