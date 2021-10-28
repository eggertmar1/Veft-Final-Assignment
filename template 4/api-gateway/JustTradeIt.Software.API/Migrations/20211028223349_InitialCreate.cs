using Microsoft.EntityFrameworkCore.Migrations;

namespace JustTradeIt.Software.API.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemImages_Items_ItemId",
                table: "ItemImages");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_ItemConditions_ItemConditionId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_ItemImages_ItemId",
                table: "ItemImages");

            migrationBuilder.RenameColumn(
                name: "OnverId",
                table: "Items",
                newName: "OwnerId");

            migrationBuilder.RenameColumn(
                name: "Identifier",
                table: "Items",
                newName: "PublicIdentifier");

            migrationBuilder.AlterColumn<int>(
                name: "ItemConditionId",
                table: "Items",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Items",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_ItemConditions_ItemConditionId",
                table: "Items",
                column: "ItemConditionId",
                principalTable: "ItemConditions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_ItemConditions_ItemConditionId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Items");

            migrationBuilder.RenameColumn(
                name: "PublicIdentifier",
                table: "Items",
                newName: "Identifier");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "Items",
                newName: "OnverId");

            migrationBuilder.AlterColumn<int>(
                name: "ItemConditionId",
                table: "Items",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ItemImages_ItemId",
                table: "ItemImages",
                column: "ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemImages_Items_ItemId",
                table: "ItemImages",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_ItemConditions_ItemConditionId",
                table: "Items",
                column: "ItemConditionId",
                principalTable: "ItemConditions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
