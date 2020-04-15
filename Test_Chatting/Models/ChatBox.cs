using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Test_Chatting.Models
{
    public class ChatBox
    {
        [ForeignKey("Account")]
        public int ChatBoxId { get; set; }
        public string Name { get; set; }
        public virtual Account Account { get; set; }
        public virtual ICollection<Chat> Chats { get; set; }
    }
}