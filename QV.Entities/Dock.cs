using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Repository.Pattern.Ef6;

namespace QV.Data.Models
{
    [DataContract]
    public partial class Dock : Entity
    {
        public Dock()
        {
            this.DockDetails = new List<DockDetail>();
        }
        [DataMember]
        public int DockId { get; set; }
        [DataMember]
        public bool Active { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public Nullable<int> Sequence { get; set; }
        [DataMember]
        public string Type { get; set; }
        [DataMember]
        [Required(ErrorMessage = "SiteID is required")]
        public int SiteId { get; set; }

        public virtual Site Site { get; set; }
        [DataMember]
        public virtual ICollection<DockDetail> DockDetails { get; set; }
    }
}
