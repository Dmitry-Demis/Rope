using Rope.Cmds;
using Rope.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;

namespace Rope.ViewModel
{
    public class TotalScoreViewModel : BaseViewModel, ICloseWindows
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
        public ObservableCollection<Points> Points { get; set; } = new ObservableCollection<Points>();
        /*public ObservableCollection<double> StagePoints { get; set; } = new ObservableCollection<double>();
        public ObservableCollection<double> AdditionalTasksPoints { get; set; } = new ObservableCollection<double>();
        public ObservableCollection<double> PenaltyPoints{ get; set; } = new ObservableCollection<double>();*/
        ///// <summary>
        ///// A constructor with parameters
        ///// </summary>
        ///// <param name="dialogService">using for showing dialogues</param>
        ///// <param name="parameter">using for chosen parameter</param>
        public TotalScoreViewModel(IDialogService dialogService, Parameter parameter)
        {
            this.dialogService = dialogService;
            this.parameter = parameter;
            if (parameter.Points.Count==0)
            {
                Random random = new Random();
                for (int i = 0; i < 7; i++)
                {
                    Points points = new Points();
                    points.StageNumber = i + 1;
                    Points.Add(points);
                }
            }            
            foreach (var value in parameter.Points)
            {
                Points.Add(value);
            }
            CurrentItem = Points?.Count > 0 ? Points[0] : null;
        }
        public double TotalScore { get; set; }
        /// <summary>
        /// An observable collection of a value list
        /// </summary>
        //public ObservableCollection<double> ValueList { get; set; } = new ObservableCollection<double>();
        /// <summary>
        /// Current item
        /// </summary>
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
                    TotalScore += -Points[i].PenaltyPoint + Points[i].StagePoint + Points[i].AdditionalTasksPoint;
                }
                parameter.Points.Clear();
                foreach (var item in Points)
                {
                    parameter.Points.Add(item);
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
               
                Close();
            }));
    }
}
