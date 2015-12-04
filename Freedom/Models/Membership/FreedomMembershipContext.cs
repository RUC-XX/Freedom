using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using Freedom.Models;

namespace Freedom
{
    public class FreedomMembershipContext : DbContext
    {
        public FreedomMembershipContext()
            : base("FreedomConnectionString")
        {
        }

        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<FreedomMembership> FreedomMemberships { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<PermissionsInRoles> PermissionsInRoles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FreedomMembership>()
              .HasMany<Role>(r => r.Roles)
              .WithMany(u => u.Members)
              .Map(m =>
              {
                  m.ToTable("webpages_UsersInRoles");
                  m.MapLeftKey("UserId");
                  m.MapRightKey("RoleId");
              });
        }
    }
}
