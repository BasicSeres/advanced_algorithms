using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace dbscan_gocpontok
{
    class Program
    {
        static void Main(string[] args)
        {
            string file = "20081115010133.plt";
            //string file = "20081024020959.plt";
            List<Coordinate> points = new List<Coordinate>();
            FileHandler.ReadData(points,file);
            ;
            Console.WriteLine(points.Count);
            //Logic.DBScan(points,5,0.001f);
            //Logic.DBScan(points,5,0.0009f); ezzel lett a harom kimenet


            //decimal s = 0.0003m;
            //Logic.DBScan(points,4,s);


            decimal s = 0.0005m;
            Logic.DBScan(points, 4, s);
            // FileHandler.WriteData(points, "outputasd.plt");
            //Kiirjuk a képet
            Console.WriteLine("Done");
            Console.ReadLine();
        }
    }
}
