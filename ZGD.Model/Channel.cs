using System;
namespace ZGD.Model
{
    /// <summary>
    /// Channel:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class Channel
    {
        public Channel()
        { }
        #region Model
        private int _id;
        private string _title;
        private int _parentid;
        private string _classlist;
        private int _classlayer;
        private int _sortid = 0;
        private string _pageurl;
        private int _kindid;
        private int _isdelete;
        /// <summary>
        /// 
        /// </summary>
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 栏目名称
        /// </summary>
        public string Title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 父栏目ID
        /// </summary>
        public int ParentId
        {
            set { _parentid = value; }
            get { return _parentid; }
        }
        /// <summary>
        /// 子栏目ID列表
        /// </summary>
        public string ClassList
        {
            set { _classlist = value; }
            get { return _classlist; }
        }
        /// <summary>
        /// 栏目深度
        /// </summary>
        public int ClassLayer
        {
            set { _classlayer = value; }
            get { return _classlayer; }
        }
        /// <summary>
        /// 排序数字
        /// </summary>
        public int SortId
        {
            set { _sortid = value; }
            get { return _sortid; }
        }
        /// <summary>
        /// 栏目管理地址
        /// </summary>
        public string PageUrl
        {
            set { _pageurl = value; }
            get { return _pageurl; }
        }
        /// <summary>
        /// 栏目自定义数字
        /// </summary>
        public int KindId
        {
            set { _kindid = value; }
            get { return _kindid; }
        }
        /// <summary>
        /// 是否禁用
        /// </summary>
        public int IsDelete
        {
            set { _isdelete = value; }
            get { return _isdelete; }
        }
        public string ImgUrl { get; set; }
        #endregion Model

    }
}

