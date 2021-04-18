using Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Core.Model;

namespace Persistent.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> template)
        {
            template.ToTable("User");
            template.HasIndex(e => e.Id).IsUnique();
            template.Property(e => e.Id).HasColumnName("UserID");
            template.Property(e => e.CompanyId).HasColumnName("CompanyID");
            template.Property(e => e.Status).HasConversion(s => (byte)s, s => (State)s);
            template.Property(e => e.Username).HasMaxLength(20);
            template.Property(e => e.Email).HasMaxLength(20);

            template.HasMany(d => d.UserProjects)
               .WithOne(p => p.User).HasForeignKey(x => x.UserId)
               .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
