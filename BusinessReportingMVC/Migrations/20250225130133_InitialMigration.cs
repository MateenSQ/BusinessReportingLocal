//using System;
//using Microsoft.EntityFrameworkCore.Migrations;

//#nullable disable

//namespace BusinessReportingMVC.Migrations
//{
//    /// <inheritdoc />
//    public partial class InitialMigration : Migration
//    {
//        /// <inheritdoc />
//        protected override void Up(MigrationBuilder migrationBuilder)
//        {
//            migrationBuilder.CreateTable(
//                name: "Admin_And_Resources",
//                columns: table => new
//                {
//                    Admin_And_Resources_Id = table.Column<long>(type: "bigint", nullable: false)
//                        .Annotation("SqlServer:Identity", "1, 1"),
//                    Points_Of_Note = table.Column<string>(type: "nvarchar(max)", nullable: true)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("PK__Admin_An__68A45A1D9A056FDD", x => x.Admin_And_Resources_Id);
//                });

//            migrationBuilder.CreateTable(
//                name: "Business_Development_Notes",
//                columns: table => new
//                {
//                    Business_Development_Notes_Id = table.Column<long>(type: "bigint", nullable: false)
//                        .Annotation("SqlServer:Identity", "1, 1"),
//                    Awareness_Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
//                    Intent_Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
//                    Tendered_0_33_Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
//                    Tendered_34_66_Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
//                    Tendered_67_100_Notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("PK__Business__183BA5824BDF1F67", x => x.Business_Development_Notes_Id);
//                });

//            migrationBuilder.CreateTable(
//                name: "Business_Development_Value",
//                columns: table => new
//                {
//                    Business_Development_Value_Id = table.Column<long>(type: "bigint", nullable: false)
//                        .Annotation("SqlServer:Identity", "1, 1"),
//                    Awareness_Value = table.Column<decimal>(type: "money", nullable: true),
//                    Intent_Value = table.Column<decimal>(type: "money", nullable: true),
//                    Tendered_0_33_Value = table.Column<decimal>(type: "money", nullable: true),
//                    Tendered_34_66_Value = table.Column<decimal>(type: "money", nullable: true),
//                    Tendered_67_100_Value = table.Column<decimal>(type: "money", nullable: true)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("PK__Business__3F0FE71E88EB7B09", x => x.Business_Development_Value_Id);
//                });

//            migrationBuilder.CreateTable(
//                name: "Claims",
//                columns: table => new
//                {
//                    Claim_Id = table.Column<long>(type: "bigint", nullable: false)
//                        .Annotation("SqlServer:Identity", "1, 1"),
//                    Claim_Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
//                    Claim_Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("PK__Claims__811C4A6D4D13DDD4", x => x.Claim_Id);
//                });

//            migrationBuilder.CreateTable(
//                name: "Financials_Actual",
//                columns: table => new
//                {
//                    Financials_Actual_Id = table.Column<long>(type: "bigint", nullable: false)
//                        .Annotation("SqlServer:Identity", "1, 1"),
//                    Turnover_Actual = table.Column<decimal>(type: "money", nullable: true),
//                    Direct_Costs_Actual = table.Column<decimal>(type: "money", nullable: true),
//                    Gross_Profit_Actual = table.Column<decimal>(type: "money", nullable: true),
//                    Indirect_Costs_Actual = table.Column<decimal>(type: "money", nullable: true),
//                    Net_Profit_Actual = table.Column<decimal>(type: "money", nullable: true),
//                    WIP_Actual = table.Column<decimal>(type: "money", nullable: true),
//                    Production_Hours_Quarter = table.Column<int>(type: "int", nullable: true),
//                    Utilisation_Quarter = table.Column<decimal>(type: "money", nullable: true),
//                    Work_In_Hand_Hours_Quarter = table.Column<int>(type: "int", nullable: true),
//                    Work_In_Hand_Money_Quarter = table.Column<decimal>(type: "money", nullable: true),
//                    Cash_Position_Actual = table.Column<decimal>(type: "money", nullable: true)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("PK__Financia__E203C6A1AB11E293", x => x.Financials_Actual_Id);
//                });

