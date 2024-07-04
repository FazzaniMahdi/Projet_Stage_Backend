using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobHosting.Migrations
{
    /// <inheritdoc />
    public partial class @this : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JobListers",
                columns: table => new
                {
                    ListerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ListerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ListerWebSite = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Listings = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobListers", x => x.ListerId);
                });

            migrationBuilder.CreateTable(
                name: "JobListings",
                columns: table => new
                {
                    JobId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JobDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    JobRequirements = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Missions = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JobLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JobHourlyPay = table.Column<double>(type: "float", nullable: false),
                    JobType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JobPositionsAvailable = table.Column<int>(type: "int", nullable: false),
                    JobsLister = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobListings", x => x.JobId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobListers");

            migrationBuilder.DropTable(
                name: "JobListings");
        }
    }
}
