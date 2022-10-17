using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MentorshipWebApplication.Repository.Entities
{
    public partial class Examiner
    {
        public Examiner()
        {
            AuditAssociateExaminers = new HashSet<Audit>();
            AuditLeadExaminers = new HashSet<Audit>();
        }

        
        public int ExaminerId { get; set; }
        [Required]
        [MinLength(4, ErrorMessage ="username must be min 4 char")]
        public string Username { get; set; } = null!;
        [Required]
        [MinLength(5, ErrorMessage = "Password must be min 5 char")]
        [MaxLength(15,ErrorMessage = "Password must be min 15 char")]
        public string Password { get; set; } = null!;
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public decimal? HoursAssigned { get; set; }
        [Required]
        public bool? IsActive { get; set; }

        public virtual ICollection<Audit> AuditAssociateExaminers { get; set; }
        public virtual ICollection<Audit> AuditLeadExaminers { get; set; }
    }
}
