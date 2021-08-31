using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rope.Model
{
    public class Points
    {
        private static int counter = 1;
        private double stagePoint;
        private double additionalTasksPoint;
        private double penaltyPoint;

        //Номер этапа.
        public int StageNumber { get; private set; }

        //Очки этапа.
        public double StagePoint
        {
            get => stagePoint;
            set
            {
                if (value < 11)
                {
                    stagePoint = value;
                }
            }
        }

        //Очки за дополнительные задания.
        public double AdditionalTasksPoint
        {
            get => additionalTasksPoint;
            set
            {
                if (value < 6)
                {
                    additionalTasksPoint = value;
                }
            }
        }

        //Штрафные очки
        public double PenaltyPoint 
        { 
            get => penaltyPoint;
            set
            {
                if (value < 3)
                {
                    penaltyPoint = value;
                }
            }
        }

        public Points()
        {
            StageNumber = counter;
            counter = (counter == 7) ? counter = 1 : ++counter;
        }
    }

}
