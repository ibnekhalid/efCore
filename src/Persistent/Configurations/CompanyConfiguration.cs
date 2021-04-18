using Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Core.Model;

namespace Persistent.Configurations
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> template)
        {
            template.ToTable("Company");
            template.HasIndex(e => e.Id).IsUnique();
            template.Property(e => e.Id).HasColumnName("CompanyID");
            template.Property(e => e.Status).HasConversion(s => (byte)s, s => (State)s);
            template.Property(e => e.Name).HasMaxLength(20);

           
            template.HasMany(d => d.Projects)
                .WithOne(p => p.Company).HasForeignKey(x => x.CompanyId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
