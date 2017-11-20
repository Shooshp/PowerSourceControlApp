using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PowerSourceControlApp.DapperDTO;

namespace PowerSourceControlApp.PowerSource
{
    class TaskManager
    {
        private Device _parentDevice;
        private List<CurrentTasks> _taskList;

        public TaskManager(Device parent)
        {
            _parentDevice = parent;
        }
 
    }
}


