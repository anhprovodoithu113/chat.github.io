using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Test_Chatting.DAL;
using Test_Chatting.Models;

namespace Test_Chatting.Controllers
{
    public class HomeController : Controller
    {
        ChatContext db = new ChatContext();
        public ActionResult Index()
        {
            List<Account> lstAccount = db.Accounts.Where(a => a.AccountId != 2).ToList();
            return View(lstAccount);
        }

        [HttpGet]
        public JsonResult ListChat(string friendId)
        {
            Account acc = db.Accounts.SingleOrDefault(m => m.AccountId == 2);
            int myAccountId = int.Parse(friendId);
            ChatBox friendChatBox = db.ChatBoxes.FirstOrDefault(t => t.Account.AccountId == myAccountId);
            var myListChat = (from c in db.Chats
                              where c.ChatBox.ChatBoxId == friendChatBox.ChatBoxId
                              select new
                              {
                                  chatContent = c.Content,
                                  createdDate = c.CreatedDate,
                                  accountId = c.Account.AccountId,
                                  accountFriendName = c.Account.Username,
                                  accountName = acc.Username
                              }).ToList();

            return Json(myListChat, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult SaveChat(string accountId, string chatContent)
        {
            try
            {
                int myAccountId = int.Parse(accountId);
                ChatBox chatBox = db.ChatBoxes.FirstOrDefault(c => c.Account.AccountId == myAccountId);
                Chat newChat = new Chat()
                {
                    Account = db.Accounts.FirstOrDefault(m => m.AccountId == 2),
                    ChatBox = chatBox,
                    Content = chatContent,
                    CreatedDate = DateTime.Now
                };
                db.Chats.Add(newChat);
                db.SaveChanges();
            }
            catch
            {
                ModelState.AddModelError("", "This message cannot save.");
            }
            return null;
        }
    }
}