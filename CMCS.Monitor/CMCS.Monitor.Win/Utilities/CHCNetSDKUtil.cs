using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DHNetSDK;
using DHPlaySDK;

namespace CMCS.Monitor.Win.Utilities
{
    public class CHCNetSDKUtil
    {
        //初始化状态
        public static bool m_bInitSDK = false;
        public static bool m_bInitSDKDH = false;

        //登录用户ID    非-1 表示已登录
        public static Int32 m_lUserID = -1;
        public static Int32 m_lUserIDDH = -1;

        //1表示失败，其他值作为NET_DVR_StopRealPlay等函数的句柄参数 非-1 表示正在预览
        public static Int32 m_lRealHandle = -1;
        public static Int32 m_lRealHandleDH = -1;

        //错误代码
        static uint iLastErr = 0;
        static int iLastErrDH = 0;

        /// <summary>
        /// 异常表
        /// </summary>
        static Dictionary<uint, string> _ErrorTable = new Dictionary<uint, string>();
        static Dictionary<uint, string> _ErrorTableDH = new Dictionary<uint, string>();

        public CHCNetSDKUtil()
        {
            #region 海康错误信息查询表
            _ErrorTable.Add(1, "用户名密码错误。注册时输入的用户名或者密码错误。");
            _ErrorTable.Add(2, "权限不足。该注册用户没有权限执行当前对设备的操作，可以与远程用户参数配置做对比。");
            _ErrorTable.Add(3, "SDK 未初始化。");
            _ErrorTable.Add(4, "通道号错误。设备没有对应的通道号。");
            _ErrorTable.Add(5, "连接到设备的用户个数超过最大。");
            _ErrorTable.Add(6, "版本不匹配。SDK 和设备的版本不匹配。");
            _ErrorTable.Add(7, "连接设备失败。设备不在线或网络原因引起的连接超时等。");
            _ErrorTable.Add(8, "向设备发送失败。");
            _ErrorTable.Add(9, "从设备接收数据失败。");
            _ErrorTable.Add(10, "从设备接收数据超时。");
            _ErrorTable.Add(11, "传送的数据有误。发送给设备或者从设备接收到的数据错误，如远程参数配置时输入设备不支持的值。");
            #endregion

            #region 大华错误信息查询表
            _ErrorTableDH.Add(0x80000000 | 1, "WindowsSystem错误");
            _ErrorTableDH.Add(0x80000000 | 2, "网络错误，可能是因为网络超时");
            _ErrorTableDH.Add(0x80000000 | 3, "设备协议不匹配");
            _ErrorTableDH.Add(0x80000000 | 4, "句柄是无效的");
            _ErrorTableDH.Add(0x80000000 | 5, "无法打开通道");
            _ErrorTableDH.Add(0x80000000 | 6, "无法关闭通道");
            _ErrorTableDH.Add(0x80000000 | 7, "用户参数是不合法的");
            _ErrorTableDH.Add(0x80000000 | 8, "SDK初始化错误");
            _ErrorTableDH.Add(0x80000000 | 9, "SDK清理错误");
            _ErrorTableDH.Add(0x80000000 | 10, "应用程序渲染的资源错误");
            _ErrorTableDH.Add(0x80000000 | 11, "打开解码错误");
            _ErrorTableDH.Add(0x80000000 | 12, "关闭解码库错误");
            _ErrorTableDH.Add(0x80000000 | 29, "CLientSDK未初始化");
            _ErrorTableDH.Add(0x80000000 | 47, "无法获取配置位置：预览参数");
            _ErrorTableDH.Add(0x80000000 | 100, "密码不正确");
            _ErrorTableDH.Add(0x80000000 | 101, "帐户不存在");
            _ErrorTableDH.Add(0x80000000 | 102, "登录超时等待回归");
            _ErrorTableDH.Add(0x80000000 | 103, "帐户已登录");
            _ErrorTableDH.Add(0x80000000 | 104, "账户已被锁定");
            _ErrorTableDH.Add(0x80000000 | 108, "网络连接失败");
            #endregion
        }

