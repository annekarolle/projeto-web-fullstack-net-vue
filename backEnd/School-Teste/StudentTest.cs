using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using orbita.Controllers;
using orbita.Interface;
using Orbita.DTO;
using Orbita.Entity;
using Orbita.Interface;
using Orbita.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orbita_Teste
{
    public class StudentTest
    {

        private SaveStudentsDTO saveStudentDTO;
        private PutStudentDTO updateDTO;
        private IConfiguration config;
        private Mock<IUserRepository> userRepositoryMock;
        private new Mock<IStudentRepository> studentRepositoryMock;
        private new Mock<ILogger<StudentController>> loggerMock;

        [SetUp]
        public void Initialize()
        {

            saveStudentDTO = new SaveStudentsDTO
            {
                Name = "John Doe",
                Email = "john.doe@example.com",
                CPF = "12345678901",
                RA = "RA",

            };

            updateDTO = new PutStudentDTO { Name = "João Silva", Email = "joãosilva@gmail.com" };

            config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            userRepositoryMock = new Mock<IUserRepository>();
            studentRepositoryMock = new Mock<IStudentRepository>();
            loggerMock = new Mock<ILogger<StudentController>>();

        }

        [Test]
        public void TestStudentCreation()
        {
            // Arrange
            Student student = new(saveStudentDTO);

            // Assert
            Assert.IsNotNull(student);
            Assert.AreEqual(saveStudentDTO.Name, student.Name);
            Assert.AreEqual(saveStudentDTO.Email, student.Email);
            Assert.AreEqual(saveStudentDTO.CPF, student.CPF);
            Assert.AreNotEqual(saveStudentDTO.RA, student.RA);

        }

        [Test]
        public void ChangeEmail()
        {
            // Arrange
            var studentDto = new SaveStudentsDTO
            {
                Name = "Marquinho da Vila",
                Email = "marquinho@gmail.com",
                CPF = "12345678901",
                RA = "RA",

            };

            var updateDTO = new PutStudentDTO { Name = "Marquinho da Vila", Email = "marquinho_Dev@gmail.com" };

            Student student = new(studentDto);
            //Act
            student.ChangeNameOrEmail(updateDTO.Name, updateDTO.Email);

            //Assert
            Assert.AreNotEqual(studentDto.Email, student.Email);

        }



        [Test]
        public void GenerateRA()
        {
            // Arrange        
            Student student = new(saveStudentDTO);

            //Act
            var ra = student.GenerateRA(saveStudentDTO.RA, saveStudentDTO.Name);

            //Assert
            Assert.NotNull(ra);
            Assert.AreEqual(ra, "RADoe");

        }

        [Test]
        public void BasicStudentConstruscor()
        {
            //Arrange & Act
            Student student = new();

            //Assert
            Assert.NotNull(student);
        }

        [Test]
        public void SaveStudentReturnsSuccess_TestController()
        {
            // Arrange
            var controller = new StudentController(userRepositoryMock.Object, loggerMock.Object, studentRepositoryMock.Object);

            var saveDto = new SaveStudentsDTO
            {
                Name = "John Doe",
                Email = "john.doe@example.com",
                RA = "123",
                CPF = "12345678901"
            };

            // Act
            var result = controller.SaveStudent(saveDto);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result is OkObjectResult);
        }

        [Test]
        public void GetUserByRA()
        {
            // Arrange
            var controller = new StudentController(userRepositoryMock.Object, loggerMock.Object, studentRepositoryMock.Object);

            var student = new Student
            {
                Name = "John Doe",
                Email = "john.doe@example.com",
                RA = "123",
                CPF = "12345678901"
            };

            // Act
            studentRepositoryMock.Setup(repo => repo.GetByRA(It.IsAny<string>())).Returns(student);
            var studentFound = controller.GetUserByRA("123");

            // Assert
            Assert.IsNotNull(studentFound);
            Assert.IsTrue(studentFound is OkObjectResult);
            var okResult = studentFound as OkObjectResult;
            Assert.IsNotNull(okResult.Value);
            Assert.AreEqual(student, okResult.Value);
        }

        [Test]
        public void GetUserByRA_NotFound()
        {
            // Arrange
            var controller = new StudentController(userRepositoryMock.Object, loggerMock.Object, studentRepositoryMock.Object);

            var student = new Student
            {
                Name = "John Doe",
                Email = "john.doe@example.com",
                RA = "123",
                CPF = "12345678901"
            };

            // Act
            studentRepositoryMock.Setup(repo => repo.GetByRA(It.IsAny<string>())).Returns(student);
            var studentFound = controller.GetUserByRA("123456");

            // Assert
            Assert.IsNotNull(studentFound);
            Assert.IsTrue(studentFound is OkObjectResult);
        }

        [Test]
        public void GetAllStudents()
        {
            //Arrange
            var controller = new StudentController(userRepositoryMock.Object, loggerMock.Object, studentRepositoryMock.Object);

            var students = new List<Student>
            {
                new Student { Name = "Maria Oliveira da Silva", Email = "mari_silva@mail.com", RA = "123", CPF = "12345678901" },
                new Student { Name = "joão Roberto Carlos Junior", Email = "jr@mail.com", RA = "456", CPF = "23456789012" }

            };

            studentRepositoryMock.Setup(repo => repo.GetAll()).Returns(students);

            var result = controller.GetAllStudents();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result is OkObjectResult);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult.Value);
            var resultList = okResult.Value as List<Student>;
            Assert.IsNotNull(resultList);
            CollectionAssert.AreEqual(students, resultList);
        }


         

    }
}