using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kepszegmentalas_kmeans
{
    static class Logic
    {
        static Random rnd = new Random();
        public static int[] hisztogram2 = new int[256];
        //index intenzitas 0-255
        //value sulyozva darab
        public static void GrayScale(ref Bitmap img)
        {
            for (int i = 0; i < img.Width; i++)
            {
                for (int j = 0; j < img.Height; j++)
                {
                    Color a = img.GetPixel(i, j);
                    int grayScale = (int)((a.R * 0.3) + (a.G * 0.59) + (a.B * 0.11));
                    Color newColor = Color.FromArgb(a.A, grayScale, grayScale, grayScale);
                    img.SetPixel(i, j, newColor);
                    hisztogram2[grayScale]++;          

                }
            }
        }


        

        public static void KMeans(Bitmap img, int n_cluster,ref Bitmap output)
        {
            /*
            1. Inicializalas, kezdo centroidok (klaszterek ”kozeppontjanak) megadasa (tipikusan veletlenszeruen).
            2. Elemek besorolasa a klaszterekbe a tavolsagi adatok alapjan (minden
            elem a hozza legkozelebbi centroid klaszterebe kerul).
            3. Ez alapjan klaszterek kozeppontjanak modosıtasa (minden centroid uj
            pozıcioja a hozza tartozo elemek pozıcioinak atlaga lesz).
            4. Amennyiben valtoztak a kozeppontok, akkor megismeteljuk a muveletet.
            5. Amennyiben nem valtoztak a kozeppontok, kilep az eljaras.
            */
            List<int>[] clusters = new List<int>[n_cluster];
            int[,] clusters2 = new int[n_cluster, 256];
            int[,] memory = new int[n_cluster, 1];
            int run_num = 0;
            bool break_con = false;
            bool valtozott = false;

            //Centroidok random kijelölése
            for (int i = 0; i < n_cluster; i++)
            {
                clusters2[i, 0] = rnd.Next(0, hisztogram2.Length);

            }
            do
            {
                //Minden elem kijelölése a legközelebbi klaszterbe
                for (int i = 0; i < hisztogram2.Length; i++)
                {
                    //int min = hisztogram2[i]*CalculateDistance(i, clusters[0][0]);
                    int min = int.MaxValue;

                    int index = 0;
                    for (int j = 0; j < n_cluster; j++)
                    {
                        int c = hisztogram2[i] * CalculateDistance(i, clusters2[j, 0]);
                        if (min > c)
                        {
                            min = c;
                            index = j;
                        }

                    }
                    if (i != 0)
                        clusters2[index, i] = min;
                }
                ;
                //Centroidok ujraszamitasa
                for (int i = 0; i < n_cluster; i++)
                {
                    int new_value = 0;
                    int darab = 0;
                    int old_v = clusters2[i, 0];
                    Console.WriteLine("OldV: " + old_v);
                    for (int j = 1; j < clusters2.GetLength(1) - 1; j++)
                    {
                        if (clusters2[i, j] != 0)
                        {
                            new_value += j * clusters2[i, j];
                            darab += clusters2[i, j];
                        }

                    }
                    int new_v = darab == 0? old_v : (int)(new_value / darab);//talál e másik értéket

                    clusters2[i, 0] =new_v;
                    Console.WriteLine(" NewV: " + new_v);
                    if (old_v != new_v && Math.Abs(old_v-new_v)>1)//valtozas checket nezunk
                    {
                        valtozott = true;
                    }
                    if (run_num % 2 == 0)//megnezzuk hogy elkezdene e oszcillalni
                    {
                        if (memory[i,0] != old_v)
                        {
                            memory[i, 0] = old_v;
                        }
                        else
                        {
                            break_con = true;
                        }
                    }
                }
                if (!break_con)//Ha nem utolso run akkor nullazza ki a dolgokat
                {
                    for (int i = 0; i < n_cluster; i++)
                    {
                        for (int j = 1; j < 256; j++)
                        {
                            clusters2[i, j] = 0;
                        }
                    }
                }
                run_num++;
                
            }
            while (valtozott&&!break_con);
            ;
            //Klaszterek kiirasa
            for (int i = 0; i < output.Width; i++)
            {
                for (int j = 0; j < output.Height; j++)
                {
                    int s = img.GetPixel(i, j).R;
                    for (int k = 0; k < n_cluster; k++)
                    {
                        if (clusters2[k, s]!=0)
                        {
                            int sf = k%3;
                            switch (sf)
                            {
                                case 0:
                                    //output.SetPixel(i, j, Color.FromArgb(0, (255 / n_cluster) * k, (255 / n_cluster) * k));
                                    output.SetPixel(i, j, Color.FromArgb(0, 0, (255 / n_cluster) * k));
                                    //output.SetPixel(i, j, Color.FromArgb(0, 0, 255));
                                    break;
                                case 1:
                                    output.SetPixel(i, j, Color.FromArgb((255 / n_cluster) * k, 0, 0));
                                    //output.SetPixel(i, j, Color.FromArgb(255 * k, 0, 0));
                                    //output.SetPixel(i, j, Color.FromArgb((255 / n_cluster) * k, 0, (255 / n_cluster) * k));
                                    break;
                                case 2:
                                    output.SetPixel(i, j, Color.FromArgb(0, (255 / n_cluster) * k,0));
                                    //output.SetPixel(i, j, Color.FromArgb(0, 255,0));

                                    break;
                            }
                        }
                    }
                }
            }
        }

        public static int CalculateDistance(int a, int b)//Tavolsag kiszamitas
        {
            return Math.Abs(b - a);
        }

    }
}
