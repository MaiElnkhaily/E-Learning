namespace ELearningManagementSystem.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ELearning : DbContext
    {
        public ELearning()
            : base("name=ELearning")
        {
        }

        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Assignment> Assignments { get; set; }
        public virtual DbSet<Chat> Chats { get; set; }
        //public virtual DbSet<Event> Course_Schedule { get; set; }
        public virtual DbSet<Cours> Courses { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Exercise> Exercises { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<Lecture> Lectures { get; set; }
        public virtual DbSet<MCQ> MCQs { get; set; }
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<Professor> Professors { get; set; }
        public virtual DbSet<Result> Results { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Student_Courses> Student_Courses { get; set; }
        public virtual DbSet<Student_Exam> Student_Exam { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<True_False> True_False { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>()
                .Property(e => e.Type)
                .IsUnicode(false);

            modelBuilder.Entity<Assignment>()
                .Property(e => e.Course_Code)
                .IsUnicode(false);

            modelBuilder.Entity<Chat>()
                .Property(e => e.Course_Code)
                .IsUnicode(false);

            

            modelBuilder.Entity<Cours>()
                .Property(e => e.Course_Code)
                .IsUnicode(false);

            modelBuilder.Entity<Cours>()
                .HasMany(e => e.Exercises)
                .WithRequired(e => e.Cours)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Cours>()
                .HasMany(e => e.Lectures)
                .WithRequired(e => e.Cours)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Cours>()
                .HasMany(e => e.Student_Courses)
                .WithRequired(e => e.Cours)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Department>()
                .HasMany(e => e.Professors)
                .WithOptional(e => e.Department)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Exercise>()
                .Property(e => e.Course_Code)
                .IsUnicode(false);

            modelBuilder.Entity<Exercise>()
                .Property(e => e.StartHour)
                .HasPrecision(1);

            modelBuilder.Entity<Exercise>()
                .Property(e => e.EndHour)
                .HasPrecision(1);

            modelBuilder.Entity<Exercise>()
                .HasMany(e => e.MCQs)
                .WithOptional(e => e.Exercise)
                .HasForeignKey(e => new { e.Cource_Code, e.Title_Id });

            modelBuilder.Entity<Exercise>()
                .HasMany(e => e.Student_Exam)
                .WithOptional(e => e.Exercise)
                .HasForeignKey(e => new { e.Course_Code, e.Title });

            modelBuilder.Entity<Exercise>()
                .HasMany(e => e.True_False)
                .WithOptional(e => e.Exercise)
                .HasForeignKey(e => new { e.Course_Id, e.Title_Id });

            modelBuilder.Entity<Lecture>()
                .Property(e => e.Course_Code)
                .IsUnicode(false);

            modelBuilder.Entity<MCQ>()
                .Property(e => e.Cource_Code)
                .IsUnicode(false);

            modelBuilder.Entity<Professor>()
                .Property(e => e.Type)
                .IsUnicode(false);

            modelBuilder.Entity<Professor>()
                .HasMany(e => e.Chats)
                .WithOptional(e => e.Professor)
                .HasForeignKey(e => e.User_Id);

            modelBuilder.Entity<Result>()
                .Property(e => e.Course_code)
                .IsUnicode(false);

            modelBuilder.Entity<Student>()
                .Property(e => e.Type)
                .IsUnicode(false);

            modelBuilder.Entity<Student>()
                .HasMany(e => e.Chats)
                .WithOptional(e => e.Student)
                .HasForeignKey(e => e.User_Id);

            modelBuilder.Entity<Student>()
                .HasMany(e => e.Student_Courses)
                .WithRequired(e => e.Student)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Student_Courses>()
                .Property(e => e.Course_Code)
                .IsUnicode(false);

            modelBuilder.Entity<Student_Exam>()
                .Property(e => e.Course_Code)
                .IsUnicode(false);

            modelBuilder.Entity<True_False>()
                .Property(e => e.Course_Id)
                .IsUnicode(false);
        }
    }
}
