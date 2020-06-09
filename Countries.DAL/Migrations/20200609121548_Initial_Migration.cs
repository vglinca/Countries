using Microsoft.EntityFrameworkCore.Migrations;

namespace Countries.DAL.Migrations
{
    public partial class Initial_Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Iso639_1 = table.Column<string>(nullable: true),
                    Iso639_2 = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Regions",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Area = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Capital = table.Column<string>(nullable: true),
                    Area = table.Column<double>(nullable: false),
                    NumericCode = table.Column<int>(nullable: false),
                    Population = table.Column<int>(nullable: false),
                    RegionId = table.Column<long>(nullable: false),
                    SubRegion = table.Column<string>(nullable: true),
                    CurrencyId = table.Column<long>(nullable: false),
                    Alpha2Code = table.Column<string>(nullable: true),
                    Alpha3Code = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Countries_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Countries_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CountryLanguages",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryId = table.Column<long>(nullable: false),
                    LanguageId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryLanguages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CountryLanguages_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CountryLanguages_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Currencies",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[,]
                {
                    { 1L, "EUR", "Euro" },
                    { 12L, "BOB", "Bolivian boliviano" },
                    { 11L, "ARS", "Argentine peso" },
                    { 9L, "MYR", "Malaysian ringgit" },
                    { 8L, "KWD", "Kuwaiti dinar" },
                    { 7L, "JOD", "Jordanian dinar" },
                    { 10L, "CAD", "Canadian dollar" },
                    { 5L, "ISK", "Icelandic króna" },
                    { 4L, "DKK", "Danish krone" },
                    { 3L, "CZK", "Czech koruna" },
                    { 2L, "ALL", "Albanian lek" },
                    { 6L, "JPY", "Japanese yen" }
                });

            migrationBuilder.InsertData(
                table: "Languages",
                columns: new[] { "Id", "Iso639_1", "Iso639_2", "Name" },
                values: new object[,]
                {
                    { 11L, "", "zsm", "Malaysian" },
                    { 17L, "qu", "que", "Quechua" },
                    { 16L, "ay", "aym", "Aymara" },
                    { 15L, "gn", "grn", "Guarani" },
                    { 14L, "es", "spa", "Spanish" },
                    { 13L, "fr", "fra", "French" },
                    { 12L, "en", "eng", "English" },
                    { 10L, "ar", "ara", "Arabic" },
                    { 5L, "fo", "fao", "Faroese" },
                    { 8L, "ja", "jpn", "Japanese" },
                    { 7L, "is", "isl", "Icelandic" },
                    { 6L, "de", "deu", "German" },
                    { 4L, "sk", "slk", "Slovak" },
                    { 3L, "cs", "ces", "Czech" },
                    { 2L, "sq", "sqi", "Albanian" },
                    { 1L, "et", "est", "Estonian" },
                    { 9L, "ar", "ara", "Arabic" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Area", "Name" },
                values: new object[,]
                {
                    { 4L, 30370000.0, "Africa" },
                    { 1L, 10180000.0, "Europe" },
                    { 2L, 44580000.0, "Asia" },
                    { 3L, 8526000.0, "Australia and Oceania" },
                    { 5L, 42550000.0, "Americas" }
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Alpha2Code", "Alpha3Code", "Area", "Capital", "CurrencyId", "Name", "NumericCode", "Population", "RegionId", "SubRegion" },
                values: new object[,]
                {
                    { 1L, "EE", "EST", 45227.0, "Tallinn", 1L, "Estonia", 233, 1315944, 1L, "Northern Europe" },
                    { 2L, "AL", "ALB", 28748.0, "Tirana", 2L, "Albania", 8, 2886026, 1L, "Southern Europe" },
                    { 3L, "CZ", "CZE", 78865.0, "Prague", 3L, "Czech Republic", 203, 10558524, 1L, "Western Europe" },
                    { 4L, "DK", "DNK", 1393.0, "Tórshavn", 4L, "Faroe Islands", 234, 49376, 1L, "Northern Europe" },
                    { 5L, "DE", "DEU", 357114.0, "Berlin", 1L, "Germany", 276, 81770900, 1L, "Western Europe" },
                    { 6L, "IS", "ISL", 103000.0, "Reykjavík", 5L, "Iceland", 352, 334300, 1L, "Northern Europe" },
                    { 7L, "JP", "JPN", 377930.0, "Tokyo", 6L, "Japan", 392, 126960000, 2L, "Eastern Asia" },
                    { 8L, "JO", "JOR", 89342.0, "Amman", 7L, "Jordan", 400, 9531712, 2L, "Western Asia" },
                    { 9L, "KW", "KWT", 17818.0, "Kuwait City", 8L, "Kuwait", 414, 4183658, 2L, "Western Asia" },
                    { 10L, "MY", "MYS", 45227.0, "Kuala Lumpur", 9L, "Malaysia", 458, 31405416, 2L, "South-Eastern Asia" },
                    { 11L, "CA", "CAN", 9984670.0, "Ottawa", 10L, "Canada", 124, 36155487, 5L, "North America" },
                    { 12L, "AR", "ARG", 2780400.0, "Buenos Aires", 11L, "Argentina", 414, 43590400, 5L, "South America" },
                    { 13L, "BO", "BOL", 1098581.0, "Sucre", 12L, "Bolivia", 68, 10985059, 5L, "South America" }
                });

            migrationBuilder.InsertData(
                table: "CountryLanguages",
                columns: new[] { "Id", "CountryId", "LanguageId" },
                values: new object[,]
                {
                    { 1L, 1L, 1L },
                    { 16L, 13L, 14L },
                    { 15L, 12L, 15L },
                    { 14L, 12L, 14L },
                    { 13L, 11L, 13L },
                    { 12L, 11L, 12L },
                    { 11L, 10L, 11L },
                    { 10L, 9L, 9L },
                    { 9L, 8L, 9L },
                    { 8L, 7L, 8L },
                    { 7L, 6L, 7L },
                    { 6L, 5L, 6L },
                    { 5L, 4L, 5L },
                    { 4L, 3L, 4L },
                    { 3L, 3L, 3L },
                    { 2L, 2L, 2L },
                    { 17L, 13L, 16L },
                    { 18L, 13L, 17L }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Countries_CurrencyId",
                table: "Countries",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Countries_RegionId",
                table: "Countries",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_CountryLanguages_CountryId",
                table: "CountryLanguages",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_CountryLanguages_LanguageId",
                table: "CountryLanguages",
                column: "LanguageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CountryLanguages");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "Languages");

            migrationBuilder.DropTable(
                name: "Currencies");

            migrationBuilder.DropTable(
                name: "Regions");
        }
    }
}
