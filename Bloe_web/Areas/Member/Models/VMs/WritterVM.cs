using Blog_model.Models.Concrete;
using System;
using System.Collections.Generic;

namespace Bloe_web.Areas.Member.Models.VMs
{
    public class WritterVM
    {
        public int UserID { get; set; }
        public string UserFUllName { get; set; }
        public string UserImage { get; set; }
        public DateTime UserCreatedDate { get; set; }

        public string AritcleCount { get; set; }


    }
}
