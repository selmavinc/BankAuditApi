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
using Newtonsoft.Json;

namespace MentorshipWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuditsController : ControllerBase
    {
        //private MentorshipWebApplicationBAL.serviceLayer _service;
        private readonly IAuditRepository _service;
        private readonly ILogger<AuditsController> _logger;



        public AuditsController(IAuditRepository service, ILogger<AuditsController> logger)
        {
            _service = service;
            _logger = logger;
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
                _logger.LogInformation("Retrieve All Audits");
                var audits = _service.GetAllAudits();

                if (audits == null)
                {
                    return NotFound(new { message = "No Audit found" });
                }

                _logger.LogDebug($"The response for the get audits is {JsonConvert.SerializeObject(audits)}");
                return Ok(audits);

            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError($"Issue at controller: {ex.Message}");
                //return StatusCode(500, ex.Message);
                throw new Exception(ex.Message);

            }
            
        }

        // ToEdit/Update Audit
        //[Route("updateAudit/{id}/{examinerId}/{associateId?}/{auditDate?}")]
        [SwaggerOperationFilter(typeof(ReApplyOptionalRouteParameterOperationFilter))]
        [HttpPut("updateAudit/{id}/{examinerId}/{associateId?}/{auditDate}")]
        public async Task<ActionResult> updateAudit(int id, int examinerId, DateTime auditDate, int associateId=0)
        {
            _logger.LogInformation("Modify Audit");
            if (!_service.AuditExists(id))
            {
                _logger.LogError($"Audit does not exist");
                return NotFound(new {message= "Audit does not exist" });   
            }
            try
            {
                var audits = await _service.UpdateAudit(id,examinerId,associateId,auditDate);

                if (audits == false)
                {
                    _logger.LogError($"Some error occured at context");
                    return NotFound(new {message= "Some error occurred at context" });
                }
                else
                {
                    _logger.LogDebug($"Updated Status is {JsonConvert.SerializeObject(audits)}");
                    return Ok(new {message= "Updated" });
                }
                
            }
            catch (DbUpdateConcurrencyException ex)
            {

                _logger.LogError($"Issue at controller: {ex.Message}");
                //return StatusCode(500, ex.Message);
                throw new Exception(ex.Message);

            }

            
        }

        // Display all audits
        [HttpGet]
        [Route("getAuditStatuses")]
        public ActionResult<IEnumerable<AuditStatus>> GetAuditstatus()
        {
            try
            {
                _logger.LogInformation("Retrieve All Audit Statuses");
                var auditstatus = _service.GetAllAuditstatus();

                if (auditstatus == null)
                {
                    return NotFound(new { message = "No Audit found" });
                }

                _logger.LogDebug($"The response for the get audit Status is {JsonConvert.SerializeObject(auditstatus)}");
                return Ok(auditstatus);
            }
            catch (DbUpdateConcurrencyException ex)
            {

                _logger.LogError($"Issue at controller: {ex.Message}");
                //return StatusCode(500, ex.Message);
                throw new Exception(ex.Message);


            }

        }

        // Display audit based on branch ID
        [Route("getAuditDetailByBranch/{BranchId}")]
        [HttpGet]
        
        public ActionResult GetAuditDetailbyBranch(string BranchId)
        {
            try
            {
                _logger.LogInformation("Retrieve Audit by Branch ID");
                var auditDetails = _service.GetAuditbyBranch(BranchId);

                if (auditDetails == null)
                {
                    return NotFound(new { message = "No Audit found" });
                }
                _logger.LogDebug($"The response for the get audit by Branch is {JsonConvert.SerializeObject(auditDetails)}");
                return Ok(auditDetails);
            }
            catch (DbUpdateConcurrencyException ex)
            {

                _logger.LogError($"Issue at controller: {ex.Message}");
                //return StatusCode(500, ex.Message);
                throw new Exception(ex.Message);

            }

        }

        
    }
}
