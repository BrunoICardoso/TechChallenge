using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BurgerRoyale.Infrastructure.Migrations
{
	/// <inheritdoc />
	public partial class RemovedProductCategoryAndOrderStatus : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropForeignKey(
				name: "FK_Orders_OrderStatus_StatusId",
				table: "Orders");

			migrationBuilder.DropForeignKey(
				name: "FK_Products_ProductCategory_CategoryId",
				table: "Products");

			migrationBuilder.DropTable(
				name: "OrderStatus");

			migrationBuilder.DropTable(
				name: "ProductCategory");

			migrationBuilder.DropIndex(
				name: "IX_Products_CategoryId",
				table: "Products");

			migrationBuilder.DropIndex(
				name: "IX_Orders_StatusId",
				table: "Orders");

			migrationBuilder.DropColumn(
				name: "CategoryId",
				table: "Products");

			migrationBuilder.DropColumn(
				name: "StatusId",
				table: "Orders");

			migrationBuilder.AddColumn<int>(
				name: "Category",
				table: "Products",
				type: "int",
				nullable: false,
				defaultValue: 0);

			migrationBuilder.AddColumn<int>(
				name: "Status",
				table: "Orders",
				type: "int",
				nullable: false,
				defaultValue: 0);
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropColumn(
				name: "Category",
				table: "Products");

			migrationBuilder.DropColumn(
				name: "Status",
				table: "Orders");

			migrationBuilder.AddColumn<Guid>(
				name: "CategoryId",
				table: "Products",
				type: "uniqueidentifier",
				nullable: false,
				defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

			migrationBuilder.AddColumn<Guid>(
				name: "StatusId",
				table: "Orders",
				type: "uniqueidentifier",
				nullable: false,
				defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

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
				name: "ProductCategory",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_ProductCategory", x => x.Id);
				});

			migrationBuilder.InsertData(
				table: "OrderStatus",
				columns: new[] { "Id", "Description" },
				values: new object[,]
				{
					{ new Guid("0b579b4b-0023-4104-986a-6623fec74d2d"), "Em preparação" },
					{ new Guid("206ea83c-d154-4cb7-9ea4-5afb5928b696"), "Pronto" },
					{ new Guid("7ea8fb27-520f-48b8-8574-8dda3fb5b18f"), "Recebido" },
					{ new Guid("b17d9238-80f0-42ed-a56b-8eeb7626987c"), "Finalizado" }
				});

			migrationBuilder.InsertData(
				table: "ProductCategory",
				columns: new[] { "Id", "Description" },
				values: new object[,]
				{
					{ new Guid("020676d4-6aac-4935-ba2e-6f4f5c33435c"), "Sobremesa" },
					{ new Guid("88f7d966-2dd6-4a9b-8236-5efd2eb622fd"), "Acompanhamento" },
					{ new Guid("b4d66914-692b-48e3-97b2-1089171632e3"), "Lanche" },
					{ new Guid("dae1f1a9-9bbe-41d1-9f57-d4e4381187f7"), "Bebida" }
				});

			migrationBuilder.CreateIndex(
				name: "IX_Products_CategoryId",
				table: "Products",
				column: "CategoryId");

			migrationBuilder.CreateIndex(
				name: "IX_Orders_StatusId",
				table: "Orders",
				column: "StatusId");

			migrationBuilder.AddForeignKey(
				name: "FK_Orders_OrderStatus_StatusId",
				table: "Orders",
				column: "StatusId",
				principalTable: "OrderStatus",
				principalColumn: "Id",
				onDelete: ReferentialAction.Cascade);

			migrationBuilder.AddForeignKey(
				name: "FK_Products_ProductCategory_CategoryId",
				table: "Products",
				column: "CategoryId",
				principalTable: "ProductCategory",
				principalColumn: "Id",
				onDelete: ReferentialAction.Cascade);
		}
	}
}
