using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FakePaymentService.Infrastructure.Migrations
{
	/// <inheritdoc />
	public partial class PaymentEntity : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "Payments",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
					ClientIdentifier = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
					CallbackUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
					Status = table.Column<int>(type: "int", nullable: false),
					CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
					UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Payments", x => x.Id);
				});
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "Payments");
		}
	}
}
