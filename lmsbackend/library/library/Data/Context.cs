using library.Models;
using Microsoft.EntityFrameworkCore;

namespace library.Data
{
    public class Context : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<BookCategory> BookCategories { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Order> Orders { get; set; }

        public Context(DbContextOptions<Context> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
         new User()
         {
             Id = 1,
             Name = "Admin",
             Email = "admin@gmail.com",
             MobileNumber = "1234567890",
             AccountStatus = AccountStatus.ACTIVE,
             UserType = UserType.ADMIN,
             Password = "admin1234",
             CreatedOn = new DateTime(2024, 06, 11, 13, 28, 12),
             TokensAvailable = 5,             
         },
         new User()
         {
             Id = 2,
             Name = "sai",
             Email = "sai@gmail.com",
             MobileNumber = "1234567890",
             AccountStatus = AccountStatus.ACTIVE,
             UserType = UserType.STUDENT,
             Password = "sai1234",
             CreatedOn = new DateTime(2024, 03, 06, 13, 30, 12),
             TokensAvailable = 5,
         },
         new User()
         {
             Id = 3,
             Name = "manu",
             Email = "manu@gmail.com",
             MobileNumber = "1234567890",
             AccountStatus = AccountStatus.ACTIVE,
             UserType = UserType.STUDENT,
             Password = "manu4321",
             CreatedOn = new DateTime(2024, 01, 06, 14, 30, 12),
             TokensAvailable = 5,
         },
           new User()
           {
               Id = 4,
               Name = "Rani",
               Email = "rani@gmail.com",
               MobileNumber = "1234567890",
               AccountStatus = AccountStatus.ACTIVE,
               UserType = UserType.STUDENT,
               Password = "rani43",
               CreatedOn = new DateTime(2024, 06, 06, 20, 40, 12),
               TokensAvailable = 5,
           }
     );

            modelBuilder.Entity<BookCategory>().HasData(
                new BookCategory { Id = 1, Category = "computer", SubCategory = "algorithm" },
                new BookCategory { Id = 2, Category = "computer", SubCategory = "programming languages" },
                new BookCategory { Id = 3, Category = "computer", SubCategory = "networking" },
                new BookCategory { Id = 4, Category = "computer", SubCategory = "hardware" },
                new BookCategory { Id = 5, Category = "mechanical", SubCategory = "machine" });

