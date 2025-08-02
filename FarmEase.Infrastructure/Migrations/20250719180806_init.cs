using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FarmEase.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Farms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Sqft = table.Column<int>(type: "int", nullable: false),
                    Occupancy = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Farms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
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
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
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
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                });

            migrationBuilder.CreateTable(
                name: "Amenities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FarmId = table.Column<int>(type: "int", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    Created_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_Date = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Amenities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Amenities_Farms_FarmId",
                        column: x => x.FarmId,
                        principalTable: "Farms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FarmId = table.Column<int>(type: "int", nullable: false),
                    CheckInDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CheckOutDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BookingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalCost = table.Column<double>(type: "float", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nights = table.Column<int>(type: "int", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bookings_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bookings_Farms_FarmId",
                        column: x => x.FarmId,
                        principalTable: "Farms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FarmRooms",
                columns: table => new
                {
                    FarmRoom_Number = table.Column<int>(type: "int", nullable: false),
                    FarmId = table.Column<int>(type: "int", nullable: false),
                    SpecialDetails = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_Date = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FarmRooms", x => x.FarmRoom_Number);
                    table.ForeignKey(
                        name: "FK_FarmRooms_Farms_FarmId",
                        column: x => x.FarmId,
                        principalTable: "Farms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3b614794-de4e-4be5-af7d-4544393d4897", null, "customer", "CUSTOMER" },
                    { "a62f2248-5e0c-4b10-8f80-54f625067caf", null, "admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "Farms",
                columns: new[] { "Id", "Created_Date", "Description", "ImageUrl", "IsAvailable", "IsDelete", "Name", "Occupancy", "Price", "Sqft", "Updated_Date" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 7, 19, 23, 38, 5, 658, DateTimeKind.Local).AddTicks(1057), "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.", "C:\\Users\\kishankumar01\\Downloads\\FarmEase\\FarmEase.WebAPI\\images\\VillaImage\\a78075cc-2905-4471-a578-1d5fe3945fcd.jpg", true, false, "Royal Farm", 4, 200.0, 550, null },
                    { 2, new DateTime(2025, 7, 19, 23, 38, 5, 658, DateTimeKind.Local).AddTicks(1065), "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.", "C:\\Users\\kishankumar01\\Downloads\\FarmEase\\FarmEase.WebAPI\\images\\VillaImage\\cac7e1b4-5ba4-493f-9b73-14434d5b56d7.jpg", true, false, "Premium Pool Farm", 4, 300.0, 550, null },
                    { 3, new DateTime(2025, 7, 19, 23, 38, 5, 658, DateTimeKind.Local).AddTicks(1069), "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.", "C:\\Users\\kishankumar01\\Downloads\\FarmEase\\FarmEase.WebAPI\\images\\VillaImage\\d75cb0ad-9ed1-46af-87bc-fa99e00b2e58.jpg", true, false, "Luxury Pool Farm", 4, 400.0, 750, null }
                });

            migrationBuilder.InsertData(
                table: "Amenities",
                columns: new[] { "Id", "Created_Date", "Description", "FarmId", "IsAvailable", "IsDelete", "Name", "Updated_Date" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 7, 19, 23, 38, 5, 658, DateTimeKind.Local).AddTicks(8), null, 1, true, false, "Private Pool", null },
                    { 2, new DateTime(2025, 7, 19, 23, 38, 5, 658, DateTimeKind.Local).AddTicks(28), null, 1, true, false, "Microwave", null },
                    { 3, new DateTime(2025, 7, 19, 23, 38, 5, 658, DateTimeKind.Local).AddTicks(29), null, 1, true, false, "Private Balcony", null },
                    { 5, new DateTime(2025, 7, 19, 23, 38, 5, 658, DateTimeKind.Local).AddTicks(31), null, 2, true, false, "Private Plunge Pool", null },
                    { 6, new DateTime(2025, 7, 19, 23, 38, 5, 658, DateTimeKind.Local).AddTicks(32), null, 2, true, false, "Microwave and Mini Refrigerator", null },
                    { 7, new DateTime(2025, 7, 19, 23, 38, 5, 658, DateTimeKind.Local).AddTicks(33), null, 2, true, false, "Private Balcony", null },
                    { 9, new DateTime(2025, 7, 19, 23, 38, 5, 658, DateTimeKind.Local).AddTicks(73), null, 3, true, false, "Private Pool", null },
                    { 10, new DateTime(2025, 7, 19, 23, 38, 5, 658, DateTimeKind.Local).AddTicks(75), null, 3, true, false, "Jacuzzi", null },
                    { 11, new DateTime(2025, 7, 19, 23, 38, 5, 658, DateTimeKind.Local).AddTicks(76), null, 3, true, false, "Private Balcony", null }
                });

            migrationBuilder.InsertData(
                table: "FarmRooms",
                columns: new[] { "FarmRoom_Number", "Created_Date", "FarmId", "SpecialDetails", "Updated_Date" },
                values: new object[,]
                {
                    { 101, new DateTime(2025, 7, 19, 23, 38, 5, 658, DateTimeKind.Local).AddTicks(1733), 1, "Master bedroom", null },
                    { 102, new DateTime(2025, 7, 19, 23, 38, 5, 658, DateTimeKind.Local).AddTicks(1736), 1, "has 3 bed", null },
                    { 103, new DateTime(2025, 7, 19, 23, 38, 5, 658, DateTimeKind.Local).AddTicks(1737), 1, "has 3 bed", null },
                    { 104, new DateTime(2025, 7, 19, 23, 38, 5, 658, DateTimeKind.Local).AddTicks(1738), 1, "has 2 bed", null },
                    { 201, new DateTime(2025, 7, 19, 23, 38, 5, 658, DateTimeKind.Local).AddTicks(1739), 2, "Master bedroom", null },
                    { 202, new DateTime(2025, 7, 19, 23, 38, 5, 658, DateTimeKind.Local).AddTicks(1740), 2, "has 2 bed", null },
                    { 203, new DateTime(2025, 7, 19, 23, 38, 5, 658, DateTimeKind.Local).AddTicks(1741), 2, "has 2 bed", null },
                    { 301, new DateTime(2025, 7, 19, 23, 38, 5, 658, DateTimeKind.Local).AddTicks(1742), 3, "Master bedroom", null },
                    { 302, new DateTime(2025, 7, 19, 23, 38, 5, 658, DateTimeKind.Local).AddTicks(1743), 3, "has 2 bed", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Amenities_FarmId",
                table: "Amenities",
                column: "FarmId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

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
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_FarmId",
                table: "Bookings",
                column: "FarmId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_UserId",
                table: "Bookings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FarmRooms_FarmId",
                table: "FarmRooms",
                column: "FarmId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Amenities");

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
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "FarmRooms");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Farms");
        }
    }
}
