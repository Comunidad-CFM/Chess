using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Clases
{
    class Nodo
    {
        public int utilidad;
        public List<Nodo> hijos;
        public int[,] tablero;

        public Nodo(int[,] tablero)
        {
            this.utilidad = -1;
            this.hijos = new List<Nodo>();
            this.tablero = tablero;


        }
    }
}
