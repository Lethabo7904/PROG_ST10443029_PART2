using System.IO;
using System;
using System.Drawing;

namespace Cybersecurity_Awareness_Bot_PART1
{
    public class logo_design
    {
        //start of constructor
        public logo_design()
        {
            //get full location of the project
            string full_location = AppDomain.CurrentDomain.BaseDirectory;

            //replacing bin\\Debug\\
            string new_location = full_location.Replace("bin\\Debug\\", "");

            //combine file path
            string fullPath = Path.Combine(new_location, "logo.jpg");

            //creating the Bitmap class
            Bitmap image = new Bitmap(fullPath);
            //then set the size
            image = new Bitmap(image, new Size(100, 120));


            //outer and inner loop
            for (int height = 15; height < image.Height; height++)
            {
                //inner loop
                for (int width = 10; width < image.Width; width++)
                {
                    Color pixelColor = image.GetPixel(width, height);
                    int gray = (pixelColor.R + pixelColor.G + pixelColor.B) / 3;
                    char asciiChar =
                        gray > 200 ? '@' :
                        gray > 50 ? ':' : '.';
                    Console.Write(asciiChar);

                }//end of inner loop
                Console.WriteLine();

            }//end of outer loop



        }//end of constructor
    }//end of class
}//end of namespace