//            migrationBuilder.CreateTable(
//                name: "Financials_Deviation",
//                columns: table => new
//                {
//                    Financials_Deviation_Id = table.Column<long>(type: "bigint", nullable: false)
//                        .Annotation("SqlServer:Identity", "1, 1"),
//                    Turnover_Deviation = table.Column<decimal>(type: "money", nullable: true),
//                    Direct_Costs_Deviation = table.Column<decimal>(type: "money", nullable: true),
//                    Gross_Profit_Deviation = table.Column<decimal>(type: "money", nullable: true),
//                    Indirect_Costs_Deviation = table.Column<decimal>(type: "money", nullable: true),
//                    Net_Profit_Deviation = table.Column<decimal>(type: "money", nullable: true),
//                    WIP_Deviation = table.Column<decimal>(type: "money", nullable: true),
//                    Production_Hours_Deviation = table.Column<int>(type: "int", nullable: true),
//                    Utilisation_Quarter = table.Column<decimal>(type: "money", nullable: true),
//                    Work_In_Hand_Hours_Quarter = table.Column<int>(type: "int", nullable: true),
//                    Work_In_Hand_Money_Quarter = table.Column<decimal>(type: "money", nullable: true),
//                    Cash_Position_Forecast = table.Column<decimal>(type: "money", nullable: true)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("PK__Financia__54D108BB5795E2D6", x => x.Financials_Deviation_Id);
//                });

//            migrationBuilder.CreateTable(
//                name: "Key_Highlights",
//                columns: table => new
//                {
//                    Key_Highlights_Id = table.Column<long>(type: "bigint", nullable: false)
//                        .Annotation("SqlServer:Identity", "1, 1"),
//                    Performance = table.Column<string>(type: "nvarchar(max)", nullable: true),
//                    Risks = table.Column<string>(type: "nvarchar(max)", nullable: true),
//                    Opportunities = table.Column<string>(type: "nvarchar(max)", nullable: true)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("PK__Key_High__257931E987896825", x => x.Key_Highlights_Id);
//                });

//            migrationBuilder.CreateTable(
//                name: "Projects",
//                columns: table => new
//                {
//                    Project_Id = table.Column<long>(type: "bigint", nullable: false)
//                        .Annotation("SqlServer:Identity", "1, 1"),
//                    Forecast_Overall_Forecast = table.Column<decimal>(type: "money", nullable: true),
//                    Forecast_Overall_Deviation = table.Column<decimal>(type: "money", nullable: true)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("PK__Projects__1CB92E037A0932F6", x => x.Project_Id);
//                });

//            migrationBuilder.CreateTable(
//                name: "Strategy",
//                columns: table => new
//                {
//                    Strategy_Id = table.Column<long>(type: "bigint", nullable: false)
//                        .Annotation("SqlServer:Identity", "1, 1"),
//                    Business_Development = table.Column<string>(type: "nvarchar(max)", nullable: true),
//                    Innovation = table.Column<string>(type: "nvarchar(max)", nullable: true),
//                    Other = table.Column<string>(type: "nvarchar(max)", nullable: true)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("PK__Strategy__E5DD67A37457D499", x => x.Strategy_Id);
//                });

//            migrationBuilder.CreateTable(
//                name: "Users",
//                columns: table => new
//                {
//                    User_Id = table.Column<long>(type: "bigint", nullable: false)
//                        .Annotation("SqlServer:Identity", "1, 1"),
//                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
//                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
//                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
//                    Salt = table.Column<string>(type: "nvarchar(max)", nullable: false),
//                    Is_Approved = table.Column<bool>(type: "bit", nullable: false)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("PK__Users__206D9170FEAFF174", x => x.User_Id);
//                });

//            migrationBuilder.CreateTable(
//                name: "Business_Development",
//                columns: table => new
//                {
//                    Business_Development_Id = table.Column<long>(type: "bigint", nullable: false)
//                        .Annotation("SqlServer:Identity", "1, 1"),
//                    Business_Development_Value_Id = table.Column<long>(type: "bigint", nullable: false),
//                    Business_Development_Notes_Id = table.Column<long>(type: "bigint", nullable: false)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("PK__Business__6B072B835A8DBE11", x => x.Business_Development_Id);
//                    table.ForeignKey(
//                        name: "FK_Business_Development_Notes_Id",
//                        column: x => x.Business_Development_Notes_Id,
//                        principalTable: "Business_Development_Notes",
//                        principalColumn: "Business_Development_Notes_Id");
//                    table.ForeignKey(
//                        name: "FK_Business_Development_Value_Id",
//                        column: x => x.Business_Development_Value_Id,
//                        principalTable: "Business_Development_Value",
//                        principalColumn: "Business_Development_Value_Id");
//                });

