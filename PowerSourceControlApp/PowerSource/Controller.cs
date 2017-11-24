using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerSourceControlApp.PowerSource
{
    class Controller
    {
        private PowerSource _parentPowerSource;
       
        public Controller(PowerSource parentPowerSource)
        {
            _parentPowerSource = parentPowerSource;

        }
    }
}
