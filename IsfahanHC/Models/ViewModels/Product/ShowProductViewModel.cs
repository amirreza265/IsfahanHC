using IsfahanHC.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IsfahanHC.Models.ViewModels
{
    public class ShowProductViewModel
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string StorName { get; set; }

        public decimal Price { get; set; }
        public int QuantityInStock { get; set; }
        public IEnumerable<string> ImagesFileName { get; set; }
    }
}
