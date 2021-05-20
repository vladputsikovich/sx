using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sx.Models
{
    public class Order
    {
        public int Id { get; set; }
        public List<Goods> Goods { get; set; } = new List<Goods>();
        public List<Size> Size { get; set; } = new List<Size>();
        public int Price { get; set; }
        public string Buyer { get; set; }
        public DateTime Date { get; set; }
    }
}
