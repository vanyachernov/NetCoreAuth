using Auth.Domain.Shared;
using Auth.Domain.UserManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auth.Infrastructure.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .ToTable("users");

        builder.ComplexProperty(u => u.FullName, ub =>
        {
            ub.Property(b => b.FirstName)
                .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH)
                .HasColumnName("first_name")
                .IsRequired();

            ub.Property(b => b.LastName)
                .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH)
                .HasColumnName("last_name")
                .IsRequired();
        });

        builder.ComplexProperty(u => u.IsDeleted, ub =>
        {
            ub.Property(b => b.Status).IsRequired();
        });
        
        builder.ComplexProperty(u => u.LastAuthAt, ub =>
        {
            ub.Property(b => b.Date).IsRequired();
        });
        
        builder.ComplexProperty(u => u.RegisterAt, ub =>
        {
            ub.Property(b => b.Date).IsRequired();
        });
    }
}