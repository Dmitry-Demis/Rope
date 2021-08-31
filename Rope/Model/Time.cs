using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rope.Model
{
    public class Time
    {

        private string minutes = "00";
        private string seconds = "00";
        private static int counter = 1;
        public Time()
        {            
            StageNumber = counter;
            counter = (counter == 7) ? counter = 1 : ++counter;
        }

        //Номер этапа.
        public double StageNumber { get; private set; }

        //Время в минутах.
        public string Minutes 
        { 
            get => minutes; 
            set
            {
                int s;
                if (value.Length > 0)
                {
                    s = int.Parse(value);
                    if (s.ToString().Length < 3 && s < 60)
                    {
                        minutes = $"{s:d2}";
                    }
                }
            }
                
        }

        //Время в секундах.
        public string Seconds
        {
            get => seconds;
            set
            {
                int s;
                if (value.Length > 0)
                {
                    s = int.Parse(value);
                    if (s.ToString().Length < 3 && s < 60)
                    {
                        seconds = $"{s:d2}";
                    }
                }
            }

        }

        //Общее время выполнения этапа.
        public TimeSpan TotalStageTime
        {
            get => TimeSpan.Parse("00:" + Minutes + ":" + Seconds);
        }

    }
}
