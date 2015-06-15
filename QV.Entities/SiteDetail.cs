using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Repository.Pattern.Ef6;

namespace QV.Data.Models
{
    [DataContract]
    public partial class SiteDetail : Entity
    {
        [DataMember]
        public int SiteDetailId { get; set; }
        [DataMember]
        public int SiteId { get; set; }
        [DataMember]
        public string Key { get; set; }
        [DataMember]
        public string Data { get; set; }

        public virtual Site Site { get; set; }

    }
}
