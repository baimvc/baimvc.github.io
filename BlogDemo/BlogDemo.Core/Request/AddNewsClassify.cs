using System;
using System.Collections.Generic;
using System.Text;

namespace BlogDemo.Core.Request
{
    public class AddNewsClassify
    {
        public string Name { get; set; }
        public int Sort { get; set; }
        public string Remark { get; set; }
    }
}
