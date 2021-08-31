using Rope.Cmds;
using Rope.Model;
using System;
using System.Collections.ObjectModel;

namespace Rope.ViewModel
{
    public class TotalScoreViewModel : BaseViewModel, ICloseWindows
    {
        public event Action Closed;

        public void Close()
        {
            Closed?.Invoke();
        }

        private readonly IDialogService dialogService;
        
        private Parameter parameter;
        public ObservableCollection<Points> PointsList { get; set; } = new ObservableCollection<Points>();
       
        private void FillInDefaultValues()
        {
            if (parameter.PointsList.Count == 0)
            {
                Random random = new Random();
                for (int i = 0; i < 7; i++)
                {
                    PointsList.Add(new Points());
                }
            }
        }
        public TotalScoreViewModel(IDialogService dialogService, Parameter parameter)
        {
            this.dialogService = dialogService;
            this.parameter = parameter;
            FillInDefaultValues();
            foreach (var value in parameter.PointsList)
            {
                PointsList.Add(value);
            }
            CurrentItem = PointsList?.Count > 0 ? PointsList[0] : null;
        }
        public double TotalScore { get; set; }

        private Points _currentItem;
        public Points CurrentItem
        {
            get => _currentItem;
            set => SetProperty(ref _currentItem, value);
        }
        /// <summary>
        /// Allows to close a window
        /// </summary>
        private RelayCommand _okCommand;
        public RelayCommand OKCommand
            => _okCommand ??
            (_okCommand = new RelayCommand(() =>
            {
                
                for (int i = 0; i < 7; i++)
                {
                    TotalScore += -PointsList[i].PenaltyPoint + PointsList[i].StagePoint + PointsList[i].AdditionalTasksPoint;
                }
                parameter.PointsList.Clear();
                foreach (var item in PointsList)
                {
                    parameter.PointsList.Add(item);
                }
                Close();
            }
            ));

        /// <summary>
        /// Allows to cancel input without saving
        /// </summary>
        private RelayCommand _cancelCommand;
        public RelayCommand CancelCommand
            => _cancelCommand ??
            (_cancelCommand = new RelayCommand(() => {
                FillInDefaultValues();
                Close();
            }));
    }
}
