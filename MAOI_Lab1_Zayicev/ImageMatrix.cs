
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAOI_Lab1_Zayicev
{
    class ImageMatrix
    {

        public ImageMatrix(Image image) 
        {
            LocalImage = image;
            this.Width = image.Width;
            this.Height = image.Height;

            SetUpMatrixes();


        }
        private void SetUpMatrixes() 
        {
            RawColorMatrix =  GetPixelsFromImageToAnArray(this.LocalImage);
            RGBColorMatrix = ConvertArraysOfColorToArrayOfStrings(RawColorMatrix);
            HalftoneMatrix = ConvertRGBToHAlftone(RGBColorMatrix);

        }
        public Image LocalImage { get; set; }
        public Color[][] RawColorMatrix { get; set; }
        public int DarkLimit { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string[][] RGBColorMatrix { get; set; }
        public string[][] BinaryMatrix { get; set; }
        public string[][] HalftoneMatrix { get; set; }


        //string[][]
        public string[][] ConvertRGBToHAlftone(string[][] rgbMatrix) 
        {
            //1

            //2 in: basepixelstring
            // string[] rGBstrings = basePixelString.Split(new char[] { ',', '\n' });

            string[][] stringOfHalftoneMatrix = new string[Width][];
            for (int i = 0; i < Width; i++)
            {
                stringOfHalftoneMatrix[i] = new string[Height];
                for (int j = 0; j < Height; j++)
                {
                    string[] rGBstrings = rgbMatrix[i][j].Split(new char[] { ',', '\n' });

                    double[] colorsRGBDouble = new double[3];
                    for (int z = 0; z < rGBstrings.Length; z++)
                    {
                        colorsRGBDouble[z] = Convert.ToDouble(rGBstrings[z]);
                    }

                    double sumOfPixelsValues = 0;
                    foreach (var item in colorsRGBDouble)
                    {
                        sumOfPixelsValues += item / 3;
                        sumOfPixelsValues = Math.Round(sumOfPixelsValues, 0);
                    }
                    stringOfHalftoneMatrix[i][j] = sumOfPixelsValues.ToString();
                }
            }
            return stringOfHalftoneMatrix;
        }
        public Color[][] ConvertArraysOfStringToArrayOfColors(string[][] imageColorString)
        {
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
            string[] rGBstrings = colorString.Split(new char[] { ',', '\n' });
            Color color = Color.FromArgb(
                Convert.ToInt32(rGBstrings[0]),
                Convert.ToInt32(rGBstrings[1]),
                Convert.ToInt32(rGBstrings[2]));
            return color;
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
        public string[][] ConvertArraysOfColorToArrayOfStrings(Color[][] imageColorPixels)
        {
            string[][] imageStringPixels = new string[imageColorPixels.Length][];
            for (int i = 0; i < imageColorPixels.Length; i++)
            {
                imageStringPixels[i] = new string[imageColorPixels[i].Length];
                for (int j = 0; j < imageColorPixels[i].Length; j++)
                {
                    imageStringPixels[i][j] = $"{imageColorPixels[i][j].R},{imageColorPixels[i][j].G},{imageColorPixels[i][j].B}";
                }
            }
            return imageStringPixels;
        }
    }
}
