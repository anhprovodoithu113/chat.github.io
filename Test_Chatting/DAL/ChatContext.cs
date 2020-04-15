using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using Test_Chatting.Models;

namespace Test_Chatting.DAL
{
    public class ChatContext : DbContext
    {
        public ChatContext() : base("ChatContext")
        {
            
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<ChatBox> ChatBoxes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}