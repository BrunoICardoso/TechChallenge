using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BurgerRoyale.Infrastructure.Migrations
{
	/// <inheritdoc />
	public partial class AddedOrderEntity : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropPrimaryKey(
				name: "PK_Users",
				table: "Users");

			migrationBuilder.RenameTable(
				name: "Users",
				newName: "User");

			migrationBuilder.AddPrimaryKey(
				name: "PK_User",
				table: "User",
				column: "Id");

			migrationBuilder.CreateTable(
				name: "OrderStatus",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_OrderStatus", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "Order",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					StatusId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					OrderTime = table.Column<DateTime>(type: "datetime2", nullable: false),
					CloseTime = table.Column<DateTime>(type: "datetime2", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Order", x => x.Id);
					table.ForeignKey(
						name: "FK_Order_OrderStatus_StatusId",
						column: x => x.StatusId,
						principalTable: "OrderStatus",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "OrderProduct",
				columns: table => new
				{
					OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					ProductPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
					Quantity = table.Column<int>(type: "int", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_OrderProduct", x => new { x.OrderId, x.ProductId });
					table.ForeignKey(
						name: "FK_OrderProduct_Order_OrderId",
						column: x => x.OrderId,
						principalTable: "Order",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_OrderProduct_Products_ProductId",
						column: x => x.ProductId,
						principalTable: "Products",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateIndex(
				name: "IX_Order_StatusId",
				table: "Order",
				column: "StatusId");

			migrationBuilder.CreateIndex(
				name: "IX_OrderProduct_ProductId",
				table: "OrderProduct",
				column: "ProductId");
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "OrderProduct");

			migrationBuilder.DropTable(
				name: "Order");

			migrationBuilder.DropTable(
				name: "OrderStatus");

			migrationBuilder.DropPrimaryKey(
				name: "PK_User",
				table: "User");

			migrationBuilder.RenameTable(
				name: "User",
				newName: "Users");

			migrationBuilder.AddPrimaryKey(
				name: "PK_Users",
				table: "Users",
				column: "Id");
		}
	}
}
