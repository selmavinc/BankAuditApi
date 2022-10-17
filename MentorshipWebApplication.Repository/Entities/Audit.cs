using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MentorshipWebApplication.Repository.Entities
{
    public partial class Audit
    {
        [Required]
        public int AuditId { get; set; }
        [Required]
        public string? BranchId { get; set; }
        public int? LeadExaminerId { get; set; }
        //public string? LeadExaminerName { get; set; }
        public int? AssociateExaminerId { get; set; }
        //public string? AssociateExaminerName { get; set; }
        public DateTime? AuditDate { get; set; }
        public decimal? AuditHours { get; set; }
        public int? AuditStatusId { get; set; }
        //public string? AuditStatuses { get; set; }

        public virtual Examiner? AssociateExaminer { get; set; }
        public virtual AuditStatus? AuditStatus { get; set; }
        public virtual BranchDetail? Branch { get; set; }
        public virtual Examiner? LeadExaminer { get; set; }
    }
}
