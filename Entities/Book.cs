using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Library_Management_System_Task.Data;

[Table("books")]
public partial class Book
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("author_id")]
    public int AuthorId { get; set; }

    [Column("title")]
    [StringLength(255)]
    public string? Title { get; set; }

    [Column("genre")]
    [StringLength(100)]
    public string? Genre { get; set; }

    [Column("published_year", TypeName = "datetime")]
    public DateTime? PublishedYear { get; set; }

    [ForeignKey("AuthorId")]
    [InverseProperty("Books")]
    public virtual Author Author { get; set; } = null!;

    [InverseProperty("Book")]
    public virtual ICollection<BorrowedBook> BorrowedBooks { get; set; } = new List<BorrowedBook>();

    [InverseProperty("Book")]
    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}
