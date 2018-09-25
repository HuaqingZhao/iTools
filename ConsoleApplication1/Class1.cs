using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class Class1
    {
        private int index;
        private bool isSuccess;
        private IDictionary<string, string> Dics;
        private static IList<Tuple<ushort[]>> _imageFrames;

        public Class1()
        {
            var f = Math.Abs(0 * 0 * 0 * 0 * 0) > 0.101;
            var image = new ushort[100];
            var image1 = CopyImage(image, 5);
            var image2 = CopyImage(image, 10);
            var image3 = CopyImage(image, 20);

            _imageFrames = new List<Tuple<ushort[]>>();
            _imageFrames.Add(new Tuple<ushort[]>(image1));
            _imageFrames.Add(new Tuple<ushort[]>(image2));
            _imageFrames.Add(new Tuple<ushort[]>(image3));
        }

        /// <summary>
        /// Copy image
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        private ushort[] CopyImage(ushort[] image, int j)
        {
            var result = new ushort[image.Length - j];

            //copy current image to _lastOffsetFrameImage
            for (var i = 0; i < image.Length - j; i++)
            {
                result[i] = image[i];
            }

            return result;
        }
    }
}
