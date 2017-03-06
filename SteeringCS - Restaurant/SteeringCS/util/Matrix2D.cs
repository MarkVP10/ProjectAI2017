using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SteeringCS.util
{
    class Matrix2D
    {
        const int matrixSize = 3;
        double[,] mat = new double[matrixSize, matrixSize]; //First [] is the row, second [] is the column.
                                                          /* How it works

                                                              Array:
                                                              { {1, 2, 3}, {4, 5, 6}, {7, 8, 9} }

                                                              Matrix:
                                                              | 1 2 3 |
                                                              | 4 5 6 |
                                                              | 7 8 9 |


                                                              [0] -> { 1, 2, 3 }
                                                              [1] -> { 4, 5, 6 } (Row is the first [])
                                                              [2] -> { 7, 8, 9 }
                                                                      [0][1][2]
                                                                      (Column is the second [], aka the row item)
                                                          */


        public Matrix2D() : 
            this(1, 0, 0,
                0, 1, 0,
                0, 0, 1)
        { }

        public Matrix2D(double m11, double m12, double m13,
            double m21, double m22, double m23,
            double m31, double m32, double m33)
        {
            mat[0, 0] = m11;
            mat[0, 1] = m12;
            mat[0, 2] = m13;

            mat[1, 0] = m21;
            mat[1, 1] = m22;
            mat[1, 2] = m23;

            mat[2, 0] = m31;
            mat[2, 1] = m32;
            mat[2, 2] = m33;
        }

        public Matrix2D(double f = 1.0) :
            this(f, 0, 0,
                 0, f, 0,
                 0, 0, f)

        { }

        public Matrix2D(Vector2D v) : 
            this(v.X, 0, 0, 
            v.Y, 0, 0,
           v.W, 0, 0)
            { }




        public static Matrix2D operator +(Matrix2D m1, Matrix2D m2)
        {
            Matrix2D result = new Matrix2D();
            for (int r = 0; r < matrixSize; r++) //Every row
                for (int c = 0; c < matrixSize; c++) //Go through every row item (every column)
                    result.mat[r, c] = m1.mat[r, c] + m2.mat[r, c];

            return result;
        }
        public static Matrix2D operator -(Matrix2D m1, Matrix2D m2)
        {
            Matrix2D result = new Matrix2D();
            for (int r = 0; r < matrixSize; r++) //Every row
                for (int c = 0; c < matrixSize; c++) //Go through every row item (every column)
                    result.mat[r, c] = m1.mat[r, c] - m2.mat[r, c];

            return result;
        }




        public static Matrix2D operator *(Matrix2D m1, float fixture)
        {
            Matrix2D result = new Matrix2D();
            for (int r = 0; r < matrixSize; r++) //Every row
                for (int c = 0; c < matrixSize; c++) //Go through every row item (every column)
                    result.mat[r, c] = m1.mat[r, c] * fixture;

            return result;
        }


        public static Matrix2D operator *(float fixture, Matrix2D m1)
        {
            return m1 * fixture;
        }

        public static Matrix2D operator *(Matrix2D m1, Matrix2D m2)
        {
            Matrix2D result = new Matrix2D();
            for (int r = 0; r < matrixSize; r++)
            {
                for (int c = 0; c < matrixSize; c++)
                {
                    double sum = 0;
                    for (int i = 0; i < 3; i++)
                        sum += m1.mat[r, i] * m2.mat[i, c];
                    result.mat[r, c] = sum;
                }
            }

            return result;
        }


        public static Vector2D operator *(Matrix2D m1, Vector2D v)
        {
            Matrix2D m2 = new Matrix2D(v);
            Matrix2D m = m1 * m2;

            return ToVector2D(m);
        }


        public static Vector2D ToVector2D(Matrix2D m)
        {
            return new Vector2D(m.mat[0, 0], m.mat[1, 0], m.mat[2, 0]);
        }



        public static Matrix2D Identity()
        {
            return new Matrix2D();
        }


        //Returns a scale matrix
        public static Matrix2D Scale(float factor)
        {
            Matrix2D m = new Matrix2D(factor);
            //m.mat[matrixSize-1, matrixSize-1] = 1; not needed, seeing as the default constructor for a matrix is making an identity matrix, that has it's lower right corner set to 1.
            return m;
        }

        //Returns a rotation matrix
        public static Matrix2D Rotate(float degrees)
        {
            double radian = Math.PI / 180 * (double)degrees;
            Matrix2D m = new Matrix2D();
            m.mat[0, 0] = (float)Math.Cos(radian);
            m.mat[0, 1] = -(float)Math.Sin(radian);
            m.mat[1, 0] = (float)Math.Sin(radian);
            m.mat[1, 1] = (float)Math.Cos(radian);

            return m;
        }

        //Returns a Translation matrix
        public static Matrix2D Translate(Vector2D translationVector)
        {
            Matrix2D m = new Matrix2D();

            m.mat[0, 2] = translationVector.X;
            m.mat[1, 2] = translationVector.Y;

            return m;
        }



        public void Print()
        {
            string message = "Matrix:\r\n";
            for (int i = 0; i < matrixSize; i++) //Every row
            {
                message += "| ";
                for (int j = 0; j < matrixSize; j++) //Go through every row item (every column)
                    message += mat[i, j] + " ";
                message += "|" + "\r\n";
            }

            MessageBox.Show(message);
        }

        public override string ToString()
        {
            string message = "";
            for (int i = 0; i < matrixSize; i++) //Every row
            {
                message += "| ";
                for (int j = 0; j < matrixSize; j++) //Go through every row item (every column)
                    message += mat[i, j] + " ";
                message += "|" + "\r\n";
            }
            return message;
        }
    }
}
