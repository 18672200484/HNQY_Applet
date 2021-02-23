// 此代码由 NhGenerator v1.0.9.0 工具生成。

using System;
using System.Collections;
using CMCS.Common.Entities.Sys;
using CMCS.DapperDber.Attrs;

namespace CMCS.Common.Entities.CarTransport
{
	/// <summary>
	/// 汽车智能化-车号识别
	/// </summary>
	[Serializable]
	[CMCS.DapperDber.Attrs.DapperBind("cmcstbtrainrecognition")]
	public class CmcsTrainRecognition
	{
		[DapperPrimaryKey]
		public string Id { get; set; }

		public DateTime CreateDate { get; set; }

		/// <summary>
		/// 顺序号
		/// </summary>
		public int OrderNum { get; set; }

		/// <summary>
		/// 车型
		/// </summary>
		public string CarModel { get; set; }

		/// <summary>
		/// 车号
		/// </summary>
		public string CarNumber { get; set; }

		/// <summary>
		/// 穿过时间
		/// </summary>
		public DateTime CrossTime { get; set; }

		/// <summary>
		/// 速度
		/// </summary>
		public int Speed { get; set; }

		/// <summary>
		/// 状态
		/// </summary>
		public int Status { get; set; }

		/// <summary>
		/// 设备编号
		/// </summary>
		public string MachineCode { get; set; }

		/// <summary>
		/// 方向
		/// </summary>
		public string Direction { get; set; }


	}
}
