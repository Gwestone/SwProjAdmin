using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SwProjAdmin.Data.Migrations
{
    /// <inheritdoc />
    public partial class CurrenciesOTMPriceUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Currencies_Prices_PriceId",
                table: "Currencies");

            migrationBuilder.DropIndex(
                name: "IX_Currencies_PriceId",
                table: "Currencies");

            migrationBuilder.DropColumn(
                name: "PriceId",
                table: "Currencies");

            migrationBuilder.CreateIndex(
                name: "IX_Prices_CurrencyId",
                table: "Prices",
                column: "CurrencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Prices_Currencies_CurrencyId",
                table: "Prices",
                column: "CurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prices_Currencies_CurrencyId",
                table: "Prices");

            migrationBuilder.DropIndex(
                name: "IX_Prices_CurrencyId",
                table: "Prices");

            migrationBuilder.AddColumn<int>(
                name: "PriceId",
                table: "Currencies",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Currencies_PriceId",
                table: "Currencies",
                column: "PriceId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Currencies_Prices_PriceId",
                table: "Currencies",
                column: "PriceId",
                principalTable: "Prices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
