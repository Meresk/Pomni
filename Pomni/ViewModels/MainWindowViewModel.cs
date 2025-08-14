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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace Pomni.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly INoteRepository _repo = new NoteRepository();
        private static event EventHandler NotesUpdated;
        private Note _selectedNote;
        private string _searchNoteText;
        private readonly ObservableCollection<Note> _notes = new ObservableCollection<Note>();
        private ICollectionView _notesView;

        public MainWindowViewModel()
        {
            NotesUpdated += OnNotesUpdated;
            LoadNotes();

            NotesView.SortDescriptions.Add(new SortDescription("CreatedDate", ListSortDirection.Descending));
        }

        public ICollectionView NotesView
        {
            get 
            {
                if (_notesView == null)
                {
                    _notesView = CollectionViewSource.GetDefaultView(_notes);
                    _notesView.Filter = NoteFilter;
                }
                return _notesView; 
            }
        }

        public string SearchNoteText
        {
            get { return _searchNoteText; }
            set
            {
                _searchNoteText = value;
                OnPropertyChanged(nameof(SearchNoteText));
                NotesView.Refresh();
            }
        }

        private bool NoteFilter(object item)
        {
            if (string.IsNullOrWhiteSpace(SearchNoteText))
                return true;

            var note = item as Note;
            if (note == null) return false;

            string search = SearchNoteText.ToLower().Trim();
            return note.Title.ToLower().Contains(search) || 
                (note.Content != null && note.Content.ToLower().Contains(search));
        }

        public Note SelectedNote
        {
            get { return _selectedNote; }
            set
            {
                _selectedNote = value;
                OnPropertyChanged(nameof(SelectedNote));
            }
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
            var notes = _repo.GetAll().ToList();
            _notes.Clear();
            foreach (var note in notes)
                _notes.Add(note);
        }

        public ICommand OpenContextMenuCommand => new RelayCommand<Button>(button =>
        {
            if (button.ContextMenu != null)
            {
                button.ContextMenu.PlacementTarget = button;
                button.ContextMenu.Placement = System.Windows.Controls.Primitives.PlacementMode.Right;
                button.ContextMenu.IsOpen = true;
            }
        });

        public ICommand SetReminderNoteCommand => new RelayCommand(() =>
        {
            if (SelectedNote == null) return;

            MessageBox.Show("Reminder заглушка");
        });

        public ICommand DeleteNoteCommand => new RelayCommand(() =>
        {
            if (SelectedNote == null) return;

            var result = MessageBox.Show(
                $"Вы действительно хотите удалить заметку \"{SelectedNote.Title}\"?",
                "Подтверждение удаления",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                _repo.Delete(SelectedNote.Id);
                _notes.Remove(SelectedNote);
            }
        });

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
