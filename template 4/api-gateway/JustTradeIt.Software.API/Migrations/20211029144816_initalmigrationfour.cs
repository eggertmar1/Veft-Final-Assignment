using Microsoft.EntityFrameworkCore.Migrations;

namespace JustTradeIt.Software.API.Migrations
{
    public partial class initalmigrationfour : Migration
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

            migrationBuilder.AlterColumn<int>(
                name: "ItemConditionId",
                table: "Items",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_ItemConditions_ItemConditionId",
                table: "Items",
                column: "ItemConditionId",
                principalTable: "ItemConditions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_ItemConditions_ItemConditionId",
                table: "Items");

            migrationBuilder.AlterColumn<int>(
                name: "ItemConditionId",
                table: "Items",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

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
                onDelete: ReferentialAction.Restrict);
        }
    }
}
