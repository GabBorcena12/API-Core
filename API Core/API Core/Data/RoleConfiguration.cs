using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API_Core.Data
{
    public class RoleConfiguration:IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder) {
            builder.HasData(
                    new IdentityRole
                    { 
                        Name = "Administrator",
                        NormalizedName = "ADMINISTRATOR"
                    },
                    new IdentityRole
                    {
                        Name = "User",
                        NormalizedName = "USER"
                    }
                );
        }
    }
}
