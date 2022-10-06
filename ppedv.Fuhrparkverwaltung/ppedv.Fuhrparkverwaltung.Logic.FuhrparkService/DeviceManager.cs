using ppedv.Fuhrparkverwaltung.Model.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ppedv.Fuhrparkverwaltung.Logic.FuhrparkService
{
    public class DeviceManager
    {
        public IDevice Device { get;  }

        public DeviceManager(IDevice device)
        {
            Device = device;
            device.Init("Initit Code 1711");
            device.Overheating += Device_Overheating;
        }

        private void Device_Overheating(string obj)
        {
            Console.Beep();
            Console.Beep();
            Console.Beep();
            Device.Init("cooldown!"+obj);
        }
    }
}
