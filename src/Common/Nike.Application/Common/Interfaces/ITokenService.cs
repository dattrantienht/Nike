using System.Threading.Tasks;

namespace Nike.Application.Common.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateJwtSecurityToken(string id);
    }
}
