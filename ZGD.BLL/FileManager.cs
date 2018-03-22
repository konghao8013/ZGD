//=============================================================================
//
// Project:�ļ����� 
// Author:ˣ��  LJ
// Data:2010-11-4 ��дʱ��
// Updated:2010-11-4 �޸�ʱ��
//
//=============================================================================
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Web;
using ZGD.Model;

namespace ZGD.AdminManage
{
    /// <summary>
    /// FileManager
    /// </summary>
    public class FileManager
    {
        /// <summary>
        /// ��Ŀ¼
        /// </summary>
        private static string strRootFolder;

        public FileManager()
        {
            strRootFolder = HttpContext.Current.Request.PhysicalApplicationPath + "Files\\";
            strRootFolder = strRootFolder.Substring(0, strRootFolder.LastIndexOf(@"\"));
        }

        public FileManager(string file)
        {
            strRootFolder = HttpContext.Current.Request.PhysicalApplicationPath + "" + file + "\\";
            strRootFolder = strRootFolder.Substring(0, strRootFolder.LastIndexOf(@"\"));
        }

        /// <summary>
        /// �����ļ���ͼ
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string GetFileList(string filePath, string pageUrl, string path, out string dirPath)
        {
            dirPath = "~/" + filePath;
            if (!string.IsNullOrEmpty(path))
            {
                path = path.Replace("|", "/");
                dirPath += path;
                path = ZGD.Common.FileDom.GetMapPath("~/" + filePath + path);
            }
            else
                path = GetRootPath();
            List<FileModel> fileList = GetItems(path);
            return CreateFileView(filePath, pageUrl, fileList);
        }

        /// <summary>
        /// �����ļ���ͼ
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string CreateFileView(string filePath, string pageUrl, List<FileModel> fileList)
        {
            StringBuilder sb = new StringBuilder();
            //�ļ�����
            sb.Append("<table class=\"msgtable\" id=\"FileList\">");
            sb.Append("<tr>");
            sb.Append("<th width=\"3%\"><input type=\"checkbox\" id=\"cbAll\"></th>");
            sb.Append("<th>����</th>");
            sb.Append("<th width=\"15%\">��������</th>");
            sb.Append("<th width=\"15%\">����������</th>");
            sb.Append("<th width=\"15%\">����޸�����</th>");
            sb.Append("<th width=\"10%\">��С</th>");
            sb.Append("</tr>");
            if (fileList.Count > 0)
            {
                int idx = 1;
                foreach (FileModel model in fileList)
                {
                    sb.Append("<tr>");

                    //�ļ��ж�
                    string fileName = "<img src=\"/Images/ico/{0}.gif\" style=\"vertical-align:middle;\" /> {1}";
                    string hzStr = model.Name.Substring(model.Name.LastIndexOf(".") + 1);
                    string url = model.FullName.Replace(GetRootPath(), "");
                    url = url.Replace("\\", "/");

                    if (!model.Name.Equals("[��һ��]") && !model.Name.Equals("[��Ŀ¼]"))
                    {
                        if (!model.IsFolder)
                        {
                            //�ж��ļ���ʽ����ͼ��
                            string logo = File.Exists(ZGD.Common.FileDom.GetMapPath("~/Images/ico/" + hzStr + ".gif")) ? hzStr : "other";
                            fileName = string.Format(fileName, logo, "<a href=\"/" + filePath + url + "\" target=\"_blank\">" + model.Name + "</a>");
                            sb.Append("<td align=\"center\"><input type=\"checkbox\" id=\"cbFile_" + idx + "\" value=\"" + url.Replace("/", "|") + "\"></td>");
                        }
                        else
                        {
                            url = url.Replace("/", "|").TrimEnd('|');
                            fileName = string.Format(fileName, "folder", "<a href=\"" + pageUrl + "?dir=" + url + "\" class=\"fw600\">" + model.Name + "</a>");
                            sb.Append("<td align=\"center\"><input type=\"checkbox\" id=\"cbFile_" + idx + "\" value=\"" + url + "\" disabled=\"disabled\"></td>");
                        }
                        sb.Append("<td>" + fileName + "</td>");
                        sb.Append("<td align=\"center\">" + model.CreationDate + "</td>");
                        sb.Append("<td align=\"center\">" + model.LastAccessDate + "</td>");
                        sb.Append("<td align=\"center\">" + model.LastWriteDate + "</td>");
                        string size = model.IsFolder ? "<span class=\"red\">�ļ���</span>" : model.Size + " B";
                        sb.Append("<td align=\"right\">" + size + "</td>");
                    }
                    else
                    {
                        sb.Append("<td align=\"center\"><input type=\"checkbox\" id=\"cbFile_" + idx + "\" value=\"" + idx + "\" disabled=\"disabled\"></td>");
                        sb.Append("<td colspan=\"5\"><img src=\"/Images/ico/back.gif\" style=\"vertical-align:middle;\" /> <a href=\"" + pageUrl + "?dir=" + url + "\" class=\"fw600\">" + model.Name + "</a></td>");
                    }
                    sb.Append("</tr>");
                    idx++;
                }
            }
            else
            {
                sb.Append("<tr><td colspan=\"6\" align=\"center\">���ļ���û���ļ�...</td></tr>");
            }
            sb.Append("</table>");
            return sb.ToString();
        }

        /// <summary>
        /// ����Ŀ¼
        /// </summary>
        /// <returns></returns>
        public static string GetRootPath()
        {
            return strRootFolder;
        }

        /// <summary>
        /// ��ȡ�б�(��Ŀ¼)
        /// </summary>
        /// <returns></returns>
        public List<FileModel> GetItems()
        {
            return GetItems(strRootFolder);
        }

        /// <summary>
        /// ��ȡ�б�(ָ��Ŀ¼)
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public List<FileModel> GetItems(string path)
        {
            List<FileModel> list = new List<FileModel>();
            if (Directory.Exists(path))
            {
                string[] folders = Directory.GetDirectories(path);
                string[] files = Directory.GetFiles(path);
                foreach (string s in folders)
                {
                    FileModel item = new FileModel();
                    DirectoryInfo di = new DirectoryInfo(s);
                    item.Name = di.Name;
                    item.FullName = di.FullName;
                    item.CreationDate = di.CreationTime;
                    item.LastWriteDate = di.LastWriteTime;
                    item.LastAccessDate = di.LastAccessTime;
                    item.IsFolder = true;
                    item.FileCount = di.GetFiles().Length;
                    item.SubFolderCount = di.GetDirectories().Length;
                    list.Add(item);
                }
                foreach (string s in files)
                {
                    FileModel item = new FileModel();
                    FileInfo fi = new FileInfo(s);
                    item.Name = fi.Name;
                    item.FullName = fi.FullName;
                    item.CreationDate = fi.CreationTime;
                    item.LastWriteDate = fi.LastWriteTime;
                    item.LastAccessDate = fi.LastAccessTime;
                    item.IsFolder = false;
                    item.Size = fi.Length;
                    list.Add(item);
                }

                if (strRootFolder != null)
                {
                    if (path.ToLower() != strRootFolder.ToLower())
                    {
                        FileModel topitem = new FileModel();
                        DirectoryInfo topdi = new DirectoryInfo(path).Parent;
                        topitem.Name = "[��һ��]";
                        topitem.FullName = topdi.FullName;
                        list.Insert(0, topitem);

                        FileModel rootitem = new FileModel();
                        DirectoryInfo rootdi = new DirectoryInfo(strRootFolder);
                        rootitem.Name = "[��Ŀ¼]";
                        rootitem.FullName = rootdi.FullName;
                        list.Insert(0, rootitem);
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// ��ȡ�ļ���Ϣ
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public FileModel GetItemInfo(string path)
        {
            FileModel item = new FileModel();
            if (Directory.Exists(path))
            {
                DirectoryInfo di = new DirectoryInfo(path);
                item.Name = di.Name;
                item.FullName = di.FullName;
                item.CreationDate = di.CreationTime;
                item.IsFolder = true;
                item.LastAccessDate = di.LastAccessTime;
                item.LastWriteDate = di.LastWriteTime;
                item.FileCount = di.GetFiles().Length;
                item.SubFolderCount = di.GetDirectories().Length;
            }
            else
            {
                FileInfo fi = new FileInfo(path);
                item.Name = fi.Name;
                item.FullName = fi.FullName;
                item.CreationDate = fi.CreationTime;
                item.LastAccessDate = fi.LastAccessTime;
                item.LastWriteDate = fi.LastWriteTime;
                item.IsFolder = false;
                item.Size = fi.Length;
            }
            return item;
        }
    }
}