using MangalWeb.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangalWeb.Repository.Repository
{
    public class MessageActionRepository
    {
        MangalDBNewEntities _context = new MangalDBNewEntities();

        public  IEnumerable<GetMessageAction_Result> GetMessageActions(int userId, int branchId)
        {
            return _context.GetMessageAction(userId, branchId);
            
        }
        public MessageActionUser UpdateMessageAction(int messageActionUserId)
        {
            var dbRecord = _context.MessageActionUsers.Where(u => u.MessageActionUserID == messageActionUserId).FirstOrDefault();
            if(dbRecord!=null)
            {
                dbRecord.IsRead = true;
                _context.SaveChanges();
            }
            return dbRecord;
        }
        public int AddMessageAction(int? messageActionID, string message, string remarks, string pageUrl, int? userCategoryID, bool? isControl, int? createdBy)
        {
            System.Data.Entity.Core.Objects.ObjectParameter output = new System.Data.Entity.Core.Objects.ObjectParameter("OutputMessageActionID", typeof(int));
            _context.AddMessageAction(messageActionID, message, remarks, pageUrl, userCategoryID, isControl, createdBy, output);

            messageActionID = (int)output.Value;

            return messageActionID ?? 0;
        }

    }
}
