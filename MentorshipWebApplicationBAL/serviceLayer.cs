using MentorshipWebApplication.Repository.Data;
using MentorshipWebApplication.Repository.Entities;
using MentorshipWebApplication.Repository.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentorshipWebApplicationBAL
{
    public class serviceLayer : IAuditRepository
    {
        //public readonly AuditRepository _repository;
        //public serviceLayer(AuditRepository repository)
        //{
        //    _repository = repository;
        //}
        public AuditRepository _repository;
        public serviceLayer()
        {
            _repository = new AuditRepository();
        }
        public IEnumerable<UpdateAudit> GetAllAudits()
        {
            try
            {
                return _repository.getAudits().ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public IEnumerable<AuditStatus> GetAllAuditstatus()
        {
            try
            {
                return _repository.GetAuditStatuses().ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public IEnumerable<Examiner> GetAllExaminers()
        {
            try
            {
                return _repository.getExaminers().ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<Examiner> GetAllLeadAndAssocExaminers()
        {
            try
            {
                return _repository.getLeadAndAssocExaminers().ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> UpdateAudit(int id, int examinerId, int associateId, DateTime auditDate)
        {
            try
            {
                decimal? hoursAssigned;
                var auditDetail = _repository.GetAuditById(id);
                var upres = false;
                var hours = auditDetail.AuditHours != null ? auditDetail.AuditHours : 0;
                hoursAssigned = Convert.ToDecimal(0.5) * hours;
                if (examinerId != 0 && associateId != 0)
                {

                    if (auditDetail.LeadExaminerId != examinerId)
                    {
                        if (auditDetail.LeadExaminerId != null && auditDetail.AssociateExaminerId != null)
                        {
                            var examinerDetail = _repository.GetExaminerById(auditDetail.LeadExaminerId);
                            if (examinerDetail.HoursAssigned != 0)
                                upres = await _repository.UpdateExaminer((int)auditDetail.LeadExaminerId, examinerDetail.HoursAssigned - hoursAssigned);
                            else
                                upres = await _repository.UpdateExaminer((int)auditDetail.LeadExaminerId, 0);

                        }
                        else
                        {
                            if (auditDetail.LeadExaminerId != null)
                            {
                                var examinerDetail = _repository.GetExaminerById(auditDetail.LeadExaminerId);
                                if (examinerDetail.HoursAssigned != 0)
                                    upres = await _repository.UpdateExaminer((int)auditDetail.LeadExaminerId, examinerDetail.HoursAssigned - auditDetail.AuditHours);
                                else
                                    upres = await _repository.UpdateExaminer((int)auditDetail.LeadExaminerId, 0);

                            }
                               

                        }

                        var LeadExaminerDetail = _repository.GetExaminerById(examinerId);
                        if (LeadExaminerDetail.HoursAssigned != 0)
                            upres = await _repository.UpdateExaminer(examinerId, LeadExaminerDetail.HoursAssigned + hoursAssigned);
                        else
                            upres = await _repository.UpdateExaminer(examinerId, hoursAssigned);



                    }
                    else
                    {
                        if (auditDetail.LeadExaminerId != null && auditDetail.AssociateExaminerId == null)
                        {
                            var examinerDetail = _repository.GetExaminerById(auditDetail.LeadExaminerId);
                            if (examinerDetail.HoursAssigned != 0)
                                upres = await _repository.UpdateExaminer((int)auditDetail.LeadExaminerId, examinerDetail.HoursAssigned - hoursAssigned);
                            else
                                upres = await _repository.UpdateExaminer((int)auditDetail.LeadExaminerId, 0);

                        }
                        else
                        {
                            upres = true;
                        }
                    }

                    if (auditDetail.AssociateExaminerId != associateId)
                    {
                        if (auditDetail.AssociateExaminerId != null && auditDetail.LeadExaminerId != null)
                        {
                            var examinerDetail = _repository.GetExaminerById(auditDetail.AssociateExaminerId);
                            if (examinerDetail.HoursAssigned != 0)
                                upres = await _repository.UpdateExaminer((int)auditDetail.AssociateExaminerId, examinerDetail.HoursAssigned - hoursAssigned);
                            else
                                upres = await _repository.UpdateExaminer((int)auditDetail.AssociateExaminerId, 0);

                        }
                        else
                        {
                            if (auditDetail.AssociateExaminerId != null)
                            {
                                var examinerDetail = _repository.GetExaminerById(auditDetail.AssociateExaminerId);
                                if (examinerDetail.HoursAssigned != 0)
                                    upres = await _repository.UpdateExaminer((int)auditDetail.AssociateExaminerId, examinerDetail.HoursAssigned - auditDetail.AuditHours);
                                else
                                    upres = await _repository.UpdateExaminer((int)auditDetail.AssociateExaminerId, 0);
                            }
                                

                        }

                        var AssoExaminerDetail = _repository.GetExaminerById(associateId);
                        if (AssoExaminerDetail.HoursAssigned != 0)
                            upres = await _repository.UpdateExaminer(associateId, AssoExaminerDetail.HoursAssigned + hoursAssigned);
                        else
                            upres = await _repository.UpdateExaminer(associateId, hoursAssigned);

                    }
                    else
                    {

                        if (auditDetail.AssociateExaminerId != null && auditDetail.LeadExaminerId == null)
                        {
                            var examinerDetail = _repository.GetExaminerById(auditDetail.AssociateExaminerId);
                            if (examinerDetail.HoursAssigned != 0)
                                upres = await _repository.UpdateExaminer((int)auditDetail.AssociateExaminerId, examinerDetail.HoursAssigned - hoursAssigned);
                            else
                                upres = await _repository.UpdateExaminer((int)auditDetail.AssociateExaminerId, 0);

                        }
                        else
                        {
                            upres = true;
                        }
                    }

                }
                else if (examinerId != 0 && associateId == 0)
                {
                    if (auditDetail.LeadExaminerId != examinerId)
                    {
                        if (auditDetail.LeadExaminerId != null && auditDetail.AssociateExaminerId != null)
                        {
                            var examinerDetail = _repository.GetExaminerById(auditDetail.LeadExaminerId);
                            if (examinerDetail.HoursAssigned != 0)
                                upres = await _repository.UpdateExaminer((int)auditDetail.LeadExaminerId, examinerDetail.HoursAssigned - hoursAssigned);
                            else
                                upres = await _repository.UpdateExaminer((int)auditDetail.LeadExaminerId, 0);

                        }
                        else
                        {
                            if (auditDetail.LeadExaminerId != null)
                            {
                                var examinerDetail = _repository.GetExaminerById(auditDetail.LeadExaminerId);
                                if (examinerDetail.HoursAssigned != 0)
                                    upres = await _repository.UpdateExaminer((int)auditDetail.LeadExaminerId, examinerDetail.HoursAssigned - auditDetail.AuditHours);
                                else
                                    upres = await _repository.UpdateExaminer((int)auditDetail.LeadExaminerId, 0);
                            }
                                
                        }

                        var LeadExaminerDetail = _repository.GetExaminerById(examinerId);
                        if (LeadExaminerDetail.HoursAssigned != 0)
                            upres = await _repository.UpdateExaminer(examinerId, LeadExaminerDetail.HoursAssigned + auditDetail.AuditHours);
                        else
                            upres = await _repository.UpdateExaminer(examinerId, auditDetail.AuditHours);

                    }
                    else
                    {
                        if (auditDetail.LeadExaminerId != null && auditDetail.AssociateExaminerId != null)
                        {
                            var examinerDetail = _repository.GetExaminerById(auditDetail.LeadExaminerId);
                            if (examinerDetail.HoursAssigned != 0)
                                upres = await _repository.UpdateExaminer((int)auditDetail.LeadExaminerId, examinerDetail.HoursAssigned + hoursAssigned);
                            else
                                upres = await _repository.UpdateExaminer((int)auditDetail.LeadExaminerId, 0);

                        }
                        else
                        {
                            upres = true;
                        }
                    }
                    if (auditDetail.AssociateExaminerId != null && auditDetail.LeadExaminerId != null)
                    {
                        var examinerDetail = _repository.GetExaminerById(auditDetail.AssociateExaminerId);
                        if (examinerDetail.HoursAssigned != 0)
                            upres = await _repository.UpdateExaminer((int)auditDetail.AssociateExaminerId, examinerDetail.HoursAssigned - hoursAssigned);
                        else
                            upres = await _repository.UpdateExaminer((int)auditDetail.AssociateExaminerId, 0);

                    }
                    else
                    {
                        if (auditDetail.AssociateExaminerId != null)
                        {
                            var examinerDetail = _repository.GetExaminerById(auditDetail.AssociateExaminerId);
                            if (examinerDetail.HoursAssigned != 0)
                                upres = await _repository.UpdateExaminer((int)auditDetail.AssociateExaminerId, examinerDetail.HoursAssigned - auditDetail.AuditHours);
                            else
                                upres = await _repository.UpdateExaminer((int)auditDetail.AssociateExaminerId, 0);
                        }
                           
                    }
                    

                }

                if (!upres)
                    return false;
                else
                    return await _repository.UpdateAudit(id, examinerId, associateId, auditDate);

            }
            catch (Exception)
            {
                throw;
            }
            

        

        }

        public async Task<bool> setInActive(int id)
        {
            try
            {
                return await _repository.Disableexaminer(id);
            }
            catch(Exception)
            {
                throw;
            }
               
            

        }

        public async Task<bool> setActiveOrInactive(int id,bool value)
        {
            try
            {
                return await _repository.EnableDisableExaminer(id, value);
            }
            catch(Exception)
            {
                throw;
            }
            
            

        }

        public async Task<Examiner> AddnewExaminer(Examiner examModel)
        {
            try
            {
                return await _repository.CreateNewExaminer(examModel);
            }
            catch(Exception)
            {
                throw;
            }
            
            

        }

        public UpdateAudit GetAuditbyBranch(string branchId)
        {
            try
            {
                return _repository.GetAuditDetailbyBranch(branchId);
            }
            catch(Exception)
            {
                throw;
            }
           
            

        }

        public bool AuditExists(int id)
        {
            try
            {
                return _repository.AuditExists(id);
            }
            catch(Exception)
            {
                throw;
            }
            
        }

        public bool ExaminerExists(int id)
        {
            try
            {
                return _repository.ExaminerExists(id);
            }
            catch(Exception)
            {
                throw;
            }
            
        }

        public bool ExaminerUsernameExists(string value)
        {
            try
            {
                return _repository.ExaminerUsernameExists(value);
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
