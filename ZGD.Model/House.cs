using System;
namespace ZGD.Model
{
    /// <summary>
    /// House:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class House
    {
        public House()
        { }
        #region Model
        private int _id;
        private string _title;
        private string _keyword;
        private string _description;
        private string _address;
        private string _hcontent;
        private string _yhcontent;
        private string _imgurl;
        private string _imagesmall;
        private int? _click = 0;
        private int? _istop = 0;
        private int? _islock = 0;
        private DateTime? _pubtime = DateTime.Now;
        /// <summary>
        /// 案例数
        /// </summary>
        public int cCount
        {
            set;
            get;
        }
        /// <summary>
        /// 工地数
        /// </summary>
        public int pCount
        {
            set;
            get;
        }
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
        public string Title
        {
            set { _title = value; }
            get { return _title; }
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
        public string Address
        {
            set { _address = value; }
            get { return _address; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string hContent
        {
            set { _hcontent = value; }
            get { return _hcontent; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string yhContent
        {
            set { _yhcontent = value; }
            get { return _yhcontent; }
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
        public string ImageSmall
        {
            set { _imagesmall = value; }
            get { return _imagesmall; }
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
        public int? IsLock
        {
            set { _islock = value; }
            get { return _islock; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? PubTime
        {
            set { _pubtime = value; }
            get { return _pubtime; }
        }
        #endregion Model

    }
}

