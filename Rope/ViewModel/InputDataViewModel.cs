using Rope.BindingEnums;
using Rope.Cmds;
using Rope.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rope.ViewModel
{
    class InputDataViewModel : BaseViewModel, ICloseWindows
    {
        public event Action Closed;

        public void Close()
        {
            Closed?.Invoke();
        }
        public InputDataViewModel(){}
        /// <summary>
        /// DialogService for showing dialogues
        /// </summary>
        private readonly IDialogService dialogService;
        public InputDataViewModel(IDialogService dialogService)
        {
            this.dialogService = dialogService;
        }
        private Parameter parameter;
        public Parameter Parameter {
            get => parameter;
            set => SetProperty(ref parameter, value);
        }
        /// <summary>
        /// Number of a group
        /// </summary>
        private string _name = "Default";
        public string Name
        {
            get => _name;
            set
            {
                SetProperty(ref _name, value);
            }
        }
        /// <summary>
        /// Name
        /// </summary>
        private string _groupNumber = "1-";
        public string GroupNumber
        {
            get => _groupNumber;
            set
            {
                SetProperty(ref _groupNumber, value);
            }
        }
        /// <summary>
        /// A parameter type
        /// </summary>
        private FacultyType _facultyType;
        public FacultyType FacultyType
        {
            get => _facultyType;
            set
            {
                SetProperty(ref _facultyType, value);
            }
        }
        /// <summary>
        /// Allows to close a window
        /// </summary>
        private RelayCommand _okCommand;
        public RelayCommand OKCommand
            => _okCommand ??
            (_okCommand = new RelayCommand(() =>
            {
                Parameter = new Parameter();
                parameter.Name = Name;
                parameter.GroupNumber = GroupNumber;
                parameter.FacultyType = FacultyType;
                Close();
            }/*, () => { return Name != null && GroupNumber.Length>2; }*/ 
            ));

        /// <summary>
        /// Allows to cancel input without saving
        /// </summary>
        private RelayCommand _cancelCommand;
        public RelayCommand CancelCommand
            => _cancelCommand ??
            (_cancelCommand = new RelayCommand(() => {
                Name = GroupNumber = null;
                FacultyType = default;
                Close();                 
            }));
    }
}
