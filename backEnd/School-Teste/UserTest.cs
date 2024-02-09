using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using orbita.Controllers;
using orbita.Interface;
using Orbita.DTO;
using Orbita.Entity;
using Orbita.Enums;
using Orbita.Interface;
using Orbita.Services;
using TechTalk.SpecFlow.CommonModels;

namespace Orbita_Teste
{
    public class UserTest
    {

        private PasswordHasherService passwordHasherMock;
        private SaveUserDTO saveDtoAdmin;
        private TokenService tokenService;
        private IConfiguration config;
        private Mock<IUserRepository> userRepositoryMock;
        private new Mock<IStudentRepository> studentRepositoryMock;
        private new Mock<ILogger<UserController>> loggerMock;
       



        [SetUp]
        public void Initialize()
        {

            passwordHasherMock = new PasswordHasherService();

            saveDtoAdmin = new SaveUserDTO
            {
                Name = "John Doe",
                Email = "john.doe@example.com",               
                Password = "old_password",
                Permitions = Orbita.Enums.PermitionsTypes.Admin,

            };

            config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            tokenService = new TokenService(config);

            userRepositoryMock = new Mock<IUserRepository>();
            loggerMock = new Mock<ILogger<UserController>>();
          


        }

        [Test]
        public void TestUserAdminCreation()
        {
            // Arrange
            User user = new(saveDtoAdmin, passwordHasherMock);

            // Assert
            Assert.IsNotNull(user);
            Assert.AreEqual(saveDtoAdmin.Name, user.Name);
            Assert.AreEqual(saveDtoAdmin.Email, user.Email);         
            Assert.IsNotNull(user.Password);
            Assert.AreEqual(user.Permitions, PermitionsTypes.Admin);
        }

        [Test]
        public void ChangePassword()
        {
            // Arrange
            var newPassword = "new_password";
            var oldPassword = "old_password";

            User user = new(saveDtoAdmin, passwordHasherMock);

            //Act
            user.ChangeUserPassword(newPassword, passwordHasherMock);

            //Assert
            Assert.IsTrue(passwordHasherMock.VerifyPassword(user.Password, newPassword));
            Assert.IsFalse(passwordHasherMock.VerifyPassword(user.Password, oldPassword));

        }

        [Test]
        public void CreateDirectorUser()
        {

            //Arrange
            var saveAdminUserDto = new SaveUserDTO
            {
                Name = "John Doe",
                Email = "john.doe@example.com",             
                Password = "old_password",
                Permitions = PermitionsTypes.Director,
            };

            //Act
            User user = new(saveAdminUserDto, passwordHasherMock);

            //Assert
            Assert.AreEqual(user.Permitions, PermitionsTypes.Director);

        }

        [Test]
        public void TestHashingPassword()
        {
            //Arrange 
            var password = "Password2024";

            //Act
            var passwordHas = passwordHasherMock.HashPassword(password);

            //Assert
            Assert.AreNotEqual(password, passwordHas);
        }

        [Test]
        public void CreateGeneralUser()
        {
            //Arrange
            var saveGeneralUserDto = new SaveUserDTO
            {
                Name = "John Doe",
                Email = "john.doe@example.com",              
                Password = "old_password",
                Permitions = PermitionsTypes.General,
            };


            //Act
            User user = new(saveGeneralUserDto, passwordHasherMock);

            //Assert
            Assert.AreEqual(user.Permitions, PermitionsTypes.General);
        }

        [Test]
        public void GetUserToken()
        {
            //Arrange
            User user = new(saveDtoAdmin, passwordHasherMock);

            //Act           
            var userToken = tokenService.GetToken(user);

            //Assert
            Assert.IsNotNull(string.IsNullOrEmpty(userToken));
            Assert.IsTrue(userToken.Length > 0);

        }

        [Test]
        public void BasicUserConstruscor()
        {
            //Arrange & Act
            User user = new();

            //Assert
            Assert.NotNull(user);
        }

        [Test]
        public void SaveUser_TestController()
        {
            // Arrange
            var controller = new UserController(userRepositoryMock.Object, loggerMock.Object, passwordHasherMock);
         
            var dto = new SaveUserDTO
            {
                Name = "John Doe",
                Email = "john.doe@example.com",
                Password = "old_password",
                Permitions = PermitionsTypes.Admin,
            };

            // Act
            var user = controller.SaveUser(dto);

            // Assert
            Assert.IsNotNull(user);
            Assert.IsTrue(user is OkObjectResult);
        }

        [Test]
        public void GettAllUser_TestController()
        {
            // Arrange
            var controller = new UserController(userRepositoryMock.Object, loggerMock.Object, passwordHasherMock);

            var dto = new SaveUserDTO
            {
                Name = "John Doe",
                Email = "john.doe@example.com",
                Password = "old_password",
                Permitions = PermitionsTypes.Admin,
            };

            var users = new List<User>
            {
                new User { Name = "Maria Oliveira da Silva", Email = "mari_silva@mail.com", Password = "123", Permitions = PermitionsTypes.Admin },
                new User { Name = "João Roberto Carlos Junior", Email = "jr@mail.com", Password = "456", Permitions = PermitionsTypes.Director },
                new User { Name = "Ana Carolina Santos", Email = "ana_santos@mail.com", Password = "789", Permitions = PermitionsTypes.General },
                new User { Name = "Fernando Lima Pereira", Email = "fernando_pereira@mail.com", Password = "abc", Permitions = PermitionsTypes.Admin }
            };

            // Act
            userRepositoryMock.Setup(repo => repo.GetAll()).Returns(users);
            var result = controller.GetAllUser();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result is OkObjectResult);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult.Value);
            var resultList = okResult.Value as List<User>;
            Assert.IsNotNull(resultList);
            CollectionAssert.AreEqual(users, resultList);
        }

        

    }
}