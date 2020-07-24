using System.Collections.Generic;
using System.Fabric;
using System.Threading.Tasks;
using Contracts;
using Microsoft.ServiceFabric.Data.Collections;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Remoting.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;

namespace TinyURLStatefulService
{
    internal sealed class TinyUrlStatefulService : StatefulService, ITinyURLService
    {
        public TinyUrlStatefulService(StatefulServiceContext context)
            : base(context)
        {
        }

        public async Task<string> CreateURLForService(string url)
        {
            var myDictionary = await StateManager.GetOrAddAsync<IReliableDictionary<int, string>>("tiny_db");

            using var tx = StateManager.CreateTransaction();
            var latestId = GetLatestId();

            await myDictionary.AddOrUpdateAsync(tx, latestId.Result, url, (k, v) => v);

            await tx.CommitAsync();

            return TinyURLUtils.IdToShortURL(latestId.Result);
        }

        public async Task<string> GetURLFromShortURL(string url)
        {
            var myDictionary = await StateManager.GetOrAddAsync<IReliableDictionary<int, string>>("tiny_db");
            using var tx = StateManager.CreateTransaction();
            var result = await myDictionary.TryGetValueAsync(tx, TinyURLUtils.ShortURLtoID(url));

            if (result.HasValue)
                return result.Value;
            return "www.facebook.com";
        }

        private async Task<int> GetLatestId()
        {
            var myDictionary = await StateManager.GetOrAddAsync<IReliableDictionary<string, int>>("id_counter");
            using var tx = StateManager.CreateTransaction();
            var result = await myDictionary.TryGetValueAsync(tx, "Counter");

            await myDictionary.AddOrUpdateAsync(tx, "Counter", 1, (key, value) => ++value);

            await tx.CommitAsync();

            if (result.HasValue)
                return result.Value + 1;
            return 1;
        }

        protected override IEnumerable<ServiceReplicaListener> CreateServiceReplicaListeners()
        {
            return this.CreateServiceRemotingReplicaListeners();
        }
    }
}