using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZGD.Web.Tools
{
    public partial class FilesUpload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //---------------------------------------------------------------------------------------------
            //组件设置a.MD5File为2，3时 的实例代码

            if (Request.QueryString["access2008_cmd"] != null && Request.QueryString["access2008_cmd"] == "2")//服务器提交MD5验证后的文件信息进行验证
            {
                //  Request.QueryString["access2008_File_name"];    //文件名
                //  Request.QueryString["access2008_File_size"];    //文件大小，单位字节
                //  Request.QueryString["access2008_File_type"];    //文件类型 例如.gif .png
                //  Request.QueryString["access2008_File_md5"];     //文件的MD5签名

                Response.Write("0");//返回命令  0 = 开始上传文件， 2 = 不上传文件，前台直接显示上传完成
                Response.End();
            }
            else if (Request.QueryString["access2008_cmd"] != null && Request.QueryString["access2008_cmd"] == "3") //服务器提交文件信息进行验证
            {
                //  Request.QueryString["access2008_File_name"];    //文件名
                //  Request.QueryString["access2008_File_size"];    //文件大小，单位字节
                //  Request.QueryString["access2008_File_type"];    //文件类型 例如.gif .png

                Response.Write("1");//返回命令 0 = 开始上传文件,1 = 提交MD5验证后的文件信息进行验证, 2 = 不上传文件，前台直接显示上传完成
                Response.End();
            }
            //---------------------------------------------------------------------------------------------

            if (Request.Files["Filedata"] != null)//判断是否有文件上传上来
            {
                SaveImages();
                //其他表单数据接收

                //if (Request.QueryString["access2008_File_md5"] != null)
                //{
                //    Response.Write("<br/>");
                //    Response.Write("MD5效验" + Request.QueryString["access2008_File_md5"]);
                //}
                //Response.Write("<br/>");
                //Response.Write("你选择的是<font color='#ff0000'>" + Request.Form["select"] + "</font>--<font color='#0000ff'>" + Request.Form["select2"] + "</font>");
                //Response.End();
            }
        }

        /// <summary>
        /// 保存文件
        /// </summary>
        /// <returns></returns>
        private void SaveImages()
        {
            ///'遍历File表单元素
            HttpFileCollection files = HttpContext.Current.Request.Files;

            ///'检查文件扩展名字
            //HttpPostedFile postedFile = files[iFile];
            HttpPostedFile postedFile = files[0]; // Request.Files["Filedata"]; //得到要上传文件
            string fileName = string.Empty, nFileName = string.Empty, fileExtension = string.Empty, filesize = string.Empty;
            fileName = System.IO.Path.GetFileName(postedFile.FileName.ToString()); //得到文件名
            filesize = System.IO.Path.GetFileName(postedFile.ContentLength.ToString()); //得到文件大小
            //按日期归类保存
            string _filePath = "/UpLoadFiles/" + DateTime.Now.ToString("yyyyMMdd") + "/";
            if (fileName != "")
            {
                ZGD.Model.SiteConfig webset = new ZGD.BLL.siteconfig().loadConfig();
                fileExtension = fileName.Substring(fileName.LastIndexOf('.') + 1);//'获取扩展名
                if (ZGD.Common.FileDom.CheckFileExt(webset.fileextension, fileExtension))
                {
                    nFileName = Guid.NewGuid().ToString() + "." + fileExtension;
                    //检查是否有该路径没有就创建
                    string toFileFullPath = HttpContext.Current.Server.MapPath(_filePath);
                    if (!Directory.Exists(toFileFullPath))
                    {
                        Directory.CreateDirectory(toFileFullPath);
                    }
                    toFileFullPath = toFileFullPath + nFileName;
                    postedFile.SaveAs(toFileFullPath);
                }
            }
            Response.Write(_filePath + nFileName);
        }
    }
}