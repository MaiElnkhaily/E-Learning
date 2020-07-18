namespace ELearningManagementSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Assignment")]
    public partial class Assignment
    {
        [StringLength(40)]
        public string Course_Code { get; set; }

        [Key]
        public int Assignment_Id { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DateLine { get; set; }

        [StringLength(300)]
        public string Title { get; set; }

        public string Path { get; set; }

        [Column(TypeName = "date")]
        public DateTime? StartDate { get; set; }

        [StringLength(50)]
        public string StartHour { get; set; }

        [StringLength(50)]
        public string StartMinute { get; set; }

        [Column(TypeName = "date")]
        public DateTime? EndDate { get; set; }

        [StringLength(50)]
        public string EndHour { get; set; }

        [StringLength(50)]
        public string EndMinute { get; set; }

        public virtual Cours Cours { get; set; }
    }
}
