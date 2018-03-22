using System;
namespace ZGD.Model
{
	/// <summary>
	/// Business:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Business
	{
		public Business()
		{}
		#region Model
		private int _id;
		private string _pname;
		private string _plogo;
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
		public string pName
		{
			set{ _pname=value;}
			get{return _pname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string pLogo
		{
			set{ _plogo=value;}
			get{return _plogo;}
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

