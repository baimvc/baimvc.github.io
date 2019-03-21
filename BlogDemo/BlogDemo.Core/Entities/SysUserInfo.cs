using System;
using System.Collections.Generic;
using System.Text;

namespace BlogDemo.Core.Entities
{
    public class SysUserInfo : EntityBase
    {
       
        public string LoginName { get; set; }

        public string LoginPWD { get; set; }

        public string RealName { get; set; }

        public string Telphone { get; set; }

        public string Mobile { get; set; }

        public string Emial { get; set; }

        public string QQ { get; set; }

        public int? Gender { get; set; }

        public int? CompanyID { get; set; }

        public int? DepID { get; set; }

        public int? WorkGroupID { get; set; }

        public string Remark { get; set; }

    }
}
