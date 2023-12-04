using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kindergarten.DAL.EntityTypeConfigurations
{
    public class ChildConfiguration : IEntityTypeConfiguration<Child>
    {
        public void Configure(EntityTypeBuilder<Child> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();
            builder.Property(c => c.FullName).IsRequired();
            builder.Property(c => c.Sex).IsRequired();
            builder.Property(c => c.BirthDate).IsRequired();
            builder.Property(c => c.FatherFullName).IsRequired();
            builder.Property(c => c.MotherFullName).IsRequired();

            builder.HasOne(c => c.Group)
                   .WithMany(g => g.Members)
                   .HasPrincipalKey(g => g.Id)
                   .HasForeignKey(c => c.GroupId);

            builder.HasData(GetChildrenArray());
        }

        private Child[] GetChildrenArray()
        {
            var random = new Random();
            var children = new Child[200];
            for (int i = 0; i < children.Length; i++)
            {
                children[i] = new Child()
                {
                    Id = 1000 + i,
                    FullName = DataGeneration.GetRandomFullName(random),
                    FatherFullName = DataGeneration.GetRandomFullName(random),
                    MotherFullName = DataGeneration.GetRandomFullName(random),
                    BirthDate = DataGeneration.GetRandomDate(16, 20),
                    Sex = random.Next(0, 2),
                    GroupId = random.Next(100, 108)
                };
            }

            return children;
        }
    }
}
