using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sx.Models
{
    public class Goods
    {
        public int Id { get; set; }
        public string ShortName { get; set; }
        public string Description { get; set; }
        public string UrlPhoto { get; set; }
        public DateTime DatePublic { get; set; }
        public int IdSeller { get; set; }
        public int Price { get; set; }
        public List<Size> Size { get; set; } = new List<Size>();
        public Category Category { get; set; }
    }
}
