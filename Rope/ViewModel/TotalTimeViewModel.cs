using Rope.Cmds;
using Rope.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Rope.ViewModel
{
    public class TotalTimeViewModel : BaseViewModel, ICloseWindows
    {
        public event Action Closed;

        public void Close()
        {
            Closed?.Invoke();
        }
        /// <summary>
        /// DialogService for showing dialogues
        /// </summary>
        private readonly IDialogService dialogService;
        /// <summary>
        /// Current parameter
        /// </summary>
        private Parameter parameter;
        public ObservableCollection<Time> Time { get; set; } = new ObservableCollection<Time>();
        public TotalTimeViewModel(IDialogService dialogService, Parameter parameter)
        {
            this.dialogService = dialogService;
            this.parameter = parameter;
            if (parameter.Time.Count == 0)
            {
                Random random = new Random();
                for (int i = 0; i < 7; i++)
                {
                    Time time = new Time();
                    time.StageNumber = i + 1;
                    time.Minutes = time.Seconds= string.Empty;
                    Time.Add(time);
                }
            }
            foreach (var value in parameter.Time)
            {
                Time.Add(value);
            }
            CurrentItem = Time?.Count > 0 ? Time[0] : null;
        }
        private Time _currentItem;
        public Time CurrentItem
        {
            get => _currentItem;
            set 
            {
                SetProperty(ref _currentItem, value);
            }
                
        }
        public TimeSpan TotalTime { get; set; }
        /// <summary>
        /// Allows to close a window
        /// </summary>
        private RelayCommand _okCommand;
        public RelayCommand OKCommand
            => _okCommand ??
            (_okCommand = new RelayCommand(() =>
            {
                TotalTime = Recalculate();
                parameter.Time.Clear();
                foreach (var item in Time)
                {
                    parameter.Time.Add(item);
                }
                Close();
            }
            ));
        private TimeSpan Recalculate()
        {
            TimeSpan time = new TimeSpan();
            for (int i = 0; i < 7; i++)
            {
                if (Time[i].Minutes == String.Empty)
                {
                    Time[i].Minutes = "00";
                }
                if (Time[i].Seconds == String.Empty)
                {
                    Time[i].Seconds = "00";
                }
                if (Time[i].Minutes.Length==1)
                {
                    Time[i].Minutes = "0" + Time[i].Minutes;
                }
                if (Time[i].Seconds.Length == 1)
                {
                    Time[i].Seconds = "0" + Time[i].Seconds;
                }
                var s = "00:"+Time[i].Minutes + ":" + Time[i].Seconds;
                var l = TimeSpan.Parse(s);
                time += l;
            }
            return time;
        }

        /// <summary>
        /// Allows to cancel input without saving
        /// </summary>
        private RelayCommand _cancelCommand;
        public RelayCommand CancelCommand
            => _cancelCommand ??
            (_cancelCommand = new RelayCommand(() => {

                Close();
            }));
    }
}