            modelBuilder.Entity<Book>().HasData(
            new Book { Id = 1, BookCategoryId = 1, Ordered = false, Price = 100, Author = "Thomas Corman", BookName = "Introduction to Algorithm", Description = "Algorithm is a step by step process", Rating = 5, userId = 1 },
            new Book { Id = 2, BookCategoryId = 1, Ordered = false, Price = 100, Author = "Thomas Corman", BookName = "Introduction to Algorithm", Description = "Renowned for its clarity and depth, this book provides an extensive exploration of algorithms and their applications. From sorting and searching algorithms to graph algorithms and data structures, it offers a wealth of knowledge supported by insightful explanations and practical examples.", Rating = 5, userId = 1 },
            new Book { Id = 3, BookCategoryId = 1, Ordered = false, Price = 200, Author = "Robert Sedgewick & Kevin Wayne", BookName = "Algorithms", Description = "This practical guide to algorithm design presents a holistic approach to problem-solving and algorithm development. ", Rating = 4, userId = 1 },
            new Book { Id = 4, BookCategoryId = 1, Ordered = false, Price = 300, Author = "Steve Skiena", BookName = "The Algorithm Design Manual", Description = "Designed for beginners, this book offers a beginner-friendly introduction to Python programming.", Rating = 5, userId = 1 },
            new Book { Id = 5, BookCategoryId = 1, Ordered = false, Price = 400, Author = "Adnan Aziz", BookName = "Algorithms For Interviews", Description = "With its unique and engaging approach, this book makes learning Python fun and accessible. ", Rating = 3, userId = 1 },
            new Book { Id = 6, BookCategoryId = 2, Ordered = false, Price = 700, Author = "Eric Matthes", BookName = "Python Crash Course: A Hands-On, Project-Based Introduction to Programming", Description = "This indispensable resource offers expert guidance on writing clean, efficient, and maintainable Java code", Rating = 3, userId = 1 },
            new Book { Id = 7, BookCategoryId = 2, Ordered = false, Price = 700, Author = "Eric Matthes", BookName = "Python Crash Course: A Hands-On, Project-Based Introduction to Programming", Description = "deal for beginners, this book provides a lively and interactive introduction to Java programming.", Rating = 3, userId = 1 },
            new Book { Id = 8, BookCategoryId = 2, Ordered = false, Price = 700, Author = "Eric Matthes", BookName = "Python Crash Course: A Hands-On, Project-Based Introduction to Programming", Description = "Designed for beginners, this book offers a beginner-friendly introduction to Python programming", Rating = 2, userId = 1 },
            new Book { Id = 9, BookCategoryId = 2, Ordered = false, Price = 800, Author = "Paul Barry", BookName = "Head First Python: A Brain-Friendly Guide", Description = "Algorithm is a step by step process", Rating = 2, userId = 1 },
            new Book { Id = 10, BookCategoryId = 2, Ordered = false, Price = 900, Author = "Joshua Bloch", BookName = "Effective Java", Description = "Algorithm is a step by step process", Rating = 3, userId = 3 },
            new Book { Id = 11, BookCategoryId = 2, Ordered = false, Price = 900, Author = "Joshua Bloch", BookName = "Effective Java", Description = "Algorithm is a step by step process", Rating = 4, userId = 3 },
            new Book { Id = 12, BookCategoryId = 3, Ordered = false, Price = 1400, Author = "James F Kurose and Keith W Ross", BookName = "A Top-Down Approach: Computer Networking", Description = "deal for beginners, this book provides a lively and interactive introduction to Java programming.", Rating = 4, userId = 2 },
            new Book { Id = 13, BookCategoryId = 3, Ordered = false, Price = 1500, Author = "Rich Seifert and James Edwards", BookName = "The All-New Switch Book (2nd Edition)", Description = "Designed for beginners, this book offers a beginner-friendly introduction to Python programming", Rating = 3, userId = 4 },
            new Book { Id = 14, BookCategoryId = 3, Ordered = false, Price = 1500, Author = "Rich Seifert and James Edwards", BookName = "The All-New Switch Book (2nd Edition)", Description = "This indispensable resource offers expert guidance on writing clean, efficient, and maintainable Java code", Rating = 5, userId = 1 },
            new Book { Id = 15, BookCategoryId = 3, Ordered = false, Price = 1600, Author = "Jerry FitzGerald, Alan Dennis, and Alexandra Durcikova", BookName = "Business Data Communications and Networking (14th Edition)", Description = "Algorithm is a step by step process", Rating = 3, userId = 2 },
            new Book { Id = 16, BookCategoryId = 4, Ordered = false, Price = 1700, Author = "Forouzan", BookName = "Data Communications and Networking with TCP/IP Protocol Suite, 6th Edition", Description = "Algorithm is a step by step process", Rating = 5, userId = 3 },
            new Book { Id = 17, BookCategoryId = 4, Ordered = false, Price = 1800, Author = "Gary Donahue", BookName = "Network Warrior, 2nd Edition", Description = "deal for beginners, this book provides a lively and interactive introduction to Java programming.", Rating = 5, userId = 3 },
            new Book { Id = 18, BookCategoryId = 5, Ordered = false, Price = 1800, Author = "Gary Donahue", BookName = "Network Warrior, 2nd Edition", Description = "This indispensable resource offers expert guidance on writing clean, efficient, and maintainable Java code", Rating = 2, userId = 2 },
            new Book { Id = 19, BookCategoryId = 5, Ordered = false, Price = 1800, Author = "Gary Donahue", BookName = "Network Warrior, 2nd Edition", Description = "Algorithm is a step by step process", Rating = 5, userId = 2 });

        }
        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<UserType>().HaveConversion<string>();
            configurationBuilder.Properties<AccountStatus>().HaveConversion<string>();
        }
    }
}
