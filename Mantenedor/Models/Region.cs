//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Mantenedor.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Region
    {
        public Region()
        {
            this.Ciudad = new HashSet<Ciudad>();
        }
    
        public int IdRegion { get; set; }
        public string NombreRegion { get; set; }
    
        public virtual ICollection<Ciudad> Ciudad { get; set; }
    }
}
