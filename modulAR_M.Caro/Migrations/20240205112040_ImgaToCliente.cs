using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace modulAR_M.Caro.Migrations
{
    public partial class ImgaToCliente : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Imagen",
                table: "Clientes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Imagen",
                table: "Clientes");
        }
    }
}
