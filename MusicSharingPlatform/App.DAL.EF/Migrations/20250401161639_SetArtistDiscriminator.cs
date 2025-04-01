using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.DAL.EF.Migrations
{
    /// <inheritdoc />
    public partial class SetArtistDiscriminator : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArtistsInTracks_AspNetUsers_ArtistId",
                table: "ArtistsInTracks");

            migrationBuilder.DropForeignKey(
                name: "FK_Playlists_AspNetUsers_ArtistId",
                table: "Playlists");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_AspNetUsers_ArtistId",
                table: "Ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_UserLinks_AspNetUsers_ArtistId",
                table: "UserLinks");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSavedTracks_AspNetUsers_ArtistId",
                table: "UserSavedTracks");

            migrationBuilder.DropIndex(
                name: "IX_UserSavedTracks_ArtistId",
                table: "UserSavedTracks");

            migrationBuilder.DropIndex(
                name: "IX_UserLinks_ArtistId",
                table: "UserLinks");

            migrationBuilder.DropIndex(
                name: "IX_Ratings_ArtistId",
                table: "Ratings");

            migrationBuilder.DropIndex(
                name: "IX_Playlists_ArtistId",
                table: "Playlists");

            migrationBuilder.DropIndex(
                name: "IX_ArtistsInTracks_ArtistId",
                table: "ArtistsInTracks");

            migrationBuilder.DropColumn(
                name: "ArtistId",
                table: "UserSavedTracks");

            migrationBuilder.DropColumn(
                name: "ArtistId",
                table: "UserLinks");

            migrationBuilder.DropColumn(
                name: "ArtistId",
                table: "Ratings");

            migrationBuilder.DropColumn(
                name: "ArtistId",
                table: "Playlists");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ArtistId",
                table: "ArtistsInTracks");

            migrationBuilder.AlterColumn<string>(
                name: "DisplayName",
                table: "AspNetUsers",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50,
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ArtistId",
                table: "UserSavedTracks",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ArtistId",
                table: "UserLinks",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ArtistId",
                table: "Ratings",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ArtistId",
                table: "Playlists",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DisplayName",
                table: "AspNetUsers",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "character varying(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ArtistId",
                table: "ArtistsInTracks",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserSavedTracks_ArtistId",
                table: "UserSavedTracks",
                column: "ArtistId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLinks_ArtistId",
                table: "UserLinks",
                column: "ArtistId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_ArtistId",
                table: "Ratings",
                column: "ArtistId");

            migrationBuilder.CreateIndex(
                name: "IX_Playlists_ArtistId",
                table: "Playlists",
                column: "ArtistId");

            migrationBuilder.CreateIndex(
                name: "IX_ArtistsInTracks_ArtistId",
                table: "ArtistsInTracks",
                column: "ArtistId");

            migrationBuilder.AddForeignKey(
                name: "FK_ArtistsInTracks_AspNetUsers_ArtistId",
                table: "ArtistsInTracks",
                column: "ArtistId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Playlists_AspNetUsers_ArtistId",
                table: "Playlists",
                column: "ArtistId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_AspNetUsers_ArtistId",
                table: "Ratings",
                column: "ArtistId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserLinks_AspNetUsers_ArtistId",
                table: "UserLinks",
                column: "ArtistId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserSavedTracks_AspNetUsers_ArtistId",
                table: "UserSavedTracks",
                column: "ArtistId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
