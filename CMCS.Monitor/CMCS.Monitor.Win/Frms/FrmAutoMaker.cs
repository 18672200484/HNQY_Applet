using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CMCS.Common;
using CMCS.Common.DAO;
using CMCS.Common.Entities;
using CMCS.Common.Entities.AutoMaker;
using CMCS.Common.Enums;
using CMCS.Monitor.DAO;
using CMCS.Monitor.Win.Core;
using CMCS.Monitor.Win.Html;
using CMCS.Monitor.Win.UserControls;
using DevComponents.DotNetBar.Metro;
using Xilium.CefGlue.WindowsForms;
using CMCS.Common.Entities.Fuel;
using CMCS.DumblyConcealer.Tasks.PneumaticTransfer_XMJS.Entities;
using CMCS.DumblyConcealer;

namespace CMCS.Monitor.Win.Frms
{
	public partial class FrmAutoMaker : MetroForm
	{
		/// <summary>
		/// 窗体唯一标识符
		/// </summary>
		public static string UniqueKey = "FrmAutoMaker";
		private string green = "#00C000", red = "#FF0000", white = "#FFFFFF";
		bool tempBool = true;
		string[] strsingna = new string[] { "6mm破碎", "6mm缩分", "3mm破碎", "3mm缩分", "0.2mm破碎", "0.2mm缩分", "红外干燥", "弃料输送机", "清洗样", "除尘系统" };

		CefWebBrowserEx cefWebBrowser = new CefWebBrowserEx();

		string currentMachineCode = GlobalVars.MachineCode_QZDZYJ_1;
		/// <summary>
		/// 当前选中的设备
		/// </summary>
		public string CurrentMachineCode
		{
			get { return currentMachineCode; }
			set { currentMachineCode = value; }
		}

		public FrmAutoMaker()
		{
			InitializeComponent();
		}

		/// <summary>
		/// 窗体初始化
		/// </summary>
		private void FormInit()
		{
#if DEBUG
			gboxTest.Visible = true;
#else
            gboxTest.Visible = false; 
#endif

			cefWebBrowser.StartUrl = SelfVars.Url_AutoMaker;
			cefWebBrowser.Dock = DockStyle.Fill;
			cefWebBrowser.WebClient = new HomePageCefWebClient(cefWebBrowser);
			cefWebBrowser.LoadEnd += new EventHandler<LoadEndEventArgs>(cefWebBrowser_LoadEnd);
			panWebBrower.Controls.Add(cefWebBrowser);
		}

		void cefWebBrowser_LoadEnd(object sender, LoadEndEventArgs e)
		{
			timer1.Enabled = true;
		}

		private void FrmAutoMakerCSKY_Load(object sender, EventArgs e)
		{
			FormInit();
		}

