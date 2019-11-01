using System;
namespace ZGD.Model
{
    /// <summary>
    /// NewsInfo:ʵ����(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    /// </summary>
    [Serializable]
    public class NewsColumns
    {
        public int Id { get; set; }
        public int NewsId { get; set; }
        public int ClassId { get; set; }
        public DateTime PubTime { get; set; }
    }

    /// <summary>
    /// NewsInfo:ʵ����(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    /// </summary>
    [Serializable]
    public partial class NewsInfo
    {
        public NewsInfo()
        { }
        #region Model
        private int _id;
        private string _title;
        private string _keyword;
        private string _description;
        private string _tags;
        private string _author;
        private string _classid;
        private string _classname;
        private string _content;
        private string _imgurl;
        private int _isimage = 0;
        private int _click = 0;
        private int _istop = 0;
        private int _islock = 0;
        private DateTime _pubtime = DateTime.Now;
        /// <summary>
        /// ����ID PK
        /// </summary>
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// ����
        /// </summary>
        public string Files { get; set; }
        /// <summary>
        /// ���ű���
        /// </summary>
        public string Title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Tags
        {
            set { _tags = value; }
            get { return _tags; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Keyword
        {
            set { _keyword = value; }
            get { return _keyword; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Description
        {
            set { _description = value; }
            get { return _description; }
        }
        /// <summary>
        /// ������
        /// </summary>
        public string Author
        {
            set { _author = value; }
            get { return _author; }
        }
        /// <summary>
        /// �������
        /// </summary>
        public string ClassId
        {
            set { _classid = value; }
            get { return _classid; }
        }
        /// <summary>
        /// �����������
        /// </summary>
        public string ClassName
        {
            set { _classname = value; }
            get { return _classname; }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public string Content
        {
            set { _content = value; }
            get { return _content; }
        }
        /// <summary>
        /// ͼƬ·��
        /// </summary>
        public string ImgUrl
        {
            set { _imgurl = value; }
            get { return _imgurl; }
        }
        /// <summary>
        /// �Ƿ�ͼƬ����
        /// </summary>
        public int IsImage
        {
            set { _isimage = value; }
            get { return _isimage; }
        }
        /// <summary>
        /// �������
        /// </summary>
        public int Click
        {
            set { _click = value; }
            get { return _click; }
        }
        /// <summary>
        /// �Ƿ��ö�
        /// </summary>
        public int IsTop
        {
            set { _istop = value; }
            get { return _istop; }
        }
        /// <summary>
        /// �Ƿ�����
        /// </summary>
        public int IsLock
        {
            set { _islock = value; }
            get { return _islock; }
        }
        /// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTime PubTime
        {
            set { _pubtime = value; }
            get { return _pubtime; }
        }
        public string SubTitle { get; set; }
        public string PubUnit { get; set; }

        public int UserId { get; set; }
        public int IsPub { get; set; }

        #endregion Model

    }
}

