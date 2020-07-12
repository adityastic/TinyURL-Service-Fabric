using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Contracts
{
    [DataContract]
    public class TinyURL
    {
        [DataMember]
        int Id { get; set; }
        [DataMember]
        string Name { get; set; }
    }
}
