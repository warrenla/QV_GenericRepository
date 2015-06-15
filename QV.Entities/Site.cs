using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Repository.Pattern.Ef6;

namespace QV.Data.Models
{
    [DataContract]
    public partial class Site : Entity
    {

        public Site()
        {
            this.Docks = new List<Dock>();
            this.SiteDetails = new List<SiteDetail>();
        }
        [DataMember]
        public int SiteId { get; set; }
        [DataMember]
        public Nullable<bool> Active { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public int Properties { get; set; }
        [DataMember]
        public string ShortName { get; set; }
        [DataMember]
        public string PropertyName { get; set; }
        [DataMember]
        public string Type { get; set; }



        public virtual ICollection<Dock> Docks { get; set; }
        [DataMember]
        public virtual ICollection<SiteDetail> SiteDetails { get; set; }
    }
}
