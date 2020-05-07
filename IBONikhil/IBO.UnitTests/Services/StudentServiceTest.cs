using IBO.Business;
using IBO.Business.DTOs;
using IBO.IBusiness;
using IBO.IRepository;
using IBO.Repository.Entities;
using Moq;
using Ploeh.AutoFixture;
using System.Collections.Generic;
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
            //Arrange
            var studentBusiness = TestFixture.Create<Task<List<StudentDTOs>>>();
            _studentRepository.Setup(x => x.GetAllStudentDetails()).Returns(studentBusiness);

            //Act
            var result = _target.GetAllStudentDetails();

            //Assert
            MockBaseRepository.VerifyAll();
            Assert.NotNull(result);
        }
        [Fact]
        public void GetAllStudentName()
        {
            //Arrange
            var studentBusinesstest = TestFixture.Create<Task<string>>();
            _studentRepository.Setup(x => x.GetAllStudentName()).Returns(studentBusinesstest);

            //Act
            var result = _target.GetAllStudentName();

            //Assert
            MockBaseRepository.VerifyAll();
            Assert.NotNull(result);
        }
        [Fact]
        public void GetAllStudentById()
        {
            //Arrange
            var studentBusiness = TestFixture.Create<Task<StudentDTOs>>();
            _studentRepository.Setup(x => x.GetStudentByID(It.IsAny<int>())).Returns(studentBusiness);

            //Act
            var result = _target.GetStudentByID(1);

            //Assert
            MockBaseRepository.VerifyAll();
            Assert.NotNull(result);
        }

        [Fact]
        public void RegisterStudent()
        {
            //Arrange
            var studentBusiness = TestFixture.Create<StudentDTOs>();
            var studentBusinesstest =TestFixture.Create<Task<bool>>();
            _studentRepository.Setup(x => x.Register(It.IsAny<Student>())).Returns(studentBusinesstest);

            //Act
            var result = _target.Register(studentBusiness);

            //Assert
            MockBaseRepository.VerifyAll();
            Assert.NotNull(result);
        }

        [Fact]
        public void UpdateStudentTest()
        {
            //Arrange
            var studentBusiness = TestFixture.Create<StudentDTOs>();
            var studentBusinesstest = TestFixture.Create<Task<bool>>();
            _studentRepository.Setup(x => x.UpdateStudent(It.IsAny<int>(),It.IsAny<Student>())).Returns(studentBusinesstest);

            //Act
            var result = _target.Update(1,studentBusiness);

            //Assert
            MockBaseRepository.VerifyAll();
            Assert.NotNull(result);
        }

        [Fact]
        public void DeleteStudentById()
        {
            //Arrange
            var studentBusinesstest = TestFixture.Create<Task<bool>>();
            _studentRepository.Setup(x => x.DeleteStudentById(It.IsAny<int>())).Returns(studentBusinesstest);

            //Act
            var result = _target.DeleteStudentById(1);

            //Assert
            MockBaseRepository.VerifyAll();
            Assert.NotNull(result);
        }
    }
}
