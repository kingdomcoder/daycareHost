using Daycare.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Daycare.Domain.Repositories.Concrete {
    public class MyUserDbContext : IdentityDbContext<ApplicationUser> {
        public MyUserDbContext(DbContextOptions<MyUserDbContext> myOptions) : base(myOptions) { }

        public DbSet<Token> Token { get; set; }
        public DbSet<ApplicationUser> User { get; set; }  //....... MicrosoftAspNetCore.Identity.IdentityUser



        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
