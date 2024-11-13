using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace library.Migrations
{
    public partial class IntialDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BookCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubCategory = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MobileNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TokensAvailable = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountStatus = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    Ordered = table.Column<bool>(type: "bit", nullable: false),
                    BookCategoryId = table.Column<int>(type: "int", nullable: false),
                    userId = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_BookCategories_BookCategoryId",
                        column: x => x.BookCategoryId,
                        principalTable: "BookCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    BookId = table.Column<int>(type: "int", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Returned = table.Column<bool>(type: "bit", nullable: false),
                    ReturnDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FinePaid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "BookCategories",
                columns: new[] { "Id", "Category", "SubCategory" },
                values: new object[,]
                {
                    { 1, "computer", "algorithm" },
                    { 2, "computer", "programming languages" },
                    { 3, "computer", "networking" },
                    { 4, "computer", "hardware" },
                    { 5, "mechanical", "machine" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccountStatus", "CreatedOn", "Email", "MobileNumber", "Name", "Password", "TokensAvailable", "UserType" },
                values: new object[,]
                {
                    { 1, "ACTIVE", new DateTime(2024, 6, 11, 13, 28, 12, 0, DateTimeKind.Unspecified), "admin@gmail.com", "1234567890", "Admin", "admin1234", 0, "ADMIN" },
                    { 2, "ACTIVE", new DateTime(2024, 3, 6, 13, 30, 12, 0, DateTimeKind.Unspecified), "sai@gmail.com", "1234567890", "sai", "sai1234", 0, "STUDENT" },
                    { 3, "ACTIVE", new DateTime(2024, 1, 6, 14, 30, 12, 0, DateTimeKind.Unspecified), "manu@gmail.com", "1234567890", "manu", "manu4321", 0, "STUDENT" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "BookCategoryId", "BookName", "Description", "Ordered", "Price", "Rating", "userId" },
                values: new object[,]
                {
                    { 1, "Thomas Corman", 1, "Introduction to Algorithm", "Algorithm is a step by step process", false, 100f, 5, 1 },
                    { 2, "Thomas Corman", 1, "Introduction to Algorithm", "Renowned for its clarity and depth, this book provides an extensive exploration of algorithms and their applications. From sorting and searching algorithms to graph algorithms and data structures, it offers a wealth of knowledge supported by insightful explanations and practical examples.", false, 100f, 5, 1 },
                    { 3, "Robert Sedgewick & Kevin Wayne", 1, "Algorithms", "This practical guide to algorithm design presents a holistic approach to problem-solving and algorithm development. ", false, 200f, 4, 1 },
                    { 4, "Steve Skiena", 1, "The Algorithm Design Manual", "Designed for beginners, this book offers a beginner-friendly introduction to Python programming.", false, 300f, 5, 1 },
                    { 5, "Adnan Aziz", 1, "Algorithms For Interviews", "With its unique and engaging approach, this book makes learning Python fun and accessible. ", false, 400f, 3, 1 },
                    { 6, "Eric Matthes", 2, "Python Crash Course: A Hands-On, Project-Based Introduction to Programming", "This indispensable resource offers expert guidance on writing clean, efficient, and maintainable Java code", false, 700f, 3, 1 },
                    { 7, "Eric Matthes", 2, "Python Crash Course: A Hands-On, Project-Based Introduction to Programming", "deal for beginners, this book provides a lively and interactive introduction to Java programming.", false, 700f, 3, 1 },
                    { 8, "Eric Matthes", 2, "Python Crash Course: A Hands-On, Project-Based Introduction to Programming", "Designed for beginners, this book offers a beginner-friendly introduction to Python programming", false, 700f, 2, 1 },
                    { 9, "Paul Barry", 2, "Head First Python: A Brain-Friendly Guide", "Algorithm is a step by step process", false, 800f, 2, 1 },
                    { 10, "Joshua Bloch", 2, "Effective Java", "Algorithm is a step by step process", false, 900f, 3, 3 },
                    { 11, "Joshua Bloch", 2, "Effective Java", "Algorithm is a step by step process", false, 900f, 4, 3 },
                    { 12, "James F Kurose and Keith W Ross", 3, "A Top-Down Approach: Computer Networking", "deal for beginners, this book provides a lively and interactive introduction to Java programming.", false, 1400f, 4, 2 },
                    { 13, "Rich Seifert and James Edwards", 3, "The All-New Switch Book (2nd Edition)", "Designed for beginners, this book offers a beginner-friendly introduction to Python programming", false, 1500f, 3, 4 },
                    { 14, "Rich Seifert and James Edwards", 3, "The All-New Switch Book (2nd Edition)", "This indispensable resource offers expert guidance on writing clean, efficient, and maintainable Java code", false, 1500f, 5, 1 },
                    { 15, "Jerry FitzGerald, Alan Dennis, and Alexandra Durcikova", 3, "Business Data Communications and Networking (14th Edition)", "Algorithm is a step by step process", false, 1600f, 3, 2 },
                    { 16, "Forouzan", 4, "Data Communications and Networking with TCP/IP Protocol Suite, 6th Edition", "Algorithm is a step by step process", false, 1700f, 5, 3 },
                    { 17, "Gary Donahue", 4, "Network Warrior, 2nd Edition", "deal for beginners, this book provides a lively and interactive introduction to Java programming.", false, 1800f, 5, 3 },
                    { 18, "Gary Donahue", 5, "Network Warrior, 2nd Edition", "This indispensable resource offers expert guidance on writing clean, efficient, and maintainable Java code", false, 1800f, 2, 2 },
                    { 19, "Gary Donahue", 5, "Network Warrior, 2nd Edition", "Algorithm is a step by step process", false, 1800f, 5, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_BookCategoryId",
                table: "Books",
                column: "BookCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_BookId",
                table: "Orders",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "BookCategories");
        }
    }
}
