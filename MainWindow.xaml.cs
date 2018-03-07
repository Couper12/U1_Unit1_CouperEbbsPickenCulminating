// Couper Ebbs-Picken
// 2/26/2018
// Identify and draw a rectangle around a power cube in a picture
// Based off sample code provided by I. Mctavish

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace canvasImage
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnGetFile_Click(object sender, RoutedEventArgs e)
        {
            if (canvas.Children.Count > 0)
            {
                canvas.Children.RemoveAt(0);
            }
            Microsoft.Win32.OpenFileDialog openFileD = new Microsoft.Win32.OpenFileDialog();
            openFileD.ShowDialog();

            BitmapImage bi = new BitmapImage(new Uri(openFileD.FileName));
            System.Windows.Media.ImageBrush ib = new ImageBrush(bi);
            canvas.Background = ib;

            int stride = bi.PixelWidth * 4;
            int size = bi.PixelHeight * stride;
            byte[] pixels = new byte[size];
            bi.CopyPixels(pixels, stride, 0);



            int x = 0;
            int y = 0;
            int index = y * stride + 4 * x;

            // declare variables
            bool breakpoint = false;
            Point firstPoint = new Point(0, 0);

            // starting a for loop that checks every 20 y values
            for (int i = 20; i < bi.PixelHeight; i += 20)
            {
                // exiting loop if this is true
                if (breakpoint == true)
                {
                    break;
                }

                // starting a loop that checks every 20 x values
                for (int j = 20; j < bi.PixelWidth; j += 20)
                {
                    //get pixel
                    stride = bi.PixelWidth * 4;
                    size = bi.PixelHeight * stride;
                    pixels = new byte[size];
                    bi.CopyPixels(pixels, stride, 0);



                    x = j;
                    y = i;
                    index = y * stride + 4 * x;


                    byte blue = pixels[index];
                    byte green = pixels[index + 1];
                    byte red = pixels[index + 2];
                    byte alpha = pixels[index + 3];
                    //MessageBox.Show(red.ToString() + ", " + green.ToString() + ", " + blue.ToString());

                    // checking if pixel is correct colour
                    if (178 > red && red > 138 && 230 > green && green > 154 && 72 > blue && blue > 32)
                    {
                        // changing variables
                        breakpoint = true;
                        firstPoint = new Point(j, i);
                        break;
                    }
                
                }
              

            }
            // redeclaring the colours to be used outside the loop
            stride = bi.PixelWidth * 4;
            size = bi.PixelHeight * stride;
            pixels = new byte[size];
            bi.CopyPixels(pixels, stride, 0);

            x = (int) firstPoint.X;
            y = (int) firstPoint.Y;
            index = y * stride + 4 * x;

            
            byte Blue = pixels[index];
            byte Green = pixels[index + 1];
            byte Red = pixels[index + 2];
            byte Alpha = pixels[index + 3];
            
            // going to the top of the block
            while (178 > Red && Red > 138 && 230 > Green && Green > 154 && 72 > Blue && Blue > 32)
            {
                firstPoint.Y += 1;
                x = (int)firstPoint.X;
                y = (int)firstPoint.Y;
                index = y * stride + 4 * x;


                Blue = pixels[index];
                Green = pixels[index + 1];
                Red = pixels[index + 2];
                Alpha = pixels[index + 3];
            }

            // going to left side of the block
            while (178 > Red && Red > 138 && 230 > Green && Green > 154 && 72 > Blue && Blue > 32)
            {
                firstPoint.X -= 1;
                x = (int)firstPoint.X;
                y = (int)firstPoint.Y;
                index = y * stride + 4 * x;


                Blue = pixels[index];
                Green = pixels[index + 1];
                Red = pixels[index + 2];
                Alpha = pixels[index + 3];
            }

            // remembering the values we just got for top and left of block
            int left = (int)firstPoint.X;
            int top = (int)firstPoint.Y;

            // going to right side of block
            while (178 > Red && Red > 138 && 230 > Green && Green > 154 && 72 > Blue && Blue > 32)
            {
                firstPoint.X += 1;
                x = (int)firstPoint.X;
                y = (int)firstPoint.Y;
                index = y * stride + 4 * x;


                Blue = pixels[index];
                Green = pixels[index + 1];
                Red = pixels[index + 2];
                Alpha = pixels[index + 3];
            }

            // going to bottom of block
            while (178 > Red && Red > 138 && 230 > Green && Green > 154 && 72 > Blue && Blue > 32)
            {
                firstPoint.Y -= 1;
                x = (int)firstPoint.X;
                y = (int)firstPoint.Y;
                index = y * stride + 4 * x;


                Blue = pixels[index];
                Green = pixels[index + 1];
                Red = pixels[index + 2];
                Alpha = pixels[index + 3];
            }

            // remembering values for right and bottom of block
            int bottom = (int)firstPoint.Y;
            int right = (int)firstPoint.X;

            int length = bottom - top;
            int width = right - left;


            //drawing rectangle where the cube is 
            Rectangle r = new Rectangle();
            r.Stroke = System.Windows.Media.Brushes.Black;
            r.Width = width;
            r.Height = length;
            r.StrokeThickness = 2;

            canvas.Children.Add(r);
            Canvas.SetLeft(r, firstPoint.X);
            Canvas.SetTop(r, firstPoint.Y);


        }
    }
}
