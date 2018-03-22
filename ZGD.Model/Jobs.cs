using System;
namespace HX.Model
{
	/// <summary>
	/// Jobs:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Jobs
	{
		public Jobs()
		{}
		#region Model
		private int _id;
		private string _workaddr;
		private decimal? _price;
		private string _age;
		private string _education;
		private string _workyear;
		private int? _jobtype=0;
		private string _job;
		private string _jobmust;
		private int? _jobcount=0;
		private int? _status=0;
		private DateTime? _updatetime= DateTime.Now;
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
		public string WorkAddr
		{
			set{ _workaddr=value;}
			get{return _workaddr;}
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
		public string Age
		{
			set{ _age=value;}
			get{return _age;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Education
		{
			set{ _education=value;}
			get{return _education;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string WorkYear
		{
			set{ _workyear=value;}
			get{return _workyear;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? JobType
		{
			set{ _jobtype=value;}
			get{return _jobtype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Job
		{
			set{ _job=value;}
			get{return _job;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string JobMust
		{
			set{ _jobmust=value;}
			get{return _jobmust;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? JobCount
		{
			set{ _jobcount=value;}
			get{return _jobcount;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Status
		{
			set{ _status=value;}
			get{return _status;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? UpdateTime
		{
			set{ _updatetime=value;}
			get{return _updatetime;}
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

