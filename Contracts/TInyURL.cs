using System.Runtime.Serialization;

namespace Contracts
{
    [DataContract]
    public class TinyUrl
    {
        [DataMember] private int Id { get; set; }
        [DataMember] private string Name { get; set; }
    }
}