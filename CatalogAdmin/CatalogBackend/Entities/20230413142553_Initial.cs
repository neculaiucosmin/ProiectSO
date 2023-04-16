using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatalogBackend.Entities
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "orar",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    grp = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    dayOFfWeek = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    year = table.Column<short>(type: "smallint", nullable: true),
                    hours = table.Column<string>(type: "varchar(12)", unicode: false, maxLength: 12, nullable: true),
                    module = table.Column<string>(type: "varchar(3)", unicode: false, maxLength: 3, nullable: true),
                    @class = table.Column<string>(name: "class", type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    type = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: true),
                    classroom = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    teacher = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    week = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__orar__3213E83F5593F8F1", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "orar");
        }
    }
}
