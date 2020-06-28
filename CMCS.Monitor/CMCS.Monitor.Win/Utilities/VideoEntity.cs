using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMCS.Monitor.Win.Utilities
{
    public class VideoEntity
    {
        public String Ip { get; set; }
        public Int32 Channel { get; set; }
        public Int32 PortNumber { get; set; }
        public String UserName { get; set; }
        public String Password { get; set; }
        public String DeviceFactory { get; set; }
        public String PicPlace { get; set; }
        public bool IsPreview { get; set; }
        public int Order { get; set; }
        //登录用户ID
        public Int32 UserId { get; set; }
        //预览窗体句柄 
        public Int32 RealHandle { get; set; }
    }
}
