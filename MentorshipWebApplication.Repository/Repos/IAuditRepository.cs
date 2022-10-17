using MentorshipWebApplication.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentorshipWebApplication.Repository.Repos
{
    public interface IAuditRepository
    {
        //public IEnumerable<UpdateAudit> getAudits();
        //public IEnumerable<AuditStatus> GetAuditStatuses();
        //public Task<bool> UpdateAudit(int id, int examinerId, int associateId, DateTime auditDate);
        //public bool AuditExists(int id);
        //public IEnumerable<Examiner> getExaminers();
        //public IEnumerable<Examiner> getLeadAndAssocExaminers();
        //public Task<bool> Disableexaminer(int id);
        //public Task<bool> EnableDisableExaminer(int id,bool value);
        //public bool ExaminerExists(int id);
        //public bool ExaminerUsernameExists(string value);
        //public Task<Examiner> CreateNewExaminer(Examiner examinerModel);
        //public UpdateAudit GetAuditDetailbyBranch(string branchID);
        //public Examiner GetExaminerById(int? id);
        //public UpdateAudit GetAuditById(int id);
        //public Task<bool> UpdateExaminer(int examinerId, decimal? hoursAssign);

        public IEnumerable<UpdateAudit> GetAllAudits();
        public IEnumerable<AuditStatus> GetAllAuditstatus();
        public Task<bool> UpdateAudit(int id, int examinerId, int associateId, DateTime auditDate);
        public bool AuditExists(int id);
        public IEnumerable<Examiner> GetAllExaminers();
        public IEnumerable<Examiner> GetAllLeadAndAssocExaminers();
        public Task<bool> setInActive(int id);
        public Task<bool> setActiveOrInactive(int id, bool value);
        public bool ExaminerExists(int id);
        public bool ExaminerUsernameExists(string value);
        public Task<Examiner> AddnewExaminer(Examiner examinerModel);
        public UpdateAudit GetAuditbyBranch(string branchID);
       
        
    }
}
