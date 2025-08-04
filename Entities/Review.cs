using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library_Management_System_Task.Data;

[Table("reviews")]
public partial class Review
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("borrower_id")]
    public int BorrowerId { get; set; }

    [Column("book_id")]
    public int BookId { get; set; }

    [Column("review_date", TypeName = "datetime")]
    public DateTime? ReviewDate { get; set; }

    [Column("rating")]
    public int? Rating { get; set; }

    [Column("comment")]
    public string? Comment { get; set; }

    [Column("created_at", TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [ForeignKey("BookId")]
    [InverseProperty("Reviews")]
    public virtual Book Book { get; set; } = null!;

    [ForeignKey("BorrowerId")]
    [InverseProperty("Reviews")]
    public virtual Borrower Borrower { get; set; } = null!;
}
