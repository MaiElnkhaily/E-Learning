namespace ELearningManagementSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Chat")]
    public partial class Chat
    {
        public int Id { get; set; }

        public string message { get; set; }

        public int? User_Id { get; set; }

        [StringLength(40)]
        public string Course_Code { get; set; }

        public virtual Cours Cours { get; set; }

        public virtual Professor Professor { get; set; }

        public virtual Student Student { get; set; }
    }
}
