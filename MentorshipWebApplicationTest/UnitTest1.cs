using MentorshipWebApplication.Controllers;
using MentorshipWebApplication.Repository.Entities;
using MentorshipWebApplication.Repository.Repos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json.Linq;
using Serilog;
using System.ComponentModel;

namespace MentorshipWebApplicationTest
{
    [TestFixture]
    public class Tests
    {
        //[SetUp]
        //public void Setup()
        //{
        //}

        //Install-Package Moq
        [Test]      // required to run the test otherwise this test case won't run
        public void GetAuditsTest()
        {
            List<UpdateAudit> audlist = new List<UpdateAudit>();
            //for (int i = 0; i < 1; i++)
            //{
                audlist.Add(new UpdateAudit { AuditId = 4, BranchId = "A114", LeadExaminerId = 7, LeadExaminerName = "John", AuditDate = Convert.ToDateTime("2022-11-17"), AssociateExaminerId = 3, AssociateExaminerName = "Veena Nair", AuditHours = 8, AuditStatusId = 2, AuditStatuses = "Scheduled" });
                audlist.Add(new UpdateAudit { AuditId = 5, BranchId = "A115", LeadExaminerId = 0, LeadExaminerName = null, AuditDate = null, AssociateExaminerId = 0, AssociateExaminerName = null, AuditHours = 4, AuditStatusId = 1, AuditStatuses = "UnScheduled" });
            //}
            //We create a mock object of type IEmployeeRepository inside the constructor, and since we want to test the controller logic, we create an instance of that controller with the mocked object as a required parameter.
            //Logging is injected using a Dependency Injection principle using Interface ILogger
            var mockRepository = new Mock<IAuditRepository>();
            //Mock all methods in that Action method of controller
            //we use the Setup method to specify a setup for the GetAllAudits method.Additionally, we have to use the Returns method to specify the value to return from the mocked GetAllAudits method.
            mockRepository.Setup(x => x.GetAllAudits())
                .Returns(audlist);
            var mockLogger=new Mock<ILogger<AuditsController>>();
            int expectedloggerInvocationCount = 2; // 2 because there are 2 invocations
            //.Returns(new Audit { AuditId = 4, BranchId = "A114", LeadExaminerId = 7, AuditDate = Convert.ToDateTime("2022-11-17"), AssociateExaminerId = 3, AuditHours = 8, AuditStatusId = 2 });
            var controller = new AuditsController(mockRepository.Object,mockLogger.Object);
            ActionResult<IEnumerable<Audit>> result = controller.GetAudits().Result;
            var okObjectResult = (OkObjectResult)result.Result;
            //Audit auditinfo = result.Result.Value;

            Assert.IsNotNull(okObjectResult.Value);          
            Assert.AreEqual(200, okObjectResult.StatusCode);
            Assert.AreEqual(expectedloggerInvocationCount, mockLogger.Invocations.Count); // verify invocation count
            Assert.AreEqual(LogLevel.Information, mockLogger.Invocations[0].Arguments[0]); // verify Logtype
            Assert.AreEqual(LogLevel.Debug, mockLogger.Invocations[1].Arguments[0]); // Verify the Exception Logs 
            //Assert.Pass();

        }

        [Test]
        public void GetAuditstatusTest()
        {
            List<AuditStatus> audstatlist = new List<AuditStatus>();           
            audstatlist.Add(new AuditStatus { AuditStatusId = 1, Name = "Unscheduled" });
            audstatlist.Add(new AuditStatus { AuditStatusId = 2, Name = "Scheduled" });

            var mockRepository = new Mock<IAuditRepository>();
            mockRepository.Setup(x => x.GetAllAuditstatus())
                .Returns(audstatlist);
            var mockLogger = new Mock<ILogger<AuditsController>>();
            int expectedloggerInvocationCount = 2;
            var controller = new AuditsController(mockRepository.Object, mockLogger.Object);
            ActionResult<IEnumerable<AuditStatus>> result = controller.GetAuditstatus().Result;
            var okObjectResult = (OkObjectResult)result.Result;

            Assert.IsNotNull(okObjectResult.Value);
            Assert.AreEqual(200, okObjectResult.StatusCode);
            Assert.AreEqual(expectedloggerInvocationCount, mockLogger.Invocations.Count);
            Assert.AreEqual(LogLevel.Information, mockLogger.Invocations[0].Arguments[0]);
            Assert.AreEqual(LogLevel.Debug, mockLogger.Invocations[1].Arguments[0]);
            //Assert.Pass();

        }

