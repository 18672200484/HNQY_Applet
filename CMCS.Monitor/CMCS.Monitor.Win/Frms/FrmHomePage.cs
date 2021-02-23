using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using Xilium.CefGlue.WindowsForms;
using CMCS.Common;
using CMCS.Monitor.Win.Core;
using CMCS.Common.DAO;
using CMCS.Monitor.Win.Html;
using CMCS.Common.Enums;
using Xilium.CefGlue;
using CMCS.Monitor.Win.UserControls;
using CMCS.Monitor.DAO;

namespace CMCS.Monitor.Win.Frms
{
	public partial class FrmHomePage : DevComponents.DotNetBar.Metro.MetroForm
	{
		/// <summary>
		/// 窗体唯一标识符
		/// </summary>
		public static string UniqueKey = "FrmHomePage";

		CommonDAO commonDAO = CommonDAO.GetInstance();

		CefWebBrowserEx cefWebBrowser = new CefWebBrowserEx();

		public FrmHomePage()
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
			cefWebBrowser.StartUrl = SelfVars.Url_HomePage;
			cefWebBrowser.Dock = DockStyle.Fill;
			cefWebBrowser.WebClient = new HomePageCefWebClient(cefWebBrowser);
			cefWebBrowser.LoadEnd += new EventHandler<LoadEndEventArgs>(cefWebBrowser_LoadEnd);
			panWebBrower.Controls.Add(cefWebBrowser);
		}

		void cefWebBrowser_LoadEnd(object sender, LoadEndEventArgs e)
		{
			timer1.Enabled = true;

			//RequestData();
		}

		private void FrmHomePage_Load(object sender, EventArgs e)
		{
			FormInit();
		}

		/// <summary>
		/// 测试 - 刷新页面
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnRefresh_Click(object sender, EventArgs e)
		{
			cefWebBrowser.Browser.Reload();
		}

		/// <summary>
		/// 测试 - 数据刷新
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnRequestData_Click(object sender, EventArgs e)
		{
			RequestData();
		}

		/// <summary>
		/// 请求数据
		/// </summary>
		void RequestData()
		{
			string value = string.Empty, machineCode = string.Empty;
			List<HtmlDataItem> datas = new List<HtmlDataItem>();

			datas.Clear();
			datas.Add(new HtmlDataItem("总体信息_汽车进厂车数", ConvertSignalValue(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HomePage_1, "总体信息_汽车进厂车数"), ""), eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("总体信息_待采样车数", ConvertSignalValue(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HomePage_1, "总体信息_待采样车数"), ""), eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("总体信息_已采样车数", ConvertSignalValue(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HomePage_1, "总体信息_已采样车数"), ""), eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("总体信息_过重车数", ConvertSignalValue(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HomePage_1, "总体信息_过重车数"), ""), eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("总体信息_待过重车数", ConvertSignalValue(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HomePage_1, "总体信息_待过重车数"), ""), eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("总体信息_已卸车数", ConvertSignalValue(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HomePage_1, "总体信息_已卸车数"), ""), eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("总体信息_待卸车数", ConvertSignalValue(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HomePage_1, "总体信息_待卸车数"), ""), eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("总体信息_过轻车数", ConvertSignalValue(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HomePage_1, "总体信息_过轻车数"), ""), eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("总体信息_待过轻车数", ConvertSignalValue(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HomePage_1, "总体信息_待过轻车数"), ""), eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("总体信息_出厂车数", ConvertSignalValue(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HomePage_1, "总体信息_出厂车数"), ""), eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("总体信息_汽车来煤量", ConvertSignalValue(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HomePage_1, "总体信息_汽车来煤量"), ""), eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("总体信息_火车车数", ConvertSignalValue(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HomePage_1, "总体信息_火车车数"), ""), eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("总体信息_火车煤量", ConvertSignalValue(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HomePage_1, "总体信息_火车煤量"), ""), eHtmlDataItemType.svg_text));

