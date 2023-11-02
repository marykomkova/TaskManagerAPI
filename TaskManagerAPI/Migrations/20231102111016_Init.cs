using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TaskManagerAPI.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfCreation = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfCreation = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaskTags",
                columns: table => new
                {
                    TaskId = table.Column<int>(type: "int", nullable: false),
                    TagId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskTags", x => new { x.TaskId, x.TagId });
                    table.ForeignKey(
                        name: "FK_TaskTags_Tages_TagId",
                        column: x => x.TagId,
                        principalTable: "Tages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskTags_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Tages",
                columns: new[] { "Id", "DateOfCreation", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 11, 2, 14, 10, 16, 693, DateTimeKind.Local).AddTicks(8809), "Work" },
                    { 2, new DateTime(2023, 11, 2, 14, 10, 16, 693, DateTimeKind.Local).AddTicks(8824), "Self-development" },
                    { 3, new DateTime(2023, 11, 2, 14, 10, 16, 693, DateTimeKind.Local).AddTicks(8825), "Part-time job" },
                    { 4, new DateTime(2023, 11, 2, 14, 10, 16, 693, DateTimeKind.Local).AddTicks(8826), "Home" },
                    { 5, new DateTime(2023, 11, 2, 14, 10, 16, 693, DateTimeKind.Local).AddTicks(8826), "Immediately" }
                });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "DateOfCreation", "Description", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 11, 2, 14, 10, 16, 693, DateTimeKind.Local).AddTicks(8924), "", "Programm" },
                    { 2, new DateTime(2023, 11, 2, 14, 10, 16, 693, DateTimeKind.Local).AddTicks(8925), "", "Training" },
                    { 3, new DateTime(2023, 11, 2, 14, 10, 16, 693, DateTimeKind.Local).AddTicks(8926), "", "Orders" },
                    { 4, new DateTime(2023, 11, 2, 14, 10, 16, 693, DateTimeKind.Local).AddTicks(8927), "", "Cleaning" }
                });

            migrationBuilder.InsertData(
                table: "TaskTags",
                columns: new[] { "TagId", "TaskId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 5, 1 },
                    { 2, 2 },
                    { 3, 3 },
                    { 4, 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaskTags_TagId",
                table: "TaskTags",
                column: "TagId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaskTags");

            migrationBuilder.DropTable(
                name: "Tages");

            migrationBuilder.DropTable(
                name: "Tasks");
        }
    }
}
