using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MedicalCenter.Data.Migrations
{
    public partial class MedicalExaminationModelAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MedicalExamination",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Diagnose = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    PatientId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DoctorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalExamination", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicalExamination_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MedicalExamination_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MedicalExamination_DoctorId",
                table: "MedicalExamination",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalExamination_PatientId",
                table: "MedicalExamination",
                column: "PatientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MedicalExamination");
        }
    }
}