//            migrationBuilder.CreateTable(
//                name: "Financials",
//                columns: table => new
//                {
//                    Financials_Id = table.Column<long>(type: "bigint", nullable: false)
//                        .Annotation("SqlServer:Identity", "1, 1"),
//                    Financials_Actual_Id = table.Column<long>(type: "bigint", nullable: false),
//                    Financials_Deviation_Id = table.Column<long>(type: "bigint", nullable: false)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("PK__Financia__08CC3C0CA9ED1B46", x => x.Financials_Id);
//                    table.ForeignKey(
//                        name: "FK_Financials_Actual_Id",
//                        column: x => x.Financials_Actual_Id,
//                        principalTable: "Financials_Actual",
//                        principalColumn: "Financials_Actual_Id");
//                    table.ForeignKey(
//                        name: "FK_Financials_Deviation_Id",
//                        column: x => x.Financials_Deviation_Id,
//                        principalTable: "Financials_Deviation",
//                        principalColumn: "Financials_Deviation_Id");
//                });

//            migrationBuilder.CreateTable(
//                name: "Project_Individual",
//                columns: table => new
//                {
//                    Project_Individual_Id = table.Column<long>(type: "bigint", nullable: false)
//                        .Annotation("SqlServer:Identity", "1, 1"),
//                    Project_Id = table.Column<long>(type: "bigint", nullable: false),
//                    Project_Code = table.Column<int>(type: "int", nullable: true),
//                    Project_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
//                    Forecast_Profit = table.Column<decimal>(type: "money", nullable: true),
//                    Deviation = table.Column<decimal>(type: "money", nullable: true),
//                    Is_Bottom = table.Column<bool>(type: "bit", nullable: true),
//                    Position = table.Column<byte>(type: "tinyint", nullable: true)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("PK__Project___D45D6EACB196AC01", x => x.Project_Individual_Id);
//                    table.ForeignKey(
//                        name: "FK_Project_Individual_Project_Id",
//                        column: x => x.Project_Id,
//                        principalTable: "Projects",
//                        principalColumn: "Project_Id");
//                });

//            migrationBuilder.CreateTable(
//                name: "User_Claims",
//                columns: table => new
//                {
//                    User_Claims_Id = table.Column<long>(type: "bigint", nullable: false)
//                        .Annotation("SqlServer:Identity", "1, 1"),
//                    User_Id = table.Column<long>(type: "bigint", nullable: false),
//                    Claim_Id = table.Column<long>(type: "bigint", nullable: false)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("PK__User_Cla__F98D208FD8673F6A", x => x.User_Claims_Id);
//                    table.ForeignKey(
//                        name: "FK_Claims_Id",
//                        column: x => x.Claim_Id,
//                        principalTable: "Claims",
//                        principalColumn: "Claim_Id");
//                    table.ForeignKey(
//                        name: "FK_User_Claims",
//                        column: x => x.User_Id,
//                        principalTable: "Users",
//                        principalColumn: "User_Id");
//                });

//            migrationBuilder.CreateTable(
//                name: "Reports",
//                columns: table => new
//                {
//                    Report_Id = table.Column<long>(type: "bigint", nullable: false)
//                        .Annotation("SqlServer:Identity", "1, 1"),
//                    Created_By_User_Id = table.Column<long>(type: "bigint", nullable: false),
//                    Report_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
//                    From_Date_Range = table.Column<DateTime>(type: "datetime", nullable: true),
//                    To_Date_Range = table.Column<DateTime>(type: "datetime", nullable: true),
//                    Created_Date = table.Column<DateTime>(type: "datetime", nullable: false),
//                    Is_Draft = table.Column<bool>(type: "bit", nullable: false),
//                    Key_Highlights_Id = table.Column<long>(type: "bigint", nullable: false),
//                    Financials_Id = table.Column<long>(type: "bigint", nullable: false),
//                    Business_Development_Id = table.Column<long>(type: "bigint", nullable: false),
//                    Strategy_Id = table.Column<long>(type: "bigint", nullable: false),
//                    Project_Id = table.Column<long>(type: "bigint", nullable: false),
//                    Admin_And_Resources_Id = table.Column<long>(type: "bigint", nullable: false),
//                    Is_Deleted = table.Column<bool>(type: "bit", nullable: false)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("PK__Reports__30FA9DD1D8F4FD4E", x => x.Report_Id);
//                    table.ForeignKey(
//                        name: "FK_Admin_And_Resources",
//                        column: x => x.Admin_And_Resources_Id,
//                        principalTable: "Admin_And_Resources",
//                        principalColumn: "Admin_And_Resources_Id");
//                    table.ForeignKey(
//                        name: "FK_Business_Development_Id",
//                        column: x => x.Business_Development_Id,
//                        principalTable: "Business_Development",
//                        principalColumn: "Business_Development_Id");
//                    table.ForeignKey(
//                        name: "FK_Created_By_User_Id",
//                        column: x => x.Created_By_User_Id,
//                        principalTable: "Users",
//                        principalColumn: "User_Id");
//                    table.ForeignKey(
//                        name: "FK_Financials_Id",
//                        column: x => x.Financials_Id,
//                        principalTable: "Financials",
//                        principalColumn: "Financials_Id");
//                    table.ForeignKey(
//                        name: "FK_Key_Highlights_Id",
//                        column: x => x.Key_Highlights_Id,
//                        principalTable: "Key_Highlights",
//                        principalColumn: "Key_Highlights_Id");
//                    table.ForeignKey(
//                        name: "FK_Project_Id",
//                        column: x => x.Project_Id,
//                        principalTable: "Projects",
//                        principalColumn: "Project_Id");
//                    table.ForeignKey(
//                        name: "FK_Strategy_Id",
//                        column: x => x.Strategy_Id,
//                        principalTable: "Strategy",
//                        principalColumn: "Strategy_Id");
//                });

