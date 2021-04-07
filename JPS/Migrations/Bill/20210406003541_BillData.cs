using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JPS.Migrations.Bill
{
    public partial class BillData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bill_Information",
                columns: table => new
                {
                    bill_id = table.Column<string>(nullable: false),
                    date_generated = table.Column<DateTime>(nullable: false),
                    due_date = table.Column<DateTime>(nullable: false),
                    cust_id = table.Column<string>(nullable: true),
                    premise_id = table.Column<string>(nullable: true),
                    address = table.Column<string>(nullable: true),
                    amount = table.Column<string>(nullable: true),
                    status = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bill_Information", x => x.bill_id);
                });

            migrationBuilder.CreateTable(
                name: "PREMISE_DETAILS",
                columns: table => new
                {
                    ID = table.Column<string>(type: "AS CONCAT(SUB_ID, CONVERT(NVARCHAR(90), ROW_ID)) PERSISTED", nullable: false),
                    ROW_ID = table.Column<string>(type: "INT IDENTITY(200, 10)", nullable: false),
                    SUB_ID = table.Column<string>(type: "NVARCHAR(10) DEFAULT 'PREPJPS'", nullable: true),
                    LOCATION_ADDRESS = table.Column<string>(type: "NVARCHAR(MAX)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PREMISE_DETAILS", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bill_Information");

            migrationBuilder.DropTable(
                name: "PREMISE_DETAILS");
        }
    }
}
