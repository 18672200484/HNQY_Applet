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
//using CMCS.Common.Entities.Inf;
using CMCS.Monitor.Win.Core;
using CMCS.Monitor.Win.Html;
using DevComponents.DotNetBar.Metro;
using Xilium.CefGlue.WindowsForms;
using CMCS.Monitor.Win.UserControls;
using CMCS.Common.Entities.AutoCupboard;
using CMCS.Common.Entities.Fuel;
using CMCS.DumblyConcealer.Tasks.PneumaticTransfer_XMJS.Entities;
using CMCS.DumblyConcealer;
//using CMCS.Common.Entities.AutoCupboard;

namespace CMCS.Monitor.Win.Frms
{
    public partial class FrmAutoCupboardPneumaticTransfer : MetroForm
    {
        private string green = "#00C000", red = "#FF0000", white = "#FFFFFF", gray = "#a6a8ab";
        public string[] strSignal = new string[] { "6mm破碎", "6mm缩分", "3mm破碎", "3mm缩分", "0.2mm破碎", "红外干燥", "6mm全水分样瓶", "3mm存查样样瓶", "0.2mm分析样样瓶", "0.2mm备查样样瓶", "总经理备查样瓶一", "总经理备查样瓶二", "总经理备查样瓶三", "弃料输送机", "上盖一体机自动" };
        public string[] strWaySignal = new string[] { "路径探测器36", "路径探测器37", "路径探测器38", "路径探测器39", "路径探测器3A", "动力风机" };
        public string[] strWaySignal2 = new string[] { "路径探测器1(36)", "路径探测器2(37)", "路径探测器3(38)", "路径探测器4(39)", "路径探测器5(3A)", "动力风机(08)" };
        /// <summary>
        /// 窗体唯一标识符
        /// </summary>
        public static string UniqueKey = "FrmAutoCupboardPneumaticTransferNew";

        CefWebBrowserEx cefWebBrowser = new CefWebBrowserEx();

        public FrmAutoCupboardPneumaticTransfer()
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
            cefWebBrowser.StartUrl = SelfVars.Url_NCGM_CYGQDCS;
            cefWebBrowser.Dock = DockStyle.Fill;
            cefWebBrowser.WebClient = new HomePageCefWebClient(cefWebBrowser);
            cefWebBrowser.LoadEnd += new EventHandler<LoadEndEventArgs>(cefWebBrowser_LoadEnd);
            panWebBrower.Controls.Add(cefWebBrowser);
        }

        void cefWebBrowser_LoadEnd(object sender, LoadEndEventArgs e)
        {
            timer1.Enabled = true;
        }

        private void FrmAutoCupboardPneumaticTransfer_Load(object sender, EventArgs e)
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

            string value = string.Empty, machineCode = string.Empty;
            List<HtmlDataItem> datas = new List<HtmlDataItem>();
            //List<CmcsEquInfHitch> equInfHitchs = new List<CmcsEquInfHitch>();

            #region 智能存样柜 #

            datas.Clear();
            List<InfCYGSam> lists = Dbers.GetInstance().SelfDber.Entities<InfCYGSam>(" where code is not null").ToList();//存样柜信息
            machineCode = GlobalVars.InterfaceType_NCGM_CYG;

            int zpcps1= int.Parse(commonDAO.GetSignalDataValue("#1智能存样柜", "中瓶已存瓶数"));
            int zpkcs1= int.Parse(commonDAO.GetSignalDataValue("#1智能存样柜", "中瓶空仓数"));
            int xpcps1= int.Parse(commonDAO.GetSignalDataValue("#1智能存样柜", "小瓶已存瓶数"));
            int xpkcs1 = int.Parse(commonDAO.GetSignalDataValue("#1智能存样柜", "小瓶空仓数"));

            int zpcps2 = int.Parse(commonDAO.GetSignalDataValue("#2智能存样柜", "中瓶已存瓶数"));
            int zpkcs2 = int.Parse(commonDAO.GetSignalDataValue("#2智能存样柜", "中瓶空仓数"));
            int xpcps2 = int.Parse(commonDAO.GetSignalDataValue("#2智能存样柜", "小瓶已存瓶数"));
            int xpkcs2 = int.Parse(commonDAO.GetSignalDataValue("#2智能存样柜", "小瓶空仓数"));

