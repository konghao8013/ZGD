using System;
namespace ZGD.Model
{
	/// <summary>
	/// Words:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Words
	{
		public Words()
		{}
		#region Model
		private int _id;
		private string _uname;
		private string _phone;
		private string _con;
		private DateTime? _pubtime= DateTime.Now;
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
		public string uName
		{
			set{ _uname=value;}
			get{return _uname;}
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
		public string Con
		{
			set{ _con=value;}
			get{return _con;}
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

