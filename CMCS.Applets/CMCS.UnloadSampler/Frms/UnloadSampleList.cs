using CMCS.Common;
using CMCS.Common.Entities.CarTransport;
using CMCS.Common.Entities.iEAA;
using CMCS.Common.Utilities;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.SuperGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CMCS.UnloadSampler.Frms
{
    public partial class UnloadSampleList : DevComponents.DotNetBar.Metro.MetroForm
    {
        /// <summary>
        /// 每页显示行数
        /// </summary>
        int PageSize = 18;

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
        public UnloadSampleList()
        {
            InitializeComponent();
        }

        private void UnloadSampleList_Load(object sender, EventArgs e)
        {
            BindData();
        }


        public void BindData()
        {
            string tempSqlWhere = this.SqlWhere;


            //string sql = "select t.* from INFTBQCJXCYUNLOADCMD t ";

            //DataTable tb = Dbers.GetInstance().SelfDber.ExecuteDataTable(sql + tempSqlWhere + " order by t.CREATEDATE desc");
            //IList<InfQCJXCYUnLoadCMD> list = ConvertHelper<InfQCJXCYUnLoadCMD>.ConvertToList(tb);
            List<InfQCJXCYUnLoadCMD> list = Dbers.GetInstance().SelfDber.ExecutePager<InfQCJXCYUnLoadCMD>(PageSize, CurrentIndex, tempSqlWhere + " order by CREATEDATE desc");
            superGridControl3.PrimaryGrid.DataSource = list;
            GetTotalCount(tempSqlWhere);
            PagerControlStatue();
            lblPagerInfo.Text = string.Format("共 {0} 条记录，每页 {1} 条，共 {2} 页，当前第 {3} 页", TotalCount, PageSize, PageCount, CurrentIndex + 1);

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.SqlWhere = " where 1=1";
            if (dtpStartTime.Value.Year > 2000) this.SqlWhere += " and CREATEDATE >= '" + dtpStartTime.Value.Date + "'";
            if (dtpEndTime.Value.Year > 2000) this.SqlWhere += " and CREATEDATE < '" + dtpEndTime.Value.AddDays(1).Date + "'";
            if (!string.IsNullOrEmpty(textSampleCode.Text)) this.SqlWhere += " and SAMPLECODE like '%" + textSampleCode.Text + "%'";
            if (!string.IsNullOrEmpty(txtPle.Text)) this.SqlWhere += " and CREATEUSER like '%" + txtPle.Text + "%'";
            CurrentIndex = 0;
            BindData();
        }

        #region Pager

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

        public void PagerControlStatue()
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
            {
                // 首页
                btnFirst.Enabled = false;
                btnPrevious.Enabled = false;
                btnLast.Enabled = true;
                btnNext.Enabled = true;
            }

            if (CurrentIndex > 0 && CurrentIndex < PageCount - 1)
            {
                // 上一页/下一页
                btnFirst.Enabled = true;
                btnPrevious.Enabled = true;
                btnLast.Enabled = true;
                btnNext.Enabled = true;
            }

            if (CurrentIndex == PageCount - 1)
            {
                // 末页
                btnFirst.Enabled = true;
                btnPrevious.Enabled = true;
                btnLast.Enabled = false;
                btnNext.Enabled = false;
            }
        }

        private void GetTotalCount(string sqlWhere)
        {
            TotalCount = Dbers.GetInstance().SelfDber.Count<InfQCJXCYUnLoadCMD>(sqlWhere);
            if (TotalCount % PageSize != 0)
                PageCount = TotalCount / PageSize + 1;
            else
                PageCount = TotalCount / PageSize;
        }
        #endregion

        private void superGridControl3_DataBindingComplete(object sender, DevComponents.DotNetBar.SuperGrid.GridDataBindingCompleteEventArgs e)
        {
            foreach (GridRow gridRow in e.GridPanel.Rows)
            {
                InfQCJXCYUnLoadCMD entity = gridRow.DataItem as InfQCJXCYUnLoadCMD;
                if (entity == null) return;
                
                User user = Dbers.GetInstance().SelfDber.Entity<User>(" where UserAccount= '" + entity.CreateUser + "'");
                if(user!=null)
                    gridRow.Cells["CreateUser"].Value = (user.UserName);
            }
        }
    }
}
