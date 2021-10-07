using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Workplaces.Data.Migrations
{
    public partial class SeedDataMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Workplaces",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2021, 10, 7, 10, 59, 29, 450, DateTimeKind.Local).AddTicks(3156), null, "Web Developer", null },
                    { 2, new DateTime(2021, 10, 7, 10, 59, 29, 453, DateTimeKind.Local).AddTicks(9190), null, "QA Specialist", null },
                    { 3, new DateTime(2021, 10, 7, 10, 59, 29, 453, DateTimeKind.Local).AddTicks(9242), null, "Mobile Developer", null },
                    { 4, new DateTime(2021, 10, 7, 10, 59, 29, 453, DateTimeKind.Local).AddTicks(9251), null, "Full Stack Developer", null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Workplaces",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Workplaces",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Workplaces",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Workplaces",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
