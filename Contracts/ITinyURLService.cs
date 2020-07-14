using Microsoft.ServiceFabric.Services.Remoting;
using System;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ITinyURLService : IService
    {
        Task<string> CreateURLForService(string Url);
        Task<string> GetURLFromShortURL(string Url);
    }
}
