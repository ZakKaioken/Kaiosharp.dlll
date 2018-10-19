using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaiosharp
{
    public class AspectRatio : KaioConsole
    {
        public float width;
        public float height;
        public float ratio;

        public AspectRatio(float Width, float Height)
        {
            width = Width;
            height = Height;
            ratio = width / height;
        }

        public AspectRatio(string Width, string Height)
        {
            float with;
            float hith;
            if (float.TryParse(Width, out with))
            {
                if (float.TryParse(Height, out hith))
                {
                    ratio = with / hith;
                }
            }
        }

        #region AspectRatios

        //tell the list of aspect ratios given
        public static void Tell(List<AspectRatio> Lar)
        {
            for (int ar = 0; ar < Lar.Count; ar++)
            {
                Tell("[" + ar + "] " + "Aspect Ratio: " + Lar[ar].width + "/" + Lar[ar].height + " (" + Lar[ar].ratio + ").");
            }
        }
        //generates a list of resolutions using one example of the aspect ratio
        public static List<AspectRatio> Resolutions = new List<AspectRatio>();
        public static List<AspectRatio> GatherNotableResolutionsForAspectRatio(AspectRatio aspectRatio, int maxres = 4097)
        {

            for (int w = 0; w < maxres; w++)
            {
                for (int h = 0; h < maxres; h++)
                {
                    AspectRatio newar = new AspectRatio(w, h);
                    if (newar.ratio == aspectRatio.ratio)
                    {
                        Resolutions.Add(newar);
                    }
                }
            }
            return Resolutions;
        }

        public static List<AspectRatio> Resolutionsmult = new List<AspectRatio>();
        public static List<AspectRatio> GatherNotableResolutionsForAspectRatioDivis(List<AspectRatio> Resolutionsx, int maxres, int div = 0)
        {

            if (!(div > 0))
            {
                if (Resolutionsx.Count > 0)
                {
                    foreach (AspectRatio res in Resolutionsx)
                    {
                        if (IsDivisible(res.width, Resolutionsx[0].width))
                        {
                            if (IsDivisible(res.height, Resolutionsx[0].height))
                            {
                                Resolutionsmult.Add(res);
                            }
                        }
                    }
                }
            }
            else
            {
                foreach (AspectRatio res in Resolutionsx)
                {
                    if (IsDivisible(res.width, div))
                    {
                        if (IsDivisible(res.height, div))
                        {
                            Resolutionsmult.Add(res);
                        }
                    }
                }
            }
            return Resolutionsmult;
        }
        #endregion

    }
}
