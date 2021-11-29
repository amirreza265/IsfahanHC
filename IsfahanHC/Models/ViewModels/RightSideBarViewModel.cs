using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IsfahanHC.Models.ViewModels
{
    public class RightSideBarViewModel<T>
    {
        public string ViewName { get; set; }
        public T Model { get; set; }
    }
}
