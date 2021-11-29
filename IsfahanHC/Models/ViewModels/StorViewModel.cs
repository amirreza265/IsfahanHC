using IsfahanHC.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IsfahanHC.Models.ViewModels
{
    public class StorViewModel
    {
        public int StorId { get; set; } 
        public string Name { get; set; }

        public int SellersCount { get; set; }
        public int ProductsCount { get; set; }
        public string ImageFileName { get; set; }
    }
}