//            migrationBuilder.CreateIndex(
//                name: "IX_Business_Development_Business_Development_Notes_Id",
//                table: "Business_Development",
//                column: "Business_Development_Notes_Id");

//            migrationBuilder.CreateIndex(
//                name: "IX_Business_Development_Business_Development_Value_Id",
//                table: "Business_Development",
//                column: "Business_Development_Value_Id");

//            migrationBuilder.CreateIndex(
//                name: "IX_Financials_Financials_Actual_Id",
//                table: "Financials",
//                column: "Financials_Actual_Id");

//            migrationBuilder.CreateIndex(
//                name: "IX_Financials_Financials_Deviation_Id",
//                table: "Financials",
//                column: "Financials_Deviation_Id");

//            migrationBuilder.CreateIndex(
//                name: "IX_Project_Individual_Project_Id",
//                table: "Project_Individual",
//                column: "Project_Id");

//            migrationBuilder.CreateIndex(
//                name: "IX_Reports_Admin_And_Resources_Id",
//                table: "Reports",
//                column: "Admin_And_Resources_Id");

//            migrationBuilder.CreateIndex(
//                name: "IX_Reports_Business_Development_Id",
//                table: "Reports",
//                column: "Business_Development_Id");

//            migrationBuilder.CreateIndex(
//                name: "IX_Reports_Created_By_User_Id",
//                table: "Reports",
//                column: "Created_By_User_Id");

//            migrationBuilder.CreateIndex(
//                name: "IX_Reports_Financials_Id",
//                table: "Reports",
//                column: "Financials_Id");

//            migrationBuilder.CreateIndex(
//                name: "IX_Reports_Key_Highlights_Id",
//                table: "Reports",
//                column: "Key_Highlights_Id");

//            migrationBuilder.CreateIndex(
//                name: "IX_Reports_Project_Id",
//                table: "Reports",
//                column: "Project_Id");

//            migrationBuilder.CreateIndex(
//                name: "IX_Reports_Strategy_Id",
//                table: "Reports",
//                column: "Strategy_Id");

//            migrationBuilder.CreateIndex(
//                name: "IX_User_Claims_Claim_Id",
//                table: "User_Claims",
//                column: "Claim_Id");

//            migrationBuilder.CreateIndex(
//                name: "IX_User_Claims_User_Id",
//                table: "User_Claims",
//                column: "User_Id");
//        }

//        /// <inheritdoc />
//        protected override void Down(MigrationBuilder migrationBuilder)
//        {
//            migrationBuilder.DropTable(
//                name: "Project_Individual");

//            migrationBuilder.DropTable(
//                name: "Reports");

//            migrationBuilder.DropTable(
//                name: "User_Claims");

//            migrationBuilder.DropTable(
//                name: "Admin_And_Resources");

//            migrationBuilder.DropTable(
//                name: "Business_Development");

//            migrationBuilder.DropTable(
//                name: "Financials");

//            migrationBuilder.DropTable(
//                name: "Key_Highlights");

//            migrationBuilder.DropTable(
//                name: "Projects");

//            migrationBuilder.DropTable(
//                name: "Strategy");

//            migrationBuilder.DropTable(
//                name: "Claims");

//            migrationBuilder.DropTable(
//                name: "Users");

//            migrationBuilder.DropTable(
//                name: "Business_Development_Notes");

//            migrationBuilder.DropTable(
//                name: "Business_Development_Value");

//            migrationBuilder.DropTable(
//                name: "Financials_Actual");

//            migrationBuilder.DropTable(
//                name: "Financials_Deviation");
//        }
//    }
//}
