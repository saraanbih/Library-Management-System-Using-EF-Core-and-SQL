using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Library_Management_System_Task.Data;

[Table("borrowed_books")]
public partial class BorrowedBook
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("book_id")]
    public int BookId { get; set; }

    [Column("borrower_id")]
    public int BorrowerId { get; set; }

    [Column("borrowed_date", TypeName = "datetime")]
    public DateTime? BorrowedDate { get; set; }

    [Column("due_date", TypeName = "datetime")]
    public DateTime? DueDate { get; set; }

    [Column("returned_date", TypeName = "datetime")]
    public DateTime? ReturnedDate { get; set; }

    [ForeignKey("BookId")]
    [InverseProperty("BorrowedBooks")]
    public virtual Book Book { get; set; } = null!;

    [ForeignKey("BorrowerId")]
    [InverseProperty("BorrowedBooks")]
    public virtual Borrower Borrower { get; set; } = null!;
}