        [Test]
        public void GetAuditDetailbyBranchTest()
        {
           
            var mockRepository = new Mock<IAuditRepository>();
            mockRepository.Setup(x => x.GetAuditbyBranch("A114"))
                .Returns(new UpdateAudit { AuditId = 4, BranchId = "A114", LeadExaminerId = 7, LeadExaminerName = "John", AuditDate = Convert.ToDateTime("2022-11-17"), AssociateExaminerId = 3, AssociateExaminerName = "Veena Nair", AuditHours = 8, AuditStatusId = 2, AuditStatuses = "Scheduled" });
            var mockLogger = new Mock<ILogger<AuditsController>>();
            int expectedloggerInvocationCount = 2;
            var controller = new AuditsController(mockRepository.Object, mockLogger.Object);
            ActionResult<UpdateAudit> result = controller.GetAuditDetailbyBranch("A114");
            var okObjectResult = (OkObjectResult)result.Result;
            UpdateAudit auditInfo = (UpdateAudit) okObjectResult.Value;
            Assert.AreEqual("A114", auditInfo.BranchId);
            Assert.AreEqual(200, okObjectResult.StatusCode);
            Assert.AreEqual(expectedloggerInvocationCount, mockLogger.Invocations.Count);
            Assert.AreEqual(LogLevel.Information, mockLogger.Invocations[0].Arguments[0]);
            Assert.AreEqual(LogLevel.Debug, mockLogger.Invocations[1].Arguments[0]);
            //Assert.Pass();

        }

        [Test]
        public void GetExaminerTest()
        {
            List<Examiner> examlist = new List<Examiner>();
            //for (int i = 0; i < 1; i++)
            //{
            examlist.Add(new Examiner { ExaminerId = 1, Username = "Mahesh", Password = "123", Name = "Mahesh", HoursAssigned = 4, IsActive = true});
            examlist.Add(new Examiner { ExaminerId = 2, Username = "Deepa", Password = "456", Name = "Deepa", HoursAssigned = 0, IsActive = true});
            //}
            var mockRepository = new Mock<IAuditRepository>();
            mockRepository.Setup(x => x.GetAllExaminers())
                .Returns(examlist);
            var mockLogger = new Mock<ILogger<ExaminerController>>();
            int expectedloggerInvocationCount = 2;
            //.Returns(new Audit { AuditId = 4, BranchId = "A114", LeadExaminerId = 7, AuditDate = Convert.ToDateTime("2022-11-17"), AssociateExaminerId = 3, AuditHours = 8, AuditStatusId = 2 });
            var controller = new ExaminerController(mockRepository.Object,mockLogger.Object);
            ActionResult<IEnumerable<Examiner>> result = controller.GetExaminer().Result;
            var okObjectResult = (OkObjectResult)result.Result;
            //Audit auditinfo = result.Result.Value;

            Assert.IsNotNull(okObjectResult.Value);
            Assert.AreEqual(200, okObjectResult.StatusCode);
            Assert.AreEqual(expectedloggerInvocationCount, mockLogger.Invocations.Count);
            Assert.AreEqual(LogLevel.Information, mockLogger.Invocations[0].Arguments[0]);
            Assert.AreEqual(LogLevel.Debug, mockLogger.Invocations[1].Arguments[0]);
            //Assert.Pass();

        }

