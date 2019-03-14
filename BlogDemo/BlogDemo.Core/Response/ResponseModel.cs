using System;
using System.Collections.Generic;
using System.Text;

namespace BlogDemo.Core.Response
{
    public class ResponseModel
    {
        public int Code { get; set; }
        public string Reslut { get; set; }
        public dynamic Data { get; set; }
    }
}
