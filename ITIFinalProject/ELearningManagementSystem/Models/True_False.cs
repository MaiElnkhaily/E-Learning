namespace ELearningManagementSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class True_False
    {
        [StringLength(50)]
        public string Title_Id { get; set; }

        [Key]
        public int Question_Id { get; set; }

        [StringLength(40)]
        public string Course_Id { get; set; }

        public string Question_Text { get; set; }

        [StringLength(50)]
        public string Choice1 { get; set; }

        [StringLength(50)]
        public string Choice2 { get; set; }

        [StringLength(50)]
        public string CorrectAnswer { get; set; }

        public int? Weight { get; set; }

        public virtual Exercise Exercise { get; set; }
    }
}
