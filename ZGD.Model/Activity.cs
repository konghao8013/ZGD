using System;
namespace ZGD.Model
{
    /// <summary>
    /// Activity:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class Activity
    {
        public Activity()
        { }
        #region Model
        private int _id;
        private int _JoinCount;
        private string _Title;
        private string _keyword;
        private string _description;
        private string _sdate;
        private string _edate;
        private string _imgurl;
        private string _aUrl;
        private string _acon;
        private DateTime? _pubtime = DateTime.Now;
        private int? _status = 1;
        private int _click;
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
        public int JoinCount
        {
            set { _JoinCount = value; }
            get { return _JoinCount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string aUrl
        {
            set { _aUrl = value; }
            get { return _aUrl; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Title
        {
            set { _Title = value; }
            get { return _Title; }
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
        public string sDate
        {
            set { _sdate = value; }
            get { return _sdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string eDate
        {
            set { _edate = value; }
            get { return _edate; }
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
        public string aCon
        {
            set { _acon = value; }
            get { return _acon; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? PubTime
        {
            set { _pubtime = value; }
            get { return _pubtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Status
        {
            set { _status = value; }
            get { return _status; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Click
        {
            set { _click = value; }
            get { return _click; }
        }
        #endregion Model

    }
}

