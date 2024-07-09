using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace ExpenseApp.Infrastructure.Migrations
{
    public partial class AddTablesForExpenseDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false),
                    Name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false),
                    FullName = table.Column<string>(type: "longtext", nullable: true),
                    Role = table.Column<string>(type: "longtext", nullable: true),
                    UserName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    PasswordHash = table.Column<string>(type: "longtext", nullable: true),
                    SecurityStamp = table.Column<string>(type: "longtext", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "longtext", nullable: true),
                    PhoneNumber = table.Column<string>(type: "longtext", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: true),
                    Description = table.Column<string>(type: "longtext", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(type: "varchar(255)", nullable: false),
                    ClaimType = table.Column<string>(type: "longtext", nullable: true),
                    ClaimValue = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false),
                    ClaimType = table.Column<string>(type: "longtext", nullable: true),
                    ClaimValue = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "varchar(255)", nullable: false),
                    ProviderKey = table.Column<string>(type: "varchar(255)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "longtext", nullable: true),
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false),
                    RoleId = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false),
                    LoginProvider = table.Column<string>(type: "varchar(255)", nullable: false),
                    Name = table.Column<string>(type: "varchar(255)", nullable: false),
                    Value = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Expenses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(type: "longtext", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    PaidUserBy = table.Column<string>(type: "longtext", nullable: true),
                    Issettled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    GroupId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Expenses_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "GroupMembers",
                columns: table => new
                {
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupMembers", x => new { x.UserId, x.GroupId });
                    table.ForeignKey(
                        name: "FK_GroupMembers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupMembers_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ExpenseSplits",
                columns: table => new
                {
                    ExpenseId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false),
                    FullName = table.Column<string>(type: "longtext", nullable: true),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseSplits", x => new { x.UserId, x.ExpenseId });
                    table.ForeignKey(
                        name: "FK_ExpenseSplits_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExpenseSplits_Expenses_ExpenseId",
                        column: x => x.ExpenseId,
                        principalTable: "Expenses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5ad93cd7-35e6-4fc7-9690-714f86ec8ef2", "5ad93cd7-35e6-4fc7-9690-714f86ec8ef2", "Admin", "ADMIN" },
                    { "ac5e271a-005b-4ec8-8bdd-86571bdcdb1a", "ac5e271a-005b-4ec8-8bdd-86571bdcdb1a", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Role", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "279e30e5-426d-449e-86c8-c2a89ffc1ada", 0, "bdce9963-3995-4740-8725-92459bcbea7b", "ishita@gmail.com", true, "Ishita Roy", false, null, "ISHITA@GMAIL.COM", "ISHITA@GMAIL.COM", "AQAAAAEAACcQAAAAEF2Ta9MLO/Q0q04XXTCfZ/knG9ByOcngR5kqBjz/9DWkYzaYAN1KDm7dAGvsEZ4p0g==", "6851114378", false, "User", "279e30e5-426d-449e-86c8-c2a89ffc1ada", false, "ishita@gmail.com" },
                    { "41377029-b399-409c-8da2-7a4bcf802978", 0, "08eddc47-f236-4d9c-be82-a6485fecb9b0", "admin01@gmail.com", true, "Admin", false, null, "ADMIN01@GMAIL.COM", "ADMIN01@GMAIL.COM", "AQAAAAEAACcQAAAAEOpmbFyu0ynN5ITADqz7LWqlOw/Y/g9t4QqNsDlPNvR5+g2KWCKznjCaY3Is74YYeQ==", "9851234567", false, "Admin", "41377029-b399-409c-8da2-7a4bcf802978", false, "admin01@gmail.com" },
                    { "4732b433-fd9c-48d3-8cb3-eccee797cb0d", 0, "f749a034-bb29-4ddf-a9d3-9d3c12e3be77", "khushi@gmail.com", true, "Khushi Seth", false, null, "KHUSHI@GMAIL.COM", "KHUSHI@GMAIL.COM", "AQAAAAEAACcQAAAAEGHoAaEPAraEOKtvlD5XmFESzt5ztXhOzrOPxYg7nevxUU9png07FFJ9KMiRogE7Lw==", "7920012980", false, "User", "4732b433-fd9c-48d3-8cb3-eccee797cb0d", false, "khushi@gmail.com" },
                    { "6e3fccd2-60fb-4090-b281-33f0405d6a45", 0, "819a3422-abba-4eba-93a4-ded9f282ff95", "rohit@gmail.com", true, "Rohit Sharma", false, null, "ROHIT@GMAIL.COM", "ROHIT@GMAIL.COM", "AQAAAAEAACcQAAAAENh5Sx7YEI1xbOjxeCSXHnG8rcnRDfWWTMxLzZhHFXpTbuL8ZXHWjwIpMZH0btYHuw==", "7853454569", false, "User", "6e3fccd2-60fb-4090-b281-33f0405d6a45", false, "rohit@gmail.com" },
                    { "77311c10-f548-4e65-8bd5-5df2dd774c1c", 0, "f625e892-e81c-4cc8-b9b6-6926492e2577", "abhi@gmail.com", true, "Abhi Verma", false, null, "ABHI@GMAIL.COM", "ABHI@GMAIL.COM", "AQAAAAEAACcQAAAAEGw2pRBjdWyst8sh/gBkFUPK7hSgD66oxziDyACkvI/yVELtMuKFnstvgpKYgxEMhg==", "6642714567", false, "User", "77311c10-f548-4e65-8bd5-5df2dd774c1c", false, "abhi@gmail.com" },
                    { "9bba7a43-19df-46d5-97ad-b1cf29053c02", 0, "9f613a62-e945-49eb-bb91-f557bfbfb0f3", "rahul@gmail.com", true, "Rahul Tiwari", false, null, "RAHUL@GMAIL.COM", "RAHUL@GMAIL.COM", "AQAAAAEAACcQAAAAEDYDdK1AZNqjiCa25IofdtcOGmeqaZrTWhNeGmWfwUf+gZeEIdJH7UD/kXJ21smDyA==", "9921184560", false, "User", "9bba7a43-19df-46d5-97ad-b1cf29053c02", false, "rahul@gmail.com" },
                    { "9c8c7ba1-9f91-4ee4-8d47-fac0125dc74c", 0, "01ccd8db-83c9-40c6-913e-c75add4d3921", "admin02@gmail.com", true, "Admin", false, null, "ADMIN02@GMAIL.COM", "ADMIN02@GMAIL.COM", "AQAAAAEAACcQAAAAEHEvSW7GaJ7ccs436UBPuRmEYqhSqme9cf8xFnr8VhW6qzSC9Ae9/0WAYRW0uEUZUQ==", "9851232351", false, "Admin", "9c8c7ba1-9f91-4ee4-8d47-fac0125dc74c", false, "admin02@gmail.com" },
                    { "ad014415-a368-4a32-9351-a8abf2485393", 0, "026dc7dc-218c-47c4-89a1-c3250fa9c6bc", "nitin@gmail.com", true, "Nitin Kumar", false, null, "NITIN@GMAIL.COM", "NITIN@GMAIL.COM", "AQAAAAEAACcQAAAAENuhCgS2m+BqG29h7zCWo2bALcSebb90fAgxeME+w9xuLbpX/WpZTdkzq6pQm1/rqQ==", "8851114567", false, "User", "ad014415-a368-4a32-9351-a8abf2485393", false, "nitin@gmail.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "ac5e271a-005b-4ec8-8bdd-86571bdcdb1a", "279e30e5-426d-449e-86c8-c2a89ffc1ada" },
                    { "5ad93cd7-35e6-4fc7-9690-714f86ec8ef2", "41377029-b399-409c-8da2-7a4bcf802978" },
                    { "ac5e271a-005b-4ec8-8bdd-86571bdcdb1a", "4732b433-fd9c-48d3-8cb3-eccee797cb0d" },
                    { "ac5e271a-005b-4ec8-8bdd-86571bdcdb1a", "6e3fccd2-60fb-4090-b281-33f0405d6a45" },
                    { "ac5e271a-005b-4ec8-8bdd-86571bdcdb1a", "77311c10-f548-4e65-8bd5-5df2dd774c1c" },
                    { "ac5e271a-005b-4ec8-8bdd-86571bdcdb1a", "9bba7a43-19df-46d5-97ad-b1cf29053c02" },
                    { "5ad93cd7-35e6-4fc7-9690-714f86ec8ef2", "9c8c7ba1-9f91-4ee4-8d47-fac0125dc74c" },
                    { "ac5e271a-005b-4ec8-8bdd-86571bdcdb1a", "ad014415-a368-4a32-9351-a8abf2485393" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_GroupId",
                table: "Expenses",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseSplits_ExpenseId",
                table: "ExpenseSplits",
                column: "ExpenseId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupMembers_GroupId",
                table: "GroupMembers",
                column: "GroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "ExpenseSplits");

            migrationBuilder.DropTable(
                name: "GroupMembers");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Expenses");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Groups");
        }
    }
}
