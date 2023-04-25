using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManageContacts.Entity.Migrations
{
    public partial class v3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AddressTypes_Users_CreatorId",
                table: "AddressTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_AddressTypes_Users_ModifierId",
                table: "AddressTypes");

            migrationBuilder.DropIndex(
                name: "IX_AddressTypes_CreatorId",
                table: "AddressTypes");

            migrationBuilder.DropIndex(
                name: "IX_AddressTypes_ModifierId",
                table: "AddressTypes");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "AddressTypes");

            migrationBuilder.DropColumn(
                name: "ModifierId",
                table: "AddressTypes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CreatorId",
                table: "AddressTypes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ModifierId",
                table: "AddressTypes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AddressTypes_CreatorId",
                table: "AddressTypes",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_AddressTypes_ModifierId",
                table: "AddressTypes",
                column: "ModifierId");

            migrationBuilder.AddForeignKey(
                name: "FK_AddressTypes_Users_CreatorId",
                table: "AddressTypes",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AddressTypes_Users_ModifierId",
                table: "AddressTypes",
                column: "ModifierId",
                principalTable: "Users",
                principalColumn: "UserId");
        }
    }
}
