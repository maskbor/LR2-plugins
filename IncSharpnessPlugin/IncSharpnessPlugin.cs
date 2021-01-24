using IncSharpnessPlugin;
using PluginInterface;
using System.Drawing;

namespace ReversePlugin
{
    [Version(1, 0)]
    public class ReverseTransform : IPlugin
    {
        public string Name
        {
            get
            {
                return "Улучшение чёткости";
            }
        }
        public string Author
        {
            get
            {
                return "Me";
            }
        }
        public void Transform(Bitmap bitmap)
        {
            var matrix = new double[,]
                             {{0, -1, 0},
                              {-1, 5, -1},
                              {0, -1, 0}};


            var w = matrix.GetLength(0);
            var h = matrix.GetLength(1);

            using (var wr = new ImageWrapper(bitmap) { DefaultColor = Color.Silver })
                foreach (var p in wr)
                {
                    double r = 0d, g = 0d, b = 0d;

                    for (int i = 0; i < w; i++)
                        for (int j = 0; j < h; j++)
                        {
                            var pixel = wr[p.X + i - 1, p.Y + j - 1];
                            r += matrix[j, i] * pixel.R;
                            g += matrix[j, i] * pixel.G;
                            b += matrix[j, i] * pixel.B;
                        }
                    wr.SetPixel(p, r, g, b);
                }




        }

    }
}