        [Test]
        public void GetLeadAssocExaminerTest()
        {
            List<Examiner> examlist = new List<Examiner>();
            examlist.Add(new Examiner { ExaminerId = 1, Username = "Mahesh", Password = "123", Name = "Mahesh", HoursAssigned = 4, IsActive = true });
            examlist.Add(new Examiner { ExaminerId = 2, Username = "Deepa", Password = "456", Name = "Deepa", HoursAssigned = 0, IsActive = true });
            
            var mockRepository = new Mock<IAuditRepository>();
            mockRepository.Setup(x => x.GetAllLeadAndAssocExaminers())
                .Returns(examlist);
            var mockLogger = new Mock<ILogger<ExaminerController>>();
            int expectedloggerInvocationCount = 2;
            //.Returns(new Audit { AuditId = 4, BranchId = "A114", LeadExaminerId = 7, AuditDate = Convert.ToDateTime("2022-11-17"), AssociateExaminerId = 3, AuditHours = 8, AuditStatusId = 2 });
            var controller = new ExaminerController(mockRepository.Object,mockLogger.Object);
            ActionResult<IEnumerable<Examiner>> result = controller.GetLeadAssocExaminer().Result;
            var okObjectResult = (OkObjectResult)result.Result;

            Assert.IsNotNull(okObjectResult.Value);
            Assert.AreEqual(200, okObjectResult.StatusCode);
            Assert.AreEqual(expectedloggerInvocationCount, mockLogger.Invocations.Count);
            Assert.AreEqual(LogLevel.Information, mockLogger.Invocations[0].Arguments[0]);
            Assert.AreEqual(LogLevel.Debug, mockLogger.Invocations[1].Arguments[0]);
            //Assert.Pass();

        }

        [Test]
        public async Task EnableDisableExaminerPassTest()
        {
 
            var mockRepository = new Mock<IAuditRepository>();
            mockRepository.Setup(x => x.ExaminerExists(1)).Returns(true);
            mockRepository.Setup(x => x.setActiveOrInactive(1,false))
                .ReturnsAsync(true);
            var mockLogger = new Mock<ILogger<ExaminerController>>();
            int expectedloggerInvocationCount = 2;
            
            var controller = new ExaminerController(mockRepository.Object,mockLogger.Object);
            ActionResult result = controller.EnableDisableExaminer(1,false).Result;
            var okObjectResult = (OkObjectResult)result;


            Assert.IsNotNull(okObjectResult.Value);
            Assert.AreEqual(200, okObjectResult.StatusCode);
            Assert.AreEqual(expectedloggerInvocationCount, mockLogger.Invocations.Count);
            Assert.AreEqual(LogLevel.Information, mockLogger.Invocations[0].Arguments[0]);
            Assert.AreEqual(LogLevel.Debug, mockLogger.Invocations[1].Arguments[0]);
            //Assert.Pass();

        }

        [Test]
        public async Task EnableDisableExaminerNotFoundTest()
        {
            
            var mockRepository = new Mock<IAuditRepository>();
            mockRepository.Setup(x => x.ExaminerExists(3)).Returns(false);
            mockRepository.Setup(x => x.setActiveOrInactive(1, false))
                .ReturnsAsync(true);
            var mockLogger = new Mock<ILogger<ExaminerController>>();
            int expectedloggerInvocationCount = 2;
            
            var controller = new ExaminerController(mockRepository.Object, mockLogger.Object);
            ActionResult result = controller.EnableDisableExaminer(3, true).Result;
            var NotFoundObjectResult = (NotFoundObjectResult)result;

            ////Assert
            Assert.AreEqual(404, NotFoundObjectResult.StatusCode);
            Assert.AreEqual(expectedloggerInvocationCount, mockLogger.Invocations.Count);
            Assert.AreEqual(LogLevel.Information, mockLogger.Invocations[0].Arguments[0]);
            Assert.AreEqual(LogLevel.Error, mockLogger.Invocations[1].Arguments[0]);
            Assert.Pass();

        }

