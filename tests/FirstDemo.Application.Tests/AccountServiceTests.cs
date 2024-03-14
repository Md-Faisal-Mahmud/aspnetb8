using Autofac.Extras.Moq;
using FirstDemo.Application.Features.Training.Repositories;
using FirstDemo.Application.Features.Training.Services;
using Moq;
using Shouldly;

namespace FirstDemo.Application.Tests
{
    public class AccountServiceTests
    {
        private AutoMock _mock;
        private Mock<IAccountRepository> _accountRepositoryMock;
        private AccountService _accountService;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _mock = AutoMock.GetLoose();
        }

        [SetUp]
        public void Setup()
        {
            _accountRepositoryMock = _mock.Mock<IAccountRepository>();
            _accountService = _mock.Create<AccountService>();
        }

        [TearDown]
        public void TearDown()
        {
            _accountRepositoryMock.Reset();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _mock?.Dispose();
        }


        [Test]
        public void CreateUser_LargeUsername_TruncateUsername()
        {
            // Arrange
            const string username = "thisisaverylongusernamethisisavery"+
                                    "longusernamethisisaverylongusernam"+
                                    "ethisisaverylongusernamethisisaver"+
                                    "ylongusernamethisisaverylongusername";

            const string password = "ljfwoijhwofhweofhw";

            const string expectedResult = "thisisaverylongusernamethisisaverylongusernamethis";

            _accountRepositoryMock.Setup(x => x.CreateAccount(expectedResult, password))
                .Verifiable();

            // Act
            _accountService.CreateUser(username, password);


            // Assert
            _accountRepositoryMock.VerifyAll();
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        [TestCase(" ")]
        public void CreateUser_UsernameMissing_ThrowsException(string username)
        {
            // Arrange
            const string password = "djfldjfjdkfjd";

            // Act & Assert
            Should.Throw<InvalidDataException>(() => 
                _accountService.CreateUser(username, password));
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        [TestCase(" ")]
        public void CreateUser_PasswordMissing_ThrowsException(string password)
        {
            // Arrange
            const string username = "jalaluddin";

            // Act & Assert
            Should.Throw<InvalidDataException>(() =>
                _accountService.CreateUser(username, password));
        }
    }
}