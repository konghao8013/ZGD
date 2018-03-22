using System;
namespace ZGD.Model
{
	/// <summary>
	/// ServiceTools:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class ServiceTools
	{
		public ServiceTools()
		{}
		#region Model
		private int _id;
		private int? _type=1;
		private string _title;
		private string _num;
		private DateTime? _pubtime;
		private int? _stuats=0;
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
		public int? Type
		{
			set{ _type=value;}
			get{return _type;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Title
		{
			set{ _title=value;}
			get{return _title;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Num
		{
			set{ _num=value;}
			get{return _num;}
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
		public int? Stuats
		{
			set{ _stuats=value;}
			get{return _stuats;}
		}
		#endregion Model

	}
}

