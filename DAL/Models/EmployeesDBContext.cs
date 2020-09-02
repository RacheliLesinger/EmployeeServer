using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DAL.Models
{
    public partial class EmployeesDBContext : DbContext
    {
        public EmployeesDBContext()
        {
        }

        public EmployeesDBContext(DbContextOptions<EmployeesDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Activity> Activity { get; set; }
        public virtual DbSet<DayReportType> DayReportType { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<HoursReport> HoursReport { get; set; }
        public virtual DbSet<UploadedProfile> UploadedProfile { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-49VMUKQ\\MSSQLSERVER01;Initial Catalog=EmployeesDB;Integrated Security=True");
               
            }
        }
      
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Activity>(entity =>
            {
                entity.Property(e => e.ActivityId).HasColumnName("activityId");

                entity.Property(e => e.ActivityDate)
                    .HasColumnName("activityDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.ActivityStatus).HasColumnName("activityStatus");

                entity.Property(e => e.EmployeeNumber).HasColumnName("employeeNumber");

                entity.HasOne(d => d.EmployeeNumberNavigation)
                    .WithMany(p => p.Activity)
                    .HasForeignKey(d => d.EmployeeNumber)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_activities");
            });

            modelBuilder.Entity<DayReportType>(entity =>
            {
                entity.Property(e => e.Value).HasColumnName("value");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.EmployeeNumber)
                    .HasName("PK_employees");

                entity.HasIndex(e => e.EmployeeId)
                    .HasName("UQ_employees")
                    .IsUnique();

                entity.Property(e => e.EmployeeNumber).HasColumnName("employeeNumber");

                entity.Property(e => e.DateAdded)
                    .HasColumnName("dateAdded")
                    .HasColumnType("datetime");

                entity.Property(e => e.Email).HasColumnName("email");

                entity.Property(e => e.EmployeeId)
                    .IsRequired()
                    .HasColumnName("employeeId")
                    .HasMaxLength(9);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("firstName")
                    .HasMaxLength(15);

                entity.Property(e => e.HoursPerDay).HasColumnName("hoursPerDay");

                entity.Property(e => e.ImageUrl)
                    .IsRequired()
                    .HasColumnName("imageUrl")
                    .HasMaxLength(90);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("lastName")
                    .HasMaxLength(25);

                entity.Property(e => e.MaximumExtraHours).HasColumnName("maximumExtraHours");

                entity.Property(e => e.NumUploadedProfiles).HasColumnName("numUploadedProfiles");

                entity.Property(e => e.Phone)
                    .HasColumnName("phone")
                    .HasMaxLength(15);
            });

            modelBuilder.Entity<HoursReport>(entity =>
            {
                entity.Property(e => e.Comment).HasColumnName("comment");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("date");

                entity.Property(e => e.DayReportType).HasColumnName("dayReportType");

                entity.Property(e => e.EmployeeNumber).HasColumnName("employeeNumber");

                entity.Property(e => e.TimeEnd).HasColumnName("timeEnd");

                entity.Property(e => e.TimeStart).HasColumnName("timeStart");

                entity.HasOne(d => d.DayReportTypeNavigation)
                    .WithMany(p => p.HoursReport)
                    .HasForeignKey(d => d.DayReportType)
                    .HasConstraintName("FK_HoursReport");

                entity.HasOne(d => d.EmployeeNumberNavigation)
                    .WithMany(p => p.HoursReport)
                    .HasForeignKey(d => d.EmployeeNumber)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Report_Employee");
            });

            modelBuilder.Entity<UploadedProfile>(entity =>
            {
                entity.HasKey(e => e.UploadedProfileNumber)
                    .HasName("PK_uploadedProfiles");

                entity.Property(e => e.UploadedProfileNumber)
                    .HasColumnName("uploadedProfileNumber")
                    .HasMaxLength(100);

                entity.Property(e => e.EmployeeNumber).HasColumnName("employeeNumber");

                entity.HasOne(d => d.EmployeeNumberNavigation)
                    .WithMany(p => p.UploadedProfile)
                    .HasForeignKey(d => d.EmployeeNumber)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_uploadedProfiles");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