        [Test]
        public async Task EnableDisableExaminerFailTest()
        {

            var mockRepository = new Mock<IAuditRepository>();
            mockRepository.Setup(x => x.ExaminerExists(1)).Returns(true);
            mockRepository.Setup(x => x.setActiveOrInactive(1, true))
                .ReturnsAsync(false);
            var mockLogger = new Mock<ILogger<ExaminerController>>();
            int expectedloggerInvocationCount = 2;
            
            var controller = new ExaminerController(mockRepository.Object, mockLogger.Object);
            ActionResult result = controller.EnableDisableExaminer(1, true).Result;
            var NotFoundObjectResult = (NotFoundObjectResult)result;

            ////Assert
            Assert.AreEqual(404, NotFoundObjectResult.StatusCode);
            Assert.AreEqual(expectedloggerInvocationCount, mockLogger.Invocations.Count);
            Assert.AreEqual(LogLevel.Information, mockLogger.Invocations[0].Arguments[0]);
            Assert.AreEqual(LogLevel.Error, mockLogger.Invocations[1].Arguments[0]);
            Assert.Pass();

        }

        [Test]
        public async Task CreateExaminerPassTest()
        {
            Examiner exam = new Examiner
            {
                ExaminerId = 3,
                Username = "Veena",
                Password = "123",
                Name = "Veena",
                HoursAssigned = 4,
                IsActive = true
            };
            var mockRepository = new Mock<IAuditRepository>();
            mockRepository.Setup(x => x.ExaminerExists(3)).Returns(false);
            mockRepository.Setup(x => x.ExaminerUsernameExists("Veena")).Returns(false);
            mockRepository.Setup(x => x.AddnewExaminer(exam))
                .ReturnsAsync(new Examiner { ExaminerId = 3, Username = "Veena", Password = "123", Name = "Veena", HoursAssigned = 4, IsActive = true });
            
            var mockLogger = new Mock<ILogger<ExaminerController>>();
            int expectedloggerInvocationCount = 2;
            var controller = new ExaminerController(mockRepository.Object, mockLogger.Object);
            ActionResult result = controller.CreateExaminer(exam).Result;
            var okObjectResult = (OkObjectResult)result;
            Examiner examinerinfo = (Examiner)okObjectResult.Value;

            Assert.IsNotNull(okObjectResult.Value);
            Assert.AreEqual(200, okObjectResult.StatusCode);
            Assert.AreEqual(exam.Name, examinerinfo.Name);
            Assert.AreEqual(expectedloggerInvocationCount, mockLogger.Invocations.Count);
            Assert.AreEqual(LogLevel.Information, mockLogger.Invocations[0].Arguments[0]);
            Assert.AreEqual(LogLevel.Debug, mockLogger.Invocations[1].Arguments[0]);
            //Assert.Pass();

        }

        [Test]
        public async Task CreateExaminerExaminerExistsTest()
        {
            Examiner exam = new Examiner
            {
                ExaminerId = 3,
                Username = "Veena",
                Password = "123",
                Name = "Veena",
                HoursAssigned = 4,
                IsActive = true
            };
            var mockRepository = new Mock<IAuditRepository>();
            mockRepository.Setup(x => x.ExaminerExists(3)).Returns(true);
            mockRepository.Setup(x => x.ExaminerUsernameExists("Veena")).Returns(false);
            var mockLogger = new Mock<ILogger<ExaminerController>>();
            int expectedloggerInvocationCount = 2;
            var controller = new ExaminerController(mockRepository.Object, mockLogger.Object);
            ActionResult result = controller.CreateExaminer(exam).Result;
            var NotFoundObjectResult = (NotFoundObjectResult)result;

            ////Assert
            Assert.AreEqual(404, NotFoundObjectResult.StatusCode);
            Assert.AreEqual(expectedloggerInvocationCount, mockLogger.Invocations.Count);
            Assert.AreEqual(LogLevel.Information, mockLogger.Invocations[0].Arguments[0]);
            Assert.AreEqual(LogLevel.Error, mockLogger.Invocations[1].Arguments[0]);
            Assert.Pass();

        }

