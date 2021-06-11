using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dbscan_gocpontok
{
    class Coordinate
    {
        public Coordinate(decimal x, decimal y,int key)
        {
            this.x = x;
            this.y = y;
            this.key = key;
        }
        public int key{ get; set; }
        public decimal x { get; set; }
        public decimal y { get; set; }
    }
}
