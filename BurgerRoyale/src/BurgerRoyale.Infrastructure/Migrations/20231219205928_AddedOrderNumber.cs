using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BurgerRoyale.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedOrderNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("1f9573a2-31be-4d05-b1ad-41edf0de0695"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("3b1cdb59-fa81-4c61-b649-fb6ab2e6c2af"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("3e576bea-1a14-4251-8cbe-47bc44e462c1"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("88bd5d3b-2842-4872-9080-2593ffe0905a"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("b4cbbf87-8fbb-4c2b-8a48-b0da0bdafb68"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("cef09993-4018-4868-9078-aa4d14329b96"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d8b38aec-e17d-4753-99a5-dedec0f08143"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("e18f8474-8dbb-4d28-af52-7c64435a1633"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("f225bdf1-320a-4e84-97cf-a5ce07213e22"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("f51d9e58-4f86-4e75-9b2e-89e6ac9a857d"));

            migrationBuilder.AddColumn<int>(
                name: "OrderNumber",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Category", "Description", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("24b2b893-e3dc-4f7d-b70f-4daa6fbae88d"), 1, "Porção de fritas grande.", "Fritas Grande", 8.9m },
                    { new Guid("3ab44238-ea8c-4a2d-b842-2965a0be4429"), 2, "500 ml com ou sem gás", "Água", 4m },
                    { new Guid("5b49e5dd-7f2e-4aad-ada7-cb066f305c05"), 3, "Sorvete de diversos sabores", "Sorvete", 7m },
                    { new Guid("68dcd0c8-43a9-4084-b460-3e07d5fd6e58"), 1, "Porção de fritas.", "Fritas", 6.9m },
                    { new Guid("8001cdcc-dd10-4977-b647-4523742378ec"), 0, "Hambúrguer de carne bovina com o dobro de cheddar.", "Burger Duplo Cheddar", 23.9m },
                    { new Guid("a0e7d06f-3555-4643-a5ed-f6d698dced62"), 2, "Copo 400 ml", "Refrigerante", 6m },
                    { new Guid("a9a2ffb7-d61a-44a3-8854-fcb331c19265"), 3, "Sundae de diversos sabores", "Sundae", 7m },
                    { new Guid("bfc38a0f-414d-4e89-b76c-eb561433e826"), 1, "Porção de fritas pequena.", "Fritas Pequena", 4.9m },
                    { new Guid("c56a7e1c-56ae-4d7c-9eb3-14ff83934320"), 0, "Hambúrguer de carne bovina com o dobro de bacon.", "Burger Duplo Bacon", 22.9m },
                    { new Guid("f132dd38-d80d-4fd8-9470-191b8d6e23f0"), 0, "Hambúrguer de carne bovina.", "Burger Tradicional", 19.2m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("24b2b893-e3dc-4f7d-b70f-4daa6fbae88d"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("3ab44238-ea8c-4a2d-b842-2965a0be4429"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("5b49e5dd-7f2e-4aad-ada7-cb066f305c05"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("68dcd0c8-43a9-4084-b460-3e07d5fd6e58"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("8001cdcc-dd10-4977-b647-4523742378ec"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("a0e7d06f-3555-4643-a5ed-f6d698dced62"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("a9a2ffb7-d61a-44a3-8854-fcb331c19265"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("bfc38a0f-414d-4e89-b76c-eb561433e826"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("c56a7e1c-56ae-4d7c-9eb3-14ff83934320"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("f132dd38-d80d-4fd8-9470-191b8d6e23f0"));

            migrationBuilder.DropColumn(
                name: "OrderNumber",
                table: "Orders");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Category", "Description", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("1f9573a2-31be-4d05-b1ad-41edf0de0695"), 0, "Hambúrguer de carne bovina com o dobro de bacon.", "Burger Duplo Bacon", 22.9m },
                    { new Guid("3b1cdb59-fa81-4c61-b649-fb6ab2e6c2af"), 1, "Porção de fritas pequena.", "Fritas Pequena", 4.9m },
                    { new Guid("3e576bea-1a14-4251-8cbe-47bc44e462c1"), 0, "Hambúrguer de carne bovina.", "Burger Tradicional", 19.2m },
                    { new Guid("88bd5d3b-2842-4872-9080-2593ffe0905a"), 3, "Sundae de diversos sabores", "Sundae", 7m },
                    { new Guid("b4cbbf87-8fbb-4c2b-8a48-b0da0bdafb68"), 2, "500 ml com ou sem gás", "Água", 4m },
                    { new Guid("cef09993-4018-4868-9078-aa4d14329b96"), 3, "Sorvete de diversos sabores", "Sorvete", 7m },
                    { new Guid("d8b38aec-e17d-4753-99a5-dedec0f08143"), 0, "Hambúrguer de carne bovina com o dobro de cheddar.", "Burger Duplo Cheddar", 23.9m },
                    { new Guid("e18f8474-8dbb-4d28-af52-7c64435a1633"), 2, "Copo 400 ml", "Refrigerante", 6m },
                    { new Guid("f225bdf1-320a-4e84-97cf-a5ce07213e22"), 1, "Porção de fritas.", "Fritas", 6.9m },
                    { new Guid("f51d9e58-4f86-4e75-9b2e-89e6ac9a857d"), 1, "Porção de fritas grande.", "Fritas Grande", 8.9m }
                });
        }
    }
}
