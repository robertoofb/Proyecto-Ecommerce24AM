using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProyectoWebDL.Migrations
{
    /// <inheritdoc />
    public partial class ScireHub : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    PkCategoria = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.PkCategoria);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    PkRoles = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.PkRoles);
                });

            migrationBuilder.CreateTable(
                name: "Articulos",
                columns: table => new
                {
                    PkArticulo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Autor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FkCategoria = table.Column<int>(type: "int", nullable: true),
                    UrlImagenPath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UrlPdfPath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articulos", x => x.PkArticulo);
                    table.ForeignKey(
                        name: "FK_Articulos_Categorias_FkCategoria",
                        column: x => x.FkCategoria,
                        principalTable: "Categorias",
                        principalColumn: "PkCategoria");
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    PKUsuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apellido1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apellido2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NombreUsuario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Correo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Contraseña = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FkRol = table.Column<int>(type: "int", nullable: true),
                    UrlImagenPath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.PKUsuario);
                    table.ForeignKey(
                        name: "FK_Usuarios_Roles_FkRol",
                        column: x => x.FkRol,
                        principalTable: "Roles",
                        principalColumn: "PkRoles");
                });

            migrationBuilder.InsertData(
                table: "Categorias",
                columns: new[] { "PkCategoria", "Nombre" },
                values: new object[,]
                {
                    { 1, "Software" },
                    { 2, "Biomédica" },
                    { 3, "Biotecnología" },
                    { 4, "Finanzas" },
                    { 5, "Administración" },
                    { 6, "Terapia Física" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "PkRoles", "Nombre" },
                values: new object[,]
                {
                    { 1, "SA" },
                    { 2, "Usuario" },
                    { 3, "Investigador" }
                });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "PKUsuario", "Apellido1", "Apellido2", "Contraseña", "Correo", "FkRol", "Nombre", "NombreUsuario", "UrlImagenPath" },
                values: new object[] { 1, "Fierro", "Ballote", "1234", "roberto@gmail.com", 1, "Roberto", "robertofb", "imagen/siu" });

            migrationBuilder.CreateIndex(
                name: "IX_Articulos_FkCategoria",
                table: "Articulos",
                column: "FkCategoria");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_FkRol",
                table: "Usuarios",
                column: "FkRol");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Articulos");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Categorias");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
