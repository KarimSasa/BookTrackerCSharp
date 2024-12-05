using System;

namespace BookTracker.Models
{
    public class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public DateTime? PublicationDate { get; set; }
        public ReadingStatus Status { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? FinishDate { get; set; }

        public Book(string title, string author, string genre, DateTime? publicationDate)
        {
            Title = title;
            Author = author;
            Genre = genre;
            PublicationDate = publicationDate;
            Status = ReadingStatus.None;
        }

        public override string ToString()
        {
            return $"Title: {Title}\n" +
                   $"Author: {Author}\n" +
                   $"Genre: {Genre}\n" +
                   $"Publication Date: {(PublicationDate?.ToShortDateString() ?? "Not specified")}\n" +
                   $"Status: {Status}\n" +
                   $"Start Date: {(StartDate?.ToShortDateString() ?? "Not started")}\n" +
                   $"Finish Date: {(FinishDate?.ToShortDateString() ?? "Not finished")}\n";
        }
    }
} 