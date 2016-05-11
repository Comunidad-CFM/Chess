using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Clases
{
    class AlphaBeta
    {
        public double alphaAux,
                      betaAux;
        public AlphaBeta()
        {
            this.alphaAux = 0;
            this.betaAux = 0;
        }

        public double alphaMax(Nodo raiz, double alpha, double beta)
        {
            if (raiz.hijos.Count == 0)
            {
                return raiz.utilidad;
            }

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

        public double betaMin(Nodo raiz, double alpha, double beta)
        {
            if (raiz.hijos.Count == 0)
            {
                return raiz.utilidad;
            }

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

        public Nodo alphaBeta(Nodo raiz)
        {
            double utilidad;
            foreach (Nodo nodo in raiz.hijos)
            {
                utilidad = alphaMax(nodo, -1000, 1000);

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
