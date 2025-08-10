using System;
using System.ComponentModel.DataAnnotations;

namespace Pomni.Models
{
    public class Note
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; private set; }

        public string Content { get; private set; }

        public DateTime CreatedDate { get; private set; }

        public DateTime? ReminderDateTime { get; private set; }

        protected Note() { }

        public Note(string title) : this()
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentNullException("Title не может быть пустым", nameof(title));

            Title = title;
            Content = string.Empty;
            CreatedDate = DateTime.Now;
        }

        public void UpdateTitle(string newTitle)
        {
            if (string.IsNullOrWhiteSpace(newTitle))
                throw new ArgumentNullException("Title не может быть пустым", nameof(newTitle));

            Title = newTitle;
        }

        public void UpdateContent(string newContent)
            => Content = newContent;

        public void SetReminder(DateTime? dateTime)
            => ReminderDateTime = dateTime;
    }
}