		/// <summary>
		/// 请求数据
		/// </summary>
		void RequestData()
		{
			CommonDAO commonDAO = CommonDAO.GetInstance();
			//MakerDAO automakerDAO = MakerDAO.GetInstance();

			//string value = string.Empty;
			//string value1 = string.Empty, value2 = string.Empty, value3 = string.Empty, value4 = string.Empty, value5 = string.Empty,
			//     value6 = string.Empty, value7 = string.Empty, value8 = string.Empty, value9 = string.Empty, value10 = string.Empty,
			//     value11 = string.Empty, value12 = string.Empty, value13 = string.Empty;

			List<HtmlDataItem> datas = new List<HtmlDataItem>();
			//List<CmcsEquInfHitch> equInfHitchs = new List<CmcsEquInfHitch>();

			#region 全自动制样机

			datas.Clear();

			//value1 = commonDAO.GetSignalDataValue(CurrentMachineCode, "一级加料输送机");
			//value2 = commonDAO.GetSignalDataValue(CurrentMachineCode, "6mm破碎");
			//value3 = commonDAO.GetSignalDataValue(CurrentMachineCode, "3mm破碎");
			//value4 = commonDAO.GetSignalDataValue(CurrentMachineCode, "0.2mm破碎");
			//value5 = commonDAO.GetSignalDataValue(CurrentMachineCode, "6mm缩分");
			//value6 = commonDAO.GetSignalDataValue(CurrentMachineCode, "3mm缩分");
			//value7 = commonDAO.GetSignalDataValue(CurrentMachineCode, "6mm输送机");
			//value8 = commonDAO.GetSignalDataValue(CurrentMachineCode, "3mm输送机");
			//value9 = commonDAO.GetSignalDataValue(CurrentMachineCode, "0.2mm输送机");
			//value10 = commonDAO.GetSignalDataValue(CurrentMachineCode, "弃料输送机");
			//value11 = commonDAO.GetSignalDataValue(CurrentMachineCode, "转接弃料输送机");
			//value12 = commonDAO.GetSignalDataValue(CurrentMachineCode, "红外干燥");
			//value13 = commonDAO.GetSignalDataValue(CurrentMachineCode, "进瓶信号");
			//datas.Add(new HtmlDataItem("一级加料输送机", value1 == "1" ? red : green, eHtmlDataItemType.svg_color));
			//datas.Add(new HtmlDataItem("6mm破碎", commonDAO.GetSignalDataValue(CurrentMachineCode, "6mm破碎"), eHtmlDataItemType.svg_color));
			//datas.Add(new HtmlDataItem("6mm缩分", commonDAO.GetSignalDataValue(CurrentMachineCode, "6mm缩分"), eHtmlDataItemType.svg_color));
			//datas.Add(new HtmlDataItem("3mm破碎", commonDAO.GetSignalDataValue(CurrentMachineCode, "3mm破碎"), eHtmlDataItemType.svg_color));
			//datas.Add(new HtmlDataItem("3mm缩分", commonDAO.GetSignalDataValue(CurrentMachineCode, "3mm缩分"), eHtmlDataItemType.svg_color));
			//datas.Add(new HtmlDataItem("02mm破碎", commonDAO.GetSignalDataValue(CurrentMachineCode, "0.2mm破碎"), eHtmlDataItemType.svg_color));
			//datas.Add(new HtmlDataItem("02mm缩分", commonDAO.GetSignalDataValue(CurrentMachineCode, "0.2mm缩分"), eHtmlDataItemType.svg_color));
			//datas.Add(new HtmlDataItem("红外干燥", value12 == "1" ? red : green, eHtmlDataItemType.svg_color));
			//datas.Add(new HtmlDataItem("弃料输送机", (value10 == "1" || value11 == "1") ? red : green, tempBool, eHtmlDataItemType.svg_color));
			//datas.Add(new HtmlDataItem("一级加料输送机", value1, tempBool, eHtmlDataItemType.svg_dyncolor));
			//datas.Add(new HtmlDataItem("6mm破碎缩分输送机", (value2 == "1" && value7 == "1") ? "1" : "0", tempBool, eHtmlDataItemType.svg_dyncolor));
			//datas.Add(new HtmlDataItem("3mm缩分破碎输送机", (value3 == "1" && value7 == "1") ? "1" : "0", tempBool, eHtmlDataItemType.svg_dyncolor));
			//datas.Add(new HtmlDataItem("6mm缩分弃料输送机", (value5 == "1" && value10 == "1") ? "1" : "0", tempBool, eHtmlDataItemType.svg_dyncolor));
			//datas.Add(new HtmlDataItem("3mm缩分弃料输送机", (value6 == "1" && value10 == "1") ? "1" : "0", tempBool, eHtmlDataItemType.svg_dyncolor));
			//datas.Add(new HtmlDataItem("02mm破碎弃料输送机", (value4 == "1" && value10 == "1") ? "1" : "0", tempBool, eHtmlDataItemType.svg_dyncolor));
			//datas.Add(new HtmlDataItem("6mm输送机", value7, tempBool, eHtmlDataItemType.svg_dyncolor));
			//datas.Add(new HtmlDataItem("3mm输送机", value8, tempBool, eHtmlDataItemType.svg_dyncolor));
			//datas.Add(new HtmlDataItem("红外干燥输送机", (value8 == "1" && value12 == "1") ? "1" : "0", tempBool, eHtmlDataItemType.svg_dyncolor));
			//datas.Add(new HtmlDataItem("02mm破碎输送机", (value4 == "1" && value8 == "1") ? "1" : "0", tempBool, eHtmlDataItemType.svg_dyncolor));
			//datas.Add(new HtmlDataItem("02mm输送机", value9, tempBool, eHtmlDataItemType.svg_dyncolor));
			//datas.Add(new HtmlDataItem("进瓶信号", value13, tempBool, eHtmlDataItemType.svg_dyncolor));

			//value = commonDAO.GetSignalDataValue(CurrentMachineCode, "6mm全水分样瓶");
			//datas.Add(new HtmlDataItem("6mm全水分样瓶", value == "1" ? red : green, eHtmlDataItemType.svg_color));
			//value = commonDAO.GetSignalDataValue(CurrentMachineCode, "3mm存查样样瓶");
			//datas.Add(new HtmlDataItem("3mm存查样样瓶", value == "1" ? red : green, eHtmlDataItemType.svg_color));
			//value = commonDAO.GetSignalDataValue(CurrentMachineCode, "0.2mm分析样样瓶");
			//datas.Add(new HtmlDataItem("02mm分析样样瓶", value == "1" ? red : green, eHtmlDataItemType.svg_color));
			//value = commonDAO.GetSignalDataValue(CurrentMachineCode, "0.2mm备查样样瓶");
			//datas.Add(new HtmlDataItem("02mm备查样样瓶", value == "1" ? red : green, eHtmlDataItemType.svg_color));
			//value = commonDAO.GetSignalDataValue(CurrentMachineCode, "总经理备查样瓶一");
			//datas.Add(new HtmlDataItem("总经理备查样瓶一", value == "1" ? red : green, eHtmlDataItemType.svg_color));
			//value = commonDAO.GetSignalDataValue(CurrentMachineCode, "总经理备查样瓶二");
			//datas.Add(new HtmlDataItem("总经理备查样瓶二", value == "1" ? red : green, eHtmlDataItemType.svg_color));
			//value = commonDAO.GetSignalDataValue(CurrentMachineCode, "总经理备查样瓶三");
			//datas.Add(new HtmlDataItem("总经理备查样瓶三", value == "1" ? red : green, eHtmlDataItemType.svg_color));

			//string MakeCode = commonDAO.GetSignalDataValue(CurrentMachineCode, "制样编码");
			//datas.Add(new HtmlDataItem("制样编码", ConvertSignalValue(MakeCode), eHtmlDataItemType.svg_text));
			//datas.Add(new HtmlDataItem("开始时间", ConvertSignalValue(commonDAO.GetSignalDataValue(CurrentMachineCode, "截取开始时间")), eHtmlDataItemType.svg_text));
			//datas.Add(new HtmlDataItem("煤种", ConvertSignalValue(commonDAO.GetSignalDataValue(CurrentMachineCode, "煤种")), eHtmlDataItemType.svg_text));
			//datas.Add(new HtmlDataItem("水分", ConvertSignalValue(commonDAO.GetSignalDataValue(CurrentMachineCode, "水分")), eHtmlDataItemType.svg_text));
			//datas.Add(new HtmlDataItem("粒度", ConvertSignalValue(commonDAO.GetSignalDataValue(CurrentMachineCode, "粒度")), eHtmlDataItemType.svg_text));
			//datas.Add(new HtmlDataItem("一级加料称", ConvertSignalValue(commonDAO.GetSignalDataValue(CurrentMachineCode, "一级加料称"), "kg"), eHtmlDataItemType.svg_text));
			//datas.Add(new HtmlDataItem("红外干燥实际温度", ConvertSignalValue(commonDAO.GetSignalDataValue(CurrentMachineCode, "红外干燥实际温度"), "℃"), eHtmlDataItemType.svg_text));
			//datas.Add(new HtmlDataItem("红外干燥设定温度", ConvertSignalValue(commonDAO.GetSignalDataValue(CurrentMachineCode, "红外干燥设定温度"), "℃"), eHtmlDataItemType.svg_text));

			////判断是否有失败的样
			//CmcsCYGControlCMDDetail errorcmd = Dbers.GetInstance().SelfDber.Entity<CmcsCYGControlCMDDetail>(" WHERE STATUS='处理失败'");
			//if (errorcmd != null)
			//{
			//    datas.Add(new HtmlDataItem("恢复当前任务", "true", eHtmlDataItemType.svg_visible));
			//    datas.Add(new HtmlDataItem("继续下条任务", "true", eHtmlDataItemType.svg_visible));
			//}
			//else
			//{
			//    datas.Add(new HtmlDataItem("恢复当前任务", "false", eHtmlDataItemType.svg_visible));
			//    datas.Add(new HtmlDataItem("继续下条任务", "false", eHtmlDataItemType.svg_visible));
			//}

			////出样明细
			//List<CmcsMakerRecord> listMakerRecord = automakerDAO.GetMakerRecordByMakeCode(MakeCode);

			//判断最后一个出样是否正在传输
			//CmcsMakerRecord lastMakerRecord = listMakerRecord.FirstOrDefault();
			//if (lastMakerRecord != null)
			//    if (ConvertCYGControlCMDStatus(automakerDAO.GetMakerRecordStatusByBarrelCode(lastMakerRecord.BarrelCode)) == "正在传输")
			//        datas.Add(new HtmlDataItem("发送站", red, eHtmlDataItemType.svg_blinkcolor));
			//    else
			//        datas.Add(new HtmlDataItem("发送站", green, eHtmlDataItemType.svg_blinkcolor));
			//else
			//    datas.Add(new HtmlDataItem("发送站", green, eHtmlDataItemType.svg_blinkcolor));
			datas.Add(new HtmlDataItem("制样机1_系统", MonitorDAO.GetInstance().ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QZDZYJ_1, eSignalDataName.系统.ToString())), eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("制样机2_系统", MonitorDAO.GetInstance().ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QZDZYJ_2, eSignalDataName.系统.ToString())), eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("红外干燥温度", commonDAO.GetSignalDataValue(CurrentMachineCode, "红外干燥设定温度"), eHtmlDataItemType.svg_text));
			string keys = string.Empty;
			foreach (var item in strsingna)
			{
				string value = commonDAO.GetSignalDataValue(CurrentMachineCode, item);
				if (value == "1")
				{
					keys += item;
				}
			}
			datas.Add(new HtmlDataItem(CurrentMachineCode + "keys", keys, eHtmlDataItemType.svg_scroll));

