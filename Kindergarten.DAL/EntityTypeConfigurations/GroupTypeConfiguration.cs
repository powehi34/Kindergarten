using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kindergarten.DAL.EntityTypeConfigurations
{
    public class GroupTypeConfiguration : IEntityTypeConfiguration<GroupType>
    {
        private string[] _groupTypeNames = new string[] { "Ясельная", "Младшая", "Средняя", "Старшая", "Подготовительная" };
        public void Configure(EntityTypeBuilder<GroupType> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).ValueGeneratedOnAdd();
            builder.Property(t => t.Name).IsRequired();
            builder.Property(t => t.Description);

            builder.HasMany(t => t.Groups)
                   .WithOne(g => g.GroupType)
                   .HasPrincipalKey(g => g.Id)
                   .HasForeignKey(t => t.GroupTypeId);

            builder.HasData(GetGroupTypeArray());
        }

        private GroupType[] GetGroupTypeArray()
        {
            var groupTypes = new GroupType[_groupTypeNames.Length];
            for (int i = 0; i < _groupTypeNames.Length; i++)
                groupTypes[i] = new GroupType() { Id = i + 1, Name = _groupTypeNames[i] };
            return groupTypes;
        }
    }
}
