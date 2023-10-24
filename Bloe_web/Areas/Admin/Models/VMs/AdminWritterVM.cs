using Blog_model.Models.Concrete;
using System;
using System.Collections.Generic;

namespace Bloe_web.Areas.Admin.Models.VMs
{
    public class AdminWritterVM
    {
        public int UserID { get; set; }
        public string UserFUllName { get; set; }
        public string UserImage { get; set; }
        public DateTime UserCreatedDate { get; set; }

        public string AritcleCount { get; set; }


    }
}
