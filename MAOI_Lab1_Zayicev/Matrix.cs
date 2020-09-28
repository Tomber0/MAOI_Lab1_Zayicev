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

        public Form1 ParentBaseForm { get; set; }
        public string[][] stringPixelMatrix { get; set; }
        public Matrix()
        {
            InitializeComponent();
        }
        private void Matrix_Load(object sender, EventArgs e)
        {
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

        private void button1_Click(object sender, EventArgs e)
        {
            //setmatrix to diffrent color mode
        }
    }
}
