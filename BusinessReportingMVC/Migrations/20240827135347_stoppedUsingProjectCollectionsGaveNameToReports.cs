using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessReportingMVC.Migrations
{
    /// <inheritdoc />
    public partial class stoppedUsingProjectCollectionsGaveNameToReports : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Project_Collection");

            migrationBuilder.AddColumn<string>(
                name: "ReportName",
                table: "Reports",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Project_Id",
                table: "Project_Individual",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Project_Individual_Project_Id",
                table: "Project_Individual",
                column: "Project_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Project_Individual_Projects_Project_Id",
                table: "Project_Individual",
                column: "Project_Id",
                principalTable: "Projects",
                principalColumn: "Project_Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Project_Individual_Projects_Project_Id",
                table: "Project_Individual");

            migrationBuilder.DropIndex(
                name: "IX_Project_Individual_Project_Id",
                table: "Project_Individual");

            migrationBuilder.DropColumn(
                name: "ReportName",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "Project_Id",
                table: "Project_Individual");

            migrationBuilder.CreateTable(
                name: "Project_Collection",
                columns: table => new
                {
                    Project_Collection_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Project_Id = table.Column<int>(type: "int", nullable: false),
                    Project_Individual_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Project___A86DC08A2FA6DD0A", x => x.Project_Collection_Id);
                    table.ForeignKey(
                        name: "FK_Parent_Project_Id",
                        column: x => x.Project_Id,
                        principalTable: "Projects",
                        principalColumn: "Project_Id");
                    table.ForeignKey(
                        name: "FK_Project_Individual_Id",
                        column: x => x.Project_Individual_Id,
                        principalTable: "Project_Individual",
                        principalColumn: "Project_Individual_Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Project_Collection_Project_Id",
                table: "Project_Collection",
                column: "Project_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Project_Collection_Project_Individual_Id",
                table: "Project_Collection",
                column: "Project_Individual_Id");
        }
    }
}
