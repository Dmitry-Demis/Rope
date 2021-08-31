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
        
        //Массив времени для текущей группы.
        public ObservableCollection<Time> TimeList { get; set; } = new ObservableCollection<Time>();

        public TotalTimeViewModel(IDialogService dialogService, Parameter current_parameter)
        {
            this.dialogService = dialogService;
            this.currentParameter = current_parameter;   

            //Если массив времени для группы пуст, заполняем
            if (currentParameter.TimeList.Count == 0)
            {
                FillInDefaultValues();
            }
            else
            {
                foreach (var value in current_parameter.TimeList)
                {
                    TimeList.Add(value);
                }
            }
        }
        private Parameter currentParameter;

        private void FillInDefaultValues()
        {
                            
                for (int i = 0; i < 7; i++)
                {
                    TimeList.Add(new Time());
                }
            
        }
        //Текущая выбранная строчка.
        private Time _currentItem;
        public Time CurrentItem
        {
            get => _currentItem;
            set 
            {
                SetProperty(ref _currentItem, value);
            }                
        }
        public TimeSpan TotalTime { get; set; } = default;
        /// <summary>
        /// Allows to close a window
        /// </summary>
        private RelayCommand _okCommand;
        public RelayCommand OKCommand
            => _okCommand ??
            (_okCommand = new RelayCommand(() =>
            {
                currentParameter.TimeList.Clear();
                currentParameter.TotalTime = default;
                foreach (var item in TimeList)
                {
                    currentParameter.TimeList.Add(item);                    
                    TotalTime += item.TotalStageTime; 
                    currentParameter.TotalTime = TotalTime;
                }
                Close();
            }
            ));     

        ///// <summary>
        ///// Allows to cancel input without saving
        ///// </summary>
        private RelayCommand _cancelCommand;
        public RelayCommand CancelCommand
            => _cancelCommand ??
            (_cancelCommand = new RelayCommand(() =>
            {
                FillInDefaultValues();
                Close();
            }));
    }
}
