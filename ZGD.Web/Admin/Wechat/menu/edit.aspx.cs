using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ZGD.Common;

namespace ZGD.Web.Admin.wechat.menu
{
    public partial class edit : ZGD.BasePage.ManagePage
    {
        private BLL.wechat n_wechat = new BLL.wechat();
        public string actions = "add";
        public Model.wechat_menu n_model = new Model.wechat_menu();
        public int editID = 0;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Request.ServerVariables["REQUEST_METHOD"] == "POST")
            {
                post();
            }
            else
            {
                if (!Page.IsPostBack)
                {
                    actions = Request["action"];
                    TreeBind();
                    if (actions == "edit")
                    {
                        showinfo();
                    }
                    else 
                    {
                        int id = DTRequest.GetQueryInt("id");
                        this.ddlParentId.SelectedValue = id.ToString();
                        n_model.menu_type = "view";
                        n_model.menu_is_show = 1;
                        n_model.menu_content_id = 0;
                    }
                }
            }

        }
        #region 绑定导航菜单=============================
        private void TreeBind()
        {
            DataTable dt = n_wechat.menu_list();

            this.ddlParentId.Items.Clear();
            this.ddlParentId.Items.Add(new ListItem("无父级导航", "0"));
            foreach (DataRow dr in dt.Rows)
            {
                string Id = dr["id"].ToString();
                int parentid = int.Parse(dr["menu_parent_id"].ToString());
                string Title = dr["menu_name"].ToString().Trim();

                if (parentid == 0)
                {
                    this.ddlParentId.Items.Add(new ListItem(Title, Id));
                }
                else
                {
                    Title = "├ " + Title;
                    Title = Utils.StringOfChar(1, "　") + Title;
                    this.ddlParentId.Items.Add(new ListItem(Title, Id));
                }
            }
        }
        #endregion

        public void showinfo()
        {
            int id = DTRequest.GetQueryInt("id");
            editID = id;
            n_model = n_wechat.menu_model(id);
            this.ddlParentId.SelectedValue = n_model.menu_parent_id.ToString();
        }

        private void post()
        {
            actions = Request["action"];
            Model.wechat_menu model = new Model.wechat_menu();
            
            string menu_name = Request["menu_name"];
            string menu_key = Request["menu_key"];
            string menu_parent_id = Request["ddlParentId"];
            string menu_sort = Request["menu_sort"];
            string menu_content_id = Request["menu_content_id"];
            string menu_type = Request["menu_type"];
            string menu_url = Request["menu_url"];
            string menu_is_show = Request["menu_is_show"];
            menu_is_show = menu_is_show == "on" ? "1" : "0";

            model.menu_name = menu_name;
            model.menu_key = menu_key==null?"":menu_key;
            model.menu_parent_id = int.Parse(menu_parent_id);
            model.menu_sort = int.Parse(menu_sort);
            model.menu_content_id = int.Parse(menu_content_id==null?"0":menu_content_id);
            model.menu_type = menu_type;
            model.menu_url = menu_url==null?"":menu_url;
            model.menu_is_show = int.Parse(menu_is_show);

            if (actions == "add")
            {
                if (n_wechat.menu_add(model) > 0)
                {
                    //WriteJscriptMsg("添加菜单成功！", "list.aspx", "Success", "");
                    string msbox = "<script>parent.jsprint(\"添加菜单成功\", \"list.aspx\", \"Success\")</script>";
                    Context.Response.Write(msbox);
                }
                else
                {
                    JscriptMsg("添加菜单失败！", "list.aspx", "Error", "");
                }
            }
            if (actions == "edit")
            {
                int id = DTRequest.GetFormInt("id");
                model.id = id;
                if (n_wechat.menu_update(model) > 0)
                {
                    //WriteJscriptMsg("添加菜单成功！", "list.aspx", "Success", "");
                    string msbox = "<script>parent.jsprint(\"添加菜单成功\", \"list.aspx\", \"Success\")</script>";
                    Context.Response.Write(msbox);
                }
                else
                {
                    JscriptMsg("添加菜单失败！", "list.aspx", "Error", "");
                }
            }
        }
    }
}