        #region 海康
        public static bool DVR_Init(ref string returnStr)
        {
            m_bInitSDK = CHCNetSDK.NET_DVR_Init();
            if (m_bInitSDK == false)
            {
                returnStr = "NET_DVR初始化错误!";
                return false;
            }
            return true;
        }

        public static void DVR_Close()
        {
            //停止预览 Stop live view 
            if (m_lRealHandle >= 0)
            {
                CHCNetSDK.NET_DVR_StopRealPlay(m_lRealHandle);  //停止预览
                m_lRealHandle = -1;
            }

            //注销登录 Logout the device
            if (m_lUserID >= 0)
            {
                CHCNetSDK.NET_DVR_Logout(m_lUserID);     //退出用户
                m_lUserID = -1;
            }

            CHCNetSDK.NET_DVR_Cleanup();    //释放SDK资源
        }

        public static bool LoginUser(string DVRIPAddress, Int32 DVRPortNumber, string DVRUserName, string DVRPassword, ref string returnStr)
        {
            if (m_lUserID < 0)
            {
                CHCNetSDK.NET_DVR_DEVICEINFO_V30 DeviceInfo = new CHCNetSDK.NET_DVR_DEVICEINFO_V30();

                //登录设备 Login the device
                m_lUserID = CHCNetSDK.NET_DVR_Login_V30(DVRIPAddress, DVRPortNumber, DVRUserName, DVRPassword, ref DeviceInfo);
                if (m_lUserID < 0)
                {
                    iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                    if (_ErrorTable.ContainsKey(iLastErr))
                        returnStr = _ErrorTable[iLastErr];
                    else
                        returnStr = "登录失败, 错误编号 " + iLastErr; //登录失败，输出错误号
                    return false;
                }
                else
                    return true;
            }
            else
            {
                returnStr = "该用户已登录,请先注销";
                return false;
            }
        }

        public static bool LogoutUser(ref string returnStr)
        {
            if (m_lUserID >= 0)
            {
                if (!CHCNetSDK.NET_DVR_Logout(m_lUserID))
                {
                    iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                    if (_ErrorTable.ContainsKey(iLastErr))
                        returnStr = _ErrorTable[iLastErr];
                    else
                        returnStr = "注销失败, 错误编号 " + iLastErr; //登录失败，输出错误号
                    return false;
                }
            }
            m_lUserID = -1;
            returnStr = "注销成功";
            return true;
        }

        public static bool StartPreview(ref string returnStr, Control pnlPreview, int Channel)
        {
            if (m_lRealHandle < 0)
            {
                CHCNetSDK.NET_DVR_PREVIEWINFO lpPreviewInfo = new CHCNetSDK.NET_DVR_PREVIEWINFO();
                lpPreviewInfo.hPlayWnd = pnlPreview.Handle;//预览窗口
                lpPreviewInfo.lChannel = Channel;//预te览的设备通道
                lpPreviewInfo.dwStreamType = 0;//码流类型：0-主码流，1-子码流，2-码流3，3-码流4，以此类推
                lpPreviewInfo.dwLinkMode = 0;//连接方式：0- TCP方式，1- UDP方式，2- 多播方式，3- RTP方式，4-RTP/RTSP，5-RSTP/HTTP 
                lpPreviewInfo.bBlocked = true; //0- 非阻塞取流，1- 阻塞取流
                lpPreviewInfo.dwDisplayBufNum = 15; //播放库播放缓冲区最大缓冲帧数

                CHCNetSDK.REALDATACALLBACK RealData = new CHCNetSDK.REALDATACALLBACK(RealDataCallBack);//预览实时流回调函数
                IntPtr pUser = new IntPtr();//用户数据

                //打开预览 Start live view 
                m_lRealHandle = CHCNetSDK.NET_DVR_RealPlay_V40(m_lUserID, ref lpPreviewInfo, null/*RealData*/, pUser);
                if (m_lRealHandle < 0)
                {
                    iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                    if (_ErrorTable.ContainsKey(iLastErr))
                        returnStr = _ErrorTable[iLastErr];
                    else
                        returnStr = "预览失败, 错误编号 " + iLastErr; //登录失败，输出错误号
                    return false;
                }
                else
                {
                    //预览成功
                    returnStr = "预览成功";
                    return true;
                }
            }
            //停止预览
            returnStr = "请先停止预览";
            return false;
        }

