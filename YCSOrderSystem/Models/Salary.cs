//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace YCSOrderSystem.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Salary
    {
        public int SalaryNum { get; set; }
        public decimal Amount { get; set; }
        public string Benefits { get; set; }
        public string Deductions { get; set; }
        public int StaffNum { get; set; }
        public string Description { get; set; }
    
        public virtual Staff Staff { get; set; }
    }
}
