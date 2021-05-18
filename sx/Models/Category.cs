using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sx.Models
{
    [Owned]
    public class Category
    {
        public Section Type { get; set; }
        public string Kind { get; set; }
    }
    public enum Section
    {
        Shoes,
        Clothing,
        Accessories
    }
}
