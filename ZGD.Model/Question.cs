using System;
namespace ZGD.Model
{
    /// <summary>
    /// Question:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class Question
    {
        public Question()
        { }
        #region Model
        private int _id;
        private int? _userid = 0;
        private string _title;
        private string _contents;
        private string _keyword;
        private string _description;
        private string _imgurl;
        private int? _click = 0;
        private int? _istop = 0;
        private int? _isgood;
        private int? _islock = 0;
        private DateTime _pubtime;
        private int? _status = 1;
        public string Nickname { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? UserId
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Contents
        {
            set { _contents = value; }
            get { return _contents; }
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
        /// 
        /// </summary>
        public string ImgUrl
        {
            set { _imgurl = value; }
            get { return _imgurl; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Click
        {
            set { _click = value; }
            get { return _click; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? IsTop
        {
            set { _istop = value; }
            get { return _istop; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? IsGood
        {
            set { _isgood = value; }
            get { return _isgood; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? IsLock
        {
            set { _islock = value; }
            get { return _islock; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime PubTime
        {
            set { _pubtime = value; }
            get { return _pubtime; }
        }
        /// <summary>
        /// 0屏蔽 1正常 2解决
        /// </summary>
        public int? Status
        {
            set { _status = value; }
            get { return _status; }
        }
        #endregion Model

    }
}

