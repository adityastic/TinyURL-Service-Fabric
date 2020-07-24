using System.Runtime.Serialization;

namespace Contracts
{
    [DataContract]
    public class TinyURLModel
    {
        public TinyURLModel(string url)
        {
            ShortUrl = url;
        }

        [DataMember] public string ShortUrl { get; set; }
    }
}