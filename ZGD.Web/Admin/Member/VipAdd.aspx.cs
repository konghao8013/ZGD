using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ZGD.Common;

namespace ZGD.Web.Admin.Member
{
    public partial class VipAdd : ZGD.BasePage.ManagePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        #region 添加
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string strErr = "";
            if (this.txtCount.Text.Trim().Length == 0)
            {
                strErr += "卡位数不能为空！\\n";
            }
            if (this.txtStart.Text.Trim().Length == 0)
            {
                strErr += "起始号码不能为空！\\n";
            }
            if (this.txtEnd.Text.Trim().Length == 0)
            {
                strErr += "结束号码不能为空！\\n";
            }
            if (strErr != "")
            {
                MessageBox.Show(this, strErr);
                return;
            }

            List<ZGD.Model.VipCard> modelList = new List<Model.VipCard>();
            ZGD.BLL.VipCard bll = new ZGD.BLL.VipCard();
            int count = Convert.ToInt32(txtCount.Text);
            int start = Convert.ToInt32(txtStart.Text);
            int elseNo = Convert.ToInt32(txtElse.Text);
            int end = Convert.ToInt32(txtEnd.Text);
            string codeList = "";
            for (int code = start; code <= end; code++)
            {
                //计算 卡号补0
                string zero = "";
                for (int j = 0; j < count - code.ToString().Length; j++)
                {
                    zero += "0";
                }
                //坚持卡号是否存在排除号
                if (code.ToString().IndexOf('4') > 0)
                {
                    continue;
                }
                codeList += zero + code + ",";
                modelList.Add(new ZGD.Model.VipCard()
                {
                    CardNo = zero + code
                });
            }
            int msg = bll.AddList(modelList);
            if (msg > 0)
            {
                txtCode.Text = codeList;
                //保存日志
                SaveLogs("[会员模块]添加VIP卡：0" + start + "至0" + end);
                JscriptMsg("发布成功");
            }
            else
            {
                JscriptMsg("发布过程中发生错误");
            }
        }
        #endregion

        protected void btnReset_Click(object sender, EventArgs e)
        {
            this.txtCount.Text = "";
            this.txtCode.Text = "";
            this.txtElse.Text = "";
            this.txtStart.Text = "";
            this.txtEnd.Text = "";
        }
    }
}