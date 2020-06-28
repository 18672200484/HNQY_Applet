using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CMCS.Common;
using CMCS.Common.DAO;
using CMCS.Common.Entities.AutoMaker;
using CMCS.Common.Entities.Inf;
using CMCS.Monitor.Win.Core;
using CMCS.Monitor.Win.Frms;
using CMCS.Monitor.Win.Frms.Sys;
using CMCS.Monitor.Win.Html;
//
using Xilium.CefGlue;
using CMCS.Common.Entities.Fuel;
using CMCS.DapperDber.Dbs.OracleDb;
using CMCS.DumblyConcealer.Tasks.CarJxSampler.Entities;

namespace CMCS.Monitor.Win.CefGlue
{
    /// <summary>
    /// 汽车采样监控 CefV8Handler
    /// </summary>
    public class CarSamplerCefV8Handler : CefV8Handler
    {
        List<InfEquInfHitch> equInfHitchs = new List<InfEquInfHitch>();

        protected override bool Execute(string name, CefV8Value obj, CefV8Value[] arguments, out CefV8Value returnValue, out string exception)
        {
            exception = null;
            returnValue = null;

            string paramSampler = arguments[0].GetStringValue();
            string CarSampleNum = arguments[1].GetStringValue();
            switch (name)
            {
                // 急停
                case "Stop":
                    if (CarSampleNum == "1")
                    {
                        if (paramSampler == "#1")
                        {
                            //SmpleConyrolCMD entity = new SmpleConyrolCMD();
                            //entity.MACHINECODE = GlobalVars.MachineCode_QCJXCYJ_1;
                            //entity.PAUSE = "1";
                            //entity.DATASTATUS = "0";
                            //SmpleConyrolCMD rulst = CommonDAO.GetInstance().SelfDber.Entity<SmpleConyrolCMD>(" where MACHINECODE=:MACHINECODE and DATASTATUS='0'", new { MACHINECODE = GlobalVars.MachineCode_QCJXCYJ_1 });


                            //if (rulst == null)
                            //{
                            //    CommonDAO.GetInstance().SelfDber.Insert(entity);
                            //}

                            try
                            {
                                OracleDapperDber equDber = new DapperDber.Dbs.OracleDb.OracleDapperDber(CommonDAO.GetInstance().GetCommonAppletConfigString("#1汽车机械采样机接口连接字符串"));
                                CMD_TB cmd = equDber.Entity<CMD_TB>("where 1=1");
                                if (cmd != null)
                                {
                                    cmd.PAUSE = "1";
                                    equDber.Update(cmd);
                                }
                            }
                            catch
                            {


                            }
                            //MessageBox.Show("#1 Stop");
                        }
                        else if (paramSampler == "#2")
                        {
                            //SmpleConyrolCMD entity = new SmpleConyrolCMD();
                            //entity.MACHINECODE = GlobalVars.MachineCode_QCJXCYJ_3;
                            //entity.PAUSE = "1";
                            //entity.DATASTATUS = "0";
                            //SmpleConyrolCMD rulst = CommonDAO.GetInstance().SelfDber.Entity<SmpleConyrolCMD>(" where MACHINECODE=:MACHINECODE and DATASTATUS='0'", new { MACHINECODE = GlobalVars.MachineCode_QCJXCYJ_3 });
                            //if (rulst == null)
                            //{
                            //    CommonDAO.GetInstance().SelfDber.Insert(entity);
                            //}
                            try
                            {
                                OracleDapperDber equDber = new DapperDber.Dbs.OracleDb.OracleDapperDber(CommonDAO.GetInstance().GetCommonAppletConfigString("#3汽车机械采样机接口连接字符串"));
                                CMD_TB cmd = equDber.Entity<CMD_TB>("where 1=1");
                                if (cmd != null)
                                {
                                    cmd.PAUSE = "1";
                                    equDber.Update(cmd);
                                }
                            }
                            catch
                            {


                            }
                        }
                    }
                    else
                    {
                        if (paramSampler == "#1")
                        {
                            //SmpleConyrolCMD entity = new SmpleConyrolCMD();
                            //entity.MACHINECODE = GlobalVars.MachineCode_QCJXCYJ_2;
                            //entity.PAUSE = "1";
                            //entity.DATASTATUS = "0";
                            //SmpleConyrolCMD rulst = CommonDAO.GetInstance().SelfDber.Entity<SmpleConyrolCMD>(" where MACHINECODE=:MACHINECODE and DATASTATUS='0'", new { MACHINECODE = GlobalVars.MachineCode_QCJXCYJ_2 });
                            //if (rulst == null)
                            //{
                            //    CommonDAO.GetInstance().SelfDber.Insert(entity);
                            //}
                            try
                            {
                                OracleDapperDber equDber = new DapperDber.Dbs.OracleDb.OracleDapperDber(CommonDAO.GetInstance().GetCommonAppletConfigString("#2汽车机械采样机接口连接字符串"));
                                CMD_TB cmd = equDber.Entity<CMD_TB>("where 1=1");
                                if (cmd != null)
                                {
                                    cmd.PAUSE = "1";
                                    equDber.Update(cmd);
                                }
                            }
                            catch
                            {


                            }
                        }
                        else if (paramSampler == "#2")
                        {
                            //SmpleConyrolCMD entity = new SmpleConyrolCMD();
                            //entity.MACHINECODE = GlobalVars.MachineCode_QCJXCYJ_4;
                            //entity.PAUSE = "1";
                            //entity.DATASTATUS = "0";
                            //SmpleConyrolCMD rulst = CommonDAO.GetInstance().SelfDber.Entity<SmpleConyrolCMD>(" where MACHINECODE=:MACHINECODE and DATASTATUS='0'", new { MACHINECODE = GlobalVars.MachineCode_QCJXCYJ_4 });
                            //if (rulst == null)
                            //{
                            //    CommonDAO.GetInstance().SelfDber.Insert(entity);
                            //}
                            try
                            {
                                OracleDapperDber equDber = new DapperDber.Dbs.OracleDb.OracleDapperDber(CommonDAO.GetInstance().GetCommonAppletConfigString("#4汽车机械采样机接口连接字符串"));
                                CMD_TB cmd = equDber.Entity<CMD_TB>("where 1=1");
                                if (cmd != null)
                                {
                                    cmd.PAUSE = "1";
                                    equDber.Update(cmd);
                                }
                            }
                            catch
                            {


                            }
                        }
                    }
                    break;
                case "Start":
                    if (CarSampleNum == "1")
                    {
                        if (paramSampler == "#1")
                        {
                            //SmpleConyrolCMD entity = new SmpleConyrolCMD();
                            //entity.MACHINECODE = GlobalVars.MachineCode_QCJXCYJ_1;
                            //entity.RESUME = "1";
                            //entity.DATASTATUS = "0";
                            //SmpleConyrolCMD rulst = CommonDAO.GetInstance().SelfDber.Entity<SmpleConyrolCMD>(" where MACHINECODE=:MACHINECODE and DATASTATUS='0'", new { MACHINECODE = GlobalVars.MachineCode_QCJXCYJ_1 });
                            //if (rulst == null)
                            //{
                            //    CommonDAO.GetInstance().SelfDber.Insert(entity);
                            //}
                            try
                            {
                                OracleDapperDber equDber = new DapperDber.Dbs.OracleDb.OracleDapperDber(CommonDAO.GetInstance().GetCommonAppletConfigString("#1汽车机械采样机接口连接字符串"));
                                CMD_TB cmd = equDber.Entity<CMD_TB>("where 1=1");
                                if (cmd != null)
                                {
                                    cmd.RESUME = "1";
                                    equDber.Update(cmd);
                                }
                            }
                            catch
                            {


                            }

                        }
                        else if (paramSampler == "#2")
                        {
                            //SmpleConyrolCMD entity = new SmpleConyrolCMD();
                            //entity.MACHINECODE = GlobalVars.MachineCode_QCJXCYJ_3;
                            //entity.RESUME = "1";
                            //entity.DATASTATUS = "0";
                            //SmpleConyrolCMD rulst = CommonDAO.GetInstance().SelfDber.Entity<SmpleConyrolCMD>(" where MACHINECODE=:MACHINECODE and DATASTATUS='0'", new { MACHINECODE = GlobalVars.MachineCode_QCJXCYJ_3 });
                            //if (rulst == null)
                            //{
                            //    CommonDAO.GetInstance().SelfDber.Insert(entity);
                            //}
                            try
                            {
                                OracleDapperDber equDber = new DapperDber.Dbs.OracleDb.OracleDapperDber(CommonDAO.GetInstance().GetCommonAppletConfigString("#3汽车机械采样机接口连接字符串"));
                                CMD_TB cmd = equDber.Entity<CMD_TB>("where 1=1");
                                if (cmd != null)
                                {
                                    cmd.RESUME = "1";
                                    equDber.Update(cmd);
                                }
                            }
                            catch
                            {


                            }
                        }
                    }
                    else
                    {

                        if (paramSampler == "#1")
                        {
                            //SmpleConyrolCMD entity = new SmpleConyrolCMD();
                            //entity.MACHINECODE = GlobalVars.MachineCode_QCJXCYJ_2;
                            //entity.RESUME = "1";
                            //entity.DATASTATUS = "0";
                            //SmpleConyrolCMD rulst = CommonDAO.GetInstance().SelfDber.Entity<SmpleConyrolCMD>(" where MACHINECODE=:MACHINECODE and DATASTATUS='0'", new { MACHINECODE = GlobalVars.MachineCode_QCJXCYJ_2 });
                            //if (rulst == null)
                            //{
                            //    CommonDAO.GetInstance().SelfDber.Insert(entity);
                            //}
                            try
                            {
                                OracleDapperDber equDber = new DapperDber.Dbs.OracleDb.OracleDapperDber(CommonDAO.GetInstance().GetCommonAppletConfigString("#2汽车机械采样机接口连接字符串"));
                                CMD_TB cmd = equDber.Entity<CMD_TB>("where 1=1");
                                if (cmd != null)
                                {
                                    cmd.RESUME = "1";
                                    equDber.Update(cmd);
                                }
                            }
                            catch
                            {


                            }
                        }
                        else if (paramSampler == "#2")
                        {
                            //SmpleConyrolCMD entity = new SmpleConyrolCMD();
                            //entity.MACHINECODE = GlobalVars.MachineCode_QCJXCYJ_4;
                            //entity.RESUME = "1";
                            //entity.DATASTATUS = "0";
                            //SmpleConyrolCMD rulst = CommonDAO.GetInstance().SelfDber.Entity<SmpleConyrolCMD>(" where MACHINECODE=:MACHINECODE and DATASTATUS='0'", new { MACHINECODE = GlobalVars.MachineCode_QCJXCYJ_4 });
                            //if (rulst == null)
                            //{
                            //    CommonDAO.GetInstance().SelfDber.Insert(entity);
                            //}
                            try
                            {
                                OracleDapperDber equDber = new DapperDber.Dbs.OracleDb.OracleDapperDber(CommonDAO.GetInstance().GetCommonAppletConfigString("#4汽车机械采样机接口连接字符串"));
                                CMD_TB cmd = equDber.Entity<CMD_TB>("where 1=1");
                                if (cmd != null)
                                {
                                    cmd.RESUME = "1";
                                    equDber.Update(cmd);
                                }
                            }
                            catch
                            {


                            }
                        }
                    }
                    break;
                // 车辆信息
                case "CarInfo":
                    if (paramSampler == "#1")
                        MessageBox.Show("#1 CarInfo");
                    else if (paramSampler == "#2")
                        MessageBox.Show("#2 CarInfo");
                    break;
                // 故障复位
                case "ErrorReset":
                    if (CarSampleNum == "1")
                    {
                        if (paramSampler == "#1")
                        {
                            //SmpleConyrolCMD entity = new SmpleConyrolCMD();
                            //entity.MACHINECODE = GlobalVars.MachineCode_QCJXCYJ_1;
                            //entity.FAULT_RESET = "1";
                            //entity.DATASTATUS = "0";
                            //SmpleConyrolCMD rulst = CommonDAO.GetInstance().SelfDber.Entity<SmpleConyrolCMD>(" where MACHINECODE=:MACHINECODE and DATASTATUS='0'", new { MACHINECODE = GlobalVars.MachineCode_QCJXCYJ_1 });
                            //if (rulst == null)
                            //{
                            //    CommonDAO.GetInstance().SelfDber.Insert(entity);
                            //}
                            try
                            {
                                OracleDapperDber equDber = new DapperDber.Dbs.OracleDb.OracleDapperDber(CommonDAO.GetInstance().GetCommonAppletConfigString("#1汽车机械采样机接口连接字符串"));
                                CMD_TB cmd = equDber.Entity<CMD_TB>("where 1=1");
                                if (cmd != null)
                                {
                                    cmd.FAULT_RESET = "1";
                                    equDber.Update(cmd);
                                }
                            }
                            catch
                            {


                            }
                        }
                        else if (paramSampler == "#2")
                        {
                            //SmpleConyrolCMD entity = new SmpleConyrolCMD();
                            //entity.MACHINECODE = GlobalVars.MachineCode_QCJXCYJ_3;
                            //entity.FAULT_RESET = "1";
                            //entity.DATASTATUS = "0";
                            //SmpleConyrolCMD rulst = CommonDAO.GetInstance().SelfDber.Entity<SmpleConyrolCMD>(" where MACHINECODE=:MACHINECODE and DATASTATUS='0'", new { MACHINECODE = GlobalVars.MachineCode_QCJXCYJ_3 });
                            //if (rulst == null)
                            //{
                            //    CommonDAO.GetInstance().SelfDber.Insert(entity);
                            //}
                            try
                            {
                                OracleDapperDber equDber = new DapperDber.Dbs.OracleDb.OracleDapperDber(CommonDAO.GetInstance().GetCommonAppletConfigString("#3汽车机械采样机接口连接字符串"));
                                CMD_TB cmd = equDber.Entity<CMD_TB>("where 1=1");
                                if (cmd != null)
                                {
                                    cmd.FAULT_RESET = "1";
                                    equDber.Update(cmd);
                                }
                            }
                            catch
                            {


                            }
                        }
                    }
                    else
                    {

                        if (paramSampler == "#1")
                        {
                            //SmpleConyrolCMD entity = new SmpleConyrolCMD();
                            //entity.MACHINECODE = GlobalVars.MachineCode_QCJXCYJ_2;
                            //entity.FAULT_RESET = "1";
                            //entity.DATASTATUS = "0";
                            //SmpleConyrolCMD rulst = CommonDAO.GetInstance().SelfDber.Entity<SmpleConyrolCMD>(" where MACHINECODE=:MACHINECODE and DATASTATUS='0'", new { MACHINECODE = GlobalVars.MachineCode_QCJXCYJ_2 });
                            //if (rulst == null)
                            //{
                            //    CommonDAO.GetInstance().SelfDber.Insert(entity);
                            //}
                            try
                            {
                                OracleDapperDber equDber = new DapperDber.Dbs.OracleDb.OracleDapperDber(CommonDAO.GetInstance().GetCommonAppletConfigString("#2汽车机械采样机接口连接字符串"));
                                CMD_TB cmd = equDber.Entity<CMD_TB>("where 1=1");
                                if (cmd != null)
                                {
                                    cmd.FAULT_RESET = "1";
                                    equDber.Update(cmd);
                                }
                            }
                            catch
                            {


                            }
                        }
                        else if (paramSampler == "#2")
                        {
                            //SmpleConyrolCMD entity = new SmpleConyrolCMD();
                            //entity.MACHINECODE = GlobalVars.MachineCode_QCJXCYJ_4;
                            //entity.FAULT_RESET = "1";
                            //entity.DATASTATUS = "0";
                            //SmpleConyrolCMD rulst = CommonDAO.GetInstance().SelfDber.Entity<SmpleConyrolCMD>(" where MACHINECODE=:MACHINECODE and DATASTATUS='0'", new { MACHINECODE = GlobalVars.MachineCode_QCJXCYJ_4 });
                            //if (rulst == null)
                            //{
                            //    CommonDAO.GetInstance().SelfDber.Insert(entity);
                            //}
                            try
                            {
                                OracleDapperDber equDber = new DapperDber.Dbs.OracleDb.OracleDapperDber(CommonDAO.GetInstance().GetCommonAppletConfigString("#4汽车机械采样机接口连接字符串"));
                                CMD_TB cmd = equDber.Entity<CMD_TB>("where 1=1");
                                if (cmd != null)
                                {
                                    cmd.FAULT_RESET = "1";
                                    equDber.Update(cmd);
                                }
                            }
                            catch
                            {


                            }
                        }
                    }
                    break;
                case "DeviceReset":
                    if (CarSampleNum == "1")
                    {
                        if (paramSampler == "#1")
                        {
                            //SmpleConyrolCMD entity = new SmpleConyrolCMD();
                            //entity.MACHINECODE = GlobalVars.MachineCode_QCJXCYJ_1;
                            //entity.EQUT_RESET = "1";
                            //entity.DATASTATUS = "0";
                            //SmpleConyrolCMD rulst = CommonDAO.GetInstance().SelfDber.Entity<SmpleConyrolCMD>(" where MACHINECODE=:MACHINECODE and DATASTATUS='0'", new { MACHINECODE = GlobalVars.MachineCode_QCJXCYJ_1 });
                            //if (rulst == null)
                            //{
                            //    CommonDAO.GetInstance().SelfDber.Insert(entity);
                            //}
                            try
                            {
                                OracleDapperDber equDber = new DapperDber.Dbs.OracleDb.OracleDapperDber(CommonDAO.GetInstance().GetCommonAppletConfigString("#1汽车机械采样机接口连接字符串"));
                                CMD_TB cmd = equDber.Entity<CMD_TB>("where 1=1");
                                if (cmd != null)
                                {
                                    cmd.EQUT_RESET = "1";
                                    equDber.Update(cmd);
                                }
                            }
                            catch
                            {


                            }
                        }
                        else if (paramSampler == "#2")
                        {
                            //SmpleConyrolCMD entity = new SmpleConyrolCMD();
                            //entity.MACHINECODE = GlobalVars.MachineCode_QCJXCYJ_3;
                            //entity.EQUT_RESET = "1";
                            //entity.DATASTATUS = "0";
                            //SmpleConyrolCMD rulst = CommonDAO.GetInstance().SelfDber.Entity<SmpleConyrolCMD>(" where MACHINECODE=:MACHINECODE and DATASTATUS='0'", new { MACHINECODE = GlobalVars.MachineCode_QCJXCYJ_3 });
                            //if (rulst == null)
                            //{
                            //    CommonDAO.GetInstance().SelfDber.Insert(entity);
                            //}
                            try
                            {
                                OracleDapperDber equDber = new DapperDber.Dbs.OracleDb.OracleDapperDber(CommonDAO.GetInstance().GetCommonAppletConfigString("#3汽车机械采样机接口连接字符串"));
                                CMD_TB cmd = equDber.Entity<CMD_TB>("where 1=1");
                                if (cmd != null)
                                {
                                    cmd.EQUT_RESET = "1";
                                    equDber.Update(cmd);
                                }
                            }
                            catch
                            {


                            }
                        }
                    }
                    else
                    {

                        if (paramSampler == "#1")
                        {
                            //SmpleConyrolCMD entity = new SmpleConyrolCMD();
                            //entity.MACHINECODE = GlobalVars.MachineCode_QCJXCYJ_2;
                            //entity.EQUT_RESET = "1";
                            //entity.DATASTATUS = "0";
                            //SmpleConyrolCMD rulst = CommonDAO.GetInstance().SelfDber.Entity<SmpleConyrolCMD>(" where MACHINECODE=:MACHINECODE and DATASTATUS='0'", new { MACHINECODE = GlobalVars.MachineCode_QCJXCYJ_2 });
                            //if (rulst == null)
                            //{
                            //    CommonDAO.GetInstance().SelfDber.Insert(entity);
                            //}
                            try
                            {
                                OracleDapperDber equDber = new DapperDber.Dbs.OracleDb.OracleDapperDber(CommonDAO.GetInstance().GetCommonAppletConfigString("#2汽车机械采样机接口连接字符串"));
                                CMD_TB cmd = equDber.Entity<CMD_TB>("where 1=1");
                                if (cmd != null)
                                {
                                    cmd.EQUT_RESET = "1";
                                    equDber.Update(cmd);
                                }
                            }
                            catch
                            {


                            }
                        }
                        else if (paramSampler == "#2")
                        {
                            //SmpleConyrolCMD entity = new SmpleConyrolCMD();
                            //entity.MACHINECODE = GlobalVars.MachineCode_QCJXCYJ_4;
                            //entity.EQUT_RESET = "1";
                            //entity.DATASTATUS = "0";
                            //SmpleConyrolCMD rulst = CommonDAO.GetInstance().SelfDber.Entity<SmpleConyrolCMD>(" where MACHINECODE=:MACHINECODE and DATASTATUS='0'", new { MACHINECODE = GlobalVars.MachineCode_QCJXCYJ_4 });
                            //if (rulst == null)
                            //{
                            //    CommonDAO.GetInstance().SelfDber.Insert(entity);
                            //}
                            try
                            {
                                OracleDapperDber equDber = new DapperDber.Dbs.OracleDb.OracleDapperDber(CommonDAO.GetInstance().GetCommonAppletConfigString("#4汽车机械采样机接口连接字符串"));
                                CMD_TB cmd = equDber.Entity<CMD_TB>("where 1=1");
                                if (cmd != null)
                                {
                                    cmd.EQUT_RESET = "1";
                                    equDber.Update(cmd);
                                }
                            }
                            catch
                            {


                            }
                        }
                    }
                    break;
                // 采样历史记录
                case "SampleHistory":
                    if (paramSampler == "#1")
                        MessageBox.Show("#1 SampleHistory");
                    else if (paramSampler == "#2")
                        MessageBox.Show("#2 SampleHistory");
                    break;
                //获取异常信息
                case "GetHitchs":
                    //异常信息
                    string machineCode = string.Empty;
                    if (CarSampleNum == "1")
                    {
                        if (paramSampler == "#1")
                            machineCode = GlobalVars.MachineCode_QCJXCYJ_1;
                        else if (paramSampler == "#2")
                            machineCode = GlobalVars.MachineCode_QCJXCYJ_3;
                    }
                    else
                    {

                        if (paramSampler == "#1")
                            machineCode = GlobalVars.MachineCode_QCJXCYJ_2;
                        else if (paramSampler == "#2")
                            machineCode = GlobalVars.MachineCode_QCJXCYJ_4;
                    }
                    equInfHitchs = CommonDAO.GetInstance().GetEquInfHitchsByTime(machineCode, DateTime.Now);
                    returnValue = CefV8Value.CreateString(Newtonsoft.Json.JsonConvert.SerializeObject(equInfHitchs.Select(a => new { MachineCode = a.MachineCode, HitchTime = a.HitchTime.ToString("yyyy-MM-dd HH:mm"), HitchDescribe = a.HitchDescribe })));
                    break;
                default:
                    returnValue = null;
                    break;
            }

            return true;
        }
    }
}
