using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PowerFlux.Db.Migrations
{
    public partial class CreatePowerFluxDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AlloyingElements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Symbol = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PartialTransformationToFerroalloyEquation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PartialTransformationToKernelEquation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PartialTransformationToGasEquation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PartialTransformationToSlagEquation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlloyingElements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Key = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DispleedName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.Id);
                    table.UniqueConstraint("AK_Settings_Id_Key", x => new { x.Id, x.Key });
                });

            migrationBuilder.InsertData(
                table: "AlloyingElements",
                columns: new[] { "Id", "Name", "PartialTransformationToFerroalloyEquation", "PartialTransformationToGasEquation", "PartialTransformationToKernelEquation", "PartialTransformationToSlagEquation", "Symbol" },
                values: new object[,]
                {
                    { 1, "Manganum", "1.30 - 2.15 * (P / S)", "1.41 - 2.65 * (P / S)", "1.83 - 4.32 * (P / S)", "0", "Mn" },
                    { 2, "Silicium", "0.67 - 1.40 * (P / S)", "0.85 - 2.0 * (P / S)", "0.85 - 2.0 * (P / S)", "0", "Si" },
                    { 3, "Carboneum", "2.81 - 6.86 * (P / S)", "1.20 - 1.59 * (P / S)", "1.24 - 1.53 * (P / S)", "0", "C" },
                    { 4, "Titanium", "0", "(25.9 * (P / S) - 2.74) * 0,0001", "0", "(25.9 * (P / S) - 2.74) * 0,0001", "Ti" }
                });

            migrationBuilder.InsertData(
                table: "Settings",
                columns: new[] { "Id", "DispleedName", "Key", "Value" },
                values: new object[,]
                {
                    { 1, "Max value of coating mass coefficient", "coatingMassCoefficient.max", "0.45" },
                    { 2, "Min value of coating mass coefficient", "coatingMassCoefficient.min", "0.35" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlloyingElements");

            migrationBuilder.DropTable(
                name: "Settings");
        }
    }
}
