using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Clases
{
    class Evaluation
    {
        public int peon = 100,
                   caballo = 320,
                   alfil = 325,
                   torre = 500,
                   reina = 975,
                   rey = 32767;
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

        public int utilidad(int[,] tablero, int jugador) 
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

            if (jugador == 1) // Blancas.
            {
                return (peonesBlancos - peonesNegros) + (3 * (caballosBlancos - caballosNegros)) + (3 * (alfilesBlancos - alfilesNegros)) + (5 * (torresBlancas - torresNegras)) + (9 * (reinaBlanca - reinaNegra));
            }
            else // Negras.
            {
                return (peonesNegros - peonesBlancos) + (3 * (caballosNegros - caballosBlancos)) + (3 * (alfilesNegros - alfilesBlancos)) + (5 * (torresNegras - torresBlancas)) + (9 * (reinaNegra - reinaBlanca));            
            }
        }
 
        public int valorPieza(int codigo) 
        {
            int valor = 0;
            switch (codigo) 
            {
                // Peones.
                case 1: case 11:
                    valor = this.peon;
                    break;
                // Torres.
                case 2: case 12:
                    valor = this.torre;
                    break;
                // Caballos.
                case 3: case 13:
                    valor = this.caballo;
                    break;
                // Alfiles.
                case 4: case 14:
                    valor = this.alfil;
                    break;
                // Reina.
                case 5: case 15:
                    valor = this.reina;
                    break;
                // Rey.
                case 6: case 16:
                    valor = this.rey;
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

        public int EvaluatePieceScore(Cuadro cuadro, bool endGamePhase, ref byte bishopCount, ref bool insufficientMaterial)
        {
            int score = 0;
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

        public int EvaluateBoardScore(int[,] tablero) 
        {
            int utilidad = 0;
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
                    //Square square = board.Squares[x];
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
    }
}
