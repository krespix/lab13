using Laba11;

namespace Laba12
{
    public class Point
    {
        public Point(Goods data)
        {
            Data = data;
        }
           
        public Goods Data { get; set; }
        public Point Next { get; set; }
    }
}