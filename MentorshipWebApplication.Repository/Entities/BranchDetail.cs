using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MentorshipWebApplication.Repository.Entities
{
    public partial class BranchDetail
    {
        public BranchDetail()
        {
            Audits = new HashSet<Audit>();
        }
        [Required]
        public string BranchId { get; set; } = null!;
        public string? Address { get; set; }
        public string? Location { get; set; }
        public byte[]? BranchManagerName { get; set; }

        public virtual ICollection<Audit> Audits { get; set; }
    }
}
