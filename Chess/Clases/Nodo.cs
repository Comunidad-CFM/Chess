using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Clases
{
    class Nodo
    {
        public double utilidad;
        public List<Nodo> hijos;
        public int[,] tablero;

        public Nodo(int[,] tablero, double utilidad)
        {
            this.utilidad = utilidad;
            this.hijos = new List<Nodo>();
            this.tablero = tablero;
        }
    }
}
