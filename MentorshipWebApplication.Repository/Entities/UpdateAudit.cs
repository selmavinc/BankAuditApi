using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentorshipWebApplication.Repository.Entities
{
    public partial class UpdateAudit
    {
        [Required]
        public int AuditId { get; set; }
        [Required]
        public string? BranchId { get; set; }
        public int? LeadExaminerId { get; set; }
        public string? LeadExaminerName { get; set; }
        public int? AssociateExaminerId { get; set; }
        public string? AssociateExaminerName { get; set; }
        public DateTime? AuditDate { get; set; }
        public decimal? AuditHours { get; set; }
        public int? AuditStatusId { get; set; }
        public string? AuditStatuses { get; set; }
    }
}
