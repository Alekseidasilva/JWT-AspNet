using JwtStore.Core.Contexts.AccountContext.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JwtStore.Infra.Contexts.AccountContext.Mappings;

public class UserMap:IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("User");
        #region Id
        builder.HasKey(x => x.Id);
        #endregion
        #region Name
       

        builder.Property(x => x.Name)
            .HasColumnName("Name")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(120).IsRequired(true);
        #endregion
        #region Image
        builder.Property(x => x.Image)
            .HasColumnName("Image")
            .HasColumnType("VARCHAR")
            .HasMaxLength(120)
            .IsRequired(true);
        

        #endregion
        #region Email
        builder.OwnsOne(x=>x.Email)
            .Property(x => x.Address)
            .HasColumnName("Email")
            .IsRequired(true);

        builder.OwnsOne(x => x.Email)
            .OwnsOne(x => x.Verification)
            .Property(x=>x.Code)
            .HasColumnName("EmailVerificationCode")
            .IsRequired(true);
        
        builder.OwnsOne(x => x.Email)
            .OwnsOne(x => x.Verification)
            .Property(x=>x.ExpiresAt)
            .HasColumnName("EmailVerificationExpiredAt")
            .IsRequired(false);

        builder.OwnsOne(x => x.Email)
            .OwnsOne(x => x.Verification)
            .Property(x=>x.VerifiedAt)
            .HasColumnName("EmailVerificationVerificationAt")
            .IsRequired(false);

        builder.OwnsOne(x => x.Email)
            .OwnsOne(x => x.Verification)
            .Ignore(x => x.IsActive);
        #endregion
        #region Password
        builder.OwnsOne(x => x.Password)
            .Property(x => x.Hash)
            .HasColumnName("PasswordHash")
            .IsRequired();

        builder.OwnsOne(x => x.Password)
            .Property(x => x.ResetCode)
            .HasColumnName("PasswordResetCode")
            .IsRequired();
        

        #endregion
        
    }
}