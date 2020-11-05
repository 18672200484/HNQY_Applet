using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using CMCS.Monitor.Win.Utilities;
using System.Xml;
using System.Reflection;
using System.IO;
using DHNetSDK;
using DevComponents.DotNetBar.Metro;
using System.Linq;
using CMCS.Common.DAO;
using CMCS.Common.Utilities;


namespace CMCS.Monitor.Win.Frms
{
    public partial class FrmVidioPreview : MetroAppForm
    {
        /// <summary>
        /// 窗体唯一标识符
        /// </summary>
        public static string UniqueKey = "FrmVidioPreview";
        CommonDAO commonDAO = CommonDAO.GetInstance();
        public FrmVidioPreview()
        {
            InitializeComponent();
        }

        TableLayoutPanelCellPosition pos;
        List<VideoEntity> listVideo = new List<VideoEntity>();
        //全局信息
        string refMessage = string.Empty;

        private void FrmVidioPreview_Load(object sender, EventArgs e)
        {
            //加载视频实体
            LoadVideoEntity();
            //初始化海康视频
            //CHCNetSDKUtil.DVR_Init(ref refMessage);
            //初始化大华视频
            //CHCNetSDKUtil.DHDVR_Init(ref refMessage);
            //初始化视频窗体
            InitializeVideo(9);
            //加载视频
            LoginVideo();
            //预览视频
            PreviewVideo();
        }

