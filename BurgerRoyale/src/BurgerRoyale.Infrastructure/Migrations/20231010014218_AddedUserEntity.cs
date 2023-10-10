using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BurgerRoyale.Infrastructure.Migrations
{
	/// <inheritdoc />
	public partial class AddedUserEntity : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "Users",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					Cpf = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
					Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
					Email = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
					UserType = table.Column<int>(type: "int", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Users", x => x.Id);
				});
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "Users");
		}
	}
}
