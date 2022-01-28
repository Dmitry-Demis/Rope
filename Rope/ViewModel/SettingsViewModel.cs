using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.ComponentModel.DataAnnotations;
using System.Data;
using Rope.BindingEnums;
using Rope.Cmds;
using Rope.Model;

namespace Rope.ViewModel
{
    internal class SettingsViewModel : BaseViewModel, IDataErrorInfo
    {
        private readonly IDialogService _dialog;

        #region Title : string - Титул

        /// <summary>Титул</summary>

        private string _title = "Окно настроек";

        /// <summary>Титул</summary>

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        #endregion

        #region NumberOfStages : string - Число этапов

        /// <summary>Число этапов</summary>

        private string _numberOfStages;

        /// <summary>Число этапов</summary>
        public string NumberOfStages
        {
            get => _numberOfStages;
            set => SetProperty(ref _numberOfStages, value);
        }

        #endregion

        public string this[string columnName] =>
            columnName switch
            {
                "NumberOfStages" => ValidateInput(),
                _ => throw new ArgumentOutOfRangeException(nameof(columnName))
            };

        private string ValidateInput()
        {
            _turnOnApplyCommand = false;
            var isEmpty = string.IsNullOrEmpty(NumberOfStages);
            if (isEmpty)
            {
                return "Value cannot be null or empty";
            }
            var isDigit = int.TryParse(NumberOfStages, out var result);
            if (!isDigit) return "It's not a digit";
            switch (result)
            {
                case <= 0:
                   // _turnOnApplyCommand = false;
                    return "Value must not be equal to zero or less";
                case > 10:
                   // _turnOnApplyCommand = false;
                    return "Maximum count of stages is 10";
                default:
                    _turnOnApplyCommand = true;
                    return string.Empty;
            }
        }

        private bool _turnOnApplyCommand;
        public string Error { get; }

        #region ApplyStageCountCommand : Применить изменения – nonparameterized

        /// <summary>Команда: Применить изменения</summary>

        private RelayCommand? _ApplyStageCountCommand;

        /// <summary>Команда: Применить изменения</summary>

        public RelayCommand ApplyStageCountCommand
        {
            get
            {
                return _ApplyStageCountCommand ??= new RelayCommand(
                    () =>
                    {
                        if (Stages is not null)
                        {
                            Stages.Clear();
                            Stages = null;
                        }
                        Stages = new ObservableCollection<StageModel>();
                        for (var i = 0; i < int.Parse(NumberOfStages); i++)
                        {
                            StageModel element = new()
                            {
                                Name = $"Этап {i+1}", StageType = StageType.Time
                            };
                            Stages.Add(element);
                        }
                        OnPropertyChanged(nameof(Stages));
                        
                    }, ()=> _turnOnApplyCommand);
            }
        }
        #endregion

        public ObservableCollection<StageModel> Stages { get; set; }

        #region StageType : StageType - Тип этапа

        /// <summary>Тип этапа</summary>

        private StageType _stageType;

        /// <summary>Тип этапа</summary>

        public StageType StageType
        {
            get => _stageType;
            set => SetProperty(ref _stageType, value);
        }

        #endregion

        #region CurrentStage : StageModel - Текущий этап

        /// <summary>Текущий этап</summary>

        private StageModel _CurrentStage;

        /// <summary>Текущий этап</summary>

        public StageModel CurrentStage
        {
            get => _CurrentStage;
            set => SetProperty(ref _CurrentStage, value);
        }

        #endregion

        public SettingsViewModel(IDialogService dialog)
        {
            _dialog = dialog;
        }
    }
}
