using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MentorshipWebApplication.Models;
using MentorshipWebApplication.Repository.Entities;
using MentorshipWebApplication.Repository.Repos;
using Swashbuckle.AspNetCore.Annotations;

namespace MentorshipWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuditsController : ControllerBase
    {
        //private MentorshipWebApplicationBAL.serviceLayer _service;
        private readonly IAuditRepository _service;



        public AuditsController(IAuditRepository service)
        {
            _service = service;
        }

        //public AuditsController(MentorshipWebApplicationBAL.serviceLayer service)
        //{
        //    //_service = new MentorshipWebApplicationBAL.serviceLayer();
        //    _service = service;
        //}

        // Display all audits
        [HttpGet]
        [Route("getAudits")]
        public ActionResult<IEnumerable<Audit>> GetAudits()
        {
            try
            {
                var audits = _service.GetAllAudits();

                if (audits == null)
                {
                    return NotFound(new { message = "No Audit found" });
                }

                return Ok(audits);

            }
            catch (DbUpdateConcurrencyException)
            {

                throw;

            }
            
        }

        // ToEdit/Update Audit
        //[Route("updateAudit/{id}/{examinerId}/{associateId?}/{auditDate?}")]
        [SwaggerOperationFilter(typeof(ReApplyOptionalRouteParameterOperationFilter))]
        [HttpPut("updateAudit/{id}/{examinerId}/{associateId?}/{auditDate}")]
        public async Task<ActionResult> updateAudit(int id, int examinerId, DateTime auditDate, int associateId=0)
        {
            if (!_service.AuditExists(id))
            {
                return NotFound(new {message= "Audit does not exist" });
            }
            try
            {
                var audits = await _service.UpdateAudit(id,examinerId,associateId,auditDate);

                if (audits == false)
                {
                    return NotFound(new {message= "Some error occurred" });
                }
                else
                {
                    return Ok(new {message= "Updated" });
                }
            }
            catch (DbUpdateConcurrencyException)
            {
               
                    throw;
                
            }

            
        }

        // Display all audits
        [HttpGet]
        [Route("getAuditStatuses")]
        public ActionResult<IEnumerable<AuditStatus>> GetAuditstatus()
        {
            try
            {
                var auditstatus = _service.GetAllAuditstatus();

                if (auditstatus == null)
                {
                    return NotFound(new { message = "No Audit found" });
                }

                return Ok(auditstatus);
            }
            catch (DbUpdateConcurrencyException)
            {

                throw;

            }

        }

        // Display audit based on branch ID
        [Route("getAuditDetailByBranch/{BranchId}")]
        [HttpGet]
        
        public ActionResult GetAuditDetailbyBranch(string BranchId)
        {
            try
            {
                var auditDetails = _service.GetAuditbyBranch(BranchId);

                if (auditDetails == null)
                {
                    return NotFound(new { message = "No Audit found" });
                }

                return Ok(auditDetails);
            }
            catch (DbUpdateConcurrencyException)
            {

                throw;

            }

        }

        
    }
}