        [Test]
        public async Task CreateExaminerUsernameExistsTest()
        {
            Examiner exam = new Examiner
            {
                ExaminerId = 3,
                Username = "Veena",
                Password = "123",
                Name = "Veena",
                HoursAssigned = 4,
                IsActive = true
            };
            var mockRepository = new Mock<IAuditRepository>();
            mockRepository.Setup(x => x.ExaminerExists(3)).Returns(false);
            mockRepository.Setup(x => x.ExaminerUsernameExists("Veena")).Returns(true);
            var mockLogger = new Mock<ILogger<ExaminerController>>();
            int expectedloggerInvocationCount = 2;
            var controller = new ExaminerController(mockRepository.Object, mockLogger.Object);
            ActionResult result = controller.CreateExaminer(exam).Result;
            var NotFoundObjectResult = (NotFoundObjectResult)result;

            ////Assert
            Assert.AreEqual(404, NotFoundObjectResult.StatusCode);
            Assert.AreEqual(expectedloggerInvocationCount, mockLogger.Invocations.Count);
            Assert.AreEqual(LogLevel.Information, mockLogger.Invocations[0].Arguments[0]);
            Assert.AreEqual(LogLevel.Error, mockLogger.Invocations[1].Arguments[0]);
            Assert.Pass();

        }

        [Test]
        public async Task updateAuditPassTest()
        {
            
            var mockRepository = new Mock<IAuditRepository>();
            mockRepository.Setup(x => x.AuditExists(5)).Returns(true);
            mockRepository.Setup(x =>  x.UpdateAudit(5, 2, 0, Convert.ToDateTime("2022-04-12")))
                .ReturnsAsync(true);
            var mockLogger = new Mock<ILogger<AuditsController>>();
            int expectedloggerInvocationCount = 2;
            AuditsController audit = new AuditsController(mockRepository.Object, mockLogger.Object);
            ActionResult result = await audit.updateAudit(5, 2, Convert.ToDateTime("2022-04-12"), 0);
            var okObjectResult = (OkObjectResult)result;

            ////Assert
            Assert.AreEqual(200, okObjectResult.StatusCode);
            Assert.AreEqual(expectedloggerInvocationCount, mockLogger.Invocations.Count);
            Assert.AreEqual(LogLevel.Information, mockLogger.Invocations[0].Arguments[0]);
            Assert.AreEqual(LogLevel.Debug, mockLogger.Invocations[1].Arguments[0]);
            Assert.Pass();
        }
        [Test]
        public async Task updateAuditNotFoundTest()
        {
           
            var mockRepository = new Mock<IAuditRepository>();
            mockRepository.Setup(x => x.AuditExists(6)).Returns(false);
            var mockLogger = new Mock<ILogger<AuditsController>>();
            int expectedloggerInvocationCount = 2;
            AuditsController audit = new AuditsController(mockRepository.Object, mockLogger.Object);
            ActionResult result = await audit.updateAudit(6, 2, Convert.ToDateTime("2022-04-12"), 0);
            var NotFoundObjectResult = (NotFoundObjectResult)result;

            Assert.AreEqual(404, NotFoundObjectResult.StatusCode);
            Assert.AreEqual(expectedloggerInvocationCount, mockLogger.Invocations.Count);
            Assert.AreEqual(LogLevel.Information, mockLogger.Invocations[0].Arguments[0]);
            Assert.AreEqual(LogLevel.Error, mockLogger.Invocations[1].Arguments[0]);
            Assert.Pass();
        }


    }
}