namespace ELearningManagementSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MCQ")]
    public partial class MCQ
    {
        [Key]
        public int Question_Id { get; set; }

        [StringLength(50)]
        public string Title_Id { get; set; }

        public string Question { get; set; }

        [StringLength(50)]
        public string CorrectAnswer { get; set; }

        public string Choice1 { get; set; }

        public string Choice2 { get; set; }

        public string Choice3 { get; set; }

        public string Choice4 { get; set; }

        [StringLength(40)]
        public string Cource_Code { get; set; }

        public bool? Checked { get; set; }

        public virtual Exercise Exercise { get; set; }
    }
}
