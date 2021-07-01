using Rope.Cmds;
using Rope.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rope.ViewModel
{
    class ResultViewModel : BaseViewModel, ICloseWindows
    {
        public event Action Closed;

        public void Close()
        {
            Closed?.Invoke();
        }

        private Parameter parameter;
        /// <summary>
        /// DialogService for showing dialogues
        /// </summary>
        private readonly IDialogService dialogService;
        public ResultViewModel(IDialogService dialogService, Parameter parameter)
        {
            this.dialogService = dialogService;
            this.parameter = parameter;
            MinFacultyTime = int.Parse(parameter.Result);
        }
        /// <summary>
        /// Number of a group
        /// </summary>
        private int _maxSP = 70;
        public int MaxSP
        {
            get => _maxSP;
            set
            {
                SetProperty(ref _maxSP, value);
                OnPropertyChanged(nameof(Koeff));
            }
        }
        /// <summary>
        /// Number of a group
        /// </summary>
        private int _maxATP = 25;
        public int MaxATP
        {
            get => _maxATP;
            set
            {
                SetProperty(ref _maxATP, value);
                OnPropertyChanged(nameof(Koeff));
            }
        }
        private int _minFacultyTime;
        public int MinFacultyTime
        {
            get => _minFacultyTime;
            set
            {
                SetProperty(ref _minFacultyTime, value);
                OnPropertyChanged(nameof(Koeff));
            }
        }
        private double CalculateKoeff()
        {
            return ((MaxATP + MaxSP) * (double)MinFacultyTime / parameter.TotalTimeInSeconds);
        }
        public string Koeff 
            => "(" + MaxATP + "+" + MaxSP + ")*" 
            + MinFacultyTime + "/" + parameter.TotalTimeInSeconds +
            "=" + string.Format("{0:0.00}", CalculateKoeff());

            private RelayCommand _okCommand;
        public RelayCommand OKCommand
            => _okCommand ??
            (_okCommand = new RelayCommand(() =>
            {
                Result = parameter.TotalScore + CalculateKoeff();
               
                Close();
            }
            ));
        public double Result { get; set; }
        /// <summary>
        /// Allows to cancel input without saving
        /// </summary>
        private RelayCommand _cancelCommand;
        public RelayCommand CancelCommand
            => _cancelCommand ??
            (_cancelCommand = new RelayCommand(() => {
                Result = default;
                Close();
            }));

    }
}
