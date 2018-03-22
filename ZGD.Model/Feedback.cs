using System;
namespace ZGD.Model
{
    /// <summary>
    /// Feedback:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class Feedback
    {
        public Feedback()
        { }
        #region Model
        private int _id;
        private int _classid;
        private int _classid2;
        private string _username;
        private string _usertel;
        private string _userqq;
        private string _house = "";
        private decimal _area;
        private string _yaoqiu;
        private string _remark;
        private string _sex;
        private DateTime _addtime = DateTime.Now;
        /// <summary>
        /// 自增ID PK
        /// </summary>
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }
        public int fType
        {
            set;
            get;
        }
        public int hType
        {
            set;
            get;
        }
        public int caclType
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public int ClassId
        {
            set { _classid = value; }
            get { return _classid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int ClassId2
        {
            set { _classid2 = value; }
            get { return _classid2; }
        }
        /// <summary>
        /// 地区
        /// </summary>
        public string AreaAddr
        {
            set;
            get;
        }
        /// <summary>
        /// 用户昵称
        /// </summary>
        public string UserName
        {
            set { _username = value; }
            get { return _username; }
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
        public string Money
        {
            set;
            get;
        }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string UserTel
        {
            set { _usertel = value; }
            get { return _usertel; }
        }
        /// <summary>
        /// 联系QQ
        /// </summary>
        public string UserQQ
        {
            set { _userqq = value; }
            get { return _userqq; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string House
        {
            set { _house = value; }
            get { return _house; }
        }
        /// <summary>
        /// 面积
        /// </summary>
        public decimal Area
        {
            set { _area = value; }
            get { return _area; }
        }
        public decimal Price
        {
            set;
            get;
        }
        /// <summary>
        /// 留言内容
        /// </summary>
        public string YaoQiu
        {
            set { _yaoqiu = value; }
            get { return _yaoqiu; }
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
        public string Sex
        {
            set { _sex = value; }
            get { return _sex; }
        }
        /// <summary>
        /// 留言时间
        /// </summary>
        public DateTime AddTime
        {
            set { _addtime = value; }
            get { return _addtime; }
        }
        #endregion Model

    }
}

