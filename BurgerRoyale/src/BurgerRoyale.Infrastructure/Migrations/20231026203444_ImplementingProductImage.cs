using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BurgerRoyale.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ImplementingProductImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("1e9db3f1-b479-4051-9fd5-709a86819e42"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("81f0ce07-3391-4024-a98f-a429479d417f"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("84873002-54a5-405b-98f2-211efb19822d"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("84b867af-4a0b-4192-96c2-3a2943b4c590"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("9b15b7cc-8434-430f-9d26-6c7802df004f"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("ab2b7dfe-d93c-40bd-b1a3-085e6d8f84e8"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("b38f5b6a-3fe4-4832-943a-bd05647d45ca"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("c153ba51-1fb0-4c81-8067-2563f0d8b4a5"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("debf48cb-d3c9-419e-acc3-43e79e1c4437"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("f84b8067-d7a4-4d58-b3bc-394f2ccfc1b5"));

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Category", "Description", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("1e9db3f1-b479-4051-9fd5-709a86819e42"), 0, "Hambúrguer de carne bovina com o dobro de cheddar.", "Burger Duplo Cheddar", 23.9m },
                    { new Guid("81f0ce07-3391-4024-a98f-a429479d417f"), 1, "Porção de fritas grande.", "Fritas Grande", 8.9m },
                    { new Guid("84873002-54a5-405b-98f2-211efb19822d"), 3, "Sundae de diversos sabores", "Sundae", 7m },
                    { new Guid("84b867af-4a0b-4192-96c2-3a2943b4c590"), 2, "Copo 400 ml", "Refrigerante", 6m },
                    { new Guid("9b15b7cc-8434-430f-9d26-6c7802df004f"), 2, "500 ml com ou sem gás", "Água", 4m },
                    { new Guid("ab2b7dfe-d93c-40bd-b1a3-085e6d8f84e8"), 3, "Sorvete de diversos sabores", "Sorvete", 7m },
                    { new Guid("b38f5b6a-3fe4-4832-943a-bd05647d45ca"), 1, "Porção de fritas.", "Fritas", 6.9m },
                    { new Guid("c153ba51-1fb0-4c81-8067-2563f0d8b4a5"), 0, "Hambúrguer de carne bovina.", "Burger Tradicional", 19.2m },
                    { new Guid("debf48cb-d3c9-419e-acc3-43e79e1c4437"), 1, "Porção de fritas pequena.", "Fritas Pequena", 4.9m },
                    { new Guid("f84b8067-d7a4-4d58-b3bc-394f2ccfc1b5"), 0, "Hambúrguer de carne bovina com o dobro de bacon.", "Burger Duplo Bacon", 22.9m }
                });
        }
    }
}
