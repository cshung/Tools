namespace Secret
{
    using System;
    using System.Drawing;

    internal static class Program
    {
        private static void Main(string[] args)
        {
            if (args.Length != 3)
            {
                Usage();
                return;
            }

            bool encode;
            if (args[0].Equals("--encode"))
            {
                encode = true;
            }
            else if (args[0].Equals("--decode"))
            {
                encode = false;
            }
            else
            {
                Usage();
                return;
            }

            string input = args[1];
            string output = args[2];
            try
            {
                Color? one = null;
                Color? two = null;
                Bitmap image1 = new Bitmap(input, true);
                int x, y;
                for (x = 0; x < image1.Width; x++)
                {
                    for (y = 0; y < image1.Height; y++)
                    {
                        Color pixelColor = image1.GetPixel(x, y);
                        if (one == null)
                        {
                            one = pixelColor;
                        }
                        else if (pixelColor.Equals(one))
                        {
                        }
                        else if (two == null)
                        {
                            two = pixelColor;
                        }
                        else if (pixelColor.Equals(two))
                        {
                        }
                        else if (!two.Equals(pixelColor))
                        {
                            Console.WriteLine(one);
                            Console.WriteLine(two);
                            Console.WriteLine(pixelColor);
                            throw new Exception("Input bitmap has more than two colors");
                        }
                    }
                }

                Color mappedOne = encode ? Color.FromArgb(248, 248, 248) : Color.FromArgb(255, 255, 255);
                Color mappedTwo = encode ? Color.FromArgb(248, 248, 249) : Color.FromArgb(0, 0, 0);
                for (x = 0; x < image1.Width; x++)
                {
                    for (y = 0; y < image1.Height; y++)
                    {
                        Color pixelColor = image1.GetPixel(x, y);
                        if (pixelColor == one)
                        {
                            image1.SetPixel(x, y, mappedOne);
                        }
                        else
                        {
                            image1.SetPixel(x, y, mappedTwo);
                        }
                    }
                }

                image1.Save(output);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private static void Usage()
        {
            Console.WriteLine("Usage: Secret [--encode|--decode] input.png output.png");
        }
    }
}
