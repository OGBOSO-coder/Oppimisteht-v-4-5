using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WebStore.Migrations
{
    /// <inheritdoc />
    public partial class AddDiscountAndShippingFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:public.discount_type", "percentage,flat");

            migrationBuilder.AddColumn<int>(
                name: "DiscountCodeId",
                table: "orders",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "discount_codes",
                columns: table => new
                {
                    discount_code_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    discount_type = table.Column<int>(type: "discount_type", nullable: false),
                    discount_value = table.Column<decimal>(type: "numeric", nullable: false),
                    expiration_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    max_usage = table.Column<int>(type: "integer", nullable: true),
                    times_used = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("discount_codes_pkey", x => x.discount_code_id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_orders_DiscountCodeId",
                table: "orders",
                column: "DiscountCodeId");

            migrationBuilder.AddForeignKey(
                name: "FK_orders_discount_codes_DiscountCodeId",
                table: "orders",
                column: "DiscountCodeId",
                principalTable: "discount_codes",
                principalColumn: "discount_code_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orders_discount_codes_DiscountCodeId",
                table: "orders");

            migrationBuilder.DropTable(
                name: "discount_codes");

            migrationBuilder.DropIndex(
                name: "IX_orders_DiscountCodeId",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "DiscountCodeId",
                table: "orders");

            migrationBuilder.AlterDatabase()
                .OldAnnotation("Npgsql:Enum:public.discount_type", "percentage,flat");
        }
    }
}
