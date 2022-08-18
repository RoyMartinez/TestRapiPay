using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cards",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    UserCreatorId = table.Column<int>(nullable: false),
                    Numbers = table.Column<string>(nullable: true),
                    CVV = table.Column<string>(nullable: true),
                    Balance = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    UserCreatorId = table.Column<int>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Records",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    UserCreatorId = table.Column<int>(nullable: false),
                    CardId = table.Column<int>(nullable: false),
                    PaymentReference = table.Column<string>(nullable: true),
                    Amount = table.Column<decimal>(nullable: false),
                    Fee = table.Column<decimal>(nullable: false),
                    Total = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Records", x => x.Id);
                    table.ForeignKey(
                        name: "fk_Records_Cards",
                        column: x => x.CardId,
                        principalTable: "Cards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Cards",
                columns: new[] { "Id", "Balance", "CVV", "CreationTime", "Numbers", "UserCreatorId" },
                values: new object[,]
                {
                    { 1, 1000000m, "001", new DateTime(2022, 8, 18, 12, 2, 47, 369, DateTimeKind.Local).AddTicks(4367), "400024001234567", 1 },
                    { 2, 1000000m, "001", new DateTime(2022, 8, 18, 12, 2, 47, 370, DateTimeKind.Local).AddTicks(3843), "500024001234567", 2 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreationTime", "Password", "UserCreatorId", "UserName" },
                values: new object[,]
                {
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "123", 0, "RoyMartinez" },
                    { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "123", 0, "JuanPerez" }
                });

            migrationBuilder.CreateIndex(
                name: "Uq_Cards_Numbers",
                table: "Cards",
                column: "Numbers",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Records_CardId",
                table: "Records",
                column: "CardId");

            migrationBuilder.CreateIndex(
                name: "Uq_Records_Reference",
                table: "Records",
                column: "PaymentReference",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "Uq_User_UserName",
                table: "Users",
                column: "UserName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Records");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Cards");
        }
    }
}
