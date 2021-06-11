using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace polygon
{
    static class FileManagement
    {
       public static void ReadFiles(List<Point> list,string path)
        {
            using (FileStream fs = new FileStream(path,FileMode.Open,FileAccess.Read))
            {
                using(StreamReader sr = new StreamReader(fs))
                {
                    while (!sr.EndOfStream)
                    {
                        string[] str = sr.ReadLine().Split(';');
                        Point tmp = new Point(float.Parse(str[0]),float.Parse(str[1]));
                        list.Add(tmp);
                    }
                }
            }

        }
        public static void WriteFile(List<Point> list_p,string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.ReadWrite))
            {
                using (StreamWriter sr = new StreamWriter(fs))
                {
                    foreach (var item in list_p)
                    {
                        sr.WriteLine(item.X+";"+item.Y);

                    }
                }
            }

        }
    }
}
