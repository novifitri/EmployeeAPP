using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeApp.Migrations
{
    public partial class Reset : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Divisi",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nama = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Divisi", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Karyawan",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nama = table.Column<string>(nullable: false),
                    NIK = table.Column<string>(nullable: false),
                    Jenis_Kelamin = table.Column<string>(nullable: false),
                    Tanggal_Lahir = table.Column<DateTime>(nullable: false),
                    Alamat = table.Column<string>(nullable: true),
                    Nomor_Telp = table.Column<string>(nullable: false),
                    Divisi_Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Karyawan", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Karyawan_Divisi_Divisi_Id",
                        column: x => x.Divisi_Id,
                        principalTable: "Divisi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Absensi",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Karyawan_Id = table.Column<int>(nullable: false),
                    Tanggal_Hadir = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Absensi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Absensi_Karyawan_Karyawan_Id",
                        column: x => x.Karyawan_Id,
                        principalTable: "Karyawan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Profile",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Password = table.Column<string>(maxLength: 8, nullable: false),
                    Karyawan_Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Profile_Karyawan_Karyawan_Id",
                        column: x => x.Karyawan_Id,
                        principalTable: "Karyawan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Absensi_Karyawan_Id",
                table: "Absensi",
                column: "Karyawan_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Karyawan_Divisi_Id",
                table: "Karyawan",
                column: "Divisi_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Profile_Karyawan_Id",
                table: "Profile",
                column: "Karyawan_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Absensi");

            migrationBuilder.DropTable(
                name: "Profile");

            migrationBuilder.DropTable(
                name: "Karyawan");

            migrationBuilder.DropTable(
                name: "Divisi");
        }
    }
}
