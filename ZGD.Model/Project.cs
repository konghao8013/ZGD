using System;
using System.Collections.Generic;
namespace ZGD.Model
{
    /// <summary>
    /// Project:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class Project
    {
        public Project()
        { }
        #region Model
        private int _id;
        private string _title;
        private string _keyword;
        private string _description;
        private string _imgurl;
        private string _imagesmall;
        private int _designerid;
        private string _designername;
        private int? _fgid = 0;
        private int? _hxid = 0;
        private int? _jgid = 0;
        private int? _mjid = 0;
        private string _remark;
        private int _click = 0;
        private int _istop = 0;
        private int _houseid = 0;
        private int _islock = 0;
        private DateTime _pubtime = DateTime.Now;
        private decimal _Price;
        private decimal _Area;
        private string _TDFiles;
        public string fg { get; set; }
        public string hx { get; set; }
        public string mj { get; set; }
        public string jg { get; set; }
        /// <summary>
        /// 3D效果
        /// </summary>
        public string TDFiles
        {
            set { _TDFiles = value; }
            get { return _TDFiles; }
        }
        /// <summary>
        /// 价格
        /// </summary>
        public decimal Price
        {
            set { _Price = value; }
            get { return _Price; }
        }
        /// <summary>
        /// 具体面积
        /// </summary>
        public decimal Area
        {
            set { _Area = value; }
            get { return _Area; }
        }
        /// <summary>
        /// 自增ID PK
        /// </summary>
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 所属小区
        /// </summary>
        public int HouseId
        {
            set { _houseid = value; }
            get { return _houseid; }
        }
        /// <summary>
        /// 项目标题
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
        /// 图片路径
        /// </summary>
        public string ImgUrl
        {
            set { _imgurl = value; }
            get { return _imgurl; }
        }
        /// <summary>
        /// 图片缩略图
        /// </summary>
        public string ImageSmall
        {
            set { _imagesmall = value; }
            get { return _imagesmall; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int DesignerId
        {
            set { _designerid = value; }
            get { return _designerid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DesignerName
        {
            set { _designername = value; }
            get { return _designername; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? fgID
        {
            set { _fgid = value; }
            get { return _fgid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? hxID
        {
            set { _hxid = value; }
            get { return _hxid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? gnID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? jgID
        {
            set { _jgid = value; }
            get { return _jgid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? mjID
        {
            set { _mjid = value; }
            get { return _mjid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 点击次数
        /// </summary>
        public int Click
        {
            set { _click = value; }
            get { return _click; }
        }
        /// <summary>
        /// 是否置顶
        /// </summary>
        public int IsTop
        {
            set { _istop = value; }
            get { return _istop; }
        }
        /// <summary>
        /// 是否锁定
        /// </summary>
        public int IsLock
        {
            set { _islock = value; }
            get { return _islock; }
        }
        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime PubTime
        {
            set { _pubtime = value; }
            get { return _pubtime; }
        }
        #endregion Model

    }
}

