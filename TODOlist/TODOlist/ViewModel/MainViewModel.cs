using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using TODOlist.Model;

namespace TODOlist.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<TodoTask> Tasks { get; set; } = new ObservableCollection<TodoTask>();

        private TodoTask _selectedTask;
        public TodoTask SelectedTask
        {
            get => _selectedTask;
            set { _selectedTask = value; OnPropertyChanged(); }
        }

        private string _newTaskText;
        public string NewTaskText
        {
            get => _newTaskText;
            set { _newTaskText = value; OnPropertyChanged(); }
        }

        private DateTime _newTaskDeadline = DateTime.Now;
        public DateTime NewTaskDeadline
        {
            get => _newTaskDeadline;
            set { _newTaskDeadline = value; OnPropertyChanged(); }
        }

        public ICommand AddCommand { get; }
        public ICommand RemoveCommand { get; }

        public MainViewModel()
        {
            AddCommand = new RelayCommand(
                _ => {
                    Tasks.Add(new TodoTask { Text = NewTaskText, Deadline = NewTaskDeadline });
                    NewTaskText = string.Empty;
                },
                _ => !string.IsNullOrWhiteSpace(NewTaskText)
            );
            RemoveCommand = new RelayCommand(
                _ => Tasks.Remove(SelectedTask),
                _ => SelectedTask != null
            );
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}   