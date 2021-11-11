using AutoFixture;
using AutoMapper;
using AutoMapper.Configuration;
using Moq;
using NUnit.Framework;
using ProductsBusinessLayer.DTOs;
using ProductsBusinessLayer.MapperProfiles;
using ProductsBusinessLayer.Services.RegistrationService;
using ProductsBusinessLayer.Services.SmtpService;
using ProductsCore.Models;
using ProductsDataLayer.Repositories.EmailRepository;
using ProductsDataLayer.Repositories.UserRepository;
using System;
using System.Threading.Tasks;

namespace ProductsBusinessLayerTests
{
    public class RegistrationServiceTests
    {
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<IEmailRepository> _emailRepositoryMock;
        private Mock<ISmtpService> _smtpServiceMock;
        private IRegistrationService _registrationService;
        private Fixture _fixtute;
        private IMapper _mapper;

        [OneTimeSetUp]
        public void Initialize()
        {
            var mapperConfigurationExpression =
               new MapperConfigurationExpression();
            mapperConfigurationExpression
                .AddMaps(typeof(ProductsProfile).Assembly);
            var mapperConfiguration =
                new MapperConfiguration(mapperConfigurationExpression);
            mapperConfiguration.AssertConfigurationIsValid();
            _mapper = new Mapper(mapperConfiguration);

            _fixtute = new Fixture();
        }

        [SetUp]
        public void Setup()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _emailRepositoryMock = new Mock<IEmailRepository>();
            _smtpServiceMock = new Mock<ISmtpService>();
            _registrationService = new RegistrationService(
                _userRepositoryMock.Object,
                _emailRepositoryMock.Object,
                _smtpServiceMock.Object,
                _mapper);
        }

        [Test]
        public async Task ConfirmEmail_WhenMessageSendTheSameAsInBase_ShouldConfirmEmail()
        {
            var inputMail = "asd";
            var inputMessage = "qwe";
            _emailRepositoryMock.Setup(x =>
                x.GetConfirmMessageAsync(inputMail))
                .ReturnsAsync(inputMessage)
                .Verifiable();
            _emailRepositoryMock.Setup(x =>
                x.ConfirmEmailAsync(inputMail))
                .Verifiable();
            //Act
            var actualResult =
                await _registrationService.ConfirmEmailAsync(
                    inputMail,
                    inputMessage);

            _emailRepositoryMock.Verify();
            Assert.True(actualResult);
        }

        [Test]
        public async Task ConfirmEmail_WhenMessageSendDiffersFromBase_ShouldFailEmailConfirmation()
        {
            var inputMail = "asd";
            var inputMessage = "qwe";
            var bdMessage = "tttt";
            _emailRepositoryMock.Setup(x =>
                x.GetConfirmMessageAsync(inputMail))
                .ReturnsAsync(bdMessage)
                .Verifiable();

            var actualResult =
                await _registrationService.ConfirmEmailAsync(
                    inputMail,
                    inputMessage);

            _emailRepositoryMock.Verify(service =>
                service.ConfirmEmailAsync(It.IsAny<string>()), Times.Never);
            _emailRepositoryMock.Verify();
            Assert.False(actualResult);
        }

        [Test]
        public async Task RegisterUserAsync_WhenUniqueUserWithoutEmailPassed_ShouldRegisterUser()
        {
            var dto = _fixtute
                .Build<AccountInfoDTO>()
                .Without(x => x.Email)
                .Create();
            var expectedUserGuid = Guid.NewGuid();
            _userRepositoryMock
                .Setup(x => x.AddUserAsync(It.IsAny<AccountInfo>()))
                .ReturnsAsync(expectedUserGuid)
                .Verifiable();

            var actualUserGuid = await _registrationService.RegisterUserAsync(dto, string.Empty);

            _userRepositoryMock.Verify();
            _emailRepositoryMock.Verify(x => x.RegisterEmailAsync(It.IsAny<Email>()), Times.Never);
            _smtpServiceMock.Verify(x => x.SendMailAsync(It.IsAny<MailDTO>()), Times.Never);
            Assert.AreEqual(expectedUserGuid, actualUserGuid);
        }

        [Test]
        public async Task RegisterUserAsync_WhenUniqueUserWithEmailPassed_ShouldRegisterUserAndSendEmail()
        {
            var dto = _fixtute.Create<AccountInfoDTO>();
            var expectedUserGuid = Guid.NewGuid();
            var emailId = _fixtute.Create<int>();
            _userRepositoryMock
                .Setup(x => x.AddUserAsync(It.IsAny<AccountInfo>()))
                .ReturnsAsync(expectedUserGuid)
                .Verifiable();
            _emailRepositoryMock
                .Setup(x => x.RegisterEmailAsync(It.IsAny<Email>()))
                .ReturnsAsync(emailId)
                .Verifiable();
            _smtpServiceMock
                .Setup(x => x.SendMailAsync(It.IsAny<MailDTO>()))
                .Verifiable();

            var actualUserGuid = await _registrationService.RegisterUserAsync(dto, string.Empty);

            _userRepositoryMock.Verify();
            _emailRepositoryMock.Verify();
            _smtpServiceMock.Verify();
            Assert.AreEqual(expectedUserGuid, actualUserGuid);
        }
    }
}