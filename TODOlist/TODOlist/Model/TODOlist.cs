using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TODOlist.Model
{
    public class TodoTask : INotifyPropertyChanged
    {
        private string _text;
        private DateTime _deadline;

        public string Text
        {
            get => _text;
            set { _text = value; OnPropertyChanged(); }
        }

        public DateTime Deadline
        {
            get => _deadline;
            set
            {   _deadline = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsOverdue));
            }
        }
        public bool IsOverdue => Deadline.Date < DateTime.Now.Date;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}