using System;
using System.Threading.Tasks;
using Nike.Application.Cities.Commands.Create;
using Nike.Application.Common.Exceptions;
using Nike.Application.Common.Security;
using Nike.Application.Districts.Commands.Create;
using Nike.Application.Districts.Queries;
using FluentAssertions;
using NUnit.Framework;

namespace Nike.Application.IntegrationTests.Districts.Queries
{
    using static Testing;

    public class ExportDistrictsTests : TestBase
    {
        [Test]
        public void ShouldDenyAnonymousUser()
        {
            var query = new ExportDistrictsQuery();

            query.GetType().Should().BeDecoratedWith<AuthorizeAttribute>();

            FluentActions.Invoking(() =>
                SendAsync(query)).Should().ThrowAsync<UnauthorizedAccessException>();
        }

        [Test]
        public async Task ShouldDenyNonAdministrator()
        {
            await RunAsDefaultUserAsync();

            var query = new ExportDistrictsQuery();

            await FluentActions.Invoking(() =>
                SendAsync(query)).Should().ThrowAsync<ForbiddenAccessException>();
        }

        [Test]
        public async Task ShouldAllowAdministrator()
        {
            await RunAsAdministratorAsync();

            var city = await SendAsync(new CreateCityCommand
            {
                Name = "Çanakkale"
            });

            var result = await SendAsync(new CreateDistrictCommand
            {
                Name = "Çan",
                CityId = city.Data.Id
            });

            var query = new ExportDistrictsQuery
            {
                CityId = result.Data.Id
            };

            await FluentActions.Invoking(() => SendAsync(query))
                .Should().NotThrowAsync();
        }
    }
}
