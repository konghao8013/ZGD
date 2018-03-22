//=============================================================================
//
// Project:文件操作类
// Author: LJ
// Data:2010-4-28 编写时间
// Updated:2010-4-28 修改时间
// Remark:主要针对文件的读写操作--公共方法
//
//=============================================================================

using System;
using System.Data;
using System.IO;
using System.Text;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Reflection;

namespace ZGD.Common
{
    public class FileDom
    {
        private static string baseConfigPath = "~/Config/SiteBase.config";
        /// <summary>
        /// 文件大小
        /// </summary>
        public static int fileSize = 0; //Convert.ToInt32(XMLDom.ReadConfig(baseConfigPath, "FileSize"));

        /// <summary>
        /// 适用于文件夹名(yyyy)
        /// </summary>
        public static string fileName_Y = DateTimeHelper.FormatDate(DateTime.Now, "yyyy");

        /// <summary>
        /// 适用于文件夹名(yyyyMM)
        /// </summary>
        public static string fileName_M = DateTimeHelper.FormatDate(DateTime.Now, "yyyyMM");

        /// <summary>
        /// 适用于文件夹名(yyyyMMdd)
        /// </summary>
        public static string fileName_D = DateTimeHelper.FormatDate(DateTime.Now, "yyyyMMdd");

        /// <summary>
        /// 适用于文件名(yyyyMMddHHmmss)
        /// </summary>
        public static string fileName_Only = Guid.NewGuid().ToString();

        /// <summary>
        /// 获取文件服务器路径（如："~/File/Img/xxx.txt"）
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetMapPath(string path)
        {
            return HttpContext.Current.Server.MapPath(path);
        }

        /// <summary>
        /// 获判断文件是否存在
        /// </summary>
        /// <returns>0：为不存在；返回路径</returns>
        public static string CheckFilePath(string path)
        {
            string retVale = String.Empty;
            string pathStr = GetMapPath(path);

            bool bl = File.Exists(pathStr);
            if (bl)
            {
                retVale = path;
            }
            else
            {
                retVale = "0";
            }
            return retVale;
        }

