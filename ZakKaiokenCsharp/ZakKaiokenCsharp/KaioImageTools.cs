using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Kaiosharp
{
    public class KaioImageTools : KaioConsole
        
    {

        public static Bitmap GenerateGradient(Color color1, Color color2, int width = 64, int height = 64)
        {
            Bitmap bmp = new Bitmap(width, height);
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    float flope = remapValue(x, 0, width, 0, 1);
                    Color Blended = ColorLerp(color1, color2, flope);
                    bmp.SetPixel(x, y, Blended);
                }
            }

            return bmp;

        }

        public static Bitmap GenerateCompareGradient16(Bitmap bm)
        {
            Bitmap bmp = bm;
            for (int r = 0;r<16;r++)
            {
                Bitmap redmap = bmp;
                bmp = GenerateCompareGradient(redmap);
            }
            return bmp;
        }

        public static Bitmap GenerateCompareGradient(Bitmap bm)
        {
            Bitmap bmp = new Bitmap(bm.Width, bm.Height);
            for (int y = 0; y < bm.Height; y++)
            {

                for (int x = 0; x < bm.Width; x+=2)
                {
                    if (x + 1 <= bm.Width)
                    {
                        Color c1 = bm.GetPixel(x, y);
                        Color c2 = bm.GetPixel(x + 1, y);

                        float flope = remapValue(x, 0, bm.Width * 2, 0, 1);
                        Color Blended = ColorLerp(c1, c2, flope);

                        bmp.SetPixel(x, y, Blended);
                        bmp.SetPixel(x + 1, y, Blended);
                    }
                }
            }


            return bmp;
                }
        

        public static Color ColorLerp(Color color1, Color color2, float time)
        {
            float red1 = remapValue(color1.R, 0, 255, 0, 1);
            float green1 = remapValue(color1.G, 0, 255, 0, 1);
            float blue1 = remapValue(color1.B, 0, 255, 0, 1);

            float red2 = remapValue(color2.R, 0, 255, 0, 1);
            float green2 = remapValue(color2.G, 0, 255, 0, 1);
            float blue2 = remapValue(color2.B, 0, 255, 0, 1);

            float rf = Lerp(red1, red2, time);
            float gf = Lerp(green1, green2, time);
            float bf = Lerp(blue1, blue2, time);

            int r = (int)remapValue(rf, 0, 1, 0, 255);
            int g = (int)remapValue(bf, 0, 1, 0, 255);
            int b = (int)remapValue(gf, 0, 1, 0, 255);

            return Color.FromArgb(r, g, b);
        }

        public static Color getAverageColor(Color[] colors)
        {
            //Used for tally
            int r = 0;
            int g = 0;
            int b = 0;

            int total = 0;

            foreach (Color color in colors)
            {
                r += color.R;
                g += color.G;
                b += color.B;

                total++;
            }

            r /= total;
            g /= total;
            b /= total;

            return Color.FromArgb(r, g, b);
        }

        public static Color getAverageColor(Bitmap bmp)//stole this code from some dude on stackoverflow
        {

            //Used for tally
            int r = 0;
            int g = 0;
            int b = 0;


            int total = 0;

            for (int x = 0; x < bmp.Width; x++)
            {
                for (int y = 0; y < bmp.Height; y++)
                {
                    Color clr = bmp.GetPixel(x, y);

                    r += clr.R;
                    g += clr.G;
                    b += clr.B;

                    total++;
                }
            }
            
            //Calculate average
            r /= total;
            g /= total;
            b /= total;

            return Color.FromArgb(r, g, b);
        }

        public static Bitmap CreateImageFromColor(Color color, int width=1, int height=1)
        {
            Bitmap bmp = new Bitmap(width, height);

            for (int x = 0; x < bmp.Width; x++)
            {
                for (int y = 0; y < bmp.Height; y++)
                {
                    bmp.SetPixel(x, y, color);
                }
            }
            return bmp; 
        }

        public static Bitmap CreateImageFromColormap(int width, int height, Color[,] color)
        {
            Bitmap bmp = new Bitmap(width, height);

            for (int x = 0; x < bmp.Width; x++)
            {
                for (int y = 0; y < bmp.Height; y++)
                {
                    bmp.SetPixel(x, y, color[x,y]);
                }
            }
            return bmp;
        }

        public static Color[,] CalculateSimilarity(Color[] colors)
        {
            Color[,] averages = new Color[colors.Length, colors.Length];
            for (int c = 0; c < colors.Length; c++)
            {
                for (int b = 0; b < colors.Length; b++)
                {
                    int red = colors[c].R - colors[b].R;
                    int green = colors[c].G - colors[b].G;
                    int blue = colors[c].B - colors[b].B;
                    int redv2 = (int)remapValue(red, -255, 255, 0, 255);
                    int greenv2 = (int)remapValue(green, -255, 255, 0, 255);
                    int bluev2 = (int)remapValue(blue, -255, 255, 0, 255);

                    averages[c,b] = Color.FromArgb(redv2, greenv2, bluev2);
                }
            }
            return averages;
        }

        

    }
}