			datas.Add(new HtmlDataItem("入厂车数", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HomePage_1, "总体信息_汽车进厂车数"), eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("出厂车数", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HomePage_1, "总体信息_汽车出厂车数"), eHtmlDataItemType.svg_text));

			datas.Add(new HtmlDataItem("火车_入厂车数", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HomePage_1, "火车_入厂车数"), eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("火车_#1已翻车数", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HomePage_1, "火车_#1已翻车数"), eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("火车_#2已翻车数", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HomePage_1, "火车_#2已翻车数"), eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("火车_已翻车数", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HomePage_1, "火车_已翻车数"), eHtmlDataItemType.svg_text));

			datas.Add(new HtmlDataItem("煤场指标_煤场煤量", ConvertSignalValue(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HomePage_1, "总体信息_煤场煤量"), ""), eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("煤场指标_热值", ConvertSignalValue(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HomePage_1, "总体信息_热值"), ""), eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("煤场指标_硫分", ConvertSignalValue(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HomePage_1, "总体信息_硫分"), ""), eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("煤场指标_挥发分", ConvertSignalValue(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HomePage_1, "煤场指标_挥发分"), ""), eHtmlDataItemType.svg_text));

			datas.Add(new HtmlDataItem("采制化存指标_采样数", ConvertSignalValue(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HomePage_1, "采制化存指标_采样数"), ""), eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("采制化存指标_制样数", ConvertSignalValue(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HomePage_1, "采制化存指标_制样数"), ""), eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("采制化存指标_化验数", ConvertSignalValue(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HomePage_1, "采制化存指标_化验数"), ""), eHtmlDataItemType.svg_text));

			int zpcps1 = int.Parse(commonDAO.GetSignalDataValue("#1智能存样柜", "中瓶已存瓶数"));
			int zpkcs1 = int.Parse(commonDAO.GetSignalDataValue("#1智能存样柜", "中瓶空仓数"));
			int xpcps1 = int.Parse(commonDAO.GetSignalDataValue("#1智能存样柜", "小瓶已存瓶数"));
			int xpkcs1 = int.Parse(commonDAO.GetSignalDataValue("#1智能存样柜", "小瓶空仓数"));

			int zpcps2 = int.Parse(commonDAO.GetSignalDataValue("#2智能存样柜", "中瓶已存瓶数"));
			int zpkcs2 = int.Parse(commonDAO.GetSignalDataValue("#2智能存样柜", "中瓶空仓数"));
			int xpcps2 = int.Parse(commonDAO.GetSignalDataValue("#2智能存样柜", "小瓶已存瓶数"));
			int xpkcs2 = int.Parse(commonDAO.GetSignalDataValue("#2智能存样柜", "小瓶空仓数"));

			int zcw = zpcps1 + zpkcs1 + xpcps1 + xpkcs1 + zpcps2 + zpkcs2 + xpcps2 + xpkcs2;
			int yccw = zpcps1 + xpcps1 + zpcps2 + xpcps2;

			datas.Add(new HtmlDataItem("采制化存指标_存样数", ConvertSignalValue(yccw.ToString() + "/" + zcw.ToString(), ""), eHtmlDataItemType.svg_text));
			//datas.Add(new HtmlDataItem("#1动态衡今日进出厂数量", string.Format("进厂:{0}节  出厂:{1}节", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_GDH_1, "今日进厂数量"), commonDAO.GetSignalDataValue(GlobalVars.MachineCode_GDH_1, "今日出厂数量")), eHtmlDataItemType.svg_text));

			//datas.Add(new HtmlDataItem("#1火车机械采样机", ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HCJXCYJ_1, eSignalDataName.系统.ToString())), eHtmlDataItemType.svg_color));
			//datas.Add(new HtmlDataItem("#2火车机械采样机", ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HCJXCYJ_2, eSignalDataName.系统.ToString())), eHtmlDataItemType.svg_color));

			//datas.Add(new HtmlDataItem("#1翻车机", ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_TrunOver_1, eSignalDataName.系统.ToString())), eHtmlDataItemType.svg_color));
			//datas.Add(new HtmlDataItem("#2翻车机", ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_TrunOver_2, eSignalDataName.系统.ToString())), eHtmlDataItemType.svg_color));

			//datas.Add(new HtmlDataItem("#1皮带采样机", ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_PDCYJ_1, eSignalDataName.系统.ToString())), eHtmlDataItemType.svg_color));
			//datas.Add(new HtmlDataItem("#2皮带采样机", ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_PDCYJ_2, eSignalDataName.系统.ToString())), eHtmlDataItemType.svg_color));

			datas.Add(new HtmlDataItem("#1重车衡", ConvertBooleanToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QC_Weighter_3, "系统")), eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("#1重车衡当前车号", ConvertSignalValue(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QC_Weighter_3, "当前车号")), eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("#2重车衡", ConvertBooleanToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QC_Weighter_4, "系统")), eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("#2重车衡当前车号", ConvertSignalValue(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QC_Weighter_4, "当前车号")), eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("#1轻车衡", ConvertBooleanToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QC_Weighter_1, "系统")), eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("#1轻车衡当前车号", ConvertSignalValue(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QC_Weighter_1, "当前车号")), eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("#2轻车衡", ConvertBooleanToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QC_Weighter_2, "系统")), eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("#2轻车衡当前车号", ConvertSignalValue(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QC_Weighter_2, "当前车号")), eHtmlDataItemType.svg_text));
			datas.Add(new HtmlDataItem("#1汽车机械采样机", ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QCJXCYJ_1, eSignalDataName.系统.ToString())), eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("#2汽车机械采样机", ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QCJXCYJ_2, eSignalDataName.系统.ToString())), eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("#3汽车机械采样机", ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QCJXCYJ_3, eSignalDataName.系统.ToString())), eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("#4汽车机械采样机", ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QCJXCYJ_4, eSignalDataName.系统.ToString())), eHtmlDataItemType.svg_color));

			datas.Add(new HtmlDataItem("#1入厂皮带采样机", ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_PDCYJ_1, eSignalDataName.系统.ToString())), eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("#2入厂皮带采样机", ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_PDCYJ_2, eSignalDataName.系统.ToString())), eHtmlDataItemType.svg_color));

			datas.Add(new HtmlDataItem("#1全自动制样机", ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QZDZYJ_1, eSignalDataName.系统.ToString())), eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("#2全自动制样机", ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QZDZYJ_2, eSignalDataName.系统.ToString())), eHtmlDataItemType.svg_color));

			datas.Add(new HtmlDataItem("#1智能存样柜", CYGConvertBooleanToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_CYG1, "总体状态")), eHtmlDataItemType.svg_color));
			datas.Add(new HtmlDataItem("#2智能存样柜", CYGConvertBooleanToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_CYG2, "总体状态")), eHtmlDataItemType.svg_color));
			//string XMJS_QD_Color = ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QD, eSignalDataName.系统.ToString()));
			//datas.Add(new HtmlDataItem("#1气动传输_气动站1", XMJS_QD_Color, eHtmlDataItemType.svg_color));
			//datas.Add(new HtmlDataItem("#1气动传输_气动站2", XMJS_QD_Color, eHtmlDataItemType.svg_color));
			//datas.Add(new HtmlDataItem("#1气动传输_气动站3", XMJS_QD_Color, eHtmlDataItemType.svg_color));

			//datas.Add(new HtmlDataItem("#1斗轮机", ConvertMachineStatusToColor(commonDAO.GetSignalDataValue("#1斗轮机", eSignalDataName.系统.ToString())), eHtmlDataItemType.svg_color));
			//datas.Add(new HtmlDataItem("#2斗轮机", ConvertMachineStatusToColor(commonDAO.GetSignalDataValue("#2斗轮机", eSignalDataName.系统.ToString())), eHtmlDataItemType.svg_color));

			//datas.Add(new HtmlDataItem("#1皮带秤", commonDAO.GetSignalDataValueDouble("#1皮带秤", eSignalDataName.瞬时流量.ToString()) > 0 ? ColorTranslator.ToHtml(EquipmentStatusColors.Working) : ColorTranslator.ToHtml(EquipmentStatusColors.BeReady), eHtmlDataItemType.svg_color));
			//datas.Add(new HtmlDataItem("#2皮带秤", commonDAO.GetSignalDataValueDouble("#2皮带秤", eSignalDataName.瞬时流量.ToString()) > 0 ? ColorTranslator.ToHtml(EquipmentStatusColors.Working) : ColorTranslator.ToHtml(EquipmentStatusColors.BeReady), eHtmlDataItemType.svg_color));

			// 添加更多...

			// 发送到页面
			cefWebBrowser.Browser.GetMainFrame().ExecuteJavaScript("requestData(" + Newtonsoft.Json.JsonConvert.SerializeObject(datas) + ");", "", 0);
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			// 界面不可见时，停止发送数据
			if (!this.Visible) return;

			RequestData();
		}

		/// <summary>
		/// 转换设备系统状态为颜色值
		/// </summary>
		/// <param name="systemStatus">系统状态</param>
		/// <returns></returns>
		private string ConvertMachineStatusToColor(string systemStatus)
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
		private string ConvertBooleanToColor(string status)
		{
			return status.ToLower() == "1" ? ColorTranslator.ToHtml(EquipmentStatusColors.BeReady) : ColorTranslator.ToHtml(EquipmentStatusColors.Breakdown);
		}


		/// <summary>
		/// 存样柜转换布尔类型状态为颜色值
		/// </summary>
		/// <param name="status">状态</param>
		/// <returns></returns>
		private string CYGConvertBooleanToColor(string status)
		{

			if (status.ToLower() == "1")
			{
				return ColorTranslator.ToHtml(EquipmentStatusColors.Working);
			}
			else if (status.ToLower() == "2")
			{
				return ColorTranslator.ToHtml(EquipmentStatusColors.Breakdown);
			}
			else
			{

				return ColorTranslator.ToHtml(EquipmentStatusColors.BeReady);
			}
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

	public class HomePageCefWebClient : CefWebClient
	{
		CefWebBrowser cefWebBrowser;

		public HomePageCefWebClient(CefWebBrowser cefWebBrowser) : base(cefWebBrowser)
		{
			this.cefWebBrowser = cefWebBrowser;
		}

		protected override bool OnProcessMessageReceived(CefBrowser browser, CefProcessId sourceProcess, CefProcessMessage message)
		{
			if (message.Name == "OpenTrainBeltSampler")
				SelfVars.MainFrameForm.OpenTrainBeltSampler();
			else if (message.Name == "OpenAutoMaker")
				SelfVars.MainFrameForm.OpenAutoMaker();
			else if (message.Name == "OpenTrainTipper")
				SelfVars.MainFrameForm.OpenTrainTipper();
			else if (message.Name == "OpenWeightBridgeLoadToday")
				SelfVars.MainFrameForm.OpenWeightBridgeLoadToday();
			else if (message.Name == "OpenTruckWeighter")
				SelfVars.MainFrameForm.OpenTruckWeighter();
			else if (message.Name == "OpenTruckMachinerySampler")
				SelfVars.MainFrameForm.OpenCarSampler("1");
			else if (message.Name == "OpenAutoCupboardPneumaticTransfer")
				SelfVars.MainFrameForm.OpenAutoCupboardPneumaticTransfer();
			else if (message.Name == "OpenAssayManage")
				SelfVars.MainFrameForm.OpenAssayManage();
			else if (message.Name == "OpenEquInfHitch")
				//设备异常信息
				SelfVars.MainFrameForm.OpenEquInfHitch(message.Arguments.GetString(0));
			else if (message.Name == "OpenControlCmd")
				//设备异常信息
				SelfVars.MainFrameForm.OpenCygControlCmd();
			else if (message.Name == "OpenCarSampler1")
				SelfVars.MainFrameForm.OpenCarSampler("1");
			else if (message.Name == "OpenCarSampler2")
				SelfVars.MainFrameForm.OpenCarSampler("2");
			else if (message.Name == "AutoMakerChangeSelected")
				SelfVars.AutoMakerForm.CurrentMachineCode = MonitorDAO.GetInstance().GetAutoMakerMachineCodeBySelected(message.Arguments.GetString(0));
			else if (message.Name == "OpenTrainRecognition")
				SelfVars.MainFrameForm.OpenTrainRecognition();
			else if (message.Name == "TruckWeighterChangeSelected")
				SelfVars.TruckWeighterForm.CurrentMachineCode = MonitorDAO.GetInstance().GetTruckWeighterMachineCodeBySelected(message.Arguments.GetString(0));
			return true;
			//return base.OnProcessMessageReceived(browser, sourceProcess, message);
		}
	}
}