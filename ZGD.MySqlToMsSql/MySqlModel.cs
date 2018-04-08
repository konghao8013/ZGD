using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZGD.MySqlToMsSql
{

    /// <summary>
    /// article:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class articleImage
    {
        public int imgId { get; set; }
        public int articleId { get; set; }
        public string imgSrc { get; set; }
        public string imgNote { get; set; }
        public DateTime createTime { get; set; }
    }

    /// <summary>
    /// article:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class articleCol
    {
        public int articleColumnRelId { get; set; }
        public int articleId { get; set; }
        public int columnId { get; set; }
    }

    /// <summary>
    /// article:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class article
    {
        public article()
        { }
        #region Model
        private int _articleid;
        private int? _userid;
        private string _articleshouldertitle;
        private string _articlesubhead;
        private string _articletitle;
        private string _publishtime;
        private string _keyword;
        private string _author;
        private int? _articlestate;
        private string _oneview;
        private string _twoview;
        private string _onetime;
        private string _towtime;
        private string _articlenote;
        private int? _istop;
        private int? _viewtimes;
        private string _articlecontent;
        private string _submitunit;
        private DateTime _submittime;
        /// <summary>
        /// auto_increment
        /// </summary>
        public int articleId
        {
            set { _articleid = value; }
            get { return _articleid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? userId
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string articleShoulderTitle
        {
            set { _articleshouldertitle = value; }
            get { return _articleshouldertitle; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string articleSubhead
        {
            set { _articlesubhead = value; }
            get { return _articlesubhead; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string articleTitle
        {
            set { _articletitle = value; }
            get { return _articletitle; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string publishTime
        {
            set { _publishtime = value; }
            get { return _publishtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string keyWord
        {
            set { _keyword = value; }
            get { return _keyword; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string author
        {
            set { _author = value; }
            get { return _author; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? articleState
        {
            set { _articlestate = value; }
            get { return _articlestate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string oneView
        {
            set { _oneview = value; }
            get { return _oneview; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string twoView
        {
            set { _twoview = value; }
            get { return _twoview; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string oneTime
        {
            set { _onetime = value; }
            get { return _onetime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string towTime
        {
            set { _towtime = value; }
            get { return _towtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string articleNote
        {
            set { _articlenote = value; }
            get { return _articlenote; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? isTop
        {
            set { _istop = value; }
            get { return _istop; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? viewTimes
        {
            set { _viewtimes = value; }
            get { return _viewtimes; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string articleContent
        {
            set { _articlecontent = value; }
            get { return _articlecontent; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string submitUnit
        {
            set { _submitunit = value; }
            get { return _submitunit; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime submitTime
        {
            set { _submittime = value; }
            get { return _submittime; }
        }
        #endregion Model
    }
}
