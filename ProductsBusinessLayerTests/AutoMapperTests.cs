using AutoFixture;
using AutoMapper;
using AutoMapper.Configuration;
using FluentAssertions;
using NUnit.Framework;
using ProductsBusinessLayer.DTOs;
using ProductsBusinessLayer.MapperProfiles;
using ProductsCore.Models;
using System;

namespace ProductsBusinessLayerTests
{
    public class AutoMapperTests
    {
        private IMapper _mapper;
        private Fixture _fixture;

        [OneTimeSetUp]
        public void Initialize()
        {
            _fixture = new Fixture();
            var mapperConfigurationExpression =
             new MapperConfigurationExpression();
            mapperConfigurationExpression
                .AddMaps(typeof(ProductsProfile).Assembly);
            var mapperConfiguration =
                new MapperConfiguration(mapperConfigurationExpression);
            mapperConfiguration.AssertConfigurationIsValid();
            _mapper = new Mapper(mapperConfiguration);
        }

        [Test]
        public void AccountInfoDTO_To_AccountInfo()
        {
            var accountInfoDTO = _fixture.Create<AccountInfoDTO>();

            var actualResult = _mapper.Map<AccountInfo>(accountInfoDTO);

            actualResult.Should().BeEquivalentTo(
                new
                {
                    accountInfoDTO.FirstName,
                    accountInfoDTO.LastName,
                    accountInfoDTO.LoginInfo,
                    IsActive = false,
                    Role = Role.User,
                    Email = default(Email),
                    EmailId = default(int?),
                    Id = default(Guid)
                });
        }
    }
}
