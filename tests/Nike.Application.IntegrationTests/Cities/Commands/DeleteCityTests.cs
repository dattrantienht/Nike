using System.Threading.Tasks;
using Nike.Application.Cities.Commands.Create;
using Nike.Application.Cities.Commands.Delete;
using Nike.Application.Common.Exceptions;
using Nike.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;
using static Nike.Application.IntegrationTests.Testing;

namespace Nike.Application.IntegrationTests.Cities.Commands
{
    public class DeleteCityTests : TestBase
    {
        [Test]
        public void ShouldRequireValidCityId()
        {
            var command = new DeleteCityCommand { Id = 99 };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().ThrowAsync<NotFoundException>();
        }

        [Test]
        public async Task ShouldDeleteCity()
        {
            var city = await SendAsync(new CreateCityCommand
            {
                Name = "Kayseri"
            });

            await SendAsync(new DeleteCityCommand
            {
                Id = city.Data.Id
            });

            var list = await FindAsync<City>(city.Data.Id);

            list.Should().BeNull();
        }
    }
}
