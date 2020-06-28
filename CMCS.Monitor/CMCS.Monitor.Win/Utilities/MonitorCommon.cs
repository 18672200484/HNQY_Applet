using CMCS.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMCS.Monitor.Win.Utilities
{
    public class MonitorCommon
    {
        private static MonitorCommon instance;

        public static MonitorCommon GetInstance()
        {
            if (instance == null)
            {
                instance = new MonitorCommon();
            }

            return instance;
        }

        /// <summary>
        /// 根据选中的制样机点击域获取设备编码
        /// </summary>
        /// <param name="selectedMachine"></param>
        /// <returns></returns>
        public string GetMakeMachineCodeBySelected(string selectedMachine)
        {
            switch (selectedMachine)
            {
                case "1号全自动制样机点击域":
                    return GlobalVars.MachineCode_QZDZYJ_1;
                case "2号全自动制样机点击域":
                    return GlobalVars.MachineCode_QZDZYJ_2;
                default:
                    return GlobalVars.MachineCode_QZDZYJ_1;
            }
        }
    }
}
