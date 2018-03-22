using System;
namespace ZGD.Model
{
    /// <summary>
    /// Repair:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class Repair
    {
        public Repair()
        { }
        #region Model
        private int _id;
        private string _htname;
        private string _sex;
        private string _uname;
        private string _phone;
        private string _address;
        private string _manager;
        private string _des;
        private string _workdate;
        private string _remark;
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
        public string vCode
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string htName
        {
            set { _htname = value; }
            get { return _htname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Sex
        {
            set { _sex = value; }
            get { return _sex; }
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
        public string Phone
        {
            set { _phone = value; }
            get { return _phone; }
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
        public string Manager
        {
            set { _manager = value; }
            get { return _manager; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Des
        {
            set { _des = value; }
            get { return _des; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string WorkDate
        {
            set { _workdate = value; }
            get { return _workdate; }
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

