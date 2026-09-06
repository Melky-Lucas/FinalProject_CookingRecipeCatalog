using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitPostgre : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ingredientcategories",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ingredientcategories", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "measureunits",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    abbreviation = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_measureunits", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "passwords",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    passwordhash = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    updatedat = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_passwords", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "recipecategories",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_recipecategories", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ingredients",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    imageurl = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    ingredientcategoryid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ingredients", x => x.id);
                    table.ForeignKey(
                        name: "fk_ingredients_ingredientcategories_ingredientcategoryid",
                        column: x => x.ingredientcategoryid,
                        principalTable: "ingredientcategories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    username = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    passwordid = table.Column<int>(type: "integer", nullable: false),
                    roleid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                    table.ForeignKey(
                        name: "fk_users_passwords_passwordid",
                        column: x => x.passwordid,
                        principalTable: "passwords",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_users_roles_roleid",
                        column: x => x.roleid,
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "recipes",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    imageurl = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    preparationtime = table.Column<TimeSpan>(type: "interval", nullable: false),
                    cookingtime = table.Column<TimeSpan>(type: "interval", nullable: false),
                    servings = table.Column<int>(type: "integer", nullable: false),
                    difficulty = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    calories = table.Column<int>(type: "integer", nullable: false),
                    userid = table.Column<int>(type: "integer", nullable: false),
                    ispublic = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_recipes", x => x.id);
                    table.ForeignKey(
                        name: "fk_recipes_users_userid",
                        column: x => x.userid,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "cookingsteps",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    recipeid = table.Column<int>(type: "integer", nullable: false),
                    stepnumber = table.Column<int>(type: "integer", nullable: false),
                    title = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    estimatedduration = table.Column<TimeSpan>(type: "interval", nullable: false),
                    instruction = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cookingsteps", x => x.id);
                    table.ForeignKey(
                        name: "fk_cookingsteps_recipes_recipeid",
                        column: x => x.recipeid,
                        principalTable: "recipes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "recipe_categories",
                columns: table => new
                {
                    categoryid = table.Column<int>(type: "integer", nullable: false),
                    recipeid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_recipe_categories", x => new { x.recipeid, x.categoryid });
                    table.ForeignKey(
                        name: "fk_recipe_categories_recipecategories_categoryid",
                        column: x => x.categoryid,
                        principalTable: "recipecategories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_recipe_categories_recipes_recipeid",
                        column: x => x.recipeid,
                        principalTable: "recipes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "recipe_ingredients",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    recipeid = table.Column<int>(type: "integer", nullable: false),
                    ingredientid = table.Column<int>(type: "integer", nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: false),
                    unitid = table.Column<int>(type: "integer", nullable: false),
                    isoptional = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_recipe_ingredients", x => x.id);
                    table.ForeignKey(
                        name: "fk_recipe_ingredients_ingredients_ingredientid",
                        column: x => x.ingredientid,
                        principalTable: "ingredients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_recipe_ingredients_measureunits_unitid",
                        column: x => x.unitid,
                        principalTable: "measureunits",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_recipe_ingredients_recipes_recipeid",
                        column: x => x.recipeid,
                        principalTable: "recipes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tips",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    userid = table.Column<int>(type: "integer", nullable: false),
                    recipeid = table.Column<int>(type: "integer", nullable: false),
                    content = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tips", x => x.id);
                    table.ForeignKey(
                        name: "fk_tips_recipes_recipeid",
                        column: x => x.recipeid,
                        principalTable: "recipes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_tips_users_userid",
                        column: x => x.userid,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "ix_cookingsteps_recipeid",
                table: "cookingsteps",
                column: "recipeid");

            migrationBuilder.CreateIndex(
                name: "ix_ingredientcategories_name",
                table: "ingredientcategories",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_ingredients_imageurl",
                table: "ingredients",
                column: "imageurl",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_ingredients_ingredientcategoryid",
                table: "ingredients",
                column: "ingredientcategoryid");

            migrationBuilder.CreateIndex(
                name: "ix_ingredients_name",
                table: "ingredients",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_measureunits_abbreviation",
                table: "measureunits",
                column: "abbreviation",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_measureunits_name",
                table: "measureunits",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_recipe_categories_categoryid",
                table: "recipe_categories",
                column: "categoryid");

            migrationBuilder.CreateIndex(
                name: "ix_recipe_ingredients_ingredientid",
                table: "recipe_ingredients",
                column: "ingredientid");

            migrationBuilder.CreateIndex(
                name: "ix_recipe_ingredients_recipeid",
                table: "recipe_ingredients",
                column: "recipeid");

            migrationBuilder.CreateIndex(
                name: "ix_recipe_ingredients_unitid",
                table: "recipe_ingredients",
                column: "unitid");

            migrationBuilder.CreateIndex(
                name: "ix_recipecategories_name",
                table: "recipecategories",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_recipes_imageurl",
                table: "recipes",
                column: "imageurl",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_recipes_title",
                table: "recipes",
                column: "title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_recipes_userid",
                table: "recipes",
                column: "userid");

            migrationBuilder.CreateIndex(
                name: "ix_roles_name",
                table: "roles",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_tips_recipeid",
                table: "tips",
                column: "recipeid");

            migrationBuilder.CreateIndex(
                name: "ix_tips_userid",
                table: "tips",
                column: "userid");

            migrationBuilder.CreateIndex(
                name: "ix_users_email",
                table: "users",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_users_passwordid",
                table: "users",
                column: "passwordid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_users_roleid",
                table: "users",
                column: "roleid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cookingsteps");

            migrationBuilder.DropTable(
                name: "recipe_categories");

            migrationBuilder.DropTable(
                name: "recipe_ingredients");

            migrationBuilder.DropTable(
                name: "tips");

            migrationBuilder.DropTable(
                name: "recipecategories");

            migrationBuilder.DropTable(
                name: "ingredients");

            migrationBuilder.DropTable(
                name: "measureunits");

            migrationBuilder.DropTable(
                name: "recipes");

            migrationBuilder.DropTable(
                name: "ingredientcategories");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "passwords");

            migrationBuilder.DropTable(
                name: "roles");
        }
    }
}
