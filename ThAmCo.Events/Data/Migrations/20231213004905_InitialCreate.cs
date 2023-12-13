﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThAmCo.Events.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    EventId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    DateAndTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    MenuId = table.Column<int>(type: "INTEGER", nullable: false),
                    BookingId = table.Column<int>(type: "INTEGER", nullable: false),
                    EventTypeId = table.Column<string>(type: "TEXT", nullable: true),
                    Reference = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.EventId);
                });

            migrationBuilder.CreateTable(
                name: "Guest",
                columns: table => new
                {
                    GuestId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guest", x => x.GuestId);
                });

            migrationBuilder.CreateTable(
                name: "Staff",
                columns: table => new
                {
                    StaffId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staff", x => x.StaffId);
                });

            migrationBuilder.CreateTable(
                name: "EventGuest",
                columns: table => new
                {
                    EventId = table.Column<int>(type: "INTEGER", nullable: false),
                    GuestId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventGuest", x => new { x.EventId, x.GuestId });
                    table.ForeignKey(
                        name: "FK_EventGuest_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "EventId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventGuest_Guest_GuestId",
                        column: x => x.GuestId,
                        principalTable: "Guest",
                        principalColumn: "GuestId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EventStaff",
                columns: table => new
                {
                    EventId = table.Column<int>(type: "INTEGER", nullable: false),
                    StaffId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventStaff", x => new { x.EventId, x.StaffId });
                    table.ForeignKey(
                        name: "FK_EventStaff_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "EventId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventStaff_Staff_StaffId",
                        column: x => x.StaffId,
                        principalTable: "Staff",
                        principalColumn: "StaffId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "EventId", "BookingId", "DateAndTime", "EventTypeId", "MenuId", "Name", "Reference" },
                values: new object[] { 1, 47531, new DateTime(2023, 1, 10, 10, 0, 0, 0, DateTimeKind.Unspecified), null, 0, "Event 1", 59310 });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "EventId", "BookingId", "DateAndTime", "EventTypeId", "MenuId", "Name", "Reference" },
                values: new object[] { 2, 35105, new DateTime(2023, 1, 11, 10, 0, 0, 0, DateTimeKind.Unspecified), null, 0, "Event 2", 93821 });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "EventId", "BookingId", "DateAndTime", "EventTypeId", "MenuId", "Name", "Reference" },
                values: new object[] { 3, 62318, new DateTime(2023, 9, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), null, 0, "Event 3", 21523 });

            migrationBuilder.InsertData(
                table: "Guest",
                columns: new[] { "GuestId", "FirstName", "LastName" },
                values: new object[] { 1, "Samuel", "Emmerson" });

            migrationBuilder.InsertData(
                table: "Guest",
                columns: new[] { "GuestId", "FirstName", "LastName" },
                values: new object[] { 2, "Tony", "Flanigan" });

            migrationBuilder.InsertData(
                table: "Guest",
                columns: new[] { "GuestId", "FirstName", "LastName" },
                values: new object[] { 3, "Jonathon", "Bartwick" });

            migrationBuilder.InsertData(
                table: "Guest",
                columns: new[] { "GuestId", "FirstName", "LastName" },
                values: new object[] { 4, "Benjamin", "Greenaway" });

            migrationBuilder.InsertData(
                table: "Guest",
                columns: new[] { "GuestId", "FirstName", "LastName" },
                values: new object[] { 5, "Stephen", "Bainbridge" });

            migrationBuilder.InsertData(
                table: "Guest",
                columns: new[] { "GuestId", "FirstName", "LastName" },
                values: new object[] { 6, "Matthew", "Bunny" });

            migrationBuilder.InsertData(
                table: "Staff",
                columns: new[] { "StaffId", "FirstName", "LastName" },
                values: new object[] { 1, "Josh", "Parkinson" });

            migrationBuilder.InsertData(
                table: "Staff",
                columns: new[] { "StaffId", "FirstName", "LastName" },
                values: new object[] { 2, "James", "Swales" });

            migrationBuilder.InsertData(
                table: "Staff",
                columns: new[] { "StaffId", "FirstName", "LastName" },
                values: new object[] { 3, "Lee", "Harrison" });

            migrationBuilder.InsertData(
                table: "Staff",
                columns: new[] { "StaffId", "FirstName", "LastName" },
                values: new object[] { 4, "Thomas", "Hadfield" });

            migrationBuilder.InsertData(
                table: "Staff",
                columns: new[] { "StaffId", "FirstName", "LastName" },
                values: new object[] { 5, "Greg", "Tomlinson" });

            migrationBuilder.InsertData(
                table: "Staff",
                columns: new[] { "StaffId", "FirstName", "LastName" },
                values: new object[] { 6, "Billy", "Blackburn" });

            migrationBuilder.InsertData(
                table: "EventGuest",
                columns: new[] { "EventId", "GuestId" },
                values: new object[] { 1, 1 });

            migrationBuilder.InsertData(
                table: "EventGuest",
                columns: new[] { "EventId", "GuestId" },
                values: new object[] { 1, 2 });

            migrationBuilder.InsertData(
                table: "EventGuest",
                columns: new[] { "EventId", "GuestId" },
                values: new object[] { 2, 3 });

            migrationBuilder.InsertData(
                table: "EventGuest",
                columns: new[] { "EventId", "GuestId" },
                values: new object[] { 2, 4 });

            migrationBuilder.InsertData(
                table: "EventGuest",
                columns: new[] { "EventId", "GuestId" },
                values: new object[] { 3, 5 });

            migrationBuilder.InsertData(
                table: "EventGuest",
                columns: new[] { "EventId", "GuestId" },
                values: new object[] { 3, 6 });

            migrationBuilder.InsertData(
                table: "EventStaff",
                columns: new[] { "EventId", "StaffId" },
                values: new object[] { 1, 1 });

            migrationBuilder.InsertData(
                table: "EventStaff",
                columns: new[] { "EventId", "StaffId" },
                values: new object[] { 1, 2 });

            migrationBuilder.InsertData(
                table: "EventStaff",
                columns: new[] { "EventId", "StaffId" },
                values: new object[] { 2, 3 });

            migrationBuilder.InsertData(
                table: "EventStaff",
                columns: new[] { "EventId", "StaffId" },
                values: new object[] { 2, 4 });

            migrationBuilder.InsertData(
                table: "EventStaff",
                columns: new[] { "EventId", "StaffId" },
                values: new object[] { 3, 5 });

            migrationBuilder.InsertData(
                table: "EventStaff",
                columns: new[] { "EventId", "StaffId" },
                values: new object[] { 3, 6 });

            migrationBuilder.CreateIndex(
                name: "IX_EventGuest_GuestId",
                table: "EventGuest",
                column: "GuestId");

            migrationBuilder.CreateIndex(
                name: "IX_EventStaff_StaffId",
                table: "EventStaff",
                column: "StaffId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventGuest");

            migrationBuilder.DropTable(
                name: "EventStaff");

            migrationBuilder.DropTable(
                name: "Guest");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Staff");
        }
    }
}
