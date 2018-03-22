using System;
namespace ZGD.Model
{
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
        private int _zxtype;
        private string _title;
        private string _keyword;
        private string _description;
        private string _tags;
        private string _author;
        private int _classid;
        private string _classname;
        private string _Address;
        private string _content;
        private string _imgurl;
        private int _isimage = 0;
        private int _click = 0;
        private int _istop = 0;
        private int _islock = 0;
        private int _assid = 0;
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
        /// 1����  2С������ House
        /// </summary>
        public int ZxType
        {
            set { _zxtype = value; }
            get { return _zxtype; }
        }
        /// <summary>
        /// ����ID ��ӦZxType
        /// </summary>
        public int AssId
        {
            set { _assid = value; }
            get { return _assid; }
        }
        /// <summary>
        /// ���ű���
        /// </summary>
        public string Title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// ��ַ (�ڽ�����)
        /// </summary>
        public string Address
        {
            set { _Address = value; }
            get { return _Address; }
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
        public int ClassId
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
        public string UserName { get; set; }
        public int DesignerId { get; set; }
        public int fgID { get; set; }
        public int hxID { get; set; }
        public int ZxKind { get; set; }
        /// <summary>
        /// װ�޽���
        /// </summary>
        public int gdStatus { get; set; }
        public int Area { get; set; }
        public decimal Price { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        public int AreaId { get; set; }
        #endregion Model

    }
}

