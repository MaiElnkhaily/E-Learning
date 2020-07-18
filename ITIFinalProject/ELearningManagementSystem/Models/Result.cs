namespace ELearningManagementSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Result")]
    public partial class Result
    {
        public int Id { get; set; }

        public int? Student_code { get; set; }

        [StringLength(40)]
        public string Course_code { get; set; }

        public int? Exercises { get; set; }

        public string QType { get; set; }

        public virtual Cours Cours { get; set; }

        public virtual Student Student { get; set; }
    }
}
