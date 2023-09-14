using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog_model.TypeConfigurations.Concrete
{
    public class IdentityRoleMap : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {

            // Identityrolemap bir kütüphane öğesi doğrudan oradan yakaladı bir data oluşturduk

            builder.HasData(new IdentityRole { Id = Guid.NewGuid().ToString(), Name = "Mamber", NormalizedName = "MAMBER" });
        }
    }
}
