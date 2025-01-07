
// BorrowRecord Model
using System;

public class BorrowRecord
{
    public int ID { get; set; }
    public int BookID { get; set; }
    public int MemberID { get; set; }
    public DateTime BorrowDate { get; set; }
    public DateTime? ReturnDate { get; set; }

    // Navigation Properties
    public virtual Book Book { get; set; }
    public virtual Member Member { get; set; }
}
