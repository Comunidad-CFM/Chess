using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Clases
{
    class Utilidad
    {
        public Utilidad()
        { }

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
        public double puntajeTotal(List<Cuadro> cuadros)
        {
            double puntaje = 0;

            foreach (Cuadro cuadro in cuadros)
            {
                switch (cuadro.codigo)
                {
                    // Peones.
                    case 1: case 11:
                        puntaje += 1;
                        break;
                    // Torres.
                    case 2: case 12:
                        puntaje += 5;
                        break;
                    // Caballos.
                    case 3: case 13:
                        puntaje += 3;
                        break;
                    // Alfiles.
                    case 4: case 14:
                        puntaje += 3;
                        break;
                    // Reina.
                    case 5: case 15:
                        puntaje += 9;
                        break;
                }
            }

            return puntaje;
        }

        public double[] calcularPuntaje(List<Cuadro> piezasJugador1, List<Cuadro> piezasJugador2) 
        {
            return new double[] { puntajeTotal(piezasJugador1), puntajeTotal(piezasJugador2) };          
        }

        public double obtener(int[,] tablero, int jugador)
        {
            double[] puntajes = calcularPuntaje(obtenerCuadros(tablero, jugador), obtenerCuadros(tablero, jugador % 2 + 1));

            return (puntajes[0] - puntajes[1]) / (puntajes[0] + puntajes[1]);
        }
    }
}
