using Pomni.Models;
using Pomni.Repositories;
using Pomni.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Pomni.ViewModels
{
    public class NewNoteViewModel : INotifyPropertyChanged
    {
        private readonly INoteRepository _repo = new NoteRepository();

        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime? ReminderDate { get; set; }

        public ICommand ICreateNote => new RelayCommand<Window>(CreateNote);

        private void CreateNote(Window window)
        {
            var note = new Note(Title);
            note.UpdateContent(Content);
            note.SetReminder(ReminderDate);

            _repo.Add(note);

            window?.Close();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
