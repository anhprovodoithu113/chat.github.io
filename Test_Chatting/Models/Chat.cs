using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Test_Chatting.Models
{
    public class Chat
    {
        public int ChatId { get; set; }
        public string Content { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string PathImage { get; set; }

        public virtual Account Account { get; set; }
        public virtual ChatBox ChatBox { get; set; }
    }
}