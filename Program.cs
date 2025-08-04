using Library_Management_System_Task.Data;
using Library_Management_System_Task.Entities;
using Library_Management_System_Task.Services;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using static System.Reflection.Metadata.BlobBuilder;
using static System.Runtime.InteropServices.JavaScript.JSType;

public class Program
{
    public static async Task Main()
    {
        var context = new AppDbContext();

        var authorService = new AuthorService(context);
        var bookService = new BookService(context);
        var borrowerService = new BorrowerService(context);
        var borrowService = new BorrowedBookService(context);
        var reviewService = new ReviewService(context);

        await RunLibraryMenuAsync(authorService, bookService, borrowerService, borrowService, reviewService);
    }

    private static async Task RunLibraryMenuAsync(AuthorService authorService,BookService bookService,
        BorrowerService borrowerService,BorrowedBookService borrowService,ReviewService reviewService)
    {
        while (true)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\t\t\t\t\t╔═════════════════════════════════════════════╗");
            Console.WriteLine("\t\t\t\t\t║           Library Management System         ║");
            Console.WriteLine("\t\t\t\t\t╚═════════════════════════════════════════════╝");
            Console.ResetColor();
            Console.WriteLine("\t\t\t\t\t=================BOOKS MANAGEMENT================");
            Console.WriteLine("\t\t\t\t\t  1. Show All Books");
            Console.WriteLine("\t\t\t\t\t  2. Add Book");
            Console.WriteLine("\t\t\t\t\t  3. Update Book");
            Console.WriteLine("\t\t\t\t\t  4. Delete Book");
            Console.WriteLine("\t\t\t\t\t  5. Borrow Book");
            Console.WriteLine("\t\t\t\t\t  6. Return Book");
            Console.WriteLine("\t\t\t\t\t  7. Show All Borrowed Books");
            Console.WriteLine("\t\t\t\t\t=================AUTHORS MANAGEMENT=============");
            Console.WriteLine("\t\t\t\t\t  8. Show All Authors");
            Console.WriteLine("\t\t\t\t\t  9. Add Author");
            Console.WriteLine("\t\t\t\t\t 10. Update Author");
            Console.WriteLine("\t\t\t\t\t 11. Delete Author");
            Console.WriteLine("\t\t\t\t\t=================BORROWERS MANAGEMENT===========");
            Console.WriteLine("\t\t\t\t\t 12. Add Borrower");
            Console.WriteLine("\t\t\t\t\t 13. Show All Borrowers");
            Console.WriteLine("\t\t\t\t\t=================REVIEWS MANAGEMENT=============");
            Console.WriteLine("\t\t\t\t\t 14. Add Review");
            Console.WriteLine("\t\t\t\t\t 15. Show All Reviews");
            Console.WriteLine("\t\t\t\t\t=====================Exit=======================");
            Console.WriteLine("\t\t\t\t\t  0. Exit");
            Console.Write("\tChoose Option: ");


            string? choice = Console.ReadLine();

            switch (choice)
            {
                // BOOKS MANAGEMENT
                case "1":
                    var books = await bookService.GetAllBooksAsync();
                    Console.WriteLine("All Books:");
                    foreach (var book in books)
                        Console.WriteLine($"- {book.Id}: {book.Title} by {book.Author?.Name ?? "Unknown"}");
                    break;

                case "2":
                    Console.Write("Enter book title: ");
                    var title = Console.ReadLine();
                    Console.Write("Enter author ID: ");
                    if (int.TryParse(Console.ReadLine(), out int authorId))
                    {
                        await bookService.AddBookAsync(new Book { Title = title, AuthorId = authorId });
                        Console.WriteLine("Book added.");
                    }
                    else Console.WriteLine("Invalid author ID.");
                    break;

                case "3":
                    Console.Write("Enter book ID to update: ");
                    if (int.TryParse(Console.ReadLine(), out int updateBookId))
                    {
                        var existingBook = await bookService.GetBookByIdAsync(updateBookId);
                        if (existingBook != null)
                        {
                            Console.Write("Enter new title: ");
                            var newTitle = Console.ReadLine();
                            if (!string.IsNullOrWhiteSpace(newTitle))
                            {
                                existingBook.Title = newTitle;
                                await bookService.UpdateBookAsync(existingBook);
                                Console.WriteLine("Book updated.");
                            }
                            else Console.WriteLine("Title cannot be empty.");
                        }
                        else Console.WriteLine("Book not found.");
                    }
                    else Console.WriteLine("Invalid ID.");
                    break;

                case "4":
                    Console.Write("Enter book ID to delete: ");
                    if (int.TryParse(Console.ReadLine(), out int deleteBookId))
                    {
                        await bookService.DeleteBookAsync(deleteBookId);
                        Console.WriteLine("Book deleted.");
                    }
                    else Console.WriteLine("Invalid ID.");
                    break;

                case "5":
                    Console.Write("Enter book ID: ");
                    if (!int.TryParse(Console.ReadLine(), out int bookId))
                    {
                        Console.WriteLine("Invalid book ID.");
                        break;
                    }
                    Console.Write("Enter borrower ID: ");
                    if (!int.TryParse(Console.ReadLine(), out int borrowerId))
                    {
                        Console.WriteLine("Invalid borrower ID.");
                        break;
                    }
                    await borrowService.BorrowBookAsync(bookId, borrowerId);
                    Console.WriteLine("Book borrowed.");
                    break;

                case "6":
                    Console.Write("Enter borrowed book ID: ");
                    if (int.TryParse(Console.ReadLine(), out int borrowId))
                    {
                        await borrowService.ReturnBookAsync(borrowId);
                        Console.WriteLine("Book returned.");
                    }
                    else Console.WriteLine("Invalid ID.");
                    break;

                case "7":
                    var borrowedBooks = await borrowService.GetAllBorrowedBooksAsync();
                    Console.WriteLine("All Borrowed Books:");
                    foreach (var bb in borrowedBooks)
                        Console.WriteLine($"- ID: {bb.Id}, Book: {bb.Book?.Title}, Borrower: {bb.Borrower?.Name}, Borrowed: {bb.BorrowedDate}, Returned: {bb.ReturnedDate?.ToString() ?? "Not yet"}");
                    break;

                // AUTHORS MANAGEMENT
                case "8":
                    var authors = await authorService.GetAllAuthorsAsync();
                    Console.WriteLine("All Authors:");
                    foreach (var author in authors)
                        Console.WriteLine($"- {author.Id}: {author.Name}");
                    break;

                case "9":
                    Console.Write("Enter author name: ");
                    var name = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(name))
                    {
                        await authorService.AddAuthorAsync(new Author { Name = name });
                        Console.WriteLine("Author added.");
                    }
                    else Console.WriteLine("Name cannot be empty.");
                    break;

                case "10":
                    Console.Write("Enter author ID to update: ");
                    if (int.TryParse(Console.ReadLine(), out int updateAuthorId))
                    {
                        var existingAuthor = await authorService.GetAuthorByIdAsync(updateAuthorId);
                        if (existingAuthor != null)
                        {
                            Console.Write("Enter new name: ");
                            var newName = Console.ReadLine();
                            if (!string.IsNullOrWhiteSpace(newName))
                            {
                                existingAuthor.Name = newName;
                                await authorService.UpdateAuthorAsync(existingAuthor);
                                Console.WriteLine("Author updated.");
                            }
                            else Console.WriteLine("Name cannot be empty.");
                        }
                        else Console.WriteLine("Author not found.");
                    }
                    else Console.WriteLine("Invalid ID.");
                    break;

                case "11":
                    Console.Write("Enter author ID to delete: ");
                    if (int.TryParse(Console.ReadLine(), out int deleteAuthorId))
                    {
                        await authorService.DeleteAuthorAsync(deleteAuthorId);
                        Console.WriteLine("Author deleted.");
                    }
                    else Console.WriteLine("Invalid ID.");
                    break;

                // BORROWERS MANAGEMENT
                case "12":
                    Console.Write("Enter borrower name: ");
                    var borrowerName = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(borrowerName))
                    {
                        await borrowerService.AddBorrowerAsync(new Borrower { Name = borrowerName });
                        Console.WriteLine("Borrower added.");
                    }
                    else Console.WriteLine("Name cannot be empty.");
                    break;

