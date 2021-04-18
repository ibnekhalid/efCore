using Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Core.Model;

namespace Persistent.Configurations
{
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> template)
        {
            template.ToTable("Project");
            template.HasIndex(e => e.Id).IsUnique();
            template.Property(e => e.Id).HasColumnName("ProjectID");
            template.Property(e => e.CompanyId).HasColumnName("CompanyId");
            template.Property(e => e.Status).HasConversion(s => (byte)s, s => (State)s);
            template.Property(e => e.Title).HasMaxLength(20);

            template.HasMany(d => d.UserProjects)
              .WithOne(p => p.Project).HasForeignKey(x => x.UserId)
              .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
