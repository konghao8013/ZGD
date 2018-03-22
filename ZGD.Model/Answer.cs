using System;
namespace ZGD.Model
{
    /// <summary>
    /// Answer:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class Answer
    {
        public Answer()
        { }
        #region Model
        private int _id;
        private int? _userid = 0;
        private int _questionid;
        private string _contents;
        private int? _islock = 0;
        private int? _isaccept = 0;
        private DateTime? _pubtime = DateTime.Now;
        public string ImgUrl { get; set; }
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
        public int QuestionId
        {
            set { _questionid = value; }
            get { return _questionid; }
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
        public int? IsLock
        {
            set { _islock = value; }
            get { return _islock; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? IsAccept
        {
            set { _isaccept = value; }
            get { return _isaccept; }
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

