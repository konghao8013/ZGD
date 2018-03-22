using System;
namespace ZGD.Model
{
    /// <summary>
    /// Photos:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class Photos
    {
        public Photos()
        { }
        #region Model
        private int _id;
        private int _year;
        private int _type;
        private int _pType;
        private string _imgurl;
        private string _photoname;
        private string _photocon;
        private int? _sort = 0;
        private DateTime? _pubtime = DateTime.Now;
        private int? _istop;
        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        public int Year
        {
            set { _year = value; }
            get { return _year; }
        }
        /// <summary>
        /// 1工艺  2材料  3荣誉
        /// </summary>
        public int Type
        {
            set { _type = value; }
            get { return _type; }
        }
        /// <summary>
        /// 1文明施工 2水电工程 3漆工壁纸 4家装阶段
        /// </summary>
        public int pType
        {
            set { _pType = value; }
            get { return _pType; }
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
        public string PhotoName
        {
            set { _photoname = value; }
            get { return _photoname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PhotoCon
        {
            set { _photocon = value; }
            get { return _photocon; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Sort
        {
            set { _sort = value; }
            get { return _sort; }
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
        public int? IsTop
        {
            set { _istop = value; }
            get { return _istop; }
        }
        #endregion Model

    }
}

