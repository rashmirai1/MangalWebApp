using MangalWeb.Model.Entity;
using MangalWeb.Model.Masters;
using MangalWeb.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangalWeb.Service.Service
{
    public class BranchService
    {
        BranchRepository _branchRepository = new BranchRepository();

        public BranchViewModel GetPincodDetails(int id)
        {
            var pincodedetails = _branchRepository.GetPincodDetails(id);
            return pincodedetails;
        }

        public List<tblCompanyBranchMaster> GetAllBranchMasters()
        {
            return _branchRepository.GetAllBranchMasters();
        }

        public tblCompanyBranchMaster GetBranchMasterById(int id)
        {
            return _branchRepository.GetBranchMasterById(id);
        }

        public List<BranchViewModel> SetDataofModalList()
        {
            return _branchRepository.SetDataofModalList();
        }

        public dynamic GetPincodeMasterList()
        {
            return _branchRepository.GetPincodeMasterList();
        }

        public List<Mst_BranchType> GetBranchTypeList()
        {
            return _branchRepository.GetBranchTypeList();
        }

        public void DeleteRecord(int id)
        {
            _branchRepository.DeleteRecord(id);
        }

        public void SaveUpdateRecord(BranchViewModel branch)
        {
            _branchRepository.SaveUpdateRecord(branch);
        }

        public string CheckBranchNameExists(string name,int id)
        {
           return _branchRepository.CheckBranchNameExists(name,id);
        }
        public BranchViewModel SetRecordinEdit(tblCompanyBranchMaster tblBranch)
        {
            return _branchRepository.SetRecordinEdit(tblBranch);
        }
        }
}