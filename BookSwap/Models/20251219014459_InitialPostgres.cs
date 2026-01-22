using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookSwap.Migrations
{
    /// <inheritdoc />
    public partial class InitialPostgres : Migration
    {
        private const string TableBooks = "Books";
        private const string TableUsers = "Users";
        private const string TableListings = "Listings";
        private const string TableRatings = "Ratings";
        private const string TableChatMessage = "ChatMessage";
        private const string TableTransaction = "Transaction";

        private const string TypeInteger = "integer";
        private const string TypeText = "text";
        private const string TypeTimestampTz = "timestamp with time zone";

        private const string AnnotationValueGeneration = "Npgsql:ValueGenerationStrategy";

        private const string ColumnUserId = "UserId";
        private const string ColumnBookId = "BookId";
        private const string ColumnListingId = "ListingId";
        private const string ColumnTransactionId = "TransactionId";

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: TableBooks,
                columns: table => new
                {
                    BookId = table.Column<int>(type: TypeInteger, nullable: false)
                        .Annotation(AnnotationValueGeneration, NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: TypeText, nullable: false),
                    Author = table.Column<string>(type: TypeText, nullable: false),
                    Subject = table.Column<string>(type: TypeText, nullable: false),
                    Description = table.Column<string>(type: TypeText, nullable: false),
                    ImageUrl = table.Column<string>(type: TypeText, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.BookId);
                });

            migrationBuilder.CreateTable(
                name: TableUsers,
                columns: table => new
                {
                    UserId = table.Column<int>(type: TypeInteger, nullable: false)
                        .Annotation(AnnotationValueGeneration, NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Email = table.Column<string>(type: TypeText, nullable: false),
                    FullName = table.Column<string>(type: TypeText, nullable: false),
                    Role = table.Column<string>(type: TypeText, nullable: false),
                    PasswordHash = table.Column<string>(type: TypeText, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: TableListings,
                columns: table => new
                {
                    ListingId = table.Column<int>(type: TypeInteger, nullable: false)
                        .Annotation(AnnotationValueGeneration, NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: TypeInteger, nullable: false),
                    BookId = table.Column<int>(type: TypeInteger, nullable: false),
                    Type = table.Column<string>(type: TypeText, nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: true),
                    Status = table.Column<string>(type: TypeText, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: TypeTimestampTz, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Listings", x => x.ListingId);
                    table.ForeignKey(
                        name: "FK_Listings_Books_BookId",
                        column: x => x.BookId,
                        principalTable: TableBooks,
                        principalColumn: ColumnBookId,
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Listings_Users_UserId",
                        column: x => x.UserId,
                        principalTable: TableUsers,
                        principalColumn: ColumnUserId,
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: TableRatings,
                columns: table => new
                {
                    RatingId = table.Column<int>(type: TypeInteger, nullable: false)
                        .Annotation(AnnotationValueGeneration, NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FromUserId = table.Column<int>(type: TypeInteger, nullable: false),
                    ToUserId = table.Column<int>(type: TypeInteger, nullable: false),
                    Stars = table.Column<int>(type: TypeInteger, nullable: false),
                    Comment = table.Column<string>(type: TypeText, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: TypeTimestampTz, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => x.RatingId);
                    table.ForeignKey(
                        name: "FK_Ratings_Users_FromUserId",
                        column: x => x.FromUserId,
                        principalTable: TableUsers,
                        principalColumn: ColumnUserId,
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ratings_Users_ToUserId",
                        column: x => x.ToUserId,
                        principalTable: TableUsers,
                        principalColumn: ColumnUserId,
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: TableChatMessage,
                columns: table => new
                {
                    MessageId = table.Column<int>(type: TypeInteger, nullable: false)
                        .Annotation(AnnotationValueGeneration, NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FromUserId = table.Column<int>(type: TypeInteger, nullable: false),
                    ToUserId = table.Column<int>(type: TypeInteger, nullable: false),
                    ListingId = table.Column<int>(type: TypeInteger, nullable: false),
                    MessageText = table.Column<string>(type: TypeText, nullable: false),
                    SentAt = table.Column<DateTime>(type: TypeTimestampTz, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatMessage", x => x.MessageId);
                    table.ForeignKey(
                        name: "FK_ChatMessage_Listings_ListingId",
                        column: x => x.ListingId,
                        principalTable: TableListings,
                        principalColumn: ColumnListingId,
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChatMessage_Users_FromUserId",
                        column: x => x.FromUserId,
                        principalTable: TableUsers,
                        principalColumn: ColumnUserId,
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChatMessage_Users_ToUserId",
                        column: x => x.ToUserId,
                        principalTable: TableUsers,
                        principalColumn: ColumnUserId,
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: TableTransaction,
                columns: table => new
                {
                    TransactionId = table.Column<int>(type: TypeInteger, nullable: false)
                        .Annotation(AnnotationValueGeneration, NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ListingId = table.Column<int>(type: TypeInteger, nullable: false),
                    LenderId = table.Column<int>(type: TypeInteger, nullable: false),
                    BorrowerId = table.Column<int>(type: TypeInteger, nullable: false),
                    Type = table.Column<string>(type: TypeText, nullable: false),
                    StartedAt = table.Column<DateTime>(type: TypeTimestampTz, nullable: false),
                    EndedAt = table.Column<DateTime>(type: TypeTimestampTz, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.TransactionId);
                    table.ForeignKey(
                        name: "FK_Transaction_Listings_ListingId",
                        column: x => x.ListingId,
                        principalTable: TableListings,
                        principalColumn: ColumnListingId,
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transaction_Users_BorrowerId",
                        column: x => x.BorrowerId,
                        principalTable: TableUsers,
                        principalColumn: ColumnUserId,
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transaction_Users_LenderId",
                        column: x => x.LenderId,
                        principalTable: TableUsers,
                        principalColumn: ColumnUserId,
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(TableChatMessage);
            migrationBuilder.DropTable(TableRatings);
            migrationBuilder.DropTable(TableTransaction);
            migrationBuilder.DropTable(TableListings);
            migrationBuilder.DropTable(TableBooks);
            migrationBuilder.DropTable(TableUsers);
        }
    }
}