        public static bool StopPreview(ref string returnStr)
        {
            if (m_lRealHandle >= 0)
            {
                //停止预览 Stop live view 
                if (!CHCNetSDK.NET_DVR_StopRealPlay(m_lRealHandle))
                {
                    iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                    if (_ErrorTable.ContainsKey(iLastErr))
                        returnStr = _ErrorTable[iLastErr];
                    else
                        returnStr = "停止预览失败, 错误编号 " + iLastErr; //登录失败，输出错误号
                    return false;
                }
                m_lRealHandle = -1;
                returnStr = "停止预览成功";
                return true;
            }
            returnStr = "未开始预览";
            return false;
        }

        public static bool JPEGCapture(ref string returnStr, int Channel, string fileName)
        {
            //string sJpegPicFileName;
            ////图片保存路径和文件名 the path and file name to save
            //sJpegPicFileName = "JPEG_test.jpg";

            CHCNetSDK.NET_DVR_JPEGPARA lpJpegPara = new CHCNetSDK.NET_DVR_JPEGPARA();
            lpJpegPara.wPicQuality = 2; //图片质量系数：0-最好，1-较好，2-一般
            lpJpegPara.wPicSize = 0xff; //抓图分辨率 Picture size: 2- 4CIF，0xff- Auto(使用当前码流分辨率)，抓图分辨率需要设备支持，更多取值请参考SDK文档

            //JPEG抓图 Capture a JPEG picture
            if (!CHCNetSDK.NET_DVR_CaptureJPEGPicture(m_lUserID, Channel, ref lpJpegPara, fileName))
            {
                iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                if (_ErrorTable.ContainsKey(iLastErr))
                    returnStr = _ErrorTable[iLastErr];
                else
                    returnStr = "JPEG抓图失败, error code= " + iLastErr;
                return false;
            }
            else
            {
                returnStr = "JPEG抓图成功 " + fileName;
                return true;
            }
        }

        /// <summary>
        /// 预览回调
        /// </summary>
        /// <param name="lRealHandle"></param>
        /// <param name="dwDataType"></param>
        /// <param name="pBuffer"></param>
        /// <param name="dwBufSize"></param>
        /// <param name="pUser"></param>
        public static void RealDataCallBack(Int32 lRealHandle, UInt32 dwDataType, ref byte pBuffer, UInt32 dwBufSize, IntPtr pUser)
        {

        }

        #endregion

        #region 大华
        /// <summary>
        /// Disconnect call
        /// </summary>
        public static fDisConnect disConnect;

        public static bool DHDVR_Init(ref string returnStr)
        {
            disConnect = new fDisConnect(DisConnectEvent);
            m_bInitSDKDH = NETClient.CLIENT_Init(disConnect, IntPtr.Zero);

            if (m_bInitSDKDH == false)
            {
                returnStr = "DHNET_DVR初始化错误!";
                return false;
            }
            return true;
        }

        public static void DisConnectEvent(int lLoginID, StringBuilder pchDVRIP, int nDVRPort, IntPtr dwUser)
        {

        }

        public static void DHDVR_Close(int port)
        {
            //停止预览 Stop live view 
            if (m_lRealHandleDH >= 0)
            {
                NETPlay.PLAY_Stop(port);  //停止预览
                NETPlay.PLAY_CloseStream(port);  //停止流
                m_lRealHandleDH = -1;
            }

            //注销登录 Logout the device
            if (m_lUserIDDH >= 0)
            {
                NETClient.CLIENT_Logout(m_lUserIDDH);     //退出用户
                m_lUserIDDH = -1;
            }

            NETClient.CLIENT_Cleanup();    //释放SDK资源
        }

