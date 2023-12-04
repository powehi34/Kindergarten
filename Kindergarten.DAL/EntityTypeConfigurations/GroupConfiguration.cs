using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kindergarten.DAL.EntityTypeConfigurations
{
    public class GroupConfiguration : IEntityTypeConfiguration<Group>
    {
        private string[] _groupNames = new string[] { "Звездочки", "Капельки", "Непоседы", "Пчелки", "Улыбка", "Буквоежка", "Ромашки", "Солнышко" };
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.HasKey(g => g.Id);
            builder.Property(g => g.Id).ValueGeneratedOnAdd();
            builder.Property(g => g.Name).IsRequired();
            builder.Property(g => g.CreationYear).IsRequired();

            builder.HasOne(g => g.GroupType)
                   .WithMany(t => t.Groups)
                   .HasPrincipalKey(t => t.Id)
                   .HasForeignKey(g => g.GroupTypeId);

            builder.HasOne(g => g.KindergartenTeacher)
                   .WithMany(t => t.Groups)
                   .HasPrincipalKey(t => t.Id)
                   .HasForeignKey(g => g.KindergartenTeacherId);

            builder.HasMany(g => g.Members)
                   .WithOne(c => c.Group)
                   .HasPrincipalKey(c => c.Id)
                   .HasForeignKey(g => g.GroupId);

            builder.HasData(GetGroupsArray());
        }

        private Group[] GetGroupsArray()
        {
            var random = new Random();
            var groups = new Group[_groupNames.Length];
            for (int i = 0; i < groups.Length; i++)
                groups[i] = new Group()
                {
                    Id = 100 + i,
                    Name = _groupNames[i],
                    GroupTypeId = random.Next(1, 6),
                    KindergartenTeacherId = random.Next(100, 110),
                    CreationYear = 2000 + random.Next(18, 24)
                };

            return groups;
        }
    }
}
