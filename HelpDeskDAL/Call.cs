namespace HelpDeskDAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Call : HelpdeskEntity
    {
        public int Id { get; set; }

        public int EmployeeId { get; set; }

        public int ProblemId { get; set; }

        public int TechId { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime DateOpened { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? DateClosed { get; set; }

        public bool OpenStatus { get; set; }

        [Required]
        [StringLength(250)]
        public string Notes { get; set; }

        [Column(TypeName = "timestamp")]
        [MaxLength(8)]
        [Timestamp]
        public byte[] Timer { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual Problem Problem { get; set; }

        public virtual Employee Employee1 { get; set; }
    }
}
