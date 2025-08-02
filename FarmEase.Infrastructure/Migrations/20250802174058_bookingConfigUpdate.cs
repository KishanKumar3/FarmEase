using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FarmEase.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class bookingConfigUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Bookings_FarmId",
                table: "Bookings");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3b614794-de4e-4be5-af7d-4544393d4897");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a62f2248-5e0c-4b10-8f80-54f625067caf");

            migrationBuilder.UpdateData(
                table: "Amenities",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created_Date",
                value: new DateTime(2025, 8, 2, 23, 10, 57, 803, DateTimeKind.Local).AddTicks(532));

            migrationBuilder.UpdateData(
                table: "Amenities",
                keyColumn: "Id",
                keyValue: 2,
                column: "Created_Date",
                value: new DateTime(2025, 8, 2, 23, 10, 57, 803, DateTimeKind.Local).AddTicks(550));

            migrationBuilder.UpdateData(
                table: "Amenities",
                keyColumn: "Id",
                keyValue: 3,
                column: "Created_Date",
                value: new DateTime(2025, 8, 2, 23, 10, 57, 803, DateTimeKind.Local).AddTicks(551));

            migrationBuilder.UpdateData(
                table: "Amenities",
                keyColumn: "Id",
                keyValue: 5,
                column: "Created_Date",
                value: new DateTime(2025, 8, 2, 23, 10, 57, 803, DateTimeKind.Local).AddTicks(552));

            migrationBuilder.UpdateData(
                table: "Amenities",
                keyColumn: "Id",
                keyValue: 6,
                column: "Created_Date",
                value: new DateTime(2025, 8, 2, 23, 10, 57, 803, DateTimeKind.Local).AddTicks(553));

            migrationBuilder.UpdateData(
                table: "Amenities",
                keyColumn: "Id",
                keyValue: 7,
                column: "Created_Date",
                value: new DateTime(2025, 8, 2, 23, 10, 57, 803, DateTimeKind.Local).AddTicks(554));

            migrationBuilder.UpdateData(
                table: "Amenities",
                keyColumn: "Id",
                keyValue: 9,
                column: "Created_Date",
                value: new DateTime(2025, 8, 2, 23, 10, 57, 803, DateTimeKind.Local).AddTicks(596));

            migrationBuilder.UpdateData(
                table: "Amenities",
                keyColumn: "Id",
                keyValue: 10,
                column: "Created_Date",
                value: new DateTime(2025, 8, 2, 23, 10, 57, 803, DateTimeKind.Local).AddTicks(597));

            migrationBuilder.UpdateData(
                table: "Amenities",
                keyColumn: "Id",
                keyValue: 11,
                column: "Created_Date",
                value: new DateTime(2025, 8, 2, 23, 10, 57, 803, DateTimeKind.Local).AddTicks(598));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4836821d-907e-435f-a8c8-783bd5b043d8", null, "admin", "ADMIN" },
                    { "eca2377b-097b-4aba-8c45-63fbc5aa6c16", null, "customer", "CUSTOMER" }
                });

            migrationBuilder.UpdateData(
                table: "FarmRooms",
                keyColumn: "FarmRoom_Number",
                keyValue: 101,
                column: "Created_Date",
                value: new DateTime(2025, 8, 2, 23, 10, 57, 803, DateTimeKind.Local).AddTicks(2990));

            migrationBuilder.UpdateData(
                table: "FarmRooms",
                keyColumn: "FarmRoom_Number",
                keyValue: 102,
                column: "Created_Date",
                value: new DateTime(2025, 8, 2, 23, 10, 57, 803, DateTimeKind.Local).AddTicks(2994));

            migrationBuilder.UpdateData(
                table: "FarmRooms",
                keyColumn: "FarmRoom_Number",
                keyValue: 103,
                column: "Created_Date",
                value: new DateTime(2025, 8, 2, 23, 10, 57, 803, DateTimeKind.Local).AddTicks(2995));

            migrationBuilder.UpdateData(
                table: "FarmRooms",
                keyColumn: "FarmRoom_Number",
                keyValue: 104,
                column: "Created_Date",
                value: new DateTime(2025, 8, 2, 23, 10, 57, 803, DateTimeKind.Local).AddTicks(2996));

            migrationBuilder.UpdateData(
                table: "FarmRooms",
                keyColumn: "FarmRoom_Number",
                keyValue: 201,
                column: "Created_Date",
                value: new DateTime(2025, 8, 2, 23, 10, 57, 803, DateTimeKind.Local).AddTicks(2997));

            migrationBuilder.UpdateData(
                table: "FarmRooms",
                keyColumn: "FarmRoom_Number",
                keyValue: 202,
                column: "Created_Date",
                value: new DateTime(2025, 8, 2, 23, 10, 57, 803, DateTimeKind.Local).AddTicks(2997));

            migrationBuilder.UpdateData(
                table: "FarmRooms",
                keyColumn: "FarmRoom_Number",
                keyValue: 203,
                column: "Created_Date",
                value: new DateTime(2025, 8, 2, 23, 10, 57, 803, DateTimeKind.Local).AddTicks(2998));

            migrationBuilder.UpdateData(
                table: "FarmRooms",
                keyColumn: "FarmRoom_Number",
                keyValue: 301,
                column: "Created_Date",
                value: new DateTime(2025, 8, 2, 23, 10, 57, 803, DateTimeKind.Local).AddTicks(2999));

            migrationBuilder.UpdateData(
                table: "FarmRooms",
                keyColumn: "FarmRoom_Number",
                keyValue: 302,
                column: "Created_Date",
                value: new DateTime(2025, 8, 2, 23, 10, 57, 803, DateTimeKind.Local).AddTicks(3000));

            migrationBuilder.UpdateData(
                table: "Farms",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created_Date",
                value: new DateTime(2025, 8, 2, 23, 10, 57, 803, DateTimeKind.Local).AddTicks(2387));

            migrationBuilder.UpdateData(
                table: "Farms",
                keyColumn: "Id",
                keyValue: 2,
                column: "Created_Date",
                value: new DateTime(2025, 8, 2, 23, 10, 57, 803, DateTimeKind.Local).AddTicks(2393));

            migrationBuilder.UpdateData(
                table: "Farms",
                keyColumn: "Id",
                keyValue: 3,
                column: "Created_Date",
                value: new DateTime(2025, 8, 2, 23, 10, 57, 803, DateTimeKind.Local).AddTicks(2395));

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_FarmId",
                table: "Bookings",
                column: "FarmId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Bookings_FarmId",
                table: "Bookings");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4836821d-907e-435f-a8c8-783bd5b043d8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "eca2377b-097b-4aba-8c45-63fbc5aa6c16");

            migrationBuilder.UpdateData(
                table: "Amenities",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created_Date",
                value: new DateTime(2025, 7, 19, 23, 38, 5, 658, DateTimeKind.Local).AddTicks(8));

            migrationBuilder.UpdateData(
                table: "Amenities",
                keyColumn: "Id",
                keyValue: 2,
                column: "Created_Date",
                value: new DateTime(2025, 7, 19, 23, 38, 5, 658, DateTimeKind.Local).AddTicks(28));

            migrationBuilder.UpdateData(
                table: "Amenities",
                keyColumn: "Id",
                keyValue: 3,
                column: "Created_Date",
                value: new DateTime(2025, 7, 19, 23, 38, 5, 658, DateTimeKind.Local).AddTicks(29));

            migrationBuilder.UpdateData(
                table: "Amenities",
                keyColumn: "Id",
                keyValue: 5,
                column: "Created_Date",
                value: new DateTime(2025, 7, 19, 23, 38, 5, 658, DateTimeKind.Local).AddTicks(31));

            migrationBuilder.UpdateData(
                table: "Amenities",
                keyColumn: "Id",
                keyValue: 6,
                column: "Created_Date",
                value: new DateTime(2025, 7, 19, 23, 38, 5, 658, DateTimeKind.Local).AddTicks(32));

            migrationBuilder.UpdateData(
                table: "Amenities",
                keyColumn: "Id",
                keyValue: 7,
                column: "Created_Date",
                value: new DateTime(2025, 7, 19, 23, 38, 5, 658, DateTimeKind.Local).AddTicks(33));

            migrationBuilder.UpdateData(
                table: "Amenities",
                keyColumn: "Id",
                keyValue: 9,
                column: "Created_Date",
                value: new DateTime(2025, 7, 19, 23, 38, 5, 658, DateTimeKind.Local).AddTicks(73));

            migrationBuilder.UpdateData(
                table: "Amenities",
                keyColumn: "Id",
                keyValue: 10,
                column: "Created_Date",
                value: new DateTime(2025, 7, 19, 23, 38, 5, 658, DateTimeKind.Local).AddTicks(75));

            migrationBuilder.UpdateData(
                table: "Amenities",
                keyColumn: "Id",
                keyValue: 11,
                column: "Created_Date",
                value: new DateTime(2025, 7, 19, 23, 38, 5, 658, DateTimeKind.Local).AddTicks(76));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3b614794-de4e-4be5-af7d-4544393d4897", null, "customer", "CUSTOMER" },
                    { "a62f2248-5e0c-4b10-8f80-54f625067caf", null, "admin", "ADMIN" }
                });

            migrationBuilder.UpdateData(
                table: "FarmRooms",
                keyColumn: "FarmRoom_Number",
                keyValue: 101,
                column: "Created_Date",
                value: new DateTime(2025, 7, 19, 23, 38, 5, 658, DateTimeKind.Local).AddTicks(1733));

            migrationBuilder.UpdateData(
                table: "FarmRooms",
                keyColumn: "FarmRoom_Number",
                keyValue: 102,
                column: "Created_Date",
                value: new DateTime(2025, 7, 19, 23, 38, 5, 658, DateTimeKind.Local).AddTicks(1736));

            migrationBuilder.UpdateData(
                table: "FarmRooms",
                keyColumn: "FarmRoom_Number",
                keyValue: 103,
                column: "Created_Date",
                value: new DateTime(2025, 7, 19, 23, 38, 5, 658, DateTimeKind.Local).AddTicks(1737));

            migrationBuilder.UpdateData(
                table: "FarmRooms",
                keyColumn: "FarmRoom_Number",
                keyValue: 104,
                column: "Created_Date",
                value: new DateTime(2025, 7, 19, 23, 38, 5, 658, DateTimeKind.Local).AddTicks(1738));

            migrationBuilder.UpdateData(
                table: "FarmRooms",
                keyColumn: "FarmRoom_Number",
                keyValue: 201,
                column: "Created_Date",
                value: new DateTime(2025, 7, 19, 23, 38, 5, 658, DateTimeKind.Local).AddTicks(1739));

            migrationBuilder.UpdateData(
                table: "FarmRooms",
                keyColumn: "FarmRoom_Number",
                keyValue: 202,
                column: "Created_Date",
                value: new DateTime(2025, 7, 19, 23, 38, 5, 658, DateTimeKind.Local).AddTicks(1740));

            migrationBuilder.UpdateData(
                table: "FarmRooms",
                keyColumn: "FarmRoom_Number",
                keyValue: 203,
                column: "Created_Date",
                value: new DateTime(2025, 7, 19, 23, 38, 5, 658, DateTimeKind.Local).AddTicks(1741));

            migrationBuilder.UpdateData(
                table: "FarmRooms",
                keyColumn: "FarmRoom_Number",
                keyValue: 301,
                column: "Created_Date",
                value: new DateTime(2025, 7, 19, 23, 38, 5, 658, DateTimeKind.Local).AddTicks(1742));

            migrationBuilder.UpdateData(
                table: "FarmRooms",
                keyColumn: "FarmRoom_Number",
                keyValue: 302,
                column: "Created_Date",
                value: new DateTime(2025, 7, 19, 23, 38, 5, 658, DateTimeKind.Local).AddTicks(1743));

            migrationBuilder.UpdateData(
                table: "Farms",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created_Date",
                value: new DateTime(2025, 7, 19, 23, 38, 5, 658, DateTimeKind.Local).AddTicks(1057));

            migrationBuilder.UpdateData(
                table: "Farms",
                keyColumn: "Id",
                keyValue: 2,
                column: "Created_Date",
                value: new DateTime(2025, 7, 19, 23, 38, 5, 658, DateTimeKind.Local).AddTicks(1065));

            migrationBuilder.UpdateData(
                table: "Farms",
                keyColumn: "Id",
                keyValue: 3,
                column: "Created_Date",
                value: new DateTime(2025, 7, 19, 23, 38, 5, 658, DateTimeKind.Local).AddTicks(1069));

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_FarmId",
                table: "Bookings",
                column: "FarmId",
                unique: true);
        }
    }
}
