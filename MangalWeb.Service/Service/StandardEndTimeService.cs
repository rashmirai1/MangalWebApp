using MangalWeb.Model.Utilities;
using MangalWeb.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangalWeb.Service.Service
{
    public class StandardEndTimeService
    {
        StandardEndTimeRepository _standardEndTimeRepository = new StandardEndTimeRepository();

        public void SaveUpdateRecord(StandardEndTime standardEndTime)
        {
            _standardEndTimeRepository.SaveUpdateRecord(standardEndTime);
        }

        public string GetTime()
        {
            return _standardEndTimeRepository.GetTime(); 
        }
    }
}
