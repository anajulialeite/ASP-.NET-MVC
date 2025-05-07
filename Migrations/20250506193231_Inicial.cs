using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LanchesMac.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CarrinhoCompraId",
                table: "CarrinhoCompraItens",
                newName: "CarrinhoCompraid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CarrinhoCompraid",
                table: "CarrinhoCompraItens",
                newName: "CarrinhoCompraId");
        }
    }
}
