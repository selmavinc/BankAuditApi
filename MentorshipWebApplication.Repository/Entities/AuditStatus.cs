using System;
using System.Collections.Generic;

namespace MentorshipWebApplication.Repository.Entities
{
    public partial class AuditStatus
    {
        public AuditStatus()
        {
            Audits = new HashSet<Audit>();
        }

        public int AuditStatusId { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<Audit> Audits { get; set; }
    }
}
