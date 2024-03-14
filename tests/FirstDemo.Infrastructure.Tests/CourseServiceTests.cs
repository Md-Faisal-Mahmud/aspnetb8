using Autofac.Extras.Moq;
using FirstDemo.Application.Features.Training.Repositories;
using FirstDemo.Application.Features.Training.Services;
using FirstDemo.Application;
using FirstDemo.Domain.Entities.Training;
using FirstDemo.Infrastructure.Features.Services;
using Moq;
using Shouldly;
using System.Linq.Expressions;
using FirstDemo.Infrastructure.Features.Exceptions;

namespace FirstDemo.Infrastructure.Tests
{
    public class CourseServiceTests
    {
        private AutoMock _mock;
        private Mock<IApplicationUnitOfWork> _applicationtUnitOfWork;
        private Mock<ICourseRepository> _courseRepositoryMock;
        private ICourseService _courseService;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _mock = AutoMock.GetLoose();
        }

        [SetUp]
        public void Setup()
        {
            _applicationtUnitOfWork = _mock.Mock<IApplicationUnitOfWork>();
            _courseRepositoryMock = _mock.Mock<ICourseRepository>();
            _courseService = _mock.Create<CourseService>();
        }

        [TearDown]
        public void TearDown()
        {
            _applicationtUnitOfWork.Reset();
            _courseRepositoryMock.Reset();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _mock?.Dispose();
        }

        [Test]
        public void CreateCourse_CourseDoesNotExists_CreatesCourse()
        {
            // Arrange
            const string name = "ABC";
            const double fees = 8000;

            Course course = new Course
            {
                Name = "blabla",
                Fees = fees
            };

            _applicationtUnitOfWork.Setup(x => x.Courses)
                .Returns(_courseRepositoryMock.Object);

            _courseRepositoryMock.Setup(x => x.IsDuplicateName(name, null))
                .Returns(false).Verifiable();

            _applicationtUnitOfWork.Setup(x => x.Save()).Verifiable();

            _courseRepositoryMock.Setup(x => x.Add(
                It.Is<Course>(y => y.Name == name && y.Fees == fees)))
                .Verifiable();

            // Act
            _courseService.CreateCourse(name, fees);

            // Assert
            this.ShouldSatisfyAllConditions(
                _applicationtUnitOfWork.VerifyAll,
                _courseRepositoryMock.VerifyAll
            );

        }
        
        [Test]
        public void CreateCourse_CourseExists_ThrowsError()
        {
            // Arrange
            const string name = "ABC";
            const double fees = 8000;

            Course course = new Course
            {
                Name = name,
                Fees = fees
            };

            _applicationtUnitOfWork.Setup(x => x.Courses)
                .Returns(_courseRepositoryMock.Object);

            _courseRepositoryMock.Setup(x => x.IsDuplicateName(name, null))
                .Returns(true);

            // Act
            Should.Throw<DuplicateNameException>(
                () => _courseService.CreateCourse(name, fees)
            );
        }

        [Test]
        public void GetCourse_ValidId_ReturnsCourse()
        {
            // Arrange
            var id = new Guid("A5DDE200-5C9B-46E1-9E55-16FD0BF7C721");

            Course course = new Course
            {
                Id = id
            };

            _applicationtUnitOfWork.Setup(x => x.Courses)
                .Returns(_courseRepositoryMock.Object);

            _courseRepositoryMock.Setup(x => x.GetById(id)).Returns(course);

            // Act
            var result = _courseService.GetCourse(id);

            // Assert
            this.ShouldSatisfyAllConditions(
                () => result.ShouldNotBeNull(),
                () => result.Id.ShouldBe(id)
            );
        }
    }
}