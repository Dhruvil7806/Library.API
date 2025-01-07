using System.Data.Entity;
namespace Library.API.Data

{
    public class LibraryDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<BorrowRecord> BorrowRecords { get; set; }

        public LibraryDbContext() : base("name=LibraryDbConnection") { }
    }
}