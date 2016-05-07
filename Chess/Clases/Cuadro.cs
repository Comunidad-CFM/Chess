using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Clases
{
    class Cuadro
    {
        public int i;
        public int j;
        public int codigo;

        public Cuadro(int i, int j, int codigo)
        {
            this.i = i;
            this.j = j;
            this.codigo = codigo;
        }
    }
}