			#region 气动传输

			//CurrentMachineCode = GlobalVars.InterfaceType_XMJS_QD;
			//value = commonDAO.GetSignalDataValue(CurrentMachineCode, "全自动存样工作站1");
			//datas.Add(new HtmlDataItem("发送站样瓶", value == "1" ? red : green, eHtmlDataItemType.svg_blinkcolor));
			//datas.Add(new HtmlDataItem("发送站样瓶号", commonDAO.GetSignalDataValue(CurrentMachineCode, "样罐芯片码"), eHtmlDataItemType.svg_text));

			#endregion

			datas.Add(new HtmlDataItem("制样编码", commonDAO.GetSignalDataValue(CurrentMachineCode, "制样编码"), eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("制样开始时间", commonDAO.GetSignalDataValue(CurrentMachineCode, "开始时间"), eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("煤种", getMeiZhongByStatus(commonDAO.GetSignalDataValue(CurrentMachineCode, "煤种")), eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("水分", getShuiFenByStatus(commonDAO.GetSignalDataValue(CurrentMachineCode, "水分")), eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("粒度", getLiDuByStatus(commonDAO.GetSignalDataValue(CurrentMachineCode, "粒度")), eHtmlDataItemType.svg_text));


			//// 发送到页面
			cefWebBrowser.Browser.GetMainFrame().ExecuteJavaScript("requestData(" + Newtonsoft.Json.JsonConvert.SerializeObject(datas) + ");", "", 0);

			#endregion

			#region 出样信息
			string MakeCode = commonDAO.GetSignalDataValue(CurrentMachineCode, "制样编码");
			//datas.Add(new HtmlDataItem("制样编码", MakeCode, eHtmlDataItemType.svg_text));
			//datas.Add(new HtmlDataItem("制样开始时间", getMeiZhongByStatus(commonDAO.GetSignalDataValue(CurrentMachineCode, "开始时间")), eHtmlDataItemType.svg_text));
			//datas.Add(new HtmlDataItem("煤种", getMeiZhongByStatus(commonDAO.GetSignalDataValue(CurrentMachineCode, "煤种")), eHtmlDataItemType.svg_text));
			//datas.Add(new HtmlDataItem("水分", getShuiFenByStatus(commonDAO.GetSignalDataValue(CurrentMachineCode, "水分")), eHtmlDataItemType.svg_text));
			//datas.Add(new HtmlDataItem("粒度", getLiDuByStatus(commonDAO.GetSignalDataValue(CurrentMachineCode, "粒度")), eHtmlDataItemType.svg_text));
			CmcsRCMake make = Dbers.GetInstance().SelfDber.Entity<CmcsRCMake>("where MakeCode=:MakeCode", new { MakeCode = MakeCode });
			if (make != null)
			{
				List<CmcsRCMakeDetail> listMakerRecord = Dbers.GetInstance().SelfDber.Entities<CmcsRCMakeDetail>("where makeid=:makeid order by BarrelTime desc", new { makeid = make.Id });
				List<object> listRes = new List<object>();
				foreach (CmcsRCMakeDetail item in listMakerRecord)
				{
					//获取样瓶传输状态
					//string Status = automakerDAO.GetMakerRecordStatusByBarrelCode(item.BarrelCode);
					string Status = "";
					InfQDBill InfQDBill = DcDbers.GetInstance().PneumaticTransfer_Dber.Entity<InfQDBill>("where SampleId=@SampleId", new { SampleId = item.BarrelCode });
					if (InfQDBill == null)
					{
						Status = "等待传输";
					}
					else
					{
						Status = getChuanShuZhuangTai(InfQDBill.DataStatus);
					}
					var stu = new
					{
						EndTime = item.BarrelTime.ToString("HH:mm:ss"),
						YPType = item.SampleType,
						BarrelCode = item.BarrelCode,
						YPWeight = item.Weight,
						Status = Status,
					};
					listRes.Add(stu);
				}
				cefWebBrowser.Browser.GetMainFrame().ExecuteJavaScript("LoadSampleInfo(" + Newtonsoft.Json.JsonConvert.SerializeObject(listRes) + ");", "", 0);
			}
			//List<CmcsRCMakeDetail> listMakerRecord = Dbers.GetInstance().SelfDber.Entities<CmcsRCMakeDetail>("where barrelcode  is not null order by CreateDate desc");
			//List<object> listRes = new List<object>();

			//foreach (CmcsMakerRecord item in listMakerRecord)
			//{
			//    //获取样瓶传输状态
			//    string Status = automakerDAO.GetMakerRecordStatusByBarrelCode(item.BarrelCode);
			//    var stu = new
			//    {
			//        EndTime = item.EndTime.ToString("yyyy-MM-dd HH:mm"),
			//        YPType = item.YPType,
			//        BarrelCode = item.BarrelCode,
			//        YPWeight = item.YPWeight,
			//        Status = ConvertCYGControlCMDStatus(Status),
			//        Place = (item.YPType == "0.2mm分析样" ? "化验室" : "存样柜")
			//    };
			//    listRes.Add(stu);
			//}

			//int longlength = listRes.Count;
			//for (int i = 0; i < 7 - longlength; i++)
			//{
			//    var stu = new
			//    {
			//        EndTime = string.Empty,
			//        YPType = string.Empty,
			//        BarrelCode = string.Empty,
			//        YPWeight = string.Empty,
			//        Status = string.Empty,
			//        Place = string.Empty
			//    };
			//    listRes.Add(stu);
			//}
			//cefWebBrowser.Browser.GetMainFrame().ExecuteJavaScript("LoadSampleInfo(" + Newtonsoft.Json.JsonConvert.SerializeObject(listRes) + ");", "", 0);

			#endregion


			//string keys = string.Empty;
			//foreach (string item in strSignal)
			//{
			//    var scrollvalue = commonDAO.GetSignalDataValue(CurrentMachineCode, item);
			//    if (scrollvalue == "运行" || scrollvalue == "1")
			//    {
			//        keys += item;
			//    }
			//}
			//datas.Add(new HtmlDataItem(CurrentMachineCode + "Keys", keys, eHtmlDataItemType.svg_scroll));

			tempBool = tempBool ? false : true;
		}

		//转换状态，临时处理
		private string ConvertCYGControlCMDStatus(string Status)
		{
			if (Status == "存样柜处理中" || Status == "气动传输处理中" || Status == "等待结果")
				return "正在传输";
			else if (Status == "处理失败")
				return "传输失败";
			else if (Status == "处理完成")
				return "传输成功";
			else if (Status == "正在处理")
				return "等待传输";
			else
				return "等待传输";
		}
		//煤种转换
		private string getMeiZhongByStatus(string Status)
		{
			if (Status == "1")
				return "褐煤";
			else if (Status == "2")
				return "烟煤";
			else if (Status == "3")
				return "无烟煤";
			else
				return "其它煤";
		}

		//水分转换
		private string getShuiFenByStatus(string Status)
		{
			if (Status == "1")
				return "湿煤";
			else if (Status == "2")
				return "一般湿煤";
			else if (Status == "3")
				return "干煤";
			else
				return "--";
		}

		//粒度转换
		private string getLiDuByStatus(string Status)
		{
			if (Status == "1")
				return "13-50";
			else if (Status == "2")
				return "50-100";
			else
				return "--";
		}

		public string getChuanShuZhuangTai(decimal ypType)
		{
			string zt = "";
			if (ypType == 0)
				zt = "待执行";
			if (ypType == 1)
				zt = "传输成功";
			if (ypType == 2)
				zt = "传输失败";
			if (ypType == 3)
				zt = "气动读取传输中";

			return zt;

		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			// 界面不可见时，停止发送数据
			if (!this.Visible) return;

			RequestData();
		}

		/// <summary>
		/// 刷新页面
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnRefresh_Click(object sender, EventArgs e)
		{
			cefWebBrowser.Browser.Reload();
		}

		private void btnRequestData_Click(object sender, EventArgs e)
		{
			RequestData();
		}

		private void buttonX1_Click(object sender, EventArgs e)
		{
			// 发送到页面
			cefWebBrowser.Browser.GetMainFrame().ExecuteJavaScript("testColor();", "", 0);
		}

		private void buttonX2_Click(object sender, EventArgs e)
		{
			FrmAutoMakerRecord frm = new FrmAutoMakerRecord();
			frm.ShowDialog();
		}

		private bool SysStatus(string systemStatus)
		{
			if ("|就绪待机|离线|".Contains("|" + systemStatus + "|"))
				return false;
			else if ("|正在运行|正在卸样|".Contains("|" + systemStatus + "|"))
				return true;
			else if ("|发生故障|".Contains("|" + systemStatus + "|"))
				return false;
			else
				return false;
		}

		/// <summary>
		/// 转换信号点值
		/// </summary>
		/// <param name="signalValue">信号点值</param>
		/// <param name="unit">单位</param>
		/// <returns></returns>
		private string ConvertSignalValue(string signalValue, string unit = "")
		{
			return !string.IsNullOrEmpty(signalValue) ? signalValue + unit : "--";
		}
	}

	[Serializable()]
	public class CmcsMakerRecord
	{
		public string BarrelTime { get; set; }//出样时间
		public string YPType { get; set; }//样品类型
		public string BarrelCode { get; set; }//样品编码
		public string YPWeight { get; set; }//样重
		public string Status { get; set; }//状态
	}

}
