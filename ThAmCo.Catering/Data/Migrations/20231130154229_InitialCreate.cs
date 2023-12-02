using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThAmCo.Catering.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FoodItem",
                columns: table => new
                {
                    FoodItemId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Price = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodItem", x => x.FoodItemId);
                });

            migrationBuilder.CreateTable(
                name: "Menu",
                columns: table => new
                {
                    MenuId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menu", x => x.MenuId);
                });

            migrationBuilder.CreateTable(
                name: "FoodBooking",
                columns: table => new
                {
                    BookingId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ClientReferenceId = table.Column<int>(type: "INTEGER", nullable: false),
                    NumberOfGuests = table.Column<int>(type: "INTEGER", nullable: false),
                    MenuId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodBooking", x => x.BookingId);
                    table.ForeignKey(
                        name: "FK_FoodBooking_Menu_MenuId",
                        column: x => x.MenuId,
                        principalTable: "Menu",
                        principalColumn: "MenuId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MenuFoodItem",
                columns: table => new
                {
                    FoodItemId = table.Column<int>(type: "INTEGER", nullable: false),
                    MenuId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuFoodItem", x => new { x.MenuId, x.FoodItemId });
                    table.ForeignKey(
                        name: "FK_MenuFoodItem_FoodItem_FoodItemId",
                        column: x => x.FoodItemId,
                        principalTable: "FoodItem",
                        principalColumn: "FoodItemId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MenuFoodItem_Menu_MenuId",
                        column: x => x.MenuId,
                        principalTable: "Menu",
                        principalColumn: "MenuId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "FoodItem",
                columns: new[] { "FoodItemId", "Name", "Price" },
                values: new object[] { 1, "Salmon", 10.0 });

            migrationBuilder.InsertData(
                table: "FoodItem",
                columns: new[] { "FoodItemId", "Name", "Price" },
                values: new object[] { 2, "Chicken", 11.0 });

            migrationBuilder.InsertData(
                table: "Menu",
                columns: new[] { "MenuId", "Name" },
                values: new object[] { 1, "Menu1" });

            migrationBuilder.InsertData(
                table: "Menu",
                columns: new[] { "MenuId", "Name" },
                values: new object[] { 2, "Menu2" });

            migrationBuilder.InsertData(
                table: "FoodBooking",
                columns: new[] { "BookingId", "ClientReferenceId", "MenuId", "NumberOfGuests" },
                values: new object[] { 1, 1, 1, 1 });

            migrationBuilder.InsertData(
                table: "FoodBooking",
                columns: new[] { "BookingId", "ClientReferenceId", "MenuId", "NumberOfGuests" },
                values: new object[] { 2, 2, 2, 2 });

            migrationBuilder.InsertData(
                table: "MenuFoodItem",
                columns: new[] { "FoodItemId", "MenuId" },
                values: new object[] { 1, 1 });

            migrationBuilder.InsertData(
                table: "MenuFoodItem",
                columns: new[] { "FoodItemId", "MenuId" },
                values: new object[] { 2, 2 });

            migrationBuilder.CreateIndex(
                name: "IX_FoodBooking_MenuId",
                table: "FoodBooking",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuFoodItem_FoodItemId",
                table: "MenuFoodItem",
                column: "FoodItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FoodBooking");

            migrationBuilder.DropTable(
                name: "MenuFoodItem");

            migrationBuilder.DropTable(
                name: "FoodItem");

            migrationBuilder.DropTable(
                name: "Menu");
        }
    }
}
