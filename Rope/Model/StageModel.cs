using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rope.BindingEnums;

namespace Rope.Model
{
    internal class StageModel
    {
        /// <summary>
        /// A stage name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// A type of a stage
        /// </summary>
        public StageType StageType { get; set; }
    }
   
}
