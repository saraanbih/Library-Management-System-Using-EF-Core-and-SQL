using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Library_Management_System_Task.Data;

[Table("borrowers")]
[Index("Email", Name = "UQ__borrower__AB6E6164FE7B72FB", IsUnique = true)]
public partial class Borrower
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(255)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [Column("email")]
    [StringLength(255)]
    [Unicode(false)]
    public string? Email { get; set; }

    [InverseProperty("Borrower")]
    public virtual ICollection<BorrowedBook> BorrowedBooks { get; set; } = new List<BorrowedBook>();

    [InverseProperty("Borrower")]
    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}