            int zcw = zpcps1 + zpkcs1 + xpcps1 + xpkcs1 + zpcps2 + zpkcs2 + xpcps2 + xpkcs2;
            int wccw = zpkcs1 + xpkcs1 + zpkcs2 + xpkcs2;
            int yccw = zpcps1 + xpcps1 + zpcps2 + xpcps2;
            datas.Add(new HtmlDataItem("总仓位", zcw.ToString(), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("未存仓位", wccw.ToString(), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("已存仓位", yccw.ToString(), eHtmlDataItemType.svg_text));
            //try
            //{
            //    int allhas = Convert.ToInt32(commonDAO.GetSignalDataValue(machineCode, "总仓位")) - Convert.ToInt32(commonDAO.GetSignalDataValue(machineCode, "未存仓位"));
            //    datas.Add(new HtmlDataItem("已存仓位", allhas.ToString(), eHtmlDataItemType.svg_text));
            //}
            //catch (Exception)
            //{
            //}
            decimal cyv =Math.Round(Convert.ToDecimal(Convert.ToDouble(yccw) / Convert.ToDouble(zcw) * 100),2);
            datas.Add(new HtmlDataItem("存样率", cyv + "%", eHtmlDataItemType.svg_text));


            datas.Add(new HtmlDataItem("6mm全水样", lists.Where(a => a.SamType == "6mm样瓶").ToList().Count.ToString(), eHtmlDataItemType.svg_text));
            //datas.Add(new HtmlDataItem("6mm总经理备查样", lists.Where(a => a.SamType == "6mm样瓶").ToList().Count.ToString(), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("3mm备查样", lists.Where(a => a.SamType == "3mm样瓶").ToList().Count.ToString(), eHtmlDataItemType.svg_text));
            int count = lists.Where(a => a.SamType == "0.2mm样瓶").ToList().Count;
            datas.Add(new HtmlDataItem("02mm备查样", lists.Where(a => a.SamType == "0.2mm样瓶").ToList().Count.ToString(), eHtmlDataItemType.svg_text));
            //datas.Add(new HtmlDataItem("样品扭转", commonDAO.GetSignalDataValue(machineCode, "样品扭转"), eHtmlDataItemType.svg_custom));

            machineCode = GlobalVars.InterfaceType_XMJS_QD;
            //气动传输路径信号
            for (int i = 0; i < strWaySignal.Length; i++)
            {
                string rulst = commonDAO.GetSignalDataValue(machineCode, strWaySignal2[i]);

                if (rulst == "1") {
                    datas.Add(new HtmlDataItem(strWaySignal[i], green, eHtmlDataItemType.svg_color));
                }
                else if (rulst == "2") {
                    datas.Add(new HtmlDataItem(strWaySignal[i], gray, eHtmlDataItemType.svg_color));
                }
                else if (rulst == "3") {
                    datas.Add(new HtmlDataItem(strWaySignal[i], red, eHtmlDataItemType.svg_color));
                }
                else if (rulst == "4") {
                    datas.Add(new HtmlDataItem(strWaySignal[i], green, eHtmlDataItemType.svg_blinkcolor));
                }
                
            }

            // 发送到页面
            cefWebBrowser.Browser.GetMainFrame().ExecuteJavaScript("requestData(" + Newtonsoft.Json.JsonConvert.SerializeObject(datas) + ");", "", 0);
            //

            //List<InfCYGSam> lists = Dbers.GetInstance().SelfDber.Entities<InfCYGSam>(" where code is not null").ToList();
            List<Tempsam> tempsams = new List<Tempsam>();
            List<TempGS> Temp = new List<TempGS>();
            DataTable dt = GetBianMaNeiRong("化验样样品类型");//获取样品类型的过期时间

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                TempGS lit = new TempGS();
                lit.Type = dt.Rows[i]["content"].ToString();
                lit.TmDay = int.Parse(dt.Rows[i]["remark"].ToString() == "" ? "0" : dt.Rows[i]["remark"].ToString());
                Temp.Add(lit);
            }
            foreach (var itemCYG in new String[] { "#1智能存样柜", "#2智能存样柜" })
            {
                foreach (var itembox in new String[] { "A", "B" })
                {
                    foreach (var itempage in new int[] { 1, 2, 3, 4, 5 })
                    {
                        for (int rowindex = 12; rowindex > 0; rowindex--)
                        {
                            for (int colindex = itempage * 11 - 10; colindex <= itempage * 11; colindex++)
                            {
                                Tempsam tempsam = new Tempsam();
                                InfCYGSam cmcscygsam = lists.Where(a => a.Place == itembox + rowindex.ToString().PadLeft(2, '0') + colindex.ToString().PadLeft(2, '0') && a.MachineCode == itemCYG).FirstOrDefault();
                                tempsam.Name = itembox + rowindex.ToString().PadLeft(2, '0') + colindex.ToString().PadLeft(2, '0');
                                if (cmcscygsam != null)
                                {
                                    int day = 0;
                                    List<TempGS> list = Temp.Where(a => a.Type == cmcscygsam.SamType).ToList();
                                    if (list.Count > 0)
                                    {
                                        day = list.FirstOrDefault().TmDay;
                                    }
                                    if (cmcscygsam.CreateDate.AddDays(day) > DateTime.Now)
                                        tempsam.Type = "1";
                                    else
                                        tempsam.Type = "0";
                                }
                                tempsams.Add(tempsam);
                            }
                        }
                    }
                }
            }
            cefWebBrowser.Browser.GetMainFrame().ExecuteJavaScript("LoadSamInfo(" + Newtonsoft.Json.JsonConvert.SerializeObject(tempsams) + ");", "", 0);

            //出样信息
            List<CmcsRCMakeDetail> listMakerRecord = Dbers.GetInstance().SelfDber.Entities<CmcsRCMakeDetail>("where barrelcode  is not null and barreltime is not null order by barreltime desc");
            //listMakerRecord.Where(a => a.Status != "");
            List<CmcsCMDDetail> listMakerRecords = new List<CmcsCMDDetail>();
            string makeid = "";
            for (int i = 0; i < 8; i++)
            {

                if (makeid == listMakerRecord[i].MakeId || makeid=="")
                {
                    CmcsCMDDetail entity = new CmcsCMDDetail();
                    entity.SamType = listMakerRecord[i].SampleType;
                    entity.Code = listMakerRecord[i].BarrelCode;



                    InfQDBill InfQDBill = DcDbers.GetInstance().PneumaticTransfer_Dber.Entity<InfQDBill>("where SampleId=@SampleId", new { SampleId = listMakerRecord[i].BarrelCode });
                    if (InfQDBill == null)
                    {
                        entity.Status = "等待传输";
                        entity.CreateDate = new DateTime(1990, 1, 1);
                    }
                    else
                    {
                        entity.Status = getChuanShuZhuangTai(InfQDBill.DataStatus);
                        entity.CreateDate = InfQDBill.Send_Time;

                    }
                    listMakerRecords.Add(entity);
                }
                else
                {

                    listMakerRecords.Add(new CmcsCMDDetail() { CreateDate = new DateTime(1990, 1, 1), SamType = "", Code = "", Status = "" });
                }

                makeid = listMakerRecord[i].MakeId;

            }
            listMakerRecords = listMakerRecords.OrderByDescending(a => a.CreateDate).ToList();
            cefWebBrowser.Browser.GetMainFrame().ExecuteJavaScript("LoadSampleInfo(" + Newtonsoft.Json.JsonConvert.SerializeObject(listMakerRecords.Select(a => new { UpdateTime = a.CreateDate.Year < 2000 ? "" : a.CreateDate.ToString("yyyy-MM-dd HH:mm"), Code = a.Code, SamType = a.SamType == null ? "" : a.SamType, Status = a.Status == null ? "" : a.Status })) + ");", "", 0);

            #endregion
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

        /// <summary>
        /// 第三方样品类型 转换成 集中管控样品类型
        /// </summary>
        /// <param name="ypType">第三方样品类型</param>
        /// <returns></returns>
        public string ConvertToCmcsYpType(string ypType)
        {
            if (ypType == "1")
                return "6mm全水样";
            if (ypType == "2")
                return "3mm备查样";
            if (ypType == "3")
                return "0.2mm分析样";
            if (ypType == "4")
                return "0.2mm备查样";
            if (ypType == "5")
                return "6mm总经理备查样1";
            if (ypType == "6")
                return "6mm总经理备查样2";
            if (ypType == "7")
                return "6mm总经理备查样3";
            else
                return "未知";
        }
        #region
        /// <summary>
        /// 根据编码模块类别名称获取编码类容
        /// </summary>
        /// <param name="YangpinType">类别名称</param>
        /// <returns></returns>
        public DataTable GetBianMaNeiRong(string YangpinType)
        {
            string sql = "select a.content,a.remark from syssmtbcodecontent a left join syssmtbcodekind b on a.kindid=b.id where b.kind='{0}'";
            sql = string.Format(sql, YangpinType);
            return Dbers.GetInstance().SelfDber.ExecuteDataTable(sql);
        }
        #endregion

        [Serializable()]
        public class Tempsam
        {
            public string Name { get; set; }
            public string Type { get; set; }
        }


        [Serializable()]
        public class TempGS
        {
            public string Type { get; set; }
            public int TmDay { get; set; }
        }

        String nullif(String st)
        {
            if (String.IsNullOrEmpty(st))
                return null;
            else
                return st;
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
        private void btnRefreshPage_Click(object sender, EventArgs e)
        {
            cefWebBrowser.Browser.Reload();
        }

        /// <summary>
        /// 刷新数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefreshData_Click(object sender, EventArgs e)
        {
            RequestData();
        }

        private void btnHistory_Click(object sender, EventArgs e)
        {
            FrmAutoMakerRecord frm = new FrmAutoMakerRecord();
            frm.ShowDialog();
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
            cefWebBrowser.Browser.GetMainFrame().ExecuteJavaScript("test1();", "", 0);
        }
    }
}
