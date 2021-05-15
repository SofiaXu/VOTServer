using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace VOTServer.Migrations
{
    public partial class First : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDelete = table.Column<bool>(type: "bit", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(24)", maxLength: 24, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccessLevel = table.Column<int>(type: "int", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDelete = table.Column<bool>(type: "bit", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserRoleId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_UserRole_UserRoleId",
                        column: x => x.UserRoleId,
                        principalTable: "UserRole",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Follows",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: true),
                    FollowerId = table.Column<long>(type: "bigint", nullable: false),
                    FolloweeId = table.Column<long>(type: "bigint", nullable: false),
                    FollowTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Follows", x => x.Id);
                    table.CheckConstraint("CK_NotEqual", "FollowerId <> FolloweeId");
                    table.ForeignKey(
                        name: "FK_Follows_Users_FolloweeId",
                        column: x => x.FolloweeId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Follows_Users_FollowerId",
                        column: x => x.FollowerId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserSecurities",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordUpdateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSecurities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserSecurities_Users_Id",
                        column: x => x.Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Videos",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Info = table.Column<string>(type: "TEXT", nullable: true),
                    UploadTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UploaderId = table.Column<long>(type: "bigint", nullable: false),
                    GoodCount = table.Column<long>(type: "bigint", nullable: false),
                    CommentsCount = table.Column<long>(type: "bigint", nullable: false),
                    FavoriteCount = table.Column<long>(type: "bigint", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Videos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Videos_Users_UploaderId",
                        column: x => x.UploaderId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDelete = table.Column<bool>(type: "bit", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(140)", maxLength: 140, nullable: true),
                    GoodCount = table.Column<long>(type: "bigint", nullable: false),
                    CommenterId = table.Column<long>(type: "bigint", nullable: false),
                    VideoId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Users_CommenterId",
                        column: x => x.CommenterId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Comments_Videos_VideoId",
                        column: x => x.VideoId,
                        principalTable: "Videos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Favorites",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: true),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    VideoId = table.Column<long>(type: "bigint", nullable: false),
                    AddedTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favorites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Favorites_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Favorites_Videos_VideoId",
                        column: x => x.VideoId,
                        principalTable: "Videos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "VideoTags",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TagId = table.Column<long>(type: "bigint", nullable: false),
                    VideoId = table.Column<long>(type: "bigint", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoTags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VideoTags_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_VideoTags_Videos_VideoId",
                        column: x => x.VideoId,
                        principalTable: "Videos",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "Id", "AccessLevel", "IsDelete", "Name" },
                values: new object[] { 1L, 999, null, "Administrators" });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "Id", "AccessLevel", "IsDelete", "Name" },
                values: new object[] { 2L, 1, null, "Guest" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "EmailAddress", "IsDelete", "UserName", "UserRoleId" },
                values: new object[] { 1L, "admin@aoba.site", null, "admin", 1L });

            migrationBuilder.InsertData(
                table: "UserSecurities",
                columns: new[] { "Id", "IsDelete", "PasswordHash", "PasswordUpdateTime", "PhoneNumber" },
                values: new object[] { 1L, null, "i8FdgByv78Ooocd0Q78EbAPBsHWdEcUkqt/2rWg6UmE=", new DateTime(2021, 5, 13, 22, 9, 40, 874, DateTimeKind.Local).AddTicks(6918), null });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_CommenterId",
                table: "Comments",
                column: "CommenterId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_VideoId",
                table: "Comments",
                column: "VideoId");

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_UserId_VideoId",
                table: "Favorites",
                columns: new[] { "UserId", "VideoId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_VideoId",
                table: "Favorites",
                column: "VideoId");

            migrationBuilder.CreateIndex(
                name: "IX_Follows_FolloweeId",
                table: "Follows",
                column: "FolloweeId");

            migrationBuilder.CreateIndex(
                name: "IX_Follows_FollowerId_FolloweeId",
                table: "Follows",
                columns: new[] { "FollowerId", "FolloweeId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tags_Name",
                table: "Tags",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Users_EmailAddress",
                table: "Users",
                column: "EmailAddress",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserRoleId",
                table: "Users",
                column: "UserRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSecurities_PhoneNumber",
                table: "UserSecurities",
                column: "PhoneNumber",
                unique: true,
                filter: "[PhoneNumber] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Videos_UploaderId",
                table: "Videos",
                column: "UploaderId");

            migrationBuilder.CreateIndex(
                name: "IX_VideoTags_TagId",
                table: "VideoTags",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_VideoTags_VideoId_TagId",
                table: "VideoTags",
                columns: new[] { "VideoId", "TagId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Favorites");

            migrationBuilder.DropTable(
                name: "Follows");

            migrationBuilder.DropTable(
                name: "UserSecurities");

            migrationBuilder.DropTable(
                name: "VideoTags");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Videos");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "UserRole");
        }
    }
}
