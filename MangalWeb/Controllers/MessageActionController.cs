using MangalWeb.Model.Entity;
using MangalWeb.Models.Security;
using MangalWeb.Service.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MangalWeb.Controllers
{
    public class MessageActionController : Controller
    {
        MessageActionService service = new MessageActionService();
        // GET: MessageAction
        //public ActionResult MessageActionIndex()
        //{
        //    return View();
        //}
        public ActionResult MessageActionIndex(bool? isControl)
        {
            List<GetMessageAction_Result> messageActionList = new List<GetMessageAction_Result>();
            if (isControl??false)
            {
               messageActionList = service.GetMessageActions(UserInfo.UserID, UserInfo.BranchId);
            }
            else
            {
                messageActionList = service.GetMessageNotificatins(UserInfo.UserID, UserInfo.BranchId);
            }
            
            return View(messageActionList);
        }

        public ActionResult UpdateMessageAction(int messageActionUserId)
        {
            var dbRecord = service.UpdateMessageAction(messageActionUserId);

            return Json(dbRecord);
        }
    }
}