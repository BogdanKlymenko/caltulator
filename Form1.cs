using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MatrixCalculator
{
    public partial class Form1 : Form
    {
        Matrix matrix1, matrix2, matrix3;
        public Form1()
        {
            InitializeComponent();
            matrix1 = new Matrix();
            matrix2 = new Matrix();
            matrix3 = new Matrix();
        }


        private bool checkOnEmpty()
        {
            return maskedTextBox2.Text != "" && maskedTextBox1.Text != "";
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (checkOnEmpty())
            {
                int n = Int32.Parse(maskedTextBox1.Text.ToString());
                int m = Int32.Parse(maskedTextBox2.Text.ToString());
               
                if (matrix1.Height != n|| matrix2.Height != m)
                {
                    clear(matrix1);
                    clear(matrix2);
                    MaskedTextBox[][] maskedTextBoxes1 = new MaskedTextBox[n][];
                    MaskedTextBox[][] maskedTextBoxes2 = new MaskedTextBox[m][];


                    for (int i = 0; i < n; i++)
                    {
                        maskedTextBoxes1[i] = new MaskedTextBox[m];
                        for (int j = 0; j < m; j++)
                        {
                            MaskedTextBox textBox = new MaskedTextBox();
                            textBox.Mask = "0";
                            textBox.Text = i.ToString();
                            textBox.Size = new Size(20, 20);
                            textBox.Location = new Point(100 + j * 20, 10 + i * 20);
                            
                            Controls.Add(textBox);
                            maskedTextBoxes1[i][j] = textBox;
                        }
                    }

                    for (int i = 0; i < m; i++)
                    {
                        maskedTextBoxes2[i] = new MaskedTextBox[n];
                        for (int j = 0; j < n; j++)
                        {
                            MaskedTextBox textBox = new MaskedTextBox();
                            textBox.Mask = "0";
                            textBox.Text = j.ToString();
                            textBox.Size = new Size(20, 20);
                            textBox.Location = new Point(300 + j * 20, 10 + i * 20);
                            Controls.Add(textBox);
                            maskedTextBoxes2[i][j] = textBox;
                        }
                    }

                    matrix1 = new Matrix(maskedTextBoxes1);
                    matrix2 = new Matrix(maskedTextBoxes2);
                }

                Form1.ActiveForm.Size = new Size(700, 500);

                clear(matrix3);

                Label label = new Label();
                label.Text = "Result Matrix:";
                label.Location = new Point(100, 220);
                Controls.Add(label);

                if (radioButton1.Checked)
                    {
                        if (n != m)
                        {
                            MessageBox.Show("Sizes of matrixes are difference");
                            return;
                        }
                        else
                        {
                            matrix3 = matrix1 + matrix2;
                        setResMatrix(n, n);
                    }
                 }
                    else if (radioButton2.Checked)
                    {
                        if (n != m)
                        {
                            MessageBox.Show("Sizes of matrixes are difference");
                            return;
                        }
                        else
                        {
                            matrix3 = matrix1 - matrix2;
                        setResMatrix(n, m);
                        
                    }
                    }
                    else if (radioButton3.Checked)
                    {
                        matrix3 = matrix1 * matrix2;
                    setResMatrix(n, n);
                    }
                    else if (radioButton4.Checked)
                    {
                        matrix3 = matrix1.Transpose();
                    //clear(matrix2);
                    setResMatrix(m, n);
                }
                    else
                    {
                        MessageBox.Show("Select operation");
                        return;
                    }
                
            }
        }

        private void setResMatrix(int n, int m)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    MaskedTextBox textBox = matrix3[i][j];
                    textBox.Size = new Size(25, 20);
                    textBox.Location = new Point(100 + j * 25, 250 + i * 20);
                    textBox.ReadOnly = true;
                    Controls.Add(textBox);
                }
            }
        }

        private void clear(Matrix matrix)
        {
            for (int i = 0; i < matrix.Height; i++)
            {
                for (int j = 0; j < matrix.Width; j++)
                {
                    matrix[i][j].Clear();
                    matrix[i][j].Visible = false;
                }
            }
        }
    }
}
