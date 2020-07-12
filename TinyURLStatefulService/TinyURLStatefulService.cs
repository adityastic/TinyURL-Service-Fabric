using System;
using System.Collections.Generic;
using System.Fabric;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Contracts;
using Microsoft.ServiceFabric.Data.Collections;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Remoting.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;

namespace TinyURLStatefulService
{
    internal sealed class TinyURLStatefulService : StatefulService, ITinyURLService
    {
        public TinyURLStatefulService(StatefulServiceContext context)
            : base(context)
        { }

        public async Task<TinyURLModel> CreateURLForService(string Url)
        {
            var myDictionary = await this.StateManager.GetOrAddAsync<IReliableDictionary<int, string>>("tiny_db");
            
            using var tx = this.StateManager.CreateTransaction();
            var LatestID = GetLatestID();

            await myDictionary.AddOrUpdateAsync(tx, LatestID.Result, Url, (k, v) => v);

            await tx.CommitAsync();

            return new TinyURLModel(TinyURLUtils.IdToShortURL(LatestID.Result));
        }

        public async Task<int> GetLatestID()
        {
            var myDictionary = await this.StateManager.GetOrAddAsync<IReliableDictionary<string, int>>("id_counter");
            using var tx = this.StateManager.CreateTransaction();
            var result = await myDictionary.TryGetValueAsync(tx, "Counter");

            await myDictionary.AddOrUpdateAsync(tx, "Counter", 1, (key, value) => ++value);

            await tx.CommitAsync();

            if (result.HasValue)
                return (result.Value + 1);
            else
                return 1;
        }

        public async Task<string> GetURLFromShortURL(string url)
        {
            var myDictionary = await this.StateManager.GetOrAddAsync<IReliableDictionary<int, string>>("tiny_db");
            using var tx = this.StateManager.CreateTransaction();
            var result = await myDictionary.TryGetValueAsync(tx, TinyURLUtils.ShortURLtoID(url));

            if (result.HasValue)
                return result.Value;
            else
                return "www.facebook.com";
        }

        protected override IEnumerable<ServiceReplicaListener> CreateServiceReplicaListeners()
        {
            return this.CreateServiceRemotingReplicaListeners();
        }
    }
}
