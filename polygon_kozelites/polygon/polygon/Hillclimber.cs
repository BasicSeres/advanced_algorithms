using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace polygon
{
    class Hillclimber
    {
        static Random rnd = new Random();
        static float CheckDistance(Point p1, Point p2,Point p)
        {
            return Convert.ToSingle(((p2.Y-p1.Y)*p.X-(p2.X-p1.X)*p.Y+p2.X*p1.Y-p2.Y*p1.X)/Math.Sqrt(Math.Pow(p2.Y-p1.Y,2)+ Math.Pow(p2.X - p1.X, 2)));
        }

        //Minden egyes pontra kiszamolja milyen messze van a polygontol 
        static float OuterDistanceToBoundary(List<Point> solution,List<Point> points)
        {
            float sum_min_dist = 0;
            for (int i = 0; i < points.Count; i++)
            {
                float min_dist = float.MaxValue;
                for (int j = 0; j < solution.Count; j++)
                {
                    float act_dist = CheckDistance(solution[j], solution[(j + 1) % solution.Count], points[i]);
                    if (j == 0 || act_dist < min_dist)
                        min_dist = act_dist;

                }
                if (min_dist < 0)//ha negativ akkor a baloldalan van
                    sum_min_dist += -min_dist;

            }

            return sum_min_dist;
        }
        //Meghatarozza a polygon oldalanak a hosszat
        static float LengthOfBoundary(List<Point> solution)
        {
            float sum = 0; 
            for (int i = 0; i < solution.Count; i++)
            {
                Point p1 = solution[i];
                Point p2 = solution[(i+1)%solution.Count];
                sum += Convert.ToSingle(Math.Sqrt(Math.Pow(p1.X - p2.X,2) + Math.Pow(p1.Y - p2.Y, 2)));

            }
            return sum;
        }
        public  static List<Point> HillClimber(List<Point> points,int p_count)
        {
            float last_fitness = -1;
            float stop_fitness= -1;
            int stop_count = 100000;
            int penalty = 500;
            List<Point> p = GenerateRandomPoints(points,p_count);
            
            float p_fitness = fitness(p, points,penalty);
            Console.WriteLine("Initial fitness ",p_fitness);
            int t = 1;
            while (!StopCondition(stop_count,ref t,ref p_fitness,ref last_fitness,stop_fitness,0.1f))
            {
                List<Point> q = GenerateRandomChange(p,1);
                float q_fitness = fitness(q,points,penalty);

               Console.WriteLine("Fitness {0}      {1}     {2}", q_fitness,p_fitness,t);
                if (q_fitness<p_fitness)
                {
                    p = q;
                    p_fitness = q_fitness;


                }
                t++;
            }
            
            Console.WriteLine("DONE SEARCHING ");
            float outer_d = OuterDistanceToBoundary(p, points);
            if (outer_d > 0)
            {
                Console.WriteLine("{0}:-> Nem jo megoldas",outer_d);
            }
            else
            {
                Console.WriteLine("{0}:-> Jo megoldas", outer_d);
            }
            Console.WriteLine();
            return p;
        }

        private static bool StopCondition(int stop_count_max,ref int stop_count, ref float p_fitness,ref float last_fitness, float stop_fitness,float stop_fitness_diff)
        {
            if (last_fitness!=-1&&last_fitness-p_fitness<stop_fitness_diff)
            {
                stop_count++;
            }
            else
            {
                stop_count = 0;
            }
            last_fitness = p_fitness;
            return stop_count > stop_count_max;
        }

        //random valtoztassuk meg az egyik pontot
        private static List<Point> GenerateRandomChange(List<Point> solution,float e)
        {
            List<Point> list =  new List<Point>();
            for (int i = 0; i < solution.Count; i++)
            {
                Point f = new Point(solution[i].X, solution[i].Y);
                list.Add(f);
            }
            for (int i = 0; i < list.Count; i++)
            {
                switch (rnd.Next(0, 4))
                {
                    case 0:
                        list[i].X -= e;
                        break;
                    case 1:
                        list[i].Y -= e;
                        break;
                    case 2:
                        list[i].X += e;
                        break;
                    case 3:
                        list[i].Y += e;
                        break;
                    default:
                        break;
                }
            }
            return list;
        }

        private static float fitness(List<Point> solution,List<Point> points,int penalty)
        {
            
            return LengthOfBoundary(solution) + OuterDistanceToBoundary(solution,points)*penalty;
      
        }
        //egy nagy körben hozzon létre megoldásokat
        private static List<Point> GenerateRandomPoints(List<Point> t_points,int p)
        {
            Point o = new Point(t_points.Average(c => (c.X) / 2), t_points.Average(c => (c.Y) / 2));
            float r = t_points.Max(c=> Math.Abs((o.X-c.X)+(o.Y-c.Y)))+10;
            int x = (int)o.X;
            int y = (int)o.Y;
            List<Point> points = new List<Point>();
            Console.WriteLine("START RANDOM GENERATE");
            for (int i = 0; i < p; i++)
            {
                float y_tmp = 0;
                int x_r = 0;

                do
                {
                    x_r = rnd.Next((int)-r, (int)r + 1);
                    if (rnd.Next(0, 2) == 1)
                    {
                        y_tmp = y + Convert.ToSingle(Math.Sqrt(Math.Pow(r, 2) - Math.Pow(x - x_r, 2)));

                    }
                    else
                    {
                        y_tmp = y - Convert.ToSingle(Math.Sqrt(Math.Pow(r, 2) - Math.Pow(x - x_r, 2)));

                    }
                } while (float.IsNaN( y_tmp) || float.IsInfinity(y_tmp));

                points.Add(new Point(x_r, y_tmp));
            }
            Console.WriteLine("DONE WITH RANDOM GENERATE");
            return points;
        }
    }
}
