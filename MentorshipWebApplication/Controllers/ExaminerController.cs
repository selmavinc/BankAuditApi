using MentorshipWebApplication.Repository.Entities;
using MentorshipWebApplication.Repository.Repos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore;

namespace MentorshipWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExaminerController : ControllerBase
    {
        //private MentorshipWebApplicationBAL.serviceLayer _service;
        private readonly IAuditRepository _service;
        public ExaminerController(IAuditRepository service)
        {
            _service = service;
        }

        //public ExaminerController()
        //{
        //    _service = new MentorshipWebApplicationBAL.serviceLayer();
        //}

        //View All Examiners
        [HttpGet]
        [Route("getExaminer")]
        public ActionResult<IEnumerable<Examiner>> GetExaminer()
        {
            try
            {
                var examiners = _service.GetAllExaminers();

                if (examiners == null)
                {
                    return NotFound(new { message = "No Examiner found" });
                }

                return Ok(examiners);
            }
            catch (DbUpdateConcurrencyException)
            {

                throw;

            }

        }
        //lead and associate examiner
        [HttpGet]
        [Route("getLeadAssociateExaminer")]
        public ActionResult<IEnumerable<Examiner>> GetLeadAssocExaminer()
        {
            try
            {
                var examiners = _service.GetAllLeadAndAssocExaminers();

                if (examiners == null)
                {
                    return NotFound(new { message = "No Examiner found" });
                }

                return Ok(examiners);
            }
            catch (DbUpdateConcurrencyException)
            {

                throw;

            }

        }
        //Enable/Disable Examiners by passing true or False as input parameter

        [Route("EnableOrDisableExaminer/{id}/{value}")]
        [HttpPut]
        public async Task<ActionResult> EnableDisableExaminer(int id, bool value)
        {
            if (!_service.ExaminerExists(id))
            {
                return NotFound(new {message= "Examiner does not exist" });
            }
            try
            {
                var audits = await _service.setActiveOrInactive(id, value);

                if (audits == false)
                {
                    return NotFound(new {message= "Already updated as '"+value+"'.No Updation required" });
                }
                else
                {
                    if (value == true)
                        return Ok(new {message= "Successfully Activated" });
                    else
                        return Ok(new {message= "Successfully Deactivated" });
                }
            }
            catch (DbUpdateConcurrencyException)
            {

                throw;

            }

            
        }

        //Enable/Disable Examiners by passing true or False as input parameter

        [Route("AddExaminer")]
        [HttpPost]
        public async Task<ActionResult> CreateExaminer(Examiner examinerModel)
        {
            if (_service.ExaminerExists(examinerModel.ExaminerId))
            {
                return NotFound(new {message= "Examiner already exists" });
            }
            if (_service.ExaminerUsernameExists(examinerModel.Username))
            {
                return NotFound(new { message = "Username already exists" });
            }
            try
            {
                var audits = await _service.AddnewExaminer(examinerModel);

                if (audits == null)
                {
                    return NotFound(new {message= "Error occurred while adding Examiner" });
                }
                else
                {
                    return Ok(new {message = "Added Successfully" });
                }
            }
            catch (DbUpdateConcurrencyException)
            {

                throw;

            }

            
        }

        

    }
}
