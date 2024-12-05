using System;
using BookTracker.Models;

namespace BookTracker
{
    class Program
    {
        private static BookCollection bookCollection = new BookCollection();

        static void Main(string[] args)
        {
            bool running = true;
            while (running)
            {
                DisplayMenu();
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddBook();
                        break;
                    case "2":
                        UpdateReadingStatus();
                        break;
                    case "3":
                        ViewCollection();
                        break;
                    case "4":
                        SortCollection();
                        break;
                    case "5":
                        SearchBook();
                        break;
                    case "6":
                        ViewStatistics();
                        break;
                    case "7":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        private static void DisplayMenu()
        {
            Console.WriteLine("\n=== Book Tracker Menu ===");
            Console.WriteLine("1. Add Book");
            Console.WriteLine("2. Update Reading Status");
            Console.WriteLine("3. View Collection");
            Console.WriteLine("4. Sort Collection");
            Console.WriteLine("5. Search for a Book");
            Console.WriteLine("6. View Statistics");
            Console.WriteLine("7. Exit");
            Console.Write("Enter your choice: ");
        }

        private static void AddBook()
        {
            Console.WriteLine("\n=== Add New Book ===");
            
            Console.Write("Enter title: ");
            string title = Console.ReadLine();
            
            Console.Write("Enter author: ");
            string author = Console.ReadLine();
            
            Console.Write("Enter genre: ");
            string genre = Console.ReadLine();
            
            DateTime? publicationDate = null;
            Console.Write("Enter publication date (MM/DD/YYYY) or press Enter to skip: ");
            string dateInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(dateInput))
            {
                if (DateTime.TryParse(dateInput, out DateTime date))
                {
                    publicationDate = date;
                }
                else
                {
                    Console.WriteLine("Invalid date format. Publication date will not be set.");
                }
            }

            Book newBook = new Book(title, author, genre, publicationDate);
            bookCollection.AddBook(newBook);
            Console.WriteLine("Book added successfully!");
        }

        private static void UpdateReadingStatus()
        {
            Console.WriteLine("\n=== Update Reading Status ===");
            Console.Write("Enter the title of the book: ");
            string title = Console.ReadLine();

            Book book = bookCollection.FindBookByTitle(title);
            if (book == null)
            {
                Console.WriteLine("Book not found.");
                return;
            }

            Console.WriteLine("\nCurrent status: " + book.Status);
            Console.WriteLine("Select new status:");
            Console.WriteLine("1. Want to Read");
            Console.WriteLine("2. In Progress");
            Console.WriteLine("3. Completed");
            Console.Write("Enter choice (1-3): ");

            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                switch (choice)
                {
                    case 1:
                        book.Status = ReadingStatus.Want_To_Read;
                        book.StartDate = null;
                        book.FinishDate = null;
                        break;
                    case 2:
                        book.Status = ReadingStatus.In_Progress;
                        Console.Write("Enter start date (MM/DD/YYYY) or press Enter to skip: ");
                        if (DateTime.TryParse(Console.ReadLine(), out DateTime startDate))
                        {
                            book.StartDate = startDate;
                        }
                        book.FinishDate = null;
                        break;
                    case 3:
                        book.Status = ReadingStatus.Completed;
                        Console.Write("Enter start date (MM/DD/YYYY) or press Enter to skip: ");
                        if (DateTime.TryParse(Console.ReadLine(), out DateTime completedStartDate))
                        {
                            book.StartDate = completedStartDate;
                        }
                        Console.Write("Enter finish date (MM/DD/YYYY) or press Enter to skip: ");
                        if (DateTime.TryParse(Console.ReadLine(), out DateTime finishDate))
                        {
                            if (book.StartDate.HasValue && finishDate < book.StartDate.Value)
                            {
                                Console.WriteLine("Finish date cannot be before start date. Dates not updated.");
                                return;
                            }
                            book.FinishDate = finishDate;
                        }
                        break;
                    default:
                        Console.WriteLine("Invalid choice.");
                        return;
                }
                Console.WriteLine("Reading status updated successfully!");
            }
        }

        private static void ViewCollection()
        {
            Console.WriteLine("\n=== Your Book Collection ===");
            var books = bookCollection.GetAllBooks();
            
            if (books.Count == 0)
            {
                Console.WriteLine("Your collection is empty.");
                return;
            }

            foreach (var book in books)
            {
                Console.WriteLine("\n" + book.ToString());
            }
        }

        private static void SortCollection()
        {
            Console.WriteLine("\n=== Sort Collection ===");
            Console.WriteLine("Sort by:");
            Console.WriteLine("1. Title");
            Console.WriteLine("2. Author");
            Console.WriteLine("3. Genre");
            Console.WriteLine("4. Reading Status");
            Console.Write("Enter choice (1-4): ");

            string criteria = Console.ReadLine() switch
            {
                "1" => "title",
                "2" => "author",
                "3" => "genre",
                "4" => "status",
                _ => ""
            };

            if (string.IsNullOrEmpty(criteria))
            {
                Console.WriteLine("Invalid choice.");
                return;
            }

            var sortedBooks = bookCollection.SortBooks(criteria);
            Console.WriteLine($"\nBooks sorted by {criteria}:");
            foreach (var book in sortedBooks)
            {
                Console.WriteLine("\n" + book.ToString());
            }
        }

        private static void SearchBook()
        {
            Console.WriteLine("\n=== Search for a Book ===");
            Console.Write("Enter book title to search: ");
            string searchTitle = Console.ReadLine();

            Book book = bookCollection.FindBookByTitle(searchTitle);
            if (book != null)
            {
                Console.WriteLine("\nBook found:");
                Console.WriteLine(book.ToString());
            }
            else
            {
                Console.WriteLine("No books found matching that title.");
            }
        }

        private static void ViewStatistics()
        {
            Console.WriteLine("\n=== Reading Statistics ===");
            Console.WriteLine($"Total books in collection: {bookCollection.GetTotalBooks()}");
            Console.WriteLine($"Books completed: {bookCollection.GetCompletedBooks()}");

            var genreStats = bookCollection.GetGenreStatistics();
            if (genreStats.Any())
            {
                Console.WriteLine("\nBooks by genre:");
                foreach (var genre in genreStats)
                {
                    Console.WriteLine($"{genre.Key}: {genre.Value} books");
                }
            }
        }
    }
} 