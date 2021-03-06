using sx.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace sx.ViewModels
{
    public class NewGoodsModel
    {
        [Required(ErrorMessage = "Не указано имя")]
        public string ShortName { get; set; }
        public string Description { get; set; }
        public string UrlPhoto { get; set; }

        [DataType(DataType.Date)]
        public DateTime DatePublish { get; set; }
        public int IdSeller { get; set; }
        [DataType(DataType.Text)]
        public int Price { get; set; }
        public List<Size> Size { get; set; }
        public Category Category { get; set; }
    }
}
