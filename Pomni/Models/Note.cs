using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pomni.Models
{
    public class Note
    {
        public int Id { get; }
        public string Title { get; private set; }
        public string Content { get; private set; }
        public DateTime? ReminderDateTime { get; private set; }

        protected Note() { }

        public Note(int id, string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentNullException("Title не может быть пустым", nameof(title));

            Id = id;
            Title = title;
            Content = string.Empty;
        }

        public void UpdateTitle (string newTitle)
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
