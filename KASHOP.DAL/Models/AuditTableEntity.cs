using System;
using System.Collections.Generic;
using System.Text;

namespace KASHOP.DAL.Models
{
    public class AuditTableEntity
    {
        public DateTime CreatedAt { get; set; }
        public string CreatedByID { get; set; } = null!;

        public ApplicationUser? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedByID { get; set; }
        public ApplicationUser? UpdatedBy { get; set; }


    }
}
