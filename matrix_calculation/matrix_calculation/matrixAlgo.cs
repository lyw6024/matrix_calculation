using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace matrix_calculation
{
    class matrixAlgo
    {
        private static void swapLine(ref List<RationalNumber> Mat, ref List<RationalNumber> Vect,int N,int k1,int k2)
        {
            RationalNumber tmp;
            tmp = Vect[k1];
            Vect[k1] = Vect[k2];
            Vect[k2] = tmp;

            for (int i=0;i<N;i++)
            {
                tmp = Mat[k1 * N + i];
                Mat[k1 * N + i] = Mat[k2 * N + i];
                Mat[k2 * N + i] = tmp;
            }
        }
        private static void upperTriangularWithAugmented_Line(ref List<RationalNumber> Mat, ref List<RationalNumber> Vect, int N, int i)
        {
            int k1, k2;
            for (k1 = i + 1; k1 < N; k1++)
            {
                RationalNumber rate = Mat[k1 * N + i] / Mat[i * N + i];
                Vect[k1] = Vect[k1] - rate * Vect[i];
                for (k2 = i; k2 < N; k2++)
                {
                    Mat[k1 * N + k2] = Mat[k1 * N + k2] - rate * Mat[i * N + k2];
                }
            }
        }
        private static bool upperTriangularWithAugmented(ref List<RationalNumber> Mat, ref List<RationalNumber> Vect,int N)
        {
            bool allLineZero = true;
            for(int i=0;i<N;i++)
            {
                for(int k1=i;k1<N;k1++)
                {
                    if (!(Mat[k1 * N + i] == 0))
                    {
                        swapLine(ref Mat, ref Vect, N, k1, i);
                        allLineZero = false;
                        break;
                    }
                }
                if (allLineZero)
                {
                    break;
                }
                else
                {
                    upperTriangularWithAugmented_Line(ref Mat,ref Vect, N, i);
                }
            }
            return allLineZero;
        }
        private static string Mat2LaTeX(ref List<RationalNumber> Mat, ref List<RationalNumber> Vect,int N)
        {
            string msg = @"\pmatrix{";
            for(int i=0;i<N;i++)
            {
                if(i>0)
                {
                    msg += @"\\";
                }
                for(int j=0;j<N;j++)
                {
                    if(j>0)
                    {
                        msg += @"&";
                    }
                    msg += Mat[i * N + j].ToLaTeX();
                }
            }
            msg += "}X=";
            msg += @"\pmatrix{";
            for(int i=0;i<N;i++)
            {
                if (i > 0)
                    msg += @"\\";
                msg += Vect[i].ToLaTeX();
            }
            msg += "}";
            return msg;
        }
        private static string Vect2LaTeX(ref List<RationalNumber> Vect,int N)
        {
            string msg = @"\pmatrix{";
            for(int i=0;i<N;i++)
            {
                if(i>0)
                {
                    msg += @"\\";
                }
                msg += Vect[i].ToLaTeX();
            }
            msg += "}";
            return msg;
        }
        private static void identityMatrix(ref List<RationalNumber> Mat, ref List<RationalNumber> Vect,int N)
        {
            int i, j;
            for(i=N-1;i>=0;i--)
            {
                RationalNumber rate = 1 / Mat[i * N + i];
                Vect[i] = rate * Vect[i];
                Mat[i * N + i] = new RationalNumber(1);
                for( j=0;j<i;j++)
                {
                    Vect[j] = Vect[j] - Vect[i] * Mat[j * N + i];
                    Mat[j * N + i] = new RationalNumber(0);
                }
            }
        }
        public static string linearEquation(ref List<RationalNumber> Mat,ref List<RationalNumber> Vect)
        {
            int N = 1;
            while(Mat.Count>=(N+1)*(N+1))
            {
                N++;
            }
            if (Vect.Count >= N)
            {
                upperTriangularWithAugmented(ref Mat, ref Vect,N);
                string latex_line1 = Mat2LaTeX(ref Mat, ref Vect, N);
                identityMatrix(ref Mat, ref Vect, N);
                Vect2LaTeX(ref Vect, N);
                return latex_line1 +",X="+ Vect2LaTeX(ref Vect, N); ;
            }
            else
            {
                return @"SyntexError";
            }
        }
    }
}
