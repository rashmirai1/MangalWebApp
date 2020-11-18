using MangalWeb.Model.Entity;
using MangalWeb.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangalWeb.Service.Service
{
    public class MessageActionService
    {
        MessageActionRepository _MessageActionRepository = new MessageActionRepository();

        public List<GetMessageAction_Result> GetMessageActions(int userId, int branchId)
        {
            return _MessageActionRepository.GetMessageActions(userId, branchId).Where(m=>m.IsControl==true).ToList();
        }
        public List<GetMessageAction_Result> GetTop5MessageActions(int userId, int branchId)
        {
            return _MessageActionRepository.GetMessageActions(userId, branchId).Where(m => m.IsControl == true).Take(5).ToList();
        }
        public List<GetMessageAction_Result> GetMessageNotificatins(int userId, int branchId)
        {
            return _MessageActionRepository.GetMessageActions(userId, branchId).Where(m => m.IsControl == false).ToList();
        }
        public List<GetMessageAction_Result> GeTop5tMessageNotificatins(int userId, int branchId)
        {
            return _MessageActionRepository.GetMessageActions(userId, branchId).Where(m => m.IsControl == false).Take(5).ToList();
        }
        public MessageActionUser UpdateMessageAction(int messageActionUserId)
        {
            var dbRecord = _MessageActionRepository.UpdateMessageAction(messageActionUserId);
            return dbRecord;

        }
    }
}
