using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows;

namespace Screen_Capture
{
    class Program
    {
        private static Bitmap myBitmap;
        private static List<int> rValues;
        private static List<int> gValues;
        private static List<int> bValues;

        static void CaptureMyScreen()
        {
            const int WIDTH_X = 1920;
            const int HEIGHT_Y = 1080;

            try
            {
                // Create a new Bitmap object
                //Bitmap captureBitmap = new Bitmap(WIDTH_X, HEIGHT_Y, PixelFormat.Format32bppArgb);
                myBitmap = new Bitmap(WIDTH_X, HEIGHT_Y, PixelFormat.Format32bppArgb);

                // Create a Rectangle object which will capture our Current Screen
                Rectangle captureRectangle = new Rectangle(0, 0, WIDTH_X, HEIGHT_Y);
                // Rectangle captureRectangle = Screen.AllScreens[0].Bounds;    // In Windows.Forms

                // Creating a New Graphics Object
                //Graphics captureGraphics = Graphics.FromImage(captureBitmap);
                Graphics captureGraphics = Graphics.FromImage(myBitmap);

                // Copying Image from The Screen
                captureGraphics.CopyFromScreen(captureRectangle.Left, captureRectangle.Top, 0, 0, captureRectangle.Size);

                //Saving the Image File (I am here Saving it in My E drive).
                //captureBitmap.Save(@"D:\Capture.jpg", ImageFormat.Jpeg);
                myBitmap.Save(@"D:\Capture.jpg", ImageFormat.Jpeg);

                //Displaying the Successfull Result 
                Console.WriteLine("Image captured");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //MessageBox.Show(ex.Message);
            }
        }

        static void ConvertToGreyScale()
        {
            // attempt to read image
            try
            {
                //Bitmap bmp = new Bitmap(@"D:\Capture.jpg");
                Bitmap bmp = myBitmap;

                // get image dimension
                int width = bmp.Width;
                int height = bmp.Height;

                // color of pixel
                Color p;

                // grayscale
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        //get pixel value
                        p = bmp.GetPixel(x, y);

                        //extract pixel component ARGB
                        int a = p.A;
                        int r = p.R;
                        int g = p.G;
                        int b = p.B;

                        //find average
                        int avg = (r + g + b) / 3;

                        //set new pixel value
                        bmp.SetPixel(x, y, Color.FromArgb(a, avg, avg, avg));
                    }
                }

                //write the grayscale image
                bmp.Save(@"D:\Grayscale.jpg", ImageFormat.Jpeg);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static void GetAverageRGB()
        {
            rValues = new List<int>();
            gValues = new List<int>();
            bValues = new List<int>();

            //Bitmap bmp = new Bitmap(@"D:\Capture.jpg");
            Bitmap bmp = myBitmap;

            // get image dimension
            int width = bmp.Width;
            int height = bmp.Height;

            Console.WriteLine(width + " x " + height);

            // color of pixel
            Color p;

            // grayscale
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    //get pixel value
                    p = bmp.GetPixel(x, y);

                    //extract pixel component ARGB
                    int a = p.A;
                    int r = p.R;
                    int g = p.G;
                    int b = p.B;

                    // Store individual r, g, b values in respective lists
                    rValues.Add(r);
                    gValues.Add(g);
                    bValues.Add(b);
                }
            }

            Console.WriteLine("\nTotal r values: " + rValues.Count);
            Console.WriteLine("Total g values: " + gValues.Count);
            Console.WriteLine("Total b values: " + bValues.Count);

            // get average pixel values
            Console.WriteLine("\nAverage r values: " + rValues.Average());
            Console.WriteLine("\nAverage g values: " + gValues.Average());
            Console.WriteLine("\nAverage b values: " + bValues.Average());
        }

        static void OutputAverageRGBImage()
        {
            const int WIDTH_X = 1920;
            const int HEIGHT_Y = 1080;
            const int A = 255;
            
            // Set average R, G, B values
            int averageR = (int)Math.Floor((decimal)rValues.Average());
            int averageG = (int)Math.Floor((decimal)rValues.Average());
            int averageB = (int)Math.Floor((decimal)rValues.Average());

            try
            {
                // Create a new Bitmap object
                Bitmap outputBitmap = new Bitmap(WIDTH_X, HEIGHT_Y, PixelFormat.Format32bppArgb);

                // Apply average R, G, B value to each pixel in turn
                for (int y = 0; y < HEIGHT_Y; y++)
                {
                    for (int x = 0; x < WIDTH_X; x++)
                    {
                        // set pixel value
                        outputBitmap.SetPixel(x, y, Color.FromArgb(A, averageR, averageG, averageB));
                    }
                }

                // Write the ouput image to file
                outputBitmap.Save(@"D:\AverageCapture.jpg", ImageFormat.Jpeg);

                Console.WriteLine("\nScreen capture: averaged Image ready...");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static void OpenBMP()
        {
            try
            {
                myBitmap = new Bitmap(@"D:\Moulton.jpg");

                myBitmap.Save(@"D:\test.jpg", ImageFormat.Jpeg);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
        }

        static void Main(string[] args)
        {
            //CaptureMyScreen();
            //ConvertToGreyScale();
            OpenBMP();
            GetAverageRGB();
            OutputAverageRGBImage();

            // Wait for key press to close
            Console.ReadKey();
        }
    }
}
