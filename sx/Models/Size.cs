using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sx.Models
{
    public class Size
    {
        public int Id { get; set; }
        public string SizeName { get; set; }
        public int Count { get; set; }
    }
}
