using MentorshipWebApplication.Repository.Entities;
using MentorshipWebApplication.Repository.Repos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NUnit.Framework;
using Serilog;
using System;

namespace MentorshipWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExaminerController : ControllerBase
    {
        //using Dependency Injection to inject the interface into examiner controller
        //private MentorshipWebApplicationBAL.serviceLayer _service;
        private readonly IAuditRepository _service;
        private readonly ILogger<ExaminerController> _logger;
        //our controller has a dependency on the repository logic through that injected interface.
        //Logging is injected using a Dependency Injection principle using Interface ILogger
        public ExaminerController(IAuditRepository service, ILogger<ExaminerController> logger)
        {
            _service = service;
            _logger = logger;
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
                _logger.LogInformation("Retrieve All Examiners");   // first invocation
                var examiners = _service.GetAllExaminers();

                if (examiners == null)
                {
                    return NotFound(new { message = "No Examiner found" });
                }
                _logger.LogDebug($"The response for the get Examiners is {JsonConvert.SerializeObject(examiners)}"); // second invocation
                return Ok(examiners);
            }
            catch (DbUpdateConcurrencyException ex)
            {

                _logger.LogError($"Something went wrong: {ex.Message}");
                //return StatusCode(500, ex.Message);
                throw new Exception(ex.Message);

            }

        }
        //lead and associate examiner
        [HttpGet]
        [Route("getLeadAssociateExaminer")]
        public ActionResult<IEnumerable<Examiner>> GetLeadAssocExaminer()
        {
            try
            {
                _logger.LogInformation("Retrieve All active Examiners");
                var examiners = _service.GetAllLeadAndAssocExaminers();

                if (examiners == null)
                {
                    return NotFound(new { message = "No Examiner found" });
                }
                _logger.LogDebug($"The response for the get Lead or associate examiners is {JsonConvert.SerializeObject(examiners)}");

                return Ok(examiners);
            }
            catch (DbUpdateConcurrencyException ex)
            {

                _logger.LogError($"Something went wrong: {ex.Message}");
                //return StatusCode(500, ex.Message);
                throw new Exception(ex.Message);

            }

        }
        //Enable/Disable Examiners by passing true or False as input parameter

        [Route("EnableOrDisableExaminer/{id}/{value}")]
        [HttpPut]
        public async Task<ActionResult> EnableDisableExaminer(int id, bool value)
        {
            _logger.LogInformation("Activate/Deactivate Examiners");
            if (!_service.ExaminerExists(id))
            {
                _logger.LogError($"Examiner does not exist");
                return NotFound(new {message= "Examiner does not exist" });
            }
            try
            {
                var isAdded = await _service.setActiveOrInactive(id, value);

                if (isAdded == false)
                {
                    _logger.LogError($"Already updated as '\"{value}\"'.No Updation required");
                    return NotFound(new {message= "Already updated as '"+value+"'.No Updation required" });
                }
                else
                {
                    _logger.LogDebug($"Done Successfully");

                    if (value == true)
                        return Ok(new {message= "Successfully Activated" });
                    else
                        return Ok(new {message= "Successfully Deactivated" });
                }
            }
            catch (DbUpdateConcurrencyException ex)
            {

                _logger.LogError($"Something went wrong: {ex.Message}");
                //return StatusCode(500, ex.Message);
                throw new Exception(ex.Message);

            }

            
        }

        //Enable/Disable Examiners by passing true or False as input parameter

        [Route("AddExaminer")]
        [HttpPost]
        public async Task<ActionResult> CreateExaminer(Examiner examinerModel)
        {
            _logger.LogInformation("Add Examiner");
            if (_service.ExaminerExists(examinerModel.ExaminerId))
            {
                _logger.LogError($"Examiner already exists");
                return NotFound(new {message= "Examiner already exists" });
            }
            if (_service.ExaminerUsernameExists(examinerModel.Username))
            {
                _logger.LogError($"Username already exists");
                return NotFound(new { message = "Username already exists" });
            }
            try
            {
                var examiner = await _service.AddnewExaminer(examinerModel);

                if (examiner == null)
                {
                    _logger.LogError($"Error occurred while adding Examiner");
                    return NotFound(new {message= "Error occurred while adding Examiner" });
                }
                else
                {
                    _logger.LogDebug($"The response for create examiners is {JsonConvert.SerializeObject(examiner)}");
                    return Ok(new { message = "Added Successfully" });
                    //return Ok(examiner);// for unit test uncomment this code and comment above code
                }
            }
            catch (DbUpdateConcurrencyException ex)
            {

                _logger.LogError($"Something went wrong: {ex.Message}");
                //return StatusCode(500, ex.Message);
                throw new Exception(ex.Message);

            }

            
        }

        

    }
}
