using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Rope.BindingEnums;
using Rope.ViewModel;

namespace Rope.Model
{
    
    public class Parameter: BaseViewModel
    {
        /// <summary>
        /// Number of a group
        /// </summary>
        public string GroupNumber { get; set; }
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// A parameter type
        /// </summary>
        public FacultyType FacultyType { get; set; } = FacultyType.ITandComputerScienceFaculty;
        /// <summary>
        /// A list of string values
        /// </summary>
        public ObservableCollection<Points> PointsList { get; set; }// = new ObservableCollection<Points>(new List<Points>(7));
        public ObservableCollection<Time> TimeList { get; set; }
        /// <summary>
        /// A parameter type
        /// </summary>
        private double _totalScore;
        public double TotalScore
        {
            get => _totalScore;
            set
            {
                SetProperty(ref _totalScore, value);
            }
        }
        /// <summary>
        /// A parameter type
        /// </summary>
        private TimeSpan _totalTime;
        public TimeSpan TotalTime
        {
            get => _totalTime;
            set
            {
                SetProperty(ref _totalTime, value);
            }
        }

        private int _totalTimeInSeconds;
        public int TotalTimeInSeconds
        {
            get => _totalTimeInSeconds;
            set
            {
                SetProperty(ref _totalTimeInSeconds, value);
            }
        }

        private string _result = "0,0";
        public string Result
        {
            get => _result;
            set
            {
                SetProperty(ref _result, value);
            }
        }       
    }
}