        public static bool DHLoginUser(string DVRIPAddress, ushort DVRPortNumber, string DVRUserName, string DVRPassword, ref string returnStr)
        {
            if (m_lUserIDDH < 0)
            {
                NET_DEVICEINFO DeviceInfo = new NET_DEVICEINFO();
                int error = 0;

                //登录设备 Login the device
                m_lUserIDDH = NETClient.CLIENT_Login(DVRIPAddress, DVRPortNumber, DVRUserName, DVRPassword, out  DeviceInfo, out error);
                if (m_lUserIDDH < 0)
                {
                    iLastErrDH = NETClient.CLIENT_GetLastError();
                    if (_ErrorTableDH.ContainsKey((uint)iLastErrDH))
                        returnStr = _ErrorTableDH[(uint)iLastErrDH];
                    else
                        returnStr = "登录失败, 错误编号 " + iLastErr; //登录失败，输出错误号
                    return false;
                }
                else
                    return true;
            }
            else
            {
                returnStr = "该用户已登录,请先注销";
                return false;
            }
        }

        public static bool DHLogoutUser(ref string returnStr)
        {
            if (m_lUserIDDH >= 0)
            {
                if (!NETClient.CLIENT_Logout(m_lUserIDDH))
                {
                    iLastErrDH = NETClient.CLIENT_GetLastError();
                    if (_ErrorTableDH.ContainsKey((uint)iLastErrDH))
                        returnStr = _ErrorTableDH[(uint)iLastErrDH];
                    else
                        returnStr = "注销失败, 错误编号 " + iLastErr; //登录失败，输出错误号
                    return false;
                }
            }
            m_lUserIDDH = -1;
            returnStr = "注销成功";
            return true;
        }

        /// <summary>
        /// Real-time data call
        /// </summary>
        private fRealDataCallBack cbRealData;

        /// <summary>
        /// 开始预览
        /// </summary>
        /// <param name="returnStr">错误信息</param>
        /// <param name="pnlPreview">预览窗口</param>
        /// <param name="Channel">通道号</param>
        /// <returns></returns>
        public static bool DHStartPreview(ref string returnStr, IntPtr intPtr, int Channel)
        {
            if (m_lUserIDDH != 0)
            {
                m_lRealHandleDH = NETClient.CLIENT_RealPlay(m_lUserIDDH, Channel, intPtr);
                bool result = NETClient.NetSetSecurityKey(m_lRealHandleDH, "SPL17THALES00000");// Set Aes Security Key
                if (!result)
                {
                    iLastErrDH = NETClient.CLIENT_GetLastError();
                    if (_ErrorTableDH.ContainsKey((uint)iLastErrDH))
                        returnStr = _ErrorTableDH[(uint)iLastErrDH];
                    else
                        returnStr = "预览失败, 错误编号 " + iLastErr;
                    return false;
                }
                else
                    return true;
            }
            returnStr = "请先登录";
            return false;
        }

        public static bool DHStopPreview(ref string returnStr)
        {
            if (m_lRealHandleDH >= 0)
            {
                //停止预览 Stop live view 
                if (!NETClient.CLIENT_StopRealPlay(m_lRealHandleDH))
                {
                    iLastErrDH = NETClient.CLIENT_GetLastError();
                    if (_ErrorTableDH.ContainsKey((uint)iLastErrDH))
                        returnStr = _ErrorTableDH[(uint)iLastErrDH];
                    else
                        returnStr = "停止预览失败, 错误编号 " + iLastErrDH;
                    return false;
                }
                m_lRealHandleDH = -1;
                returnStr = "停止预览成功";
                return true;
            }
            returnStr = "未开始预览";
            return false;
        }

        /// <summary>
        ///抓图
        /// </summary>
        /// <param name="hPlayHandle"></param>
        public static bool CapturePicture(ref string returnStr)
        {
            if (m_lRealHandleDH != 0)
            {
                string bmpPath = Application.StartupPath + @"\CapturePic\DH_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".bmp";
                bool result = NETClient.CLIENT_CapturePicture(m_lRealHandleDH, bmpPath);
                if (!result)
                {
                    iLastErrDH = NETClient.CLIENT_GetLastError();
                    if (_ErrorTableDH.ContainsKey((uint)iLastErrDH))
                        returnStr = _ErrorTableDH[(uint)iLastErrDH];
                    else
                        returnStr = "抓图失败, 错误编号 " + iLastErr;
                    return false;
                }
                else
                    return true;
            }
            returnStr = "请先预览视频";
            return false;
        }
        #endregion
    }
}
