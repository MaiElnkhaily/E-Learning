namespace ELearningManagementSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Student")]
    public partial class Student
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Student()
        {
            Chats = new HashSet<Chat>();
            Results = new HashSet<Result>();
            Student_Courses = new HashSet<Student_Courses>();
            Student_Exam = new HashSet<Student_Exam>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Student_Code { get; set; }

        [StringLength(40)]
        public string E_Mail { get; set; }

        [StringLength(40)]
        public string UserName { get; set; }

        [StringLength(15)]
        public string Gender { get; set; }

        //public int Calss { get; set; }

        public int? Dept_Id { get; set; }

        [Column(TypeName = "date")]
        public DateTime? BirthDate { get; set; }

        [StringLength(40)]
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "Password doesn't match !!")]
        [NotMapped]
        public string CPassword { get; set; }

        public int? Admin_Id { get; set; }

        public string Photo { get; set; }

        [StringLength(200)]
        public string Personal_Name { get; set; }

        public int? Attendence { get; set; }

        public bool? Visible { get; set; }

        [StringLength(50)]
        public string Type { get; set; }

        [NotMapped]
        public string Course { get; set; }

        public virtual Admin Admin { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Chat> Chats { get; set; }

        public virtual Department Department { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Result> Results { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Student_Courses> Student_Courses { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Student_Exam> Student_Exam { get; set; }
    }
}
