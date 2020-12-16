using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MAOI_Lab1_Zayicev
{
    public partial class Matrix : Form
    {
        public enum ImageModeOptions {Color, Halftone, Binary};
        public Form1 ParentBaseForm { get; set; }
        public string[][] stringPixelMatrix { get; set; }
        public Matrix()
        {
            InitializeComponent();
        }
        private void Matrix_Load(object sender, EventArgs e)
        {
            textBox1.Text = "100";
            button1.Text = "Обновить матрицу";
            comboBox1.SelectedIndex = 0;
            string[][] stringPixelArray = stringPixelMatrix;
            dataGridView1.ColumnCount = stringPixelMatrix.Length;
            dataGridView1.RowCount = stringPixelMatrix[0].Length;
            for (int i = 0; i < stringPixelArray[0].Length; i++)
            {
                for (int j = 0; j < stringPixelArray.Length; j++)
                {
                    dataGridView1.Rows[i].Cells[j].Value = stringPixelArray[j][i];
                }
            }
        }
        /// <summary>
        /// обновить матрицу в соответствии с DropBoxMenu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();

            //setmatrix to diffrent color mode
            double[][] sendDoubleArray = new double[dataGridView1.RowCount][];
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                sendDoubleArray[i] = new double[dataGridView1.ColumnCount];
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                {
                    string newPixelWithIndexIJ = TransformMatrix((ImageModeOptions)comboBox1.SelectedIndex,dataGridView1.Rows[i].Cells[j].Value.ToString());
                    //string newValue    = String.Format("{0:d4}", newPixelWithIndexIJ);
                    dataGridView1.Rows[i].Cells[j].Value = newPixelWithIndexIJ;//newValue;
                    int colorBg = Convert.ToInt32(newPixelWithIndexIJ);
                    dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.FromArgb(colorBg, colorBg, colorBg);
                    sendDoubleArray[i][j] =Convert.ToDouble( dataGridView1.Rows[i].Cells[j].Value);                   
                }
            }
            //f1.MatrixHalfTone;
            ParentBaseForm.UpdateIntencivityMatrix(sendDoubleArray);
        }
        private string TransformMatrix(ImageModeOptions mode, string basePixelString) 
        {
            switch (mode)
            {
                case ImageModeOptions.Color:
                    break;
                case ImageModeOptions.Halftone:
                    return TransformPixelTohalftone(basePixelString);
                    
                case ImageModeOptions.Binary:
                    break;
                default:
                    return "0";
                       
            }
            return "0";
        }
        private string TransformPixelTohalftone(string basePixelString)
        {
            double darkLimit = Convert.ToDouble(textBox1.Text);
            string[] rGBstrings = basePixelString.Split(new char[] { ',', '\n' });

            double[] colorsRGBDouble = new double[3];
            for (int i = 0; i < rGBstrings.Length; i++)
            {
                colorsRGBDouble[i] = Convert.ToDouble(rGBstrings[i]);
            }

            double sumOfPixelsValues =0;
            foreach (var item in colorsRGBDouble)
            {
                sumOfPixelsValues += item/3;
                sumOfPixelsValues = Math.Round(sumOfPixelsValues, 0);
            }
            return sumOfPixelsValues.ToString();
            //return colorsRGBDouble;
        }        
    }
}
