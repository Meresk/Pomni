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
        public DateTime? ReminderDateTime { get; private set; }

        private DateTime? _reminderDate;
        private string _reminderTime;

        public DateTime? ReminderDate
        {
            get => _reminderDate;
            set
            {
                _reminderDate = value;
                OnPropertyChanged(nameof(ReminderDate));
                UpdateRimenderDateTime();
            }
        }
        public string ReminderTime
        {
            get => _reminderTime;
            set
            {
                if (TimeSpan.TryParse(value, out _))
                {
                    _reminderTime = value;
                    OnPropertyChanged(nameof(ReminderTime));
                    UpdateRimenderDateTime();
                }
            }
        }

        private void UpdateRimenderDateTime()
        {
            if (ReminderDate.HasValue && TimeSpan.TryParse(ReminderTime, out var time))
                ReminderDateTime = ReminderDate.Value.Date + time;
            else
                ReminderDateTime = null;

            OnPropertyChanged(nameof(ReminderDateTime));
        }

        public ICommand ICreateNote => new RelayCommand<Window>(CreateNote);

        private void CreateNote(Window window)
        {
            var note = new Note(Title);
            note.UpdateContent(Content);
            note.SetReminder(ReminderDateTime);

            _repo.Add(note);
            MainWindowViewModel.RaiseNotesUpdated();
            window?.Close();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
