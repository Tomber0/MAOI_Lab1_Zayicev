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
        public object StrMatrix { get; set; }


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
            }
        }
        private void SetMatrix() 
        {
            dataGridView1.ColumnCount = pictureBox1.Width;
            dataGridView1.RowCount = pictureBox1.Height;
            
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
        private Color[][] GetPixelsFromImageToAnArray(Image image)
        {
            int height = image.Height;
            int width = image.Width;
            Bitmap imageBitmap = new Bitmap(image);
            Color[][] imagePixels = new Color[width][];

            for (int i = 0; i < width; i++)
            {
                imagePixels[i] = new Color[height];
                for (int j = 0; j < height; j++)
                {
                    imagePixels[i][j] = imageBitmap.GetPixel(i, j);
                }
            }
            return imagePixels;
        }
        private string[][] ConvertArraysOfColorToArrayOfStrings(Color[][] imageColorPixels)
        {
            string[][] imageStringPixels = new string[imageColorPixels.Length][];
            for (int i = 0; i < imageColorPixels.Length; i++)
            {
                imageStringPixels[i] = new string[imageColorPixels[i].Length];
                for (int j = 0; j < imageColorPixels[i].Length; j++)
                {
                    imageStringPixels[i][j] =$"{imageColorPixels[i][j].R},{imageColorPixels[i][j].G},{imageColorPixels[i][j].B}";
                }
            }
            return imageStringPixels;
        }
        private Color[][] ConvertArraysOfStringToArrayOfColors(string[][] imageColorString)
        {
            string[][] imageStringPixels = new string[imageColorString.Length][];

            Color[][] imageColorPixel = new Color[imageColorString.Length][];
            for (int i = 0; i < imageColorString.Length; i++)
            {
                imageColorPixel[i] = new Color[imageColorString[i].Length];
                for (int j = 0; j < imageColorString[i].Length; j++)
                {
                    //imageColorPixel[i][j] =  new Color();// $"{imageColorString[i][j].R},{imageColorString[i][j].G},{imageColorString[i][j].B}";
                    //imageColorPixel[i][j].R = imageColorString[i][j]

                    imageColorPixel[i][j] = GetColorFromString(imageColorString[i][j]);
                }
            }
            return imageColorPixel;
        }
        private Color GetColorFromString(string colorString) 
        {

            string[] rGBstrings = colorString.Split(new char[] {',','\n' });
            Color color = Color.FromArgb(
                Convert.ToInt32(rGBstrings[0]),
                Convert.ToInt32(rGBstrings[1]),
                Convert.ToInt32(rGBstrings[2]));

            return color;
        
        }
        private void button1_Click(object sender, EventArgs e)
        {


            string[][] abs =  ConvertArraysOfColorToArrayOfStrings( GetPixelsFromImageToAnArray(pictureBox1.Image));
            ConvertArraysOfStringToArrayOfColors(abs);


            this.StrMatrix = abs;
            Matrix matrixForm = new Matrix();
            matrixForm.ParentBaseForm = this;
            matrixForm.stringPixelMatrix = (string[][])StrMatrix;
            matrixForm.Show();


        }
    }
}
