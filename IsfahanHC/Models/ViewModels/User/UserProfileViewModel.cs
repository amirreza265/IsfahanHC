using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IsfahanHC.Models.ViewModels.User
{
    public class UserProfileViewModel
    {
        public UserViewModel User { get; set; }
        public List<OrderViewModel> Orders { get; set; }
    }
}
