using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dbscan_gocpontok
{
    static class Logic
    {
        static Random rnd = new Random();
        public static void DBScan(List<Coordinate> points,int minPts, decimal e)
        {

            List<List<Coordinate>> p_cluster = new List<List<Coordinate>>();
            List<Coordinate> processed_points = new List<Coordinate>();


            foreach (var c_pont in points)//c_pont = p
            {
                if (!processed_points.Contains(c_pont))
                {
                    List<Coordinate> q_points = new List<Coordinate>();//Q
                    //q_points.Add(c_pont);
                    foreach (var szomszed in points)//kivalogatas
                    {
                        decimal dif = CalculateDistance(c_pont,szomszed);
                        if (dif <= e)
                        {
                            q_points.Add(szomszed);
                        }
                    }
                    ;
                    if (q_points.Count >= minPts)
                    {
                        List<Coordinate> tmp_cluster = new List<Coordinate>();//R
                        List<Coordinate> to_add = new List<Coordinate>();
                        bool breakcon = false;
                        do
                        {
                            foreach (var item in q_points)//item = q
                            {
                                if (!processed_points.Contains(item))//megnezi hogy még vannak e clusterben
                                {
                                    processed_points.Add(item);
                                    //Console.WriteLine(item.key);
                                    tmp_cluster.Add(item);
                                    List<Coordinate> d_points = new List<Coordinate>();//D

                                    foreach (var szomszed in points)
                                    {
                                        decimal dif = CalculateDistance(item, szomszed);
                                        if (dif <= e)
                                        {
                                            d_points.Add(szomszed);
                                        }
                                    }
                                    if (d_points.Count >= minPts)
                                    {
                                        foreach (var f in d_points)
                                        {
                                            to_add.Add(f);
                                        }
                                        breakcon = true;
                                    }
                                    else
                                    {
                                        breakcon = false;
                                    }
                                }
                                else
                                {
                                    breakcon = false;
                                }
                                //Console.WriteLine(tmp_cluster.Count);
                            }
                            foreach (var item in to_add)
                            {
                               if(!q_points.Contains(item))
                                    q_points.Add(item);
                            }
                            to_add = new List<Coordinate>();
                        } while (breakcon);
                        p_cluster.Add(tmp_cluster);
                    }

                }

            }
            LegitCheck(p_cluster);
            //foreach (var item in p_cluster[0])
            //{
            //    Console.WriteLine("Key:"+item.key);
            //}
            ;
            int counter = 0;
            foreach (var item in p_cluster)
            {
                FileHandler.WriteData(item, $"output{counter}.plt");
                counter++;
            }
            ;

        }
        public static bool LegitCheck(List<List<Coordinate>> p_cluster)
        {
            bool duplicate = false;
            foreach (var item in p_cluster)
            {
                foreach (var item2 in item)
                {
                    //Console.WriteLine(item2.key);
                    foreach (var item3 in item)
                    {
                        if (item2 != item3 && item2.key == item3.key)
                        {
                            duplicate = true;
                            return true;
                        }
                    }

                }
            }
            return false;
        }
        public static decimal CalculateDistance(Coordinate a, Coordinate b)//Tavolsag kiszamitas
        
        {
            return Convert.ToDecimal(Math.Abs(Math.Sqrt(Math.Pow(decimal.ToDouble(a.x-b.x),2)+Math.Pow(decimal.ToDouble(a.y-b.y),2))));
        }

    }
}
