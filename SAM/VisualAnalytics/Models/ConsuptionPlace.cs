//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VisualAnalytics.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ConsuptionPlace
    {
        public ConsuptionPlace()
        {
            this.Consuptions = new HashSet<Consuption>();
            this.ConsuptionsHourlies = new HashSet<ConsuptionsHourly>();
            this.ConsuptionsDailies = new HashSet<ConsuptionsDaily>();
        }
    
        public long IDConsuptionPlace { get; set; }
        public string ZipCode { get; set; }
        public string CityName { get; set; }
        public string DistrictName { get; set; }
    
        public virtual ICollection<Consuption> Consuptions { get; set; }
        public virtual ICollection<ConsuptionsHourly> ConsuptionsHourlies { get; set; }
        public virtual ICollection<ConsuptionsDaily> ConsuptionsDailies { get; set; }
    }
}
