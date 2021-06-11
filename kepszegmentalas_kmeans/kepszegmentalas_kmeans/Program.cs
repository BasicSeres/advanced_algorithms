using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageProcessor;
using System.Drawing;
using System.Drawing.Imaging;

namespace kepszegmentalas_kmeans
{
    class Program
    {
        static void Main(string[] args)
        {
            string file = "bemenet2.jpg";
            string output = "kimenet3.jpg";
            Image img = Image.FromFile(file);
            Bitmap bitmap = new Bitmap(img);
            Bitmap output_bitmap = new Bitmap(img.Width, img.Height);
            //Meghivjuk a GrayScalet hogy előálljon a hisztogram
            Logic.GrayScale(ref bitmap);
            // Megadjuk a clusterek számát
            int n_cluster = 3;
            //Meghivjuk a kepszegmentalast
            Logic.KMeans(bitmap, n_cluster, ref output_bitmap);
            //Kiirjuk a képet
            output_bitmap.Save(output);
            Console.WriteLine("Done");
            Console.ReadLine();

        }
       
    }
}
