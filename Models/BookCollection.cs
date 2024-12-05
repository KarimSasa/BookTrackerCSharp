using System;
using System.Collections.Generic;
using System.Linq;

namespace BookTracker.Models
{
    public class BookCollection
    {
        private List<Book> books;

        public BookCollection()
        {
            books = new List<Book>();
        }

        public void AddBook(Book book)
        {
            books.Add(book);
        }

        public Book FindBookByTitle(string title)
        {
            return books.FirstOrDefault(b => b.Title.ToLower().Contains(title.ToLower()));
        }

        public List<Book> GetAllBooks()
        {
            return books.ToList();
        }

        public List<Book> SortBooks(string criteria)
        {
            return criteria.ToLower() switch
            {
                "title" => books.OrderBy(b => b.Title).ToList(),
                "author" => books.OrderBy(b => b.Author).ToList(),
                "genre" => books.OrderBy(b => b.Genre).ToList(),
                "status" => books.OrderBy(b => GetStatusOrder(b.Status)).ToList(),
                _ => books.ToList(),
            };
        }

        private int GetStatusOrder(ReadingStatus status)
        {
            return status switch
            {
                ReadingStatus.Completed => 0,
                ReadingStatus.In_Progress => 1,
                ReadingStatus.None => 2,
                ReadingStatus.Want_To_Read => 3,
                _ => 4
            };
        }

        public Dictionary<string, int> GetGenreStatistics()
        {
            return books.GroupBy(b => b.Genre)
                       .ToDictionary(g => g.Key, g => g.Count());
        }

        public int GetTotalBooks() => books.Count;
        public int GetCompletedBooks() => books.Count(b => b.Status == ReadingStatus.Completed);
    }
} 