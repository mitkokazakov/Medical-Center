using Microsoft.EntityFrameworkCore.Migrations;

namespace MedicalCenter.Data.Migrations
{
    public partial class ParamsSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Parameters",
                columns: new[] { "Id", "MaxValue", "MinValue", "Name" },
                values: new object[,]
                {
                    { 1, 17.300000000000001, 11.0, "Hemoglobin" },
                    { 22, 45.0, 9.0, "ALAT" },
                    { 21, 36.0, 15.0, "ASAT" },
                    { 20, 1.2, 0.69999999999999996, "Creatinine" },
                    { 19, 3.0, 0.65000000000000002, "CRP" },
                    { 18, 2.25, 1.6899999999999999, "Triglycerides" },
                    { 17, 1.55, 1.03, "HDL" },
                    { 16, 4.1100000000000003, 3.3599999999999999, "LDL" },
                    { 15, 6.2000000000000002, 5.2999999999999998, "Total Cholesterol" },
                    { 14, 21.5, 11.4, "RDW" },
                    { 13, 472.0, 152.0, "Plateles count" },
                    { 12, 2.0, 0.0, "Eosinophils" },
                    { 11, 2.0, 0.0, "Basophils" },
                    { 10, 14.0, 1.0, "Monocytes" },
                    { 9, 78.0, 15.0, "Neutrophils" },
                    { 8, 75.0, 15.0, "Lymphocytes" },
                    { 7, 21.600000000000001, 3.1000000000000001, "WBC" },
                    { 6, 33.600000000000001, 25.800000000000001, "MCHC" },
                    { 5, 41.100000000000001, 26.0, "MCH" },
                    { 4, 128.0, 90.400000000000006, "MCV" },
                    { 3, 56.5, 35.399999999999999, "Hematocrit" },
                    { 2, 7.2999999999999998, 3.1200000000000001, "Red cell count" },
                    { 23, 58.0, 11.0, "GGT" },
                    { 24, 8.0999999999999996, 5.0, "Erythrocyte" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: 24);
        }
    }
}
