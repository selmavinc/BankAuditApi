using MentorshipWebApplication.Controllers;
using MentorshipWebApplication.Repository.Entities;
using MentorshipWebApplication.Repository.Repos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace MentorshipWebApplicationTest
{
    [TestFixture]
    public class Tests
    {
        //[SetUp]
        //public void Setup()
        //{
        //}

        [Test]
        public void GetAuditsTest()
        {
            List<UpdateAudit> audlist = new List<UpdateAudit>();
            //for (int i = 0; i < 1; i++)
            //{
                audlist.Add(new UpdateAudit { AuditId = 4, BranchId = "A114", LeadExaminerId = 7, LeadExaminerName = "John", AuditDate = Convert.ToDateTime("2022-11-17"), AssociateExaminerId = 3, AssociateExaminerName = "Veena Nair", AuditHours = 8, AuditStatusId = 2, AuditStatuses = "Scheduled" });
                audlist.Add(new UpdateAudit { AuditId = 5, BranchId = "A115", LeadExaminerId = 0, LeadExaminerName = null, AuditDate = null, AssociateExaminerId = 0, AssociateExaminerName = null, AuditHours = 4, AuditStatusId = 1, AuditStatuses = "UnScheduled" });
            //}
            var mockRepository = new Mock<IAuditRepository>();
            mockRepository.Setup(x => x.GetAllAudits())
                .Returns(audlist);
            var mockLogger=new Mock<ILogger<AuditsController>>();
            int expectedloggerInvocationCount = 2;
            //.Returns(new Audit { AuditId = 4, BranchId = "A114", LeadExaminerId = 7, AuditDate = Convert.ToDateTime("2022-11-17"), AssociateExaminerId = 3, AuditHours = 8, AuditStatusId = 2 });
            var controller = new AuditsController(mockRepository.Object,mockLogger.Object);
            ActionResult<IEnumerable<Audit>> result = controller.GetAudits().Result;
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
        public void GetAuditstatusTest()
        {
            List<AuditStatus> audstatlist = new List<AuditStatus>();
            //for (int i = 0; i < 1; i++)
            //{
            audstatlist.Add(new AuditStatus { AuditStatusId = 1, Name = "Unscheduled" });
            audstatlist.Add(new AuditStatus { AuditStatusId = 2, Name = "Scheduled" });
            //}
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
            //for (int i = 0; i < 1; i++)
            //{
            examlist.Add(new Examiner { ExaminerId = 1, Username = "Mahesh", Password = "123", Name = "Mahesh", HoursAssigned = 4, IsActive = true });
            examlist.Add(new Examiner { ExaminerId = 2, Username = "Deepa", Password = "456", Name = "Deepa", HoursAssigned = 0, IsActive = true });
            //}
            var mockRepository = new Mock<IAuditRepository>();
            mockRepository.Setup(x => x.GetAllLeadAndAssocExaminers())
                .Returns(examlist);
            var mockLogger = new Mock<ILogger<ExaminerController>>();
            int expectedloggerInvocationCount = 2;
            //.Returns(new Audit { AuditId = 4, BranchId = "A114", LeadExaminerId = 7, AuditDate = Convert.ToDateTime("2022-11-17"), AssociateExaminerId = 3, AuditHours = 8, AuditStatusId = 2 });
            var controller = new ExaminerController(mockRepository.Object,mockLogger.Object);
            ActionResult<IEnumerable<Examiner>> result = controller.GetLeadAssocExaminer().Result;
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
        public async Task EnableDisableExaminerPassTest()
        {
            List<Examiner> examlist = new List<Examiner>();
            //for (int i = 0; i < 1; i++)
            //{
            examlist.Add(new Examiner { ExaminerId = 1, Username = "Mahesh", Password = "123", Name = "Mahesh", HoursAssigned = 4, IsActive = true });
            examlist.Add(new Examiner { ExaminerId = 2, Username = "Deepa", Password = "456", Name = "Deepa", HoursAssigned = 0, IsActive = true });
            //}
            var mockRepository = new Mock<IAuditRepository>();
            mockRepository.Setup(x => x.ExaminerExists(1)).Returns(true);
            mockRepository.Setup(x => x.setActiveOrInactive(1,false))
                .ReturnsAsync(true);
            var mockLogger = new Mock<ILogger<ExaminerController>>();
            int expectedloggerInvocationCount = 2;
            //.Returns(new Audit { AuditId = 4, BranchId = "A114", LeadExaminerId = 7, AuditDate = Convert.ToDateTime("2022-11-17"), AssociateExaminerId = 3, AuditHours = 8, AuditStatusId = 2 });
            var controller = new ExaminerController(mockRepository.Object,mockLogger.Object);
            ActionResult result = controller.EnableDisableExaminer(1,false).Result;
            var okObjectResult = (OkObjectResult)result;
            //Audit auditinfo = result.Result.Value;

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
            List<Examiner> examlist = new List<Examiner>();
            //for (int i = 0; i < 1; i++)
            //{
            examlist.Add(new Examiner { ExaminerId = 1, Username = "Mahesh", Password = "123", Name = "Mahesh", HoursAssigned = 4, IsActive = true });
            examlist.Add(new Examiner { ExaminerId = 2, Username = "Deepa", Password = "456", Name = "Deepa", HoursAssigned = 0, IsActive = true });
            //}
            var mockRepository = new Mock<IAuditRepository>();
            mockRepository.Setup(x => x.ExaminerExists(3)).Returns(false);
            mockRepository.Setup(x => x.setActiveOrInactive(1, false))
                .ReturnsAsync(true);
            var mockLogger = new Mock<ILogger<ExaminerController>>();
            int expectedloggerInvocationCount = 2;
            //.Returns(new Audit { AuditId = 4, BranchId = "A114", LeadExaminerId = 7, AuditDate = Convert.ToDateTime("2022-11-17"), AssociateExaminerId = 3, AuditHours = 8, AuditStatusId = 2 });
            var controller = new ExaminerController(mockRepository.Object, mockLogger.Object);
            ActionResult result = controller.EnableDisableExaminer(3, true).Result;
            var NotFoundObjectResult = (NotFoundObjectResult)result;
            //Audit auditinfo = (Audit)okObjectResult.Value;

            //Assert.IsNotNull(okObjectResult.Value);
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
            List<Examiner> examlist = new List<Examiner>();
            //for (int i = 0; i < 1; i++)
            //{
            examlist.Add(new Examiner { ExaminerId = 1, Username = "Mahesh", Password = "123", Name = "Mahesh", HoursAssigned = 4, IsActive = true });
            examlist.Add(new Examiner { ExaminerId = 2, Username = "Deepa", Password = "456", Name = "Deepa", HoursAssigned = 0, IsActive = true });
            //}
            var mockRepository = new Mock<IAuditRepository>();
            mockRepository.Setup(x => x.ExaminerExists(1)).Returns(true);
            mockRepository.Setup(x => x.setActiveOrInactive(1, true))
                .ReturnsAsync(false);
            var mockLogger = new Mock<ILogger<ExaminerController>>();
            int expectedloggerInvocationCount = 2;
            //.Returns(new Audit { AuditId = 4, BranchId = "A114", LeadExaminerId = 7, AuditDate = Convert.ToDateTime("2022-11-17"), AssociateExaminerId = 3, AuditHours = 8, AuditStatusId = 2 });
            var controller = new ExaminerController(mockRepository.Object, mockLogger.Object);
            ActionResult result = controller.EnableDisableExaminer(1, true).Result;
            var NotFoundObjectResult = (NotFoundObjectResult)result;
            //Audit auditinfo = (Audit)okObjectResult.Value;

            //Assert.IsNotNull(okObjectResult.Value);
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
            List<Examiner> examlist = new List<Examiner>();
            //for (int i = 0; i < 1; i++)
            //{
            examlist.Add(new Examiner { ExaminerId = 1, Username = "Mahesh", Password = "123", Name = "Mahesh", HoursAssigned = 4, IsActive = true });
            examlist.Add(new Examiner { ExaminerId = 2, Username = "Deepa", Password = "456", Name = "Deepa", HoursAssigned = 0, IsActive = true });
            //}
            var mockRepository = new Mock<IAuditRepository>();
            mockRepository.Setup(x => x.ExaminerExists(3)).Returns(false);
            mockRepository.Setup(x => x.ExaminerUsernameExists("Veena")).Returns(false);
            mockRepository.Setup(x => x.AddnewExaminer(new Examiner { ExaminerId = 3, Username = "Veena", Password = "123", Name = "Veena", HoursAssigned = 4, IsActive = true }))
                .ReturnsAsync(new Examiner { ExaminerId = 3, Username = "Veena", Password = "123", Name = "Veena", HoursAssigned = 4, IsActive = true });
            //.Returns(new Audit { AuditId = 4, BranchId = "A114", LeadExaminerId = 7, AuditDate = Convert.ToDateTime("2022-11-17"), AssociateExaminerId = 3, AuditHours = 8, AuditStatusId = 2 });
            var mockLogger = new Mock<ILogger<ExaminerController>>();
            int expectedloggerInvocationCount = 2;
            var controller = new ExaminerController(mockRepository.Object, mockLogger.Object);
            ActionResult result = controller.CreateExaminer(new Examiner { ExaminerId = 3, Username = "Veena", Password = "123", Name = "Veena", HoursAssigned = 4, IsActive = true }).Result;
            var okObjectResult = (OkObjectResult)result;
            //Audit auditinfo = result.Result.Value;

            Assert.IsNotNull(okObjectResult.Value);
            Assert.AreEqual(200, okObjectResult.StatusCode);
            Assert.AreEqual(expectedloggerInvocationCount, mockLogger.Invocations.Count);
            Assert.AreEqual(LogLevel.Information, mockLogger.Invocations[0].Arguments[0]);
            Assert.AreEqual(LogLevel.Debug, mockLogger.Invocations[1].Arguments[0]);
            //Assert.Pass();

        }

        [Test]
        public async Task updateAuditPassTest()
        {
            List<UpdateAudit> audlist = new List<UpdateAudit>();
            audlist.Add(new UpdateAudit { AuditId = 4, BranchId = "A114", LeadExaminerId = 7, LeadExaminerName = "John", AuditDate = Convert.ToDateTime("2022-11-17"), AssociateExaminerId = 3, AssociateExaminerName = "Veena Nair", AuditHours = 8, AuditStatusId = 2, AuditStatuses = "Scheduled" });
            audlist.Add(new UpdateAudit { AuditId = 5, BranchId = "A115", LeadExaminerId = 0, LeadExaminerName = null, AuditDate = null, AssociateExaminerId = 0, AssociateExaminerName = null, AuditHours = 4, AuditStatusId = 1, AuditStatuses = "UnScheduled" });
            var mockRepository = new Mock<IAuditRepository>();
            mockRepository.Setup(x => x.AuditExists(5)).Returns(true);
            mockRepository.Setup(x =>  x.UpdateAudit(5, 2, 0, Convert.ToDateTime("2022-04-12")))
                .ReturnsAsync(true);
            var mockLogger = new Mock<ILogger<AuditsController>>();
            int expectedloggerInvocationCount = 2;
            AuditsController audit = new AuditsController(mockRepository.Object, mockLogger.Object);
            ActionResult result = await audit.updateAudit(5, 2, Convert.ToDateTime("2022-04-12"), 0);
            var okObjectResult = (OkObjectResult)result;
            //Audit auditinfo = (Audit)okObjectResult.Value;

            //Assert.IsNotNull(okObjectResult.Value);
            ////Assert
            Assert.AreEqual(200, okObjectResult.StatusCode);
            Assert.AreEqual(expectedloggerInvocationCount, mockLogger.Invocations.Count);
            Assert.AreEqual(LogLevel.Information, mockLogger.Invocations[0].Arguments[0]);
            Assert.AreEqual(LogLevel.Debug, mockLogger.Invocations[1].Arguments[0]);
            //Assert.AreEqual(5, auditinfo.AuditId);
            Assert.Pass();
        }
        [Test]
        public async Task updateAuditNotFoundTest()
        {
            List<UpdateAudit> audlist = new List<UpdateAudit>();
            audlist.Add(new UpdateAudit { AuditId = 4, BranchId = "A114", LeadExaminerId = 7, LeadExaminerName = "John", AuditDate = Convert.ToDateTime("2022-11-17"), AssociateExaminerId = 3, AssociateExaminerName = "Veena Nair", AuditHours = 8, AuditStatusId = 2, AuditStatuses = "Scheduled" });
            audlist.Add(new UpdateAudit { AuditId = 5, BranchId = "A115", LeadExaminerId = 0, LeadExaminerName = null, AuditDate = null, AssociateExaminerId = 0, AssociateExaminerName = null, AuditHours = 4, AuditStatusId = 1, AuditStatuses = "UnScheduled" });
            var mockRepository = new Mock<IAuditRepository>();
            mockRepository.Setup(x => x.AuditExists(6)).Returns(false);
            var mockLogger = new Mock<ILogger<AuditsController>>();
            int expectedloggerInvocationCount = 2;
            AuditsController audit = new AuditsController(mockRepository.Object, mockLogger.Object);
            ActionResult result = await audit.updateAudit(6, 2, Convert.ToDateTime("2022-04-12"), 0);
            var NotFoundObjectResult = (NotFoundObjectResult)result;
            //Audit auditinfo = (Audit)okObjectResult.Value;

            //Assert.IsNotNull(okObjectResult.Value);
            ////Assert
            Assert.AreEqual(404, NotFoundObjectResult.StatusCode);
            Assert.AreEqual(expectedloggerInvocationCount, mockLogger.Invocations.Count);
            Assert.AreEqual(LogLevel.Information, mockLogger.Invocations[0].Arguments[0]);
            Assert.AreEqual(LogLevel.Error, mockLogger.Invocations[1].Arguments[0]);
            //Assert.AreEqual(5, auditinfo.AuditId);
            Assert.Pass();
        }

        //[Test]
        //public void GetExaminerTest()
        //{
        //    ExaminerController examiner = new ExaminerController();
        //    ActionResult<IEnumerable<Examiner>> result = examiner.GetExaminer();
        //    var okObjectResult = (OkObjectResult)result.Result;

        //    //Assert
        //    okObjectResult.StatusCode.Equals(200);
        //    //Assert.NotNull(result.Result);
        //    //Assert.Pass();
        //}

        //[Test]
        //public void GetLeadAssocExaminerTest()
        //{
        //    ExaminerController examiner = new ExaminerController();
        //    ActionResult<IEnumerable<Examiner>> result = examiner.GetLeadAssocExaminer();
        //    var okObjectResult = (OkObjectResult)result.Result;

        //    //Assert
        //    okObjectResult.StatusCode.Equals(200);
        //    //Assert.NotNull(result.Result);
        //    //Assert.Pass();
        //}

        //[Test]
        //public void DisableExaminerTest()
        //{
        //    ExaminerController examiner = new ExaminerController();
        //    Task<ActionResult> result = examiner.EnableDisableExaminer(1,false);
        //    var okObjectResult = (OkObjectResult)result.Result;
        //    //var NotFoundObjectResult = (NotFoundObjectResult)result.Result;


        //    //Assert
        //    okObjectResult.StatusCode.Equals(200);
        //    //Assert.NotNull(result.Result);
        //    //Assert.Pass();
        //}

        //[Test]
        //public void EnableExaminerTest()
        //{
        //    ExaminerController examiner = new ExaminerController();
        //    Task<ActionResult> result = examiner.EnableDisableExaminer(1, true);
        //    var okObjectResult = (OkObjectResult)result.Result;


        //    //Assert
        //    okObjectResult.StatusCode.Equals(200);
        //}

        //[Test]
        //public void CreateExaminerTest()
        //{
        //    ExaminerController examiner = new ExaminerController();
        //    Examiner examinerDetail = new Examiner
        //    {
        //                //ExaminerId = 4,  
        //                Name = "Test",  
        //                Username="Test",
        //                Password="123",
        //                HoursAssigned=0,
        //                IsActive=true
        //    };
        //    Task<ActionResult> result = examiner.CreateExaminer(examinerDetail);
        //    var okObjectResult = (OkObjectResult)result.Result;


        //    //Assert
        //    okObjectResult.StatusCode.Equals(200);
        //}


    }
}