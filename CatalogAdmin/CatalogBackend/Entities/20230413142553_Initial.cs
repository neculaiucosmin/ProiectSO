#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace CatalogBackend.Entities;

/// <inheritdoc />
public partial class Initial : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            "orar",
            table => new
            {
                id = table.Column<long>("bigint", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                grp = table.Column<string>("varchar(10)", false, 10, nullable: true),
                dayOFfWeek = table.Column<string>("varchar(10)", false, 10, nullable: true),
                year = table.Column<short>("smallint", nullable: true),
                hours = table.Column<string>("varchar(12)", false, 12, nullable: true),
                module = table.Column<string>("varchar(3)", false, 3, nullable: true),
                @class = table.Column<string>(name: "class", type: "varchar(100)", unicode: false, maxLength: 100,
                    nullable: true),
                type = table.Column<string>("varchar(15)", false, 15, nullable: true),
                classroom = table.Column<string>("varchar(10)", false, 10, nullable: true),
                teacher = table.Column<string>("varchar(100)", false, 100, nullable: true),
                week = table.Column<string>("varchar(5)", false, 5, nullable: true)
            },
            constraints: table => { table.PrimaryKey("PK__orar__3213E83F5593F8F1", x => x.id); });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            "orar");
    }
}