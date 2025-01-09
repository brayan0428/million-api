using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "Photo",
                table: "Owners",
                type: "varbinary(max)",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)");

            migrationBuilder.InsertData(
                table: "Owners",
                columns: new[] { "IdOwner", "Address", "Birthday", "Name", "Photo" },
                values: new object[,]
                {
                    { 1, "Calle 123, Bogotá", new DateTime(1980, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Juan Pérez", null },
                    { 2, "Avenida Central 45, Medellín", new DateTime(1992, 9, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "María Rodríguez", null },
                    { 3, "Carrera 10 #15-20, Cali", new DateTime(1975, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Carlos Gómez", null },
                    { 4, "Diagonal 25 #33-45, Barranquilla", new DateTime(1988, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ana Martínez", null }
                });

            migrationBuilder.InsertData(
                table: "Properties",
                columns: new[] { "IdProperty", "Address", "CodeInternal", "IdOwner", "Name", "Price", "Year" },
                values: new object[,]
                {
                    { 1, "Sector Punta Arena, Cartagena", "PROP001", 1, "Casa en la Playa", 350000000m, 2015 },
                    { 2, "Carrera 7 #12-34, Bogotá", "PROP002", 2, "Apartamento en el Centro", 500000000m, 2020 },
                    { 3, "Kilómetro 15 vía al Llano, Villavicencio", "PROP003", 3, "Finca Campestre", 800000000m, 2010 },
                    { 4, "Zona Rosa, Medellín", "PROP004", 4, "Local Comercial", 950000000m, 2018 },
                    { 5, "Edificio Ocean View, Santa Marta", "PROP005", 1, "Penthouse de Lujo", 1200000000m, 2019 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "IdProperty",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "IdProperty",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "IdProperty",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "IdProperty",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "IdProperty",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Owners",
                keyColumn: "IdOwner",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Owners",
                keyColumn: "IdOwner",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Owners",
                keyColumn: "IdOwner",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Owners",
                keyColumn: "IdOwner",
                keyValue: 4);

            migrationBuilder.AlterColumn<byte[]>(
                name: "Photo",
                table: "Owners",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0],
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)",
                oldNullable: true);
        }
    }
}
