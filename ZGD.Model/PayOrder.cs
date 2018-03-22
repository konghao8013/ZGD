using System;
namespace ZGD.Model
{
    /// <summary>
    /// PayOrder:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class PayOrder
    {
        public PayOrder()
        { }
        #region Model
        private int _id;
        private int? _assid = 0;
        private decimal? _paymoney = 0M;
        private string _paymethod = "alipay";
        private string _payid;
        private string _username;
        private string _phone;
        private string _remark;
        private string _vCode;
        private string _html;
        private DateTime? _pubtime;
        private DateTime? _paytime;
        private int? _status = 0;
        private int? _asstype = 1;
        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 关联ID
        /// </summary>
        public int? AssID
        {
            set { _assid = value; }
            get { return _assid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? PayMoney
        {
            set { _paymoney = value; }
            get { return _paymoney; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PayMethod
        {
            set { _paymethod = value; }
            get { return _paymethod; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string vCode
        {
            set { _vCode = value; }
            get { return _vCode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string HtmlText
        {
            set { _html = value; }
            get { return _html; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PayID
        {
            set { _payid = value; }
            get { return _payid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string UserName
        {
            set { _username = value; }
            get { return _username; }
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
        /// <summary>
        /// 
        /// </summary>
        public DateTime? PayTime
        {
            set { _paytime = value; }
            get { return _paytime; }
        }
        /// <summary>
        /// 0默认未支付  1已支付
        /// </summary>
        public int? Status
        {
            set { _status = value; }
            get { return _status; }
        }
        /// <summary>
        /// 关联类型  1活动 
        /// </summary>
        public int? AssType
        {
            set { _asstype = value; }
            get { return _asstype; }
        }
        #endregion Model

    }
}