                case "13":
                    var borrowers = await borrowerService.GetAllBorrowersAsync();
                    Console.WriteLine("All Borrowers:");
                    foreach (var b in borrowers)
                        Console.WriteLine($"- {b.Id}: {b.Name}");
                    break;

                // REVIEWS MANAGEMENT
                case "14":
                    Console.Write("Enter book ID for review: ");
                    if (int.TryParse(Console.ReadLine(), out int reviewBookId))
                    {
                        Console.Write("Enter borrower ID: ");
                        if (int.TryParse(Console.ReadLine(), out int reviewBorrowerId))
                        {
                            Console.Write("Enter rating (1-5): ");
                            if (int.TryParse(Console.ReadLine(), out int rating))
                            {
                                Console.Write("Enter comment: ");
                                var comment = Console.ReadLine();

                                await reviewService.AddReviewAsync(new Review
                                {
                                    BookId = reviewBookId,
                                    BorrowerId = reviewBorrowerId,
                                    Rating = rating,
                                    Comment = comment
                                });

                                Console.WriteLine("Review added.");
                            }
                            else Console.WriteLine("Invalid rating.");
                        }
                        else Console.WriteLine("Invalid borrower ID.");
                    }
                    else Console.WriteLine("Invalid book ID.");
                    break;

                case "15":
                    var reviews = await reviewService.GetAllReviewsAsync();
                    Console.WriteLine("All Reviews:");
                    foreach (var r in reviews)
                        Console.WriteLine($"- ID: {r.Id}, Book: {r.Book?.Title}, Borrower: {r.Borrower?.Name}, Rating: {r.Rating}, Comment: {r.Comment}");
                    break;

                case "0":
                    Console.WriteLine("Exiting... Goodbye.");
                    return;

                default:
                    Console.WriteLine("Invalid option. Try again.");
                    break;
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nPress any key to continue...");
            Console.ResetColor();
            Console.ReadKey();

        }
    }
}
