using CMCS.Common;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Metro;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CMCS.Monitor.Win.Frms
{
    public partial class FrmAutoCupboardRecord : MetroForm
    {
        /// <summary>
        /// 每页显示行数
        /// </summary>
        int PageSize = 15;

        /// <summary>
        /// 总页数
        /// </summary>
        int PageCount = 0;

        /// <summary>
        /// 总记录数
        /// </summary>
        int TotalCount = 0;

        /// <summary>
        /// 当前页索引
        /// </summary>
        int CurrentIndex = 0;

        string SqlWhere = string.Empty;

        /// <summary>
        /// 当前日期
        /// </summary>
        DateTime CurrentDay = DateTime.Now;

        public FrmAutoCupboardRecord()
        {
            InitializeComponent();
        }

        private void btnDay_Click(object sender, EventArgs e)
        {
            ButtonX btn = sender as ButtonX;
            switch (btn.CommandParameter.ToString())
            {
                case "Last":
                    CurrentDay = CurrentDay.AddDays(-1);
                    break;
                case "Next":
                    CurrentDay = CurrentDay.AddDays(1);
                    break;
                case "Today":
                    CurrentDay = DateTime.Now;
                    break;
            }
            InitTimeControl();
            BindData();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            CurrentIndex = 0;
            BindData();
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            this.SqlWhere = string.Empty;
            txtBarrelCode_Ser.Text = string.Empty;
            dtInputStart.Text = string.Empty;
            dtInputEnd.Text = string.Empty;

            CurrentIndex = 0;
            BindData();
        }

        private void BindData()
        {
            this.SqlWhere = @"                       
              select *   from(select rownum AS RN, b.makecode,t.code,a.sampletype as YPTYPE,t.opstart as STARTPLACE,t.opend as PLACE,t.samplestatus as STATUS,t.starttime as CREATEDATE from inftbrecord t inner join cmcstbrcmakedetail a on 
                t.code=a.barrelcode left join cmcstbrcmake b on a.makeid=b.id order by t.starttime desc) where 1=1 ";

            if (!string.IsNullOrEmpty(txtBarrelCode_Ser.Text))
                this.SqlWhere += " and code like '%" + txtBarrelCode_Ser.Text + "%'";

            if (!string.IsNullOrEmpty(dtInputStart.Text))
                this.SqlWhere += " and createdate >= '" + dtInputStart.Value + "'";

            if (!string.IsNullOrEmpty(dtInputEnd.Text))
                this.SqlWhere += " and createdate <= '" + dtInputEnd.Value + "'";

            int rownums = 1;
            int rownume = 18;

            string pageMotify = string.Format(" and rn between {0} and {1} ", CurrentIndex * PageSize + 1, (CurrentIndex + 1) * PageSize);

            DataTable dt = new DataTable();
            dt = Dbers.GetInstance().SelfDber.ExecuteDataTable(SqlWhere + pageMotify);
            superGridControl1.PrimaryGrid.DataSource = dt;

            GetTotalCount(SqlWhere);
            PagerControlStatue();

            lblPagerInfo.Text = string.Format("共 {0} 条记录，每页 {1} 条，共 {2} 页，当前第 {3} 页", TotalCount, PageSize, PageCount, CurrentIndex + 1);
        }

        private void PagerControlStatue()
        {
            if (PageCount <= 1)
            {
                btnFirst.Enabled = false;
                btnPrevious.Enabled = false;
                btnLast.Enabled = false;
                btnNext.Enabled = false;

                return;
            }

            if (CurrentIndex == 0)
            {// 首页
                btnFirst.Enabled = false;
                btnPrevious.Enabled = false;
                btnLast.Enabled = true;
                btnNext.Enabled = true;
            }

            if (CurrentIndex > 0 && CurrentIndex < PageCount - 1)
            {// 上一页/下一页
                btnFirst.Enabled = true;
                btnPrevious.Enabled = true;
                btnLast.Enabled = true;
                btnNext.Enabled = true;
            }

            if (CurrentIndex == PageCount - 1)
            {// 末页
                btnFirst.Enabled = true;
                btnPrevious.Enabled = true;
                btnLast.Enabled = false;
                btnNext.Enabled = false;
            }
        }

        private void GetTotalCount(string sqlWhere)
        {
            TotalCount = Dbers.GetInstance().SelfDber.ExecuteDataTable(SqlWhere).Rows.Count;
            if (TotalCount % PageSize != 0)
                PageCount = TotalCount / PageSize + 1;
            else
                PageCount = TotalCount / PageSize;
        }

        private void btnPagerCommand_Click(object sender, EventArgs e)
        {
            ButtonX btn = sender as ButtonX;
            switch (btn.CommandParameter.ToString())
            {
                case "First":
                    CurrentIndex = 0;
                    break;
                case "Previous":
                    CurrentIndex = CurrentIndex - 1;
                    break;
                case "Next":
                    CurrentIndex = CurrentIndex + 1;
                    break;
                case "Last":
                    CurrentIndex = PageCount - 1;
                    break;
            }

            BindData();
        }

        private void FrmAutoCupboardRecord_Load(object sender, EventArgs e)
        {
            superGridControl1.PrimaryGrid.AutoGenerateColumns = false;
            InitTimeControl();
            btnSearch_Click(null, null);
        }

        private void InitTimeControl()
        {
            dtInputStart.Value = DateTime.Parse(CurrentDay.ToString("yyyy-MM-dd"));
            dtInputEnd.Value = dtInputStart.Value.AddDays(1).AddSeconds(-1);
        }

        private void superGridControl1_BeginEdit(object sender, DevComponents.DotNetBar.SuperGrid.GridEditEventArgs e)
        {
            // 取消编辑
            e.Cancel = true;
        }

        private void superGridControl1_GetRowHeaderText(object sender, DevComponents.DotNetBar.SuperGrid.GridGetRowHeaderTextEventArgs e)
        {
            e.Text = ((this.CurrentIndex * this.PageSize) + e.GridRow.RowIndex + 1).ToString();
        }
    }
}
