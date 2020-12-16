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
    public partial class Form1 : Form
    {

        ImageMatrix ImageMatrix;
        public object StrMatrix { get; set; }
        public object MatrixHalfTone { get; set; }

        public void UpdateIntencivityMatrix(double[][] arrayOfHalftone) 
        {
            dataGridView1.RowCount = arrayOfHalftone.Length;
            dataGridView1.ColumnCount = arrayOfHalftone[0].Length;
            for (int i = 0; i < arrayOfHalftone.Length; i++)
            {
                for (int j = 0; j < arrayOfHalftone[0].Length; j++)
                {
                    dataGridView1.Rows[i].Cells[j].Value = arrayOfHalftone[i][j];
                }
            }

            //dataGridView1.Rows[0].Cells[0].Value = (DataGridView)MatrixHalfTone;
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void OpenFile_Click(object sender, EventArgs e)
        {
            string filePath;
            Image image;
            DialogResult dialogRes = openFileDialog1.ShowDialog();
            if (CheckDialogResult(dialogRes)) 
            {
                filePath =
                openFileDialog1.FileName;
             
                image = new Bitmap(filePath);
                pictureBox1.Image = image;
                ImageMatrix = new ImageMatrix(image);
                this.Text = $"Maoi_zayicev {openFileDialog1.SafeFileName}";
                
            }
        }
        private void SetMatrix() 
        {
            //dataGridView1.ColumnCount = pictureBox1.Width;
           // dataGridView1.RowCount = pictureBox1.Height;
            
        }
        private bool CheckDialogResult(DialogResult result)  
        {
            switch (result)
            {
                case DialogResult.None:
                    return false;
                case DialogResult.OK:
                    return true;
                case DialogResult.Cancel:
                    return false;
                case DialogResult.Abort:
                    return false;
                case DialogResult.Retry:
                    return false;
                case DialogResult.Ignore:
                    return false;
                case DialogResult.Yes:
                    return true;
                case DialogResult.No:
                    return false;
                default:
                    return false;
            }


        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.ColumnCount = pictureBox1.Image.Width;
            dataGridView1.RowCount = pictureBox1.Image.Height;

            FillGridWithStringMatrix(ImageMatrix.HalftoneMatrix);
            BuildMatrixOfJointOccurencies(ImageMatrix.HalftoneMatrix);
/*            ImageMatrix.
*/
            /*            Matrix matrixForm = new Matrix();
                        matrixForm.ParentBaseForm = this;
                        matrixForm.stringPixelMatrix = (string[][])StrMatrix;
                        matrixForm.Show();
            */

        }
        private void BuildMatrixOfJointOccurencies(string[][] halftoneMatrix) 
        {
            //d = 1
            //& = 90*

            //steps => 1. get all gradations
            //2. build matrix accordingly :2.1: matrix size, caption;
            //3.analys matrix and with function p(i,j,d,&) and fill matrix


            List<int> gradations = new List<int>();
            foreach (var item in halftoneMatrix)
            {
                foreach (var element in item)
                {
                    int elemInt = Convert.ToInt32(element);
                    if(gradations.IndexOf(elemInt) == -1) 
                    {
                        gradations.Add(elemInt);              
                    }
                }
            }

            gradations.Sort();
            
            dataGridView2.ColumnCount = gradations.Count;
            dataGridView2.RowCount = gradations.Count;
            for (int i = 0; i < dataGridView2.ColumnCount; i++)
            {
                dataGridView2.Rows[i].HeaderCell.Value = gradations.ElementAt(i).ToString(); //i.ToString();
                dataGridView2.Columns[i].HeaderCell.Value = gradations.ElementAt(i).ToString();//i.ToString();

                //dataGridView2.Rows[i].HeaderCell.Value = i;
            }
            
            int[][] arrayOfGradations = new int[gradations.Count][];
            for (int i = 0; i < arrayOfGradations.Length; i++)
            {
                arrayOfGradations[i] = new int[gradations.Count]; 
                for (int j = 0; j < arrayOfGradations[i].Length; j++)
                {
                    arrayOfGradations[i][j] = 0;
                }
            }
            for (int i = 0; i < halftoneMatrix.Length; i++)
            {
                for (int j = 0; j < halftoneMatrix[i].Length; j++)
                {
                    if (j == 0)//change to (d-1)+cos(&) 
                        //j => (d-1) +sin(&)
                    { 
                        continue; 
                    }
                    else
                    {
                        arrayOfGradations[gradations.IndexOf(Convert.ToInt32(halftoneMatrix[i][j-1]))]
                            [gradations.IndexOf(Convert.ToInt32(halftoneMatrix[i][j]))]++;
                    }
                }
            }

            int sumOfGrad = 0;
            for (int i = 0; i < dataGridView2.RowCount; i++)
            {
                for (int j = 0; j < dataGridView2.ColumnCount; j++)
                {
                    dataGridView2.Rows[i].Cells[j].Value = arrayOfGradations[i][j];
                    sumOfGrad += arrayOfGradations[i][j];
                }
            }
            double[][] arraysOfNormalsGrad = new double[gradations.Count][];
            
            dataGridView3.RowCount = dataGridView2.RowCount;
            dataGridView3.ColumnCount = dataGridView2.ColumnCount;
            for (int i = 0; i < dataGridView3.RowCount; i++)
            {
                arraysOfNormalsGrad[i] = new double[dataGridView3.ColumnCount];
                for (int j = 0; j < dataGridView3.ColumnCount; j++)
                {
                    arraysOfNormalsGrad[i][j] =Math.Round( Convert.ToDouble( arrayOfGradations[i][j]) / sumOfGrad,3);
                    dataGridView3.Rows[i].Cells[j].Value = arraysOfNormalsGrad[i][j];

                }
            }
            SetPropertiesTable(arraysOfNormalsGrad);


        }
        private void SetPropertiesTable(double[][] normalsArr) 
        {
            dataGridView4.RowCount = 4;
            dataGridView4.ColumnCount = 2;

            dataGridView4.Rows[0].Cells[0].Value = "Энергия";
            dataGridView4.Rows[1].Cells[0].Value = "Энтропия";
            dataGridView4.Rows[2].Cells[0].Value = "Контраст";
            dataGridView4.Rows[3].Cells[0].Value = "Гомогенность";

            dataGridView4.Rows[0].Cells[1].Value = GetPropertyEnergy(normalsArr);
            dataGridView4.Rows[1].Cells[1].Value = GetPropertyEntropy(normalsArr);
            dataGridView4.Rows[2].Cells[1].Value = GetPropertyContrast(normalsArr);
            dataGridView4.Rows[3].Cells[1].Value = GetPropertyHomogeinty(normalsArr);



        }
        private string GetPropertyEnergy(double[][] normalsArr) 
        {
            double sum = 0;
            foreach (var rows in normalsArr)
            {
                foreach (var item in rows)
                {
                    sum += Math.Pow(item,2);
                }
            }
            return sum.ToString();
        
        }
        private string GetPropertyEntropy(double[][] normalsArr)
        {
            double sum = 0;

            for (int i = 0; i < normalsArr.Length; i++)
            {

                for (int j = 0; j < normalsArr[i].Length; j++)
                {
                    double log= Math.Log(normalsArr[i][j]+1, 2.0);
                    sum += normalsArr[i][j] * log;
                }
            }

            return (-sum).ToString();


        }
        private string GetPropertyContrast(double[][] normalsArr) 
        {

            double sum = 0;

            for (int i = 0; i < normalsArr.Length; i++)
            {

                for (int j = 0; j < normalsArr[i].Length; j++)
                {
                    sum += normalsArr[i][j] * Math.Pow(i-j,2);
                }
            }

            return sum.ToString();


        }
        private string GetPropertyHomogeinty(double[][] normalsArr) 
        {

            double sum = 0;

            for (int i = 0; i < normalsArr.Length; i++)
            {

                for (int j = 0; j < normalsArr[i].Length; j++)
                {
                    sum += normalsArr[i][j] / (1 + Math.Abs(i -j));
                }
            }

            return sum.ToString();
        }


        public void FillGridWithStringMatrix(string[][] givenMatrix) 
        {
            for (int i = 0; i < givenMatrix.Length; i++)
            {
                for (int j = 0; j < givenMatrix[i].Length; j++)
                {
                    dataGridView1.Columns[i].Width = 35;
                    dataGridView1.Rows[j].Cells[i].Value = givenMatrix[i][j];
                }
            }
        
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SetMatrix();
        }
        
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Form1 newForm = new Form1();
            newForm.Show();
        }
    }
}
