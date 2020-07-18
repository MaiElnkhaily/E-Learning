namespace ELearningManagementSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class News
    {
        [Key]
        public int News_Id { get; set; }

        [StringLength(500)]
        public string Adress { get; set; }

        public string Text { get; set; }

        public string Image { get; set; }

        [StringLength(500)]
        public string Active { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Date { get; set; }
    }
}
