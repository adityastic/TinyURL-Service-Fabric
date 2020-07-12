using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Contracts
{
    [DataContract]
    public class TinyURLModel
    {
        [DataMember]
        string ShortUrl { get; set; }

        public TinyURLModel(string url)
        {
            this.ShortUrl = url;
        }
    }
}
