using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogWeb.UI
{
    public class MessageServices
    {
        public static Task SendEmailAsync(string email, string subject, string message)
        {
            return Task.FromResult(0);
        }
        public static Task SendSmsAsync(string number, string message)
        {
            return Task.FromResult(0);
        }
    }
}
