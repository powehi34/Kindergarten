using Entities;
using Kindergarten.DAL.EntityTypeConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Kindergarten.DAL
{
    public class KindergartenContext : DbContext
    {
        public DbSet<Child> Children { get; set; }
        public DbSet<KindergartenTeacher> Teachers { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupType> GroupTypes { get; set; }
        public KindergartenContext(DbContextOptions<KindergartenContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new GroupConfiguration());
            modelBuilder.ApplyConfiguration(new GroupTypeConfiguration());
            modelBuilder.ApplyConfiguration(new KindergartenTeacherConfiguration());
            modelBuilder.ApplyConfiguration(new ChildConfiguration());
        }
    }
}
