using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Test_Chatting.Models
{
    public class Account
    {
        [Key]
        public int AccountId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public virtual ICollection<Chat> Chats { get; set; }
        public virtual ChatBox ChatBox { get; set; }
    }
}