using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kindergarten.DAL.EntityTypeConfigurations
{
    public class KindergartenTeacherConfiguration : IEntityTypeConfiguration<KindergartenTeacher>
    {
        public void Configure(EntityTypeBuilder<KindergartenTeacher> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).ValueGeneratedOnAdd();
            builder.Property(t => t.FullName).IsRequired();
            builder.Property(t => t.Address).IsRequired();
            builder.Property(t => t.PhoneNumber).IsRequired();
            builder.Property(t => t.ExperienceInfo);
            builder.Property(t => t.AwardsInfo);

            builder.HasMany(t => t.Groups)
                   .WithOne(g => g.KindergartenTeacher)
                   .HasPrincipalKey(g => g.Id)
                   .HasForeignKey(t => t.KindergartenTeacherId);

            builder.HasData(GetKindergartenTeachersArray());
        }

        private KindergartenTeacher[] GetKindergartenTeachersArray()
        {
            var random = new Random();
            var teachers = new KindergartenTeacher[10];
            for (int i = 0; i < teachers.Length; i++)
            {
                teachers[i] = new KindergartenTeacher()
                {
                    Id = 100 + i,
                    FullName = DataGeneration.GetRandomFullName(random),
                    PhoneNumber = DataGeneration.GetRandomPhoneNumber(random),
                    ExperienceInfo = random.Next(5, 10).ToString() + " лет опыта",
                    Address = DataGeneration.GetRandomAddress(random, "Гомель")
                };
            }

            return teachers;
        }
    }
}
