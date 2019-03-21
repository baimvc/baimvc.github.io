using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogDemo.Web.Areas.Admin.Query
{
    public class UserLoginQuery
    {
        public string LoginName { get; set; }

        public string LoginPWD { get; set; }
        public string VCode { get; set; }
    }
}
