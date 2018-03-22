//=============================================================================
//
// Project:�ļ�������
// Author: LJ
// Data:2010-4-28 ��дʱ��
// Updated:2010-4-28 �޸�ʱ��
// Remark:��Ҫ����ļ��Ķ�д����--��������
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
        /// �ļ���С
        /// </summary>
        public static int fileSize = 0; //Convert.ToInt32(XMLDom.ReadConfig(baseConfigPath, "FileSize"));

        /// <summary>
        /// �������ļ�����(yyyy)
        /// </summary>
        public static string fileName_Y = DateTimeHelper.FormatDate(DateTime.Now, "yyyy");

        /// <summary>
        /// �������ļ�����(yyyyMM)
        /// </summary>
        public static string fileName_M = DateTimeHelper.FormatDate(DateTime.Now, "yyyyMM");

        /// <summary>
        /// �������ļ�����(yyyyMMdd)
        /// </summary>
        public static string fileName_D = DateTimeHelper.FormatDate(DateTime.Now, "yyyyMMdd");

        /// <summary>
        /// �������ļ���(yyyyMMddHHmmss)
        /// </summary>
        public static string fileName_Only = Guid.NewGuid().ToString();

        /// <summary>
        /// ��ȡ�ļ�������·�����磺"~/File/Img/xxx.txt"��
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetMapPath(string path)
        {
            return HttpContext.Current.Server.MapPath(path);
        }

        /// <summary>
        /// ���ж��ļ��Ƿ����
        /// </summary>
        /// <returns>0��Ϊ�����ڣ�����·��</returns>
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
        /// �ڸ�Ŀ¼�´����ļ���
        /// 1:�����ɹ� 0:����ʧ�� -1:���ļ����Ѵ���
        /// </summary>
        /// <param name="FolderPath">Ҫ�������ļ�·��</param>
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
        /// ɾ��һ���ļ�����������ļ��к��ļ�
        /// 1:ɾ���ɹ� 0:ɾ��ʧ�� -1:���ļ�Ŀ¼������
        /// </summary>
        /// <param name="FolderPathName">�ļ���·��</param>
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
        /// ɾ��һ���ļ�
        /// 1:ɾ���ɹ� 0:ɾ��ʧ�� -1:���ļ�������
        /// </summary>
        /// <param name="FilePathName">�ļ�·��</param>
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
        /// ɾ��һ���ļ�
        /// 1:ɾ���ɹ� 0:ɾ��ʧ�� -1:���ļ�������
        /// </summary>
        /// <param name="FilePathName">�ļ�·��</param>
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
        /// ��ȡ�ļ���
        /// </summary>
        /// <param name="path">·��</param>
        /// <param name="encode">�����ʽ</param>
        /// <returns></returns>
        public static string GetStream(string path, Encoding codeType)
        {
            StreamReader sr = null;
            string str = String.Empty;
            string pathStr = GetMapPath(path);

            //��ȡģ��
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
        /// д�ļ���
        /// </summary>
        /// <param name="stream">�ļ���Ϣ</param>
        /// <param name="enCode">ģ�����</param>
        /// <param name="toFilePath">��������ļ���·��</param>
        /// <param name="fileName">�ļ�������׺</param>
        /// <param name="isAppend">׷������ true �������� false</param>
        /// <returns></returns>
        public static bool WriteStream(string stream, Encoding codeType, string toFilePath, string fileName, bool isAppend)
        {
            StreamWriter sw = null;
            bool bl = false;
            try
            {
                //�滻ģ���ǩ����
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
        /// ��ָ����ContentType���ָ���ļ�
        /// </summary>
        /// <param name="filepath">�ļ�·��</param>
        /// <param name="filename">������ļ���</param>
        /// <param name="filetype">���ļ����ʱ���õ�ContentType</param>
        public static void ResponseFile(string filepath, string filename, string filetype)
        {
            Stream iStream = null;

            // ������Ϊ10k
            byte[] buffer = new Byte[10000];
            // �ļ�����
            int length;
            // ��Ҫ�������ݳ���
            long dataToRead;

            try
            {
                // ���ļ�
                iStream = new FileStream(filepath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

                // ��Ҫ�������ݳ���
                dataToRead = iStream.Length;

                HttpContext.Current.Response.ContentType = filetype;
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(filename.Trim()).Replace("+", " "));

                while (dataToRead > 0)
                {
                    // ���ͻ����Ƿ񻹴�������״̬
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
                        // �������������������ѭ��
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
                    // �ر��ļ�
                    iStream.Close();
                }
            }
            HttpContext.Current.Response.End();
        }
        
        /// <summary>
        /// ����Ƿ�Ϊ�Ϸ����ϴ��ļ�
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
