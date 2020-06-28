using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CMCS.Monitor.Win.Core;
using Xilium.CefGlue.WindowsForms;
using CMCS.Common;
using CMCS.Common.DAO;
using CMCS.Monitor.Win.Html;
using CMCS.Common.Entities;
using DevComponents.DotNetBar.Metro;
using CMCS.Monitor.DAO;
using CMCS.Common.Entities.Fuel;

namespace CMCS.Monitor.Win.Frms
{
    public partial class FrmAssayManage : MetroForm
    {
        /// <summary>
        /// 窗体唯一标识符
        /// </summary>
        public static string UniqueKey = "FrmAssayManage";

        CefWebBrowser cefWebBrowser = new CefWebBrowser();

        public FrmAssayManage()
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

            cefWebBrowser.StartUrl = SelfVars.Url_AssayManage;
            cefWebBrowser.Dock = DockStyle.Fill;
            cefWebBrowser.LoadEnd += new EventHandler<LoadEndEventArgs>(cefWebBrowser_LoadEnd);
            panWebBrower.Controls.Add(cefWebBrowser);
        }

        void cefWebBrowser_LoadEnd(object sender, LoadEndEventArgs e)
        {
            timer1.Enabled = true;
        }

        private void FrmAssayManage_Load(object sender, EventArgs e)
        {
            FormInit();
        }
        /// <summary>
        /// 请求数据
        /// </summary>
        void RequestData()
        {
            CommonDAO commonDAO = CommonDAO.GetInstance();

            string value = string.Empty, machineCode = string.Empty;
            List<HtmlDataItem> datas = new List<HtmlDataItem>();

            #region 化验室网络管理

            datas.Clear();
            machineCode = GlobalVars.MachineCode_AssayManage;

            #region 取化验相关数据
            int total = Convert.ToInt32(commonDAO.GetSignalDataValueDouble(machineCode, "化验总数_数量"));
            decimal yihuayan = Convert.ToDecimal(commonDAO.GetSignalDataValueDouble(machineCode, "化验完成_数量"));
            decimal huayanzhong = Convert.ToDecimal(commonDAO.GetSignalDataValueDouble(machineCode, "化验中_数量"));
            decimal daihuayan = Convert.ToDecimal(commonDAO.GetSignalDataValueDouble(machineCode, "待化验数_数量"));
            decimal yishenhe = Convert.ToDecimal(commonDAO.GetSignalDataValueDouble(machineCode, "已审核_数量"));
            decimal weishenhe = Convert.ToDecimal(commonDAO.GetSignalDataValueDouble(machineCode, "未审核_数量"));

            datas.Add(new HtmlDataItem("化验完成_Line", yihuayan.ToString(), eHtmlDataItemType.svg_width));
            datas.Add(new HtmlDataItem("化验完成_数量", yihuayan + " 个", eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("化验中_Line", huayanzhong.ToString(), eHtmlDataItemType.svg_width));
            datas.Add(new HtmlDataItem("化验中_数量", huayanzhong + " 个", eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("待化验数_Line", daihuayan.ToString(), eHtmlDataItemType.svg_width));
            datas.Add(new HtmlDataItem("待化验数_数量", daihuayan + " 个", eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("已审核_数量", commonDAO.GetSignalDataValueDouble(machineCode, "已审核_数量") + " 个", eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("未审核_数量", commonDAO.GetSignalDataValueDouble(machineCode, "未审核_数量") + " 个", eHtmlDataItemType.svg_text));
            #endregion

            #region 取化验设备当天化验条数

            //datas.Add(new HtmlDataItem("量热仪1_运行状态", "#00A551", eHtmlDataItemType.svg_color));
            //datas.Add(new HtmlDataItem("量热仪2_运行状态", "#00A551", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("1#量热仪_数量", commonDAO.GetSignalDataValueDouble(machineCode, "1#量热仪_数量") + " 个", eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("2#量热仪_数量", commonDAO.GetSignalDataValueDouble(machineCode, "2#量热仪_数量") + " 个", eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("3#量热仪_数量", commonDAO.GetSignalDataValueDouble(machineCode, "3#量热仪_数量") + " 个", eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("4#量热仪_数量", commonDAO.GetSignalDataValueDouble(machineCode, "4#量热仪_数量") + " 个", eHtmlDataItemType.svg_text));

            datas.Add(new HtmlDataItem("1#入炉量热仪_数量", commonDAO.GetSignalDataValueDouble(machineCode, "1#入炉量热仪_数量") + " 个", eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("2#入炉量热仪_数量", commonDAO.GetSignalDataValueDouble(machineCode, "2#入炉量热仪_数量") + " 个", eHtmlDataItemType.svg_text));

            //datas.Add(new HtmlDataItem("光波水分仪1_运行状态", "#00A551", eHtmlDataItemType.svg_color));
            //datas.Add(new HtmlDataItem("光波水分仪2_运行状态", "#00A551", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("1#水分仪_数量", commonDAO.GetSignalDataValueDouble(machineCode, "1#水分仪_数量") + " 个", eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("2#水分仪_数量", commonDAO.GetSignalDataValueDouble(machineCode, "2#水分仪_数量") + " 个", eHtmlDataItemType.svg_text));

            datas.Add(new HtmlDataItem("1#入炉水分仪_数量", commonDAO.GetSignalDataValueDouble(machineCode, "1#入炉水分仪_数量") + " 个", eHtmlDataItemType.svg_text));
            //datas.Add(new HtmlDataItem("3#水分仪_数量", commonDAO.GetSignalDataValueDouble(machineCode, "3#水分仪_数量") + " 个", eHtmlDataItemType.svg_text));
            //datas.Add(new HtmlDataItem("测硫仪1_运行状态", "#00A551", eHtmlDataItemType.svg_color));
            //datas.Add(new HtmlDataItem("测硫仪2_运行状态", "#00A551", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("1#测硫仪_数量", commonDAO.GetSignalDataValueDouble(machineCode, "1#测硫仪_数量") + " 个", eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("2#测硫仪_数量", commonDAO.GetSignalDataValueDouble(machineCode, "2#测硫仪_数量") + " 个", eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("3#测硫仪_数量", commonDAO.GetSignalDataValueDouble(machineCode, "3#测硫仪_数量") + " 个", eHtmlDataItemType.svg_text));

            datas.Add(new HtmlDataItem("1#入炉测硫仪_数量", commonDAO.GetSignalDataValueDouble(machineCode, "1#入炉测硫仪_数量") + " 个", eHtmlDataItemType.svg_text));
            //datas.Add(new HtmlDataItem("工分仪1_运行状态", "#00A551", eHtmlDataItemType.svg_color));
            //datas.Add(new HtmlDataItem("工分仪2_运行状态", "#00A551", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("1#工分仪_数量", commonDAO.GetSignalDataValueDouble(machineCode, "1#工分仪_数量") + " 个", eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("2#工分仪_数量", commonDAO.GetSignalDataValueDouble(machineCode, "2#工分仪_数量") + " 个", eHtmlDataItemType.svg_text));

            datas.Add(new HtmlDataItem("1#入炉工分仪_数量", commonDAO.GetSignalDataValueDouble(machineCode, "1#入炉工分仪_数量") + " 个", eHtmlDataItemType.svg_text));
            #endregion

            #region 取温湿度仪数据
            List<Fultblaboratoryhumiture> lists = Dbers.GetInstance().SelfDber.Entities<Fultblaboratoryhumiture>(" where 1=1").ToList().OrderByDescending(a=>a.CreateDate).ToList();//存样柜信息


            if (lists.Count > 0)
            {
                datas.Add(new HtmlDataItem("入厂量热室温度", "温度:" + lists[0].INFA_HEAT_TEMP + "℃", eHtmlDataItemType.svg_text));
                datas.Add(new HtmlDataItem("入厂量热室湿度", "湿度:" + lists[0].INFA_HEAT_HUMI + "%", eHtmlDataItemType.svg_text));
                datas.Add(new HtmlDataItem("入炉量热室温度", "温度:" + lists[0].INFU_HEAT_TEMP + "℃", eHtmlDataItemType.svg_text));
                datas.Add(new HtmlDataItem("入炉量热室湿度", "湿度:" + lists[0].INFU_HEAT_HUMI + "%", eHtmlDataItemType.svg_text));


                datas.Add(new HtmlDataItem("入厂水分室温度", "温度:" + lists[0].INFA_MOISTURE_TEMP + "℃", eHtmlDataItemType.svg_text));
                datas.Add(new HtmlDataItem("入厂水分室湿度", "湿度:" + lists[0].INFA_MOISTURE_HUMI + "%", eHtmlDataItemType.svg_text));
                datas.Add(new HtmlDataItem("入炉水分室温度", "温度:" + lists[0].INFU_MOISTURE_TEMP + "℃", eHtmlDataItemType.svg_text));
                datas.Add(new HtmlDataItem("入炉水分室湿度", "湿度:" + lists[0].INFU_MOISTURE_HUMI + "%", eHtmlDataItemType.svg_text));


                datas.Add(new HtmlDataItem("入厂工分室温度", "温度:" + lists[0].INFA_PROXIMATE_TEMP + "℃", eHtmlDataItemType.svg_text));
                datas.Add(new HtmlDataItem("入厂工分室湿度", "湿度:" + lists[0].INFA_PROXIMATE_HUMI + "%", eHtmlDataItemType.svg_text));
                datas.Add(new HtmlDataItem("入炉工分室温度", "温度:" + lists[0].INFU_PROXIMATE_TEMP + "℃", eHtmlDataItemType.svg_text));
                datas.Add(new HtmlDataItem("入炉工分室湿度", "湿度:" + lists[0].INFU_PROXIMATE_HUMI + "%", eHtmlDataItemType.svg_text));


                datas.Add(new HtmlDataItem("入厂测硫室温度", "温度:" + lists[0].INFA_SULFUR_TEMP + "℃", eHtmlDataItemType.svg_text));
                datas.Add(new HtmlDataItem("入厂测硫室湿度", "湿度:" + lists[0].INFA_SULFUR_HUMI + "%", eHtmlDataItemType.svg_text));
                datas.Add(new HtmlDataItem("入炉测硫室温度", "温度:" + lists[0].INFU_SULFUR_TEMP + "℃", eHtmlDataItemType.svg_text));
                datas.Add(new HtmlDataItem("入炉测硫室湿度", "湿度:" + lists[0].INFU_SULFUR_HUMI + "%", eHtmlDataItemType.svg_text));

            }
            else {

                datas.Add(new HtmlDataItem("入厂量热室温度", "温度:--℃", eHtmlDataItemType.svg_text));
                datas.Add(new HtmlDataItem("入厂量热室湿度", "湿度:--%", eHtmlDataItemType.svg_text));
                datas.Add(new HtmlDataItem("入炉量热室温度", "温度:--℃", eHtmlDataItemType.svg_text));
                datas.Add(new HtmlDataItem("入炉量热室湿度", "湿度:--%", eHtmlDataItemType.svg_text));


                datas.Add(new HtmlDataItem("入厂水分室温度", "温度:--℃", eHtmlDataItemType.svg_text));
                datas.Add(new HtmlDataItem("入厂水分室湿度", "湿度:--%", eHtmlDataItemType.svg_text));
                datas.Add(new HtmlDataItem("入炉水分室温度", "温度:--℃", eHtmlDataItemType.svg_text));
                datas.Add(new HtmlDataItem("入炉水分室湿度", "湿度:--%", eHtmlDataItemType.svg_text));


                datas.Add(new HtmlDataItem("入厂工分室温度", "温度:--℃", eHtmlDataItemType.svg_text));
                datas.Add(new HtmlDataItem("入厂工分室湿度", "湿度:--%", eHtmlDataItemType.svg_text));
                datas.Add(new HtmlDataItem("入炉工分室温度", "温度:--℃", eHtmlDataItemType.svg_text));
                datas.Add(new HtmlDataItem("入炉工分室湿度", "湿度:--%", eHtmlDataItemType.svg_text));


                datas.Add(new HtmlDataItem("入厂测硫室温度", "温度:--℃", eHtmlDataItemType.svg_text));
                datas.Add(new HtmlDataItem("入厂测硫室湿度", "湿度:--%", eHtmlDataItemType.svg_text));
                datas.Add(new HtmlDataItem("入炉测硫室温度", "温度:--℃", eHtmlDataItemType.svg_text));
                datas.Add(new HtmlDataItem("入炉测硫室湿度", "湿度:--%", eHtmlDataItemType.svg_text));

            }
            //datas.Add(new HtmlDataItem("水分室温度", Math.Round(commonDAO.GetSignalDataValueDouble(machineCode, "水分测定室_温度"), 1) + " ℃", eHtmlDataItemType.svg_text));
            //datas.Add(new HtmlDataItem("水分室湿度", commonDAO.GetSignalDataValueDouble(machineCode, "水分测定室_湿度") + " %", eHtmlDataItemType.svg_text));
            //if (commonDAO.GetSignalDataValueDouble(machineCode, "水分测定室_温度") < 57) datas.Add(new HtmlDataItem("水分室温度", "#008200", eHtmlDataItemType.svg_color));
            //if (commonDAO.GetSignalDataValueDouble(machineCode, "水分测定室_温度") >= 57) datas.Add(new HtmlDataItem("水分室温度", "Red", eHtmlDataItemType.svg_color));

            //datas.Add(new HtmlDataItem("工分室温度", Math.Round(commonDAO.GetSignalDataValueDouble(machineCode, "工业分析仪_温度"), 1) + " ℃", eHtmlDataItemType.svg_text));
            //datas.Add(new HtmlDataItem("工分室湿度", commonDAO.GetSignalDataValueDouble(machineCode, "工业分析仪_湿度") + " %", eHtmlDataItemType.svg_text));
            //if (commonDAO.GetSignalDataValueDouble(machineCode, "工业分析仪_温度") < 57) datas.Add(new HtmlDataItem("工分室温度", "#008200", eHtmlDataItemType.svg_color));
            //if (commonDAO.GetSignalDataValueDouble(machineCode, "工业分析仪_温度") >= 57) datas.Add(new HtmlDataItem("工分室温度", "Red", eHtmlDataItemType.svg_color));

            //datas.Add(new HtmlDataItem("测硫室温度", Math.Round(commonDAO.GetSignalDataValueDouble(machineCode, "测硫室_温度"), 1) + " ℃", eHtmlDataItemType.svg_text));
            //datas.Add(new HtmlDataItem("测硫室湿度", commonDAO.GetSignalDataValueDouble(machineCode, "测硫室_湿度") + " %", eHtmlDataItemType.svg_text));
            //if (commonDAO.GetSignalDataValueDouble(machineCode, "测硫室_温度") < 57) datas.Add(new HtmlDataItem("测硫室温度", "#008200", eHtmlDataItemType.svg_color));
            //if (commonDAO.GetSignalDataValueDouble(machineCode, "测硫室_温度") >= 57) datas.Add(new HtmlDataItem("测硫室温度", "Red", eHtmlDataItemType.svg_color));

            //datas.Add(new HtmlDataItem("量热室温度", Math.Round(commonDAO.GetSignalDataValueDouble(machineCode, "测热室_温度"), 1) + " ℃", eHtmlDataItemType.svg_text));
            //datas.Add(new HtmlDataItem("量热室湿度", commonDAO.GetSignalDataValueDouble(machineCode, "测热室_温度") + " %", eHtmlDataItemType.svg_text));
            //if (commonDAO.GetSignalDataValueDouble(machineCode, "测热室_温度") < 57) datas.Add(new HtmlDataItem("量热室温度", "#008200", eHtmlDataItemType.svg_color));
            //if (commonDAO.GetSignalDataValueDouble(machineCode, "测热室_温度") >= 57) datas.Add(new HtmlDataItem("量热室温度", "Red", eHtmlDataItemType.svg_color));
            #endregion

            #endregion

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
    }
}
