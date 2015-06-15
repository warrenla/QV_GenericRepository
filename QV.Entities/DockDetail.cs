using System;
using System.Collections.Generic;
using Repository.Pattern.Ef6;

namespace QV.Data.Models
{
    public partial class DockDetail : Entity
    {
        public int DockDetailId { get; set; }
        public int DockId { get; set; }
        public string Key { get; set; }
        public string Data { get; set; }
       
        public virtual Dock Dock { get; set; }
    }
}