        /// <summary>
        /// 在根目录下创建文件夹
        /// 1:创建成功 0:创建失败 -1:该文件夹已存在
        /// </summary>
        /// <param name="FolderPath">要创建的文件路径</param>
        public static int CreateFolder(string FolderPathName)
        {
            int msg = 0;
            if (!string.IsNullOrEmpty(FolderPathName))
            {
                try
                {
                    string CreatePath = GetMapPath(FolderPathName).ToString();
                    if (!Directory.Exists(CreatePath))
                    {
                        Directory.CreateDirectory(CreatePath);
                        msg = 1;
                    }
                    else
                    {
                        msg = -1;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return msg;
        }

        /// <summary>
        /// 删除一个文件夹下面的子文件夹和文件
        /// 1:删除成功 0:删除失败 -1:该文件目录不存在
        /// </summary>
        /// <param name="FolderPathName">文件夹路径</param>
        public static int DeleteChildFolder(string FolderPathName)
        {
            int msg = 0;
            if (!string.IsNullOrEmpty(FolderPathName))
            {
                try
                {
                    string CreatePath = GetMapPath(FolderPathName).ToString();
                    if (Directory.Exists(CreatePath))
                    {
                        Directory.Delete(CreatePath, true);
                        msg = 1;
                    }
                    else
                    {
                        msg = -1;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return msg;
        }

        /// <summary>
        /// 删除一个文件
        /// 1:删除成功 0:删除失败 -1:该文件不存在
        /// </summary>
        /// <param name="FilePathName">文件路径</param>
        public static int DeleteFiles(string FilePathName)
        {
            int msg = 0;
            try
            {
                if (!string.IsNullOrEmpty(FilePathName))
                {
                    FileInfo DeleFile = new FileInfo(FilePathName);
                    DeleFile.Delete();
                    msg = 1;
                }
                else
                {
                    msg = -1;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return msg;
        }

        /// <summary>
        /// 删除一个文件
        /// 1:删除成功 0:删除失败 -1:该文件不存在
        /// </summary>
        /// <param name="FilePathName">文件路径</param>
        public static int DeleteFile(string FilePathName)
        {
            int msg = 0;
            try
            {
                if (!string.IsNullOrEmpty(FilePathName))
                {
                    FileInfo DeleFile = new FileInfo(GetMapPath(FilePathName).ToString());
                    DeleFile.Delete();
                    msg = 1;
                }
                else
                {
                    msg = -1;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return msg;
        }

        /// <summary>
        /// 读取文件流
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="encode">编码格式</param>
        /// <returns></returns>
        public static string GetStream(string path, Encoding codeType)
        {
            StreamReader sr = null;
            string str = String.Empty;
            string pathStr = GetMapPath(path);

            //读取模板
            try
            {
                sr = new StreamReader(pathStr, codeType);
                str = sr.ReadToEnd();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sr.Close();
            }
            return str;
        }

        /// <summary>
        /// 写文件流
        /// </summary>
        /// <param name="stream">文件信息</param>
        /// <param name="enCode">模板编码</param>
        /// <param name="toFilePath">存放生成文件的路径</param>
        /// <param name="fileName">文件名带后缀</param>
        /// <param name="isAppend">追加内容 true 覆盖内容 false</param>
        /// <returns></returns>
        public static bool WriteStream(string stream, Encoding codeType, string toFilePath, string fileName, bool isAppend)
        {
            StreamWriter sw = null;
            bool bl = false;
            try
            {
                //替换模板标签内容
                //string pathStr = GetMapPath(toFilePath + fileName);
                string pathStr = toFilePath + fileName;
                sw = new StreamWriter(pathStr, isAppend, codeType);
                sw.Write(stream);
                sw.Flush();
                bl = true;
            }
            catch (Exception ex)
            {
                bl = false;
                throw ex;
            }
            finally
            {
                sw.Close();
                sw.Dispose();
            }
            return bl;
        }

        /// <summary>
        /// 以指定的ContentType输出指定文件
        /// </summary>
        /// <param name="filepath">文件路径</param>
        /// <param name="filename">输出的文件名</param>
        /// <param name="filetype">将文件输出时设置的ContentType</param>
        public static void ResponseFile(string filepath, string filename, string filetype)
        {
            Stream iStream = null;

            // 缓冲区为10k
            byte[] buffer = new Byte[10000];
            // 文件长度
            int length;
            // 需要读的数据长度
            long dataToRead;

            try
            {
                // 打开文件
                iStream = new FileStream(filepath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

                // 需要读的数据长度
                dataToRead = iStream.Length;

                HttpContext.Current.Response.ContentType = filetype;
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(filename.Trim()).Replace("+", " "));

                while (dataToRead > 0)
                {
                    // 检查客户端是否还处于连接状态
                    if (HttpContext.Current.Response.IsClientConnected)
                    {
                        length = iStream.Read(buffer, 0, 10000);
                        HttpContext.Current.Response.OutputStream.Write(buffer, 0, length);
                        HttpContext.Current.Response.Flush();
                        buffer = new Byte[10000];
                        dataToRead = dataToRead - length;
                    }
                    else
                    {
                        // 如果不再连接则跳出死循环
                        dataToRead = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write("Error : " + ex.Message);
            }
            finally
            {
                if (iStream != null)
                {
                    // 关闭文件
                    iStream.Close();
                }
            }
            HttpContext.Current.Response.End();
        }
        
        /// <summary>
        /// 检查是否为合法的上传文件
        /// </summary>
        /// <returns></returns>
        public static bool CheckFileExt(string _fileType, string _fileExt)
        {
            string[] allowExt = _fileType.Split(',');
            for (int i = 0; i < allowExt.Length; i++)
            {
                if (allowExt[i].ToLower() == _fileExt.ToLower()) { return true; }
            }
            return false;
        }
    }
}
