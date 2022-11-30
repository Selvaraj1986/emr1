using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;

namespace emr.Models.DBContext
{
    public class EmrDbContext : DbContext
    {
        public EmrDbContext(DbContextOptions<EmrDbContext> options) : base(options)
        {
        }

        public DbSet<people> people { get; set; } = null!;
        public DbSet<courses> courses { get; set; } = null!;
        public DbSet<course_people> course_people { get; set; } = null!;
        public DbSet<roles> roles { get; set; } = null!;
        public DbSet<access_logs> access_logs { get; set; } = null!;
        public DbSet<medications> medications { get; set; } = null!;
        public DbSet<dosages> dosages { get; set; } = null!;
        public DbSet<providers> providers { get; set; } = null!;
        public DbSet<options> options { get; set; } = null!;
        public DbSet<patients> patients { get; set; } = null!;
        public DbSet<admitting_diagnoses> admitting_diagnoses { get; set; } = null!;
        public DbSet<code_statuses> code_statuses { get; set; } = null!;
        public DbSet<precautions> precautions { get; set; } = null!;
        public DbSet<allergies> allergies { get; set; } = null!;
        public DbSet<patient_medications> patient_medications { get; set; } = null!;
        public DbSet<rooms> rooms { get; set; } = null!;
        public DbSet<heights> heights { get; set; } = null!;
        public DbSet<weights> weights { get; set; } = null!;
        public DbSet<daily_activities> daily_activities { get; set; } = null!;
        public DbSet<treatments> treatments { get; set; } = null!;
        public DbSet<consults> consults { get; set; } = null!;
        public DbSet<dietaries> dietaries { get; set; } = null!;
        public DbSet<provider_orders> provider_orders { get; set; } = null!;
        public DbSet<notes> notes { get; set; } = null!;
        public DbSet<admissions> admissions { get; set; } = null!;
        public DbSet<ob_admissions> ob_admissions { get; set; } = null!;
    }

}

