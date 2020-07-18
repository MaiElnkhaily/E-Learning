using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ELearningManagementSystem.Models
{
    public partial class Prof_Courses
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(40)]
        public string Course_Code { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Prof_Id { get; set; }

        public virtual Cours Cours { get; set; }

        public virtual Professor Professor { get; set; }
    }
}