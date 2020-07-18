namespace ELearningManagementSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web;

    public partial class Lecture
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(40)]
        public string Course_Code { get; set; }

        [Key]
        [Column("Lecture", Order = 1)]
        [StringLength(20)]
        public string Lecture1 { get; set; }

        public string PDF { get; set; }
        [NotMapped]
        public byte[] FileContent { get; set; }
        [NotMapped]
        public HttpPostedFileBase File { get; set; }
        public string Video { get; set; }
        [NotMapped]
        public HttpPostedFileBase Vid { get; set; }
        public string Links { get; set; }

        public string Audio { get; set; }
        [NotMapped]
        public HttpPostedFileBase Aud { get; set; }

        public bool? IsVisible { get; set; }

        public virtual Cours Cours { get; set; }
    }
}
