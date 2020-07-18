namespace ELearningManagementSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Student_Courses
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(40)]
        public string Course_Code { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Student_Code { get; set; }

        public int? Result { get; set; }

        public int? Academic_Year { get; set; }

        public virtual Cours Cours { get; set; }

        public virtual Student Student { get; set; }
    }
}
