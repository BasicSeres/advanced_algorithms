using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dbscan_gocpontok
{
    static class FileHandler
    {
        public static void ReadData(List<Coordinate> points,string path)
        {
            using (FileStream fs = new FileStream(path,FileMode.Open,FileAccess.Read))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    int key = 0;
                    while (!sr.EndOfStream)
                    {
                        
                        string[] data = sr.ReadLine().Split(',');
                        if (data.Length==7)
                        {
                            //Coordinate tmp = new Coordinate(float.Parse(data[0], CultureInfo.InvariantCulture), float.Parse(data[1], CultureInfo.InvariantCulture),key);
                            Coordinate tmp = new Coordinate(decimal.Parse(data[0], CultureInfo.InvariantCulture), decimal.Parse(data[1], CultureInfo.InvariantCulture),key);
                            points.Add(tmp);
                            key++;
                        }
                    }
                }
            }
        }
        static public void WriteData(List<Coordinate> points,string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    foreach (var item in points)
                    {

                        sw.WriteLine($"{item.x.ToString(CultureInfo.InvariantCulture)},{item.y.ToString(CultureInfo.InvariantCulture)}");

                    }

                }
            }
        }
    }
}
