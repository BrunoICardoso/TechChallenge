using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BurgerRoyale.Infrastructure.Migrations
{
	/// <inheritdoc />
	public partial class SeedInitialData : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
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
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DeleteData(
				table: "OrderStatus",
				keyColumn: "Id",
				keyValue: new Guid("0b579b4b-0023-4104-986a-6623fec74d2d"));

			migrationBuilder.DeleteData(
				table: "OrderStatus",
				keyColumn: "Id",
				keyValue: new Guid("206ea83c-d154-4cb7-9ea4-5afb5928b696"));

			migrationBuilder.DeleteData(
				table: "OrderStatus",
				keyColumn: "Id",
				keyValue: new Guid("7ea8fb27-520f-48b8-8574-8dda3fb5b18f"));

			migrationBuilder.DeleteData(
				table: "OrderStatus",
				keyColumn: "Id",
				keyValue: new Guid("b17d9238-80f0-42ed-a56b-8eeb7626987c"));

			migrationBuilder.DeleteData(
				table: "ProductCategory",
				keyColumn: "Id",
				keyValue: new Guid("020676d4-6aac-4935-ba2e-6f4f5c33435c"));

			migrationBuilder.DeleteData(
				table: "ProductCategory",
				keyColumn: "Id",
				keyValue: new Guid("88f7d966-2dd6-4a9b-8236-5efd2eb622fd"));

			migrationBuilder.DeleteData(
				table: "ProductCategory",
				keyColumn: "Id",
				keyValue: new Guid("b4d66914-692b-48e3-97b2-1089171632e3"));

			migrationBuilder.DeleteData(
				table: "ProductCategory",
				keyColumn: "Id",
				keyValue: new Guid("dae1f1a9-9bbe-41d1-9f57-d4e4381187f7"));
		}
	}
}
