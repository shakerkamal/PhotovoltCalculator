﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhotovoltCalculatorAPI.Migrations
{
    public partial class UserIdInRefreshToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "RefreshTokens");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "RefreshTokens",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
