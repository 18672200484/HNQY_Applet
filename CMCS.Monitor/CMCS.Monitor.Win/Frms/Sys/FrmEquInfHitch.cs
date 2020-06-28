using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CMCS.Common.DAO;
using CMCS.Common.Entities;
using DevComponents.DotNetBar.Metro;
using CMCS.Common;
using DevComponents.DotNetBar;
using CMCS.Common.Entities.BaseInfo;
using CMCS.Common.Entities.Inf;

namespace CMCS.Monitor.Win.Frms.Sys
{
    public partial class FrmEquInfHitch : MetroForm
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

        string MachineCode = string.Empty;
        public FrmEquInfHitch(string machineCode)
        {
            InitializeComponent();
            this.MachineCode = machineCode;
        }

        private void FrmEquInfHitch_Load(object sender, EventArgs e)
        {
            // 加载设备
            cmbEquipment.DisplayMember = "EquipmentName";
            cmbEquipment.ValueMember = "EquipmentCode";
            cmbEquipment.DataSource = Dbers.GetInstance().SelfDber.Entities<CmcsCMEquipment>("WHERE PARENTID IN (SELECT ID FROM CMCSTBCMEQUIPMENT A WHERE A.EQUIPMENTCODE in ('智能存样柜','气动传输')) ORDER BY SEQUENCE");
            cmbEquipment.SelectedValue = this.MachineCode;

            dateTimeInput1.Value = DateTime.Now.Date;
            dateTimeInput2.Value = DateTime.Now.Date.AddDays(1).AddMilliseconds(-1);

            superGridControl1.PrimaryGrid.AutoGenerateColumns = false;

            btnSerach_Click(null, null);
        }

        public void BindData()
        {
            string tempSqlWhere = this.SqlWhere;
            List<InfEquInfHitch> list = Dbers.GetInstance().SelfDber.ExecutePager<InfEquInfHitch>(PageSize, CurrentIndex, tempSqlWhere + " order by HitchTime desc");
            GetTotalCount(tempSqlWhere);

            superGridControl1.PrimaryGrid.DataSource = list;
            PagerControlStatue();
            lblPagerInfo.Text = string.Format("共 {0} 条记录，每页 {1} 条，共 {2} 页，当前第 {3} 页", TotalCount, PageSize, PageCount, CurrentIndex + 1);
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

        private void btnSerach_Click(object sender, EventArgs e)
        {
            this.SqlWhere = string.Empty;

            CmcsCMEquipment cMEquipment = cmbEquipment.SelectedItem as CmcsCMEquipment;
            if (cMEquipment != null) SqlWhere += " and MachineCode='" + cMEquipment.EquipmentCode + "' ";

            if (!String.IsNullOrEmpty((String)dateTimeInput1.Text))
            {
                SqlWhere += " and HitchTime>= to_date('" + dateTimeInput1.Value.ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss') ";
            }
            if (!String.IsNullOrEmpty((String)dateTimeInput2.Text))
            {
                SqlWhere += " and HitchTime< to_date('" + dateTimeInput2.Value.ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss') ";
            }

            if (!String.IsNullOrEmpty(this.SqlWhere))
            {
                SqlWhere = " where 1=1 " + SqlWhere;
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
            TotalCount = Dbers.GetInstance().SelfDber.Count<InfEquInfHitch>(sqlWhere);
            if (TotalCount % PageSize != 0)
                PageCount = TotalCount / PageSize + 1;
            else
                PageCount = TotalCount / PageSize;
        }
        #endregion

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