        #region 其他
        public void LoadVideoEntity()
        {
            XmlDocument xdoc = new XmlDocument();
            xdoc.Load(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.xml"));
            List<VideoEntity> list = new List<VideoEntity>();
            
            foreach (XmlNode xnItem in xdoc.SelectNodes("/root/VideoManage/VideoItem"))
            {
                VideoEntity entity = new VideoEntity();
                PropertyInfo[] propertyinfos = entity.GetType().GetProperties();

                foreach (PropertyInfo propertyinfo in propertyinfos)
                {
                    if (xnItem.Attributes[propertyinfo.Name] != null)
                        propertyinfo.SetValue(entity, Convert.ChangeType(xnItem.Attributes[propertyinfo.Name].Value, propertyinfo.PropertyType), null);
                }
                entity.UserId = -1;
                entity.RealHandle = -1;
                list.Add(entity);
            }
            listVideo = list.OrderBy(a => a.Order).ToList();
        }
        #endregion

        /// <summary>
        /// 调整画面布局
        /// </summary>
        /// <param name="videoNum"></param>
        /// <returns></returns>
        public bool InitializeVideo(int videoNum)
        {
            //计算行列
            int rowcol;
            if (videoNum <= 0 || !int.TryParse(Math.Sqrt(videoNum).ToString(), out rowcol))
            {
                return false;
            }
            //计算宽高
            int Width = (int)(MainPanel.Width / rowcol);
            int Height = (int)(MainPanel.Height / rowcol);

            //重新设置表格
            //MainPanel为TableLayoutPanel控件
            MainPanel.Controls.Clear();
            MainPanel.RowCount = MainPanel.ColumnCount = rowcol;
            MainPanel.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            MainPanel.Refresh();
            for (int i = 0; i < MainPanel.ColumnStyles.Count; i++)
            {
                MainPanel.ColumnStyles[i].SizeType = SizeType.Absolute;
                MainPanel.ColumnStyles[i].Width = Width;
            }
            for (int i = 0; i < MainPanel.RowStyles.Count; i++)
            {
                MainPanel.RowStyles[i].SizeType = SizeType.Absolute;
                MainPanel.RowStyles[i].Height = Height;
            }
            //添加控件
            for (int i = 0; i < videoNum; i++)
            {
                PictureBox pVideo = new PictureBox();
                pVideo.Padding = pVideo.Margin = new System.Windows.Forms.Padding(0);
                pVideo.Name = "pVideo" + i.ToString();
                pVideo.Width = Width;
                pVideo.Height = Height;
                pVideo.Dock = DockStyle.Fill;
                //pVideo.BackgroundImage = Resources.bg;
                //pVideo.MouseDown += new MouseEventHandler(Pic_MouseDown);
                //pVideo.MouseMove += new MouseEventHandler(Pic_MouseMove);
                pVideo.BackgroundImageLayout = ImageLayout.Stretch;
                //pVideo.Click += new EventHandler(pVideo_Click);
                pVideo.DoubleClick += new EventHandler(pVideo_DoubleClick);

                MainPanel.Controls.Add(pVideo, i % rowcol, i / rowcol);
            }
            this.Controls.Add(MainPanel);
            return true;
        }


        #region 移动窗体 移动窗口
        //private Point _mousePoint;
        //private int topA(Control cc)
        //{
        //    if (cc == null || cc == this) return 0;
        //    if (cc.Parent == null || cc.Parent == this)
        //        return cc.Top;
        //    else
        //        return topA(cc.Parent) + cc.Top;
        //}

        //private int leftA(Control cc)
        //{
        //    if (cc == null || cc == this) return 0;
        //    if (cc.Parent == null || cc.Parent == this)
        //        return cc.Left;
        //    else
        //        return leftA(cc.Parent) + cc.Left;
        //}

        //private void Pic_MouseDown(object sender, MouseEventArgs e)
        //{
        //    Control cc = (Control)sender;
        //    if (e.Button == MouseButtons.Left)
        //    {
        //        _mousePoint.X = e.X + leftA(cc);
        //        _mousePoint.Y = e.Y + topA(cc);
        //    }
        //}

        //private void Pic_MouseMove(object sender, MouseEventArgs e)
        //{
        //    if (e.Button == MouseButtons.Left)
        //    {
        //        Top = MousePosition.Y - _mousePoint.Y;
        //        Left = MousePosition.X - _mousePoint.X;
        //    }
        //}
        #endregion

        /// <summary>
        /// 加载视频
        /// </summary>
        /// <param name="listVideo"></param>
        public void LoginVideo()
        {
            //登录设备
            foreach (VideoEntity item in listVideo)
            {
                if (item.UserId < 0)
                {
                    if (item.DeviceFactory == "海康")
                    {
                        CHCNetSDK.NET_DVR_DEVICEINFO_V30 DeviceInfo = new CHCNetSDK.NET_DVR_DEVICEINFO_V30();
                        //登录设备 Login the device
                        item.UserId = CHCNetSDK.NET_DVR_Login_V30(item.Ip, item.PortNumber, item.UserName, item.Password, ref DeviceInfo);
                    }
                    else
                    {
                        NET_DEVICEINFO DeviceInfo = new NET_DEVICEINFO();
                        int error = 0;
                        item.UserId = NETClient.CLIENT_Login(item.Ip, (ushort)item.PortNumber, item.UserName, item.Password, out  DeviceInfo, out error);
                    }
                }
            }
        }

        /// <summary>
        /// 预览视频
        /// </summary>
        public void PreviewVideo()
        {
            //预览设备
            int i = 0;
     
            string strIP = commonDAO.GetAppletConfigString("公共配置", "视频服务器IP地址");
            string strPort = commonDAO.GetAppletConfigString("公共配置", "视频服务器端口号");
               IntPtr nPDLLHandle = (IntPtr)0;
                    IntPtr result1 = DHSDK.DPSDK_Create(DHSDK.dpsdk_sdk_type_e.DPSDK_CORE_SDK_SERVER, ref nPDLLHandle);//初始化数据交互接口
                    IntPtr result2 = DHSDK.DPSDK_InitExt();//初始化解码播放接口
                    if (result1 == (IntPtr)0 && result2 == (IntPtr)0)
                    {
                        if (DHSDK.Logion(strIP, int.Parse(strPort), "system", "admin123", nPDLLHandle))
                        {
                            foreach (VideoEntity item in listVideo.Where(a => a.DeviceFactory == "视频窗口一"))
                            {
                                if (item.IsPreview)
                                {
                                    //if (item.UserId > 0)
                                    //{
                                        if (item.RealHandle < 0)
                                        {
                                            IntPtr intPtr = MainPanel.Controls["pVideo" + i.ToString()].Handle;//预览窗口
                                            ////预览
                                            //item.RealHandle = NETClient.CLIENT_RealPlay(item.UserId, item.Channel, intPtr);
                                            //bool result = NETClient.NetSetSecurityKey(item.RealHandle, "SPL17THALES00000");// Set Aes Security Key
                                            IntPtr realseq = default(IntPtr);
                                            string szCameraId1 = item.Channel;
                                            if (DHSDK.StartPreview(intPtr, szCameraId1, nPDLLHandle, realseq))
                                            {
                                                MainPanel.Controls["pVideo" + i.ToString()].Refresh();
                                            }
                                        }
                                    //}
                                }
                                i++;
                            }
                        }
                    }
        }


        public void pVideo_DoubleClick(object sender, EventArgs e)
        {
            PictureBox pVideo = (PictureBox)sender;
            if (MainPanel.GetColumnSpan(pVideo) == 1)
            {
                //隐藏其它控件
                foreach (Control ctr in MainPanel.Controls)
                {
                    if (ctr.Name != pVideo.Name)
                        ctr.Visible = false;
                }
                pos = MainPanel.GetPositionFromControl(pVideo);
                MainPanel.SetCellPosition(pVideo, new TableLayoutPanelCellPosition(0, 0));
                MainPanel.SetRowSpan(pVideo, MainPanel.RowCount);
                MainPanel.SetColumnSpan(pVideo, MainPanel.ColumnCount);
            }
            else
            {
                //显示所有控件
                foreach (Control ctr in MainPanel.Controls)
                {
                    ctr.Visible = true;
                }
                MainPanel.SetCellPosition(pVideo, pos);
                MainPanel.SetRowSpan(pVideo, 1);
                MainPanel.SetColumnSpan(pVideo, 1);
            }
        }
    }
}