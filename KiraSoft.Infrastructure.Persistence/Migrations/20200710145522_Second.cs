using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KiraSoft.Infrastructure.Persistence.Migrations
{
    public partial class Second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirstFamilyName",
                table: "User",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SecondFamilyName",
                table: "User",
                maxLength: 128,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TokensHistory",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 50, nullable: true),
                    CreationDate = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    LastModifiedBy = table.Column<string>(maxLength: 50, nullable: true),
                    LastModificationDate = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    DeletedBy = table.Column<string>(maxLength: 50, nullable: true),
                    DeletionDate = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    Enabled = table.Column<bool>(nullable: false),
                    TokenId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    UserName = table.Column<string>(nullable: false),
                    UserEmail = table.Column<string>(nullable: false),
                    Iat = table.Column<DateTime>(nullable: false),
                    Nbf = table.Column<DateTime>(nullable: false),
                    Exp = table.Column<DateTime>(nullable: false),
                    Audience = table.Column<string>(nullable: false),
                    Issuer = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TokensHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TokensHistory_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TokensHistory_UserId",
                table: "TokensHistory",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TokensHistory");

            migrationBuilder.DropColumn(
                name: "FirstFamilyName",
                table: "User");

            migrationBuilder.DropColumn(
                name: "SecondFamilyName",
                table: "User");
        }
    }
}
