using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Clases
{
    class Evaluacion
    {
        Arbol arbol;
        public readonly int peon;
        public readonly int caballo;
        public readonly int alfil;
        public readonly int torre;
        public readonly int reina;
        public readonly int rey;
        public readonly int[,] tablaPeon;
        public readonly int[,] tablaCaballo;
        public readonly int[,] tablaAlfil;
        public readonly int[,] tablaTorre;
        public readonly int[,] tablaReina;
        public readonly int[,] tablaRey;
        public readonly int[,] KingTableEndGame;

        public Evaluacion()
        {
            this.peon = 100;
            this.caballo = 320;
            this.alfil = 330;
            this.torre = 500;
            this.reina = 900;
            this.rey = 20000;
            this.tablaPeon = new int[,]
            {
                { 0, 0,  0,  0,  0,  0, 0, 0},
                {50,50, 50, 50, 50, 50,50,50},
                {10,10, 20, 30, 40, 20,10,10},
                { 5, 5, 10, 25, 25, 10, 5, 5},
                { 0, 0,  0, 20, 20,  0, 0, 0},
                { 5,-5,-10,  0,  0,-10,-5, 5},
                { 5,10, 10,-20,-20, 10,10, 5},
                { 0, 0,  0,  0,  0,  0, 0, 0}
            };
            this.tablaCaballo = new int[,]
            {
                {-50,-40,-30,-30,-30,-30,-40,-50},
                {-40,-20,  0,  0,  0,  0,-20,-40},
                {-30,  0, 10, 15, 15, 10,  0,-30},
                {-30,  5, 15, 20, 20, 15,  5,-30},
                {-30,  0, 15, 20, 20, 15,  0,-30},
                {-30,  5, 10, 15, 15, 10,  5,-30},
                {-40,-20,  0,  5,  5,  0,-20,-40},
                {-50,-40,-20,-30,-30,-20,-40,-50}
            };
            this.tablaAlfil = new int[,]
            {
                {-20,-10,-10,-10,-10,-10,-10,-20},
                {-10,  0,  0,  0,  0,  0,  0,-10},
                {-10,  0,  5, 10, 10,  5,  0,-10},
                {-10,  5,  5, 10, 10,  5,  5,-10},
                {-10,  0, 10, 10, 10, 10,  0,-10},
                {-10, 10, 10, 10, 10, 10, 10,-10},
                {-10,  5,  0,  0,  0,  0,  5,-10},
                {-20,-10,-40,-10,-10,-40,-10,-20}
            };
            this.tablaTorre = new int[,]
            {
                { 0, 0, 0, 0, 0, 0, 0, 0},
                { 5,10,10,10,10,10,10, 5},
                {-5, 0, 0, 0, 0, 0, 0,-5},
                {-5, 0, 0, 0, 0, 0, 0,-5},
                {-5, 0, 0, 0, 0, 0, 0,-5},
                {-5, 0, 0, 0, 0, 0, 0,-5},
                {-5, 0, 0, 0, 0, 0, 0,-5},
                { 0, 0, 0, 5, 5, 0, 0, 0}
            };
            this.tablaReina = new int[,]
            {
                {-20,-10,-10, -5, -5,-10,-10,-20},
                {-10,  0,  0,  0,  0,  0,  0,-10},
                {-10,  0,  5,  5,  5,  5,  0,-10},
                { -5,  0,  5,  5,  5,  5,  0, -5},
                {  0,  0,  5,  5,  5,  5,  0, -5},
                {-10,  5,  5,  5,  5,  5,  0,-10},
                {-10,  0,  5,  0,  0,  0,  0,-10},
                {-20,-10,-10, -5, -5,-10,-10,-20}
            };
            this.tablaRey = new int[,]
            {
                {-30,-40,-40,-50,-50,-40,-40,-30},
                {-30,-40,-40,-50,-50,-40,-40,-30},
                {-30,-40,-40,-50,-50,-40,-40,-30},
                {-30,-40,-40,-50,-50,-40,-40,-30},
                {-20,-30,-30,-40,-40,-30,-30,-20},
                {-10,-20,-20,-20,-20,-20,-20,-10}, 
                { 20, 20,  0,  0,  0,  0, 20, 20},
                { 20, 30, 10,  0,  0, 10, 30, 20}
            };
            this.KingTableEndGame = new int[,]
            {
                {-50,-40,-30,-20,-20,-30,-40,-50},
                {-30,-20,-10,  0,  0,-10,-20,-30},
                {-30,-10, 20, 30, 30, 20,-10,-30},
                {-30,-10, 30, 40, 40, 30,-10,-30},
                {-30,-10, 30, 40, 40, 30,-10,-30},
                {-30,-10, 20, 30, 30, 20,-10,-30},
                {-30,-30,  0,  0,  0,  0,-30,-30},
                {-50,-30,-30,-30,-30,-30,-30,-50}
            };
        }

        public int obtenerValor(Cuadro cuadro) 
        {
            int valor = 0;
            switch (cuadro.codigo)
            {
                // Peones.
                case 1: case 11:
                    valor = this.peon + this.tablaPeon[cuadro.i, cuadro.j];
                    break;
                // Torres.
                case 2: case 12:
                    valor = this.torre + this.tablaTorre[cuadro.i, cuadro.j];
                    break;
                // Caballos.
                case 3: case 13:
                    valor = this.caballo + this.tablaCaballo[cuadro.i, cuadro.j];
                    break;
                // Alfiles.
                case 4: case 14:
                    valor = this.alfil + this.tablaAlfil[cuadro.i, cuadro.j];
                    break;
                // Reina.
                case 5: case 15:
                    valor = this.reina + this.tablaReina[cuadro.i, cuadro.j];
                    break;
                // Reina.
                case 6: case 16:
                    valor = this.rey + this.tablaRey[cuadro.i, cuadro.j];
                    break;
            }
            return valor;
        }

        public List<Cuadro> obtenerCuadros(int[,] tablero)
        {
            List<Cuadro> cuadros = new List<Cuadro>();
            int i, j, length = 8;

            for (i = 0; i < length; i++)
            {
                for (j = 0; j < length; j++)
                {
                    if (tablero[i, j] != 0)
                    {
                        cuadros.Add(new Cuadro(i, j, tablero[i, j]));
                    }
                }
            }

            return cuadros;
        }

        public int obtener(int[,] tablero)
        {
            int utilidad = 0,
                index = 0;
            List<Cuadro> cuadros = obtenerCuadros(tablero);

            foreach (Cuadro cuadro in cuadros)
            {
                if (cuadro.codigo > 10) // Blancas.
                {
                    // Se obtiene la posición como si fuese una lista y se invierte.
                    cuadro.i = 7 - cuadro.i;
                }

                utilidad += obtenerValor(cuadro);
            }

            return utilidad;
        }
    }
}
