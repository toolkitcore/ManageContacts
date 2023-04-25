using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManageContacts.Entity.Migrations
{
    public partial class v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_Users_CreatorId",
                table: "Contacts");

            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_Users_ModifierId",
                table: "Contacts");

            migrationBuilder.DropForeignKey(
                name: "FK_EmailTypes_Users_CreatorId",
                table: "EmailTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_EmailTypes_Users_ModifierId",
                table: "EmailTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Users_CreatorId",
                table: "Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Users_ModifierId",
                table: "Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_PhoneTypes_Users_CreatorId",
                table: "PhoneTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_PhoneTypes_Users_ModifierId",
                table: "PhoneTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_RelativeTypes_Users_CreatorId",
                table: "RelativeTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_RelativeTypes_Users_ModifierId",
                table: "RelativeTypes");

            migrationBuilder.DropIndex(
                name: "IX_RelativeTypes_CreatorId",
                table: "RelativeTypes");

            migrationBuilder.DropIndex(
                name: "IX_RelativeTypes_ModifierId",
                table: "RelativeTypes");

            migrationBuilder.DropIndex(
                name: "IX_PhoneTypes_CreatorId",
                table: "PhoneTypes");

            migrationBuilder.DropIndex(
                name: "IX_PhoneTypes_ModifierId",
                table: "PhoneTypes");

            migrationBuilder.DropIndex(
                name: "IX_Groups_CreatorId",
                table: "Groups");

            migrationBuilder.DropIndex(
                name: "IX_Groups_ModifierId",
                table: "Groups");

            migrationBuilder.DropIndex(
                name: "IX_EmailTypes_CreatorId",
                table: "EmailTypes");

            migrationBuilder.DropIndex(
                name: "IX_EmailTypes_ModifierId",
                table: "EmailTypes");

            migrationBuilder.DropIndex(
                name: "IX_Contacts_CreatorId",
                table: "Contacts");

            migrationBuilder.DropIndex(
                name: "IX_Contacts_ModifierId",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "RelativeTypes");

            migrationBuilder.DropColumn(
                name: "ModifierId",
                table: "RelativeTypes");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "PhoneTypes");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "ModifierId",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "EmailTypes");

            migrationBuilder.DropColumn(
                name: "ModifierId",
                table: "EmailTypes");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "ModifierId",
                table: "Contacts");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Groups",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Contacts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Groups_UserId",
                table: "Groups",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Users_UserId",
                table: "Groups",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Users_UserId",
                table: "Groups");

            migrationBuilder.DropIndex(
                name: "IX_Groups_UserId",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Groups");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatorId",
                table: "RelativeTypes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ModifierId",
                table: "RelativeTypes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatorId",
                table: "PhoneTypes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatorId",
                table: "Groups",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ModifierId",
                table: "Groups",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatorId",
                table: "EmailTypes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ModifierId",
                table: "EmailTypes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Contacts",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatorId",
                table: "Contacts",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ModifierId",
                table: "Contacts",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RelativeTypes_CreatorId",
                table: "RelativeTypes",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_RelativeTypes_ModifierId",
                table: "RelativeTypes",
                column: "ModifierId");

            migrationBuilder.CreateIndex(
                name: "IX_PhoneTypes_CreatorId",
                table: "PhoneTypes",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_PhoneTypes_ModifierId",
                table: "PhoneTypes",
                column: "ModifierId");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_CreatorId",
                table: "Groups",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_ModifierId",
                table: "Groups",
                column: "ModifierId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailTypes_CreatorId",
                table: "EmailTypes",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailTypes_ModifierId",
                table: "EmailTypes",
                column: "ModifierId");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_CreatorId",
                table: "Contacts",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_ModifierId",
                table: "Contacts",
                column: "ModifierId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_Users_CreatorId",
                table: "Contacts",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_Users_ModifierId",
                table: "Contacts",
                column: "ModifierId",
                principalTable: "Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmailTypes_Users_CreatorId",
                table: "EmailTypes",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmailTypes_Users_ModifierId",
                table: "EmailTypes",
                column: "ModifierId",
                principalTable: "Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Users_CreatorId",
                table: "Groups",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Users_ModifierId",
                table: "Groups",
                column: "ModifierId",
                principalTable: "Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PhoneTypes_Users_CreatorId",
                table: "PhoneTypes",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PhoneTypes_Users_ModifierId",
                table: "PhoneTypes",
                column: "ModifierId",
                principalTable: "Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_RelativeTypes_Users_CreatorId",
                table: "RelativeTypes",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_RelativeTypes_Users_ModifierId",
                table: "RelativeTypes",
                column: "ModifierId",
                principalTable: "Users",
                principalColumn: "UserId");
        }
    }
}
