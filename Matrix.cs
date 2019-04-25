using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MatrixCalculator
{
    public class Matrix
    {
        private MaskedTextBox[][] matrix;
        private int height = int.MinValue;
        private int width = int.MinValue;

        public int Height
        {
            get
            {
                if (this.height == int.MinValue)
                {
                    this.height = this.matrix.Length;
                }

                return this.height;
            }
        }

        public int Width
        {
            get
            {
                if (this.width == int.MinValue)
                {
                    if (this.matrix.Length <= 0)
                    {
                        this.width = 0;
                    }
                    else
                    {
                        this.width = this.matrix[0].Length;
                    }
                }

                return this.width;
            }
        }

        public Matrix(MaskedTextBox[][] matrix)
        {
            this.matrix = matrix;
        }

        public Matrix() {
            matrix = new MaskedTextBox[0][];
        }

        public MaskedTextBox At(int x, int y)
        {
            return this.matrix[y][x];
        }

        public void Set(int x, int y, MaskedTextBox value)
        {
            this.matrix[y][x] = value;
        }

        public override string ToString()
        {
            string s = "";

            int width = this.Width;
            int height = this.Height;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    s += this.At(x, y) + " ";
                }

                s += "\r\n";
            }

            return s;
        }
        
        public MaskedTextBox[] this[int index]
        {
            get
            {
                return (MaskedTextBox[])this.matrix[index];
            }

            set
            {
                throw new Exception("Cannot set Matrix values directly");
            }
        }
        
        public static Matrix operator *(Matrix m1, Matrix m2)
        {
            if (m1.Width != m2.Height)
            {
                throw new Exception("Cannot multiply matrices where m1 width != m2 height");
            }

            int newHeight = m1.Height;
            int newWidth = m2.Width;

            MaskedTextBox[][] newMatrix = new MaskedTextBox[newHeight][];
         
            for (int y = 0; y < newHeight; y++)
            {
                    newMatrix[y] = new MaskedTextBox[newWidth];
                for (int x = 0; x < newWidth; x++)
                {
                    int newVal = 0;

                    for (int xo = 0; xo < m1.Width; xo++)
                    {
                        newVal += Convert.ToInt16(m1[y][xo].Text) * Convert.ToInt16(m2[xo][x].Text);
                    }
                    MaskedTextBox maskedTextBox = new MaskedTextBox();
                    maskedTextBox.Text = newVal.ToString();
                    newMatrix[y][x] = maskedTextBox;
                }
            }
            
            return new Matrix(newMatrix);
        }
        
        public static Matrix operator +(Matrix m1, Matrix m2)
        {
            if (m1.Height != m2.Height || m1.Width != m2.Width)
            {
                throw new Exception("Matrix dimensions must be equal when adding");
            }

            Matrix clone = m1.Clone();

            for (int x = 0; x < clone.Width; x++)
            {
                for (int y = 0; y < clone.Height; y++)
                {
                    int el1 = Convert.ToInt32(m1.At(x, y).Text);
                    int el2 = Convert.ToInt32(m2.At(x, y).Text);
                    MaskedTextBox maskedTextBox = new MaskedTextBox();
                    maskedTextBox.Text = (el1 + el2).ToString();
                    clone.Set(x, y, maskedTextBox);
                }
            }

            return clone;
        }
        
        public static Matrix operator -(Matrix m1, Matrix m2)
        {
            if (m1.Height != m2.Height || m1.Width != m2.Width)
            {
                throw new Exception("Matrix dimensions must be equal when adding");
            }

            Matrix clone = m1.Clone();

            for (int x = 0; x < clone.Width; x++)
            {
                for (int y = 0; y < clone.Height; y++)
                {
                    int el1 = Convert.ToInt32(m1.At(x, y).Text);
                    int el2 = Convert.ToInt32(m2.At(x, y).Text);
                    MaskedTextBox maskedTextBox = new MaskedTextBox();
                    maskedTextBox.Text = (el1 - el2).ToString();
                    clone.Set(x, y, maskedTextBox);
                }
            }

            return clone;
        }

        public Matrix Clone()
        {
            //MessageBox.Show(this.height.ToString());
            MaskedTextBox[][] cloned = new MaskedTextBox[this.height][];
            for (int i = 0; i < this.height; i++)
            {
                cloned[i] = new MaskedTextBox[this.width];
                for (int j = 0; j < this.width; j++)
                {
                    MaskedTextBox maskedTextBox = new MaskedTextBox();
                    maskedTextBox.Text = this.At(i, j).Text;
                    cloned[i][j] = maskedTextBox;
                }
            }

            return new Matrix(cloned);
        }


        public static bool operator ==(Matrix m1, Matrix m2)
        {
            return m1.Equals(m2);
        }

        public static bool operator !=(Matrix m1, Matrix m2)
        {
            return !m1.Equals(m2);
        }
        
        public override bool Equals(object obj)
        {
            if (obj.GetType() != typeof(Matrix)) return false;

            Matrix other = (Matrix)obj;

            if (this.Width != other.Width || this.Height != other.Height) return false;

            for (int x = 0; x < this.Width; x++)
            {
                for (int y = 0; y < this.Height; y++)
                {
                    int myValue = Convert.ToInt32(this.At(x, y).ToString());
                    int theirValue = Convert.ToInt32(other.At(x, y).ToString());

                    if (myValue != theirValue) return false;
                }
            }

            return true;
        }

        public override int GetHashCode()
        {
            return this.matrix.GetHashCode();
        }

        public Matrix Transpose()
        {
            int width = this.Width;
            int height = this.Height;

            MaskedTextBox[][] newMatrix = new MaskedTextBox[width][];

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (newMatrix[x] == null)
                    {
                        newMatrix[x] = new MaskedTextBox[height];
                    }

                    MaskedTextBox maskedTextBox = new MaskedTextBox();
                    maskedTextBox.Text = this.At(x, y).Text;

                    newMatrix[x][y] = maskedTextBox;
                }
            }

            return new Matrix(newMatrix);
        }
    }
}