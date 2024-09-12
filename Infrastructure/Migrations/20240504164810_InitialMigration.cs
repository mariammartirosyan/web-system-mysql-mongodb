using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Country = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    ZipCode = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AttractionType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttractionType", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "nvarchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Tour",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tour", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TouristAttraction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TouristAttraction", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(type: "nvarchar(30)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(30)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    AddressId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Address_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Accommodation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TourId = table.Column<int>(type: "int", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    CheckInDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CheckOutDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accommodation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accommodation_Tour_TourId",
                        column: x => x.TourId,
                        principalTable: "Tour",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TourTour",
                columns: table => new
                {
                    IncludedTourId = table.Column<int>(type: "int", nullable: false),
                    SupersetTourId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourTour", x => new { x.IncludedTourId, x.SupersetTourId });
                    table.ForeignKey(
                        name: "FK_TourTour_Tour_IncludedTourId",
                        column: x => x.IncludedTourId,
                        principalTable: "Tour",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TourTour_Tour_SupersetTourId",
                        column: x => x.SupersetTourId,
                        principalTable: "Tour",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Transportation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TourId = table.Column<int>(type: "int", nullable: false),
                    DepartureLocation = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    DepartureDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ArrivalLocation = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    ArrivalDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transportation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transportation_Tour_TourId",
                        column: x => x.TourId,
                        principalTable: "Tour",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AttractionTypeTouristAttraction",
                columns: table => new
                {
                    AttractionTypeId = table.Column<int>(type: "int", nullable: false),
                    TouristAttractionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttractionTypeTouristAttraction", x => new { x.AttractionTypeId, x.TouristAttractionId });
                    table.ForeignKey(
                        name: "FK_AttractionTypeTouristAttraction_AttractionType_AttractionTyp~",
                        column: x => x.AttractionTypeId,
                        principalTable: "AttractionType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AttractionTypeTouristAttraction_TouristAttraction_TouristAtt~",
                        column: x => x.TouristAttractionId,
                        principalTable: "TouristAttraction",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Booking",
                columns: table => new
                {
                    TourId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    BookingDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Booking", x => new { x.UserId, x.TourId, x.BookingDate });
                    table.ForeignKey(
                        name: "FK_Booking_Tour_TourId",
                        column: x => x.TourId,
                        principalTable: "Tour",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Booking_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RoleUser",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleUser", x => new { x.RoleId, x.UserId });
                    table.ForeignKey(
                        name: "FK_RoleUser_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleUser_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Address",
                columns: new[] { "Id", "City", "Country", "Street", "ZipCode" },
                values: new object[,]
                {
                    { 1, "Vienna", "Austria", "Mariahilfer Straße 1A", 1070 },
                    { 2, "Salzburg", "Austria", "Getreidegasse 9", 5020 },
                    { 3, "Innsbruck", "Austria", "Maria-Theresien-Straße 18", 6020 },
                    { 4, "Graz", "Austria", "Herrengasse 10", 8010 },
                    { 5, "Linz", "Austria", "Landstraße 35", 4020 },
                    { 6, "Klagenfurt", "Austria", "Bahnhofstraße 44", 9020 },
                    { 7, "Bregenz", "Austria", "Römerstraße 15", 6900 },
                    { 8, "Villach", "Austria", "Italiener Straße 21", 9500 },
                    { 9, "Wels", "Austria", "Stadtplatz 27", 4600 },
                    { 10, "St. Pölten", "Austria", "Wiener Straße 56", 3100 }
                });

            migrationBuilder.InsertData(
                table: "AttractionType",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "An official location where pieces of political, military, cultural, or social history have been preserved due to their cultural heritage value.", "Historic Site" },
                    { 2, "Building or institution that cares for and displays a collection of artifacts and other objects of artistic, cultural, historical, or scientific importance.", "Museum" },
                    { 3, "The original landscape that exists before it is acted upon by human culture.", "Natural Landscape" }
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Admin" },
                    { 2, "User" }
                });

            migrationBuilder.InsertData(
                table: "Tour",
                columns: new[] { "Id", "Description", "EndDate", "Price", "StartDate", "Status" },
                values: new object[,]
                {
                    { 1, "The tour will take place 27/02/2026 - 03/03/2026 and will cost 235$.", new DateTime(2026, 3, 3, 13, 52, 0, 0, DateTimeKind.Local), 235m, new DateTime(2026, 2, 27, 14, 47, 0, 0, DateTimeKind.Local), "Active" },
                    { 2, "The tour will take place 27/02/2026 - 03/03/2026 and will cost 235$.", new DateTime(2026, 3, 3, 13, 52, 0, 0, DateTimeKind.Local), 235m, new DateTime(2026, 2, 27, 14, 47, 0, 0, DateTimeKind.Local), "Active" },
                    { 3, "The tour will take place 27/02/2026 - 03/03/2026 and will cost 470$.", new DateTime(2026, 3, 3, 13, 52, 0, 0, DateTimeKind.Local), 470m, new DateTime(2026, 2, 27, 14, 47, 0, 0, DateTimeKind.Local), "Active" },
                    { 4, "The tour will take place 05/09/2024 - 15/09/2024 and will cost 195$.", new DateTime(2024, 9, 15, 1, 56, 0, 0, DateTimeKind.Local), 195m, new DateTime(2024, 9, 5, 0, 46, 0, 0, DateTimeKind.Local), "Active" },
                    { 5, "The tour will take place 12/12/2025 - 24/12/2025 and will cost 245$.", new DateTime(2025, 12, 24, 0, 13, 0, 0, DateTimeKind.Local), 245m, new DateTime(2025, 12, 12, 14, 43, 0, 0, DateTimeKind.Local), "Active" },
                    { 6, "The tour will take place 27/09/2024 - 08/10/2024 and will cost 190$.", new DateTime(2024, 10, 8, 14, 3, 0, 0, DateTimeKind.Local), 190m, new DateTime(2024, 9, 27, 16, 49, 0, 0, DateTimeKind.Local), "Active" },
                    { 7, "The tour will take place 05/09/2024 - 03/03/2026 and will cost 1090$.", new DateTime(2026, 3, 3, 13, 52, 0, 0, DateTimeKind.Local), 1090m, new DateTime(2024, 9, 5, 0, 46, 0, 0, DateTimeKind.Local), "Active" },
                    { 8, "The tour will take place 05/09/2024 - 03/03/2026 and will cost 1475$.", new DateTime(2026, 3, 3, 13, 52, 0, 0, DateTimeKind.Local), 1475m, new DateTime(2024, 9, 5, 0, 46, 0, 0, DateTimeKind.Local), "Active" },
                    { 9, "The tour will take place 05/09/2024 - 03/03/2026 and will cost 2565$.", new DateTime(2026, 3, 3, 13, 52, 0, 0, DateTimeKind.Local), 2565m, new DateTime(2024, 9, 5, 0, 46, 0, 0, DateTimeKind.Local), "Active" },
                    { 10, "The tour will take place 03/05/2025 - 09/05/2025 and will cost 240$.", new DateTime(2025, 5, 9, 9, 28, 0, 0, DateTimeKind.Local), 240m, new DateTime(2025, 5, 3, 15, 0, 0, 0, DateTimeKind.Local), "Active" },
                    { 11, "The tour will take place 11/04/2026 - 16/04/2026 and will cost 220$.", new DateTime(2026, 4, 16, 8, 25, 0, 0, DateTimeKind.Local), 220m, new DateTime(2026, 4, 11, 15, 21, 0, 0, DateTimeKind.Local), "Active" },
                    { 12, "The tour will take place 03/06/2024 - 14/06/2024 and will cost 90$.", new DateTime(2024, 6, 14, 19, 21, 0, 0, DateTimeKind.Local), 90m, new DateTime(2024, 6, 3, 6, 42, 0, 0, DateTimeKind.Local), "Active" },
                    { 13, "The tour will take place 27/09/2024 - 16/04/2026 and will cost 410$.", new DateTime(2026, 4, 16, 8, 25, 0, 0, DateTimeKind.Local), 410m, new DateTime(2024, 9, 27, 16, 49, 0, 0, DateTimeKind.Local), "Active" },
                    { 14, "The tour will take place 27/09/2024 - 16/04/2026 and will cost 1120$.", new DateTime(2026, 4, 16, 8, 25, 0, 0, DateTimeKind.Local), 1120m, new DateTime(2024, 9, 27, 16, 49, 0, 0, DateTimeKind.Local), "Active" },
                    { 15, "The tour will take place 15/03/2025 - 19/03/2025 and will cost 225$.", new DateTime(2025, 3, 19, 17, 2, 0, 0, DateTimeKind.Local), 225m, new DateTime(2025, 3, 15, 5, 32, 0, 0, DateTimeKind.Local), "Active" },
                    { 16, "The tour will take place 03/06/2024 - 03/03/2026 and will cost 1760$.", new DateTime(2026, 3, 3, 13, 52, 0, 0, DateTimeKind.Local), 1760m, new DateTime(2024, 6, 3, 6, 42, 0, 0, DateTimeKind.Local), "Active" },
                    { 17, "The tour will take place 05/09/2024 - 16/04/2026 and will cost 1785$.", new DateTime(2026, 4, 16, 8, 25, 0, 0, DateTimeKind.Local), 1785m, new DateTime(2024, 9, 5, 0, 46, 0, 0, DateTimeKind.Local), "Active" },
                    { 18, "The tour will take place 05/09/2024 - 16/04/2026 and will cost 3070$.", new DateTime(2026, 4, 16, 8, 25, 0, 0, DateTimeKind.Local), 3070m, new DateTime(2024, 9, 5, 0, 46, 0, 0, DateTimeKind.Local), "Active" },
                    { 19, "The tour will take place 26/09/2025 - 01/10/2025 and will cost 140$.", new DateTime(2025, 10, 1, 23, 10, 0, 0, DateTimeKind.Local), 140m, new DateTime(2025, 9, 26, 8, 20, 0, 0, DateTimeKind.Local), "Active" },
                    { 20, "The tour will take place 15/04/2026 - 25/04/2026 and will cost 90$.", new DateTime(2026, 4, 25, 6, 50, 0, 0, DateTimeKind.Local), 90m, new DateTime(2026, 4, 15, 13, 8, 0, 0, DateTimeKind.Local), "Active" },
                    { 21, "The tour will take place 27/12/2025 - 30/12/2025 and will cost 265$.", new DateTime(2025, 12, 30, 19, 44, 0, 0, DateTimeKind.Local), 265m, new DateTime(2025, 12, 27, 8, 25, 0, 0, DateTimeKind.Local), "Active" },
                    { 22, "The tour will take place 06/10/2025 - 09/10/2025 and will cost 110$.", new DateTime(2025, 10, 9, 5, 33, 0, 0, DateTimeKind.Local), 110m, new DateTime(2025, 10, 6, 7, 18, 0, 0, DateTimeKind.Local), "Active" },
                    { 23, "The tour will take place 05/09/2024 - 16/04/2026 and will cost 4890$.", new DateTime(2026, 4, 16, 8, 25, 0, 0, DateTimeKind.Local), 4890m, new DateTime(2024, 9, 5, 0, 46, 0, 0, DateTimeKind.Local), "Active" },
                    { 24, "The tour will take place 14/05/2024 - 18/05/2024 and will cost 130$.", new DateTime(2024, 5, 18, 5, 29, 0, 0, DateTimeKind.Local), 130m, new DateTime(2024, 5, 14, 0, 49, 0, 0, DateTimeKind.Local), "Active" },
                    { 25, "The tour will take place 11/07/2024 - 20/07/2024 and will cost 230$.", new DateTime(2024, 7, 20, 22, 12, 0, 0, DateTimeKind.Local), 230m, new DateTime(2024, 7, 11, 0, 3, 0, 0, DateTimeKind.Local), "Active" },
                    { 26, "The tour will take place 30/01/2026 - 03/02/2026 and will cost 225$.", new DateTime(2026, 2, 3, 23, 40, 0, 0, DateTimeKind.Local), 225m, new DateTime(2026, 1, 30, 11, 29, 0, 0, DateTimeKind.Local), "Active" },
                    { 27, "The tour will take place 03/06/2024 - 03/03/2026 and will cost 2235$.", new DateTime(2026, 3, 3, 13, 52, 0, 0, DateTimeKind.Local), 2235m, new DateTime(2024, 6, 3, 6, 42, 0, 0, DateTimeKind.Local), "Active" },
                    { 28, "The tour will take place 24/05/2025 - 04/06/2025 and will cost 280$.", new DateTime(2025, 6, 4, 18, 33, 0, 0, DateTimeKind.Local), 280m, new DateTime(2025, 5, 24, 14, 46, 0, 0, DateTimeKind.Local), "Active" },
                    { 29, "The tour will take place 15/03/2025 - 16/04/2026 and will cost 1035$.", new DateTime(2026, 4, 16, 8, 25, 0, 0, DateTimeKind.Local), 1035m, new DateTime(2025, 3, 15, 5, 32, 0, 0, DateTimeKind.Local), "Active" },
                    { 30, "The tour will take place 20/10/2025 - 28/10/2025 and will cost 90$.", new DateTime(2025, 10, 28, 14, 29, 0, 0, DateTimeKind.Local), 90m, new DateTime(2025, 10, 20, 6, 37, 0, 0, DateTimeKind.Local), "Active" },
                    { 31, "The tour will take place 06/03/2025 - 14/03/2025 and will cost 170$.", new DateTime(2025, 3, 14, 15, 19, 0, 0, DateTimeKind.Local), 170m, new DateTime(2025, 3, 6, 5, 39, 0, 0, DateTimeKind.Local), "Active" },
                    { 32, "The tour will take place 03/06/2024 - 03/03/2026 and will cost 3125$.", new DateTime(2026, 3, 3, 13, 52, 0, 0, DateTimeKind.Local), 3125m, new DateTime(2024, 6, 3, 6, 42, 0, 0, DateTimeKind.Local), "Active" },
                    { 33, "The tour will take place 20/01/2025 - 29/01/2025 and will cost 320$.", new DateTime(2025, 1, 29, 8, 0, 0, 0, DateTimeKind.Local), 320m, new DateTime(2025, 1, 20, 1, 49, 0, 0, DateTimeKind.Local), "Active" },
                    { 34, "The tour will take place 05/10/2025 - 14/10/2025 and will cost 145$.", new DateTime(2025, 10, 14, 11, 6, 0, 0, DateTimeKind.Local), 145m, new DateTime(2025, 10, 5, 4, 40, 0, 0, DateTimeKind.Local), "Active" },
                    { 35, "The tour will take place 11/07/2024 - 16/04/2026 and will cost 5580$.", new DateTime(2026, 4, 16, 8, 25, 0, 0, DateTimeKind.Local), 5580m, new DateTime(2024, 7, 11, 0, 3, 0, 0, DateTimeKind.Local), "Active" },
                    { 36, "The tour will take place 26/05/2025 - 04/06/2025 and will cost 260$.", new DateTime(2025, 6, 4, 13, 55, 0, 0, DateTimeKind.Local), 260m, new DateTime(2025, 5, 26, 17, 46, 0, 0, DateTimeKind.Local), "Active" },
                    { 37, "The tour will take place 12/02/2026 - 22/02/2026 and will cost 250$.", new DateTime(2026, 2, 22, 9, 4, 0, 0, DateTimeKind.Local), 250m, new DateTime(2026, 2, 12, 11, 18, 0, 0, DateTimeKind.Local), "Active" },
                    { 38, "The tour will take place 18/06/2024 - 29/06/2024 and will cost 160$.", new DateTime(2024, 6, 29, 7, 39, 0, 0, DateTimeKind.Local), 160m, new DateTime(2024, 6, 18, 4, 3, 0, 0, DateTimeKind.Local), "Active" },
                    { 39, "The tour will take place 14/05/2024 - 25/04/2026 and will cost 2625$.", new DateTime(2026, 4, 25, 6, 50, 0, 0, DateTimeKind.Local), 2625m, new DateTime(2024, 5, 14, 0, 49, 0, 0, DateTimeKind.Local), "Active" },
                    { 40, "The tour will take place 24/07/2024 - 29/07/2024 and will cost 200$.", new DateTime(2024, 7, 29, 19, 17, 0, 0, DateTimeKind.Local), 200m, new DateTime(2024, 7, 24, 12, 48, 0, 0, DateTimeKind.Local), "Active" },
                    { 41, "The tour will take place 14/05/2024 - 04/06/2025 and will cost 585$.", new DateTime(2025, 6, 4, 13, 55, 0, 0, DateTimeKind.Local), 585m, new DateTime(2024, 5, 14, 0, 49, 0, 0, DateTimeKind.Local), "Active" },
                    { 42, "The tour will take place 03/06/2024 - 03/03/2026 and will cost 2040$.", new DateTime(2026, 3, 3, 13, 52, 0, 0, DateTimeKind.Local), 2040m, new DateTime(2024, 6, 3, 6, 42, 0, 0, DateTimeKind.Local), "Active" },
                    { 43, "The tour will take place 11/07/2024 - 16/04/2026 and will cost 6000$.", new DateTime(2026, 4, 16, 8, 25, 0, 0, DateTimeKind.Local), 6000m, new DateTime(2024, 7, 11, 0, 3, 0, 0, DateTimeKind.Local), "Active" },
                    { 44, "The tour will take place 17/12/2024 - 21/12/2024 and will cost 330$.", new DateTime(2024, 12, 21, 21, 0, 0, 0, DateTimeKind.Local), 330m, new DateTime(2024, 12, 17, 15, 48, 0, 0, DateTimeKind.Local), "Active" }
                });

            migrationBuilder.InsertData(
                table: "TouristAttraction",
                columns: new[] { "Id", "Description", "Location", "Name" },
                values: new object[,]
                {
                    { 1, "https://carlowtourism.com/carlow-town-walk/", "6.929489,52.83525", "Carlow Town Walk - Slí na Sláinte" },
                    { 2, "http://carlingfordandcooleypeninsula.ie/carlingford-activities/walking-trails/t%C3%A1in-way", "-6.39708,54.004607", "The Táin Way" },
                    { 3, "http://www.heritageireland.ie/en", "-9.504292,52.018083", "Muckross Friary" },
                    { 4, "http://www.irishtrails.ie/Trail/O-Gormans-Lane-/583/", "-7.182288,52.592353", "Gorman Lane Loop Walk" },
                    { 5, "http://www.mayoroots.com/", "-9.222428,53.623485", "South Mayo Family Research Centre" },
                    { 6, "https://www.facebook.com/Kilgarvan-Motormuseum-708264642618370", "-9.434164,51.889134", "Kilgarvan Motor Museum" },
                    { 7, "https://www.castles.nl/kanturk-castle", "-8.90273,52.164382", "Kanturk Castle" },
                    { 8, "https://www.crawfordartgallery.ie/", "-8.47678, 51.89944", "Crawford Art Gallery" },
                    { 9, "https://heritageireland.ie/places-to-visit/newmills-corn-and-flax-mills/", "-7.808195,54.929103", "Newmills Corn And Flax Mills" },
                    { 10, "http://museum.maynoothcollege.ie", "-6.598102,53.378611", "Maynooth Ecclesiastical Museum" },
                    { 11, "http://www.thewesternway.ie", "-9.048728,53.276473", "Walking - The Western Way, Galway" },
                    { 12, "http://www.stbrigidscathedral.com/", "-6.911371,53.157927", "St Brigid’s Cathedral & Round Tower" },
                    { 13, "https://www.ballintubberabbey.ie/the-abbey/", "-9.282757,53.756722", "Ballintubber Abbey" },
                    { 14, "http://www.irishtrails.ie/trail/tipperary-heritage-way/36/", "-7.88583,52.516411", "Tipperary Heritage Way" },
                    { 15, "https://www.sportireland.ie/outdoors/walking/trails/mid-clare-way", "-8.899006,52.760668", "The Mid Clare Way" },
                    { 16, "https://www.littlemuseum.ie/", "-8.972707,52.346003", "The Little Museum of Dublin" },
                    { 17, "http://walkbroadfordashford.com/trail/killagholehane-way/", "-6.92881,52.602079", "Broadford Ashford Walking Trails - Killagholehane Way" },
                    { 18, "http://www.irishtrails.ie/Trail/South-Leinster-Way/33/", "-9.46143,51.902656", "The South Leinster Way" },
                    { 19, "http://www.historic-ireland.com/places/778.html", "-8.46535,54.2711", "Michael J Quill Centre" },
                    { 20, "https://sligowalks.ie/walks/the-sligo-way/", "-6.564811,52.961927", "The Sligo Way" },
                    { 21, "http://www.heritageireland.ie/en/midlands-eastcoast/dwyermcallistercottage", "-6.580391,52.686197", "Dwyer McAllister Cottage" },
                    { 22, "http://www.irishtrails.ie/Trail/Coolmelagh---Prospect-Loop/427/", "-7.619401,52.088633", "Coolmelagh - Prospect Loop" },
                    { 23, "https://www.waterfordmuseum.ie/exhibit/web", "-9.908755,53.996015", "Waterford County Museum" },
                    { 24, "https://www.huntmuseum.com/", "-8.62305, 52.66437", "Hunt Museum" },
                    { 25, "https://www.sportireland.ie/outdoors/walking/trails/rinn-duin-castle-loop?county=53&grade=402&length=All#list", "-8.004326,53.544197", "Rinn Duin Castle Loop" },
                    { 26, "http://www.coillteoutdoors.ie", "-10.029291,52.205709", "Glanteenassig Wood" }
                });

            migrationBuilder.InsertData(
                table: "Accommodation",
                columns: new[] { "Id", "CheckInDateTime", "CheckOutDateTime", "Location", "Price", "TourId", "Type" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 2, 27, 19, 3, 0, 0, DateTimeKind.Local), new DateTime(2026, 2, 28, 19, 3, 0, 0, DateTimeKind.Local), "-6.1882, 54.0425", 85m, 1, "Apartment" },
                    { 2, new DateTime(2026, 3, 2, 3, 36, 0, 0, DateTimeKind.Local), new DateTime(2026, 3, 3, 3, 36, 0, 0, DateTimeKind.Local), "-8.9059, 52.1770", 130m, 1, "Hotel" },
                    { 3, new DateTime(2024, 9, 12, 15, 18, 0, 0, DateTimeKind.Local), new DateTime(2024, 9, 13, 15, 18, 0, 0, DateTimeKind.Local), "-8.9059, 52.1770", 130m, 4, "Hotel" },
                    { 4, new DateTime(2025, 12, 20, 3, 36, 0, 0, DateTimeKind.Local), new DateTime(2025, 12, 21, 3, 36, 0, 0, DateTimeKind.Local), "-6.2672, 53.3498", 120m, 5, "Hotel" },
                    { 5, new DateTime(2025, 12, 21, 15, 28, 0, 0, DateTimeKind.Local), new DateTime(2025, 12, 22, 15, 28, 0, 0, DateTimeKind.Local), "-9.3152, 53.7649", 90m, 5, "Apartment" },
                    { 6, new DateTime(2024, 10, 4, 3, 31, 0, 0, DateTimeKind.Local), new DateTime(2024, 10, 5, 3, 31, 0, 0, DateTimeKind.Local), "-9.5254, 52.0208", 40m, 6, "Tent" },
                    { 7, new DateTime(2024, 10, 6, 7, 32, 0, 0, DateTimeKind.Local), new DateTime(2024, 10, 7, 7, 32, 0, 0, DateTimeKind.Local), "-6.1882, 54.0425", 85m, 6, "Apartment" },
                    { 8, new DateTime(2025, 5, 6, 3, 46, 0, 0, DateTimeKind.Local), new DateTime(2025, 5, 7, 3, 46, 0, 0, DateTimeKind.Local), "-6.2672, 53.3498", 120m, 10, "Hotel" },
                    { 9, new DateTime(2026, 4, 12, 11, 56, 0, 0, DateTimeKind.Local), new DateTime(2026, 4, 13, 11, 56, 0, 0, DateTimeKind.Local), "-7.1119, 52.2610", 100m, 11, "Hotel" },
                    { 10, new DateTime(2026, 4, 15, 4, 54, 0, 0, DateTimeKind.Local), new DateTime(2026, 4, 16, 4, 54, 0, 0, DateTimeKind.Local), "-8.4781, 51.8986", 95m, 11, "Apartment" },
                    { 11, new DateTime(2024, 6, 3, 8, 18, 0, 0, DateTimeKind.Local), new DateTime(2024, 6, 4, 8, 18, 0, 0, DateTimeKind.Local), "-7.7333, 54.9480", 45m, 12, "Tent" },
                    { 12, new DateTime(2025, 3, 15, 21, 33, 0, 0, DateTimeKind.Local), new DateTime(2025, 3, 16, 21, 33, 0, 0, DateTimeKind.Local), "-9.3152, 53.7649", 90m, 15, "Apartment" },
                    { 13, new DateTime(2025, 3, 18, 7, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 3, 19, 7, 0, 0, 0, DateTimeKind.Local), "-9.4598, 51.9831", 35m, 15, "Tent" },
                    { 14, new DateTime(2025, 9, 28, 18, 23, 0, 0, DateTimeKind.Local), new DateTime(2025, 9, 29, 18, 23, 0, 0, DateTimeKind.Local), "-6.2672, 53.3498", 120m, 19, "Hotel" },
                    { 15, new DateTime(2026, 4, 20, 8, 41, 0, 0, DateTimeKind.Local), new DateTime(2026, 4, 21, 8, 41, 0, 0, DateTimeKind.Local), "-9.4598, 51.9831", 35m, 20, "Tent" },
                    { 16, new DateTime(2025, 12, 27, 14, 10, 0, 0, DateTimeKind.Local), new DateTime(2025, 12, 28, 14, 10, 0, 0, DateTimeKind.Local), "-8.9059, 52.1770", 130m, 21, "Hotel" },
                    { 17, new DateTime(2025, 12, 30, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 12, 31, 0, 0, 0, 0, DateTimeKind.Local), "-6.1882, 54.0425", 85m, 21, "Apartment" },
                    { 18, new DateTime(2025, 10, 7, 3, 16, 0, 0, DateTimeKind.Local), new DateTime(2025, 10, 8, 3, 16, 0, 0, DateTimeKind.Local), "-9.5254, 52.0208", 40m, 22, "Tent" },
                    { 19, new DateTime(2024, 5, 15, 1, 37, 0, 0, DateTimeKind.Local), new DateTime(2024, 5, 16, 1, 37, 0, 0, DateTimeKind.Local), "-8.4781, 51.8986", 95m, 24, "Apartment" },
                    { 20, new DateTime(2024, 7, 15, 13, 28, 0, 0, DateTimeKind.Local), new DateTime(2024, 7, 16, 13, 28, 0, 0, DateTimeKind.Local), "-9.4598, 51.9831", 35m, 25, "Tent" },
                    { 21, new DateTime(2024, 7, 20, 10, 45, 0, 0, DateTimeKind.Local), new DateTime(2024, 7, 21, 10, 45, 0, 0, DateTimeKind.Local), "-8.9059, 52.1770", 130m, 25, "Hotel" },
                    { 22, new DateTime(2026, 1, 31, 8, 25, 0, 0, DateTimeKind.Local), new DateTime(2026, 2, 1, 8, 25, 0, 0, DateTimeKind.Local), "-9.5254, 52.0208", 40m, 26, "Tent" },
                    { 23, new DateTime(2026, 2, 2, 15, 38, 0, 0, DateTimeKind.Local), new DateTime(2026, 2, 3, 15, 38, 0, 0, DateTimeKind.Local), "-8.4781, 51.8986", 95m, 26, "Apartment" },
                    { 24, new DateTime(2025, 5, 31, 1, 58, 0, 0, DateTimeKind.Local), new DateTime(2025, 6, 1, 1, 58, 0, 0, DateTimeKind.Local), "-8.9059, 52.1770", 130m, 28, "Hotel" },
                    { 25, new DateTime(2025, 6, 2, 13, 6, 0, 0, DateTimeKind.Local), new DateTime(2025, 6, 3, 13, 6, 0, 0, DateTimeKind.Local), "-8.4781, 51.8986", 95m, 28, "Apartment" },
                    { 26, new DateTime(2025, 10, 23, 7, 34, 0, 0, DateTimeKind.Local), new DateTime(2025, 10, 24, 7, 34, 0, 0, DateTimeKind.Local), "-9.5254, 52.0208", 40m, 30, "Tent" },
                    { 27, new DateTime(2025, 3, 7, 5, 16, 0, 0, DateTimeKind.Local), new DateTime(2025, 3, 8, 5, 16, 0, 0, DateTimeKind.Local), "-6.5890, 53.3813", 110m, 31, "Hotel" },
                    { 28, new DateTime(2025, 1, 20, 6, 22, 0, 0, DateTimeKind.Local), new DateTime(2025, 1, 21, 6, 22, 0, 0, DateTimeKind.Local), "-8.9059, 52.1770", 130m, 33, "Hotel" },
                    { 29, new DateTime(2025, 1, 26, 3, 33, 0, 0, DateTimeKind.Local), new DateTime(2025, 1, 27, 3, 33, 0, 0, DateTimeKind.Local), "-8.9059, 52.1770", 130m, 33, "Hotel" },
                    { 30, new DateTime(2025, 10, 10, 19, 47, 0, 0, DateTimeKind.Local), new DateTime(2025, 10, 11, 19, 47, 0, 0, DateTimeKind.Local), "-6.1882, 54.0425", 85m, 34, "Apartment" },
                    { 31, new DateTime(2025, 10, 13, 0, 55, 0, 0, DateTimeKind.Local), new DateTime(2025, 10, 14, 0, 55, 0, 0, DateTimeKind.Local), "-9.4598, 51.9831", 35m, 34, "Tent" },
                    { 32, new DateTime(2025, 5, 28, 15, 34, 0, 0, DateTimeKind.Local), new DateTime(2025, 5, 29, 15, 34, 0, 0, DateTimeKind.Local), "-8.4781, 51.8986", 95m, 36, "Apartment" },
                    { 33, new DateTime(2025, 6, 2, 10, 42, 0, 0, DateTimeKind.Local), new DateTime(2025, 6, 3, 10, 42, 0, 0, DateTimeKind.Local), "-6.1882, 54.0425", 85m, 36, "Apartment" },
                    { 34, new DateTime(2026, 2, 12, 11, 24, 0, 0, DateTimeKind.Local), new DateTime(2026, 2, 13, 11, 24, 0, 0, DateTimeKind.Local), "-6.1882, 54.0425", 85m, 37, "Apartment" },
                    { 35, new DateTime(2026, 2, 18, 17, 51, 0, 0, DateTimeKind.Local), new DateTime(2026, 2, 19, 17, 51, 0, 0, DateTimeKind.Local), "-8.9059, 52.1770", 130m, 37, "Hotel" },
                    { 36, new DateTime(2024, 6, 24, 21, 41, 0, 0, DateTimeKind.Local), new DateTime(2024, 6, 25, 21, 41, 0, 0, DateTimeKind.Local), "-7.7333, 54.9480", 45m, 38, "Tent" },
                    { 37, new DateTime(2024, 6, 28, 0, 26, 0, 0, DateTimeKind.Local), new DateTime(2024, 6, 29, 0, 26, 0, 0, DateTimeKind.Local), "-9.4598, 51.9831", 35m, 38, "Tent" },
                    { 38, new DateTime(2024, 7, 26, 17, 10, 0, 0, DateTimeKind.Local), new DateTime(2024, 7, 27, 17, 10, 0, 0, DateTimeKind.Local), "-9.3152, 53.7649", 90m, 40, "Apartment" },
                    { 39, new DateTime(2024, 12, 18, 9, 40, 0, 0, DateTimeKind.Local), new DateTime(2024, 12, 19, 9, 40, 0, 0, DateTimeKind.Local), "-6.5890, 53.3813", 110m, 44, "Hotel" },
                    { 40, new DateTime(2024, 12, 20, 3, 6, 0, 0, DateTimeKind.Local), new DateTime(2024, 12, 21, 3, 6, 0, 0, DateTimeKind.Local), "-8.9059, 52.1770", 130m, 44, "Hotel" }
                });

            migrationBuilder.InsertData(
                table: "AttractionTypeTouristAttraction",
                columns: new[] { "AttractionTypeId", "TouristAttractionId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 3 },
                    { 1, 7 },
                    { 1, 12 },
                    { 1, 13 },
                    { 1, 17 },
                    { 1, 21 },
                    { 1, 25 },
                    { 2, 5 },
                    { 2, 6 },
                    { 2, 8 },
                    { 2, 9 },
                    { 2, 10 },
                    { 2, 16 },
                    { 2, 19 },
                    { 2, 21 },
                    { 2, 23 },
                    { 2, 24 },
                    { 3, 1 },
                    { 3, 2 },
                    { 3, 3 },
                    { 3, 4 },
                    { 3, 11 },
                    { 3, 14 },
                    { 3, 15 },
                    { 3, 18 },
                    { 3, 20 },
                    { 3, 22 },
                    { 3, 26 }
                });

            migrationBuilder.InsertData(
                table: "TourTour",
                columns: new[] { "IncludedTourId", "SupersetTourId" },
                values: new object[,]
                {
                    { 1, 2 },
                    { 1, 3 },
                    { 1, 7 },
                    { 1, 29 },
                    { 1, 35 },
                    { 1, 39 },
                    { 2, 3 },
                    { 3, 7 },
                    { 3, 14 },
                    { 3, 32 },
                    { 4, 7 },
                    { 4, 8 },
                    { 4, 16 },
                    { 4, 17 },
                    { 4, 18 },
                    { 4, 23 },
                    { 4, 41 },
                    { 5, 17 },
                    { 5, 23 },
                    { 5, 27 },
                    { 5, 29 },
                    { 6, 7 },
                    { 6, 8 },
                    { 6, 13 },
                    { 6, 43 },
                    { 7, 8 },
                    { 7, 9 },
                    { 7, 18 },
                    { 8, 9 },
                    { 8, 16 },
                    { 8, 23 },
                    { 9, 23 },
                    { 9, 32 },
                    { 10, 14 },
                    { 11, 13 },
                    { 11, 29 },
                    { 12, 16 },
                    { 12, 32 },
                    { 13, 14 },
                    { 13, 23 },
                    { 13, 39 },
                    { 14, 17 },
                    { 15, 17 },
                    { 15, 29 },
                    { 15, 35 },
                    { 16, 27 },
                    { 16, 39 },
                    { 16, 42 },
                    { 17, 18 },
                    { 20, 39 },
                    { 22, 29 },
                    { 23, 35 },
                    { 24, 39 },
                    { 24, 41 },
                    { 25, 27 },
                    { 25, 35 },
                    { 25, 43 },
                    { 28, 42 },
                    { 35, 43 },
                    { 36, 41 }
                });

            migrationBuilder.InsertData(
                table: "Transportation",
                columns: new[] { "Id", "ArrivalDateTime", "ArrivalLocation", "DepartureDateTime", "DepartureLocation", "Price", "TourId", "Type" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 3, 3, 2, 28, 0, 0, DateTimeKind.Local), "-6.1882, 54.0425", new DateTime(2026, 3, 1, 10, 6, 0, 0, DateTimeKind.Local), "-6.2672, 53.3498", 20m, 1, "Train" },
                    { 2, new DateTime(2024, 9, 11, 17, 47, 0, 0, DateTimeKind.Local), "-6.1882, 54.0425", new DateTime(2024, 9, 8, 5, 40, 0, 0, DateTimeKind.Local), "-6.2672, 53.3498", 20m, 4, "Train" },
                    { 3, new DateTime(2024, 9, 8, 22, 44, 0, 0, DateTimeKind.Local), "-8.9059, 52.1770", new DateTime(2024, 9, 8, 0, 11, 0, 0, DateTimeKind.Local), "-9.4598, 51.9831", 45m, 4, "Bus" },
                    { 4, new DateTime(2025, 12, 19, 12, 31, 0, 0, DateTimeKind.Local), "-9.3152, 53.7649", new DateTime(2025, 12, 17, 1, 34, 0, 0, DateTimeKind.Local), "-7.1119, 52.2610", 35m, 5, "Bus" },
                    { 5, new DateTime(2024, 10, 8, 0, 40, 0, 0, DateTimeKind.Local), "-9.7808, 53.4786", new DateTime(2024, 10, 4, 3, 27, 0, 0, DateTimeKind.Local), "-6.5890, 53.3813", 65m, 6, "Bus" },
                    { 6, new DateTime(2025, 5, 8, 3, 22, 0, 0, DateTimeKind.Local), "-9.7808, 53.4786", new DateTime(2025, 5, 5, 1, 37, 0, 0, DateTimeKind.Local), "-6.5890, 53.3813", 65m, 10, "Bus" },
                    { 7, new DateTime(2025, 5, 7, 17, 30, 0, 0, DateTimeKind.Local), "-7.7333, 54.9480", new DateTime(2025, 5, 6, 17, 2, 0, 0, DateTimeKind.Local), "-8.4781, 51.8986", 55m, 10, "Bus" },
                    { 8, new DateTime(2026, 4, 14, 11, 49, 0, 0, DateTimeKind.Local), "-9.5254, 52.0208", new DateTime(2026, 4, 11, 23, 47, 0, 0, DateTimeKind.Local), "-6.1882, 54.0425", 25m, 11, "Bus" },
                    { 9, new DateTime(2024, 6, 13, 21, 29, 0, 0, DateTimeKind.Local), "-8.9059, 52.1770", new DateTime(2024, 6, 10, 10, 26, 0, 0, DateTimeKind.Local), "-9.4598, 51.9831", 45m, 12, "Bus" },
                    { 10, new DateTime(2025, 3, 18, 7, 10, 0, 0, DateTimeKind.Local), "-6.5890, 53.3813", new DateTime(2025, 3, 15, 9, 3, 0, 0, DateTimeKind.Local), "-7.7333, 54.9480", 60m, 15, "Train" },
                    { 11, new DateTime(2025, 3, 18, 12, 39, 0, 0, DateTimeKind.Local), "-9.4598, 51.9831", new DateTime(2025, 3, 17, 6, 14, 0, 0, DateTimeKind.Local), "-9.3152, 53.7649", 40m, 15, "Train" },
                    { 12, new DateTime(2025, 9, 28, 2, 41, 0, 0, DateTimeKind.Local), "-6.1882, 54.0425", new DateTime(2025, 9, 26, 14, 26, 0, 0, DateTimeKind.Local), "-6.2672, 53.3498", 20m, 19, "Train" },
                    { 13, new DateTime(2026, 4, 18, 19, 13, 0, 0, DateTimeKind.Local), "-9.3152, 53.7649", new DateTime(2026, 4, 18, 8, 0, 0, 0, DateTimeKind.Local), "-7.1119, 52.2610", 35m, 20, "Bus" },
                    { 14, new DateTime(2026, 4, 20, 21, 6, 0, 0, DateTimeKind.Local), "-6.1882, 54.0425", new DateTime(2026, 4, 19, 16, 46, 0, 0, DateTimeKind.Local), "-6.2672, 53.3498", 20m, 20, "Train" },
                    { 15, new DateTime(2025, 12, 30, 15, 22, 0, 0, DateTimeKind.Local), "-6.1882, 54.0425", new DateTime(2025, 12, 27, 17, 50, 0, 0, DateTimeKind.Local), "-6.2672, 53.3498", 20m, 21, "Train" },
                    { 16, new DateTime(2025, 12, 29, 7, 42, 0, 0, DateTimeKind.Local), "-7.1119, 52.2610", new DateTime(2025, 12, 28, 9, 20, 0, 0, DateTimeKind.Local), "-9.5254, 52.0208", 30m, 21, "Train" },
                    { 17, new DateTime(2025, 10, 7, 4, 9, 0, 0, DateTimeKind.Local), "-8.9059, 52.1770", new DateTime(2025, 10, 6, 14, 10, 0, 0, DateTimeKind.Local), "-9.4598, 51.9831", 45m, 22, "Bus" },
                    { 18, new DateTime(2025, 10, 7, 10, 6, 0, 0, DateTimeKind.Local), "-9.5254, 52.0208", new DateTime(2025, 10, 6, 17, 51, 0, 0, DateTimeKind.Local), "-6.1882, 54.0425", 25m, 22, "Bus" },
                    { 19, new DateTime(2024, 5, 17, 3, 57, 0, 0, DateTimeKind.Local), "-9.3152, 53.7649", new DateTime(2024, 5, 15, 22, 59, 0, 0, DateTimeKind.Local), "-7.1119, 52.2610", 35m, 24, "Bus" },
                    { 20, new DateTime(2024, 7, 18, 8, 45, 0, 0, DateTimeKind.Local), "-8.9059, 52.1770", new DateTime(2024, 7, 14, 10, 31, 0, 0, DateTimeKind.Local), "-9.4598, 51.9831", 45m, 25, "Bus" },
                    { 21, new DateTime(2024, 7, 17, 23, 30, 0, 0, DateTimeKind.Local), "-6.1882, 54.0425", new DateTime(2024, 7, 16, 13, 5, 0, 0, DateTimeKind.Local), "-6.2672, 53.3498", 20m, 25, "Train" },
                    { 22, new DateTime(2026, 2, 3, 6, 10, 0, 0, DateTimeKind.Local), "-9.3152, 53.7649", new DateTime(2026, 2, 1, 17, 40, 0, 0, DateTimeKind.Local), "-7.1119, 52.2610", 35m, 26, "Bus" },
                    { 23, new DateTime(2026, 2, 2, 4, 20, 0, 0, DateTimeKind.Local), "-7.7333, 54.9480", new DateTime(2026, 1, 31, 18, 4, 0, 0, DateTimeKind.Local), "-8.4781, 51.8986", 55m, 26, "Bus" },
                    { 24, new DateTime(2025, 5, 31, 1, 45, 0, 0, DateTimeKind.Local), "-7.7333, 54.9480", new DateTime(2025, 5, 25, 18, 18, 0, 0, DateTimeKind.Local), "-8.4781, 51.8986", 55m, 28, "Bus" },
                    { 25, new DateTime(2025, 10, 26, 23, 59, 0, 0, DateTimeKind.Local), "-7.1119, 52.2610", new DateTime(2025, 10, 22, 16, 35, 0, 0, DateTimeKind.Local), "-9.5254, 52.0208", 30m, 30, "Train" },
                    { 26, new DateTime(2025, 10, 24, 15, 55, 0, 0, DateTimeKind.Local), "-6.1882, 54.0425", new DateTime(2025, 10, 24, 12, 46, 0, 0, DateTimeKind.Local), "-6.2672, 53.3498", 20m, 30, "Train" },
                    { 27, new DateTime(2025, 3, 13, 8, 29, 0, 0, DateTimeKind.Local), "-6.5890, 53.3813", new DateTime(2025, 3, 10, 2, 27, 0, 0, DateTimeKind.Local), "-7.7333, 54.9480", 60m, 31, "Train" },
                    { 28, new DateTime(2025, 1, 28, 22, 3, 0, 0, DateTimeKind.Local), "-7.1119, 52.2610", new DateTime(2025, 1, 22, 2, 1, 0, 0, DateTimeKind.Local), "-9.5254, 52.0208", 30m, 33, "Train" },
                    { 29, new DateTime(2025, 1, 26, 15, 46, 0, 0, DateTimeKind.Local), "-7.1119, 52.2610", new DateTime(2025, 1, 22, 15, 16, 0, 0, DateTimeKind.Local), "-9.5254, 52.0208", 30m, 33, "Train" },
                    { 30, new DateTime(2025, 10, 10, 19, 18, 0, 0, DateTimeKind.Local), "-9.5254, 52.0208", new DateTime(2025, 10, 7, 22, 38, 0, 0, DateTimeKind.Local), "-6.1882, 54.0425", 25m, 34, "Bus" },
                    { 31, new DateTime(2025, 5, 29, 7, 33, 0, 0, DateTimeKind.Local), "-9.3152, 53.7649", new DateTime(2025, 5, 27, 17, 48, 0, 0, DateTimeKind.Local), "-7.1119, 52.2610", 35m, 36, "Bus" },
                    { 32, new DateTime(2025, 5, 31, 4, 24, 0, 0, DateTimeKind.Local), "-8.9059, 52.1770", new DateTime(2025, 5, 26, 20, 55, 0, 0, DateTimeKind.Local), "-9.4598, 51.9831", 45m, 36, "Bus" },
                    { 33, new DateTime(2026, 2, 20, 6, 52, 0, 0, DateTimeKind.Local), "-9.3152, 53.7649", new DateTime(2026, 2, 16, 18, 1, 0, 0, DateTimeKind.Local), "-7.1119, 52.2610", 35m, 37, "Bus" },
                    { 34, new DateTime(2024, 6, 22, 9, 22, 0, 0, DateTimeKind.Local), "-9.3152, 53.7649", new DateTime(2024, 6, 19, 4, 4, 0, 0, DateTimeKind.Local), "-7.1119, 52.2610", 35m, 38, "Bus" },
                    { 35, new DateTime(2024, 6, 20, 15, 18, 0, 0, DateTimeKind.Local), "-8.9059, 52.1770", new DateTime(2024, 6, 20, 13, 45, 0, 0, DateTimeKind.Local), "-9.4598, 51.9831", 45m, 38, "Bus" },
                    { 36, new DateTime(2024, 7, 29, 3, 30, 0, 0, DateTimeKind.Local), "-8.9059, 52.1770", new DateTime(2024, 7, 25, 13, 28, 0, 0, DateTimeKind.Local), "-9.4598, 51.9831", 45m, 40, "Bus" },
                    { 37, new DateTime(2024, 7, 29, 7, 11, 0, 0, DateTimeKind.Local), "-9.7808, 53.4786", new DateTime(2024, 7, 25, 18, 44, 0, 0, DateTimeKind.Local), "-6.5890, 53.3813", 65m, 40, "Bus" },
                    { 38, new DateTime(2024, 12, 20, 20, 31, 0, 0, DateTimeKind.Local), "-8.4781, 51.8986", new DateTime(2024, 12, 20, 14, 29, 0, 0, DateTimeKind.Local), "-8.9059, 52.1770", 50m, 44, "Train" },
                    { 39, new DateTime(2024, 12, 20, 23, 31, 0, 0, DateTimeKind.Local), "-9.4598, 51.9831", new DateTime(2024, 12, 19, 12, 22, 0, 0, DateTimeKind.Local), "-9.3152, 53.7649", 40m, 44, "Train" }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "AddressId", "Email", "FirstName", "LastName", "Password" },
                values: new object[,]
                {
                    { 1, 1, "mariam@example.com", "Mariam", "Martirosyan", "pass" },
                    { 2, 3, "janesmith@example.com", "Jane", "Smith", "secure123" },
                    { 3, 2, "michaelj@example.com", "Michael", "Johnson", "johnson78" },
                    { 4, 5, "emilydavis@example.com", "Emily", "Davis", "password321" },
                    { 5, 8, "williamb@example.com", "William", "Brown", "willie2024" },
                    { 6, 7, "oliviajones@example.com", "Olivia", "Jones", "ojones123" },
                    { 7, 8, "noahm@example.com", "Noah", "Miller", "nmiller654" },
                    { 8, 4, "avawilson@example.com", "Ava", "Wilson", "avaPW2024" },
                    { 9, 10, "ethant@example.com", "Ethan", "Taylor", "etaylor321" },
                    { 10, 9, "sophiaa@example.com", "Sophia", "Anderson", "sophieA123" }
                });

            migrationBuilder.InsertData(
                table: "Booking",
                columns: new[] { "BookingDate", "TourId", "UserId", "Price" },
                values: new object[,]
                {
                    { new DateTime(2024, 6, 7, 14, 11, 0, 0, DateTimeKind.Local), 7, 1, 1090m },
                    { new DateTime(2024, 8, 13, 6, 15, 0, 0, DateTimeKind.Local), 9, 1, 2565m },
                    { new DateTime(2025, 4, 1, 22, 18, 0, 0, DateTimeKind.Local), 19, 1, 140m },
                    { new DateTime(2024, 12, 16, 2, 51, 0, 0, DateTimeKind.Local), 21, 1, 265m },
                    { new DateTime(2024, 7, 21, 18, 18, 0, 0, DateTimeKind.Local), 23, 1, 4890m },
                    { new DateTime(2024, 6, 2, 22, 29, 0, 0, DateTimeKind.Local), 25, 1, 230m },
                    { new DateTime(2024, 5, 11, 7, 21, 0, 0, DateTimeKind.Local), 33, 1, 320m },
                    { new DateTime(2024, 5, 25, 20, 59, 0, 0, DateTimeKind.Local), 35, 1, 5580m },
                    { new DateTime(2024, 11, 15, 1, 12, 0, 0, DateTimeKind.Local), 44, 1, 330m },
                    { new DateTime(2025, 6, 23, 21, 33, 0, 0, DateTimeKind.Local), 19, 2, 140m },
                    { new DateTime(2024, 6, 25, 16, 45, 0, 0, DateTimeKind.Local), 25, 2, 230m },
                    { new DateTime(2025, 1, 16, 13, 24, 0, 0, DateTimeKind.Local), 33, 2, 320m },
                    { new DateTime(2025, 1, 12, 4, 29, 0, 0, DateTimeKind.Local), 34, 2, 145m },
                    { new DateTime(2024, 5, 6, 20, 39, 0, 0, DateTimeKind.Local), 39, 2, 2625m },
                    { new DateTime(2024, 4, 20, 16, 12, 0, 0, DateTimeKind.Local), 41, 2, 585m },
                    { new DateTime(2024, 4, 26, 13, 5, 0, 0, DateTimeKind.Local), 13, 3, 410m },
                    { new DateTime(2024, 5, 1, 9, 18, 0, 0, DateTimeKind.Local), 16, 3, 1760m },
                    { new DateTime(2024, 5, 15, 4, 29, 0, 0, DateTimeKind.Local), 27, 3, 2235m },
                    { new DateTime(2025, 5, 6, 12, 50, 0, 0, DateTimeKind.Local), 28, 3, 280m },
                    { new DateTime(2024, 5, 7, 12, 41, 0, 0, DateTimeKind.Local), 39, 3, 2625m },
                    { new DateTime(2024, 6, 21, 0, 43, 0, 0, DateTimeKind.Local), 8, 4, 1475m },
                    { new DateTime(2024, 10, 26, 18, 46, 0, 0, DateTimeKind.Local), 10, 4, 240m },
                    { new DateTime(2025, 2, 17, 14, 52, 0, 0, DateTimeKind.Local), 10, 4, 240m },
                    { new DateTime(2024, 5, 25, 8, 28, 0, 0, DateTimeKind.Local), 12, 4, 90m },
                    { new DateTime(2024, 7, 16, 18, 9, 0, 0, DateTimeKind.Local), 14, 4, 1120m },
                    { new DateTime(2024, 5, 30, 15, 20, 0, 0, DateTimeKind.Local), 18, 4, 3070m },
                    { new DateTime(2024, 6, 1, 13, 0, 0, 0, DateTimeKind.Local), 27, 4, 2235m },
                    { new DateTime(2024, 7, 18, 18, 56, 0, 0, DateTimeKind.Local), 28, 4, 280m },
                    { new DateTime(2024, 10, 2, 9, 4, 0, 0, DateTimeKind.Local), 1, 5, 235m },
                    { new DateTime(2024, 5, 6, 11, 27, 0, 0, DateTimeKind.Local), 4, 5, 195m },
                    { new DateTime(2024, 12, 9, 19, 43, 0, 0, DateTimeKind.Local), 5, 5, 245m },
                    { new DateTime(2024, 7, 19, 4, 8, 0, 0, DateTimeKind.Local), 7, 5, 1090m },
                    { new DateTime(2024, 7, 8, 21, 28, 0, 0, DateTimeKind.Local), 9, 5, 2565m },
                    { new DateTime(2024, 10, 15, 4, 27, 0, 0, DateTimeKind.Local), 15, 5, 225m },
                    { new DateTime(2024, 5, 10, 17, 52, 0, 0, DateTimeKind.Local), 24, 5, 130m },
                    { new DateTime(2024, 8, 11, 7, 3, 0, 0, DateTimeKind.Local), 30, 5, 90m },
                    { new DateTime(2025, 4, 9, 12, 12, 0, 0, DateTimeKind.Local), 36, 5, 260m },
                    { new DateTime(2025, 4, 19, 16, 45, 0, 0, DateTimeKind.Local), 36, 5, 260m },
                    { new DateTime(2024, 5, 6, 0, 20, 0, 0, DateTimeKind.Local), 40, 5, 200m },
                    { new DateTime(2024, 7, 22, 13, 39, 0, 0, DateTimeKind.Local), 3, 6, 470m },
                    { new DateTime(2024, 4, 6, 5, 2, 0, 0, DateTimeKind.Local), 17, 6, 1785m },
                    { new DateTime(2024, 4, 13, 20, 19, 0, 0, DateTimeKind.Local), 25, 6, 230m },
                    { new DateTime(2024, 4, 15, 2, 58, 0, 0, DateTimeKind.Local), 25, 6, 230m },
                    { new DateTime(2025, 2, 22, 10, 50, 0, 0, DateTimeKind.Local), 29, 6, 1035m },
                    { new DateTime(2025, 1, 15, 5, 47, 0, 0, DateTimeKind.Local), 30, 6, 90m },
                    { new DateTime(2024, 5, 30, 12, 1, 0, 0, DateTimeKind.Local), 32, 6, 3125m },
                    { new DateTime(2025, 3, 5, 8, 1, 0, 0, DateTimeKind.Local), 34, 6, 145m },
                    { new DateTime(2024, 6, 14, 8, 36, 0, 0, DateTimeKind.Local), 35, 6, 5580m },
                    { new DateTime(2024, 5, 19, 17, 48, 0, 0, DateTimeKind.Local), 38, 6, 160m },
                    { new DateTime(2024, 9, 2, 2, 7, 0, 0, DateTimeKind.Local), 4, 7, 195m },
                    { new DateTime(2024, 6, 8, 0, 1, 0, 0, DateTimeKind.Local), 8, 7, 1475m },
                    { new DateTime(2024, 7, 21, 19, 55, 0, 0, DateTimeKind.Local), 14, 7, 1120m },
                    { new DateTime(2024, 4, 11, 14, 37, 0, 0, DateTimeKind.Local), 39, 7, 2625m },
                    { new DateTime(2024, 5, 31, 11, 7, 0, 0, DateTimeKind.Local), 42, 7, 2040m },
                    { new DateTime(2024, 6, 12, 10, 20, 0, 0, DateTimeKind.Local), 43, 7, 6000m },
                    { new DateTime(2025, 7, 25, 20, 59, 0, 0, DateTimeKind.Local), 1, 8, 235m },
                    { new DateTime(2025, 5, 7, 23, 33, 0, 0, DateTimeKind.Local), 3, 8, 470m },
                    { new DateTime(2024, 9, 24, 0, 47, 0, 0, DateTimeKind.Local), 6, 8, 190m },
                    { new DateTime(2024, 5, 12, 22, 39, 0, 0, DateTimeKind.Local), 12, 8, 90m },
                    { new DateTime(2024, 7, 22, 9, 59, 0, 0, DateTimeKind.Local), 15, 8, 225m },
                    { new DateTime(2024, 8, 11, 10, 10, 0, 0, DateTimeKind.Local), 17, 8, 1785m },
                    { new DateTime(2024, 8, 18, 7, 59, 0, 0, DateTimeKind.Local), 17, 8, 1785m },
                    { new DateTime(2025, 8, 5, 13, 19, 0, 0, DateTimeKind.Local), 19, 8, 140m },
                    { new DateTime(2024, 7, 8, 19, 40, 0, 0, DateTimeKind.Local), 33, 8, 320m },
                    { new DateTime(2024, 11, 5, 2, 56, 0, 0, DateTimeKind.Local), 37, 8, 250m },
                    { new DateTime(2024, 4, 13, 6, 17, 0, 0, DateTimeKind.Local), 39, 8, 2625m },
                    { new DateTime(2024, 5, 3, 23, 13, 0, 0, DateTimeKind.Local), 7, 9, 1090m },
                    { new DateTime(2024, 7, 11, 19, 8, 0, 0, DateTimeKind.Local), 8, 9, 1475m },
                    { new DateTime(2024, 4, 11, 20, 12, 0, 0, DateTimeKind.Local), 9, 9, 2565m },
                    { new DateTime(2024, 9, 5, 5, 38, 0, 0, DateTimeKind.Local), 11, 9, 220m },
                    { new DateTime(2025, 7, 24, 1, 37, 0, 0, DateTimeKind.Local), 20, 9, 90m },
                    { new DateTime(2025, 11, 21, 5, 30, 0, 0, DateTimeKind.Local), 20, 9, 90m },
                    { new DateTime(2024, 5, 3, 7, 46, 0, 0, DateTimeKind.Local), 24, 9, 130m },
                    { new DateTime(2024, 9, 25, 7, 1, 0, 0, DateTimeKind.Local), 28, 9, 280m },
                    { new DateTime(2024, 12, 18, 22, 40, 0, 0, DateTimeKind.Local), 28, 9, 280m },
                    { new DateTime(2025, 4, 16, 6, 9, 0, 0, DateTimeKind.Local), 30, 9, 90m },
                    { new DateTime(2024, 11, 12, 0, 15, 0, 0, DateTimeKind.Local), 31, 9, 170m },
                    { new DateTime(2024, 4, 7, 4, 21, 0, 0, DateTimeKind.Local), 35, 9, 5580m },
                    { new DateTime(2024, 11, 22, 7, 19, 0, 0, DateTimeKind.Local), 19, 10, 140m }
                });

            migrationBuilder.InsertData(
                table: "RoleUser",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 2, 2 },
                    { 2, 3 },
                    { 2, 4 },
                    { 2, 5 },
                    { 2, 6 },
                    { 2, 7 },
                    { 2, 8 },
                    { 2, 9 },
                    { 2, 10 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accommodation_TourId",
                table: "Accommodation",
                column: "TourId");

            migrationBuilder.CreateIndex(
                name: "IX_AttractionTypeTouristAttraction_TouristAttractionId",
                table: "AttractionTypeTouristAttraction",
                column: "TouristAttractionId");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_TourId",
                table: "Booking",
                column: "TourId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleUser_UserId",
                table: "RoleUser",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TourTour_SupersetTourId",
                table: "TourTour",
                column: "SupersetTourId");

            migrationBuilder.CreateIndex(
                name: "IX_Transportation_TourId",
                table: "Transportation",
                column: "TourId");

            migrationBuilder.CreateIndex(
                name: "IX_User_AddressId",
                table: "User",
                column: "AddressId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accommodation");

            migrationBuilder.DropTable(
                name: "AttractionTypeTouristAttraction");

            migrationBuilder.DropTable(
                name: "Booking");

            migrationBuilder.DropTable(
                name: "RoleUser");

            migrationBuilder.DropTable(
                name: "TourTour");

            migrationBuilder.DropTable(
                name: "Transportation");

            migrationBuilder.DropTable(
                name: "AttractionType");

            migrationBuilder.DropTable(
                name: "TouristAttraction");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Tour");

            migrationBuilder.DropTable(
                name: "Address");
        }
    }
}
