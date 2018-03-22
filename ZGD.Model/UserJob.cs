using System;
namespace HX.Model
{
	/// <summary>
	/// UserJob:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class UserJob
	{
		public UserJob()
		{}
		#region Model
		private int _id;
		private string _uname;
		private string _sex;
		private DateTime? _birthday;
		private string _phone;
		private int? _workyear=0;
		private string _files;
		private string _profession;
		private string _curjob;
		private string _city;
		private decimal? _curprice;
		private decimal? _price;
		private string _intime;
		private DateTime? _pubtime= DateTime.Now;
		private int? _status=0;
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
		public string Sex
		{
			set{ _sex=value;}
			get{return _sex;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? Birthday
		{
			set{ _birthday=value;}
			get{return _birthday;}
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
		public int? WorkYear
		{
			set{ _workyear=value;}
			get{return _workyear;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Files
		{
			set{ _files=value;}
			get{return _files;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Profession
		{
			set{ _profession=value;}
			get{return _profession;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CurJob
		{
			set{ _curjob=value;}
			get{return _curjob;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string City
		{
			set{ _city=value;}
			get{return _city;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? CurPrice
		{
			set{ _curprice=value;}
			get{return _curprice;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? Price
		{
			set{ _price=value;}
			get{return _price;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string InTime
		{
			set{ _intime=value;}
			get{return _intime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? PubTime
		{
			set{ _pubtime=value;}
			get{return _pubtime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Status
		{
			set{ _status=value;}
			get{return _status;}
		}
		#endregion Model

	}
}

