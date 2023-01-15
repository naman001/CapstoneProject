using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using ClinicAPI.Models;


namespace ClinicAPI.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public virtual DbSet<Appointment> Appointments { get; set; }
        public virtual DbSet<Doctor> Doctors { get; set; }
        public virtual DbSet<ReservedSchedule> ReservedSchedules { get; set; }
        public virtual DbSet<TimeSlot> TimeSlots { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Doctor>().HasData(
                new Doctor { DocId = 1, DocName = "Doctor A" },
                new Doctor { DocId = 2, DocName = "Doctor B" },
                new Doctor { DocId = 3, DocName = "Doctor C" });
            modelBuilder.Entity<Patient>().HasData(
                new Patient { PatientId = 1, PatientName = "Mr.A" },
                new Patient { PatientId = 2, PatientName = "Mrs.B" });
            modelBuilder.Entity<TimeSlot>().HasData(
                new TimeSlot { SlotId=1, SlotDesc = "T08:00:00,T09:30:00" },
                new TimeSlot { SlotId=2, SlotDesc = "T09:30:00,T11:00:00" },
                new TimeSlot { SlotId=3, SlotDesc = "T11:00:00,T12:30:00" },
                new TimeSlot { SlotId=4, SlotDesc = "T14:00:00,T15:30:00" },
                new TimeSlot { SlotId=5, SlotDesc = "T15:30:00,T17:00:00" },
                new TimeSlot { SlotId=6, SlotDesc = "T17:00:00,T18:30:00" });
        }
        public DbSet<ClinicAPI.Models.Patient> Patient { get; set; }
    }
}
