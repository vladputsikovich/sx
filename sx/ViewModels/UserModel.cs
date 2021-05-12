using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using sx.Models;

namespace sx.ViewModels
{
    public class UserModel
    {
        public User user { get; set; }
        public List<Goods> goods { get; set; }
    }
}
