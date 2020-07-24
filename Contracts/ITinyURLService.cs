using System.Threading.Tasks;
using Microsoft.ServiceFabric.Services.Remoting;

namespace Contracts
{
    public interface ITinyURLService : IService
    {
        Task<string> CreateURLForService(string Url);
        Task<string> GetURLFromShortURL(string Url);
    }
}