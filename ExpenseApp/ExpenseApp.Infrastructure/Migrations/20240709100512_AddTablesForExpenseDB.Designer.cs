﻿// <auto-generated />
using System;
using ExpenseApp.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ExpenseApp.Infrastructure.Migrations
{
    [DbContext(typeof(ExpenseDbContext))]
    [Migration("20240709100512_AddTablesForExpenseDB")]
    partial class AddTablesForExpenseDB
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.31")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("ExpenseApp.Core.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("FullName")
                        .HasColumnType("longtext");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Role")
                        .HasColumnType("longtext");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "41377029-b399-409c-8da2-7a4bcf802978",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "08eddc47-f236-4d9c-be82-a6485fecb9b0",
                            Email = "admin01@gmail.com",
                            EmailConfirmed = true,
                            FullName = "Admin",
                            LockoutEnabled = false,
                            NormalizedEmail = "ADMIN01@GMAIL.COM",
                            NormalizedUserName = "ADMIN01@GMAIL.COM",
                            PasswordHash = "AQAAAAEAACcQAAAAEOpmbFyu0ynN5ITADqz7LWqlOw/Y/g9t4QqNsDlPNvR5+g2KWCKznjCaY3Is74YYeQ==",
                            PhoneNumber = "9851234567",
                            PhoneNumberConfirmed = false,
                            Role = "Admin",
                            SecurityStamp = "41377029-b399-409c-8da2-7a4bcf802978",
                            TwoFactorEnabled = false,
                            UserName = "admin01@gmail.com"
                        },
                        new
                        {
                            Id = "9c8c7ba1-9f91-4ee4-8d47-fac0125dc74c",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "01ccd8db-83c9-40c6-913e-c75add4d3921",
                            Email = "admin02@gmail.com",
                            EmailConfirmed = true,
                            FullName = "Admin",
                            LockoutEnabled = false,
                            NormalizedEmail = "ADMIN02@GMAIL.COM",
                            NormalizedUserName = "ADMIN02@GMAIL.COM",
                            PasswordHash = "AQAAAAEAACcQAAAAEHEvSW7GaJ7ccs436UBPuRmEYqhSqme9cf8xFnr8VhW6qzSC9Ae9/0WAYRW0uEUZUQ==",
                            PhoneNumber = "9851232351",
                            PhoneNumberConfirmed = false,
                            Role = "Admin",
                            SecurityStamp = "9c8c7ba1-9f91-4ee4-8d47-fac0125dc74c",
                            TwoFactorEnabled = false,
                            UserName = "admin02@gmail.com"
                        },
                        new
                        {
                            Id = "ad014415-a368-4a32-9351-a8abf2485393",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "026dc7dc-218c-47c4-89a1-c3250fa9c6bc",
                            Email = "nitin@gmail.com",
                            EmailConfirmed = true,
                            FullName = "Nitin Kumar",
                            LockoutEnabled = false,
                            NormalizedEmail = "NITIN@GMAIL.COM",
                            NormalizedUserName = "NITIN@GMAIL.COM",
                            PasswordHash = "AQAAAAEAACcQAAAAENuhCgS2m+BqG29h7zCWo2bALcSebb90fAgxeME+w9xuLbpX/WpZTdkzq6pQm1/rqQ==",
                            PhoneNumber = "8851114567",
                            PhoneNumberConfirmed = false,
                            Role = "User",
                            SecurityStamp = "ad014415-a368-4a32-9351-a8abf2485393",
                            TwoFactorEnabled = false,
                            UserName = "nitin@gmail.com"
                        },
                        new
                        {
                            Id = "6e3fccd2-60fb-4090-b281-33f0405d6a45",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "819a3422-abba-4eba-93a4-ded9f282ff95",
                            Email = "rohit@gmail.com",
                            EmailConfirmed = true,
                            FullName = "Rohit Sharma",
                            LockoutEnabled = false,
                            NormalizedEmail = "ROHIT@GMAIL.COM",
                            NormalizedUserName = "ROHIT@GMAIL.COM",
                            PasswordHash = "AQAAAAEAACcQAAAAENh5Sx7YEI1xbOjxeCSXHnG8rcnRDfWWTMxLzZhHFXpTbuL8ZXHWjwIpMZH0btYHuw==",
                            PhoneNumber = "7853454569",
                            PhoneNumberConfirmed = false,
                            Role = "User",
                            SecurityStamp = "6e3fccd2-60fb-4090-b281-33f0405d6a45",
                            TwoFactorEnabled = false,
                            UserName = "rohit@gmail.com"
                        },
                        new
                        {
                            Id = "9bba7a43-19df-46d5-97ad-b1cf29053c02",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "9f613a62-e945-49eb-bb91-f557bfbfb0f3",
                            Email = "rahul@gmail.com",
                            EmailConfirmed = true,
                            FullName = "Rahul Tiwari",
                            LockoutEnabled = false,
                            NormalizedEmail = "RAHUL@GMAIL.COM",
                            NormalizedUserName = "RAHUL@GMAIL.COM",
                            PasswordHash = "AQAAAAEAACcQAAAAEDYDdK1AZNqjiCa25IofdtcOGmeqaZrTWhNeGmWfwUf+gZeEIdJH7UD/kXJ21smDyA==",
                            PhoneNumber = "9921184560",
                            PhoneNumberConfirmed = false,
                            Role = "User",
                            SecurityStamp = "9bba7a43-19df-46d5-97ad-b1cf29053c02",
                            TwoFactorEnabled = false,
                            UserName = "rahul@gmail.com"
                        },
                        new
                        {
                            Id = "279e30e5-426d-449e-86c8-c2a89ffc1ada",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "bdce9963-3995-4740-8725-92459bcbea7b",
                            Email = "ishita@gmail.com",
                            EmailConfirmed = true,
                            FullName = "Ishita Roy",
                            LockoutEnabled = false,
                            NormalizedEmail = "ISHITA@GMAIL.COM",
                            NormalizedUserName = "ISHITA@GMAIL.COM",
                            PasswordHash = "AQAAAAEAACcQAAAAEF2Ta9MLO/Q0q04XXTCfZ/knG9ByOcngR5kqBjz/9DWkYzaYAN1KDm7dAGvsEZ4p0g==",
                            PhoneNumber = "6851114378",
                            PhoneNumberConfirmed = false,
                            Role = "User",
                            SecurityStamp = "279e30e5-426d-449e-86c8-c2a89ffc1ada",
                            TwoFactorEnabled = false,
                            UserName = "ishita@gmail.com"
                        },
                        new
                        {
                            Id = "4732b433-fd9c-48d3-8cb3-eccee797cb0d",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "f749a034-bb29-4ddf-a9d3-9d3c12e3be77",
                            Email = "khushi@gmail.com",
                            EmailConfirmed = true,
                            FullName = "Khushi Seth",
                            LockoutEnabled = false,
                            NormalizedEmail = "KHUSHI@GMAIL.COM",
                            NormalizedUserName = "KHUSHI@GMAIL.COM",
                            PasswordHash = "AQAAAAEAACcQAAAAEGHoAaEPAraEOKtvlD5XmFESzt5ztXhOzrOPxYg7nevxUU9png07FFJ9KMiRogE7Lw==",
                            PhoneNumber = "7920012980",
                            PhoneNumberConfirmed = false,
                            Role = "User",
                            SecurityStamp = "4732b433-fd9c-48d3-8cb3-eccee797cb0d",
                            TwoFactorEnabled = false,
                            UserName = "khushi@gmail.com"
                        },
                        new
                        {
                            Id = "77311c10-f548-4e65-8bd5-5df2dd774c1c",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "f625e892-e81c-4cc8-b9b6-6926492e2577",
                            Email = "abhi@gmail.com",
                            EmailConfirmed = true,
                            FullName = "Abhi Verma",
                            LockoutEnabled = false,
                            NormalizedEmail = "ABHI@GMAIL.COM",
                            NormalizedUserName = "ABHI@GMAIL.COM",
                            PasswordHash = "AQAAAAEAACcQAAAAEGw2pRBjdWyst8sh/gBkFUPK7hSgD66oxziDyACkvI/yVELtMuKFnstvgpKYgxEMhg==",
                            PhoneNumber = "6642714567",
                            PhoneNumberConfirmed = false,
                            Role = "User",
                            SecurityStamp = "77311c10-f548-4e65-8bd5-5df2dd774c1c",
                            TwoFactorEnabled = false,
                            UserName = "abhi@gmail.com"
                        });
                });

            modelBuilder.Entity("ExpenseApp.Core.Models.Expense", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<int>("GroupId")
                        .HasColumnType("int");

                    b.Property<bool>("Issettled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("PaidUserBy")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.ToTable("Expenses");
                });

            modelBuilder.Entity("ExpenseApp.Core.Models.ExpenseSplit", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("ExpenseId")
                        .HasColumnType("int");

                    b.Property<decimal>("Balance")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("FullName")
                        .HasColumnType("longtext");

                    b.HasKey("UserId", "ExpenseId");

                    b.HasIndex("ExpenseId");

                    b.ToTable("ExpenseSplits");
                });

            modelBuilder.Entity("ExpenseApp.Core.Models.Group", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("ExpenseApp.Core.Models.GroupMembers", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("GroupId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "GroupId");

                    b.HasIndex("GroupId");

                    b.ToTable("GroupMembers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "ac5e271a-005b-4ec8-8bdd-86571bdcdb1a",
                            ConcurrencyStamp = "ac5e271a-005b-4ec8-8bdd-86571bdcdb1a",
                            Name = "User",
                            NormalizedName = "USER"
                        },
                        new
                        {
                            Id = "5ad93cd7-35e6-4fc7-9690-714f86ec8ef2",
                            ConcurrencyStamp = "5ad93cd7-35e6-4fc7-9690-714f86ec8ef2",
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("RoleId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = "41377029-b399-409c-8da2-7a4bcf802978",
                            RoleId = "5ad93cd7-35e6-4fc7-9690-714f86ec8ef2"
                        },
                        new
                        {
                            UserId = "9c8c7ba1-9f91-4ee4-8d47-fac0125dc74c",
                            RoleId = "5ad93cd7-35e6-4fc7-9690-714f86ec8ef2"
                        },
                        new
                        {
                            UserId = "ad014415-a368-4a32-9351-a8abf2485393",
                            RoleId = "ac5e271a-005b-4ec8-8bdd-86571bdcdb1a"
                        },
                        new
                        {
                            UserId = "6e3fccd2-60fb-4090-b281-33f0405d6a45",
                            RoleId = "ac5e271a-005b-4ec8-8bdd-86571bdcdb1a"
                        },
                        new
                        {
                            UserId = "9bba7a43-19df-46d5-97ad-b1cf29053c02",
                            RoleId = "ac5e271a-005b-4ec8-8bdd-86571bdcdb1a"
                        },
                        new
                        {
                            UserId = "279e30e5-426d-449e-86c8-c2a89ffc1ada",
                            RoleId = "ac5e271a-005b-4ec8-8bdd-86571bdcdb1a"
                        },
                        new
                        {
                            UserId = "4732b433-fd9c-48d3-8cb3-eccee797cb0d",
                            RoleId = "ac5e271a-005b-4ec8-8bdd-86571bdcdb1a"
                        },
                        new
                        {
                            UserId = "77311c10-f548-4e65-8bd5-5df2dd774c1c",
                            RoleId = "ac5e271a-005b-4ec8-8bdd-86571bdcdb1a"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Value")
                        .HasColumnType("longtext");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("ExpenseApp.Core.Models.Expense", b =>
                {
                    b.HasOne("ExpenseApp.Core.Models.Group", "Group")
                        .WithMany("Expenses")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");
                });

            modelBuilder.Entity("ExpenseApp.Core.Models.ExpenseSplit", b =>
                {
                    b.HasOne("ExpenseApp.Core.Models.Expense", "Expense")
                        .WithMany("Splits")
                        .HasForeignKey("ExpenseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ExpenseApp.Core.Models.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Expense");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ExpenseApp.Core.Models.GroupMembers", b =>
                {
                    b.HasOne("ExpenseApp.Core.Models.Group", "Group")
                        .WithMany("Members")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ExpenseApp.Core.Models.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("ExpenseApp.Core.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("ExpenseApp.Core.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ExpenseApp.Core.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("ExpenseApp.Core.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ExpenseApp.Core.Models.Expense", b =>
                {
                    b.Navigation("Splits");
                });

            modelBuilder.Entity("ExpenseApp.Core.Models.Group", b =>
                {
                    b.Navigation("Expenses");

                    b.Navigation("Members");
                });
#pragma warning restore 612, 618
        }
    }
}