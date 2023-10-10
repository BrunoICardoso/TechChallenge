using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BurgerRoyale.Infrastructure.Migrations
{
	/// <inheritdoc />
	public partial class AddedProductsEntities : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.RenameColumn(
				name: "Category",
				table: "Products",
				newName: "Description");

			migrationBuilder.AddColumn<Guid>(
				name: "CategoryId",
				table: "Products",
				type: "uniqueidentifier",
				nullable: false,
				defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

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

			migrationBuilder.CreateTable(
				name: "ProductImage",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
					Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
					ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_ProductImage", x => x.Id);
					table.ForeignKey(
						name: "FK_ProductImage_Products_ProductId",
						column: x => x.ProductId,
						principalTable: "Products",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateIndex(
				name: "IX_Products_CategoryId",
				table: "Products",
				column: "CategoryId");

			migrationBuilder.CreateIndex(
				name: "IX_ProductImage_ProductId",
				table: "ProductImage",
				column: "ProductId");

			migrationBuilder.AddForeignKey(
				name: "FK_Products_ProductCategory_CategoryId",
				table: "Products",
				column: "CategoryId",
				principalTable: "ProductCategory",
				principalColumn: "Id",
				onDelete: ReferentialAction.Cascade);
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropForeignKey(
				name: "FK_Products_ProductCategory_CategoryId",
				table: "Products");

			migrationBuilder.DropTable(
				name: "ProductCategory");

			migrationBuilder.DropTable(
				name: "ProductImage");

			migrationBuilder.DropIndex(
				name: "IX_Products_CategoryId",
				table: "Products");

			migrationBuilder.DropColumn(
				name: "CategoryId",
				table: "Products");

			migrationBuilder.RenameColumn(
				name: "Description",
				table: "Products",
				newName: "Category");
		}
	}
}
