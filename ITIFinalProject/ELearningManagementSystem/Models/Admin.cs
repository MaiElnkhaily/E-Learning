namespace ELearningManagementSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Admin")]
    public partial class Admin
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Admin()
        {
            //Events = new HashSet<Event>();
            Professors = new HashSet<Professor>();
            Students = new HashSet<Student>();
        }

        [Key]
        public int Admin_Id { get; set; }

        [StringLength(40)]
        [EmailAddress(ErrorMessage ="Enter Valid Email!")]
        public string E_Mail { get; set; }

        [StringLength(40)]
        [Required]
        public string UserName { get; set; }

        [StringLength(40)]
        public string Password { get; set; }

        [NotMapped]
        [Compare("Password",ErrorMessage ="Password doesn't match!!")]
        public string CPassword { get; set; }

        [StringLength(50)]
        public string PersonalName { get; set; }

        public string Photo { get; set; }

        public bool? Active { get; set; }

        [StringLength(50)]
        public string Type { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Event> Events { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Professor> Professors { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Student> Students { get; set; }
    }
}
