using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Clases
{
    // Algoritmo alpha-beta
    // Busqueda por niveles(max, min), en un arbol de la mejor hoja segun la utilidad. Tambien utiliza poda en la busqueda
    class AlphaBeta
    {
        public AlphaBeta()
        { }

        // Busqueda del nivel Max
        public double alphaMax(Nodo raiz, double alpha, double beta)
        {
            if (raiz.hijos.Count == 0)
            {
                return raiz.utilidad;
            }

            double alphaAux = 0;

            foreach (Nodo hijo in raiz.hijos)
            {
                alphaAux = betaMin(hijo, alpha, beta);
                if (alphaAux >= beta)
                    return beta;
                if (alphaAux > alpha)
                    alpha = alphaAux;
            }
            return alpha;
        }

        // Busqueda del nivel Min
        public double betaMin(Nodo raiz, double alpha, double beta)
        {
            if (raiz.hijos.Count == 0)
            {
                return raiz.utilidad;
            }

            double betaAux = 0;

            foreach (Nodo hijo in raiz.hijos)
            {
                betaAux = alphaMax(hijo, alpha, beta);
                if (betaAux <= alpha)
                    return alpha;
                if (betaAux < beta)
                    beta = betaAux;
            }
            return beta;
        }

        // Reduce el numero de nodos evaluados cuando se encuentra con una peores posibilidades que las previamente evaluadas
        // Se obtiene la utilidad de cada hijo de la raiz
        public Nodo alphaBeta(Nodo raiz)
        {
            double utilidad = 0;

            Parallel.ForEach(raiz.hijos, nodo =>
            {
                lock (nodo)
                {
                    nodo.utilidad = alphaMax(nodo, -1000000, 1000000);
                }

            });

            foreach (Nodo nodo in raiz.hijos)
            {
                utilidad = nodo.utilidad;

                if (raiz.utilidad <= utilidad)
                {
                    raiz.tablero = nodo.tablero;
                    raiz.utilidad = utilidad;
                }
            }

            return raiz;
        }
    }
}
