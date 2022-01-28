using Rope.BindingEnums;
using Rope.Cmds;
using Rope.Interfaces;
using Rope.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Rope.ViewModel
{
    public class MainWindowViewModel : BaseViewModel, ICloseWindows
    {
        /// <summary>
        /// Services
        /// </summary>
        private readonly IDialogService dialogService;
        private readonly IFileService fileService;
        private Dictionary<FacultyType, int> Dict = new Dictionary<FacultyType, int>();

        public event Action Closed;
        public void Close()
        {
            if (dialogService.ShowMessageBoxDialog("Сохранение изменений", $"Хотите ли вы сохранить изменения перед выходом?") == MessageBoxResult.Yes)
            {
                if (Save())
                {
                    Closed?.Invoke();
                }
            }
            else
            {
                Closed?.Invoke();
            }

        }

        /// <summary>
        /// A save method for the SaveFileDialogCommand
        /// </summary>
        public bool Save()

        {
            try
            {
                if (dialogService.SaveFileDialog("demo test format file (*.dtff)|*.dtff") == true)
                {
                    fileService.Save(dialogService.FilePath, Parameters.ToList());
                    dialogService.ShowMessageBoxDialog("Файл сохранен");
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                dialogService.ShowMessageBoxDialog(ex.Message);
                return false;
            }
        }
        public MainWindowViewModel(IDialogService dialogService, IFileService fileService)
        {
            this.dialogService = dialogService;
            this.fileService = fileService;
        }
        /// <summary>
        /// Parameters - a collection of parametres in a data table
        /// </summary>
        public ObservableCollection<Parameter> Parameters { get; set; } = new ObservableCollection<Parameter>();

        /// <summary>
        /// A command, which allows to close a window
        /// </summary>
        private RelayCommand _closeWindowCommand;
        public RelayCommand CloseWindowCommand
        {
            get
            {
                return _closeWindowCommand ??
                    (
                    _closeWindowCommand = new RelayCommand(() =>
                    {
                        Close();
                    }));
            }
        }

        /// <summary>
        /// CurrentParameter - a selected parameter in a datatable shows here
        /// </summary>
        private Parameter _currentParameter;
        public Parameter CurrentParameter
        {
            get => _currentParameter;
            set => SetProperty(ref _currentParameter, value);
        }

        /// <summary>
        /// A command of adding item to the list of parameters
        /// </summary>
        private RelayCommand _addItem;
        public RelayCommand AddItem
        {
            get
            {
                return _addItem ??= new RelayCommand(() =>
                {
                    InputDataViewModel viewModel = new InputDataViewModel();
                    dialogService.ShowDialog(viewModel);
                    if (viewModel.Name != null)
                    {
                        Parameter parameter = new Parameter();
                        parameter.Name = viewModel.Name;
                        parameter.GroupNumber = viewModel.GroupNumber;
                        parameter.FacultyType = viewModel.FacultyType;
                        parameter.PointsList = new ObservableCollection<Points>();
                        parameter.TimeList = new ObservableCollection<Time>();
                        CurrentParameter = parameter;
                        Parameters.Add(CurrentParameter);
                        /*if (!Dict.ContainsKey(parameter.FacultyType))
                          {
                              Dict.Add(parameter.FacultyType, parameter.TotalTimeInSeconds);
                          }
                          else
                          {
                              Dict[parameter.FacultyType] = Math.Min(Dict[parameter.FacultyType], parameter.TotalTimeInSeconds);
                          }*/
                    }
                }, ()=> CanUseOtherCommands);
            }
        }

       /* private RelayCommand<Parameter> _changeInputDataWindowCommand;
        public RelayCommand<Parameter> ChangeInputDataWindowCommand
        {
            get
            {
                return _changeInputDataWindowCommand ??
                       (_changeInputDataWindowCommand = new RelayCommand<Parameter>((param) =>
                       {
                           InputDataViewModel viewModel = new InputDataViewModel(dialogService, param);
                           dialogService.ShowDialog(viewModel);
                           if (viewModel.Parameter != null)
                           {
                               param.FacultyType = viewModel.FacultyType;
                               param.Name = viewModel.Name;
                               param.GroupNumber = viewModel.GroupNumber;
                           }
                       },
                           (param) =>
                           {
                               return CurrentParameter != null;
                           }
                    )) ;
            }
        }*/
        /// <summary>
        /// A command, which can change a selected parameter
        /// </summary>
        private RelayCommand<Parameter> _changeParameterCommand;
        public RelayCommand<Parameter> ChangeParameterCommand
        {
            get
            {
                return _changeParameterCommand ??
                       (_changeParameterCommand = new RelayCommand<Parameter>((param) =>
                       {
                           TotalScoreViewModel viewModel = new TotalScoreViewModel(dialogService, param);
                           dialogService.ShowDialog(viewModel);
                           if (viewModel.TotalScore != default)
                           {
                               param.TotalScore = viewModel.TotalScore;
                           }
                           
                       }
                    ));
            }
        }
        /// <summary>
        /// A command, which can change a selected parameter
        /// </summary>
        private RelayCommand<Parameter> _changeTimeCommand;
        public RelayCommand<Parameter> ChangeTimeCommand
        {
            get
            {
                return _changeTimeCommand ??
                       (_changeTimeCommand = new RelayCommand<Parameter>((param) =>
                       {
                           TotalTimeViewModel viewModel = new TotalTimeViewModel(dialogService, param);
                           dialogService.ShowDialog(viewModel);
                           if (viewModel.TotalTime!=default)
                           {
                               param.TotalTimeInSeconds = (int)viewModel.TotalTime.TotalSeconds;
                           }
                           if (!Dict.ContainsKey(param.FacultyType))
                           {
                               Dict.Add(param.FacultyType, param.TotalTimeInSeconds);
                           }
                           else
                           {
                               Dict[param.FacultyType] = Math.Min(Dict[param.FacultyType], param.TotalTimeInSeconds);
                           }

                       }
                    ));
            }
        }
        /// <summary>
        /// A command, which can change a selected parameter
        /// </summary>
        private RelayCommand<Parameter> _resultCommand;
        public RelayCommand<Parameter> ResultCommand
        {
            get
            {
                return _resultCommand ??
                       (_resultCommand = new RelayCommand<Parameter>((param) =>
                       {
                           
                           ResultViewModel viewModel = new ResultViewModel(dialogService, param, Dict);
                           dialogService.ShowDialog(viewModel);
                           param.Result = string.Format("{0:0.00}", viewModel.Result);

                       }
                    ));
            }
        }
        /// <summary>
        /// A command, which raises the element 
        /// </summary>
        private RelayCommand _upCommand;
        public RelayCommand UpCommand
        {
            get
            {
                return _upCommand ??
                       (_upCommand = new RelayCommand(() =>
                       {
                           var currParameter = CurrentParameter;
                           var indexOfcurrItem = Parameters.IndexOf(currParameter);
                           var nextElement = Parameters.ElementAt(indexOfcurrItem - 1);
                           Parameters[indexOfcurrItem] = nextElement;
                           Parameters[indexOfcurrItem - 1] = currParameter;
                           CurrentParameter = currParameter;
                       },
                           () =>
                           {
                               return CurrentParameter != null && CurrentParameter != Parameters?[0];
                           }));
            }
        }
        /// <summary>
        /// A command, which lowers the element down
        /// </summary>
        private RelayCommand _downCommand;
        public RelayCommand DownCommand
        {
            get
            {
                return _downCommand ??
                       (_downCommand = new RelayCommand(() =>
                       {
                           var currParameter = CurrentParameter;
                           var indexOfcurrItem = Parameters.IndexOf(currParameter);
                           var nextElement = Parameters.ElementAt(indexOfcurrItem + 1);
                           Parameters[indexOfcurrItem] = nextElement;
                           Parameters[indexOfcurrItem + 1] = currParameter;
                           CurrentParameter = currParameter;
                       },
                           () =>
                           {
                               return CurrentParameter != null && CurrentParameter != Parameters[Parameters.Count - 1];
                           }));
            }
        }

        /// <summary>
        /// A command of deleting item from the list of parameters
        /// </summary>
        public RelayCommand _deleteItem;
        public RelayCommand DeleteItem
        {
            get
            {
                return _deleteItem ??= new RelayCommand(() =>
                    {
                        if (dialogService.ShowMessageBoxDialog("Удаление элемента из таблицы", $"Вы правда хотите удалить элемент {CurrentParameter.Name}?") == MessageBoxResult.Yes)
                            Parameters.Remove(CurrentParameter);
                        //OnPropertyChanged(nameof(IsTableEmpty));
                    },
                    () => CurrentParameter != null && Parameters.Count > 0);
            }
        }
        /// <summary>
        /// A command, which allows to save a datatable
        /// </summary>
        private RelayCommand _saveFileDialogCommand;
        public RelayCommand SaveFileDialogCommand
        {
            get
            {
                return _saveFileDialogCommand ??
                       (_saveFileDialogCommand = new RelayCommand(() =>
                       {
                           Save();
                       }));
            }
        }
        /// <summary>
        /// A command, which allows to open a file dialogue
        /// </summary>
        private RelayCommand<Parameter> _openFileDialogCommand;
        public RelayCommand<Parameter> OpenFileDialogCommand
        {
            get
            {
                return _openFileDialogCommand ??
                  (_openFileDialogCommand = new RelayCommand<Parameter>((param) =>
                  {
                      Open();
                     // OnPropertyChanged(nameof(IsTableEmpty));
                      //dialogService.ShowMessageBoxDialog("Файл открыт");
                  }));
            }
        }


        /// <summary>
        /// An open method for the OpenFileDialogCommand
        /// </summary>
        private void Open()
        {
            try
            {
                if (dialogService.OpenFileDialog("demo test format file (*.dtff)|*.dtff") == true)
                {
                    var parameters = fileService.Open(dialogService.FilePath);
                    Parameters.Clear();
                    foreach (var p in parameters)
                        Parameters.Add(p);
                }
            }
            catch (Exception ex)
            {
                dialogService.ShowMessageBoxDialog(ex.Message);
            }
        }

        #region ShowSettingsCommand : Настройки – nonparameterized

        /// <summary>Команда: Настройки</summary>

        private RelayCommand? _ShowSettingsCommand;

        /// <summary>Команда: Настройки</summary>

        public RelayCommand ShowSettingsCommand
        {
            get
            {
                return _ShowSettingsCommand ??= new RelayCommand(
                    () =>
                    {
                        SettingsViewModel viewModel = new SettingsViewModel(dialogService);
                        if (dialogService.ShowDialog(viewModel) == true)
                        {
                            CanUseOtherCommands = true;
                        }
                    });
            }
        }

        #endregion

        #region CanUseOtherCommands : bool - Доступ к другим командам

        /// <summary>Доступ к другим командам</summary>

        private bool _CanUseOtherCommands;

        /// <summary>Доступ к другим командам</summary>

        public bool CanUseOtherCommands
        {
            get => _CanUseOtherCommands;
            set => SetProperty(ref _CanUseOtherCommands, value);
        }

        #endregion
    }
}
