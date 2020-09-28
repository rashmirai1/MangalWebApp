using MangalWeb.Model.Entity;
using MangalWeb.Model.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangalWeb.Repository.Repository
{
    public class StandardEndTimeRepository
    {
        MangalDBNewEntities _context = new MangalDBNewEntities();

        public void SaveUpdateRecord(StandardEndTime standardEndTime)
        {
            tblStandardEndTime objStandardEndTime = new tblStandardEndTime();
            int id = _context.tblStandardEndTimes.Select(x => x.Id).FirstOrDefault();
            if (id==0)
            {
                objStandardEndTime.StatdardEndTime = standardEndTime.StandadrDateTime;
                _context.tblStandardEndTimes.Add(objStandardEndTime);
            }
            else
            {
                var time = _context.tblStandardEndTimes.SingleOrDefault(x => x.Id == id);
                time.StatdardEndTime = standardEndTime.StandadrDateTime;
                _context.Entry(time).State = EntityState.Modified;
            }
            
            _context.SaveChanges();
        }

        public string GetTime()
        {
            return _context.tblStandardEndTimes.Select(x => x.StatdardEndTime).FirstOrDefault();
        }
    }
}
