using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DanceStudio.Catalog.DAL.Migrations
{
    /// <inheritdoc />
    public partial class InitialCatalogCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "dance_classes",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    difficultylevel = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    defaultprice = table.Column<decimal>(type: "numeric(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dance_classes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "instructors",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    firstname = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    lastname = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    bio = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    isactive = table.Column<bool>(type: "boolean", nullable: false),
                    createdat = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_instructors", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "dance_class_details",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false),
                    description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    videourl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    requirements = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dance_class_details", x => x.id);
                    table.ForeignKey(
                        name: "FK_dance_class_details_dance_classes_id",
                        column: x => x.id,
                        principalTable: "dance_classes",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "class_instructors",
                columns: table => new
                {
                    danceclassid = table.Column<long>(type: "bigint", nullable: false),
                    instructorid = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_class_instructors", x => new { x.danceclassid, x.instructorid });
                    table.ForeignKey(
                        name: "FK_class_instructors_dance_classes_danceclassid",
                        column: x => x.danceclassid,
                        principalTable: "dance_classes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_class_instructors_instructors_instructorid",
                        column: x => x.instructorid,
                        principalTable: "instructors",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_class_instructors_instructorid",
                table: "class_instructors",
                column: "instructorid");

            migrationBuilder.CreateIndex(
                name: "IX_instructors_email",
                table: "instructors",
                column: "email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "class_instructors");

            migrationBuilder.DropTable(
                name: "dance_class_details");

            migrationBuilder.DropTable(
                name: "instructors");

            migrationBuilder.DropTable(
                name: "dance_classes");
        }
    }
}
