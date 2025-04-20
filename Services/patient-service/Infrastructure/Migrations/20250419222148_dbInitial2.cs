using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PatientService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class dbInitial2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalRecords_Patients_PatientId1",
                table: "MedicalRecords");

            migrationBuilder.RenameColumn(
                name: "PatientId1",
                table: "MedicalRecords",
                newName: "PatientInfoId");

            migrationBuilder.RenameIndex(
                name: "IX_MedicalRecords_PatientId1",
                table: "MedicalRecords",
                newName: "IX_MedicalRecords_PatientInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalRecords_Patients_PatientInfoId",
                table: "MedicalRecords",
                column: "PatientInfoId",
                principalTable: "Patients",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalRecords_Patients_PatientInfoId",
                table: "MedicalRecords");

            migrationBuilder.RenameColumn(
                name: "PatientInfoId",
                table: "MedicalRecords",
                newName: "PatientId1");

            migrationBuilder.RenameIndex(
                name: "IX_MedicalRecords_PatientInfoId",
                table: "MedicalRecords",
                newName: "IX_MedicalRecords_PatientId1");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalRecords_Patients_PatientId1",
                table: "MedicalRecords",
                column: "PatientId1",
                principalTable: "Patients",
                principalColumn: "Id");
        }
    }
}
