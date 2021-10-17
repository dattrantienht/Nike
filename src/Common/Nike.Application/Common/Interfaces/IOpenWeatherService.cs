using System.Threading;
using System.Threading.Tasks;
using Nike.Application.Common.Models;
using Nike.Application.ExternalServices.OpenWeather.Request;
using Nike.Application.ExternalServices.OpenWeather.Response;

namespace Nike.Application.Common.Interfaces
{
    public interface IOpenWeatherService
    {
        Task<ServiceResult<OpenWeatherResponse>> GetCurrentWeatherForecast(OpenWeatherRequest request,
            CancellationToken cancellationToken);
    }
}