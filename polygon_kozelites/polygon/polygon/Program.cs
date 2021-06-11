using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace polygon
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Point> points = new List<Point>();
            FileManagement.ReadFiles(points, "input.log");
            List<Point> solution = Hillclimber.HillClimber(points, 7);
            FileManagement.WriteFile(solution, "output.log");
            Console.ReadLine();
        }
    }
}
