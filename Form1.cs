using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab6
{
    public partial class Form1 : Form
    {
        int MatrixSize = 0;
        double[,] matrix;
        double[,] matrix2;
        double[] p;

        public double divKoof;
        string f;
        double[] X;//лямбда
        public Form1()
        {
            InitializeComponent();
        }
        void ReadMatrixFromGrid()
        {
            matrix = new double[MatrixSize, MatrixSize];//создаем матрицу

            for (int i = 0; i < MatrixSize; i++)
            {
                for (int j = 0; j < MatrixSize; j++)
                {
                    matrix[i, j] = Convert.ToDouble(dgMatrix.Rows[i].Cells[j].Value);//запоминаем значения
                }
            }
            ShowMatrixInGrid();//показываем матрицу
        }


        void ShowMatrixInGrid()
        {
            for (int i = 0; i < MatrixSize; i++)
            {
                for (int j = 0; j < MatrixSize; j++)
                {
                    dgMatrix.Rows[i].Cells[j].Value = matrix[i, j].ToString();//выводим матрицу
                }
            }
        }

        void CreateMatrixTest()
        {
            MatrixSize = Convert.ToInt32(tbSize.Text);
            dgMatrix.Rows.Clear();
            dgMatrix.Columns.Clear(); // очищаем солонки
            int i = 0;
            for (i = 0; i < MatrixSize; i++)
            {
                dgMatrix.Columns.Add("x" + i.ToString(), "x" + i.ToString());
            }

            dgMatrix.Rows.Add(MatrixSize);
            i = 0;
            dgMatrix.Rows[i].Cells[0].Value = "2,2";
            dgMatrix.Rows[i].Cells[1].Value = "1";
            dgMatrix.Rows[i].Cells[2].Value = "0,5";
            dgMatrix.Rows[i].Cells[3].Value = "2";

            i = 1;
            dgMatrix.Rows[i].Cells[0].Value = "1";
            dgMatrix.Rows[i].Cells[1].Value = "1,3";
            dgMatrix.Rows[i].Cells[2].Value = "2";
            dgMatrix.Rows[i].Cells[3].Value = "1";

            i = 2;
            dgMatrix.Rows[i].Cells[0].Value = "0,5";
            dgMatrix.Rows[i].Cells[1].Value = "2";
            dgMatrix.Rows[i].Cells[2].Value = "0,5";
            dgMatrix.Rows[i].Cells[3].Value = "1,6";

            i = 3;
            dgMatrix.Rows[i].Cells[0].Value = "2";
            dgMatrix.Rows[i].Cells[1].Value = "1";
            dgMatrix.Rows[i].Cells[2].Value = "1,6";
            dgMatrix.Rows[i].Cells[3].Value = "2";
            //i = 0;
            //dgMatrix.Rows[i].Cells[0].Value = "1";
            //dgMatrix.Rows[i].Cells[1].Value = "1,2";
            //dgMatrix.Rows[i].Cells[2].Value = "2";
            //dgMatrix.Rows[i].Cells[3].Value = "0,5";

            //i = 1;
            //dgMatrix.Rows[i].Cells[0].Value = "1,2";
            //dgMatrix.Rows[i].Cells[1].Value = "1";
            //dgMatrix.Rows[i].Cells[2].Value = "0,4";
            //dgMatrix.Rows[i].Cells[3].Value = "1,2";

            //i = 2;
            //dgMatrix.Rows[i].Cells[0].Value = "2";
            //dgMatrix.Rows[i].Cells[1].Value = "0,4";
            //dgMatrix.Rows[i].Cells[2].Value = "2";
            //dgMatrix.Rows[i].Cells[3].Value = "1,5";

            //i = 3;
            //dgMatrix.Rows[i].Cells[0].Value = "0,5";
            //dgMatrix.Rows[i].Cells[1].Value = "1,2";
            //dgMatrix.Rows[i].Cells[2].Value = "1,5";
            //dgMatrix.Rows[i].Cells[3].Value = "1";
        }

        void CreateMatrix2()
        {
            MatrixSize = Convert.ToInt32(tbSize.Text);
            dgMatrix.Rows.Clear();
            dgMatrix.Columns.Clear(); // очищаем солонки
            for (int i = 0; i < MatrixSize; i++)
            {
                dgMatrix.Columns.Add("x" + i.ToString(), "x" + i.ToString());
            }

            dgMatrix.Rows.Add(MatrixSize);
            for (int i = 0; i < MatrixSize; i++)
            {
                for (int j = 0; j < MatrixSize; j++)
                {
                    dgMatrix.Rows[i].Cells[j].Value = "1"; // записываем значеня в соответсвующие места
                }
            }
        }

        double[,] Multiply(double[,] A, double[,] B)
        {
            double[,] K = new double[MatrixSize,MatrixSize];
            double S;
            for (int i = 0; i < MatrixSize; i++)
            {
                for (int j = 0; j < MatrixSize; j++)
                {
                    S = 0;
                    for (int k = 0; k < MatrixSize; k++)
                    {
                        S = S + A[i, k] * B[k,j];
                    }
                    K[i,j] = S;
                }
                
            }
            return K;
        }

        double[] MultiplyVector(double[,] A, double[] b)
        {
            double[] b_i = new double[MatrixSize];
            double S;
            for (int i = 0; i < MatrixSize; i++)
            {
                S = 0;
                for (int j = 0; j < MatrixSize; j++)
                {
                    S = S + A[i, j] * b[j];
                }
                b_i[i] = S;
            }
            return b_i;
        }

        double [,] Ematrix()
        {
            double[,] E = new double[MatrixSize, MatrixSize];
            for(int i = 0; i < MatrixSize; i++)
            {
                for (int j = 0; j < MatrixSize; j++)
                {
                    if (i == j) E[i, j] = 1;
                    else E[i, j] = 0;
                }
            }
            return E;
        }

        void F()
        {
            f = " ";
            for (int i = 0; i < MatrixSize + 1; i++)
            {
                if (i == 0) f = $"x^{MatrixSize} +";
                else
                {
                    if (i < MatrixSize) f += $"({p[i - 1]}) * x^{MatrixSize - i} +";
                    else f += $"({p[i - 1]}) * x^{MatrixSize - i}";
                }
            }
        }

        void Danilevskogo()
        {
            matrix = new double[MatrixSize, MatrixSize];
            ReadMatrixFromGrid();
            double[,] M = new double[MatrixSize, MatrixSize];
            double[,] M_minus = new double[MatrixSize, MatrixSize];
            double[,] B = new double[MatrixSize, MatrixSize];
            p = new double[MatrixSize];
            double[] y = new double[MatrixSize];
            double[] vector = new double[MatrixSize];
            for (int k = MatrixSize - 2; k >= 0; k--)
            {
                M = Ematrix();
                M_minus = Ematrix();
                for(int j = 0; j < MatrixSize; j++)
                {
                    if (j == k) M[k, j] = 1 / matrix[k + 1, k];
                    else M[k, j] = -matrix[k + 1, j] / matrix[k + 1, k];

                    M_minus[k, j] = matrix[k + 1, j];
                }

                matrix = Multiply(M_minus, matrix);
                matrix = Multiply(matrix, M);
                if (k == MatrixSize - 2)
                {
                    for (int i = 0; i < MatrixSize; i++)
                    {
                        for (int j = 0; j < MatrixSize; j++)
                        {
                            B[i, j] = M[i, j];
                        }
                    }
                }
                else B = Multiply(B, M);
            }
            for(int i = 0; i< MatrixSize; i++)
            {
                p[i] = -matrix[0, i];
            }
            Lobachevskogo();
            int iter = 0;

            do
            {
                y[MatrixSize - 1] = 1;
                for(int i = MatrixSize - 2; i >= 0; i--)
                {
                    y[i] = X[iter] * y[i + 1];
                }

                vector = MultiplyVector(B, y);
                listBox2.Items.Add("---------------");
                for (int i = 0; i < MatrixSize; i++)
                {
                    listBox2.Items.Add(Math.Round(vector[i],4));
                }
                iter++;
            } while (iter != MatrixSize);

        }

        void Lobachevskogo()
        {
            F();
            MathExpression fun = new MathExpression(f);
            listBox1.Items.Clear();
            double E = 0.01;
            double[] A1 = new double[MatrixSize + 1];
            double[] A2 = new double[MatrixSize + 1];
            X = new double[MatrixSize];
            int k, iter, iter1, k1, k2;
            double S, res, m, x, tmp, max;
            for (int i = 0; i < MatrixSize + 1; i++)
            {
                if (i == 0) A1[i] = 1;
                else A1[i] = p[i - 1];
            }
            iter = 0;
            do
            {
                k = 0;
                for (int i = 0; i < MatrixSize + 1; i++)
                {
                    S = 0;
                    k1 = i - 1;
                    k2 = i + 1;
                    iter1 = 1;
                    while (true)
                    {
                        if (k1 < 0 || k2 > MatrixSize) break;
                        else
                        {
                            if (iter1 % 2 == 0) S = S + 2 * (A1[k1] * A1[k2]);//шукаю проміжні результати для знаходження наступних значень
                            else S = S - 2 * (A1[k1] * A1[k2]);
                        }

                        iter1++;
                        k1 = i - iter1;
                        k2 = i + iter1;
                    }
                    A2[i] = Math.Pow(A1[i], 2) + S;//шукаю по формулам масив значень після піднесення в степінь
                }

                for (int i = 0; i < MatrixSize + 1; i++)
                {
                    res = Math.Round(Math.Pow(A1[i], 2), 3);
                    tmp = Math.Round(A2[i], 3) - res;
                    max = Math.Abs(p.Max());

                    if (max < 16)
                    {
                        if (Math.Abs(tmp) <= E || iter == 6) k++;//перевіряю умову виходу з ітераційного процессу
                    }
                    else
                    {
                        if (Math.Abs(tmp) <= E || iter == 5) k++;
                    }
                }

                for (int i = 0; i < MatrixSize + 1; i++)
                {
                    A1[i] = A2[i];
                }
                iter++;
            }
            while (k != MatrixSize + 1);

            m = Math.Pow(2, iter);

            for (int i = 0; i <= MatrixSize - 1; i++)
            {
                x = -A1[i + 1] / A1[i];//по формулам рахую х
                if (x < 0) X[i] = (-1) * Math.Pow(Math.Abs(x), 1 / m);
                else X[i] = Math.Pow(x, 1 / m);
            }
            for (int i = 0; i < MatrixSize; i++)//MatrixSize - count
            {
                double f = fun.Calculate(X[i]);
                if (Math.Round(f) != 0) X[i] = X[i] * (-1);
                f = fun.Calculate(X[i]);
                if (Math.Round(f) == 0) listBox1.Items.Add("l" + i.ToString() + " = " + Math.Round(X[i], 4));//виводжу лямбда
            }
        }
        private void btnMatrix_Click(object sender, EventArgs e)
        {
            CreateMatrix2();
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            CreateMatrixTest();
        }

        private void btnCalc_Click(object sender, EventArgs e)
        {
            Danilevskogo();
        }
    }
}
