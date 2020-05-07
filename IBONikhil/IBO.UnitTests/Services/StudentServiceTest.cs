using IBO.Business;
using IBO.Business.DTOs;
using IBO.IBusiness;
using IBO.IRepository;
using IBO.Repository.Entities;
using Moq;
using Ploeh.AutoFixture;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace IBO.UnitTests.Services
{
    public class StudentServiceTest : BaseUnitTest
    {
        private readonly Mock<IStudentService> _studentService;
        private readonly Mock<IStudentRepository> _studentRepository;
        private readonly Mock<ILoggerRepository> _loggerRepository;

        private IStudentService _target;
        public StudentServiceTest()
        {
            _studentService = MockBaseRepository.Create<IStudentService>();
            _studentRepository = MockBaseRepository.Create<IStudentRepository>();
            _loggerRepository = MockBaseRepository.Create<ILoggerRepository>();
            _target = new StudentService(_studentRepository.Object, _loggerRepository.Object);

        }
        [Fact]
        public void GetStudentDetails()
        {
            var studentBusiness = TestFixture.Create<Task<List<StudentDTOs>>>();
            _studentRepository.Setup(x => x.GetAllStudentDetails()).Returns(studentBusiness);

            var result = _target.GetAllStudentDetails();

            MockBaseRepository.VerifyAll();
            Assert.NotNull(result);
        }

    }
}
