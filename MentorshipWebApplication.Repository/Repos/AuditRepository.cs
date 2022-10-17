using MentorshipWebApplication.Repository.Data;
using MentorshipWebApplication.Repository.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentorshipWebApplication.Repository.Repos
{
    public class AuditRepository
    {
        //private readonly AuditAppContext _context;

        //public AuditRepository(AuditAppContext context)
        //{
        //    _context = context;
        //}

        //Implementation of context class in .NET 6+ version
        private AuditAppContext _context;

        public AuditRepository()
        {
            _context = new AuditAppContext();
        }

        public IEnumerable<UpdateAudit> getAudits()
        {
            try
            {
                var audits = (from t1 in _context.Audits.DefaultIfEmpty()
                            from t2 in _context.Examiners.Where(z => z.ExaminerId == t1.LeadExaminerId).DefaultIfEmpty()
                            from t3 in _context.Examiners.Where(q => q.ExaminerId == t1.AssociateExaminerId).DefaultIfEmpty()
                            from t4 in _context.AuditStatuses.Where(s => s.AuditStatusId == t1.AuditStatusId).DefaultIfEmpty()
                              select new UpdateAudit()
                            {
                                AuditId = t1.AuditId,
                                BranchId = t1.BranchId,
                                LeadExaminerId = t1.LeadExaminerId,
                                LeadExaminerName = t2.Name,
                                AssociateExaminerId = t1.AssociateExaminerId,
                                AssociateExaminerName = t3.Name,
                                AuditDate = t1.AuditDate,
                                AuditHours = t1.AuditHours,
                                AuditStatusId = t1.AuditStatusId,
                                AuditStatuses = t4.Name
                              }).ToList();
                //  select new
                //{
                //    auditid = t1.AuditId,
                //    branchid = t1.BranchId,
                //    Leadexaminerid=t1.LeadExaminerId,
                //    Leadexaminername = t2.Name,
                //    Associateexaminerid=t1.AssociateExaminerId,
                //    Associateexaminername = t3.Name,
                //    Auditdate = t1.AuditDate,
                //    Audithours = t1.AuditHours,
                //    AuditstatusId = t1.AuditStatusId,
                //    AuditStatuses=t4.Name
                //      //EmailId = purchase.EmailId == null? "NA" : purchase.EmailId
                //  })
                //            .Select(x => new Audit()
                //            {
                //                AuditId = x.auditid,
                //                BranchId = x.branchid,
                //                LeadExaminerId=x.Leadexaminerid,
                //                LeadExaminerName = x.Leadexaminername,
                //                AssociateExaminerId=x.Associateexaminerid,
                //                AssociateExaminerName = x.Associateexaminername,
                //                AuditDate = x.Auditdate,
                //                AuditHours = x.Audithours,
                //                AuditStatusId = x.AuditstatusId,
                //                AuditStatuses=x.AuditStatuses
                //            }).ToList();

                return audits;
            }
            catch(Exception)
            {
                throw new NotImplementedException();
            }
            
        }

        
        public IEnumerable<AuditStatus> GetAuditStatuses()
        {
            try
            {
                return _context.AuditStatuses.ToList();

            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
            
        }

        public IEnumerable<Examiner> getExaminers()
        {
            try
            {
                var examiner = _context.Examiners.ToList();
               
                return examiner;
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }

        }

        public IEnumerable<Examiner> getLeadAndAssocExaminers()
        {
            try
            {
                var examiner = _context.Examiners.Where(x => x.IsActive==true).ToList();
                return examiner;
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }

        }

        public async Task<bool> UpdateAudit(int id, int examinerId, int associateId, DateTime auditDate)
        {
            bool isUpdated = false;
            try
            {

                var data = await _context.Audits.FirstOrDefaultAsync(x => x.AuditId == id);
                if (data != null)
                    {
                        data.LeadExaminerId = examinerId ==0?null:examinerId;
                        data.AssociateExaminerId = associateId == 0 ? null : associateId;
                        data.AuditDate = auditDate;
                        data.AuditStatusId = 2;

                }
                //isUpdated = _context.SaveChanges() > 0;
                var obj = _context.Update(data);
                if (obj != null)
                {
                    _context.SaveChanges();
                    isUpdated = true;

                }
            }
            catch (Exception)
            {
                throw;
            }
            return isUpdated;
        }

        public async Task<bool> UpdateExaminer(int examinerId, decimal? hoursAssign)
        {
            bool isUpdated = false;
            try
            {
                if (examinerId != 0)
                {
                    var data = await _context.Examiners.FirstOrDefaultAsync(x => x.ExaminerId == examinerId);
                if (data != null)
                {
                    data.HoursAssigned = hoursAssign;

                }
                    isUpdated = _context.SaveChanges() > 0;

                }
            }
            catch (Exception)
            {
                throw;
            }
            return isUpdated;
        }

        public async Task<bool> Disableexaminer(int id)
        {
            bool isUpdated = false;
            try
            {
                
                    var data = await _context.Examiners.FirstOrDefaultAsync(x => x.ExaminerId == id);
                    if (data != null)
                    {
                        data.IsActive = false;

                    }
                isUpdated = _context.SaveChanges() > 0;


            }
            catch (Exception)
            {
                throw;
            }
            return isUpdated;
        }

        public async Task<bool> EnableDisableExaminer(int id,bool value)
        {
            bool isActivated = false;
            try
            {

                var data = await _context.Examiners.FirstOrDefaultAsync(x => x.ExaminerId == id);
                if (data != null)
                {
                    data.IsActive = value;

                }
                isActivated = _context.SaveChanges() > 0;


            }
            catch (Exception)
            {
                throw;
            }
            return isActivated;
        }

        public bool AuditExists(int id)
        {
            try
            {
                return _context.Audits.Any(e => e.AuditId == id);
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        public bool ExaminerExists(int id)
        {
            try
            {
                return _context.Examiners.Any(e => e.ExaminerId == id);
            }
            catch (Exception)
            {
                throw;
            }
           
        }

        public bool ExaminerUsernameExists(string value)
        {
            try
            {
                return _context.Examiners.Any(e => e.Username == value);
            }
            catch (Exception)
            {
                throw;
            }

        }

        public async Task<Examiner> CreateNewExaminer(Examiner examinerUser)
        {
            try
            {
                
                    var obj = _context.Add<Examiner>(examinerUser);
                    await _context.SaveChangesAsync();
                    return obj.Entity;
                
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Examiner GetExaminerById(int? Id)
        {
            try
            {
                
                    var Obj = _context.Examiners.FirstOrDefault(x => x.ExaminerId == Id);
                    return Obj;
                
            }
            catch (Exception)
            {
                throw;
            }
        }

        public UpdateAudit GetAuditById(int Id)
        {
            try
            {
                var Obj = (from t1 in _context.Audits
                           join t2 in _context.Examiners on t1.LeadExaminerId equals t2.ExaminerId into lead
                           from leadResult in lead.DefaultIfEmpty()
                           join t3 in _context.Examiners on t1.AssociateExaminerId equals t3.ExaminerId into associate
                           from associateResult in associate.DefaultIfEmpty()
                           join t4 in _context.AuditStatuses on t1.AuditStatusId equals t4.AuditStatusId into auditStatus
                           from status in auditStatus.DefaultIfEmpty()
                           where t1.AuditId == Id
                           select new UpdateAudit
                           {
                               AuditId = t1.AuditId,
                               BranchId = t1.BranchId,
                               LeadExaminerId = t1.LeadExaminerId,
                               LeadExaminerName = leadResult.Name == null ? "NA" : leadResult.Name,
                               AssociateExaminerId = t1.AssociateExaminerId,
                               AssociateExaminerName = associateResult.Name == null ? "NA" : associateResult.Name,
                               AuditDate = t1.AuditDate,
                               AuditHours = t1.AuditHours,
                               AuditStatusId = t1.AuditStatusId,
                               AuditStatuses = status.Name
                           }).FirstOrDefault();
                //var Obj = _context.Audits.FirstOrDefault(x => x.AuditId == Id);
                    return Obj;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public UpdateAudit GetAuditDetailbyBranch(string branchId)
        {
            try
            {
                
                    var Obj = (from t1 in _context.Audits
                               join t2 in _context.Examiners on t1.LeadExaminerId equals t2.ExaminerId into lead
                               from leadResult in lead.DefaultIfEmpty()
                               join t3 in _context.Examiners on t1.AssociateExaminerId equals t3.ExaminerId into associate
                               from associateResult in associate.DefaultIfEmpty()
                               join t4 in _context.AuditStatuses on t1.AuditStatusId equals t4.AuditStatusId into auditStatus
                               from status in auditStatus.DefaultIfEmpty()
                               where t1.BranchId == branchId
                               select new UpdateAudit
                               {
                                   AuditId = t1.AuditId,
                                   BranchId = t1.BranchId,
                                   LeadExaminerId = t1.LeadExaminerId,
                                   LeadExaminerName = leadResult.Name == null ? "NA" : leadResult.Name,
                                   AssociateExaminerId = t1.AssociateExaminerId,
                                   AssociateExaminerName = associateResult.Name == null ? "NA" : associateResult.Name,
                                   AuditDate = t1.AuditDate,
                                   AuditHours = t1.AuditHours,
                                   AuditStatusId = t1.AuditStatusId,
                                   AuditStatuses = status.Name
                               }).FirstOrDefault();
                            return Obj;

            }
            catch (Exception)
            {
                throw;
            }
        }
        


    }
}
