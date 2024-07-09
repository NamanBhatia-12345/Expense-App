using ExpenseApp.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ExpenseApp.Infrastructure.DbContext
{
    public class ExpenseDbContext : IdentityDbContext<ApplicationUser>
    {
        public ExpenseDbContext(DbContextOptions<ExpenseDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);

            var adminRoleId = "5ad93cd7-35e6-4fc7-9690-714f86ec8ef2";
            var userRoleId = "ac5e271a-005b-4ec8-8bdd-86571bdcdb1a";
            var roles = new List<IdentityRole>
            {
                 new IdentityRole
                 {
                     Id = userRoleId,
                     ConcurrencyStamp = userRoleId,
                     Name = "User",
                     NormalizedName = "User".ToUpper()
                 },
                 new IdentityRole
                 {
                     Id = adminRoleId,
                     ConcurrencyStamp = adminRoleId,
                     Name = "Admin",
                     NormalizedName = "Admin".ToUpper()
                 }
            };

            builder.Entity<IdentityRole>().HasData(roles);

            var hasher = new PasswordHasher<ApplicationUser>();
            var admin = new List<ApplicationUser>
            {
                new ApplicationUser
                {
                    Id = "41377029-b399-409c-8da2-7a4bcf802978",
                    UserName = "admin01@gmail.com",
                    NormalizedUserName = "ADMIN01@GMAIL.COM",
                    Email = "admin01@gmail.com",
                    NormalizedEmail = "ADMIN01@GMAIL.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "Admin@123"),
                    SecurityStamp = "41377029-b399-409c-8da2-7a4bcf802978",
                    PhoneNumber = "9851234567",
                    FullName = "Admin",
                    Role = "Admin"
                },
                new ApplicationUser
                {
                    Id = "9c8c7ba1-9f91-4ee4-8d47-fac0125dc74c",
                    UserName = "admin02@gmail.com",
                    NormalizedUserName = "ADMIN02@GMAIL.COM",
                    Email = "admin02@gmail.com",
                    NormalizedEmail = "ADMIN02@GMAIL.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "Admin@123"),
                    SecurityStamp = "9c8c7ba1-9f91-4ee4-8d47-fac0125dc74c",
                    PhoneNumber = "9851232351",
                    FullName = "Admin",
                    Role = "Admin"
                }
            };
           
            builder.Entity<ApplicationUser>().HasData(admin);

            var users = new List<ApplicationUser>
            {
                new ApplicationUser
                {
                    Id = "ad014415-a368-4a32-9351-a8abf2485393",
                    UserName = "nitin@gmail.com",
                    NormalizedUserName = "NITIN@GMAIL.COM",
                    Email = "nitin@gmail.com",
                    NormalizedEmail = "NITIN@GMAIL.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "User@123"),
                    SecurityStamp = "ad014415-a368-4a32-9351-a8abf2485393",
                    FullName = "Nitin Kumar",
                    PhoneNumber = "8851114567",
                    Role = "User"
                },
                new ApplicationUser
                {
                    Id = "6e3fccd2-60fb-4090-b281-33f0405d6a45",
                    UserName = "rohit@gmail.com",
                    NormalizedUserName = "ROHIT@GMAIL.COM",
                    Email = "rohit@gmail.com",
                    NormalizedEmail = "ROHIT@GMAIL.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "User@123"),
                    SecurityStamp = "6e3fccd2-60fb-4090-b281-33f0405d6a45",
                    PhoneNumber = "7853454569",
                    FullName = "Rohit Sharma",
                    Role = "User"
                },
                new ApplicationUser
                {
                    Id = "9bba7a43-19df-46d5-97ad-b1cf29053c02",
                    UserName = "rahul@gmail.com",
                    NormalizedUserName = "RAHUL@GMAIL.COM",
                    Email = "rahul@gmail.com",
                    NormalizedEmail = "RAHUL@GMAIL.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "User@123"),
                    SecurityStamp = "9bba7a43-19df-46d5-97ad-b1cf29053c02",
                    PhoneNumber = "9921184560",
                    FullName = "Rahul Tiwari",
                    Role = "User"
                },
                new ApplicationUser
                {
                    Id = "279e30e5-426d-449e-86c8-c2a89ffc1ada",
                    UserName = "ishita@gmail.com",
                    NormalizedUserName = "ISHITA@GMAIL.COM",
                    Email = "ishita@gmail.com",
                    NormalizedEmail = "ISHITA@GMAIL.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "User@123"),
                    SecurityStamp = "279e30e5-426d-449e-86c8-c2a89ffc1ada",
                    PhoneNumber = "6851114378",
                    FullName = "Ishita Roy",
                    Role = "User"
                },
                new ApplicationUser
                {
                    Id = "4732b433-fd9c-48d3-8cb3-eccee797cb0d",
                    UserName = "khushi@gmail.com",
                    NormalizedUserName = "KHUSHI@GMAIL.COM",
                    Email = "khushi@gmail.com",
                    NormalizedEmail = "KHUSHI@GMAIL.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "User@123"),
                    SecurityStamp = "4732b433-fd9c-48d3-8cb3-eccee797cb0d",
                    PhoneNumber = "7920012980",
                    FullName = "Khushi Seth",
                    Role = "User"
                },
                new ApplicationUser
                {
                    Id = "77311c10-f548-4e65-8bd5-5df2dd774c1c",
                    UserName = "abhi@gmail.com",
                    NormalizedUserName = "ABHI@GMAIL.COM",
                    Email = "abhi@gmail.com",
                    NormalizedEmail = "ABHI@GMAIL.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "User@123"),
                    SecurityStamp = "77311c10-f548-4e65-8bd5-5df2dd774c1c",
                    PhoneNumber = "6642714567",
                    FullName = "Abhi Verma",
                    Role = "User"
                }
            };
            builder.Entity<ApplicationUser>().HasData(users);

            var userRoles = new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string>
                {
                    UserId = admin[0].Id,
                    RoleId = adminRoleId
                },
                new IdentityUserRole<string>
                {
                    UserId = admin[1].Id,
                    RoleId = adminRoleId
                },
                new IdentityUserRole<string>
                {
                    UserId = users[0].Id,
                    RoleId = userRoleId
                },
                new IdentityUserRole<string>
                {
                    UserId = users[1].Id,
                    RoleId = userRoleId
                },
                new IdentityUserRole<string>
                {
                    UserId = users[2].Id,
                    RoleId = userRoleId
                },
                new IdentityUserRole<string>
                {
                    UserId = users[3].Id,
                    RoleId = userRoleId
                },
                new IdentityUserRole<string>
                {
                    UserId = users[4].Id,
                    RoleId = userRoleId
                },
                new IdentityUserRole<string>
                {
                    UserId = users[5].Id,
                    RoleId = userRoleId
                }
            };
            builder.Entity<IdentityUserRole<string>>().HasData(userRoles);

            builder.Entity<GroupMembers>()
                .HasKey(gm => new { gm.UserId, gm.GroupId });

            builder.Entity<ExpenseSplit>()
                .HasKey(ep => new { ep.UserId, ep.ExpenseId });

            builder.Entity<Group>()
                .HasMany(g => g.Members)
                .WithOne(gm => gm.Group)
                .HasForeignKey(gm => gm.GroupId);

            builder.Entity<Group>()
                .HasMany(g => g.Expenses)
                .WithOne(e => e.Group)
                .HasForeignKey(e => e.GroupId);

            builder.Entity<Expense>()
               .HasMany(e => e.Splits)
               .WithOne(ep => ep.Expense)
               .HasForeignKey(ep => ep.ExpenseId);

        }
        // Define the DbSets
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupMembers> GroupMembers { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<ExpenseSplit> ExpenseSplits { get; set; }
    }
}
