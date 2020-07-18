 namespace ELearningManagementSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Professor")]
    public partial class Professor
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Professor()
        {
            Chats = new HashSet<Chat>();
            //Prof_Courses = new HashSet<Prof_Courses>();
            Courses = new HashSet<Cours>();
        }

        [Key]
        public int Prof_Id { get; set; }

        [StringLength(40)]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage ="Please Enter Valid Email")]
        [Required]
        public string E_Mail { get; set; }

        [StringLength(40)]
        [Required]
        public string UserName { get; set; }

        [StringLength(40)]
        [Required]
        public string Password { get; set; }

        [StringLength(40)]
        [NotMapped]
        
        [Compare("Password", ErrorMessage = "Password doesn't match !!")]
        public string CPassword { get; set; }

        [StringLength(40)]
        public string PersonalName { get; set; }

        [Column(TypeName = "date")]
        public DateTime? HireDate { get; set; }
        public string Photo { get; set; }

        [StringLength(50)]
        public string Type { get; set; }

        public string course_code { get; set; }

        public int? Admin_Id { get; set; }

        public int? Dept_Id { get; set; }


        public bool? Visible { get; set; }

        public virtual Admin Admin { get; set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Chat> Chats { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cours> Courses { get; set; }
        //public virtual Cours Cours { set; get; }

        public virtual Department Department { get; set; }
    }
}
