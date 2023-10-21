using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BurgerRoyale.Infrastructure.Migrations
{
	/// <inheritdoc />
	public partial class FixingOrderType : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropForeignKey(
				name: "FK_Order_OrderStatus_StatusId",
				table: "Order");

			migrationBuilder.DropForeignKey(
				name: "FK_OrderProduct_Order_OrderId",
				table: "OrderProduct");

			migrationBuilder.DropPrimaryKey(
				name: "PK_User",
				table: "User");

			migrationBuilder.DropPrimaryKey(
				name: "PK_Order",
				table: "Order");

			migrationBuilder.RenameTable(
				name: "User",
				newName: "Users");

			migrationBuilder.RenameTable(
				name: "Order",
				newName: "Orders");

			migrationBuilder.RenameIndex(
				name: "IX_Order_StatusId",
				table: "Orders",
				newName: "IX_Orders_StatusId");

			migrationBuilder.AddPrimaryKey(
				name: "PK_Users",
				table: "Users",
				column: "Id");

			migrationBuilder.AddPrimaryKey(
				name: "PK_Orders",
				table: "Orders",
				column: "Id");

			migrationBuilder.AddForeignKey(
				name: "FK_OrderProduct_Orders_OrderId",
				table: "OrderProduct",
				column: "OrderId",
				principalTable: "Orders",
				principalColumn: "Id",
				onDelete: ReferentialAction.Cascade);

			migrationBuilder.AddForeignKey(
				name: "FK_Orders_OrderStatus_StatusId",
				table: "Orders",
				column: "StatusId",
				principalTable: "OrderStatus",
				principalColumn: "Id",
				onDelete: ReferentialAction.Cascade);
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropForeignKey(
				name: "FK_OrderProduct_Orders_OrderId",
				table: "OrderProduct");

			migrationBuilder.DropForeignKey(
				name: "FK_Orders_OrderStatus_StatusId",
				table: "Orders");

			migrationBuilder.DropPrimaryKey(
				name: "PK_Users",
				table: "Users");

			migrationBuilder.DropPrimaryKey(
				name: "PK_Orders",
				table: "Orders");

			migrationBuilder.RenameTable(
				name: "Users",
				newName: "User");

			migrationBuilder.RenameTable(
				name: "Orders",
				newName: "Order");

			migrationBuilder.RenameIndex(
				name: "IX_Orders_StatusId",
				table: "Order",
				newName: "IX_Order_StatusId");

			migrationBuilder.AddPrimaryKey(
				name: "PK_User",
				table: "User",
				column: "Id");

			migrationBuilder.AddPrimaryKey(
				name: "PK_Order",
				table: "Order",
				column: "Id");

			migrationBuilder.AddForeignKey(
				name: "FK_Order_OrderStatus_StatusId",
				table: "Order",
				column: "StatusId",
				principalTable: "OrderStatus",
				principalColumn: "Id",
				onDelete: ReferentialAction.Cascade);

			migrationBuilder.AddForeignKey(
				name: "FK_OrderProduct_Order_OrderId",
				table: "OrderProduct",
				column: "OrderId",
				principalTable: "Order",
				principalColumn: "Id",
				onDelete: ReferentialAction.Cascade);
		}
	}
}
