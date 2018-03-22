using System;
namespace ZGD.Model
{
	/// <summary>
	/// ActivityJoin:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class ActivityJoin
	{
		public ActivityJoin()
		{}
		#region Model
		private int _id;
		private int _aid=0;
		private string _username;
		private string _phone;
		private string _qq;
		private string _house;
		private string _address;
		private string _remark;
        private DateTime? _pubtime = DateTime.Now;
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
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int aID
		{
			set{ _aid=value;}
			get{return _aid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string UserName
		{
			set{ _username=value;}
			get{return _username;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Phone
		{
			set{ _phone=value;}
			get{return _phone;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string QQ
		{
			set{ _qq=value;}
			get{return _qq;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string House
		{
			set{ _house=value;}
			get{return _house;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Address
		{
			set{ _address=value;}
			get{return _address;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? PubTime
		{
			set{ _pubtime=value;}
			get{return _pubtime;}
		}
		#endregion Model

	}
}

