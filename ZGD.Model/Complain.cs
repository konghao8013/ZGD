using System;
namespace ZGD.Model
{
    /// <summary>
    /// Message:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class Complain
    {
        public Complain()
        { }
        #region Model
        private int _id;
        private string _uname;
        private string _phone;
        private string _area;
        private int? _type = 1;
        private string _mcon;
        private DateTime? _pubtime = DateTime.Now;
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
        public string uName
        {
            set { _uname = value; }
            get { return _uname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string vCode
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string Phone
        {
            set { _phone = value; }
            get { return _phone; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Area
        {
            set { _area = value; }
            get { return _area; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Type
        {
            set { _type = value; }
            get { return _type; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string mCon
        {
            set { _mcon = value; }
            get { return _mcon; }
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

