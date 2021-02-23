using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using CMCS.Common;
//
using CMCS.Common.Entities;
using CMCS.Common.Entities.AutoMaker;
using CMCS.Common.Entities.Fuel;
using CMCS.Common.Entities.Inf;
using CMCS.DapperDber.Util;

namespace CMCS.Monitor.DAO
{
	public class MonitorDAO
	{
		private static MonitorDAO instance;

		public static MonitorDAO GetInstance()
		{
			if (instance == null)
			{
				instance = new MonitorDAO();
			}

			return instance;
		}

		private MonitorDAO()
		{ }

		/// <summary>
		/// 根据选中的制样点击域获取设备编码
		/// </summary>
		/// <param name="selectedMachine"></param>
		/// <returns></returns>
		public string GetAutoMakerMachineCodeBySelected(string selectedMachine)
		{
			switch (selectedMachine)
			{
				case "#1制样机点击区域":
					return GlobalVars.MachineCode_QZDZYJ_1;
				case "#2制样机点击区域":
					return GlobalVars.MachineCode_QZDZYJ_2;
				default:
					return GlobalVars.MachineCode_QZDZYJ_1;
			}
		}

		/// <summary>
		/// 根据选中的衡器点击域获取设备编码
		/// </summary>
		/// <param name="selectedMachine"></param>
		/// <returns></returns>
		public string GetTruckWeighterMachineCodeBySelected(string selectedMachine)
		{
			switch (selectedMachine)
			{
				case "汽车衡_1号衡点击域":
					return GlobalVars.MachineCode_QC_Weighter_1;
				case "汽车衡_2号衡点击域":
					return GlobalVars.MachineCode_QC_Weighter_2;
				case "汽车衡_3号衡点击域":
					return GlobalVars.MachineCode_QC_Weighter_3;
				case "汽车衡_4号衡点击域":
					return GlobalVars.MachineCode_QC_Weighter_4;
				case "汽车衡_5号衡点击域":
					return GlobalVars.MachineCode_QC_Weighter_5;
				case "汽车衡_6号衡点击域":
					return GlobalVars.MachineCode_QC_Weighter_6;
				case "汽车衡_7号衡点击域":
					return GlobalVars.MachineCode_QC_Weighter_7;
				case "汽车衡_8号衡点击域":
					return GlobalVars.MachineCode_QC_Weighter_8;
				default:
					return GlobalVars.MachineCode_QC_Weighter_1;
			}
		}

		#region 公共采样机

		/// <summary>
		/// 获取采样机的集样罐清单
		/// </summary>
		/// <param name="machineCode">设备编号</param>
		/// <returns></returns>
		public List<InfEquInfSampleBarrel> GetEquInfSampleBarrels(string machineCode)
		{
			return Dbers.GetInstance().SelfDber.Entities<InfEquInfSampleBarrel>("where MachineCode=:MachineCode order by BarrelType,BarrelNumber asc", new { MachineCode = machineCode });
		}

		/// <summary>
		/// 获取采样机的集样罐清单
		/// </summary>
		/// <param name="machineCode">设备编号</param>
		/// <param name="barreltype">样罐类型 底卸式 密码罐</param>
		/// <returns></returns>
		public List<InfEquInfSampleBarrel> GetEquInfSampleBarrels(string machineCode, string barreltype)
		{
			return Dbers.GetInstance().SelfDber.Entities<InfEquInfSampleBarrel>("where MachineCode=:MachineCode and BarrelType=:BarrelType order by BarrelNumber asc", new { MachineCode = machineCode, BarrelType = barreltype });
		}

		/// <summary>
		/// 获取第三方设备故障信息
		/// </summary>
		/// <param name="machineCode">设备编号</param>
		/// <param name="dtStart">故障起始时间</param>
		/// <returns></returns>
		public List<InfEquInfHitch> GetEquInfHitchs(string machineCode, DateTime dtStart)
		{
			return Dbers.GetInstance().SelfDber.Entities<InfEquInfHitch>("where MachineCode=:MachineCode and HitchTime=:HitchTime order by BarrelNumber asc", new { MachineCode = machineCode, HitchTime = dtStart });
		}

		/// <summary>
		/// 根据批次id获取采样单明细
		/// </summary>
		/// <param name="batchId"></param>
		/// <returns></returns>
		public List<CmcsRCSampling> GetSamplings(string batchId)
		{
			return Dbers.GetInstance().SelfDber.Entities<CmcsRCSampling>("where InfactoryBatchId=:InfactoryBatchId or INFURNACEID=:InfactoryBatchId order by SamplingDate asc", new { InfactoryBatchId = batchId });
		}
		#endregion

		#region 皮带采样机



		#endregion

		#region 翻车机

		/// <summary>
		/// 根据火车入厂记录Id获取煤种、供煤单位、发站、矿点等信息
		/// </summary>
		/// <param name="trainWeightRecord">CmcsTrainWeightRecord ID</param>
		/// <returns></returns>
		public DataTable GetInFactoryBatchInfoByTrainWeightRecordId(string trainWeightRecord)
		{
			return Dbers.GetInstance().SelfDber.ExecuteDataTable(string.Format(@"select s.name as SupplierName,m.name as MineName,fk.FuelName,si.name as StationName from cmcstbtrainweightrecord twr left join cmcstbtransport t on t.Pkid=twr.Id left join cmcstbinfactorybatch ifb on ifb.Id=t.Infactorybatchid left join fultbsupplier s on s.Id=ifb.supplierid left join fultbstationinfo si
on si.Id=ifb.stationid left join fultbmine m on m.Id=ifb.mineid left join fultbfuelkind fk on fk.Id=ifb.fuelkindid where t.id='{0}'", trainWeightRecord));
		}

		#endregion

		/// <summary>
		/// 转换设备系统状态为颜色值
		/// </summary>
		/// <param name="systemStatus">系统状态</param>
		/// <returns></returns>
		public string ConvertMachineStatusToColor(string systemStatus)
		{
			if ("|就绪待机|".Contains("|" + systemStatus + "|"))
				return ColorTranslator.ToHtml(EquipmentStatusColors.BeReady);
			else if ("|正在运行|正在卸样|".Contains("|" + systemStatus + "|"))
				return ColorTranslator.ToHtml(EquipmentStatusColors.Working);
			else if ("|发生故障|".Contains("|" + systemStatus + "|"))
				return ColorTranslator.ToHtml(EquipmentStatusColors.Breakdown);
			else
				return ColorTranslator.ToHtml(EquipmentStatusColors.Forbidden);
		}

		/// <summary>
		/// 转换布尔类型状态为颜色值
		/// </summary>
		/// <param name="status">状态</param>
		/// <returns></returns>
		public string ConvertBooleanToColor(string status)
		{
			return (string.IsNullOrEmpty(status) ? "0" : status) == "1" ? ColorTranslator.ToHtml(EquipmentStatusColors.Working) : ColorTranslator.ToHtml(EquipmentStatusColors.Forbidden);
		}
	}
}
