using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace PasswordManager.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string? Family { get; set; }
        public AccountType UserType { get; set; }
    }

    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(p => p.Id).IsRequired();
            builder.Property(p => p.UserName).IsRequired();
            builder.Property(p => p.Password).IsRequired();
            builder.Property(p => p.Name).IsRequired();
        }
    }

    public enum AccountType
    {
        Admin = 1,

        User = 2
    }
}
