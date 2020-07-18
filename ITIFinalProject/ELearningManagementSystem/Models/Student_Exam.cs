namespace ELearningManagementSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Student_Exam
    {
        [StringLength(50)]
        public string Title { get; set; }

        public int? Student_Code { get; set; }

        [Key]
        public int Answer_Id { get; set; }

        [StringLength(40)]
        public string Course_Code { get; set; }

        public int? Result { get; set; }

        [StringLength(60)]
        public string Time { get; set; }

        public virtual Cours Cours { get; set; }

        public virtual Exercise Exercise { get; set; }

        public virtual Student Student { get; set; }
    }
}
