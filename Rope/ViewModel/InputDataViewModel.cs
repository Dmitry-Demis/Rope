using Rope.BindingEnums;
using Rope.Cmds;
using Rope.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Rope.ViewModel
{
    class InputDataViewModel : BaseViewModel, ICloseWindows
    {
        public event Action Closed;
        //TODO: сделать в файле.
        //Словарь факулетов и связанных с ними групп.
        private readonly Dictionary<FacultyType, string[]> faculty_groups = new Dictionary<FacultyType, string[]>
        {
            [FacultyType.ITandComputerScienceFaculty] = new string[] { "41", "42В", "43", "44", "45", "47", "48" },
            [FacultyType.PhysicsAndEngineeringFaculty] = new string[] { "11", "12", "13", "15" },
            [FacultyType.HeatEngineeringFaculty] = new string[] { "1", "2", "3", "3А", "10"},
            [FacultyType.EconomicsAndManagementFaculty] = new string[] { "53"},
            [FacultyType.ElectromechanicsFaculty] = new string[] { "31", "33", "34", "35", "35В", "36", "38" },
            [FacultyType.ElectricalPowerEngineeringFaculty] = new string[] { "23", "24", "25", "25В", "26", "27", "28"}
        };
        
        
        public void Close()
        {
            Closed?.Invoke();
        }
        private readonly IDialogService dialogService;
                    
        public InputDataViewModel(IDialogService dialogService = null, Parameter param = null)
        {
            this.dialogService = dialogService;          
            this.NumberOfGroups = faculty_groups[0];
            this.GroupNumber = NumberOfGroups?[0];
        }

        private Parameter parameter;
        public Parameter Parameter {
            get => parameter;
            set => SetProperty(ref parameter, value);
        }

        //Имя группы.
        private string _name = "Default";
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        //Номер группы.
        private string _groupNumber;
        public string GroupNumber
        {
            get => _groupNumber;
            set => SetProperty(ref _groupNumber, value);
        }

        //Факультет.
        private FacultyType _facultyType;
        public FacultyType FacultyType
        {
            get => _facultyType;
            set
            {
                SetProperty(ref _facultyType, value);
                NumberOfGroups = faculty_groups[FacultyType];
                GroupNumber = NumberOfGroups[0];
            }
        }

        private string[] _numberOfGroups;
        public string[] NumberOfGroups
        {
            get { return _numberOfGroups; }
            set 
            {
               SetProperty(ref _numberOfGroups, value);
            }
        }

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
            }
            ));
       
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
