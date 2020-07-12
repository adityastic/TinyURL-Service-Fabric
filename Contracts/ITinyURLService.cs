using Microsoft.ServiceFabric.Services.Remoting;
using System;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ITinyURLService : IService
    {
        Task<TinyURLModel> CreateURLForService(string Url);
        Task<string> GetURLFromShortURL(int id);
    }
}
