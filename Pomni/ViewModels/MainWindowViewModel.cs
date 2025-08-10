using Pomni.Models;
using Pomni.Repositories;
using Pomni.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Pomni.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly INoteRepository _repo = new NoteRepository();
        private static event EventHandler NotesUpdated;
        private Note _selectedNote;

        public ObservableCollection<Note> Notes { get; } = new ObservableCollection<Note>();

        public Note SelectedNote
        {
            get { return _selectedNote; }
            set
            {
                _selectedNote = value;
                OnPropertyChanged(nameof(SelectedNote));
            }
        }

        public MainWindowViewModel()
        {
            NotesUpdated += OnNotesUpdated;
            LoadNotes();
        }

        private void OnNotesUpdated(object sender, EventArgs e)
        {
            LoadNotes();
        }

        public static void RaiseNotesUpdated()
        {
            NotesUpdated?.Invoke(null, EventArgs.Empty);
        }

        private void LoadNotes()
        {
            var notes = _repo.GetAll();
            Notes.Clear();
            foreach (var note in notes)
                Notes.Add(note);
        }

        public ICommand IOpenNewWidnow => new RelayCommand(() =>
        {
            new NewNoteWindow().Show();
        });

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
