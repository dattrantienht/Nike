using System.Threading.Tasks;
using Nike.Application.Common.Models;

namespace Nike.Application.Common.Interfaces
{
    public interface IEmailService
    {
        Task SendAsync(EmailRequest request);
    }
}
